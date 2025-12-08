using Godot;
using Godot.Collections;
using System;

public partial class StateMachine : Node
{
    [Export] State startingState;
    public State currentState;

    public void init(PlayerObject parent, AnimatedSprite2D animations, Controller controller) {
        Array<Node> childArray = GetChildren();
        for (int i = 0; i < childArray.Count; i++) {
            State child = (State)childArray[i];
            child.parent = parent;
            child.animations = animations;
            child.controller = controller;
        }

        ChangeState(startingState);
    }

    public void ChangeState(State newState) {
        if (currentState != null) {currentState.Exit();}
        currentState = newState;
        currentState.Enter();
    }

    public void PhysicsProcess(float delta) {
        GD.Print("CURRENT STATE -- " + currentState.Name);
        State newState = currentState.PhysicsProcess(delta);
        if (newState != null) {ChangeState(newState);}
    }
    public void Process(float delta) {
        State newState = currentState.Process(delta);
        if (newState != null) {ChangeState(newState);};
    }
    public void ProcessInput(InputEvent @event) {
        State newState = currentState.ProcessInput(@event);
        if (newState != null) {ChangeState(newState);};
    }
}
