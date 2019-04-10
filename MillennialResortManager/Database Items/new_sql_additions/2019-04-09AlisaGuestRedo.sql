print '' print '*** Creating sp_retrieve_all_guests'
GO
CREATE PROCEDURE sp_retrieve_all_guests
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active], [CheckedIn]
		FROM   [Guest]
		ORDER BY [GuestID], [Active]
	END
GO


print '' print '*** Creating sp_check_out_guest_by_id'
GO
CREATE PROCEDURE sp_check_out_guest_by_id
	(
		@GuestID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Guest]
		SET 	[CheckedIn] = 0
		WHERE	[GuestID] = @GuestID
		  
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_check_in_guest_by_id'
GO
CREATE PROCEDURE sp_check_in_guest_by_id
	(
		@GuestID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Guest]
		SET 	[CheckedIn] = 1
		WHERE	[GuestID] = @GuestID
		  
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_retrieve_all_guest_types'
GO
CREATE PROCEDURE sp_retrieve_all_guest_types
AS
	BEGIN
		SELECT 		[GuestTypeID]
		FROM		[GuestType]
		ORDER BY 	[GuestTypeID]
	END
GO