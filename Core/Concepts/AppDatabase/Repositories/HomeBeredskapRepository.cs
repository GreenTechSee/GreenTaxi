using Microsoft.Extensions.Configuration;

namespace Core.Concepts.AppDatabase.Repositories;

public interface IHomeBeredskapRepository
{
}


public class HomeBeredskapRepository : AppDatabaseRepository, IHomeBeredskapRepository
{
	public HomeBeredskapRepository(IConfiguration configuration) : base(configuration)
	{
	}


}
