/*
   mandag 11. mars 202419:34:30
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
ALTER TABLE dbo.Home
	DROP CONSTRAINT FK_Home_Person
GO
ALTER TABLE dbo.Person SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Person', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Person', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Person', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Home
	(
	Id bigint NOT NULL,
	PersonId nvarchar(20) NOT NULL,
	Name nvarchar(50) NOT NULL,
	Adress nvarchar(250) NOT NULL,
	NumberOfInhabitants int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Home SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Home)
	 EXEC('INSERT INTO dbo.Tmp_Home (Id, PersonId, Name, Adress)
		SELECT Id, PersonId, Name, Adress FROM dbo.Home WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Home
GO
EXECUTE sp_rename N'dbo.Tmp_Home', N'Home', 'OBJECT' 
GO
ALTER TABLE dbo.Home ADD CONSTRAINT
	PK_Home PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Home ADD CONSTRAINT
	FK_Home_Person FOREIGN KEY
	(
	PersonId
	) REFERENCES dbo.Person
	(
	Fnr
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Home', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Home', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Home', 'Object', 'CONTROL') as Contr_Per 