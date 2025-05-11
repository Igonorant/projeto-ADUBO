using Godot;
using System;

public partial class ShortRangeHit : Area2D
{
    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        //AreaExited += OnAreaExited;
    }

    private void OnAreaEntered(Area2D other)
    {
            GD.Print("sensor pegou overlap");
    
        GetParent<AttackComponent>().OnBodyEntered(other);
    }

    //private void OnAreaExited(Area2D other)
    //{
    //    GD.Print($"Exited by: {other.Name}");
    //}
}