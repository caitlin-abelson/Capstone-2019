USE [MillennialResort_DB]
GO

/*********************************************************************/
/*                          How to comment                           */

/* Start {Your name} */

-- Created: {Date_you_wrote_the_script}
-- Update {date of update} Author: {who_did_the_update}
-- Update {date_of_update} Desc: {what_is_the_update}
/* The first line of your code here */


/*********************************************************************/
/* Developers place their test code here to be submitted to database */
/* vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv */
/*********************************************************************/

/* Start Austin Delaney */

--ALTER TABLE [dbo].[ItemSupplier] ALTER COLUMN [PrimarySupplier] [bit] NOT NULL;
--GO

-- Review sp_count_supplier_order
-- sp_create_supplierOrder
-- sp_deactivate_employee
-- sp_deactivate_SupplierOrder needs looked at
-- Think we are dropping sp_delete_employeeID_role
-- Does maintenance type really need an active field? no
-- Does room type really need an active field? no
-- Where is appointmenttype?
-- Keeping event request?
-- Need checked in update for guest?
-- sp_insert_item review
-- Kill sp_insert_product
-- change insert_drink to insert recipe with date?
-- Who is doing room status?
-- Who did sp_insert_supplier?
-- Appointment to AppointmentType foreign key

--Ensure the following are backed up by default_data before adding statements

--ALTER TABLE [dbo].[Building] ADD DEFAULT ((?)) FOR [BuildingStatusID]
--GO
--ALTER TABLE [dbo].[Event] ADD DEFAULT ((?)) FOR [EventTypeID]
--GO
--ALTER TABLE [dbo].[Item] ADD DEFAULT ((?)) FOR [ItemTypeID]
--GO
--ALTER TABLE [dbo].[MaintenanceWorkOrder] ADD  DEFAULT ((?)) FOR [MaintenanceTypeID]
--GO
--ALTER TABLE [dbo].[MaintenanceWorkOrder] ADD  DEFAULT ((?)) FOR [MaintenanceStatusID]
--GO
--ALTER TABLE [dbo].[Offering] ADD  DEFAULT ((?)) FOR [OfferingTypeID]
--GO
--ALTER TABLE [dbo].[ResortVehicle] ADD  DEFAULT ((?)) FOR [ResortVehicleStatusID]
--GO
--ALTER TABLE [dbo].[Room] ADD  DEFAULT ((?)) FOR [RoomTypeID]
--GO
--ALTER TABLE [dbo].[Room] ADD  DEFAULT ((?)) FOR [RoomStatusID]
--GO

-- Event [Sponsored] trigger

ALTER PROCEDURE [dbo].[sp_insert_event]
	(
		@EventTitle [nvarchar](50),
		@OfferingID	[int],
		@EmployeeID	[int],
		@EventTypeID [nvarchar](15),
		@Description [nvarchar](1000),
		@EventStartDate [date],
		@EventEndDate [date],
		@KidsAllowed [bit],
		@SeatsRemaining [int],
		@NumGuests [int],
		@Location [nvarchar](50),
		@PublicEvent [bit],
		@Approved [bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
			([EventTitle]
			,[OfferingID]
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
			,[Approved])
			VALUES
			(@EventTitle
			,@OfferingID
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
			,@Approved)

			RETURN @@ROWCOUNT
	END
GO

ALTER PROCEDURE [dbo].[sp_update_event]
	(
		@EventID				[int],
		@OfferingID				[int],
		@EventTitle				[nvarchar](50),
		@EmployeeID			 	[int],
		@EventTypeID			[nvarchar](15),
		@Description			[nvarchar](1000),
		@EventStartDate			[date],
		@EventEndDate			[date],
		@KidsAllowed			[bit],
		@NumGuests				[int],
		@SeatsRemaining			[int],
		@Location				[nvarchar](50),
		@Sponsored				[bit],
		@Approved				[bit],
		@PublicEvent			[bit],

		@OldEventTitle			[nvarchar](50),
		@OldOfferingID			[int],
		@OldEmployeeID			[int],
		@OldEventTypeID			[nvarchar](15),
		@OldDescription			[nvarchar](1000),
		@OldEventStartDate		[date],
		@OldEventEndDate		[date],
		@OldKidsAllowed			[bit],
		@OldNumGuests			[int],
		@OldSeatsRemaining		[int],
		@OldLocation			[nvarchar](50),
		@OldSponsored			[bit],
		@OldApproved			[bit],
		@OldPublicEvent			[bit]

	)
AS
	BEGIN
		UPDATE [Event]
		SET		[EventTitle] = @EventTitle,
				[EmployeeID] = @EmployeeID,
				[EventTypeID] = @EventTypeID,
				[Description] = @Description,
				[EventStartDate] = @EventStartDate,
				[EventEndDate] = @EventEndDate,
				[KidsAllowed] = @KidsAllowed,
				[NumGuests] = @NumGuests,
				[Location] = @Location,
				[Sponsored] = @Sponsored,
				[Approved] = @Approved,
				[SeatsRemaining] = @SeatsRemaining,
				[OfferingID] = @OfferingID,
				[PublicEvent] = @PublicEvent
		FROM 	[dbo].[Event]
		WHERE	[EventID] = @EventID
		AND 	[EventTitle] = @OldEventTitle
		AND		[EmployeeID] = @OldEmployeeID
		AND		[EventTypeID] = @OldEventTypeID
		AND		[Description] = @OldDescription
		AND		[EventStartDate] = @OldEventStartDate
		AND		[EventEndDate] = @OldEventEndDate
		AND		[KidsAllowed] = @OldKidsAllowed
		AND		[SeatsRemaining] = @OldSeatsRemaining
		AND		[NumGuests] = @OldNumGuests
		AND		[Location] = @OldLocation
		AND 	[Sponsored] = @OldSponsored
		AND		[Approved] = @OldApproved
		AND		[PublicEvent] = @OldPublicEvent

			RETURN @@ROWCOUNT
	END
GO

ALTER PROCEDURE [dbo].[sp_retrieve_all_events]
AS
	BEGIN
		SELECT
		[EventID],
		[OfferingID],
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
		[PublicEvent]
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
	END
GO

ALTER PROCEDURE [dbo].[sp_retrieve_event]
	(
		@EventID [int]
	)
AS
	BEGIN
		SELECT  [EventID],
				[OfferingID]
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
				[PublicEvent]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
		WHERE	[EventID] = @EventID
	END
GO

/* Start Craig */

-- Following block ready for submission after gender is re-added
CREATE PROCEDURE sp_insert_pet
	(
		@PetName				    [nvarchar](50),
		@Species     				[nvarchar](50),
		@PetTypeID				    [nvarchar](25),
		@GuestID				    [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Pet]
			([PetName], [Species], [PetTypeID],[GuestID])
			VALUES
			(@PetName,  @Species, @PetTypeID, @GuestID)

			RETURN @@ROWCOUNT
	END
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Craig Barkley'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_insert_pet'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-02-17'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_insert_pet'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-03-09 Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_insert_pet'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-03-09 Desc'
	,@value = N'Removed Gender support'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_insert_pet'
GO
CREATE PROCEDURE sp_retrieve_all_pets
AS
    BEGIN
        SELECT [PetID],[PetName], [Species], [PetTypeID], [GuestID]
        FROM   [Pet]
        ORDER BY [PetID]
    END
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Craig Barkley'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_all_pets'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-02-17'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_all_pets'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-03-09 Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_all_pets'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-03-09 Desc'
	,@value = N'Removed gender support'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_retrieve_all_pets'
GO
CREATE PROCEDURE [dbo].[sp_update_pet]
	(
		@PetID			 		    [int],

		@oldPetName				    [nvarchar](50),
		@oldSpecies      			[nvarchar](50),
		@oldPetTypeID				[nvarchar](25),
		@oldGuestID				    [int],

		@newPetName				    [nvarchar](50),
		@newSpecies      			[nvarchar](50),
		@newPetTypeID				[nvarchar](25),
		@newGuestID				    [int]
	)
AS
	BEGIN
		UPDATE [Pet]
			SET [PetName] = @newPetName,
				[Species] = @newSpecies,
				[PetTypeID] = @newPetTypeID,
				[GuestID] = @newGuestID
			WHERE
				[PetID] = @PetID
			AND [PetName] = @oldPetName
			AND [Species] = @oldSpecies
			AND	[PetTypeID] = @oldPetTypeID
			AND	[GuestID] = @oldGuestID
		RETURN @@ROWCOUNT
	END
GO
EXEC sys.sp_addextendedproperty
	@name = N'Author'
	,@value = N'Craig Barkley'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_pet'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Created Date'
	,@value = N'2019-02-17'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_pet'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Updated 2019-03-09 Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_pet'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Updated 2019-03-09 Desc'
	,@value = N'Removed gender support'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_update_pet'
GO

-- Created: 2019-02-17
CREATE PROCEDURE sp_select_pet_by_id
-- Pending add per Austin D.
-- What is this used for?
AS
    BEGIN
		SELECT 		[PetID]
		FROM		[Pet]
		ORDER BY 	[PetID]
	END
GO

-- Created: 2019-02-17
CREATE PROCEDURE [dbo].[sp_delete_appointment_type_by_id]
    (
        @AppointmentTypeID    [nvarchar](15)
    )
AS
    BEGIN
        DELETE
        FROM     [AppointmentType]
        WHERE     [AppointmentTypeID] = @AppointmentTypeID

        RETURN @@ROWCOUNT
    END
GO
-- sp_select_event_type_by_id(id)
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_appointment_type_by_id.>>

print '' print ' Creating sp_appointment_type_by_id'
GO
CREATE PROCEDURE sp_appointment_type_by_id
AS
    BEGIN
		SELECT 		[AppointmentTypeID]
		FROM		[AppointmentType]
		ORDER BY 	[AppointmentTypeID]
	END
GO

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

/* End Wes Richardson */

/* Start Gunardi */

print '' print '*** Creating Sponsor Table'
GO
CREATE TABLE [dbo].[Sponsor](
	[SponsorID]			[int]IDENTITY(100000, 1)	NOT NULL,
	[Name]				[nvarchar](50)				NOT NULL,
	[Address]			[nvarchar](25)				NOT NULL,
	[City]				[nvarchar](50)				NOT NULL,
	[State]				[nvarchar](2)				NOT NULL,
	[PhoneNumber]		[nvarchar](11)				NOT NULL,
	[Email]				[nvarchar](50)				NOT NULL,
	[ContactFirstName]	[nvarchar](50)				NOT NULL,
	[ContactLastName]	[nvarchar](100)				NOT NULL,
	[StatusID]			[nvarchar](50)				NOT NULL,
	[DateAdded]			[datetime]			NOT NULL,
	[Active]			[bit]						NOT NULL 	DEFAULT 1,

CONSTRAINT [pk_SponsorID] PRIMARY KEY([SponsorID] ASC),
)
GO


print '' print'*** Inserting Sponsor test records'
GO
INSERT INTO [dbo].[Sponsor]
		([Name],[Address],[City]	,[State]		,[PhoneNumber],   [Email],[ContactFirstName], [ContactLastName],[StatusID],[DateAdded])
	VALUES
		('ABC', '123 ABC','Cedar','IA', '13195551234','joanne@abc.com', 'Joanne',  'Smith','Sponsoring Event','2019-02-01'),
			('DEF', '123 DEF','Cedar','CA', '13195551234','adam@def.com		', 'Adam',  'Junior','New','2019-02-01')
GO

print '' print '*** Creating status Table'
Create TABLE [dbo].[Status]
(
	[statusID] [nvarchar] (50) NOT NULL,
	constraint [pk_statusid] primary key ([statusID] asc)
)

print '' print '*** Inserting status Table'
GO
Insert Into [dbo].[status]([statusID])
values
	('Sponsoring Event'),
	('In Review'),('New')
GO

print '' print '*** Creating sp_retrieve_all_statusid'
GO
	Create procedure [dbo].[sp_retrieve_all_statusid]
As
Begin
	Select [statusId]
	from [status]
	order by [statusId]
End
GO

print '' print '*** Creating State Table'
GO
CREATE TABLE [dbo].[State](
[StateCode] [nvarchar](2)   NOT NULL,
[StateName] [nvarchar](128) NOT NULL,

CONSTRAINT [PK_StateCode] PRIMARY KEY ([StateCode] ASC),
)
GO

print '' print '*** Inserting State Table'
GO
INSERT INTO [dbo].[State]
([StateCode],[StateName])
VALUES
('AL', 'Alabama'),
('AK', 'Alaska'),
('AZ', 'Arizona'),
('AR', 'Arkansas'),
('CA', 'California'),
('CO', 'Colorado'),
('CT', 'Connecticut'),
('DE', 'Delaware'),
('DC', 'District of Columbia'),
('FL', 'Florida'),
('GA', 'Georgia'),
('HI', 'Hawaii'),
('ID', 'Idaho'),
('IL', 'Illinois'),
('IN', 'Indiana'),
('IA', 'Iowa'),
('KS', 'Kansas'),
('KY', 'Kentucky'),
('LA', 'Louisiana'),
('ME', 'Maine'),
('MD', 'Maryland'),
('MA', 'Massachusetts'),
('MI', 'Michigan'),
('MN', 'Minnesota'),
('MS', 'Mississippi'),
('MO', 'Missouri'),
('MT', 'Montana'),
('NE', 'Nebraska'),
('NV', 'Nevada'),
('NH', 'New Hampshire'),
('NJ', 'New Jersey'),
('NM', 'New Mexico'),
('NY', 'New York'),
('NC', 'North Carolina'),
('ND', 'North Dakota'),
('OH', 'Ohio'),
('OK', 'Oklahoma'),
('OR', 'Oregon'),
('PA', 'Pennsylvania'),
('PR', 'Puerto Rico'),
('RI', 'Rhode Island'),
('SC', 'South Carolina'),
('SD', 'South Dakota'),
('TN', 'Tennessee'),
('TX', 'Texas'),
('UT', 'Utah'),
('VT', 'Vermont'),
('VA', 'Virginia'),
('WA', 'Washington'),
('WV', 'West Virginia'),
('WI', 'Wisconsin'),
('WY', 'Wyoming')
GO

print '' print '*** Creating sp_retrieve_all_states'
GO
CREATE PROCEDURE sp_retrieve_all_states
AS
	BEGIN
		SELECT [StateCode]

		FROM   [State]
		ORDER BY [StateCode]
	END
GO

print '' print '*** Creating sp_retrieve_sponsor_by_status_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_sponsor_by_status_id]
	(
		@StatusID	[nvarchar]
	)
AS
	BEGIN
		SELECT 	[SponsorID], [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
		[ContactLastName],[StatusID],[DateAdded],[Active]
		FROM [Sponsor]
		WHERE [StatusID] = @StatusID
		RETURN @@ROWCOUNT
	END
GO


-- Author: Gunardi Saputra
-- Created: 2019/01/23
print '' print '*** Creating sp_select_all_sponsors'
GO
CREATE PROCEDURE [dbo].[sp_select_all_sponsors]
AS
	BEGIN
		SELECT 	[SponsorID], [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
		[ContactLastName],[StatusID],[DateAdded],[Active]
		FROM [Sponsor]

	END
GO


-- Author: Gunardi Saputra
-- Created: 2019/01/23
print '' print '*** Creating sp_insert_sponsor'
GO
CREATE PROCEDURE sp_insert_sponsor
	(
		@Name				[nvarchar](50),
		@Address			[nvarchar](25),
		@City				[nvarchar](50),
		@State				[nvarchar](2)	,
		@PhoneNumber		[nvarchar](11),
		@Email				[nvarchar](50),
		@ContactFirstName	[nvarchar](50),
		@ContactLastName	[nvarchar](100),
		@StatusID			[nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Sponsor]
			( [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
			[ContactLastName],[StatusID],[DateAdded])
		VALUES
			(@Name, @Address, @City, @State,
			@PhoneNumber, @Email, @ContactFirstName, @ContactLastName, @StatusID,Convert(Varchar(10), GetDate(), 101))

		RETURN @@ROWCOUNT
	END
GO

-- Author: Gunardi Saputra
-- Created Date: 2019/02/20
-- Description: This stored procedure updates a sponsor record in the sponsor table by its id.
print '' print '*** Creating sp_update_sponsor'
GO
CREATE PROCEDURE sp_update_sponsor
	(
		@SponsorID			[int],

		@OldName			[nvarchar](50),
		@OldAddress			[nvarchar](25),
		@OldCity			[nvarchar](50),
		@OldState			[nvarchar](2),
		@OldPhoneNumber		[nvarchar](11),
		@OldEmail			[nvarchar](50),
		@OldContactFirstName	[nvarchar](50),
		@OldContactLastName	[nvarchar](100),
		@OldStatusID		[nvarchar](50),
		@OldDateAdded		[datetime],
		@OldActive			[bit],


		@Name				[nvarchar](50),
		@Address			[nvarchar](25),
		@City			[nvarchar](50),
		@State			[nvarchar](2),
		@PhoneNumber		[nvarchar](11),
		@Email			[nvarchar](50),
		@ContactFirstName [nvarchar](50),
		@ContactLastName [nvarchar](100),
		@StatusID		[nvarchar](50),
		@DateAdded		[datetime],
		@Active			[bit]
	)
AS
	BEGIN
		UPDATE	[Sponsor]
		SET
			[Name]				= @Name,
			[Address]			= @Address,
			[City]				= @City,
			[State]				= @State,
			[PhoneNumber]		= @PhoneNumber,
			[Email]				= @Email,
			[ContactFirstName]	= @ContactFirstName,
			[ContactLastName]	= @ContactLastName,
			[StatusID]			= @StatusID,
			[DateAdded]			= @DateAdded,
			[Active]			= @Active
		WHERE	[SponsorID] 	= @SponsorID
			AND [Name]			= @OldName
			AND [Address]		=  @OldAddress
			AND [City]		 	=  @OldCity
			AND [State]			=  @OldState
			AND [PhoneNumber]	=  @OldPhoneNumber
			AND [Email]		 	=  @OldEmail
			AND [ContactFirstName]		=  @OldContactFirstName
			AND [ContactLastName]		=  @OldContactLastName
			AND [StatusID]		=  @OldStatusID
			AND [DateAdded]		=  @OldDateAdded
			AND [Active]		=  @OldActive

		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_activate_sponsor_by_id'
GO
CREATE PROCEDURE sp_activate_sponsor_by_id
	(
		@SponsorID		[int]
	)
AS
	BEGIN
		UPDATE 	[Sponsor]
		SET 	[Active] = 1
		WHERE	[SponsorID] = @SponsorID

		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_sponsor'
GO
CREATE PROCEDURE sp_deactivate_sponsor
	(
		@SponsorID		[int]
	)
AS
	BEGIN
		UPDATE 	[Sponsor]
		SET 	[Active] = 0
		WHERE	[SponsorID] = @SponsorID

		RETURN @@ROWCOUNT
	END
GO



print '' print '*** Creating sp_delete_sponsor'
GO

CREATE PROCEDURE [dbo].[sp_delete_sponsor]
	(
		@SponsorID 				[int]
	)
AS
	BEGIN
		DELETE
		FROM [Sponsor]
		WHERE  [SponsorID] = @SponsorID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_select_sponsor'
GO

CREATE PROCEDURE [dbo].[sp_select_sponsor]
(
	@SponsorID 				[int]
)
AS
	BEGIN
		SELECT [SponsorID], [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
			[ContactLastName],[StatusID],[DateAdded],[Active]
			FROM [Sponsor]
		WHERE [SponsorID] = @SponsorID
	END
GO


print '' print '*** Creating sp_retrieve_all_view_model_sponsors'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_view_model_sponsors]
AS
	BEGIN
		SELECT [Sponsor].[SponsorID],
		[Sponsor].[SponsorID],
		[Sponsor].[Name],
		[Sponsor].[Address],
		[Sponsor].[City],
		[Sponsor].[State],
		[Sponsor].[PhoneNumber],
		[Sponsor].[Email],
		[Sponsor].[ContactFirstName],
		[Sponsor].[ContactLastName],
		[Sponsor].[StatusID],
		[Sponsor].[DateAdded],
		[Sponsor].[Active]
		FROM Sponsor
	END
GO

/* End Gunardi */

/* Start Eduardo */

-- Name: Eduardo Colon
-- Date: 2019-03-05
print '' print '*** Creating SetupList Table'
GO
CREATE TABLE [dbo].[SetupList] (
	[SetupListID]		[int] IDENTITY(100000, 1) 			  NOT NULL,
	[SetupID]			[int]			  	        		  NOT NULL,
	[Completed] 		[bit]					 DEFAULT 0	  NOT NULL,
	[Description]		[nvarchar](1000)					  NOT NULL,
	[Comments]			[nvarchar](1000)				      NULL,

	CONSTRAINT [pk_SetupListID] PRIMARY KEY([SetupListID] ASC),
)
GO




-- Name: Eduardo Colon
-- Date: 2019-03-05
print '' print '*** Creating sp_select_setuplists'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_setuplists]
AS
	BEGIN
		SELECT 	    [SetupListID], [SetupID], [Completed], [Description],	[Comments]
		FROM		[SetupList]
	END
GO

-- NAME:  Eduardo Colon'
-- Date:   2019-03-05'
print '' print '*** Creating sp_retrieve_setuplist_by_id '
GO
CREATE PROCEDURE [dbo].[sp_retrieve_setuplist_by_id]
	(
		@SetupListID				[int]
	)
AS
	BEGIN
		SELECT [SetupListID], [SetupID],[Completed],[Description], [Comments]
		FROM [SetupList]
		WHERE [SetupListID] = @SetupListID
	END
GO

/* End Eduardo */

/* Start Eric */

-- Eric Bostwick
-- Created: 3/7/2019
-- Retrieves All SupplierOrders in the supplier order table
print '' print '*** Creating sp_select_all_supplier_orders ***'
GO
CREATE Procedure [dbo].[sp_select_all_supplier_orders]
As
    BEGIN
        SELECT [so].[SupplierOrderID], [so].[SupplierID], [s].[Name] AS SupplierName, [so].[EmployeeID], [e].[FirstName], [e].[LastName], [so].[Description],
        [so].[DateOrdered], [so].[OrderComplete]
        FROM [SupplierOrder] so INNER JOIN [Employee] e ON [so].[EmployeeID] = [e].[EmployeeID]
		INNER JOIN [Supplier] s ON [s].[SupplierID] = [so].[SupplierID]
    END
GO

-- Eric Bostwick
-- Created: 3/7/2019
-- Retrieves All SupplierOrderLines for a supplier order --
print '' print '*** Creating sp_select_all_supplier_order_lines ***'
GO
CREATE Procedure [dbo].[sp_select_all_supplier_order_lines]
	@SupplierOrderID [int]
As
    BEGIN
        SELECT [SupplierOrderID], [ItemID], [Description], [OrderQty], [UnitPrice], [QtyReceived]
        FROM [SupplierOrderLine]
		WHERE [SupplierOrderID] = @SupplierOrderID
    END
GO

-- Eric Bostwick
-- Created: 3/7/2019
-- Deletes All SupplierOrderLines for a supplier order
print '' print '*** Creating sp_delete_supplier_order_lines ***'
GO
CREATE Procedure [dbo].[sp_delete_supplier_order_lines]

	@SupplierOrderID [int]

As
    BEGIN
        DELETE FROM [SupplierOrderLine]
		WHERE [SupplierOrderID] = @SupplierOrderID
    END
GO

-- Eric Bostwick
-- Created: 3/7/2019
-- Updates Supplier Order For the SupplierOrderID--
print '' print '*** Creating sp_update_supplier_order ***'
GO
CREATE PROCEDURE [dbo].[sp_update_supplier_order]
	(
		@SupplierOrderID	[int],
		@Description		[nvarchar](1000)
	)
AS
BEGIN
		UPDATE [dbo].[SupplierOrder] SET [Description] = @Description
		WHERE [SupplierOrderID] = @SupplierOrderID

END
GO

-- Eric Bostwick
-- Created: 3/7/2019
-- Deletes A SupplierOrder
print '' print '*** Creating sp_delete_supplier_order ***'
GO
CREATE Procedure [dbo].[sp_delete_supplier_order]

	@SupplierOrderID [int]

As
    BEGIN
        DELETE FROM [SupplierOrder]
		WHERE [SupplierOrderID] = @SupplierOrderID
    END
GO

/* End Eric */

/* Start Jacob */

ALTER PROCEDURE [dbo].[sp_select_performance_by_id]
	(
		@PerformanceID	[int]
	)
AS
	BEGIN
		SELECT 	[PerformanceID], [PerformanceTitle], [PerformanceDate], [Description]
		FROM [Performance]
		WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Author',
	@value=N'Jacob Miller',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_select_performance_by_id'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Desc',
	@value=N'Added PerformanceTitle support',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_select_performance_by_id'
GO

ALTER PROCEDURE [dbo].[sp_select_all_performance]
AS
	BEGIN
		SELECT [PerformanceID], [PerformanceTitle], [PerformanceDate], [Description]
		FROM [Performance]
	END
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Author',
	@value=N'Jacob Miller',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_select_all_performance'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Desc',
	@value=N'Added PerformanceTitle support',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_select_all_performance'
GO

CREATE PROCEDURE [dbo].[sp_search_performances]
	(
		@SearchTerm		[nvarchar](100)
	)
AS
	BEGIN
		SELECT [PerformanceID], [PerformanceTitle], [PerformanceDate], [Description]
		FROM [Performance]
		WHERE [PerformanceTitle] LIKE '%' + @SearchTerm + '%'
		OR [Description] LIKE '%' + @SearchTerm + '%'
	END
GO
EXEC sys.sp_addextendedproperty
	@name=N'Author',
	@value=N'Jacob Miller',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Created Date',
	@value=N'2019-03-06',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-09 Author',
	@value=N'Austin Delaney',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-09 Desc',
	@value=N'Added searching description support',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO

ALTER PROCEDURE [dbo].[sp_insert_performance]
	(
		@PerformanceName	[nvarchar](100),
		@PerformanceDate	[date],
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Performance]
			([PerformanceTitle], [PerformanceDate], [Description])
		VALUES
			(@PerformanceName, @PerformanceDate, @Description)
	END
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Author',
	@value=N'Jacob Miller',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_insert_performance'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Desc',
	@value=N'Added PerformanceTitle support',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_insert_performance'
GO

ALTER PROCEDURE [dbo].[sp_update_performance]
	(
		@PerformanceID		[int],
		@PerformanceName	[nvarchar](100),
		@PerformanceDate	[date],
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [Performance]
			SET [PerformanceTitle] = @PerformanceName, [PerformanceDate] = @PerformanceDate, [Description] = @Description
			WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Author',
	@value=N'Jacob Miller',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_update_performance'
GO
EXEC sys.sp_addextendedproperty
	@name=N'Update 2019-03-06 Desc',
	@value=N'Added PerformanceTitle support',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_update_performance'
GO

/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
print '' print '*** Creating Guest Table'
GO
CREATE TABLE [dbo].[Guest](
	[GuestID]              [int] IDENTITY(100000,1) NOT NULL,
	[MemberID]             [int]                    NOT NULL,
	[GuestTypeID]          [nvarchar](25)           NOT NULL,
	[FirstName]            [nvarchar](50)           NOT NULL,
	[LastName]             [nvarchar](100)          NOT NULL,
	[PhoneNumber]          [nvarchar](11)           NOT NULL,
	[Email]                [nvarchar](250)          NOT NULL,
	[Minor]                [bit]                    NOT NULL Default 0,
	[Active]               [bit]                    NOT NULL Default 1,
	[ReceiveTexts]         [bit]                    NOT NULL Default 1,
	[EmergencyFirstName]   [nvarchar](50)           NOT NULL,
	[EmergencyLastName]    [nvarchar](100)          NOT NULL,
	[EmergencyPhoneNumber] [nvarchar](11)           NOT NULL,
	[EmergencyRelation]    [nvarchar](25)           NOT NULL Default 1,
    
    Constraint [pk_GuestID] Primary Key([GuestID] ASC)
)
GO


/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
print '' print '*** Creating GuestVehicle Table'
GO
Create Table [dbo].[GuestVehicle](
    [GuestID]           [int]           Not Null,
    [Make]              [nvarchar](30)  Not Null,
    [Model]             [nvarchar](30)  Not Null,
    [PlateNumber]       [nvarchar](10)  Not Null,
    [Color]             [nvarchar](30),
    [ParkingLocation]   [nvarchar](50),
    
    Constraint [pk_Make_Model_PlateNumber] Primary Key([Make] ASC, [Model] ASC, [PlateNumber] ASC)
)

/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
print '' print '*** Inserting Guest Records'
GO
Insert INTO [dbo].[Guest]
        ([MemberID], [GuestTypeID], [FirstName], [LastName], [PhoneNumber], [Email],
         [EmergencyFirstName], [EmergencyLastName], 
         [EmergencyPhoneNumber], [EmergencyRelation])
     Values
        (100000, 'Adult', 'Joanne', 'Smith', '1234567890', 'joanne@company.com',
         'Leo', 'Williams', '0987654321', 'Friend' )
 GO

/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
print '' print '*** Creating sp_retrieve_guest_names_and_ids'
GO
Create Procedure [dbo].[sp_retrieve_guest_names_and_ids]
AS
    BEGIN
        Select [FirstName], [LastName], [GuestID]
        From Guest
        Where Active = 1
    END
GO

/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
print '' print '*** Creating sp_insert_guest_vehicle'
GO
Create Procedure [dbo].[sp_insert_guest_vehicle]
(
    @GuestID           [int],
    @Make              [nvarchar](30),
    @Model             [nvarchar](30),
    @PlateNumber       [nvarchar](10),
    @Color             [nvarchar](30),
    @ParkingLocation   [nvarchar](50)
)
AS 
    BEGIN
        Insert into [GuestVehicle]
        ([GuestID], [Make], [Model], [PlateNumber], [Color], [ParkingLocation])
        Values (@GuestID, @Make, @Model, @PlateNumber, @Color, @ParkingLocation)
        Return @@Rowcount
    END
GO


/*
 * Author: Richard Carroll
 * Created: 2019/3/8
 */
print '' print '*** Creating sp_select_all_guest_vehicles'
GO
Create Procedure [dbo].[sp_select_all_guest_vehicles]
AS
    BEGIN
        Select [FirstName], [LastName], [Guest].[GuestID], [Make], [Model],
        [PlateNumber], [Color], [ParkingLocation]
        From GuestVehicle Inner Join Guest on 
        [Guest].[GuestID] = [GuestVehicle].[GuestID]
    END
GO

/*
 * Author: Richard Carroll
 * Created: 2019/3/8
 */
print '' print '*** Creating sp_update_guest_vehicle'
GO
Create Procedure [dbo].[sp_update_guest_vehicle]
(
    @OldGuestID           [int],
    @OldMake              [nvarchar](30),
    @OldModel             [nvarchar](30),
    @OldPlateNumber       [nvarchar](10),
    @OldColor             [nvarchar](30),
    @OldParkingLocation   [nvarchar](50),
    @GuestID              [int],
    @Make                 [nvarchar](30),
    @Model                [nvarchar](30),
    @PlateNumber          [nvarchar](10),
    @Color                [nvarchar](30),
    @ParkingLocation      [nvarchar](50)
)
AS 
    BEGIN
        Update GuestVehicle
        Set [GuestID] = @GuestID,
        [Make] = @Make,
        [Model] = @Model,
        [PlateNumber] = @PlateNumber,
        [Color] = @Color,
        [ParkingLocation] = @ParkingLocation
        Where [GuestID] = @OldGuestID AND
        [Make] = @OldMake AND
        [Model] = @OldModel AND
        [PlateNumber] = @OldPlateNumber AND
        [Color] = @OldColor AND
        [ParkingLocation] = @OldParkingLocation
        Return @@Rowcount
    END
GO

/* End Jacob */