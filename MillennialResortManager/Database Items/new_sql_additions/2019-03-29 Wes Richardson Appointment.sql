/*Start Wes Richardson 2019-03-01*/

/* Wes Richardson
 * Created: 2019-03-01
 *
 * Add Appointment Table
 */
print '' print '*** Creating Appointment Type Table'
GO
CREATE TABLE [dbo].[AppointmentType] (
	[AppointmentTypeID]	[nvarchar](15)		NOT NULL,
	[Description]		[nvarchar](1000)	NULL,

	CONSTRAINT [pk_AppointmentTypeID] PRIMARY KEY([AppointmentTypeID] ASC)
)

/* Wes Richardson
 * Created: 2019-03-01
 *
 * Add Appointment Table
 */
print '' print '*** Creating Appointment Table'
GO
CREATE TABLE [dbo].[Appointment] (
	[AppointmentID]		[int] IDENTITY(100000, 1)	NOT NULL,
	[AppointmentTypeID]	[nvarchar](15)				NOT NULL,
	[GuestID]			[int]						NOT NULL,
	[StartDate]			[DateTime]					NOT NULL,
	[EndDate]			[DateTime]					NOT NULL,
	[Description]		[nvarchar](1000)			NULL

	CONSTRAINT [pk_AppointmentID] PRIMARY KEY([AppointmentID] ASC),
)
GO
ALTER TABLE [dbo].[Appointment]  WITH NOCHECK ADD  CONSTRAINT [fk_AppointmentTypeID] FOREIGN KEY([AppointmentTypeID])
REFERENCES [dbo].[AppointmentType] ([AppointmentTypeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Appointment]  WITH NOCHECK ADD  CONSTRAINT [fk_GuestID] FOREIGN KEY([GuestID])
REFERENCES [dbo].[Guest] ([GuestID])
ON UPDATE CASCADE
GO
/* Wes Richardson
 * Created: 2019-03-01
 *
 * Insert new appointment
 */
print '' print '*** Creating sp_insert_appointment'
GO
CREATE PROCEDURE [dbo].[sp_insert_appointment]
	(
	@AppointmentTypeID	[nvarchar](15),
	@GuestID			[int],
	@StartDate			[DateTime],
	@EndDate			[DateTime],
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Appointment]
			([AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description])
		VALUES
			(@AppointmentTypeID, @GuestID, @StartDate, @EndDate, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Wes Richardson
 * Created: 2019-03-01
 *
 * Update an appointment
 */
print '' print '*** Creating sp_update_appointment'
GO
CREATE PROCEDURE [dbo].[sp_update_appointment]
	(
	@AppointmentID		[int],
	@AppointmentTypeID	[nvarchar](15),
	@GuestID			[int],
	@StartDate			[DateTime],
	@EndDate			[DateTime],
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE	[Appointment]
			SET	[AppointmentTypeID] = @AppointmentTypeID,
					[GuestID] = @GuestID,
					[StartDate] = @StartDate,
					[EndDate] = @EndDate,
					[Description] = @Description
			WHERE	[AppointmentID] = @AppointmentID
		RETURN @@ROWCOUNT
	END
GO

/* Wes Richardson
 * Created: 2019-03-01
 *
 * Select an appointment by appointment id
 */

print '' print '*** Creating sp_select_appointment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_appointment_by_id]
	(
		@AppointmentID		[int]
	)
AS
	BEGIN
		SELECT	[AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description]
		FROM	[Appointment]
		WHERE 	[AppointmentID] = @AppointmentID
	END
GO

/* Wes Richardson
 * Created: 2019-03-01
 *
 * Select appointment type list
 */

print '' print '*** Creating sp_select_appointment_types'
GO
CREATE PROCEDURE [dbo].[sp_select_appointment_types]
AS
	BEGIN
		SELECT [AppointmentTypeID], [Description]
		FROM AppointmentType
	END
GO

/* Wes Richardson
 * Created: 2019-03-01
 *
 * Select a list of guests for a appointment guest view model
 */

print '' print '*** Creating sp_select_appointment_guest_view_list'
GO
CREATE PROCEDURE [dbo].[sp_select_appointment_guest_view_list]
AS
	BEGIN
		SELECT [GuestID], [FirstName], [LastName], [Email]
		FROM [Guest]
	END
GO

/* Wes Richardson
 * Created: 2019-03-28
 *
 * Select Appointments by GuestID
 */
 print '' print '*** Creating sp_select_appointment_by_guest_id'
 GO
CREATE PROCEDURE [dbo].[sp_select_appointment_by_guest_id]
	(
		@GuestID		[int]
	)
AS
	BEGIN
		SELECT	[AppointmentID], [AppointmentTypeID], [StartDate], [EndDate], [Description]
		FROM	[Appointment]
		WHERE 	[GuestID] = @GuestID
	END
GO
/* Wes Richardson
 * Created: 2019-03-28
 *
 * Delete Appointment
 */
 print '' print '*** Creating sp_delete_appointment_by_id'
 GO
CREATE PROCEDURE [dbo].[sp_delete_appointment_by_id]
	(
		@AppointmentID		[int]
	)

AS
	BEGIN
		DELETE
		FROM [Appointment]
		WHERE [AppointmentID] = @AppointmentID
		RETURN @@ROWCOUNT
	END
GO

/* End Wes Richardson */