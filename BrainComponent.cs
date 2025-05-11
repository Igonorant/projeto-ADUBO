using Godot;
using System;

public partial class BrainComponent : Node
{

    private Character parentCharacter;

    public override void _Ready()
    {
        parentCharacter = GetParent<Character>();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("attack"))
        {
            parentCharacter.Attack();
        }
    }



}
