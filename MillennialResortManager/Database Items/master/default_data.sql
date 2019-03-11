USE [MillennialResort_DB]
GO
--Default data that is required for the functional deployment of the database
INSERT INTO [dbo].[RoomType]
           ([RoomTypeID],[Description],[Active])
     VALUES
           ('UNASSIGNED','Room type is unnassigned',1)
GO
INSERT INTO [dbo].[GuestType]
           ([GuestTypeID],[Description])
     VALUES
           ('Basic guest','')
GO
INSERT INTO [dbo].[PetType]
           ([PetTypeID],[Description])
     VALUES
           ('UNASSIGNED','Pets that have not been assigned a type')
GO