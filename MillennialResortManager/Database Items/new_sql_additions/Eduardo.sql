USE [MillennialResort_DB]
GO

/*  Name: Eduardo Colon
    Date: 2019-03-25
*/

print '' print '*** Creating ShuttleReservation Table'
GO

CREATE TABLE [dbo].[ShuttleReservation](
	[ShuttleReservationID]				[int] IDENTITY(100000, 1) 			  NOT NULL,
	[GuestID]                           [int]                                 NOT NULL,
	[EmployeeID] 						[int]						  	 	  NOT NULL, 
	[PickupLocation]					[nvarchar](150)						  NOT NULL,
	[DropoffDestination]				[nvarchar](150)						  NOT NULL,
	[PickupDateTime]					[datetime]		   			    	  NOT NULL,				
	[DropoffDateTime]					[datetime]			  				  NULL,
	[Active]							[bit]                                 NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_ShuttleReservationID] PRIMARY KEY([ShuttleReservationID] ASC)

)
GO




/*  Name: Eduardo Colon
    Date: 2019-03-25
*/
print '' print '*** Inserting ShuttleReservation Test Records'


GO

INSERT INTO [ShuttleReservation]
		([GuestID],[EmployeeID] ,[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime])
	VALUES
		(100000,100000 , '1700 Millenium Resort Avenue' , '900 Kirkwood Avenue'  ,'2019-05-01' , '2019-05-01'),
		(100001,100001 , '1800 Millenium Resort Avenue' , '500 Plaze Center'  ,'2019-05-03' , '2019-05-03'),
		(100002,100002 , '1500 Millenium Resort Avenue' , '500 Plaze Center'  ,'2019-05-04' , '2019-05-04')
	



/*  Name: Eduardo Colon
    Date: 2019-03-25 
*/
print '' print '*** Creating sp_retrieve_shuttle_reservations'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_shuttle_reservations]

AS
	BEGIN
		SELECT 	  [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime], [Active]
		FROM	  [ShuttleReservation]
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_shuttle_reservation_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_shuttle_reservation_by_id]
	(
		@ShuttleReservationID			[int]
	)
AS
	BEGIN
		SELECT [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime]
		FROM [ShuttleReservation]
		WHERE ShuttleReservationID = @ShuttleReservationID
	END
GO





/*

 NAME:  Eduardo Colon
 Date:   2019-03-25
*/

print '' print '*** Creating sp_insert_shuttle_reservation'


GO
CREATE PROCEDURE sp_insert_shuttle_reservation
	(
	@GuestID							[int],
	@EmployeeID							[int],
	@PickupLocation						[nvarchar](150),
	@DropoffDestination					[nvarchar](150),
	@PickupDateTime						[datetime],				
	@DropoffDateTime					[datetime]
	
	)
AS
	BEGIN
		INSERT INTO [ShuttleReservation]
			([GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime])
		VALUES
			(@GuestID, 	@EmployeeID,@PickupLocation, @DropoffDestination, @PickupDateTime,@DropoffDateTime)
	  
		RETURN @@ROWCOUNT
	END
GO



/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
  */

print '' print '*** Creating sp_update_shuttle_by_id  '

GO
CREATE PROCEDURE [dbo].[sp_update_shuttle_reservation_by_id]
	(
		
	@ShuttleReservationID			 		[int],
	@OldGuestID							    [int],
	@NewGuestID							    [int],
	@OldEmployeeID 							[int],
	@NewEmployeeID 							[int],
	@OldPickupLocation						[nvarchar](150),
	@NewPickupLocation						[nvarchar](150),
	@OldDropoffDestination					[nvarchar](150),
	@NewDropoffDestination					[nvarchar](150),
	@OldPickupDateTime						[datetime],	
	@NewPickupDateTime						[datetime]
	)
AS
	BEGIN
	
		BEGIN
			UPDATE [ShuttleReservation]
				SET   [GuestID]				 = 	@NewGuestID,
					  [EmployeeID]			 = 	@NewEmployeeID,
					  [PickupLocation]	 	 = 	@NewPickupLocation,
					  [DropoffDestination] 	 = 	@NewDropoffDestination,
					  [PickupDateTime]	 	 =  @NewPickupDateTime
				WHERE [ShuttleReservationID] =  @ShuttleReservationID
				AND	  [GuestID] 			 = 	@OldGuestID
				AND	  [EmployeeID] 			 = 	@OldEmployeeID
				AND   [PickupLocation]       = 	@OldPickupLocation
				AND	  [DropoffDestination]   = 	@OldDropoffDestination
				AND	  [PickupDateTime]       =  @OldPickupDateTime
			RETURN @@ROWCOUNT
		END
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
  */

print '' print '*** Creating sp_shuttle_dropoff_time_now'

GO
CREATE PROCEDURE [dbo].[sp_shuttle_dropoff_time_now]
	(
	@ShuttleReservationID			 		[int],
	@OldGuestID							    [int],
	@OldEmployeeID 							[int],
	@OldPickupLocation						[nvarchar](150),
	@OldDropoffDestination					[nvarchar](150),
	@OldPickupDateTime						[datetime]
	)
AS
	BEGIN
	
		BEGIN
			UPDATE [ShuttleReservation]
				SET   [DropoffDateTime] = getutcdate()
				WHERE [ShuttleReservationID] =  @ShuttleReservationID
				AND	  [GuestID] 			 = 	@OldGuestID
				AND	  [EmployeeID] 			 = 	@OldEmployeeID
				AND   [PickupLocation]       = 	@OldPickupLocation
				AND	  [DropoffDestination]   = 	@OldDropoffDestination
				AND	  [PickupDateTime]       =  @OldPickupDateTime
			RETURN @@ROWCOUNT
		END
	END
GO


/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_delete_ShuttleReservationID '

GO
CREATE PROCEDURE [dbo].[sp_delete_ShuttleReservationID ]
	(
		@ShuttleReservationID		[int]
	)
	
AS
	BEGIN
		DELETE
		FROM [ShuttleReservation]
		WHERE [ShuttleReservationID] = @ShuttleReservationID	
		RETURN @@ROWCOUNT
	END
GO

		
/*

  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_deactivate_shuttle_reservation_by_id '
GO
CREATE PROCEDURE sp_deactivate_shuttle_reservation_by_id 
	(
		@ShuttleReservationID		[int]
	)
AS
	BEGIN
		UPDATE [ShuttleReservation]
		SET 	[Active] = 0
		WHERE 	[ShuttleReservationID] = @ShuttleReservationID
	  
		RETURN @@ROWCOUNT
	END
GO



/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_shuttle_reservation_by_term_in_pickup_location '
GO
CREATE PROCEDURE sp_retrieve_shuttle_reservation_by_term_in_pickup_location
(
		@SearchTerm		[nvarchar](250)
	)
AS
	BEGIN
		SELECT   [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime]
		FROM 	[ShuttleReservation]
		WHERE 	[PickupLocation] LIKE '%' + @SearchTerm + '%'
		
	END
GO


/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_shuttle_reservation_by_term_in_guest_last_name'
GO
CREATE PROCEDURE sp_retrieve_guest_by_term_in_last_name
(
		@SearchTerm		[nvarchar](250)
	)
AS
	BEGIN
		SELECT   [GuestID],[FirstName],[LastName],[PhoneNumber]
		FROM 	[Guest]
		WHERE 	[LastName] LIKE '%' + @SearchTerm + '%'
		
	END
GO


/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_employee_info_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_info_by_id]
(
	@EmployeeID [int]
)
AS
	BEGIN
		SELECT [EmployeeID],[FirstName],[LastName]
		FROM [Employee]
		WHERE [EmployeeID] = @EmployeeID
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_employee_info'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_info]

AS
	BEGIN
		SELECT [EmployeeID],[FirstName],[LastName]
		FROM [Employee]
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_guest_info_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_guest_info_by_id]
(
	@GuestID [int]
)
AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[PhoneNumber]
		FROM [Guest]
		WHERE [GuestID] = @GuestID
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_guest_info'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_guest_info]

AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[PhoneNumber]
		FROM [Guest]
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_shuttle_reservation_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_active_shuttle_reservation]
AS
	BEGIN
		SELECT [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime], [Active]
		FROM [ShuttleReservation]
		WHERE [Active] = 1
	END
GO

/*
  NAME:  Eduardo Colon
  Date:   2019-03-25
*/
print '' print '*** Creating sp_retrieve_shuttle_reservation_inactive'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inactive_shuttle_reservation]
AS
	BEGIN
		SELECT [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime], [Active]
		FROM [ShuttleReservation]
		WHERE [Active] = 0
	END
GO

/*  Name: Eduardo Colon
    Date: 2019-03-25
*/

print '' print '*** Adding Foreign Key GuestID for ShuttleReservation'
GO
ALTER TABLE [dbo].[ShuttleReservation] WITH NOCHECK
	ADD CONSTRAINT [fk_GuestID] FOREIGN KEY([GuestID])
	REFERENCES [dbo].[Guest]([GuestID])
	ON UPDATE CASCADE
GO


/*  Name: Eduardo Colon
    Date: 2019-03-25
*/

print '' print '*** Adding Foreign Key EmployeeID for ShuttleReservation'
GO
ALTER TABLE [dbo].[ShuttleReservation] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE CASCADE
GO