using Godot;
using System;

public partial class Main : Node2D
{
	private bool DevToolsToggledOn = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		GetNode<Camera2D>("MainCamera").Offset = new Vector2(0, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("OpenDevTools"))
        {
			Camera2D mainCamera = GetNode<Camera2D>("MainCamera");
            if (!DevToolsToggledOn)
            {
				DevToolsToggledOn = true;
                GD.Print("Dev Tools Opened!");
                mainCamera.Offset = new Vector2(1920, 0);
            } else if (DevToolsToggledOn)
            {
                DevToolsToggledOn = false;
                GD.Print("Dev Tools Closed!");
                mainCamera.Offset = new Vector2(0, 0);
			}
		}
	}
}