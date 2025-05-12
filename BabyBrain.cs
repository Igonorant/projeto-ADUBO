using Godot;
using System;

public partial class BabyBrain : BrainComponent
{
    [Export] public bool enableDebug = false;

    private RandomNumberGenerator rng;
    private Vector2 currentDirection = Vector2.Zero;
    private Character mommy;
    private float maxDistanceToMommy;

    private Timer idleTimer;
    private Timer wanderingTimer;
    private Timer followingTimer;
    private enum State
    {
        IDLE,
        WANDERING,
        FOLLOWING,
        STUNNED
    }
    private String GetStateString(State state)
    {
        switch (state)
        {
            case State.IDLE: return "IDLE";
            case State.WANDERING: return "WANDERING";
            case State.FOLLOWING: return "FOLLOWING";
            case State.STUNNED: return "STUNNED";
            default: return "Unknown state";
        }
    }
    private State currentState = State.IDLE;

    [Export] private AnimationPlayer animation;


    private PackedScene debugLabelScene = (PackedScene)GD.Load("uid://bexfqv8srwl01");
    private Label debugLabel = null;


    public override void _Ready()
    {
        base._Ready();
        rng = new RandomNumberGenerator();
        rng.Randomize();

        idleTimer = (Timer)FindChild("IdleTimer");
        wanderingTimer = (Timer)FindChild("WanderingTimer");
        followingTimer = (Timer)FindChild("FollowingTimer");
        idleTimer.Timeout += OnIdleTimerTimeout;
        wanderingTimer.Timeout += OnWanderingTimerTimeout;
        followingTimer.Timeout += OnFollowingTimerTimeout;

        currentState = State.IDLE;
        idleTimer.Start();

        if (enableDebug)
        {
            debugLabel = debugLabelScene.Instantiate<Label>();
            debugLabel.Scale = new Vector2(1.0f / parentCharacter.Scale.X, 1.0f / parentCharacter.Scale.Y);
            debugLabel.Position = new Vector2(0.0f, -35.0f);
            parentCharacter.CallDeferred("add_child", debugLabel);
        }
    }

    public void SetTimers(float idleTime, float wanderingTime, float followingTime)
    {
        idleTimer.WaitTime = idleTime;
        wanderingTimer.WaitTime = wanderingTime;
        followingTimer.WaitTime = followingTime;
    }

    public override void _PhysicsProcess(double delta)
    {
        ComputeNextState();

        switch (currentState)
        {
            case State.IDLE: HandleIdleState(); break;
            case State.WANDERING: HandleWanderingState(); break;
            case State.FOLLOWING: HandleFollowingState(); break;
            case State.STUNNED: HandleStunnedState(); break;
        }
    }

    public void SelectAnimation()
    {
        Vector2 velocityAbs = parentCharacter.Velocity.Abs();
        if (parentCharacter.Velocity.IsZeroApprox())
        {
            animation.Play("idle");
        }
        else
        {
            if (velocityAbs.X > velocityAbs.Y)
            {
                // Moving right/left
                if (parentCharacter.Velocity.X >= 0.0f)
                {
                    animation.Play("moving_right");
                }
                else
                {
                    animation.Play("moving_left");
                }
            }
            else
            {
                if (parentCharacter.Velocity.Y <= 0.0f)
                {
                    animation.Play("moving_up");
                }
                else
                {
                    animation.Play("moving_down");
                }
            }
        }
    }

    private void ComputeNextState()
    {
        
        if (IsTakingDamage() || isStunned)
        {
            currentState = State.STUNNED;
        }

        // Other states are select by the timers' timeout signals

        if (debugLabel != null) { debugLabel.Text = GetStateString(currentState); }
    }

    private void OnIdleTimerTimeout()
    {
        if (IsFarFromMommy())
        {
            currentState = State.FOLLOWING;
            followingTimer.Start();
        }
        else
        {
            currentState = State.WANDERING;
            wanderingTimer.Start();
        }

    }

    private void OnWanderingTimerTimeout()
    {
        currentState = State.IDLE;
        idleTimer.Start();
    }

    private void OnFollowingTimerTimeout()
    {
        currentState = State.IDLE;
        idleTimer.Start();
    }

    private bool IsTakingDamage()
    {
        // TODO
        return false;
    }

    private bool IsFarFromMommy()
    {
        return (mommy.GlobalPosition - parentCharacter.GlobalPosition).Length() > maxDistanceToMommy;
    }

    private void HandleIdleState()
    {
        currentDirection = Vector2.Zero;
    }

    private void HandleWanderingState()
    {
        if (currentDirection.IsZeroApprox())
        {
            currentDirection.X = rng.RandfRange(-1.0f, 1.0f);
            currentDirection.Y = rng.RandfRange(-1.0f, 1.0f);
            currentDirection = currentDirection.Normalized();
        }
    }

    private void HandleFollowingState()
    {
        currentDirection = Vector2.Zero;
        currentDirection = (mommy.GlobalPosition - parentCharacter.GlobalPosition).Normalized();
    }

    private void HandleStunnedState()
    {
        // TODO
        currentDirection = Vector2.Zero;
    }


    public void SetMommy(Character character, float maxDistance)
    {
        mommy = character;
        maxDistanceToMommy = maxDistance;
    }

    public override Vector2 GetMovingDirection()
    {
        return currentDirection;
    }
}
