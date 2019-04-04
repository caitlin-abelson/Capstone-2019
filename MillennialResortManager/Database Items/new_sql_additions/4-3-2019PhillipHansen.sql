USE [MillennialResort_DB]
GO

print '' print '*** Altering Event table***'
GO
print '' print 'Adding Price column'
GO
ALTER TABLE [dbo].[Event]
	ADD		Price	[money]
GO
print '' print 'Dropping OfferingID Column'
GO
ALTER TABLE [dbo].[Event]
	DROP CONSTRAINT fk_OfferingID
GO
ALTER TABLE [dbo].[Event]
	DROP COLUMN OfferingID
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
		@Approved 		[bit],
		@Price			[money]
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
			([EventTitle]
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
			,[Approved]
			,[Price])
			VALUES
			(@EventTitle
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
			,@Approved
			,@Price)

			RETURN @@ROWCOUNT
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
				[EventTitle],
				[Event].[EmployeeID],
				[Employee].[FirstName],
				[EventTypeID],
				[Description],
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
		WHERE	[EventID] = @EventID
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
		[PublicEvent],
		[Price]
		
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
		[PublicEvent],
		[Price]
		
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