-- AUTHOR  : Francis Mingomba
-- CREATED : 2019/04/15

USE [MillennialResort_DB]
GO

-- RESORT PROPERTY TYPE STORED PROCEDURES UPDATE --------------------------------------------------
-- sp_delete_resort_property_type_by_id
print '' print '*** altering sp_delete_resort_property_type_by_id'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.sp_delete_resort_property_type_by_id'))
   exec('CREATE PROCEDURE [dbo].[sp_delete_resort_property_type_by_id] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[sp_delete_resort_property_type_by_id] 
	(
		@ResortPropertyTypeId		[nvarchar](20)
	)
AS
	BEGIN
		DELETE
		FROM	[ResortPropertyType]
		WHERE	[ResortPropertyTypeId] = @ResortPropertyTypeId

		RETURN @@ROWCOUNT
	END
GO
-- END OF RESORT PROPERTY STORED PROCEDURES---------------------------------------------------------


-- RESORT VEHICLE CHECKOUT STORED PROCEDURES UPDATE --------------------------------------------------
-- sp_create_vehicle_checkout
print '' print '*** atering sp_create_vehicle_checkout'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.sp_create_vehicle_checkout'))
   exec('CREATE PROCEDURE [dbo].[sp_create_vehicle_checkout] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[sp_create_vehicle_checkout]
	(
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
			(   [EmployeeId]
				, [DateCheckedOut]
				, [DateReturned]
				, [DateExpectedBack]
				, [Returned]
				, [ResortVehicleId]
			) 
		VALUES
			(
				  @EmployeeId			
				, @DateCheckedOut		
				, @DateReturned		
				, @DateExpectedBack 
				, @Returned			
				, @ResortVehicleId	
			)
		SELECT SCOPE_IDENTITY() 
	END
GO