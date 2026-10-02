using System;
using System.Collections.Generic;
using Godot;

public partial class Stocks : Node
{

	private Label StockGodot;
	private Label stockTitle;
	private int stockIdIndicator = 0;
	public static List<string> stockList = new List<string>() { ""};
	public static Dictionary<int, string> stocksDict = new Dictionary<int, string>();
	private List<int> stockId = new List<int>() { };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        Stock alphaCo = new Stock("Alpha Co.", 1, "SMT.APC", 50, 60, 50);
		Stock betaCo = new Stock("Beta Co.", 1, "SMT.APC", 50, 60, 50);
        Stock gammaCo = new Stock("Gamma Co.", 1, "SMT.APC", 50, 60, 50);
        Stock deltaCo = new Stock("Delta Co.", 1, "SMT.APC", 50, 60, 50);
        Stock epsilonCo = new Stock("Epsilon Co.", 1, "SMT.APC", 50, 60, 50);
        Stock zetaCo = new Stock("Zeta Co.", 1, "SMT.APC", 50, 60, 50);
        
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
	string name;
	int id;
    string stockCode;
    float volatility; // This is a percentage
    float buyPrice;
	float sellPrice;
	public Stock(string name, int id, string stockCode, float volatility, float buyPrice, float sellPrice)
	{
		this.name = name;
		this.id = id;
        this.stockCode = stockCode;
        this.volatility = volatility;
        this.buyPrice = buyPrice;
		this.sellPrice = sellPrice;
	}
}
