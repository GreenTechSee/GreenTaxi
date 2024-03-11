using Microsoft.Extensions.Configuration;

namespace Core.Concepts.AppDatabase
{
	public static class IConfigurationExtensions
	{
		public static string GetOrThrow(this IConfiguration configuration, string key)
		{
			return configuration[key] ?? throw new Exception($"Missing configuration: {key}");
		}
	}
}
