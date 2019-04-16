USE [MillennialResort_DB]
GO

DROP PROCEDURE [dbo].[sp_select_guest_member]
GO

/*
Author: Caitlin Abelson
Created Date: 4/13/2019

This is the stored procedure for selecting all Guests and their Members
*/
print '' print '*** Creating sp_select_guest_member'
GO
CREATE PROCEDURE [dbo].[sp_select_guest_member]
AS 
	SELECT	[Guest].[GuestID], [Guest].[MemberID], [Guest].[GuestTypeID], [Guest].[FirstName], [Guest].[LastName], [Guest].[PhoneNumber], 
			[Guest].[Email], [Guest].[Minor], [Guest].[Active], [Guest].[ReceiveTexts], [Guest].[EmergencyFirstName],  
			[Guest].[EmergencyLastName], [Guest].[EmergencyPhoneNumber], [Guest].[EmergencyRelation], [Guest].[CheckedIn], 
			[Member].[FirstName], [Member].[LastName]
	FROM	[Guest] inner join [Member] on
			[Guest].[MemberID] = [Member].[MemberID]
GO			
















