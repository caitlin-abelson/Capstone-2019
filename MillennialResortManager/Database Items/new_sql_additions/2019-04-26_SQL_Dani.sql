SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
GO

print '' print '*** Create Procedure sp_update_inspection ***'
GO
/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
CREATE PROCEDURE [dbo].[sp_update_inspection]
	(
		@ResortPropertyID					[int],
		@InspectionID						[int],
		
		@OldName							[nvarchar](50),
		@OldDateInspected					[date],
		@OldRating							[nvarchar](50),
		@OldResortInspectionAffiliation		[nvarchar](25),
		@OldInspectionProblemNotes			[nvarchar](1000),
		@OldInspectionFixNotes				[nvarchar](1000),

		@NewName							[nvarchar](50),
		@NewDateInspected					[date],
		@NewRating							[nvarchar](50),
		@NewResortInspectionAffiliation		[nvarchar](25),
		@NewInspectionProblemNotes			[nvarchar](1000),
		@NewInspectionFixNotes				[nvarchar](1000)		
	)
AS
	BEGIN
		
		UPDATE 	[Inspection]
		
		SET		[Name] = @NewName,
				[DateInspected] = @NewDateInspected,
				[Rating] = @NewRating,
				[ResortInspectionAffiliation] = @NewResortInspectionAffiliation,
				[InspectionProblemNotes] = @NewInspectionProblemNotes,
				[InspectionFixNotes] = @NewInspectionFixNotes
		
		WHERE	[ResortPropertyID] = @ResortPropertyID
		AND		[InspectionID] = @InspectionID
		AND		[Name] = @OldName
		AND		[DateInspected] = @OldDateInspected
		AND		[Rating] = @OldRating
		AND		[ResortInspectionAffiliation] = @OldResortInspectionAffiliation
		AND		[InspectionProblemNotes] = @OldInspectionProblemNotes
		AND		[InspectionFixNotes] = @OldInspectionFixNotes
		
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
print '' print '*** Creating sp_insert_work_order'
GO
CREATE PROCEDURE sp_create_work_order
	(
	@MaintenanceTypeID				[nvarchar](50),
	@DateRequested					[Date],
	@RequestingEmployeeID			[int],
	@WorkingEmployeeID				[int],
	@Description					[nvarchar](1000),
	@MaintenanceStatus			[nvarchar](50),
	@ResortPropertyID				[int]
	)
AS
	BEGIN
		INSERT INTO [MaintenanceWorkOrder]
			([MaintenanceTypeID], [DateRequested], [RequestingEmployeeID], 
			[WorkingEmployeeID], [Description], [MaintenanceStatus], [ResortPropertyID])
		VALUES
			(@MaintenanceTypeID, @DateRequested, @RequestingEmployeeID, 
			@WorkingEmployeeID, @Description, @MaintenanceStatus, @ResortPropertyID )
	  
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/
print '' print '*** Creating sp_select_all_work_orders'
GO

CREATE PROCEDURE [dbo].[sp_select_all_work_orders] 
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/
print '' print '*** Creating sp_select_work_order_by_id'
GO

CREATE PROCEDURE [dbo].[sp_select_work_order_by_id] 
(
	@MaintenanceWorkOrderID		[int]
)
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
		WHERE [MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/
print '' print '*** Creating sp_select_work_order_by_working_employee_id'
GO

CREATE PROCEDURE [dbo].[sp_select_work_order_by_employee_id] 
(
	@WorkingEmployeeID		[int]
)
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
		WHERE [WorkingEmployeeID] = @WorkingEmployeeID
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/
print '' print '*** Creating sp_select_work_order_by_type'
GO

CREATE PROCEDURE [dbo].[sp_select_work_order_by_type] 
(
	@MaintenanceTypeID		[nvarchar](50)
)
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
		WHERE [MaintenanceTypeID] = @MaintenanceTypeID
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
print '' print '*** Creating sp_update_work_order'
GO
CREATE PROCEDURE [dbo].[sp_update_work_order]
	(
	@MaintenanceWorkOrderID				[int],
	
	@oldMaintenanceTypeID				[nvarchar](50),
	@oldDateRequested					[Date],
	@oldRequestingEmployeeID			[int],
	@oldWorkingEmployeeID				[int],
	@oldDescription						[nvarchar](1000),
	@oldMaintenanceStatusID				[nvarchar](50),
	@oldResortPropertyID				[int],
	@oldComplete						[bit],

	@newMaintenanceTypeID				[nvarchar](50),
	@newRequestingEmployeeID			[int],
	@newWorkingEmployeeID				[int],
	@newDescription						[nvarchar](1000),
	@newComments 						[nvarchar](1000),
	@newMaintenanceStatusID				[nvarchar](50),
	@newResortPropertyID				[int],
	@newComplete						[bit]
	)
AS
	BEGIN
		UPDATE [MaintenanceWorkOrder]
			SET [MaintenanceTypeID] = @newMaintenanceTypeID,
				[RequestingEmployeeID] = @newRequestingEmployeeID,
				[WorkingEmployeeID] = @newWorkingEmployeeID,
				[Description] = @newDescription,
				[Comments] = @newComments,
				[MaintenanceStatus] = @newMaintenanceStatusID,
				[ResortPropertyID] = @newResortPropertyID,
				[Complete] = @newComplete
			WHERE 	
				[MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
			AND [MaintenanceTypeID] = @oldMaintenanceTypeID
			AND	[DateRequested] = @oldDateRequested
			AND	[RequestingEmployeeID] = @oldRequestingEmployeeID
			AND	[WorkingEmployeeID] = @oldWorkingEmployeeID
			AND	[Description] = @oldDescription
			AND	[MaintenanceStatus] = @oldMaintenanceStatusID
			AND	[ResortPropertyID] = @oldResortPropertyID
			AND	[Complete] = @oldComplete 	
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
print '' print '*** Creating sp_deactivate_work_order'
GO

CREATE PROCEDURE [dbo].[sp_deactivate_work_order]
	(
		@MaintenanceWorkOrderID			[int]
	)
AS
	BEGIN
		UPDATE [MaintenanceWorkOrder]
			SET [Complete] = 0
			WHERE 	
				[MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
print '' print '*** Creating sp_delete_work_order'
GO

CREATE PROCEDURE [dbo].[sp_delete_work_order]
	(
		@MaintenanceWorkOrderID 				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [MaintenanceWorkOrder]
		WHERE  [MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
		RETURN @@ROWCOUNT
	END
GO

/*
Created By: Dalton Cleveland
Date:  2-13-201
*/
print '' print '*** Creating sp_select_all_view_model_work_orders'
GO

CREATE PROCEDURE [dbo].[sp_select_all_view_model_work_orders]
AS
	BEGIN
		SELECT [MaintenanceWorkOrder].[MaintenanceWorkOrderID],
		[MaintenanceWorkOrder].[MaintenanceTypeID],
		[MaintenanceWorkOrder].[DateRequested],
		[MaintenanceWorkOrder].[DateCompleted],
		[MaintenanceWorkOrder].[RequestingEmployeeID],
		[MaintenanceWorkOrder].[WorkingEmployeeID],
		[MaintenanceWorkOrder].[Description],
		[MaintenanceWorkOrder].[Comments], 
		[MaintenanceWorkOrder].[Complete],
		[MaintenanceStatus].[MaintenanceStatusID], 
		[MaintenanceStatus].[Description], 
		[MaintenanceType].[MaintenanceTypeID], 
		[MaintenanceType].[Description]
		FROM MaintenanceWorkOrder 
		INNER JOIN MaintenanceType ON MaintenanceWorkOrder.MaintenanceTypeID = MaintenanceType.MaintenanceTypeID
		INNER JOIN MaintenanceStatus ON MaintenanceWorkOrder.MaintenanceStatus = MaintenanceStatus.MaintenanceStatusID
		

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
