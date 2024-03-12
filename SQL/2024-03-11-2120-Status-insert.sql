USE [greentaxi-dev]
GO

INSERT INTO [dbo].[Status]
           ([StatusId]
           ,[IsActive]
           ,[Name]
           ,[Description])
     VALUES
           (1
           ,1
           ,'Normal'
           ,'Det er ikke noe farevarsel aktivt i regionen for øyeblikket')
GO

INSERT INTO [dbo].[Status]
           ([StatusId]
           ,[IsActive]
           ,[Name]
           ,[Description])
     VALUES
           (2
           ,0
           ,'Risikabelt'
           ,'Det er et oransje farevarsel aktivt i regionen for øyeblikket')
GO

INSERT INTO [dbo].[Status]
           ([StatusId]
           ,[IsActive]
           ,[Name]
           ,[Description])
     VALUES
           (3
           ,0
           ,'Farlig'
           ,'Det er et rødt farevarsel aktivt i regionen for øyeblikket')
GO
