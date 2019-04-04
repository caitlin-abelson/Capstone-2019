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
