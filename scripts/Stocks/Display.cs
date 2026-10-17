using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;


public partial class Display : GridContainer
{
	[Export]
	private Stocks _stocksNode;
	[Export]
	private StockUpdater _stockUpdaterNode;

	int pageNum = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_stocksNode.StocksReady += DisplayStocks;
		_stockUpdaterNode.UpdateStocks += UpdateStockDisplay;
		for (int i = 1 * pageNum; i <= 6; i++)
		{
			Label godotStock = new Label();
			godotStock.Name = $"CurrentStock{i}";
			godotStock.Text = "Empty";
			AddChild(godotStock);

			Label stockBuyPrice = new Label();
			stockBuyPrice.Name = $"BuyPrice{i}";
			stockBuyPrice.Text = $"{Stocks.GetValue($"{i}", 4)}";
			godotStock.AddChild(stockBuyPrice);

			Label stockSellPrice = new Label();
			stockSellPrice.Name = $"SellPrice{i}";
			stockSellPrice.Text = $"{Stocks.GetValue($"{i}", 5)}";
			godotStock.AddChild(stockSellPrice);
			
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
			Label stockSellPrice = stockFromGodot.GetNode<Label>($"SellPrice{stockNum}");
			
			stockBuyPrice.Position = new Vector2 (0 - 70, 0 + 70);
			stockSellPrice.Position = new Vector2 (0 + 100, 0 + 70);

			stockFromGodot.Text = SqliteHelpers.GetValue("Stocks", "Id", stockNum);
		}
	}
	public void UpdateStockDisplay()
	{
		foreach(Label stockFromGodot in GetChildren())
        {
			string stockNum = stockFromGodot.Name;
			stockNum = stockNum.Replace("CurrentStock", "");
			Label stockBuyPriceNode = stockFromGodot.GetNode<Label>($"BuyPrice{stockNum}");
            Label stockSellPriceNode = stockFromGodot.GetNode<Label>($"SellPrice{stockNum}");
            
            Random random = new Random();
            int[] plusOrMinusChances = { -1, 0, 1};
            int randomPriceChange = random.Next(plusOrMinusChances.Length);

            int newBuyPrice = (Int32.Parse($"{Stocks.GetValue($"{stockNum}", 4)}") + plusOrMinusChances[randomPriceChange]);
            int newSellPrice = (Int32.Parse($"{Stocks.GetValue($"{stockNum}", 5)}") + plusOrMinusChances[randomPriceChange]);
		}
	}
}
