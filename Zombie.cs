using Godot;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;



public partial class Zombie : Character
{
    [Export] public bool enableDebug = false;
    [Export] public CharacterBody2D mommy;
    [Export] public CharacterBody2D baby;
    [Export] public float speed = 50.0f;
    [Export] public float chasingMommyDistance = 75.0f;
    [Export] public float attackingRange = 50.0f;

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

    private CharacterBody2D target;

    private PackedScene debugLabelScene = (PackedScene)GD.Load("uid://bexfqv8srwl01");
    private Label debugLabel = null;

    public override void _Ready()
    {
        target = baby;
        if (enableDebug)
        {
            debugLabel = debugLabelScene.Instantiate<Label>();
            AddChild(debugLabel);
            debugLabel.Scale = new Vector2(1.0f / Scale.X, 1.0f / Scale.Y);
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

        MoveAndSlide();
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
        Vector2 distanceToMommy = mommy.GlobalPosition - GlobalPosition;
        if (distanceToMommy.Length() < chasingMommyDistance)
        {
            target = mommy;
        }
        else
        {
            target = baby;
        }
    }

    private bool TargetInAttackRange()
    {
        return (target.GlobalPosition - GlobalPosition).Length() < attackingRange;
    }

    private bool IsTakingDamage()
    {
        // TODO
        return false;
    }

    private void HandleIdleState()
    {
        Velocity = Vector2.Zero;
    }

    private void HandleChasingState()
    {
        Velocity = (target.GlobalPosition - GlobalPosition).Normalized() * speed;
    }

    private void HandleAttackingState()
    {
        // TODO
        Velocity = Vector2.Zero;
    }

    private void HandleStunnedState()
    {
        // TODO
        Velocity = Vector2.Zero;
    }
}
