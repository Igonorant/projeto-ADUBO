using Godot;
using System;

public partial class PlayerSprite : Sprite2D
{
    [Export] private AttackComponent attackComponent;
    
    [Export] private HealthComponent healthComponent;
    
    [Export] private Timer cooldownIndicationTimer;
    
    [Export] private float cooldownIndicationDuration;
    
    [Export] private Timer hurtIndicationTimer;
    
    [Export] private float hurtIndicationDuration;
    
    
    public override void _Ready()
    {
        attackComponent.Connect(AttackComponent.SignalName.CooldownEnd, new Callable(this, nameof(IndicateCooldownEnd)));
    
        healthComponent.Connect(HealthComponent.SignalName.HealthChanged, new Callable(this, nameof(InitiateHurtIndication)));
        
        cooldownIndicationTimer.Timeout += EndCooldownIndication;
        
        hurtIndicationTimer.Timeout += EndHurtIndication;
    
    }

    private void IndicateCooldownEnd()
    {
        this.SelfModulate = new Color(1, 1, 1, 1);
        
        cooldownIndicationTimer.WaitTime = cooldownIndicationDuration;
        
        cooldownIndicationTimer.Start();
    }

    private void EndCooldownIndication()
    {
        this.SelfModulate = new Color(0, 0, 0, 0);
        
        cooldownIndicationTimer.Stop();
    }
    
    private void InitiateHurtIndication(int damage)
    {
        if(damage > 0)
        {
                  this.SelfModulate = new Color(1, 0, 0, 1);
    
        hurtIndicationTimer.WaitTime = hurtIndicationDuration;
        
        hurtIndicationTimer.Start();  
        }
    
    }
    
    private void EndHurtIndication()
    {

    
        this.SelfModulate = new Color(0, 0, 0, 0);
        
        hurtIndicationTimer.Stop();
    }

}
