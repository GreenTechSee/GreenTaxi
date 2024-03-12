using oledid.SyntaxImprovement.Generators.Sql;
using oledid.SyntaxImprovement;

namespace Core.Core.Entities.HomeBeredskap;

public class ItemTypeEntity : DatabaseTable
{
	public override string GetTableName() => nameof(ItemTypeEntity).RemoveFromEnd("Entity");

	[IsIdentity]
	[IsPrimaryKey]
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int RecomendedNumberPerson { get; set; }
}
