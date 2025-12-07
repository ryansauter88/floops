using Godot;

public partial class FullCharge : State
{
    // [Export] State dashing;
    // [Export] State halfCharge;
    // [Export] State noCharge;
    // [Export] State inactiveCharge;

    public override void _Ready()
    {
        base._Ready();
    }
    public override State PhysicsProcess(float delta)
    {
        body = parent.GetNode<RigidBody2D>("PlayerBody");
        ProcessRotation(body);
        return null;
    }

        public void ProcessRotation(RigidBody2D rigidBody)
    {
        rotationInput = controller.GetMovementFloat();
        rigidBody.ApplyTorque(rotationInput * torque);
    }
}