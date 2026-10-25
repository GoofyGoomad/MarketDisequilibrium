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
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void DisplayStocks()
	{
		int pageNum = 1;
		for (int i = 1 * pageNum; i <= 6 * pageNum; i++)
		{
			int stockId = i;
			Label godotStock = new Label();
			godotStock.Name = $"CurrentStock{i}";
			godotStock.Text = Stocks.GetDBValue($"{i}", 1);
			AddChild(godotStock);

			Label stockBuyPrice = new Label();
			stockBuyPrice.Name = $"BuyPrice{i}";
			stockBuyPrice.Text = $"{Stocks.GetDBValue($"{i}", 4)}";
			godotStock.AddChild(stockBuyPrice);

			Label stockSellPrice = new Label();
			stockSellPrice.Name = $"SellPrice{i}";
			stockSellPrice.Text = $"{Stocks.GetDBValue($"{i}", 5)}";
			godotStock.AddChild(stockSellPrice);
			

			Button buyStock = new Button();
			buyStock.Name = $"BuyStock{i}";
			buyStock.Text = "Buy Stock";
			
			buyStock.Pressed += () => Stocks.BuyStock(stockId, godotStock);
			godotStock.AddChild(buyStock);
			
			Button sellStock = new Button();
			sellStock.Name = $"SellStock{i}";
			sellStock.Text = "Sell Stock";
			sellStock.Pressed += () => Stocks.SellStock(stockId, godotStock);
			godotStock.AddChild(sellStock);
			
			Label stockAmount = new Label();
			stockAmount.Name = $"Amount{i}";
			stockAmount.Text = "0";
			godotStock.AddChild(stockAmount);
		}
		GD.Print(GetChildren());
		foreach(Label stockFromGodot in GetChildren())
		{
			
			string stockNum = stockFromGodot.Name;
			stockNum = stockNum.Replace("CurrentStock", "");
			Label stockBuyPrice = stockFromGodot.GetNode<Label>($"BuyPrice{stockNum}");
			Label stockSellPrice = stockFromGodot.GetNode<Label>($"SellPrice{stockNum}");
			Button buyStock = stockFromGodot.GetNode<Button>($"BuyStock{stockNum}");
			Button sellStock = stockFromGodot.GetNode<Button>($"SellStock{stockNum}");
			Label stockAmount = stockFromGodot.GetNode<Label>($"Amount{stockNum}");

			stockBuyPrice.Position = new Vector2 (0 - 70, 0 + 70);
			stockSellPrice.Position = new Vector2(0 + 100, 0 + 70);
			buyStock.Position = new Vector2(0 - 85, 0 + 120);
			sellStock.Position = new Vector2(0 + 100, 0 + 120);
			stockAmount.Position = new Vector2(0 + 40, 0 + 30);
		}
	}
	public void UpdateStockDisplay()
	{
		foreach(Label stockFromGodot in GetChildren())
		{
			
			string stockNum = stockFromGodot.Name;
			stockNum = stockNum.Replace("CurrentStock", "");
			
			Label stockBuyPrice = stockFromGodot.GetNode<Label>($"BuyPrice{stockNum}");
			Label stockSellPrice = stockFromGodot.GetNode<Label>($"SellPrice{stockNum}");

			stockFromGodot.Text = Stocks.GetDBValue($"{stockNum}", 1);
			stockBuyPrice.Text = $"{Stocks.GetDBValue($"{stockNum}", 4)}";
			stockSellPrice.Text = $"{Stocks.GetDBValue($"{stockNum}", 5)}";
		}
	}
}
