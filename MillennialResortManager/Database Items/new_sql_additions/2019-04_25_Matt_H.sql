USE [MillennialResort_DB]
GO

print '' print '*** Creating sp_select_member_tab_line_by_member_tab_id'
GO

CREATE PROCEDURE [dbo].[sp_select_member_tab_line_by_member_tab_id]
	(
		@MemberTabID	[int]
	)
AS
	BEGIN
		SELECT [MemberTabLineID], [MemberTabID], [Offering].[OfferingTypeID], [Offering].[Description], [Quantity], [MemberTabLine].[Price], [MemberTabLine].[EmployeeID], [GuestID], [DatePurchased]
		FROM [dbo].[MemberTabLine] 
		INNER JOIN [Offering] ON [MemberTabLine].[OfferingID] = [Offering].[OfferingID]
		WHERE [MemberTabID] = @MemberTabID
		AND [Offering].[OfferingID] = [MemberTabLine].[OfferingID]
	END
GO
		
			
			
			
			
			
			
			
			
			
			
			