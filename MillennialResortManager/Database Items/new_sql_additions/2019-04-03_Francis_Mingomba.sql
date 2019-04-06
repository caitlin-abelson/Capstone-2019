-- AUTHOR  : Francis Mingomba
-- CREATED : 04/03/2019

USE [MillennialResort_DB]
GO

-- RESORT VEHICLE -----------------------------------------------------------------
print '' print '*** creating ResortVehicle'
GO
CREATE TABLE [dbo].[ResortVehicle](
	[VehicleID]			    [int] IDENTITY(100000, 1) 	NOT NULL,
	[Make]				    [nvarchar](30)				NOT NULL,
	[Model]				    [nvarchar](30)				NOT NULL,
	[Year]	                [int]						NOT NULL,
	[License]			    [nvarchar](10)				NOT NULL,
	[Mileage]			    [int]						NOT NULL,
	[Capacity]			    [int]						NOT NULL,
	[Color]				    [nvarchar](30)				NOT NULL,
	[PurchaseDate]		    [Date]						NOT NULL,
	[Description]		    [nvarchar](1000)			NOT NULL,
	[Active]			    [bit]						NOT NULL,
	[DeactivationDate]	    [Date]								,
	[Available]			    [bit]						NOT NULL,
	[ResortVehicleStatusId]	[nvarchar](25)				NOT NULL,
	[ResortPropertyId]		[int]						NOT NULL
	
	CONSTRAINT [pk_VehicleID] PRIMARY KEY([VehicleID] ASC)
)
GO

-- Stored Procedures

print '' print '*** creating sp_create_vehicle'
GO
CREATE PROCEDURE [dbo].[sp_create_vehicle]
	(
		@Make				   [nvarchar](30),			
		@Model				   [nvarchar](30),
		@Year	               [int],					
		@License			   [nvarchar](10),			
		@Mileage			   [int],							
		@Capacity			   [int],					
		@Color				   [nvarchar](30),			
		@PurchaseDate		   [Date],					
		@Description		   [nvarchar](1000),		
		@Active				   [bit],					
		@DeactivationDate	   [Date],
		@Available			   [bit],
		@ResortVehicleStatusId [nvarchar] (25),
		@ResortPropertyId	   [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortVehicle] 
			(
				 [Make] 
				, [Model]
				, [Year]
				, [License]
				, [Mileage]
				, [Capacity]
				, [Color]
				, [PurchaseDate]
				, [Description]
				, [Active]
				, [DeactivationDate]
				, [Available]
				, [ResortVehicleStatusId]
				, [ResortPropertyId]
			) 
		VALUES
			(
				  @Make
				, @Model
				, @Year
				, @License
				, @Mileage
				, @Capacity
				, @Color
				, @PurchaseDate
				, @Description
				, @Active
				, @DeactivationDate
				, @Available
				, @ResortVehicleStatusId
				, @ResortPropertyId
			)
		SELECT SCOPE_IDENTITY() 
	END
GO

print '' print '*** creating sp_select_vehicles'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicles]
AS
	BEGIN
		SELECT   [VehicleID]
		       , [Make]
			   , [Model]
			   , [Year]
			   , [License]
			   , [Mileage]
			   , [Capacity]
			   , [Color]
			   , [PurchaseDate]
			   , [Description]
			   , [Active]
			   , [DeactivationDate]
			   , [Available]
			   , [ResortVehicleStatusId]
			   , [ResortPropertyId]
		FROM [ResortVehicle]
	END
GO

print '' print '*** creating sp_select_vehicle_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_by_id]
	(
		@VehicleId [int]	
	)
AS
	BEGIN
		SELECT [VehicleID]
				, [Make]
				, [Model]
				, [Year]
				, [License]
				, [Mileage]
				, [Capacity]
				, [Color]
				, [PurchaseDate]
				, [Description]
				, [Active]
				, [DeactivationDate]
				, [Available]
				, [ResortVehicleStatusId]
				, [ResortPropertyId]
		FROM [ResortVehicle]
		WHERE [VehicleID] = @VehicleId
	END
GO

print '' print '*** creating sp_update_vehicle'
GO
CREATE PROCEDURE [dbo].[sp_update_vehicle]
	(
		@VehicleId				  [int],
		@OldMake				  [nvarchar](30),			
		@OldModel				  [nvarchar](30),			
		@OldYear	              [int],					
		@OldLicense				  [nvarchar](10),			
		@OldMileage				  [int],		
		@OldCapacity			  [int],					
		@OldColor				  [nvarchar](30),			
		@OldPurchaseDate		  [Date],					
		@OldDescription			  [nvarchar](1000),		
		@OldActive				  [bit],					
		@DeactivationDate		  [Date],
		@OldAvailable			  [bit],
		@OldResortVehicleStatusId [nvarchar](25),
		@OldResortPropertyId	  [int],
		
		@NewMake				  [nvarchar](30),			
		@NewModel				  [nvarchar](30),			
		@NewYear	              [int],					
		@NewLicense				  [nvarchar](10),			
		@NewMileage				  [int],							
		@NewCapacity			  [int],					
		@NewColor				  [nvarchar](30),			
		@NewPurchaseDate		  [Date],					
		@NewDescription			  [nvarchar](1000),		
		@NewActive				  [bit],
		@NewAvailable			  [bit],
		@NewResortVehicleStatusId [nvarchar](25),
		@NewResortPropertyId	  [int]
	)
AS
	BEGIN
		UPDATE [ResortVehicle]
			SET [Make]				    = @NewMake,
				[Model]				    = @NewModel,					
				[Year]	                = @NewYear,				
				[License]			    = @NewLicense,					
				[Mileage]			    = @NewMileage,									
				[Capacity]			    = @NewCapacity,
				[Color]				    = @NewColor,				
				[PurchaseDate]		    = @NewPurchaseDate,
				[Description]		    = @NewDescription,				
				[Active]			    = @NewActive,			
				[DeactivationDate]      = @DeactivationDate,
				[Available]             = @NewAvailable,
				[ResortVehicleStatusId] = @NewResortVehicleStatusId,
				[ResortPropertyId]		= @NewResortPropertyId
			WHERE 	    [VehicleId]             = @VehicleId
					AND	[Make]				    = @OldMake
					AND [Model]				    = @OldModel
					AND [Year]	                = @OldYear
					AND [License]			    = @OldLicense
					AND [Mileage]			    = @OldMileage
					AND [Capacity]			    = @OldCapacity
					AND [Color]				    = @OldColor
					AND [PurchaseDate]		    = @OldPurchaseDate
					AND [Description]		    = @OldDescription
					AND [Active]			    = @OldActive
					AND [Available]			    = @OldAvailable
					AND [ResortVehicleStatusId] = @OldResortVehicleStatusId
					AND [ResortPropertyId]	 	= @OldResortPropertyId
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_delete_vehicle_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_vehicle_by_id]
	(
		@VehicleId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[ResortVehicle]
		WHERE	[VehicleId] = @VehicleId
		  AND	[Active] = 0

		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_deactivate_vehicle_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_vehicle_by_id]
	(
		@VehicleId		[int]
	)
AS
	BEGIN
		UPDATE 	[ResortVehicle]
		SET 	[Active] = 0
		WHERE	[VehicleId] = @VehicleId

		RETURN @@ROWCOUNT
	END
GO

-- END OF RESORT VEHICLE ---------------------------------------------------------------

-- RESORT VEHICLE STATUS----------------------------------------------------------------
-- TABLES
print '' print '*** creating ResortVehicleStatus Table'
GO
CREATE TABLE [dbo].[ResortVehicleStatus](
	[ResortVehicleStatusId] [nvarchar](25) 	    NOT NULL,
	[Description]			[nvarchar](1000)    NOT NULL
	
	CONSTRAINT [pk_ResortVehicleStatusId] PRIMARY KEY([ResortVehicleStatusId] ASC)
)
GO

-- STORED PROCEDURES
print '' print '*** creating sp_create_resort_vehicle_status'
GO
CREATE PROCEDURE [dbo].[sp_create_resort_vehicle_status]
	(
		@ResortVehicleStatusId	[nvarchar](20),
		@Description			[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortVehicleStatus] ([ResortVehicleStatusId],[Description]) 
		VALUES(@ResortVehicleStatusId, @Description)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- sp_select_resort_vehicle_statuses
print '' print '*** creating sp_select_resort_vehicle_statuses'
GO
CREATE PROCEDURE [dbo].[sp_select_resort_vehicle_statuses]
AS
	BEGIN
		SELECT [ResortVehicleStatusId],[Description]
		FROM [ResortVehicleStatus]
	END
GO

-- sp_select_resort_vehicle_status_by_id
print '' print '*** creating sp_select_resort_vehicle_status_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_resort_vehicle_status_by_id]
	(
		@ResortVehicleStatusId [int]	
	)
AS
	BEGIN
		SELECT [ResortVehicleStatusId], [Description]
		FROM [ResortVehicleStatus]
		WHERE [ResortVehicleStatusId] = @ResortVehicleStatusId
	END
GO

-- sp_update_resort_vehicle_status
print '' print '*** creating sp_update_resort_vehicle_status'
GO
CREATE PROCEDURE [dbo].[sp_update_resort_vehicle_status]
	(
		@ResortVehicleStatusId	[nvarchar](25),
		
		@OldDescription	[nvarchar](1000),
		
		@NewDescription	[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [ResortVehicleStatus]
			SET [Description]			 = @NewDescription
			WHERE       [ResortVehicleStatusId] = @ResortVehicleStatusId
					AND [Description] = @OldDescription
		RETURN @@ROWCOUNT
	END
GO

-- sp_delete_resort_vehicle_status_by_id
print '' print '*** creating sp_delete_resort_vehicle_status_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_resort_vehicle_status_by_id]
	(
		@ResortVehicleStatusId		[nvarchar](25)
	)
AS
	BEGIN
		DELETE
		FROM	[ResortVehicleStatus]
		WHERE	[ResortVehicleStatusId] = @ResortVehicleStatusId

		RETURN @@ROWCOUNT
	END
GO

-- END OF RESORT VEHICLE STATUS-------------------------------------------------------------

-- VEHICLE CHECKOUT ------------------------------------------------------------------------
--TABLES

-- VehicleCheckout Table
print '' print '*** creating VehicleCheckout Table'
GO
CREATE TABLE [dbo].[VehicleCheckout](
	[VehicleCheckoutId]		[int]IDENTITY(100000, 1)	NOT NULL,
	[EmployeeId] 			[int] 	    				NOT NULL,
	[DateCheckedOut] 		[Date] 	    				NOT NULL,
	[DateReturned] 			[Date] 	            				,
	[DateExpectedBack]		[Date] 	    				NOT NULL,
	[Returned] 			    [bit] 	            				,
	[ResortVehicleId] 		[int]						NOT NULL
	
	CONSTRAINT [pk_VehicleCheckoutId] PRIMARY KEY([VehicleCheckoutId] ASC)
)
GO

-- STORED PROCEDURES
-- VehicleCheckout
-- sp_create_vehicle_checkout
print '' print '*** creating sp_create_vehicle_checkout'
GO
CREATE PROCEDURE [dbo].[sp_create_vehicle_checkout]
	(
		@VehicleCheckoutId	[int],
		@EmployeeId			[int],
		@DateCheckedOut		[Date],
		@DateReturned		[Date],
		@DateExpectedBack	[Date],
		@Returned			[bit],
		@ResortVehicleId	[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[VehicleCheckout] 
			(	  [VehicleCheckoutId]
				, [EmployeeId]
				, [DateCheckedOut]
				, [DateReturned]
				, [DateExpectedBack]
				, [Returned]
				, [ResortVehicleId]
			) 
		VALUES
			(
				  @VehicleCheckoutId
				, @EmployeeId			
				, @DateCheckedOut		
				, @DateReturned		
				, @DateExpectedBack 
				, @Returned			
				, @ResortVehicleId	
			)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- sp_select_vehicle_checkouts
print '' print '*** creating sp_select_vehicle_checkouts'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_checkouts]
AS
	BEGIN
		SELECT [VehicleCheckoutId]
		       , [EmployeeId]
			   , [DateCheckedOut]
			   , [DateReturned]
			   , [DateExpectedBack]
			   , [Returned]
			   , [ResortVehicleId]
		FROM [VehicleCheckout]
	END
GO

-- sp_select_vehicle_checkout_by_id
print '' print '*** creating sp_select_vehicle_checkout_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_checkout_by_id]
	(
		@VehicleCheckoutId [int]	
	)
AS
	BEGIN
		SELECT   [VehicleCheckoutId]
		       , [EmployeeId]
			   , [DateCheckedOut]
			   , [DateReturned]
			   , [DateExpectedBack]
			   , [Returned]
			   , [ResortVehicleId]
		FROM [VehicleCheckout]
		WHERE [VehicleCheckoutId] = @VehicleCheckoutId
	END
GO

-- sp_update_vehicle_checkout
print '' print '*** creating sp_update_vehicle_checkout'
GO
CREATE PROCEDURE [dbo].[sp_update_vehicle_checkout]
	(
		@VehicleCheckoutId	[int],
		
		@OldEmployeeId			[int],
		@OldDateCheckedOut		[Date],
		@OldDateReturned		[Date],
		@OldDateExpectedBack	[Date],
		@OldReturned			[bit],
		@OldResortVehicleId		[int],
		
		@NewEmployeeId			[int],
		@NewDateCheckedOut		[Date],
		@NewDateReturned		[Date],
		@NewDateExpectedBack	[Date],
		@NewReturned			[bit],
		@NewResortVehicleId		[int]
	)
AS
	BEGIN
		UPDATE [VehicleCheckout]
			SET [EmployeeId]		 = @NewEmployeeId		
				, [DateCheckedOut]	 = @NewDateCheckedOut	
				, [DateReturned]	 = @NewDateReturned	
				, [DateExpectedBack] = @NewDateExpectedBack
				, [Returned]		 = @NewReturned		
				, [ResortVehicleId]	 = @NewResortVehicleId
			WHERE       [VehicleCheckoutId] = @VehicleCheckoutId
					AND [EmployeeId]	    = @OldEmployeeId		
					AND [DateCheckedOut]    = @OldDateCheckedOut		
					AND [DateExpectedBack]  = @OldDateExpectedBack
					AND [Returned]		    = @OldReturned		
					AND [ResortVehicleId]   = @OldResortVehicleId
		RETURN @@ROWCOUNT
	END
GO

-- sp_delete_vehicle_checkout_by_id
print '' print '*** creating sp_delete_vehicle_checkout_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_vehicle_checkout_by_id]
	(
		@VehicleCheckoutId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[VehicleCheckout]
		WHERE	[VehicleCheckoutId] = @VehicleCheckoutId

		RETURN @@ROWCOUNT
	END
GO

-- END OF VEHICLE CHECKOUT ------------------------------------------------------------------------

-- FOREIGN KEY CONTRAINTS -------------------------------------------------------------------------
-- fk_VehicleCheckout_ResortVehicleId_ResortVehicle
print '' print 'creating fk_VehicleCheckout_ResortVehicleId_ResortVehicle'
GO
ALTER TABLE [dbo].[VehicleCheckout] WITH NOCHECK 
ADD  CONSTRAINT [fk_VehicleCheckout_ResortVehicleId_ResortVehicle] FOREIGN KEY([ResortVehicleId])
REFERENCES [dbo].[ResortVehicle] ([VehicleId])
GO

-- fk_ResortVehicle_RVStatusId_ResortVehicleStatus
print '' print 'creating fk_ResortVehicle_RVStatusId_ResortVehicleStatus'
GO
ALTER TABLE [dbo].[ResortVehicle] WITH NOCHECK 
ADD  CONSTRAINT [fk_ResortVehicle_RVStatusId_ResortVehicleStatus] FOREIGN KEY([ResortVehicleStatusId])
REFERENCES [dbo].[ResortVehicleStatus] ([ResortVehicleStatusId])
GO

-- fk_ResortVehicle_ResortPropertyId_ResortProperty
print '' print 'creating fk_ResortVehicle_ResortPropertyId_ResortProperty'
GO
ALTER TABLE [dbo].[ResortVehicle] WITH NOCHECK 
ADD  CONSTRAINT [fk_ResortVehicle_ResortPropertyId_ResortProperty] FOREIGN KEY([ResortPropertyId])
REFERENCES [dbo].[ResortProperty] ([ResortPropertyId])
GO
-- END OF FOREIGN KEY CONTRAINTS -------------------------------------------------------------------------

-- RESORT PROPERTY STORED PROCEDURES----------------------------------------------------------------------
-- sp_create_resort_property
print '' print '*** creating sp_create_resort_property'
GO
CREATE PROCEDURE [dbo].[sp_create_resort_property]
	(
		@ResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortProperty] ([ResortPropertyTypeId]) 
		VALUES(@ResortPropertyTypeId)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- sp_select_resort_properties
print '' print '*** creating sp_select_resort_properties'
GO
CREATE PROCEDURE [dbo].[sp_select_resort_properties]
AS
	BEGIN
		SELECT [ResortPropertyID], [ResortPropertyTypeId]
		FROM [ResortProperty]
	END
GO

-- sp_select_resort_property_by_id
print '' print '*** creating sp_select_resort_property_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_resort_property_by_id]
	(
		@ResortPropertyId [int]	
	)
AS
	BEGIN
		SELECT [ResortPropertyID], [ResortPropertyTypeId]
		FROM [ResortProperty]
		WHERE [ResortPropertyID] = @ResortPropertyId
	END
GO

-- sp_update_resort_property
print '' print '*** creating sp_update_resort_property'
GO
CREATE PROCEDURE [dbo].[sp_update_resort_property]
	(
		@ResortPropertyId			[int],
		@OldResortPropertyTypeId	[nvarchar](20),
		
		@NewResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		UPDATE [ResortProperty]
			SET [ResortPropertyTypeId]	= 	@NewResortPropertyTypeId
			WHERE 	    [ResortPropertyId]  = @ResortPropertyId
					AND	[ResortPropertyTypeId] = @OldResortPropertyTypeId
		RETURN @@ROWCOUNT
	END
GO

-- sp_delete_resort_property_by_id
print '' print '*** creating sp_delete_resort_property_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_resort_property_by_id]
	(
		@ResortPropertyId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[ResortProperty]
		WHERE	[ResortPropertyId] = @ResortPropertyId

		RETURN @@ROWCOUNT
	END
GO
-- END OF RESORT PROPERTY STORED PROCEDURES -------------------------------------------------------------------
 
-- RESORT PROPERTY TYPE STORED PROCEDURES --------------------------------------------------------------------
-- sp_create_resort_property_type
print '' print '*** creating sp_create_resort_property_type'
GO
CREATE PROCEDURE [dbo].[sp_create_resort_property_type]
	(
		@ResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortPropertyType] ([ResortPropertyTypeId]) 
		VALUES(@ResortPropertyTypeId)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- sp_select_resort_property_types
print '' print '*** creating sp_select_resort_property_types'
GO
CREATE PROCEDURE [dbo].[sp_select_resort_property_types]
AS
	BEGIN
		SELECT [ResortPropertyTypeId]
		FROM [ResortPropertyType]
	END
GO

-- sp_select_resort_property_type_by_id
print '' print '*** creating sp_select_resort_property_type_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_resort_property_type_by_id]
	(
		@ResortPropertyTypeId [int]	
	)
AS
	BEGIN
		SELECT [ResortPropertyTypeId]
		FROM [ResortPropertyType]
		WHERE [ResortPropertyTypeId] = @ResortPropertyTypeId
	END
GO

-- sp_update_resort_property_type
print '' print '*** creating sp_update_resort_property_type'
GO
CREATE PROCEDURE [dbo].[sp_update_resort_property_type]
	(
		@OldResortPropertyTypeId	[nvarchar](20),
		
		@NewResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		UPDATE [ResortPropertyType]
			SET [ResortPropertyTypeId]	 = @NewResortPropertyTypeId
			WHERE [ResortPropertyTypeId] = @OldResortPropertyTypeId
		RETURN @@ROWCOUNT
	END
GO

-- sp_delete_resort_property_type_by_id
print '' print '*** creating sp_delete_resort_property_type_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_resort_property_type_by_id]
	(
		@ResortPropertyTypeId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[ResortPropertyType]
		WHERE	[ResortPropertyTypeId] = @ResortPropertyTypeId

		RETURN @@ROWCOUNT
	END
GO
-- END OF RESORT PROPERTY STORED PROCEDURES----------------------------------------------------------------------