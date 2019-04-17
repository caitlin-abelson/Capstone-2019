/*
 * Author: Wes Richardson
 * Created: 2019/04/12
 * This replaces the sp_insert_appointment
 */
USE [MillennialResort_DB]
GO
CREATE PROCEDURE [dbo].[sp_insert_appointment_by_guest]
	(
	@AppointmentTypeID	[nvarchar](15),
	@GuestID			[int],
	@StartDate			[DateTime],
	@EndDate			[DateTime],
	@Description		[nvarchar](1000),
	@ServiceComponentID	[nvarchar](50)
	)
	AS
		BEGIN
		BEGIN TRANSACTION
			BEGIN Try
				Declare @AppointmentID[int]
				INSERT INTO [dbo].[Appointment]
					([AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description])
				VALUES
					(@AppointmentTypeID, @GuestID, @StartDate, @EndDate, @Description)
				SET @AppointmentID = SCOPE_IDENTITY();

				INSERT INTO [dbo].ScheduledItem
					([AppointmentID], [ServiceComponentID])
				VALUES
					(@AppointmentID, @ServiceComponentID)
				DECLARE @OfferingID[int]
				DECLARE @Price[money]
				DECLARE @MemberID[int]
				DECLARE @MemberTabID[int]

				SELECT @OfferingID = OfferingID
				FROM ServiceComponent
				WHERE ServiceComponentID = @ServiceComponentID

				SELECT @Price = Price
				FROM Offering
				WHERE OfferingID = @OfferingID

				SELECT @MemberID = MemberID
				FROM Guest
				WHERE GuestID = @GuestID

				SELECT @MemberTabID = MemberTabID
				FROM MemberTab
				WHERE MemberID = @MemberID
				AND 
				Active = 1

				INSERT INTO [dbo].[MemberTabLine]
					([MemberTabID], [OfferingID], [Quantity], [Price])
				VALUES
					(@MemberTabID, @OfferingID, 1, @Price)
				COMMIT
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
				SET @AppointmentID = -1;
			END CATCH

		SELECT @AppointmentID
	END
GO

/* Wes Richardson
 * Created 2019-04-18
 */

CREATE PROCEDURE [dbo].[sp_retrieve_reservation_by_guestID]
	(
		@GuestID			[int]
	)
	AS
		BEGIN
			DECLARE @MemberID[int]
			SELECT 	@MemberID = MemberID
			FROM 	Guest
			WHERE 	GuestID = @GuestID
			
			SELECT 		
					[ReservationID], [MemberID], [NumberOfGuests], [NumberOfPets], [ArrivalDate], [DepartureDate], [Notes]
			FROM 	[Reservation]
			WHERE	[MemberID] = @MemberID
			AND		[Active] = 1
		END
GO