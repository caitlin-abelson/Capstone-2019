USE [MillennialResort_DB]
GO

print '' print '*** Creating HouseKeepingRequest Table'
GO
CREATE TABLE [dbo].[HouseKeepingRequest] (
	[HouseKeepingRequestID]				[int] IDENTITY(1, 1)	 	 NOT NULL,
	[BuildingNumber]					[int]				  	 	 NOT NULL,
	[RoomNumber]						[int]			  		 	 NOT NULL,
	[Description]						[nvarchar](1000)			 NOT NULL,
	[WorkingEmployeeID]					[int]								 ,
	[Active]							[bit]						NOT NULL DEFAULT 1
	CONSTRAINT [pk_HouseKeepingRequestID] PRIMARY KEY ([HouseKeepingRequestID] ASC)
)
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Creating sp_create_house_keeping_request'
GO
CREATE PROCEDURE sp_create_house_keeping_request
	(
	@BuildingNumber					[int],
	@RoomNumber						[int],
	@Description					[nvarchar](1000),
	@Active							[bit]
	)
AS
	BEGIN
		INSERT INTO [HouseKeepingRequest]
			([BuildingNumber], [RoomNumber], [Description],[Active])
		VALUES
			(@BuildingNumber, @RoomNumber, @Description, @Active )  
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Creating sp_select_all_house_keeping_requests'
GO

CREATE PROCEDURE [dbo].[sp_select_all_house_keeping_requests] 
AS
	BEGIN
		SELECT 
			[HouseKeepingRequestID], 
			[BuildingNumber], 
			[RoomNumber],
			[Description],
			[WorkingEmployeeID],
			[Active]
		FROM HouseKeepingRequest
	END
GO

/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Creating sp_select_house_keeping_request_by_id'
GO

CREATE PROCEDURE [dbo].[sp_select_house_keeping_request_by_id] 
(
	@HouseKeepingRequestID		[int]
)
AS
	BEGIN
		SELECT 
			[HouseKeepingRequestID], 
			[BuildingNumber], 
			[RoomNumber],
			[Description],
			[WorkingEmployeeID],
			[Active]
		FROM HouseKeepingRequest
		WHERE [HouseKeepingRequestID] = @HouseKeepingRequestID
	END
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Creating sp_update_house_keeping_request'
GO
CREATE PROCEDURE [dbo].[sp_update_house_keeping_request]
	(
	@HouseKeepingRequestID				[int],
	
	@oldBuildingNumber					[int],
	@oldRoomNumber						[int],
	@oldDescription						[nvarchar](1000),
	@oldActive							[bit],
	
	@newBuildingNumber					[int],
	@newRoomNumber						[int],
	@newDescription						[nvarchar](1000),
	@newWorkingEmployeeID				[int],
	@newActive							[bit]	
	)
AS
	BEGIN
		UPDATE [HouseKeepingRequest]
			SET [BuildingNumber] = @newBuildingNumber,
				[RoomNumber] = @newRoomNumber,
				[Description] = @newDescription,
				[Active] = @newActive,
				[WorkingEmployeeID] = @newWorkingEmployeeID
			WHERE [BuildingNumber] = @oldBuildingNumber
				AND [RoomNumber] = @oldRoomNumber
				AND [Description] = @oldDescription
				AND [Active] = @oldActive
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Creating sp_deactivate_house_keeping_request'
GO

CREATE PROCEDURE [dbo].[sp_deactivate_house_keeping_request]
	(
		@HouseKeepingRequestID			[int]
	)
AS
	BEGIN
		UPDATE [HouseKeepingRequest]
			SET [Active] = 0
			WHERE 	
				[HouseKeepingRequestID] = @HouseKeepingRequestID
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Creating sp_delete_house_keeping_request'
GO

CREATE PROCEDURE [dbo].[sp_delete_house_keeping_request]
	(
		@HouseKeepingRequestID 				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [HouseKeepingRequest]
		WHERE  [HouseKeepingRequestID] = @HouseKeepingRequestID
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Inserting House Keeping Request Test Records'
GO

INSERT INTO [HouseKeepingRequest]
			([BuildingNumber], [RoomNumber], [Description], [WorkingEmployeeID], [Active])
		VALUES
			(1, 1, "Timmy Threw Up All Over The Floor.", 100000, 1),
			(2, 2, "We Need Extra Towels For Tomorrow Morning.", 100000, 1)
GO