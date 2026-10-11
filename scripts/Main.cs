using Godot;
using System;

public partial class Main : Node2D
{
	private bool DevToolsToggledOn = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("OpenDevTools"))
        {
            if (DevToolsToggledOn == false)
            {
				DevToolsToggledOn = true;
                GD.Print("Dev Tools Opened!");
            } else if (DevToolsToggledOn == true)
            {
                DevToolsToggledOn = false;
                GD.Print("Dev Tools Closed!");
			}
		}
	}
}
