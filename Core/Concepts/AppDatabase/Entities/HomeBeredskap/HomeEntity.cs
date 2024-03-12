using oledid.SyntaxImprovement;
using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Core.Entities.HomeBeredskap;

public class HomeEntity : DatabaseTable
{
	public override string GetTableName() => nameof(HomeEntity).RemoveFromEnd("Entity");

	[IsPrimaryKey]
	[IsIdentity]
	public long Id { get; set; }
	public string PersonFnr { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Adress { get; set; } = string.Empty;
	public int numberOfInhabitants { get; set; }

	[Ignore]
	public List<ItemEntity> Items { get; set; } = new List<ItemEntity>();
}
