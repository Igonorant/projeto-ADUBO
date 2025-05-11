using Godot;
using System;

[GlobalClass]
public partial class BrainComponent : Node
{

    [Export] protected Hurtbox hurtbox;

    protected Character parentCharacter;

    public override void _Ready()
    {
        parentCharacter = GetParent<Character>();
    }

    public virtual Vector2 GetMovingDirection() { return Vector2.Zero; }

    public virtual Vector2 GetAttackDirection() { return Vector2.Zero; }

}
