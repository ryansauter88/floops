using System;
using Godot;

[GlobalClass]
public partial class State : Node {
    [Export] public string animationName;
    [Export] public float torque;
    public Node2D parent;
    public RigidBody2D body;
    public AnimatedSprite2D animations;
    public Controller controller;
    public float rotationInput;
    public string currentAnim;

    public override void _Ready()
    {
        base._Ready();
    }
    public virtual void Enter() {
        // animations.Play(animationName);
    }
    public virtual void Exit() {
    }

    public virtual State PhysicsProcess(float delta) {
        return null;
    }
    public virtual State Process(float delta) {
        return null;
    }
    public virtual State ProcessInput(InputEvent @event) {
        return null;
    }

    public float GetMovementFloat() {
        return controller.GetMovementFloat();
    }
    public bool GetAttackInput() {
        return controller.WantsAttack();
    }
    public bool GetDashInput() {
        return controller.WantsDash();
    }
}