using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructure.Migrations;
public static class DatabaseMigration
{
    public static void Migrate(string connection, IServiceProvider serviceProvider)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connection);
        var databaseName = connectionStringBuilder.InitialCatalog;
        connectionStringBuilder.Remove("Database");

        using var databaseConnection = new SqlConnection(connectionStringBuilder.ConnectionString);
        
        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = databaseConnection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters);

        if (!records.Any())
            databaseConnection.Execute($"CREATE DATABASE {databaseName}");

        MigrationDatabase(serviceProvider);
    }

    private static void MigrationDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();
        runner.MigrateUp();
    }
}
