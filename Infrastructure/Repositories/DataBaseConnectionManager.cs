using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gdb.Logging;
using Microsoft.Extensions.Logging;

namespace GDB.App.Infrastructure.Repositories
{
    public class DataBaseConnectionManager
    {
        private static readonly ILogger _logger = AppLogger.CreateLogger<DataBaseConnectionManager>();

        public static DbConnection GetConnection()
        {
            var settings = ConfigurationManager.ConnectionStrings["GDBConnection"];

            if (settings == null)
            {
                _logger.LogWarning("Connection string 'GDBConnection' not found in configuration. Falling back to environment variable 'GDBConnection'.");

                var envConn = Environment.GetEnvironmentVariable("GDBConnection");
                if (string.IsNullOrWhiteSpace(envConn))
                {
                    _logger.LogError("Connection string 'GDBConnection' is missing from configuration and environment.");
                    throw new ConfigurationErrorsException("Connection string 'GDBConnection' not found in configuration or environment.");
                }

                // If provider is specified in environment, attempt to use it, otherwise default to SqlClient
                var envProvider = Environment.GetEnvironmentVariable("GDBProvider") ?? "System.Data.SqlClient";

                try
                {
                    if (envProvider.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) ||
                        envProvider.Equals("Microsoft.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
                    {
                        // Use SqlConnection directly as a simple fallback in .NET Core
                        var sqlConn = new SqlConnection(envConn);
                        return sqlConn;
                    }

                    // Attempt to use DbProviderFactories for other providers
                    DbProviderFactory factory = DbProviderFactories.GetFactory(envProvider);
                    DbConnection connection = factory.CreateConnection();
                    connection.ConnectionString = envConn;
                    return connection;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create DB connection using environment provider {ProviderName}", envProvider);
                    throw;
                }
            }

            string connectionString = settings.ConnectionString;
            string providerName = settings.ProviderName;

            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                DbConnection connection = factory.CreateConnection();

                connection.ConnectionString = connectionString;

                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create DB connection for provider {ProviderName}", providerName);
                throw;
            }
        }
    }
}
