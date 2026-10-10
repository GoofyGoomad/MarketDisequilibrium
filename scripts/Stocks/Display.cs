using System;
using System.Reflection;
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
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void DisplayStocks()
	{
		int pageNum = 1;
		foreach(Label stockFromGodot in GetChildren())
		{
			string stockNum = stockFromGodot.Name;
			stockNum = stockNum.Replace("CurrentStock", "");
			int stockNumInt = Int32.Parse(stockNum);
			Type stockInCode = Type.GetType($"Stocks.{Stocks.stocksDict[stockNumInt]}");
			GD.Print($"Stocks.{Stocks.stocksDict[stockNumInt]}");
			GD.Print("hello?");
			GD.Print($"{stockInCode}");
			GD.Print(stockInCode);
			//GD.Print($"{Stocks.stocksDict}");
			//Stocks.alphaco;
            GD.Print(Stocks.stocksDict[1]);
			//GD.Print(Stocks.stocksDict[stockNumInt]);
			//stockFromGodot.Text = Type.GetType(Stocks.stocksDict[stockNumInt]).GetProperty("name").ToString(); //(string)typeof(Stock).GetProperty("name").GetValue(stockFromCode, null);
		}
	}
	/*
	public static string GrabAttrFromDict(Stock theClass, string theAttr)
	{
		//Type thing = Type.GetType(theClass);
		//Stock aClass = Activator.CreateInstance(thing);
		//string theValue = theClass.name;
		//return theValue;
	}
*/
}
