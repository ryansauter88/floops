using Godot;

public partial class Dashing : State
{
    [Export] State fullCharge;
    [Export] State halfCharge;
    [Export] State noCharge;
    [Export] State inactive;
    [Export] float dashForce = 30;
    [Export] int dashDuration = 15;
    private int dashCurrentTimer = 0;

    public override void Enter()
    {
        base.Enter();
        dashCurrentTimer = dashDuration + 1;
        controller.dashPress = false;
    }

    public override State PhysicsProcess(float delta)
    {
        body = parent.GetNode<RigidBody2D>("PlayerBody");
        ProcessDash(body);
        ProcessRotation(body);
        dashCurrentTimer -= 1;
        if (dashCurrentTimer <= 0)
        {
            if (parent.dashTank >= 2) {return fullCharge;}
            if (parent.dashTank == 1) {return halfCharge;}
            if (parent.dashTank <= 0) {return noCharge;}
        }
        return null;
    }

    public void ProcessDash(RigidBody2D rigidBody)
    {
        // applies a force (which is reduced by the percentage of current frame of dash over total dash frames) in the direction the arrow is pointing
        float adjustedDashForce = dashForce * dashCurrentTimer / dashDuration;
        Vector2 forceVector = new Vector2(Mathf.Cos(rigidBody.Rotation - Mathf.Pi/2),Mathf.Sin(rigidBody.Rotation - Mathf.Pi/2 )) * adjustedDashForce;

        rigidBody.ApplyCentralForce(forceVector);
    }
    public void ProcessRotation(RigidBody2D rigidBody)
    {
        rotationInput = controller.GetMovementFloat();
        GD.Print("ROTATION INPUT " + rotationInput);
        GD.Print("ANGULAR VELOCITY " + rigidBody.AngularVelocity);
        GD.Print("LINEAR VELOCITY " + rigidBody.LinearVelocity);
        GD.Print("INERTIA " + rigidBody.Inertia);
        rigidBody.ApplyTorque(rotationInput * torque);
    }
}