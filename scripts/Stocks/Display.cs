using System;
using System.Collections.Generic;
using System.Reflection;
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
            stockFromGodot.Text = SqliteHelpers.GetValue("Stocks", "Id", stockNum);
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
