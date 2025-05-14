using Godot;
using System;

public partial class Game : Node
{

    Dialog mainDialog;


    public override void _Ready()
    {
        mainDialog = GetNode<Dialog>("%Dialog");
        mainDialog.SetDisplayText("SAVE YOUR CHILD AND SURVIVE!!");
        mainDialog.SetFontSize(150);
        GetTree().CreateTimer(2).Timeout += () =>
        {
            mainDialog.Visible = false;
        };
    }

}
