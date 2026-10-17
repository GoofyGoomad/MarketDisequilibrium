using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;


public partial class Display : GridContainer
{
	[Export]
	private Stocks _stocksNode;
	int pageNum = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _stocksNode.StocksReady += DisplayStocks;
        for (int i = 1 * pageNum; i <= 6; i++)
        {
			Label godotStock = new Label();
			godotStock.Name = $"CurrentStock{i}";
			godotStock.Text = "Empty";
            AddChild(godotStock);

            Label stockBuyPrice = new Label();
            stockBuyPrice.Name = $"BuyPrice{i}";
            stockBuyPrice.Text = "cheese";
            godotStock.AddChild(stockBuyPrice);
            stockBuyPrice.OffsetTransformEnabled = true;
            //stockBuyPrice.Offsetoffset_transform_position
            
		}
        //GetNode<Label>("CurrentStock6").QueueFree();
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
            Label stockBuyPrice = stockFromGodot.GetNode<Label>($"BuyPrice{stockNum}");
            GD.Print(stockFromGodot.GetGlobalTransform().Y + "and" + stockFromGodot.GetGlobalTransform().X);
            stockBuyPrice.Position = new Vector2 (0 - 70, 0 + 70);
            GD.Print(stockNum);
            stockFromGodot.Text = SqliteHelpers.GetValue("Stocks", "Id", stockNum);
        }
	}
}
