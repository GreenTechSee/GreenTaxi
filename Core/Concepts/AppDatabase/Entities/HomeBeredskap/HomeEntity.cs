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
	public int NumberOfInhabitants { get; set; }

	[Ignore]
	public List<ItemEntity> Items { get; set; } = new List<ItemEntity>();

	[Ignore]
	public List<ItemTypeEntity>? ItemTypes { get; set; }

	[Ignore]
	public bool HasLackingItems { get
		{
			var itemsGrouped = Items.GroupBy(e => e.ItemTypeId).ToDictionary(e => e.Key, e => e.ToList());
			if (ItemTypes == null) return false;

			foreach (var type in ItemTypes)
			{
				var items = itemsGrouped.GetValueOrNull(type.Id);

				if (items == null)
				{
					return true;
				}

				if (type.ExcludeFromTotal == false)
				{
					var count = 0;
					foreach (var item in Items)
					{
						count += item.NumberOfUnits;
					}
					if (count < (type.RecomendedUnitPerPerson * NumberOfInhabitants))
					{
						return true;
					}
				}
			}

			return false;
		}
	}

	[Ignore]
	public int NumberOfMissingTypes
	{
		get
		{
			var itemsGrouped = Items.GroupBy(e => e.ItemTypeId).ToDictionary(e => e.Key, e => e.ToList());
			if (ItemTypes == null) return 0;

			var i = 0;
			foreach (var type in ItemTypes)
			{
				var items = itemsGrouped.GetValueOrNull(type.Id);

				if (items == null)
				{
					i++;
				}

				else if (type.ExcludeFromTotal == false && items.Count < type.RecomendedUnitPerPerson * NumberOfInhabitants)
				{
					var count = 0;
					foreach (var item in Items)
					{
						count += item.NumberOfUnits;
					}
					if (count < (type.RecomendedUnitPerPerson * NumberOfInhabitants))
					{
						i++;
					}
				}
			}

			return i;
		}
	}

	[Ignore]
	public ItemTypeEntity? firstMissingItemType
	{
		get
		{
			var itemsGrouped = Items.GroupBy(e => e.ItemTypeId).ToDictionary(e => e.Key, e => e.ToList());
			if (ItemTypes == null) return null;

			foreach (var type in ItemTypes)
			{
				var items = itemsGrouped.GetValueOrNull(type.Id);

				if (items == null)
				{
					return type;
				}

				if (type.ExcludeFromTotal == false)
				{
					var count = 0;
					foreach (var item in Items)
					{
						count += item.NumberOfUnits;
					}
					if (count < (type.RecomendedUnitPerPerson * NumberOfInhabitants))
					{
						return type;
					}
				}
			}

			return null;
		}
	}
}
