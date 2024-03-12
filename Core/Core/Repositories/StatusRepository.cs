using Core.Concepts.AppDatabase.Repositories;
using Core.Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using oledid.SyntaxImprovement.Generators.Sql;
using System.Data.Common;

namespace Core.Core.Repositories;

public interface IStatusRepository
{
	Task SetActiveStatus(DbConnection connection, int id);
	Task<int> GetActiveStatusId(DbConnection connection);
	Task<Status> GetActiveStatus(DbConnection connection);
}


public class StatusRepository : AppDatabaseRepository, IStatusRepository
{
	public StatusRepository(IConfiguration configuration) : base(configuration) {}

	public async Task<Status> GetActiveStatus(DbConnection connection)
	{
		var select = new Select<Status>().Where(e => e.IsActive).ToQuery();
		return await connection.QuerySingleAsync<Status>(select.QueryText, select.Parameters);
	}

	public async Task<int> GetActiveStatusId(DbConnection connection)
	{
		return (await GetActiveStatus(connection)).StatusId;
	}

	public async Task SetActiveStatus(DbConnection connection, int id)
	{
		var update = new Update<Status>().Set(e => e.IsActive, false).Where(e => e.IsActive).ToQuery();
		await connection.ExecuteAsync(update.QueryText, update.Parameters);
		var update2 = new Update<Status>().Set(e => e.IsActive, true).Where(e => e.StatusId == id).ToQuery();
	}
}
