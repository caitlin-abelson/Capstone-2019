SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
GO

print '' print '*** Altering Room Table: Removing constraint ak_RoomNumber_BuildingID ***'
GO
ALTER TABLE [dbo].[Room]
	DROP CONSTRAINT [ak_RoomNumber_BuildingID]
GO

print '' print '*** Altering Room Table: Changing RoomNumber datatype to int ***'
GO
ALTER TABLE [dbo].[Room]
	ALTER COLUMN [RoomNumber] [int]
GO

print '' print '*** Altering Room Table: Adding back constraint ak_RoomNumber_BuildingID***'
GO
ALTER TABLE [dbo].[Room]
	ADD CONSTRAINT [ak_RoomNumber_BuildingID] UNIQUE NONCLUSTERED 
		(
			[RoomNumber] ASC,
			[BuildingID] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/* 	Created By:  Dani Russo 
	Created: 04/10/2019
	
*/ 
print '' print '*** Create Procedure sp_select_room_list_by_buildingid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_list_by_buildingid]
	(
		@BuildingID	[nvarchar](50)
	)
AS
	BEGIN
		SELECT DISTINCT 	[Room].[RoomID],
							[Room].[RoomNumber], 
							[Room].[RoomTypeID], 
							[Room].[Description], 
							[Room].[Capacity], 
							[Offering].[Price],
							[Room].[ResortPropertyID], 
							[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
		WHERE [Room].[BuildingID] = @BuildingID
	END
GO


print '' print '*** Drop Procedure sp_select_room_list ***'
GO
DROP PROCEDURE [dbo].[sp_select_room_list]
GO
/* 	Created By:  Dani Russo 
	Created: 03/29/2019
	
	Dani Russo
	Updated: 04/11/2019
	Added Inner Join
*/ 
print '' print '*** Create Procedure sp_select_room_list ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_list]
AS
	BEGIN
		SELECT 	[Room].[RoomID],
				[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity], 
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[OfferingID], 
				[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
	END
GO

print '' print '*** Drop Procedure sp_select_room_by_id ***'
GO
DROP PROCEDURE [dbo].[sp_select_room_by_id]
/* 	Created By:  Dani Russo 
	Created: 03/29/2019
	
	Dani Russo
	Updated: 04/11/2019
	Added Inner Join
*/ 
print '' print '*** Create Procedure sp_select_room_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_by_id]
(
		@RoomID	[int]
)
AS
	BEGIN
		SELECT 	[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity], 
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[OfferingID], 
				[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
		WHERE 	[RoomID] = @RoomID
	END
GO