using Core.Concepts.AppDatabase.Repositories;
using Microsoft.Extensions.Configuration;

namespace Core.Core.Repositories;

public class HomeBeredskapRepository : AppDatabaseRepository
{
	public HomeBeredskapRepository(IConfiguration configuration) : base(configuration)
	{
	}
}
