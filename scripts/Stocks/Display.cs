using System;
using System.Collections.Generic;
using Godot;

public partial class Display : GridContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		GD.Print(this.GetChildren());
        //Label stockFromGodot = GetNode<Label>("CurrentStock1");
        foreach(Label stockFromGodot in GetChildren())
        {
            GD.Print(stockFromGodot.Name);
            GD.Print("There are nodes!");
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
