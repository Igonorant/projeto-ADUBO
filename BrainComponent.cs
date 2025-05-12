using Godot;
using System;
using System.Diagnostics;

[GlobalClass]
public partial class BrainComponent : Node
{
    [Export] private Timer stunTimer;
    
    protected bool isStunned;

    [Export] protected Hurtbox hurtbox;

    protected Character parentCharacter;

    public override void _Ready()
    {
        parentCharacter = GetParent<Character>();
        
        hurtbox.Connect(Hurtbox.SignalName.HurtboxHit, new Callable(this, nameof(ReceiveStunInfo)));
    
        stunTimer.Timeout += EndStun;
    
    }

    public virtual Vector2 GetMovingDirection() { return Vector2.Zero; }

    public virtual Vector2 GetAttackDirection() { return Vector2.Zero; }

    public void ReceiveStunInfo(EffectPackage effect)
    {
        if(effect.stunTime > 0)
        {
            Stun(effect.stunTime);
        }
    }

    protected void Stun(float duration)
    {
        stunTimer.WaitTime = duration;
        stunTimer.Start();
        isStunned = true;
    }
    
    protected void EndStun()
    {
        isStunned = false;
        stunTimer.Stop();
    }

}
