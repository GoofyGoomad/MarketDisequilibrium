using Godot;
using System;
using Microsoft.Data.Sqlite;

public partial class SaveButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Pressed += SaveProgress;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SaveProgress()
	{
		using var connection = new SqliteConnection("Data Source=temp-storage/stocks.sqlite3");
		connection.Open();
		using var sqlCommand = new SqliteCommand($"SELECT * FROM CurrentUser", connection);
		using (var sqlOutput = sqlCommand.ExecuteReader())
		{
			var config = new ConfigFile();
			while (sqlOutput.Read())
			{
				string userName = sqlOutput[1].ToString();
				string balance = sqlOutput[2].ToString();
				config.SetValue(userName, "Balance", balance);
				break;
			}
			config.Save("res://saves/user.cfg");
		}
		using var sqlCommand2 = new SqliteCommand($"SELECT * FROM Stocks", connection);
		using (var sqlOutput = sqlCommand2.ExecuteReader())
		{
			var config = new ConfigFile();
			while (sqlOutput.Read())
			{
				string stockCode = sqlOutput[2].ToString();
				string buyPrice = sqlOutput[4].ToString();
				string sellPrice = sqlOutput[5].ToString();
				string amount = sqlOutput[7].ToString();
				config.SetValue(stockCode, "StockCode", stockCode);
				config.SetValue(stockCode, "BuyPrice", buyPrice);
				config.SetValue(stockCode, "SellPrice", sellPrice);
				config.SetValue(stockCode, "Amount", amount);
			}
			config.Save("res://saves/stocks.cfg");
		}
	}
}
