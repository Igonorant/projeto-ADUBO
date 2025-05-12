using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;



[GlobalClass]
public partial class HealthComponent : Node2D
{
    private int tempHealth;
    
    [Export] private bool visibleBar;
    
    [Export] private int maxHealth;
    
    [Export] private Hurtbox hurtbox;
   
    [Signal] public delegate void HealthSetEventHandler(int health);    
    
    [Signal] public delegate void HealthChangedEventHandler(int change);
    
    [Signal] public delegate void HealthDepletedEventHandler();
    
    [Signal] public delegate void ToggleBarEventHandler(bool show);
    
    public override void _Ready()
    {
        tempHealth = maxHealth;
        
        EmitSignal(nameof(HealthSet), tempHealth);
        
        hurtbox.Connect(Hurtbox.SignalName.HurtboxHit, new Callable(this, nameof(HurtboxEvent)));
        
    }
    
    private void HurtboxEvent(EffectPackage effect)
    {
        if (effect.damage>0)
        {
            Heal(effect.damage);
        }
        else if(effect.damage<0)
        {
            ReceiveDamage(effect.damage);
        }
    }
    
    private void ReceiveDamage(int dmg)
    {
                GD.Print("DAMAGE: " + dmg);
    
        tempHealth += dmg;
        GD.Print("temp health is" + tempHealth);
        
        if(tempHealth <= 0)
        {
            GD.Print("tempHealth below zero");
            tempHealth = 0;
            Die();
        }
        else
        {
           EmitSignal(nameof(HealthChanged), dmg);         
        }
        
        if(tempHealth < maxHealth)
        {
            if(visibleBar)
            {
                ToggleLifeBar(true);            
            }

        }
    }
    
    private void Heal(int heal)
    {
                    GD.Print("HEAL: " + heal);
    
        tempHealth += heal;
        
        if(tempHealth >= maxHealth)
        {
            tempHealth = maxHealth;
            
            ToggleLifeBar(false);
        }
        
        
          
        EmitSignal(nameof(HealthChanged), heal);
    }
    
    private void Die()
    {
        GD.Print("Health component diagnose: DEAD");
        EmitSignal(nameof(HealthDepleted));
    }
    
    private void ToggleLifeBar(bool show)
    {
        EmitSignal(nameof(ToggleBar), show);
        
    }

}
