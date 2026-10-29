using System;
using Godot;

public partial class User : Node2D
{
	[Export]
	private Stocks _stocksNode;
	// Called when the node enters the scene tree for the first time.
	public static double balance = Convert.ToDouble(SqliteHelpers.GetValue("CurrentUser", "Id", "1", 2));
	
	public override void _Ready()
	{
		_stocksNode.StocksReady += LoadUserConfig;
		_stocksNode.StocksReady += LoadStocksConfig;
		SqliteHelpers.ExecuteSQLCommand("DROP TABLE IF EXISTS CurrentUser");
		SqliteHelpers.ExecuteSQLCommand(@"CREATE TABLE CurrentUser (
		Id int PRIMARY KEY,
		Name VARCHAR(10) NOT NULL,
		Balance VARCHAR(255) NOT NULL
		);");
		SqliteHelpers.ExecuteSQLCommand("INSERT INTO CurrentUser VALUES ('1', 'test', '500')");

		UpdateBalance(GetNode<Label>("/root/Game/BalanceDisplay"));
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateBalance(GetNode<Label>("/root/Game/BalanceDisplay"));
	}
	public void UpdateBalance(Label _balanceDisplay)
	{
		SqliteHelpers.ExecuteSQLCommand($"UPDATE CurrentUser SET Balance='{User.balance}' WHERE Id='1'");
		
		_balanceDisplay.Text = Double.Round(User.balance, 3).ToString();
	}
	public void LoadUserConfig()
	{
		var config = new ConfigFile();
		Error err = config.Load("res://saves/user.cfg");
		if (err != Error.Ok)
		{
			return;
		}
		User.balance = (double)config.GetValue("test", "Balance");
		UpdateBalance(GetNode<Label>("/root/Game/BalanceDisplay"));
	}
	public void LoadStocksConfig()
	{
		var config = new ConfigFile();

		// Load data from a file.
		Error err = config.Load("res://saves/stocks.cfg");

		// If the file didn't load, ignore it.
		if (err != Error.Ok)
		{
			return;
		}

		foreach (String stock in config.GetSections())
		{
			var stockCode = (String)config.GetValue(stock, "StockCode");
			var buyPrice = (String)config.GetValue(stock, "BuyPrice");
			var sellPrice = (String)config.GetValue(stock, "SellPrice");
			var amount = (String)config.GetValue(stock, "Amount");
			try
			{
				SqliteHelpers.ExecuteSQLCommand($"UPDATE Stocks SET Buy_Price='{buyPrice}', Sell_Price='{sellPrice}', Amount='{amount}' WHERE Code='{stockCode}'");
				GD.Print($"Stock code {stockCode} loaded successfully.");
			}
			catch (Exception e)
			{
				GD.Print($"Stock code {stockCode} not found.");
			}
		}
	}
}
