/*
   onsdag 13. mars 202408:50:49
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
ALTER TABLE dbo.Item
	DROP CONSTRAINT FK_Item_Home
GO
ALTER TABLE dbo.Home SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Home', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Home', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Home', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Item
	DROP CONSTRAINT FK_Item_ItemType
GO
ALTER TABLE dbo.ItemType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ItemType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ItemType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ItemType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Item
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	ItemTypeId bigint NOT NULL,
	HomeId bigint NOT NULL,
	SellByDate date NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Item SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Item ON
GO
IF EXISTS(SELECT * FROM dbo.Item)
	 EXEC('INSERT INTO dbo.Tmp_Item (Id, ItemTypeId, HomeId, SellByDate)
		SELECT Id, ItemTypeId, HomeId, SellByDate FROM dbo.Item WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Item OFF
GO
DROP TABLE dbo.Item
GO
EXECUTE sp_rename N'dbo.Tmp_Item', N'Item', 'OBJECT' 
GO
ALTER TABLE dbo.Item ADD CONSTRAINT
	PK_Item PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Item ADD CONSTRAINT
	FK_Item_ItemType FOREIGN KEY
	(
	ItemTypeId
	) REFERENCES dbo.ItemType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Item ADD CONSTRAINT
	FK_Item_Home FOREIGN KEY
	(
	HomeId
	) REFERENCES dbo.Home
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Item', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Item', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Item', 'Object', 'CONTROL') as Contr_Per 