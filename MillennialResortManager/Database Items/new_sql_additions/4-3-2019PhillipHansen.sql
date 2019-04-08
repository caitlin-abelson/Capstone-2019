USE [MillennialResort_DB]
GO


print '' print '***Altering multiple Event Stored Procedures***'
GO
print '' print 'sp_insert_event'
DROP PROCEDURE [dbo].[sp_insert_event]
GO
CREATE PROCEDURE [dbo].[sp_insert_event]
(
		@EventTitle 	[nvarchar](50),
		@EmployeeID		[int],
		@EventTypeID 	[nvarchar](15),
		@Description 	[nvarchar](1000),
		@EventStartDate [date],
		@EventEndDate 	[date],
		@KidsAllowed 	[bit],
		@SeatsRemaining [int],
		@NumGuests 		[int],
		@Location 		[nvarchar](50),
		@PublicEvent 	[bit],
		@Sponsored		[bit],
		@Approved 		[bit],
		@Price			[money]
)
AS
	BEGIN
		INSERT INTO [dbo].[Offering]
		([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
		('Event', @EmployeeID, @Description, @Price)
		DECLARE @NewOfferingID [int] = (SELECT @@IDENTITY)
	
		INSERT INTO [dbo].[Event]
			([OfferingID]
			,[EventTitle]
			,[EmployeeID]
			,[EventTypeID]
			,[Description]
			,[EventStartDate]
			,[EventEndDate]
			,[KidsAllowed]
			,[SeatsRemaining]
			,[NumGuests]
			,[Location]
			,[PublicEvent]
			,[Sponsored]
			,[Approved])
			VALUES
			(@NewOfferingID
			,@EventTitle
			,@EmployeeID
			,@EventTypeID
			,@Description
			,@EventStartDate
			,@EventEndDate
			,@KidsAllowed
			,@SeatsRemaining
			,@NumGuests
			,@Location
			,@PublicEvent
			,@Sponsored
			,@Approved
			)

			RETURN SCOPE_IDENTITY()
	END
GO
print '' print'sp_retrieve_event'
GO
DROP PROCEDURE [dbo].[sp_retrieve_event]
GO
CREATE PROCEDURE [dbo].[sp_retrieve_event]
	(
		@EventID [int]
	)
AS
	BEGIN
		SELECT  [EventID],
				[Event].[OfferingID],
				[EventTitle],
				[Event].[EmployeeID],
				[Employee].[FirstName],
				[EventTypeID],
				[Event].[Description],
				[EventStartDate],
				[EventEndDate],
				[KidsAllowed],
				[NumGuests],
				[SeatsRemaining],
				[Location],
				[Sponsored],
				[Approved],
				[PublicEvent],
				[Price]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
		ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
		INNER JOIN [dbo].[Offering]
		ON		[Offering].[OfferingID] = [Event].[OfferingID]
		WHERE	[EventID] = @EventID
	END
GO
print '' print'sp_update_event'
DROP PROCEDURE [dbo].[sp_update_event]
GO
CREATE PROCEDURE [dbo].[sp_update_event]
	(
		@EventID				[int],
		@EventTitle				[nvarchar](50),
		@EmployeeID			 	[int],
		@EventTypeID			[nvarchar](15),
		@Description			[nvarchar](1000),
		@EventStartDate			[date],
		@EventEndDate			[date],
		@KidsAllowed			[bit],
		@NumGuests				[int],
		@SeatsRemaining			[int],
		@Location				[nvarchar](50),
		@Sponsored				[bit],
		@Approved				[bit],
		@PublicEvent			[bit],

		@OldEventTitle			[nvarchar](50),
		@OldOfferingID			[int],
		@OldEmployeeID			[int],
		@OldEventTypeID			[nvarchar](15),
		@OldDescription			[nvarchar](1000),
		@OldEventStartDate		[date],
		@OldEventEndDate		[date],
		@OldKidsAllowed			[bit],
		@OldNumGuests			[int],
		@OldSeatsRemaining		[int],
		@OldLocation			[nvarchar](50),
		@OldSponsored			[bit],
		@OldApproved			[bit],
		@OldPublicEvent			[bit]

	)
AS
	BEGIN
		UPDATE [Event]
		SET		[EventTitle] = @EventTitle,
				[EmployeeID] = @EmployeeID,
				[EventTypeID] = @EventTypeID,
				[Description] = @Description,
				[EventStartDate] = @EventStartDate,
				[EventEndDate] = @EventEndDate,
				[KidsAllowed] = @KidsAllowed,
				[NumGuests] = @NumGuests,
				[Location] = @Location,
				[Sponsored] = @Sponsored,
				[Approved] = @Approved,
				[SeatsRemaining] = @SeatsRemaining,
				[PublicEvent] = @PublicEvent
		FROM 	[dbo].[Event]
		WHERE	[EventID] = @EventID
		AND		[OfferingID] = @OldOfferingID
		AND 	[EventTitle] = @OldEventTitle
		AND		[EmployeeID] = @OldEmployeeID
		AND		[EventTypeID] = @OldEventTypeID
		AND		[Description] = @OldDescription
		AND		[EventStartDate] = @OldEventStartDate
		AND		[EventEndDate] = @OldEventEndDate
		AND		[KidsAllowed] = @OldKidsAllowed
		AND		[SeatsRemaining] = @OldSeatsRemaining
		AND		[NumGuests] = @OldNumGuests
		AND		[Location] = @OldLocation
		AND 	[Sponsored] = @OldSponsored
		AND		[Approved] = @OldApproved
		AND		[PublicEvent] = @OldPublicEvent

			RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating table EventSponsor'
GO
CREATE TABLE [dbo].[EventSponsor](
	[EventID]		[int]	NOT NULL,
	[SponsorID]		[int]	NOT NULL

	CONSTRAINT [pk_EventID_SponsorID]
		PRIMARY KEY([EventID] ASC, [SponsorID] ASC)
)
GO

print '' print '*** Creating sp_insert_event_sponsor'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_sponsor]
	(
		@EventID		[int],
		@SponsorID		[int]
	)
AS
	BEGIN	
		
		INSERT INTO 	[EventSponsor]
			([EventID], [SponsorID])
		VALUES
			(@EventID, @SponsorID)
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_all_event_sponsors'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_event_sponsors]
AS
	BEGIN	
		SELECT 	[EventSponsor].[EventID], [Event].[EventTitle],
					[Sponsor].[Name], [EventSponsor].[SponsorID]
		FROM	[Event] INNER JOIN [EventSponsor]
				ON	[Event].[EventID] = [EventSponsor].[EventID]
		INNER JOIN [Sponsor] ON [EventSponsor].[SponsorID] = [Sponsor].[SponsorID]
		WHERE [Event].[EventID] = [EventSponsor].[EventID]
		AND 	[Sponsor].[SponsorID] = [EventSponsor].[SponsorID]
		AND		[Event].[Sponsored] = 1
	END
GO

print '' print '*** Creating sp_delete_event_sponsor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_event_sponsor_by_id]
	(
		@EventID		[int],
		@SponsorID		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[EventSponsor]
		WHERE 	[EventID] = @EventID
		AND		[SponsorID] = @SponsorID
	END
GO

print '' print '*** Creating sp_retrieve_all_events_uncancelled'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_events_uncancelled]
AS
	BEGIN
		SELECT
		[EventID],
		[Event].[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Event].[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[Cancelled],
		[PublicEvent],
		[Offering].[Price]
		
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
			INNER JOIN [dbo].[Offering] 
			ON [Offering].[OfferingID] = [Event].[OfferingID]
		WHERE [Cancelled] = 0
	END
GO

print '' print '*** Creating sp_retrieve_all_events_uncancelled'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_events_cancelled]
AS
	BEGIN
		SELECT
		[EventID],
		[Event].[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Event].[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[Cancelled],
		[PublicEvent],
		[Offering].[Price]
		
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
			INNER JOIN [dbo].[Offering] 
			ON [Offering].[OfferingID] = [Event].[OfferingID]
		WHERE [Cancelled] = 1
	END
GO

print '' print '*** Creating sp_update_event_to_uncancelled'
GO
CREATE PROCEDURE [dbo].[sp_update_event_to_uncancelled]
	(
		@EventID		[int]
	)
AS
	BEGIN
		UPDATE 	[Event]
		SET		[Cancelled] = 0
		WHERE	[EventID] = @EventID
		AND		[Cancelled] = 1
	END
GO

print '' print '*** Creating sp_update_event_to_cancelled'
GO
CREATE PROCEDURE [dbo].[sp_update_event_to_cancelled]
	(
		@EventID		[int]
	)
AS
	BEGIN
		UPDATE 	[Event]
		SET		[Cancelled] = 1
		WHERE	[EventID] = @EventID
		AND		[Cancelled] = 0
	END
GO

print '' print '*** Creating sp_select_all_sponsors'
GO
CREATE PROCEDURE [dbo].[sp_select_all_sponsors]
AS
	BEGIN
		SELECT 	[SponsorID],[Name],[Address],[City],[State],[PhoneNumber],
					[Email],[ContactFirstName],[ContactLastName],[DateAdded],[Active]
		FROM	[dbo].[Sponsor]
	END
GO