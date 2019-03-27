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
INSERT INTO [dbo].[EventType]
           ([EventTypeID],[Description])
     VALUES
           ('UNASSIGNED','Event that has not been assigned a type')
GO
INSERT INTO [dbo].[ItemType]
           ([ItemTypeID],[Description])
     VALUES
           ('UNASSIGNED','Item that has not been assigned a type')
GO
INSERT INTO [dbo].[OfferingType]
           ([OfferingTypeID],[Description])
     VALUES
           ('UNASSIGNED','Offering that has not been assigned a type')
GO

