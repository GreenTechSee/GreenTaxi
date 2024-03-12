using Core.Core.Entities.HomeBeredskap;
using Dapper;
using Microsoft.Extensions.Configuration;
using oledid.SyntaxImprovement.Generators.Sql;
using System.Collections.Generic;
using System.Data.Common;

namespace Core.Concepts.AppDatabase.Repositories;

public interface IHomeBeredskapRepository
{
	Task AddHome(DbConnection connection, string fnr, int numberOfInhabitants);
	Task<HomeEntity?> GetHome(DbConnection connection, string fnr);
	Task AddItem(DbConnection connection, ItemEntity item);
	Task RemoveItem(DbConnection connection, long itemId);
	Task<List<ItemTypeEntity>> GetItemTypes(DbConnection connection);
	Task<List<ItemEntity>> GetItems(DbConnection connection, long homeId);
}


public class HomeBeredskapRepository : AppDatabaseRepository, IHomeBeredskapRepository
{
	public HomeBeredskapRepository(IConfiguration configuration) : base(configuration)
	{
	}

	public async Task AddHome(DbConnection connection, string fnr, int numberOfInhabitants)
	{
		var select = new Select<HomeEntity>().Where(e => e.PersonFnr == fnr).ToQuery();
		var existing = await connection.QueryFirstOrDefaultAsync<HomeEntity>(select.QueryText, select.Parameters);

		if (existing != null)
		{
			throw new NotSupportedException();
		}

		var newHome = new HomeEntity
		{
			Name = "Mitt hjem",
			Adress = "",
			PersonFnr = fnr,
			NumberOfInhabitants = numberOfInhabitants
		};

		var insert = new Insert<HomeEntity>().Add(newHome).ToQuery();
		await connection.ExecuteAsync(insert.QueryText, insert.Parameters);
	}

	public async Task AddItem(DbConnection connection, ItemEntity item)
	{
		var insert = new Insert<ItemEntity>().Add(item).ToQuery();
		await connection.QueryAsync(insert.QueryText, insert.Parameters);
	}
	public async Task RemoveItem(DbConnection connection, long itemId)
	{
		var delete = new Delete<ItemEntity>().Where(e => e.Id == itemId).ToQuery();
		await connection.ExecuteAsync(delete.QueryText, delete.Parameters);
	}

	public async Task<HomeEntity?> GetHome(DbConnection connection, string fnr)
	{
		var select = new Select<HomeEntity>().Where(e => e.PersonFnr == fnr).ToQuery();
		var home = await connection.QueryFirstOrDefaultAsync<HomeEntity>(select.QueryText, select.Parameters);
		if (home != null)
		{
			home.Items = await GetItems(connection, home.Id);
			home.ItemTypes = await GetItemTypes(connection);
		}

		return home;
	}

	public async Task<List<ItemEntity>> GetItems(DbConnection connection, long homeId)
	{
		var select = new Select<ItemEntity>().Where(e => e.HomeId == homeId).ToQuery();
		var items = (await connection.QueryAsync<ItemEntity>(select.QueryText, select.Parameters)).ToList();

		var itemTypes = await GetItemTypes(connection);

		foreach (var item in items)
		{
			item.ItemType = itemTypes.Find(e => e.Id == item.ItemTypeId);
		}

		return items;
	}

	public async Task<List<ItemTypeEntity>> GetItemTypes(DbConnection connection)
	{
		var select = new Select<ItemTypeEntity>().ToQuery();
		return (await connection.QueryAsync<ItemTypeEntity>(select.QueryText, select.Parameters)).ToList();
	}
}
