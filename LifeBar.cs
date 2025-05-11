using Godot;
using System;

public partial class LifeBar : Godot.TextureProgressBar
{

HealthComponent healthComponent;

int tempLife;

int maxLife;



    public override void _Ready()
    {
        healthComponent = GetParent<HealthComponent>();
    
        healthComponent.Connect(HealthComponent.SignalName.HealthSet, new Callable(this, nameof(SetHealthValue)));
        healthComponent.Connect(HealthComponent.SignalName.HealthChanged, new Callable(this, nameof(UpdateValueUponChange)));
        healthComponent.Connect(HealthComponent.SignalName.ToggleBar, new Callable(this, nameof(ToggleBar)));
    }
    
    private void UpdateValueUponChange(int valueChange)
    {
        tempLife += valueChange;
        
        if(tempLife > maxLife)
        {
            tempLife = maxLife;
            
        }
    
        if(tempLife < 0)
        {
            tempLife = 0;
        }
        
        Value = tempLife;
        
    }
    
    private void SetHealthValue(int value)
    {
        maxLife = value;
        tempLife =  value;
        
        MaxValue = maxLife;
        Value = tempLife;
    }
    
    private void ToggleBar(bool tog)
    {
        Visible = tog;
    }

}
