using System;
using System.ComponentModel.DataAnnotations;
using Godot;

public partial class GamemodeCore : Node
{
    public void StartGame()
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
                state.Transform = new Transform2D(0,player.startPos);
                body._IntegrateForces(state);
                body.SetDeferred("Freeze", false);
            }
            if (children[i].Name == "BallObject")
            {
                Node2D ball = (Node2D)children[i];
                RigidBody2D body = ball.GetNode<RigidBody2D>("BallBody");
                PhysicsDirectBodyState2D state = PhysicsServer2D.BodyGetDirectState(body.GetRid());
                state.Sleeping = true;
                state.Transform = new Transform2D(0,new Vector2(960f,350f));
                body._IntegrateForces(state);
                body.SetDeferred("Freeze", false);
            }
        }
    }
    
    public void EndGame()
    {
        
    }

    public void BallScored()
    {
        // set all players to inactive
        // celebration effects go here?
        // update scoreboard
        // reset positions

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
                body.SetDeferred("Freeze", true);
            }
            GetNode<Timer>("StartGameTimer").Start();
        }
        // re-engage starting sequence (probably a function in this script)
    }
    public void StartGameTimerTimeout()
    {
        StartGame();
    }
}