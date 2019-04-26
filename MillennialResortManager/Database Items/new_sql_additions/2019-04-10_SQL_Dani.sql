
SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
GO


print '' print '************************ New Stuff 4/11/2019 **************************'
GO

print '' print '*** Drop Procedure sp_insert_room ***'
GO
DROP PROCEDURE [dbo].[sp_insert_room]
GO

print '' print '*** Create Procedure sp_insert_room ***'
GO

/* 	Created By:  Wes 
	Created: 
	Updated: Danielle Russo
			03/28/2019
			Added Price field
			Added ResortPropertyID 
			
	Updated: Danielle Russo
			04/11/2019
			Changed @RoomNumber datatype to int
*/ 
CREATE PROCEDURE [dbo].[sp_insert_room]
	(
	@RoomNumber			[int],
	@BuildingID			[nvarchar](50),
	@RoomTypeID			[nvarchar](15),
	@Description		[nvarchar](1000),
	@Capacity			[int],
	@RoomStatusID		[nvarchar](25),
	
	@OfferingTypeID		[nvarchar](15),
	@EmployeeID			[int],
	@Price				[Money]
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Room')
		DECLARE @NewResortProperyID [int] = (SELECT @@IDENTITY)

		INSERT INTO [Offering]	
		([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
		(@OfferingTypeID, @EmployeeID, @Description, @Price)	
		DECLARE @NewOfferingID [int] = (SELECT @@IDENTITY)
		
		
		INSERT INTO [dbo].[Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [RoomStatusID],[OfferingID],[ResortPropertyID])
		VALUES
			(@RoomNumber, @BuildingID, @RoomTypeID, @Description, @Capacity, @RoomStatusID, @NewOfferingID, @NewResortProperyID)	
		RETURN @@ROWCOUNT
	END
GO

/* 	Created By:  Dani Russo 
	Created: 04/11/2019
	
	Does not create an offering when new room is created
*/
print '' print '*** Create Procedure sp_insert_room_with_no_price ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_room_with_no_price]
	(
		@RoomNumber			[int],
		@BuildingID			[nvarchar](50),
		@RoomTypeID			[nvarchar](15),
		@Description		[nvarchar](1000),
		@Capacity			[int],
		@RoomStatusID		[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Room')
		DECLARE @NewResortProperyID [int] = (SELECT @@IDENTITY)
		
		
		INSERT INTO [dbo].[Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [RoomStatusID], [ResortPropertyID])
		VALUES
			(@RoomNumber, @BuildingID, @RoomTypeID, @Description, @Capacity, @RoomStatusID, @NewResortProperyID)	
		RETURN @@ROWCOUNT
	END
GO 

/* 	Created By:  Dani Russo 
	Created: 04/11/2019
	
*/
print '' print '*** Create Procedure sp_update_room ***'
GO
CREATE PROCEDURE [dbo].[sp_update_room]
	(
		@RoomID				[int],
		@OfferingID			[int],
		
		@OldRoomNumber			[int],
		@OldBuildingID			[nvarchar](50),
		@OldDescription			[nvarchar](1000),
		@OldRoomTypeID			[nvarchar](15),
		@OldRoomStatusID		[nvarchar](25),
		@OldPrice				[Money],
		@OldCapacity			[int],
		
		@NewRoomNumber			[int],
		@NewBuildingID			[nvarchar](50),
		@NewDescription			[nvarchar](1000),
		@NewRoomTypeID			[nvarchar](15),
		@NewRoomStatusID		[nvarchar](25),
		@NewPrice				[Money],
		@NewCapacity			[int]
		
	)
AS
	BEGIN
		UPDATE 	[Room]
			SET	[RoomNumber] = @NewRoomNumber,
				[BuildingID] = @NewBuildingID,
				[Description] = @NewDescription,
				[RoomStatusID] = @NewRoomStatusID,
				[RoomTypeID] = @NewRoomTypeID,
				[Capacity] = @NewCapacity
		WHERE 	[RoomID] = @RoomID
		AND		[RoomNumber] = @OldRoomNumber
		AND		[BuildingID] = @OldBuildingID
		AND		[Description] = @OldDescription
		AND		[RoomStatusID] = @OldRoomStatusID
		AND		[RoomTypeID] = @OldRoomTypeID
		AND		[Capacity] = @OldCapacity
			
		UPDATE	[Offering]
			SET	[Price] = @NewPrice
		WHERE	[OfferingID] = @OfferingID
		AND		[Price] = @OldPrice
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Drop Procedure sp_select_room_list_by_buildingid ***'
GO
DROP PROCEDURE [dbo].[sp_select_room_list_by_buildingid]
		
/* 	Created By:  Dani Russo 
	Created: 04/10/2019
	
	Dani Russo
	Updated: 04/11/2019
	Added Inner Join
	
*/ 
print '' print '*** Create Procedure sp_select_room_list_by_buildingid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_list_by_buildingid]
	(
		@BuildingID	[nvarchar](50)
	)
AS
	BEGIN
		SELECT  [Room].[RoomID],
				[Room].[RoomNumber], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity],
				[Offering].[OfferingID],	
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
		WHERE [Room].[BuildingID] = @BuildingID
	END
GO		
		
			
			
			
			
			
			
			
			
			
			
			