using Godot;

public partial class Inactive : State
{
    [Export] State dashing;
    [Export] State halfCharge;
    [Export] State noCharge;
    [Export] State fullCharge;

    public override State PhysicsProcess(float delta)
    {
        // ProcessRotation();
        return null;
    }
}