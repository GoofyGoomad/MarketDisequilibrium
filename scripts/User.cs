using System;
using Godot;

public partial class User : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public static double balance = Convert.ToDouble(SqliteHelpers.GetValue("CurrentUser", "Id", "1", 2));
	
	public override void _Ready()
	{
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
}
public class UserCommands
{
	
}
