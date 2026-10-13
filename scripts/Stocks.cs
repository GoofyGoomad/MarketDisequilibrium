using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Godot;

public partial class Stocks : Node
{
	public static int availableStockId = 0;
	private Label StockGodot;
	private Label stockTitle;
	private int stockIdIndicator = 0;
	public static List<string> stockList = new List<string>() { ""};
	public static Dictionary<int, string> stocksDict = new Dictionary<int, string>();
    private List<int> stockId = new List<int>() { };
    
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
		Volatility INT(3) NOT NULL,
		Buy_Price INT(255) NOT NULL,
		Sell_Price INT(255) NOT NULL
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
            int volatility = Int32.Parse(fileReader.ReadLine());
            int buyPrice = Int32.Parse(fileReader.ReadLine());
            int sellPrice = Int32.Parse(fileReader.ReadLine());
            fileReader.Close();
            SqliteHelpers.ExecuteSQLCommand($"INSERT INTO Stocks VALUES ('{Stocks.availableStockId}', '{stockName}', '{stockCode}', '{volatility}', '{buyPrice}', '{sellPrice}');");

        }
            //SqliteHelpers.ExecuteSQLCommand(@$"");
            /*
            Stock alphaCo = new Stock("alphaCo", "Alpha Co.", 1, "SMT.APC", 50, 60, 50);
            Stock betaCo = new Stock("betaCo", "Beta Co.", 1, "SMT.BTC", 50, 60, 50);
            Stock gammaCo = new Stock("gammaCo", "Gamma Co.", 1, "SMT.GPC", 50, 60, 50);
            Stock deltaCo = new Stock("deltaCo", "Delta Co.", 1, "SMT.DPC", 50, 60, 50);
            Stock epsilonCo = new Stock("epsilonCo", "Epsilon Co.", 1, "SMT.EPC", 50, 60, 50);
            Stock zetaCo = new Stock("zetaCo", "Zeta Co.", 1, "SMT.ZPC", 50, 60, 50);
            */
            EmitSignal(SignalName.StocksReady);
        /*
		// For when configs are actually made
		// Loops through each stock in stockList
		foreach (string stockName in stockList)
		{
			stockIdIndicator++;
			//Stock thing = new Stock("Apple");
			stocksDict.Add(stockIdIndicator, stockName);
			for (int i = (10 - stockIdIndicator.ToString().Length); i > 0; i--)
			{
				stockId.Add(0);
			}
			stockId.Add(stockIdIndicator);

			StockGodot = new Label();
			StockGodot.Name = $"Stock{String.Join("", stockId)}";
			StockGodot.Text = stockName;
			GetNode<GridContainer>("Display").AddChild(StockGodot);

			stockId = new List<int>() { };
		}
		*/
    }

	public override void _Process(double delta)
	{
		// Called every frame. 'delta' is the elapsed time since the previous frame.
	}
}
public class Stock
{
	string codeName;
	string name;
	int gameId;
    string stockCode;
    float volatility; // This is a percentage
    float buyPrice;
	float sellPrice;
	public Stock(string codeName, string name, int gameId, string stockCode, float volatility, float buyPrice, float sellPrice)
    {
    	Stocks.availableStockId++;
    	this.codeName = codeName;
		this.name = name;
		this.gameId = Stocks.availableStockId;
        this.stockCode = stockCode;
        this.volatility = volatility;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
        
        Stocks.stocksDict.Add(this.gameId, this.codeName);
	}
}
