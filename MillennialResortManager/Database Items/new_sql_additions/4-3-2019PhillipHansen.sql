USE [MillennialResort_DB]
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
	END
GO

print '' print '*** Creating sp_retrieve_all_event_sponsors'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_event_sponsors]
AS
	BEGIN	
		SELECT 	[EventSponsor].[EventID], [EventSponsor].[SponsorID]
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
		[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[Cancelled],
		[PublicEvent]
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
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
		[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[Cancelled],
		[PublicEvent]
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
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