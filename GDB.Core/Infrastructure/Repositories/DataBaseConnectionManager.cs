using System;
using System.Data.Common;
using GDB.Core.Infrastructure.Repositories.Contracts;
using GDB.Core.Logging;
using Microsoft.Extensions.Logging;

namespace GDB.Core.Infrastructure.Repositories
{
    public class DataBaseConnectionManager : IDataBaseConnectionManager
    {
        private static readonly ILogger _logger =
            AppLogger.CreateLogger<DataBaseConnectionManager>();

        private readonly string _connectionString;

        public DataBaseConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection GetConnection()
        {
            try
            {
                DbConnection connection =
                    System.Data.SqlClient.SqlClientFactory.Instance.CreateConnection();

                connection.ConnectionString = _connectionString;

                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to create database connection");

                throw;
            }
        }
    }
}