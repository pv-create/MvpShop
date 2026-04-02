using Npgsql;

namespace MvpShop.Data;

public sealed class DatabaseRecoveryService(IServiceProvider serviceProvider, ILogger<DatabaseRecoveryService> logger)
{
    private readonly SemaphoreSlim _recoveryLock = new(1, 1);

    public Task EnsureInitializedAsync(CancellationToken cancellationToken = default)
    {
        return AppDbInitializer.InitializeAsync(serviceProvider, cancellationToken);
    }

    public async Task<bool> TryRecoverMissingDatabaseAsync(Exception exception, CancellationToken cancellationToken = default)
    {
        if (exception is not PostgresException postgresException || postgresException.SqlState != PostgresErrorCodes.InvalidCatalogName)
        {
            return false;
        }

        await _recoveryLock.WaitAsync(cancellationToken);

        try
        {
            logger.LogWarning(
                postgresException,
                "PostgreSQL database is missing. Attempting to recreate the database and apply migrations.");

            await AppDbInitializer.InitializeAsync(serviceProvider, cancellationToken);

            logger.LogInformation("Database recovery completed successfully.");
            return true;
        }
        catch (Exception recoveryException)
        {
            logger.LogError(recoveryException, "Database recovery failed.");
            return false;
        }
        finally
        {
            _recoveryLock.Release();
        }
    }
}
