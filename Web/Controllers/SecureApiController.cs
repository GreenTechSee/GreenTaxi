using Core;
using Core.Concepts.AppDatabase.Repositories;
using Core.Core.Entities;
using Core.Core.Entities.HomeBeredskap;
using Microsoft.AspNetCore.Mvc;

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

	private async Task<string> GetLoggedInFnr()
	{
		return User.FindFirst(AppClaims.Fnr)?.Value ?? "";
	}

	/// <summary>
	/// Adds a home to the logged in user
	/// </summary>
	/// <param name="numberOfInhabitants">
	/// Number of people living in the home
	/// </param>
	/// <returns></returns>
	public async Task AddHome(int numberOfInhabitants)
	{
		var fnr = await GetLoggedInFnr();
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		await homeBeredskapRepository.AddHome(connection, fnr, numberOfInhabitants);
	}

	/// <summary>
	/// Gets the home of the logged in user
	/// </summary>
	/// <returns cref="HomeEntity">
	/// Home registered to current user
	/// </returns>
	public async Task<HomeEntity?> GetHome()
	{
		var fnr = await GetLoggedInFnr();
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await homeBeredskapRepository.GetHome(connection, fnr);
	}

	/// <summary>
	/// Adds an item to a home
	/// </summary>
	/// <param name="item" cref="ItemEntity">
	/// ItemEntity to add
	/// </param>
	/// <returns></returns>
	public async Task AddItem(ItemEntity item)
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		await homeBeredskapRepository.AddItem(connection, item);
	}

	/// <summary>
	/// Removes item from home
	/// </summary>
	/// <param name="itemId">
	/// Id of the item to remove
	/// </param>
	/// <returns></returns>
	public async Task RemoveItem(long itemId)
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		await homeBeredskapRepository.RemoveItem(connection, itemId);
	}

	/// <summary>
	/// Gets all itemTypes
	/// </summary>
	/// <returns cref="ItemTypeEntity">
	/// All itemTypes in DB
	/// </returns>
	public async Task<List<ItemTypeEntity>> GetItemTypes()
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await homeBeredskapRepository.GetItemTypes(connection);
	}

	/// <summary>
	/// Get items in a home
	/// </summary>
	/// <param name="homeId">
	/// Id of home to get for
	/// </param>
	/// <returns cref="ItemEntity">
	/// List of items
	/// </returns>
	public async Task<List<ItemEntity>> GetItems(long homeId)
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await homeBeredskapRepository.GetItems(connection, homeId);
	}

	/// <summary>
	/// Gets the status of the application
	/// </summary>
	/// <returns></returns>
	public async Task<Status> GetStatus()
	{
		await using var connection = await AppDatabaseRepository.OpenConnectionAsync(configuration);

		return await statusRepository.GetActiveStatus(connection);
	}

	/// <summary>
	/// Gets the AzureMapsKey 
	/// </summary>
	/// <returns></returns>
	public async Task<string> GetAzureMapsKey()
	{
		return configuration["Secrets:AzureMapsKey"] ?? string.Empty;
	}

	/// <summary>
	/// Checks if user is the user with easter egg profile
	/// </summary>
	/// <returns>
	/// True if user has correct fnr, else false
	/// </returns>
	public async Task<bool> IsEasterEggActivated()
	{
		return User.FindFirst(AppClaims.EasterEgg)?.Value?.ToLowerInvariant() == "true";
	}
}
