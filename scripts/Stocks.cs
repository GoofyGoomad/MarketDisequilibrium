using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Godot;
using Microsoft.Data.Sqlite;

public partial class Stocks : Node
{
	public static int availableStockId = 0;
	private Label StockGodot;
	private Label stockTitle;
	private int stockIdIndicator = 0;
	public static List<string> stockList = new List<string>() { ""};
	public static Dictionary<int, string> stocksDict = new Dictionary<int, string>();
    private List<int> stockId = new List<int>() {};
    
    [Signal]
    public delegate void StocksReadyEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		SqliteHelpers.ExecuteSQLCommand("DROP TABLE IF EXISTS Stocks");
        SqliteHelpers.ExecuteSQLCommand(@"CREATE TABLE Stocks (
		Id int PRIMARY KEY, 
		Name VARCHAR(10) NOT NULL,
		Code VARCHAR(15) NOT NULL,
		Volatility VARCHAR(7) NOT NULL,
		Buy_Price VARCHAR(100) NOT NULL,
		Sell_Price VARCHAR(100) NOT NULL,
		DividendPercent VARCHAR(10) NOT NULL,
		Amount VARCHAR(255) NOT NULL
		);");

        string[] stockFileList = Directory.GetFiles("config/stocks/", "*.cfg", SearchOption.AllDirectories);
		Array.Sort(stockFileList);
        foreach (string stockFile in stockFileList)
        {
            GD.Print("Reading file: " + Path.GetFileName(stockFile));
            Stocks.availableStockId++;
            StreamReader fileReader = new StreamReader(stockFile);
            string stockName = fileReader.ReadLine();
            string stockCode = fileReader.ReadLine();
            string volatility = fileReader.ReadLine();
            string buyPrice = fileReader.ReadLine();
            string sellPrice = fileReader.ReadLine();
            string dividendPercent = fileReader.ReadLine();
            
            fileReader.Close();
            SqliteHelpers.ExecuteSQLCommand($"INSERT INTO Stocks VALUES ('{Stocks.availableStockId}', '{stockName}', '{stockCode}', '{volatility}', '{buyPrice}', '{sellPrice}', '{dividendPercent}', '0');");
        }
        EmitSignal(SignalName.StocksReady);
    }
	public override void _Process(double delta)
	{
        // Called every frame. 'delta' is the elapsed time since the previous frame.
    }
    public static void BuyStock(int stockId, Label godotStock)
    {
        double stockPrice = Convert.ToDouble(GetDBValue(stockId.ToString(), 4));
        if (User.balance >= stockPrice)
        {
            User.balance -= stockPrice;
            var _user = new User();
            _user.UpdateBalance(godotStock.GetNode<Label>("/root/Game/BalanceDisplay"));
            int stockAmount = Int32.Parse(GetDBValue(stockId.ToString(), 7));
            stockAmount++;
            SetDBValue(stockId.ToString(), "Amount", stockAmount);
            
            godotStock.GetNode<Label>($"Amount{stockId}").Text = stockAmount.ToString();
            //User.Up
        }
    }
    public static void SellStock(int stockId, Label godotStock)
    {
        int stockAmount = Int32.Parse(GetDBValue(stockId.ToString(), 7));
        stockAmount--;
        SetDBValue(stockId.ToString(), "Amount", stockAmount);
        
        godotStock.GetNode<Label>($"Amount{stockId}").Text = stockAmount.ToString();
    }

    public static string GetDBValue(string id, int columnId)
    {
		using var connection = new SqliteConnection("Data Source=temp-storage/stocks.sqlite3");
		connection.Open();
		using var sqlCommand = new SqliteCommand($"SELECT * FROM Stocks WHERE Id='{id}'", connection);
        var sqlOutput = sqlCommand.ExecuteReader();

        sqlOutput.Read(); // Need to do Read() or else it won't have the proper output
        return sqlOutput.GetValue(columnId).ToString();
    }
    public static void SetDBValue(string id, string column, double value)
    {
		using var connection = new SqliteConnection("Data Source=temp-storage/stocks.sqlite3");
		connection.Open();
		using var sqlCommand = new SqliteCommand($"UPDATE Stocks SET {column}='{value}' WHERE Id='{id}'", connection);
        var sqlOutput = sqlCommand.ExecuteReader();
        //connection.Close();
	}
}
