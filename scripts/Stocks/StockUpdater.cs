using System;
using Godot;
using Microsoft.Data.Sqlite;

public partial class StockUpdater : Timer
{
    [Signal]
    public delegate void UpdateStocksEventHandler();

    int stockTruePrice = 50;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //GetNode<Label>("../Display/StockBuyPrice").Text = $"{stockTruePrice}";
        //GetNode<Label>("../Display/StockSellPrice").Text = $"{Math.Round(((double)stockTruePrice / 100) * 90)}";
        this.Timeout += UpdateStocksMethod;
        this.Timeout += PayoutDividends;
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }
    
    public void UpdateStocksMethod()
    {
        foreach(Label stockNode in GetNode<GridContainer>("../DisplayEnabler/Display").GetChildren())
        {
			string stockNum = stockNode.Name;
            stockNum = stockNum.Replace("CurrentStock", "");
            
			Label stockBuyPriceNode = stockNode.GetNode<Label>($"BuyPrice{stockNum}");
            Label stockSellPriceNode = stockNode.GetNode<Label>($"SellPrice{stockNum}");

            int stockVolatility = Int32.Parse(Stocks.GetDBValue(stockNum, 3));
            double stockCurrentBuyPrice = (double)Convert.ToDouble($"{Stocks.GetDBValue($"{stockNum}", 4)}");
            double stockCurrentSellPrice = (double)Convert.ToDouble($"{Stocks.GetDBValue($"{stockNum}", 5)}");

            
            Random random = new Random();
            int[] plusOrMinusChances = {-3, -2, -1, 0, 1, 2, 3};
            int randomPriceChange = random.Next(plusOrMinusChances.Length);
            double newBuyPrice;
            double newSellPrice;
            while (true)
            {
                double stockPriceChange = Double.Round((plusOrMinusChances[randomPriceChange] * random.NextDouble() * stockVolatility), 3);

                newBuyPrice = Double.Round((stockCurrentBuyPrice + stockPriceChange), 3);
                newSellPrice = Double.Round((stockCurrentSellPrice + stockPriceChange), 3);
                if (newBuyPrice <= 0 || newSellPrice <= 0)
                {
                    continue;
                } else {
                    break;
                }
            }    
            Stocks.SetDBValue(stockNum, "Buy_Price", newBuyPrice);
            Stocks.SetDBValue(stockNum, "Sell_Price", newSellPrice);
        }
        EmitSignal(SignalName.UpdateStocks);
    }
    public void PayoutDividends()
    {
		using var connection = new SqliteConnection("Data Source=temp-storage/stocks.sqlite3");
		connection.Open();
		using var sqlCommand = new SqliteCommand($"SELECT * FROM Stocks", connection);
        using (var sqlOutput = sqlCommand.ExecuteReader())
        {
            while (sqlOutput.Read())
            {
                User.balance += ((Convert.ToDouble(sqlOutput[5]) / 100) * Convert.ToDouble(sqlOutput[6]) * Convert.ToDouble(sqlOutput[7]));
                //var _user = new User();
                //_user.UpdateBalance(this.GetNode<Label>("/root/Game/BalanceDisplay"));
                //GD.Print(sqlOutput[0]);
            }
        }
    }
}
