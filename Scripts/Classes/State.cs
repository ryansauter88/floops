using Godot;

[GlobalClass]
public partial class State : Node {
    [Export] public string animationName;
    public Node2D parent;
    public RigidBody2D body;
    public AnimatedSprite2D animations;
    public Controller controller;
    public Vector2 movement;
    public string currentAnim;

    public override void _Ready()
    {
        body = parent.GetNode<RigidBody2D>("PlayerBody");
        base._Ready();
    }
    public virtual void Enter() {
        animations.Play(animationName);
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

    public Vector2 GetMovementInput() {
        return controller.GetMovementDirection();
    }
    public bool GetAttackInput() {
        return controller.WantsAttack();
    }
    public bool GetBoostInput() {
        return controller.WantsBoost();
    }
}