using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;



[GlobalClass]
public partial class HealthComponent : Node2D
{
    private int tempHealth;
    
    [Export] private bool visibleBar;
    
    [Export] private int maxHealth;
    
    [Export] private float shortLifebarDuration;
    
    [Export] private float longLifebarDuration;
   
    [Signal] public delegate void HealthSetEventHandler(int health);    
    
    [Signal] public delegate void HealthChangedEventHandler(int change);
    
    [Signal] public delegate void HealthDepletedEventHandler();
    
    [Signal] public delegate void ToggleBarEventHandler(bool show);
    
    public override void _Ready()
    {
        tempHealth = maxHealth;
        
        EmitSignal(nameof(HealthSetEventHandler), tempHealth);
        
    }
    
    private void ReceiveDamage(int dmg)
    {
        tempHealth -= dmg;
        
        if(tempHealth <= 0)
        {
            tempHealth = 0;
            Die();
        }
        else
        {
           EmitSignal(nameof(HealthChangedEventHandler), -dmg);         
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
        tempHealth += heal;
        
        if(tempHealth >= maxHealth)
        {
            tempHealth = maxHealth;
            
            ToggleLifeBar(false);
        }
        
        
          
        EmitSignal(nameof(HealthChangedEventHandler), heal);
    }
    
    private void Die()
    {
        EmitSignal(nameof(HealthDepletedEventHandler));
    }
    
    private void ToggleLifeBar(bool show)
    {
        EmitSignal(nameof(ToggleBarEventHandler), show);
        
    }

}
