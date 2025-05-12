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
            GD.Print("sensor pegou overlap");
    
        GetParent<InteractionComponent>().OnBodyDetected(other);
    }
}
