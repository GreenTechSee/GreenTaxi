using oledid.SyntaxImprovement.Generators.Sql;

namespace Core.Core.Entities;

public class Status : DatabaseTable
{
	public override string GetTableName() => nameof(Status);
	public int StatusId { get; set; }
	public bool IsActive { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
}
