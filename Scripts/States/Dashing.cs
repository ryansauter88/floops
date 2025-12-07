using Godot;

public partial class Dashing : State
{
    // [Export] State dashing;
    // [Export] State halfCharge;
    // [Export] State noCharge;
    // [Export] State inactiveCharge;

    public override State PhysicsProcess(float delta)
    {
        body = parent.GetNode<RigidBody2D>("PlayerBody");
        ProcessRotation(body);
        return base.PhysicsProcess(delta);
    }

    public void ProcessRotation(RigidBody2D rigidBody)
    {
        rotationInput = controller.GetMovementFloat();
        rigidBody.ApplyTorque(rotationInput * torque);
    }
}