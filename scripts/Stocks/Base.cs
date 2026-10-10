using Microsoft.Data.Sqlite;
using Godot;

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
        } catch (SqliteException theError) {
            GD.Print(theError.Message);
		}
    }
}