using Godot;

public partial class FullCharge : State
{
    [Export] State dashing;
    [Export] State inactive;

    public override State PhysicsProcess(float delta)
    {
        body = parent.GetNode<RigidBody2D>("PlayerBody");
        ProcessRotation(body);
        return null;
    }

    public override State ProcessInput(InputEvent @event)
    {
        if (GetDashInput()) {return dashing;}
        return null;
    }
    public void ProcessRotation(RigidBody2D rigidBody)
    {
        rotationInput = controller.GetMovementFloat();
        rigidBody.ApplyTorque(rotationInput * torque);
    }
}