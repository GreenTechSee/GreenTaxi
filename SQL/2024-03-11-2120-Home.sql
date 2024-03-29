/*
   mandag 11. mars 202421:23:01
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
EXECUTE sp_rename N'dbo.Home.PersonId', N'Tmp_PersonFnr', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Home.Tmp_PersonFnr', N'PersonFnr', 'COLUMN' 
GO
ALTER TABLE dbo.Home SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Home', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Home', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Home', 'Object', 'CONTROL') as Contr_Per 