using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Core.Entities.HomeBeredskap;

public class HomeEntity
{
	[IsPrimaryKey]
	[IsIdentity]
	public long Id { get; set; }
	public long PersonId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Adress { get; set; } = string.Empty;
}
