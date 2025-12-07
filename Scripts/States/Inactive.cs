using Godot;

public partial class Inactive : State
{
    // [Export] State dashing;
    // [Export] State halfCharge;
    // [Export] State noCharge;
    // [Export] State inactiveCharge;

    public override State PhysicsProcess(float delta)
    {
        // ProcessRotation();
        return base.PhysicsProcess(delta);
    }
}