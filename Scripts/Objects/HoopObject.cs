using Godot;

public partial class HoopObject : Node2D
{
    [Export] public int teamFlag = 0; // 0 or 1 based on which side it's on
    public bool ballScored = false;
    private GamemodeCore gamemode;

    public override void _Ready()
    {
        base._Ready();
        Node parent = GetParent();
        gamemode = parent.GetNode<GamemodeCore>("GamemodeCore");
    }

    public void OnScoreAreaEntered(Node2D ball)
    {
        ballScored = true;
        gamemode.BallScored(teamFlag); // maybe have the ball store who hit it last and pass it to this function
    }
}