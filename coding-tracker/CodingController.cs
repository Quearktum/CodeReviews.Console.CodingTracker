using System.Configuration;
using Microsoft.Data.Sqlite;
internal class CodingController
{
    string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");


    internal void Post (Coding coding)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"INSERT INTO coding (date, duration) VALUES ('{coding.Date}', '{coding.Duration}')";
                tableCmd.ExecuteNonQuery();
            }
        }
    }

    internal void Get()
    {
        List<Coding> tableData = new List<Coding>();
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"SELECT * from coding";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tableData.Add(new Coding{
                                Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Duration = reader.GetString(2),
                            });

                        }
                    }
                    else{
                        System.Console.WriteLine("\n\nNo rows found\n\n");
                    }
                }
            }

            TableVisualization.ShowTable(tableData);
        }
    }

    internal Coding GetById(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"SELECT * from coding WHERE Id = '{id}'";

                using (var reader = tableCmd.ExecuteReader())
                {
                    Coding coding = new Coding();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        coding.Id = reader.GetInt32(0);
                        coding.Date = reader.GetString(1);
                        coding.Duration = reader.GetString(2);
                    }
                    else{
                        System.Console.WriteLine("\n\nNo rows found\n\n");
                    }

                    return coding;
                }
            }
        }


    }

    internal void Delete(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"DELETE from coding WHERE Id = '{id}'";
                tableCmd.ExecuteNonQuery();
            }
        }
    }

    internal void Update(Coding coding)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"UPDATE coding SET Date = '{coding.Date}', Duration = '{coding.Duration}' WHERE Id = '{coding.Id}'";
                tableCmd.ExecuteNonQuery();
            }
        }
    }
    
}