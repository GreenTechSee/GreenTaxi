using Core.Concepts.AppDatabase.Repositories;
using Microsoft.Extensions.Configuration;

namespace Core.Core.Repositories;

public interface IHomeBeredskapRepository
{
}


public class HomeBeredskapRepository : AppDatabaseRepository, IHomeBeredskapRepository
{
	public HomeBeredskapRepository(IConfiguration configuration) : base(configuration)
	{
	}

	
}
