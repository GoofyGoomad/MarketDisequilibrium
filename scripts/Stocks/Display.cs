using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;


public partial class Display : GridContainer
{
	[Export]
	private Stocks _stocksNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _stocksNode.StocksReady += DisplayStocks;
        for (int i = 1; i <= 6; i++)
        {
			Label godotStock = new Label();
			godotStock.Name = $"CurrentStock{i}";
			godotStock.Text = "Empty";
			AddChild(godotStock);
		}
        GetNode<Label>("CurrentStock6").QueueFree();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void DisplayStocks()
	{
        int pageNum = 1;
        GD.Print(GetChildren());
		foreach(Label stockFromGodot in GetChildren())
        {
            string stockNum = stockFromGodot.Name;
            stockNum = stockNum.Replace("CurrentStock", "");
            GD.Print(stockNum);
            stockFromGodot.Text = SqliteHelpers.GetValue("Stocks", "Id", stockNum);
        }
	}
}
