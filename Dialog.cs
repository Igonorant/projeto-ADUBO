using Godot;
using System;

public partial class Dialog : Control
{
    Label label;


    public override void _Ready()
    {
        label = GetNode<Label>("%DisplayText");
    }


    public void SetFontSize(int size)
    {
        label.AddThemeFontSizeOverride("font_size", size);
    }

    public void SetDisplayText(String text)
    {
        label.Text = text;
    }

}
