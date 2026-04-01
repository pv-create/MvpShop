using Npgsql;

namespace MvpShop.Data;

public static class PostgresDatabaseBootstrapper
{
    public static async Task EnsureDatabaseExistsAsync(string connectionString, CancellationToken cancellationToken = default)
    {
        var targetBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var targetDatabase = targetBuilder.Database;

        if (string.IsNullOrWhiteSpace(targetDatabase))
        {
            throw new InvalidOperationException("Connection string must include a database name.");
        }

        var adminBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Database = string.Equals(targetDatabase, "postgres", StringComparison.OrdinalIgnoreCase)
                ? "template1"
                : "postgres"
        };

        await using var connection = new NpgsqlConnection(adminBuilder.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await using var existsCommand = connection.CreateCommand();
        existsCommand.CommandText = "SELECT 1 FROM pg_database WHERE datname = @databaseName;";
        existsCommand.Parameters.AddWithValue("databaseName", targetDatabase);

        var exists = await existsCommand.ExecuteScalarAsync(cancellationToken) is not null;
        if (exists)
        {
            return;
        }

        var escapedDatabaseName = targetDatabase.Replace("\"", "\"\"", StringComparison.Ordinal);

        await using var createCommand = connection.CreateCommand();
        createCommand.CommandText = $"CREATE DATABASE \"{escapedDatabaseName}\";";
        await createCommand.ExecuteNonQueryAsync(cancellationToken);
    }
}
