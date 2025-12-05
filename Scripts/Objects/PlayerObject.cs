using Godot;
using System;

public partial class PlayerObject : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")){
            controller.moveInput = Input.GetVector("move_left","move_right","move_up","move_down");
        } else {
            // StandStill();
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
        }

		stateMachine.ProcessInput(@event);
	}
}
