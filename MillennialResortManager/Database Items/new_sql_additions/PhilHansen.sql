USE [MillennialResort_DB]
GO

print '' print '***Creating sp_insert_event_performance'
GO
CREATE PROCEDURE [dbo].[sp_insert_event_performance]
	(
		@EventID		[int],
		@PerformanceID	[int]
	)
AS
	BEGIN
		INSERT INTO [EventPerformance]
			([EventID], [PerformanceID])
		VALUES
			(@EventID, @PerformanceID)
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '***Creating sp_retrieve_all_event_performances'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_event_performances]
AS
	BEGIN
		SELECT [EventPerformance].[EventID]
					, [Event].[EventTitle]
					,[Performance].[PerformanceTitle]
					, [EventPerformance].[PerformanceID]
		FROM	[Event] INNER JOIN [EventPerformance]
					ON [Event].[EventID] = [EventPerformance].[EventID]
				INNER JOIN [Performance] 
				ON [EventPerformance].[PerformanceID] = [Performance].[PerformanceID]
		WHERE 	[Event].[EventID] = [EventPerformance].[EventID]
		AND		[Performance].[PerformanceID] = [EventPerformance].[PerformanceID]
	END
GO
