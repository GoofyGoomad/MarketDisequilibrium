using System;
using System.IO;
using Godot;
using Microsoft.Data.Sqlite;

public partial class Main : Node2D
{
	private bool DevToolsToggledOn = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        GetNode<Camera2D>("MainCamera").Offset = new Vector2(0, 0);
        GetNode<Label>("BalanceDisplay").Text = "0.000";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("OpenDevTools"))
		{
			Camera2D mainCamera = GetNode<Camera2D>("MainCamera");
			if (!DevToolsToggledOn)
			{
				DevToolsToggledOn = true;
				GD.Print("Dev Tools Opened!");
				mainCamera.Offset = new Vector2(1920, 0);
			}
			else if (DevToolsToggledOn)
			{
				DevToolsToggledOn = false;
				GD.Print("Dev Tools Closed!");
				mainCamera.Offset = new Vector2(0, 0);
			}
		}
	}
}

public static class SqliteHelpers
{
    public static void ExecuteSQLCommand(string command)
    {
        try 
		{
			using var connection = new SqliteConnection("Data Source=temp-storage/stocks.sqlite3");
            connection.Open();
            using var sqlCommand = new SqliteCommand(command, connection);
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Dispose();
            connection.Close();
        } catch (SqliteException theError) {
            GD.Print(theError.Message);
		}
    }
    public static string GetValue(string table, string column, string value)
    {
        using var connection = new SqliteConnection("Data Source=temp-storage/stocks.sqlite3");
        connection.Open();
        using var sqlCommand = new SqliteCommand($"SELECT * FROM {table} WHERE {column}='{value}'", connection);
        var sqlOutput = sqlCommand.ExecuteReader();
        GD.Print(sqlOutput.Read());
        GD.Print(sqlOutput.GetValue(1));
        return sqlOutput.GetValue(1).ToString();
    }
}