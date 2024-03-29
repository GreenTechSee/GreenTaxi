/*
   tirsdag 12. mars 202411:48:58
   User: green-taxi
   Server: greentaxi.database.windows.net,1433
   Database: greentaxi-dev
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.ItemType
	(
	Id bigint NOT NULL,
	Name nvarchar(50) NOT NULL,
	RecomendedUnitPerPerson int NOT NULL,
	Unit nvarchar(10) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ItemType ADD CONSTRAINT
	PK_ItemType PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ItemType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ItemType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ItemType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ItemType', 'Object', 'CONTROL') as Contr_Per 