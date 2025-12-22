using Godot;

public partial class NoCharge : State
{
    [Export] State dashing;
    [Export] State halfCharge;
    [Export] State inactive;

    public override State PhysicsProcess(float delta)
    {
        body = parent.GetNode<RigidBody2D>("PlayerBody");
        ProcessRotation(body);
        if (parent.dashTank == 1) {return halfCharge;}
        return null;
    }

    public override State ProcessInput(InputEvent @event)
    {
        if (GetGravityInput()) {body.GravityScale = -body.GravityScale;controller.gravityChange = false;}
        return null;
    }

    public void ProcessRotation(RigidBody2D rigidBody)
    {
        rotationInput = controller.GetMovementFloat();
        rigidBody.ApplyTorque(rotationInput * torque);
    }
}