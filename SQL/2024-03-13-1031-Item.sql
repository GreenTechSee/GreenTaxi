/*
   onsdag 13. mars 202410:31:00
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
ALTER TABLE dbo.Item ADD
	NumberOfUnits int NOT NULL CONSTRAINT DF_Item_NumberOfUnits DEFAULT 1
GO
ALTER TABLE dbo.Item SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Item', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Item', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Item', 'Object', 'CONTROL') as Contr_Per 