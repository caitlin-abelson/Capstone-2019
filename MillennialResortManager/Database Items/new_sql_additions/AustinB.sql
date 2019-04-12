USE [MillennialResort_DB]
GO
print '' print '*** Creating sp_select_member_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_member_by_email]
(
	@Email			[nvarchar](50)
)
AS
	BEGIN
		SELECT 	[MemberID]  
		FROM	[Member]
		WHERE	[Email] = @Email
	END
GO