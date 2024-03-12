using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concepts.AppDatabase.Repositories;

public interface IPersonRepository
{
}

public class PersonRepository : AppDatabaseRepository, IPersonRepository
{
	public PersonRepository(IConfiguration configuration) : base(configuration)
	{
	}
}
