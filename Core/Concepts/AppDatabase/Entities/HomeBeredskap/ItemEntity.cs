using oledid.SyntaxImprovement;
using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Core.Entities.HomeBeredskap;

public class ItemEntity : DatabaseTable
{
	public override string GetTableName() => nameof(ItemEntity).RemoveFromEnd("Entity");

	[IsIdentity]
	[IsPrimaryKey]
	public long Id { get; set; }
	public long ItemTypeId { get; set; }
	public long HomeId { get; set; }
	public int NumberOfUnits { get; set; }
	public DateTime? SellByDate { get; set; }

	[Ignore]
	public ItemTypeEntity? ItemType { get; set; }

}
