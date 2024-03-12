using Core;
using Core.Concepts.AppDatabase.Repositories;
using Core.Core.Entities;
using Core.Core.Entities.HomeBeredskap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Web.Controllers;

[ApiController]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
[Route("/Secure/[controller]/[action]")]
public class SecureApiController : ControllerBase
{
	private readonly IConfiguration configuration;
	private readonly IHomeBeredskapRepository homeBeredskapRepository;
	private readonly IStatusRepository statusRepository;

	public SecureApiController(IConfiguration configuration, IHomeBeredskapRepository homeBeredskapRepository, IStatusRepository statusRepository)
	{
		this.configuration = configuration;
		this.homeBeredskapRepository = homeBeredskapRepository;
		this.statusRepository = statusRepository;
	}

	public async Task<string> GetLoggedInFnr()
	{
		return User.FindFirst(AppClaims.Fnr)?.Value ?? "";
	}

	public async Task AddHome(int numberOfInhabitants)
	{
		var fnr = await GetLoggedInFnr();
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		await homeBeredskapRepository.AddHome(connection, fnr, numberOfInhabitants);
	}

	public async Task<HomeEntity?> GetHome()
	{
		var fnr = await GetLoggedInFnr();
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await homeBeredskapRepository.GetHome(connection, fnr);
	}

	public async Task AddItem(ItemEntity item)
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		await homeBeredskapRepository.AddItem(connection, item);
	}

	public async Task RemoveItem(long itemId)
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		await homeBeredskapRepository.RemoveItem(connection, itemId);
	}

	public async Task<List<ItemTypeEntity>> GetItemTypes()
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await homeBeredskapRepository.GetItemTypes(connection);
	}

	public async Task<List<ItemEntity>> GetItems(long homeId)
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await homeBeredskapRepository.GetItems(connection, homeId);
	}

	public async Task<Status> GetStatus()
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await statusRepository.GetActiveStatus(connection);
	}

	public async Task<string> GetAzureMapsKey()
	{
		return configuration["Secrets:AzureMapsKey"] ?? string.Empty;
	}
}
