using Godot;
using System;

public partial class PlayerObject : Node2D
{
    public AnimatedSprite2D animations;
    public StateMachine stateMachine;
	public Controller controller;
    public int dashRefillTImer = 0;
    [Export] int dashRefillDuration = 30; //time to refill one charge in the dash tank
    public int dashTank = 2;
    public override void _Ready()
    {
        animations = null; //GetNode<AnimatedSprite2D>("Animations");
        stateMachine = GetNode<StateMachine>("StateMachine");
        controller = GetNode<Controller>("Controller");
        stateMachine.init(this, animations, controller);
    }

	public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")){
            controller.moveInput = Input.GetAxis("move_left","move_right");
        } else {
            controller.moveInput = 0;
        }

        if (dashTank < 2) {dashRefillTImer += 1;}
        if (dashRefillTImer >= dashRefillDuration || dashTank >= 2)
        {
            dashRefillTImer = 0;
            dashTank += 1;
        } else {dashRefillTImer += 1;}
        
        stateMachine.PhysicsProcess((float)delta);
    }

    public override void _Process(double delta) {
        base._Process(delta);
        stateMachine.Process((float)delta);
    }

	public override void _UnhandledInput(InputEvent @event) {
		if(@event.IsActionPressed("attack")) {
            // lastInput = "attack";
            // bufferTimer.Start();
            controller.attackPress = true;
        }
        if(@event.IsActionPressed("dash")) {
            // lastInput = "dash";
            // bufferTimer.Start();
            controller.dashPress = true;
            dashTank -= 1;
        }
		stateMachine.ProcessInput(@event);
	}
}
