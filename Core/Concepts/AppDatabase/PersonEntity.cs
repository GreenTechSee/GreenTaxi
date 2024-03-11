using oledid.SyntaxImprovement;
using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Concepts.AppDatabase
{
	public class PersonEntity : DatabaseTable
	{
		public override string GetTableName() => nameof(PersonEntity).RemoveFromEnd("Entity");

		[IsPrimaryKey]
		public string Fnr { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
	}
}
