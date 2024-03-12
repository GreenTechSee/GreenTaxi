using Core.Concepts.AppDatabase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public class Dependencies
{
	public static void AddDependencies(IServiceCollection services)
	{
		services.AddSingleton<IHomeBeredskapRepository, HomeBeredskapRepository>();
		services.AddSingleton<IPersonRepository, PersonRepository>();
		services.AddSingleton<IStatusRepository, StatusRepository>();
	}
}
