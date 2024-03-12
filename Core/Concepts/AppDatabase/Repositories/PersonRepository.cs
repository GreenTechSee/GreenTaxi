using Core.Core.Entities.HomeBeredskap;
using Dapper;
using Microsoft.Extensions.Configuration;
using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Concepts.AppDatabase.Repositories;

public interface IPersonRepository
{
	Task UpsertPerson(string fnr);
}

public class PersonRepository : AppDatabaseRepository, IPersonRepository
{
	public PersonRepository(IConfiguration configuration) : base(configuration)
	{
	}

	public async Task UpsertPerson(string fnr)
	{
		await using var connection = await OpenConnectionAsync();

		var select = new Select<PersonEntity>().Where(e => e.Fnr == fnr).ToQuery();
		var existing = await connection.QueryFirstOrDefaultAsync<HomeEntity>(select.QueryText, select.Parameters);

		if (existing == null)
		{
			var newPerson = new PersonEntity
			{
				Fnr = fnr,
				Name = "Bruker"
			};

			var insert = new Insert<PersonEntity>().Add(newPerson).ToQuery();
			await connection.ExecuteAsync(insert.QueryText, insert.Parameters);
		}
		else
		{
			var newPerson = new PersonEntity
			{
				Fnr = fnr,
				Name = "Bruker"
			};

			var update = new Update<PersonEntity>().Set(newPerson).Where(e => e.Fnr == fnr).ToQuery();
			await connection.ExecuteAsync(update.QueryText, update.Parameters);
		}
	}
}
