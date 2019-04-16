USE [MillennialResort_DB]
GO
print '' print '*** Creating sp_select_rooms'
GO
CREATE PROCEDURE [dbo].[sp_select_rooms]
AS
	BEGIN
		SELECT 	[Room].[RoomID],
				[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[RoomStatusID],
				[Room].[Capacity], 
				[Room].[ResortPropertyID],
				[Room].[OfferingID]
		FROM 	[Room] 
	END
GO