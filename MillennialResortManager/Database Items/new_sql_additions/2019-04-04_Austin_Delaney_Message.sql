CREATE TABLE [dbo].[MessageThread](
	[ThreadID] [int] identity(100,1) NOT NULL,
	[FirstMessage] [int] NULL,
	[Archived] [bit] NOT NULL DEFAULT 0

	CONSTRAINT [pk_MessageThreadID] PRIMARY KEY ([ThreadID] ASC)
)
GO

CREATE TABLE [dbo].[Message](
	[MessageID] [int] identity(1000000,1) NOT NULL,
	[SenderEmail] [nvarchar](250) NOT NULL,
	[DateTimeSent] [datetime] NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Body] [nvarchar](1000) NOT NULL,
	[ThreadID] [int] NOT NULL
	
	CONSTRAINT [pk_MessageID]  PRIMARY KEY ([MessageID] ASC)	
)
GO

CREATE PROCEDURE [dbo].[sp_get_thread_messages]
(
	@ThreadID [int]
)
AS
	BEGIN
		
		SELECT [MessageID],[SenderEmail],[DateTimeSent],[Subject],[Body]
		FROM [dbo].[Message]
		WHERE [ThreadID] = @ThreadID
	
	END
GO

CREATE PROCEDURE [dbo].[sp_new_message]
(
	@ThreadID [int] OUTPUT,
	@SenderEmail [nvarchar](250),
	@DateTimeSent [datetime],
	@Subject [nvarchar](100),
	@Body [nvarchar](1000)
)
AS
	BEGIN
		
		INSERT INTO [MessageThread]
		([Archived])
		VALUES
		('0')

		SET @ThreadID = @@IDENTITY
		
		INSERT INTO [Message]
		([SenderEmail], [DateTimeSent], [Subject], [Body], [ThreadID])
		VALUES
		(@SenderEmail, @DateTimeSent, @Subject, @Body, @ThreadID)
		
		DECLARE @MessageID [int] = @@IDENTITY

		UPDATE [MessageThread]
		SET [FirstMessage] = @MessageID
		WHERE [ThreadID] = @ThreadID

		RETURN @ThreadID

	END
GO

CREATE PROCEDURE [dbo].[sp_select_newest_thread_message]
(
	@ThreadID [int]
)
AS
	BEGIN
		SELECT [MessageID],[SenderEmail],[DateTimeSent],[Subject],[Body]
		FROM [dbo].[Message]
		WHERE [ThreadID] = @ThreadID
		AND [DateTimeSent] = (SELECT MAX(DateTimeSent) FROM [dbo].[Message] WHERE [ThreadID] = @ThreadID)
	END
GO

CREATE TABLE [dbo].[ThreadParticipant](
	[ThreadID] [int] NOT NULL,
	[ParticipantEmail] [nvarchar](250) NOT NULL,
	[ParticipantAlias] [nvarchar](250) NOT NULL,
	[SilenceNewMessages] [bit] NOT NULL DEFAULT 0,
	[HasUnreadMessages] [bit] NOT NULL DEFAULT 0,
	[HideThread] [bit] NOT NULL DEFAULT 0

	CONSTRAINT [pk_threadID_participantEmail] PRIMARY KEY ([ThreadID],[ParticipantEmail])
)
GO

CREATE PROCEDURE [dbo].[sp_new_reply]
(
	@ThreadID [int],
	@SenderEmail [nvarchar](250),
	@DateTimeSent [datetime],
	@Subject [nvarchar](100),
	@Body [nvarchar](1000)
)
AS
	BEGIN
		
		INSERT INTO [Message]
		([ThreadID], [SenderEmail], [DateTimeSent], [Subject], [Body])

		VALUES
		(@ThreadID, @SenderEmail, @DateTimeSent, @Subject, @Body)

		DECLARE @Output [int] = @@ROWCOUNT

		UPDATE [ThreadParticipant]
		SET [HasUnreadMessages] = '1'
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] != @SenderEmail
		AND [SilenceNewMessages] != '1'

		RETURN @Output
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_thread_archived_state]
(
	@ThreadID [int],
	@Archived [bit]
)
AS
	BEGIN
		
		UPDATE [MessageThread]
		SET [Archived] = @Archived
		WHERE [ThreadID] = @ThreadID

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_participant_thread_hidden_state]
(
	@ThreadID [int],
	@Hidden [bit],
	@ParticipantEmail [nvarchar](250)
)  
AS
	BEGIN
		
		UPDATE [ThreadParticipant]
		SET [HideThread] = @Hidden
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @ParticipantEmail

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_participant_thread_alert_user_new_messages]
(
	@ThreadID [int],
	@AlertStatus [bit],
	@ParticipantEmail [nvarchar](250)
)
AS
	BEGIN
	
		UPDATE [ThreadParticipant]
		SET [SilenceNewMessages] = @AlertStatus
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @ParticipantEmail

		RETURN @@ROWCOUNT
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_thread_data_by_participant_email]
(
	@Email [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
	
		SELECT [ParticipantAlias],[SilenceNewMessages],[HasUnreadMessages],[HideThread]
		FROM [ThreadParticipant]
		WHERE [ParticipantEmail] = @Email
		AND [ThreadID] = @ThreadID
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_small_threads_by_participant_email_and_archived]
(
	@Email [nvarchar](250),
	@Archived [bit]
)
AS
	BEGIN
	
		SELECT [HasUnreadMessages],[HideThread],[Archived],[ParticipantAlias],[Message].[SenderEmail],[Message].[Subject],[Message].[DateTimeSent]
		FROM [ThreadParticipant] INNER JOIN [MessageThread]
			ON [ThreadParticipant].[ThreadID] = [MessageThread].[ThreadID]
			INNER JOIN [Message]
			ON [MessageThread].[FirstMessage] = [Message].[MessageID]
		WHERE [ParticipantEmail] = @Email
		AND [Archived] = @Archived
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_thread_participant_has_new_messages]
(
	@Email [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		SELECT [HasUnreadMessages]
		FROM [ThreadParticipant]
		WHERE [ParticipantEmail] = @Email
		AND [ThreadID] = @ThreadID
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_thread_participant_alias]
(
	@Email [nvarchar](250),
	@ThreadID [int],
	@NewAlias [nvarchar](250)
)
AS
	BEGIN
	
		UPDATE [ThreadParticipant]
		SET [ParticipantAlias] = @NewAlias
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @Email

		RETURN @@ROWCOUNT
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_thread_participants]
(
	@ThreadID [int]
)
AS
	BEGIN
	
		SELECT [ParticipantEmail], [ParticipantAlias]
		FROM [ThreadParticipant]
		WHERE [ThreadID] = @ThreadID
	
	END
GO

CREATE PROCEDURE [dbo].[sp_delete_participant_from_thread]
(
	@Email [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN

		DELETE FROM [ThreadParticipant]
		WHERE [ParticipantEmail] = @Email
		AND [ThreadID] = @ThreadID

		RETURN @@ROWCOUNT		
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_thread_new_messages_status_for_participant]
(
	@ThreadID [int],
	@Email [nvarchar](250)
)
AS
	BEGIN
		
		UPDATE [ThreadParticipant]
		SET [HasUnreadMessages] = '0'
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @Email
		
		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_employee_to_thread]
(
	@EmployeeEmail [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		VALUES
		(@ThreadID,@EmployeeEmail,@EmployeeEmail)

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_member_to_thread]
(
	@MemberEmail [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		VALUES
		(@ThreadID,@MemberEmail,@MemberEmail)

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_guest_to_thread]
(
	@GuestEmail [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		VALUES
		(@ThreadID,@GuestEmail,@GuestEmail)

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_employee_participants_by_role]
(
	@ThreadID [int],
	@RoleID [nvarchar](250)
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		SELECT @ThreadID,[Employee].[Email],@RoleID
		FROM [Employee] INNER JOIN [EmployeeRole]
			ON [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
		WHERE [RoleID]= @RoleID
		
		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_employee_participants_by_department]
(
	@ThreadID [int],
	@DepartmentID [nvarchar](50)
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		SELECT @ThreadID,[Email],@DepartmentID
		FROM [Employee]
		WHERE [DepartmentID] = @DepartmentID
		
		RETURN @@ROWCOUNT
		
	END
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'MessageThread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'MessageThread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'Represents a thread to which users can attach messages to for the purpose of being viewed by other users.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'MessageThread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'Auto generated ID of the thread.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'MessageThread'
	,@level2type = N'Column', @level2name = 'ThreadID'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'A reference to the first message that is used to contain the thread. Other header information is contained in this first message.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'MessageThread'
	,@level2type = N'Column', @level2name = 'FirstMessage'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'If the thread is marked as archived or not, typically by age or inactivity.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'MessageThread'
	,@level2type = N'Column', @level2name = 'Archived'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'A text based message that is sent/attached to a message thread to communicate with other users.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'Auto generated ID field of the message.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
	,@level2type = N'Column', @level2name = 'MessageID'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The email of the user who sent the message.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
	,@level2type = N'Column', @level2name = 'SenderEmail'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The date and time that the message was sent.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
	,@level2type = N'Column', @level2name = 'DateTimeSent'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The subject line of the message.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
	,@level2type = N'Column', @level2name = 'Subject'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The primary text body of the message.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
	,@level2type = N'Column', @level2name = 'Body'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The ID of the thread that the message is attached to.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'Message'
	,@level2type = N'Column', @level2name = 'ThreadID'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_get_thread_messages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_get_thread_messages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_new_message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_new_message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_new_reply'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_new_reply'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_select_newest_thread_message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_select_newest_thread_message'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'A user who can view/attach messages within a thread.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The ID of the thread that the user is allowed to modify'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
	,@level2type = N'Column', @level2name = 'ThreadID'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The email of the participant.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
	,@level2type = N'Column', @level2name = 'ParticipantEmail'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'The name or identity to which the participant will be externally viewed as.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
	,@level2type = N'Column', @level2name = 'ParticipantAlias'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'If the participant recieves alerts for when new messages are posted to this thread.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
	,@level2type = N'Column', @level2name = 'SilenceNewMessages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'If the thread contains messages which have not been viewed by this participant.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
	,@level2type = N'Column', @level2name = 'HasUnreadMessages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Description'
	,@value = N'If the thread should be "hidden" from this participants basic view.'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Table', @level1name = 'ThreadParticipant'
	,@level2type = N'Column', @level2name = 'HideThread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_thread_Archived_state'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_thread_Archived_state'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_participant_thread_hidden_state'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_participant_thread_hidden_state'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_participant_thread_alert_user_new_messages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_participant_thread_alert_user_new_messages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_thread_data_by_participant_email'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_thread_data_by_participant_email'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_small_threads_by_participant_email_and_archived'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_small_threads_by_participant_email_and_archived'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_thread_participant_has_new_messages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_thread_participant_has_new_messages'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_thread_participant_alias'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_thread_participant_alias'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_all_thread_participants'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_all_thread_participants'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_delete_participant_from_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_delete_participant_from_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_thread_new_messages_status_for_participant'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_thread_new_messages_status_for_participant'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_employee_to_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_employee_to_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_member_to_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_member_to_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_guest_to_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_guest_to_thread'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_employee_participants_by_role'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_employee_participants_by_role'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_employee_participants_by_department'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-04-24'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_add_employee_participants_by_department'
GO
