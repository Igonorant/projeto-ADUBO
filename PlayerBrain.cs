using Godot;
using System;

public partial class PlayerBrain : BrainComponent
{
    // Debug variables
    [Export] public bool enableDebug = false;
    private PackedScene debugLabelScene = (PackedScene)GD.Load("uid://bexfqv8srwl01");
    private Label debugLabel = null;


    [Signal] public delegate void InteractionOrderedEventHandler();
    private Vector2 last_direction = Vector2.Zero;


    // States
    private enum State
    {
        IDLE,
        MOVING,
        ATTACKING,
        STUNNED
    }
    private String GetStateString(State state)
    {
        switch (state)
        {
            case State.IDLE: return "IDLE";
            case State.MOVING: return "MOVING";
            case State.ATTACKING: return "ATTACKING";
            case State.STUNNED: return "STUNNED";
            default: return "Unknown state";
        }
    }
    private State currentState = State.IDLE;

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
        if (debugLabel != null) { debugLabel.Text = GetStateString(currentState); }

        switch (currentState)
        {
            case State.IDLE: HandleIdleState(); break;
            case State.MOVING: HandleMovingState(); break;
            case State.ATTACKING: HandleAttackingState(); break;
            case State.STUNNED: HandleStunnedState(); break;
        }

        // This probably will need to be moved to a state soon
        if (!isStunned && Input.IsActionJustPressed("interact"))
        {
            EmitSignal(nameof(InteractionOrdered));
        }
    }


    private void ComputeNextState()
    {
        if (isStunned)
        {
            currentState = State.STUNNED;
            return;
        }

        Vector2 movingDirection = ComputeMovingDirection();

        if (Input.IsActionJustPressed("attack"))
        {
            currentState = State.ATTACKING;
            return;
        }

        if (movingDirection.IsZeroApprox())
        {
            currentState = State.IDLE;
            return;
        }

        currentState = State.MOVING;
    }

    private Vector2 ComputeMovingDirection()
    {
        if (!isStunned)
        {
            last_direction = Vector2.Zero;
            if (Input.IsActionPressed("up"))
            {
                last_direction.Y = -1.0f;
            }
            if (Input.IsActionPressed("down"))
            {
                last_direction.Y = 1.0f;
            }
            if (Input.IsActionPressed("left"))
            {
                last_direction.X = -1.0f;
            }
            if (Input.IsActionPressed("right"))
            {
                last_direction.X = 1.0f;
            }

            if (last_direction.IsZeroApprox())
            {
                return Vector2.Zero;
            }

            last_direction = last_direction.Normalized();
            return last_direction;
        }
        else
        {
            return Vector2.Zero;
        }

    }

    public override Vector2 GetAttackDirection() { return last_direction; }

    private void HandleIdleState()
    {
        last_direction = Vector2.Zero;
    }

    private void HandleMovingState()
    {
        // Don't need to do nothing, last direction was already updated.
        // When we have animations, here will be the place to update it.
    }

    private void HandleAttackingState()
    {
        parentCharacter.Attack();
    }

    private void HandleStunnedState()
    {
        last_direction = parentCharacter.knockbackDirection;
    }

    public override Vector2 GetMovingDirection()
    { return last_direction; }

}
