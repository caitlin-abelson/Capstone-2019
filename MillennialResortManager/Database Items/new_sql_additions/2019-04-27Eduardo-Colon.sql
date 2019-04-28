USE [MillennialResort_DB]
GO

/*

 NAME:  Eduardo Colon
 Date:   2019-04-23
-- May be redundant and deleted later. */
print 'Creating sp_appointment_type_by_id'
GO

CREATE PROCEDURE [dbo].[sp_appointment_type_by_id]
AS
	BEGIN
		SELECT [AppointmentTypeID]
		FROM AppointmentType
	END
GO