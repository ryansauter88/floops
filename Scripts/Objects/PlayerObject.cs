using Godot;

public partial class PlayerObject : Node2D
{
    public AnimatedSprite2D animations;
    public StateMachine stateMachine;
	public Controller controller;
    public int dashRefillTImer = 0;
    public Vector2 startPos;
    [Export] int dashRefillDuration = 30; //time to refill one charge in the dash tank
    public int dashTank = 2;
    private Timer bufferTimer;
    private string lastInput;
    public override void _Ready()
    {
        animations = null; //GetNode<AnimatedSprite2D>("Animations");
        stateMachine = GetNode<StateMachine>("StateMachine");
        controller = GetNode<Controller>("Controller");
        bufferTimer = GetNode<Timer>("InputBufferTimer");
        stateMachine.init(this, animations, controller);
        startPos = GlobalPosition;
    }

	public override void _UnhandledInput(InputEvent @event) {
		if(@event.IsActionPressed("attack")) {
            lastInput = "attack";
            bufferTimer.Start();
            controller.attackPress = true;
        }
        if(@event.IsActionPressed("dash")) {
            lastInput = "dash";
            bufferTimer.Start();
            controller.dashPress = true;
        }
		stateMachine.ProcessInput(@event);
	}
	public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right")){
            controller.moveInput = Input.GetAxis("move_left","move_right");
        } else {
            controller.moveInput = 0;
        }

        if (dashRefillTImer >= dashRefillDuration)
        {
            dashRefillTImer = 0;
            dashTank += 1;
        } else {dashRefillTImer += 1;}
        if (dashTank > 2) {dashTank = 2;}

        stateMachine.PhysicsProcess((float)delta);
    }

    public override void _Process(double delta) {
        base._Process(delta);
        stateMachine.Process((float)delta);
    }

    public void InputBufferTimerTimeout() {
        if (lastInput == "dash") {controller.dashPress = false;}
        if (lastInput == "attack") {controller.attackPress = false;}
        lastInput = "";
    }
}
