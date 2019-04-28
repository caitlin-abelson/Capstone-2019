USE [MillennialResort_DB]
GO

/*

 NAME:  Eduardo Colon
 Date:   2019-04-23
-- May be redundant and deleted later. */
print 'Creating sp_appointment_type_by_id'

CREATE PROCEDURE [dbo].[sp_appointment_type_by_id]
AS
	BEGIN
		SELECT [AppointmentTypeID]
		FROM AppointmentType
	END
GO

/* Eduardo Colon Script 
   Date: 2019-04-23 
   to be added to the sql procedures file
*/



CREATE PROCEDURE [dbo].[sp_retrieve_appointments]
AS
	BEGIN
		SELECT [AppointmentID], [AppointmentTypeID], [GuestID], 
		[StartDate], [EndDate], [Description]
		FROM Appointment
	END
GO




/*  Created By: Eduardo Colon
  Date:   2019-04-23
*/
GO
CREATE PROCEDURE [dbo].[sp_retrieve_guest_appointment_info]

AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[Email]
		FROM [Guest]
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-04-23
*/
CREATE PROCEDURE [dbo].[sp_retrieve_guest_appointment_info_by_id]
(
	@GuestID [int]
)
AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[Email]
		FROM [Guest]
		WHERE [GuestID] = @GuestID
	END
GO
