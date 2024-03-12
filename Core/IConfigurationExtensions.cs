using System;
using Microsoft.Extensions.Configuration;

namespace Core;

// ReSharper disable once InconsistentNaming
public static class IConfigurationExtensions
{
	/// <summary>
	/// Returns configuration key. Throws if it is null.
	/// </summary>
	public static string GetOrThrow(this IConfiguration configuration, string key)
	{
		return configuration[key] ?? throw new Exception($"Missing configuration: {key}");
	}
}
