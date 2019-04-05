USE [MillennialResort_DB]
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating Table LuggageStatus'
GO
CREATE TABLE [dbo].[LuggageStatus](
	[LuggageStatusID]	[nvarchar](50)	NOT NULL,
	CONSTRAINT[pk_LuggageStatusID] PRIMARY KEY([LuggageStatusID] ASC)
)
GO

/*	Author: Jacob Miller
	Created: 3/2/19
	Updated:
*/
print '' print '*** Creating sp_select_all_luggage_status'
GO
CREATE PROCEDURE [dbo].[sp_select_all_luggage_status]
AS
	BEGIN
		SELECT [LuggageStatusID]
		FROM [LuggageStatus]
	END
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating Table Luggage'
GO
CREATE TABLE [dbo].[Luggage](
	[LuggageID]			[int]IDENTITY(100000, 1)	NOT NULL,
	[GuestID]			[int]						NOT NULL,
	[LuggageStatusID]	[nvarchar](50)				NOT NULL,
	CONSTRAINT[pk_LuggageID] PRIMARY KEY ([LuggageID] ASC),
	CONSTRAINT[fk_LuggageStatusID] FOREIGN KEY ([LuggageStatusID])
		REFERENCES [dbo].[LuggageStatus]([LuggageStatusID])
		ON UPDATE CASCADE,
	CONSTRAINT[fk_LuggageGuestID] FOREIGN KEY ([GuestID])
		REFERENCES [dbo].[Guest]([GuestID])
		ON UPDATE CASCADE
)
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Inserting LuggageStatus Records'
GO
INSERT INTO [dbo].[LuggageStatus]
		([LuggageStatusID])
	VALUES
		('In Lobby'),
		('In Room'),
		('In Transit')
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating sp_insert_luggage'
GO
CREATE PROCEDURE [dbo].[sp_insert_luggage]
	(
		@GuestID			[int],
		@LuggageStatusID	[nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO [Luggage]
			(
				[GuestID], [LuggageStatusID]
			)
		VALUES
			(
				@GuestID, @LuggageStatusID
			)
	END
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating sp_select_luggage_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_luggage_by_id]
	(
		@LuggageID	[int]
	)
AS
	BEGIN
		SELECT	[LuggageID], [GuestID], [LuggageStatusID]
		FROM	[Luggage]
		WHERE	[LuggageID] = @LuggageID
	END
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating sp_select_all_luggage'
GO
CREATE PROCEDURE [dbo].[sp_select_all_luggage]
AS
	BEGIN
		SELECT	[LuggageID], [GuestID], [LuggageStatusID]
		FROM	[Luggage]
	END
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating sp_update_luggage_status'
GO
CREATE PROCEDURE [dbo].[sp_update_luggage_status]
	(
		@LuggageID			[int],
		@GuestID			[int],
		@OldLuggageStatus	[nvarchar](50),
		@NewLuggageStatus	[nvarchar](50)
	)
AS
	BEGIN
		UPDATE	[Luggage]
			SET	[LuggageStatusID] = @NewLuggageStatus
		WHERE	[GuestID] = @GuestID
			AND	[LuggageID] = @LuggageID
			AND	[LuggageStatusID] = @OldLuggageStatus
		RETURN @@ROWCOUNT
	END
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating sp_update_luggage'
GO
CREATE PROCEDURE [dbo].[sp_update_luggage]
	(
		@LuggageID			[int],
		@OldGuestID			[int],
		@NewGuestID			[int],
		@OldLuggageStatusID	[nvarchar](50),
		@NewLuggageStatusID	[nvarchar](50)
	)
AS
	BEGIN
		UPDATE	[Luggage]
			SET	[LuggageStatusID] = @NewLuggageStatusID,
				[GuestID] = @NewGuestID
		WHERE	[LuggageID] = @LuggageID
			AND		[GuestID] = @OldGuestID
			AND		[LuggageStatusID] = @OldLuggageStatusID
		RETURN @@ROWCOUNT
	END
GO

/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print '' print '*** Creating sp_delete_luggage'
GO
CREATE PROCEDURE [dbo].[sp_delete_luggage]
	(
		@LuggageID	[int]
	)
AS
	BEGIN
		DELETE FROM [Luggage]
		WHERE [LuggageID] = @LuggageID
		RETURN @@ROWCOUNT
	END
GO

/* 	Author: Jacob Miller
	Created: 4/4/19
	Updated:
*/
print '' print '*** Creating sp_retrieve_all_guests'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_guests]
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active],[ReceiveTexts], [EmergencyFirstName], 
			   [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation], [CheckedIn]
		FROM   [Guest]
	END

GO

print '' print '*** Inserting Member Records'
GO
INSERT INTO [dbo].[Member]
		([FirstName], [LastName], [PhoneNumber], [Email], [PasswordHash], [Active])
	VALUES
		('Joe', 'Blow', 13191231234, 'blowj@domain.com', '7', 1),
		('John', 'Doe', 13194443333, 'Doej@domain.com', '8', 1),
		('Tom', 'Cat', 13193214321, 'catt@domain.com', '9', 1),
		('Bill', 'Bob', 13193333333, 'bobb@domain.com', '10', 1)
GO

print '' print '*** Inserting Guest Records'
GO
INSERT INTO [dbo].[Guest]
		([MemberID],	
		[GuestTypeID], 
		[FirstName], 
		[LastName], 
		[PhoneNumber],
		[Email],	
		[Minor], 
		[Active],	
		[ReceiveTexts],	
		[EmergencyFirstName], 
		[EmergencyLastName], 
		[EmergencyPhoneNumber], 
		[EmergencyRelation],	
		[CheckedIn])
	VALUES
		(100000, 'Basic guest', 'Joe', 'Blow', 13191231234, 'blowj@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1),
		(100001, 'Basic guest', 'John', 'Doe', 13194443333, 'doej@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1),
		(100002, 'Basic guest', 'Tom', 'Cat', 13193214321, 'catt@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1),
		(100003, 'Basic guest', 'Bill', 'Bob', 13193333333, 'bobb@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1)
GO

print '' print '*** Inserting Luggage Records'
GO
INSERT INTO [dbo].[Luggage]
		([GuestID], [LuggageStatusID])
	VALUES
		(100000, 'In Lobby'),
		(100001, 'In Room'),
		(100002, 'In Room'),
		(100003, 'In Transit')
GO