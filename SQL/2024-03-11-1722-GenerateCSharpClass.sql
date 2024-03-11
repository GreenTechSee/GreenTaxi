-- run: EXEC GenerateCSharpClass 'TableName'

IF OBJECT_ID('GenerateCSharpClass', 'P') IS NOT NULL 
DROP PROCEDURE GenerateCSharpClass;
GO

CREATE PROCEDURE GenerateCSharpClass
    @tableName VARCHAR(128)
AS
BEGIN
    DECLARE @colName AS VARCHAR(128)
    DECLARE @dataType AS VARCHAR(128)
    DECLARE @isNullable AS BIT
    DECLARE @isIdentity AS BIT
    DECLARE @isPrimaryKey AS BIT
    DECLARE @description AS VARCHAR(MAX)

    PRINT 'public class ' + @tableName + 'Entity : DatabaseTable'
    PRINT '{'
    PRINT '    public override string GetTableName() => nameof(' + @tableName + 'Entity).RemoveFromEnd("Entity");'
    PRINT ''

    DECLARE cur CURSOR FOR
        SELECT c.name, t.name, c.is_nullable, COLUMNPROPERTY(c.object_id,c.name,'IsIdentity') as is_identity,
               CASE WHEN pk.column_id IS NULL THEN 0 ELSE 1 END as is_primary_key, CAST(ep.value AS VARCHAR(MAX))
        FROM sys.columns c
        INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
        LEFT JOIN sys.extended_properties ep ON ep.major_id = c.object_id AND ep.minor_id = c.column_id
        LEFT JOIN (
            SELECT ic.object_id, ic.column_id
            FROM sys.index_columns ic
            JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
            JOIN sys.key_constraints kc ON i.object_id = kc.parent_object_id AND i.name = kc.name
            WHERE kc.type = 'PK'
        ) pk ON c.object_id = pk.object_id AND c.column_id = pk.column_id
        WHERE c.object_id = OBJECT_ID(@tableName)

    OPEN cur

    FETCH NEXT FROM cur INTO @colName, @dataType, @isNullable, @isIdentity, @isPrimaryKey, @description

    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @isPrimaryKey = 1 PRINT '    [IsPrimaryKey]'
        IF @isIdentity = 1 PRINT '    [IsIdentity]'
        IF @description IS NOT NULL PRINT '    /// <summary>'
        IF @description IS NOT NULL PRINT '    /// ' + @description
        IF @description IS NOT NULL PRINT '    /// </summary>'
        PRINT '    public ' + 
            CASE 
                WHEN @dataType = 'bigint' THEN 'long'
                WHEN @dataType = 'nvarchar' THEN 'string'
                WHEN @dataType = 'date' THEN 'DateTime'
                WHEN @dataType = 'smalldatetime' THEN 'DateTime'
                WHEN @dataType = 'int' THEN 'int'
                WHEN @dataType = 'bit' THEN 'bool'
                -- Add more mappings as needed
                ELSE 'UNKNOWN_TYPE_' + @dataType
            END
            + CASE WHEN @isNullable = 1 THEN '?' ELSE '' END
            + ' ' + @colName + ' { get; set; }'
        PRINT ''
        FETCH NEXT FROM cur INTO @colName, @dataType, @isNullable, @isIdentity, @isPrimaryKey, @description
    END

    CLOSE cur
    DEALLOCATE cur

    PRINT '}'
END
GO