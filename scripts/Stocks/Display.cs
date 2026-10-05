using System;
using System.Collections.Generic;
using Godot;

public partial class Display : GridContainer
{
    [Export]
    private Stocks _stocksNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        _stocksNode.StocksReady += DisplayStocks;
        
    }
    public void DisplayStocks()
    {
        int pageNum = 1;
        foreach(Label stockFromGodot in GetChildren())
        {
            string stockNum = stockFromGodot.Name;
            stockNum = stockNum.Replace("CurrentStock", "");
            int stockNumInt = Int32.Parse(stockNum);
            stockFromGodot.Text = Stocks.stocksDict[stockNumInt];
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
