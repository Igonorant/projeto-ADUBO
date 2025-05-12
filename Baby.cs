using Godot;
using System;

public partial class Baby : Character
{
    [Export] public bool enableDebug = false;
    [Export] public CharacterBody2D mommy;
    [Export] public float maxDistanceToMommy = 200.0f;
    [Export] public float babySpeed = 50.0f;
    [Export] public float wanderingTime = 2.0f;
    [Export] public float idleTime = 1.0f;
    [Export] public float followingTime = 2.0f;

    private RandomNumberGenerator rng;

    private Vector2 currentDirection = Vector2.Zero;

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

    private AnimationPlayer animation;

    private PackedScene debugLabelScene = (PackedScene)GD.Load("uid://bexfqv8srwl01");
    private Label debugLabel = null;


    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        rng.Randomize();

        idleTimer = (Timer)FindChild("IdleTimer");
        wanderingTimer = (Timer)FindChild("WanderingTimer");
        followingTimer = (Timer)FindChild("FollowingTimer");
        idleTimer.WaitTime = idleTime;
        wanderingTimer.WaitTime = wanderingTime;
        followingTimer.WaitTime = followingTime;
        idleTimer.Timeout += OnIdleTimerTimeout;
        wanderingTimer.Timeout += OnWanderingTimerTimeout;
        followingTimer.Timeout += OnFollowingTimerTimeout;


        currentState = State.IDLE;
        idleTimer.Start();

        animation = (AnimationPlayer)FindChild("AnimationPlayer");

        if (enableDebug)
        {
            debugLabel = debugLabelScene.Instantiate<Label>();
            AddChild(debugLabel);
            debugLabel.Scale = new Vector2(1.0f / Scale.X, 1.0f / Scale.Y);
            debugLabel.Position = new Vector2(0.0f, -35.0f);
        }
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
        SelectAnimation();
        MoveAndSlide();
    }

    private void SelectAnimation()
    {
        Vector2 velocityAbs = Velocity.Abs();
        if (Velocity.IsZeroApprox())
        {
            animation.Play("idle");
        }
        else
        {
            if (velocityAbs.X > velocityAbs.Y)
            {
                // Moving right/left
                if (Velocity.X >= 0.0f)
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
                if (Velocity.Y <= 0.0f)
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
        if (IsTakingDamage())
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
        return (mommy.GlobalPosition - GlobalPosition).Length() > maxDistanceToMommy;
    }

    private void HandleIdleState()
    {
        Velocity = Vector2.Zero;
    }

    private void HandleWanderingState()
    {
        if (currentDirection.IsZeroApprox())
        {
            currentDirection.X = rng.RandfRange(-1.0f, 1.0f);
            currentDirection.Y = rng.RandfRange(-1.0f, 1.0f);
            currentDirection = currentDirection.Normalized();
        }
        Velocity = currentDirection * babySpeed;
    }

    private void HandleFollowingState()
    {
        currentDirection = Vector2.Zero;
        Vector2 distance_to_mommy = mommy.GlobalPosition - GlobalPosition;
        Velocity = distance_to_mommy.Normalized() * babySpeed;
    }

    private void HandleStunnedState()
    {
        // TODO
        Velocity = Vector2.Zero;
    }
}
