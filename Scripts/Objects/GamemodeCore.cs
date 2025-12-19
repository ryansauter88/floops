
using Godot;

public partial class GamemodeCore : Node
{
    RichTextLabel timerText;
    RichTextLabel leftScoreText;
    RichTextLabel rightScoreText;
    float gameTime = 18000; // FRAMES (assuming 60fps, i know there's a way to make it frame independent but i'll figure that out later)
    int leftTeamScore = 0;
    int rightTeamScore = 0;
    int playersPerTeam;
    bool stopTimer;
    string selectedMode;

    public override void _Ready()
    {
        timerText = GetParent().GetNode<RichTextLabel>("CanvasObject/TimerObject/TimerText");
        leftScoreText = GetParent().GetNode<RichTextLabel>("CanvasObject/ScoreboardObject/ScoreboardContainer/LeftTeamScoreText");
        rightScoreText = GetParent().GetNode<RichTextLabel>("CanvasObject/ScoreboardObject/ScoreboardContainer/RightTeamScoreText");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (!stopTimer)
        {
            gameTime -=1;
            float realTime = Mathf.Floor(gameTime / 60);
            float minutes = Mathf.Floor(realTime / 60);
            float seconds = realTime - (60 * minutes);
            if (seconds < 10) {timerText.Text = "[b]"+minutes+":0"+seconds+"[/b]";}
            else {timerText.Text = "[b]"+minutes+":"+seconds+"[/b]";}
        }
    }

    public void GameTimerCountdown()
    {
        
    }


    public void StartGame()
    {
        // (after setting up multiplayer)
        // wait for enough players in lobby
        //      set players per team from resource
        //      as players join, sort into teams
        // (once enough players)
        // load information from resource (set by the server / authority ??)
        //      set timer
        //      set player positions / ball position
        // ensure score is at zero
        // play any starting animation or countdown or whatever
        // rock out with our socks out??

    }

    public void EndGame()
    {
        
    }

    public void StartPoint()
    {
        Godot.Collections.Array<Node> children = GetParent().GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].GetType() == new PlayerObject().GetType())
            {
                PlayerObject player = (PlayerObject)children[i];
                RigidBody2D body = player.GetNode<RigidBody2D>("PlayerBody");
                player.stateMachine.ChangeState(player.GetNode<State>("StateMachine/fullcharge"));
                PhysicsDirectBodyState2D state = PhysicsServer2D.BodyGetDirectState(body.GetRid());
                state.Sleeping = true;
                state.Transform = new Transform2D(0, player.startPos);
                state.AngularVelocity = 0;
                state.LinearVelocity = Vector2.Zero;
                body._IntegrateForces(state);
                body.SetDeferred("Freeze", false);
            }
            if (children[i].Name == "BallObject")
            {
                Node2D ball = (Node2D)children[i];
                RigidBody2D body = ball.GetNode<RigidBody2D>("BallBody");
                body.SetCollisionMaskValue(7, true);
                PhysicsDirectBodyState2D state = PhysicsServer2D.BodyGetDirectState(body.GetRid());
                state.Sleeping = true;
                state.Transform = new Transform2D(0, new Vector2(960f, 350f));
                state.AngularVelocity = 0;
                state.LinearVelocity = Vector2.Zero;
                body._IntegrateForces(state);
                body.SetDeferred("Freeze", false);
            }
        }
        stopTimer = false;
    }
    
    public void EndPoint()
    {
        
    }

    public void BallScored(int teamFlag)
    {
        // celebration effects go here?
        // update scoreboard
        if (teamFlag == 0) {
            rightTeamScore += 1;
            rightScoreText.Text = rightTeamScore.ToString();
        }
        if (teamFlag == 1) {
            leftTeamScore += 1;
            leftScoreText.Text = leftTeamScore.ToString();
        }

        // set all players to inactive
        // reset positions
        stopTimer = true;
        Godot.Collections.Array<Node> children = GetParent().GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].GetType() == new PlayerObject().GetType())
            {
                PlayerObject player = (PlayerObject)children[i];
                RigidBody2D body = player.GetNode<RigidBody2D>("PlayerBody");
                player.stateMachine.ChangeState(player.GetNode<State>("StateMachine/inactive"));
                body.SetDeferred("Freeze", true);
            }
            if (children[i].Name == "BallObject")
            {
                Node2D ball = (Node2D)children[i];
                RigidBody2D body = ball.GetNode<RigidBody2D>("BallBody");
                body.SetCollisionMaskValue(7, false);
                body.SetDeferred("Freeze", true);
            }
            GetNode<Timer>("StartPointTimer").Start();
        }
        // re-engage starting sequence (probably a function in this script)
    }
    public void StartPointTimerTimeout()
    {
        StartPoint();
    }
}