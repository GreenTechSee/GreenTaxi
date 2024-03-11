using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Core.Entities;

internal class Status : DatabaseTable
{
	public override string GetTableName() => nameof(Status);
	public int StatusId { get; set; }
}
