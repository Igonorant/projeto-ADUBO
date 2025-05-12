using Godot;
using System;

public partial class ZombieBrain : BrainComponent
{
    // Debug variables
    [Export] public bool enableDebug = false;
    private PackedScene debugLabelScene = (PackedScene)GD.Load("uid://bexfqv8srwl01");
    private Label debugLabel = null;

    // States
    private enum State
    {
        IDLE,
        CHASING,
        ATTACKING,
        STUNNED
    }
    private String GetStateString(State state)
    {
        switch (state)
        {
            case State.IDLE: return "IDLE";
            case State.CHASING: return "CHASING";
            case State.ATTACKING: return "ATTACKING";
            case State.STUNNED: return "STUNNED";
            default: return "Unknown state";
        }
    }
    private State currentState = State.IDLE;



    private Vector2 last_direction = Vector2.Zero;
    private Character target;
    private float attackRange;



    public override void _Ready()
    {
        base._Ready();
        if (enableDebug)
        {
            debugLabel = debugLabelScene.Instantiate<Label>();
            debugLabel.Scale = new Vector2(1.0f / parentCharacter.Scale.X, 1.0f / parentCharacter.Scale.Y);
            parentCharacter.CallDeferred("add_child", debugLabel);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        ComputeNextState();
        if (debugLabel != null) { debugLabel.Text = GetStateString(currentState) + "\n" + target.Name; }

        switch (currentState)
        {
            case State.IDLE: HandleIdleState(); break;
            case State.CHASING: HandleChasingState(); break;
            case State.ATTACKING: HandleAttackingState(); break;
            case State.STUNNED: HandleStunnedState(); break;
        }

    }

    private void ComputeNextState()
    {
        if (IsTakingDamage())
        {
            currentState = State.STUNNED;
            return;
        }

        if (TargetInAttackRange())
        {
            currentState = State.ATTACKING;
            return;
        }

        currentState = State.CHASING;
    }

    private bool TargetInAttackRange()
    {
        return (target.GlobalPosition - parentCharacter.GlobalPosition).Length() < attackRange;
    }

    private bool IsTakingDamage()
    {
        // TODO
        return false;
    }

    private void HandleIdleState()
    {
        last_direction = Vector2.Zero;
    }

    private void HandleChasingState()
    {
        last_direction = (target.GlobalPosition - parentCharacter.GlobalPosition).Normalized();
    }

    private void HandleAttackingState()
    {
        // TODO
        last_direction = Vector2.Zero;
    }

    private void HandleStunnedState()
    {
        // TODO
        last_direction = Vector2.Zero;
    }

    public override Vector2 GetMovingDirection()
    {

        return last_direction;
    }

    public override Vector2 GetAttackDirection() { return last_direction; }

    public void SetTarget(Character newTarget)
    {
        target = newTarget;
    }

    public void SetAttackRange(float range)
    {
        attackRange = range;
    }

}
