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

/* Start Craig */

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

-- sp_select_event_type_by_id(id)
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_appointment_type_by_id.>>
-- Pending add by Jared Greenfield : rename to match convention and correct in c#
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
					[ContactLastName],[DateAdded],[Active]
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
		[ContactLastName],[DateAdded],[Active]
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
		@ContactLastName	[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Sponsor]
			( [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
			[ContactLastName],[DateAdded])
		VALUES
			(@Name, @Address, @City, @State,
			@PhoneNumber, @Email, @ContactFirstName, @ContactLastName, Convert(Varchar(10), GetDate(), 101))

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
/* Start Eric */




-- Eric Bostwick
-- Created: 3/7/2019
-- Updates Supplier Order For the SupplierOrderID
-- More complete version in master?
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
/* End Eric */

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







