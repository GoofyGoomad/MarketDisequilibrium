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
			while (sqlOutput.Read())
			{
				var config = new ConfigFile();
				string userName = sqlOutput[1].ToString();
				string balance = sqlOutput[2].ToString();
				config.SetValue(userName, "Balance", balance);
				config.Save("res://saves/the-save.cfg");
				break;
			}
		}
	}
}
