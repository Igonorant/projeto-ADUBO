using Godot;
using System;

public partial class InteractionHit : Area2D
{    
public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        //AreaExited += OnAreaExited;
    }

    private void OnAreaEntered(Area2D other)
    {  
        GetParent<InteractionComponent>().OnBodyDetected(other);
    }
}
