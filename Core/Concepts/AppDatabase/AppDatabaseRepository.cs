using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Core.Concepts.AppDatabase.Repositories
{
	public abstract class AppDatabaseRepository
	{
		protected readonly IConfiguration configuration;

		protected AppDatabaseRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task<DbConnection> OpenConnectionAsync()
		{
			return await OpenConnectionAsync(configuration);
		}

		public static async Task<DbConnection> OpenConnectionAsync(IConfiguration configuration)
		{
			var connectionString = configuration.GetOrThrow("Secrets:AppDatabase:ConnectionString")
				.Replace("{USERNAME}", configuration.GetOrThrow("Secrets:AppDatabase:Username"))
				.Replace("{PASSWORD}", "\"" + configuration.GetOrThrow("Secrets:AppDatabase:Password") + "\"")
				.Replace("{DATABASENAME}", configuration.GetOrThrow("Secrets:AppDatabase:DatabaseName"));
			var connection = new SqlConnection(connectionString);

			await connection.OpenAsync();
			return connection;
		}

		public DbConnection OpenConnectionSync()
		{
			var connectionString = configuration.GetOrThrow("Secrets:AppDatabase:ConnectionString")
				.Replace("{USERNAME}", configuration.GetOrThrow("Secrets:AppDatabase:Username"))
				.Replace("{PASSWORD}", "\"" + configuration.GetOrThrow("Secrets:AppDatabase:Password") + "\"")
				.Replace("{DATABASENAME}", configuration.GetOrThrow("Secrets:AppDatabase:DatabaseName"));
			var connection = new SqlConnection(connectionString);

			connection.Open();
			return connection;
		}
	}
}
