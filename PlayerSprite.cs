using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public partial class PlayerSprite : CharacterSprite
{
    [Export] private AttackComponent attackComponent;
    
    [Export] private Timer cooldownIndicationTimer;
    
    [Export] private float cooldownIndicationDuration;
    
    [Export] private Material cooldownMaterial;
    
    
    public override void _Ready()
    {
        base._Ready();
    
        attackComponent.Connect(AttackComponent.SignalName.CooldownEnd, new Callable(this, nameof(IndicateCooldownEnd)));
    
        //healthComponent.Connect(HealthComponent.SignalName.HealthChanged, new Callable(this, nameof(InitiateHurtIndication)));
        
        cooldownIndicationTimer.Timeout += EndCooldownIndication;
        
        //hurtIndicationTimer.Timeout += EndHurtIndication;
    
    }

    private void IndicateCooldownEnd()
    {
        GD.Print("Cooldown end");
    
        this.Material = cooldownMaterial;
        
        //this.SelfModulate = new Color(1, 0, 0, 1);
        
        cooldownIndicationTimer.WaitTime = cooldownIndicationDuration;
        
        cooldownIndicationTimer.Start();

    }

    private void EndCooldownIndication()
    {
    
        this.Material = originalMaterial;
        //this.SelfModulate = new Color(0, 0, 0, 0); 
              
        cooldownIndicationTimer.Stop();
    }
    
    
    /*
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
    */
}
