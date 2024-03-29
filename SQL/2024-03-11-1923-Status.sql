/*
   mandag 11. mars 202419:23:12
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
CREATE TABLE dbo.Status
	(
	StatusId int NOT NULL,
	IsActive bit NOT NULL,
	Name nvarchar(50) NOT NULL,
	Description nvarchar(250) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Status SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Status', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Status', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Status', 'Object', 'CONTROL') as Contr_Per 