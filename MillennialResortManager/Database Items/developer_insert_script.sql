USE [MillennialResort_DB]
GO

/*********************************************************************/
/*                          How to comment                           */

/* Start {Your name} */

-- Created: {Date you wrote the script}
-- Update {date of update} Author: {who did the update}
-- Update {date of update} Desc: {what is the update}
/* The first line of your code here */


/*********************************************************************/
/* Developers place their test code here to be submitted to database */
/* vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv */
/*********************************************************************/
/* Start Jacob Miller */
-- Created: 2019-3-06
print '' print '*** Creating Performance Table'
GO
CREATE TABLE [dbo].[Performance](
	[PerformanceID]		[int]IDENTITY(100000, 1)	NOT NULL,
	[PerformanceName]	[nvarchar](100)				NOT NULL,
	[PerformanceDate]	[date]						NOT NULL,
	[Description]		[nvarchar](1000),
	CONSTRAINT [pk_PerformanceID] PRIMARY KEY([PerformanceID])
)
GO
-- Created: 2019-3-06
print '' print '*** Inserting Performance Test Records'
GO
INSERT INTO [dbo].[Performance]
	([PerformanceName], [PerformanceDate], [Description])
	VALUES
		('Juggler', '2018-6-27', 'It is a juggler, not much else to say'),
		('Firebreather', '2018-5-15', 'This one is for Matt LaMarche')
GO
-- Created: 2019-3-06
print '' print '*** Creating sp_select_performance_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_performance_by_id]
	(
		@PerformanceID	[int]
	)
AS
	BEGIN
		SELECT 	[PerformanceID], [PerformanceName], [PerformanceDate], [Description]
		FROM [Performance]
		WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END
GO
-- Created: 2019-3-06
print '' print '*** Creating sp_select_all_performance'
GO
CREATE PROCEDURE [dbo].[sp_select_all_performance]
AS
	BEGIN
		SELECT [PerformanceID], [PerformanceName], [PerformanceDate], [Description]
		FROM [Performance]
	END
GO
-- Created: 2019-3-06
print '' print '*** Creating sp_search_performances'
GO
CREATE PROCEDURE [dbo].[sp_search_performances]
	(
		@SearchTerm		[nvarchar](100)
	)
AS
	BEGIN
		SELECT [PerformanceID], [PerformanceName], [PerformanceDate], [Description]
		FROM [Performance]
		WHERE [PerformanceName] LIKE '%' + @SearchTerm + '%'
	END
GO
-- Created: 2019-3-06
print '' print '*** Creating sp_insert_performance'
GO
CREATE PROCEDURE [dbo].[sp_insert_performance]
	(
		@PerformanceName	[nvarchar](100),
		@PerformanceDate	[date],
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Performance]
			([PerformanceName], [PerformanceDate], [Description])
		VALUES
			(@PerformanceName, @PerformanceDate, @Description)
	END
GO
-- Created: 2019-3-06
print '' print '*** Creating sp_update_performance'
GO
CREATE PROCEDURE [dbo].[sp_update_performance]
	(
		@PerformanceID		[int],
		@PerformanceName	[nvarchar](100),
		@PerformanceDate	[date],
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [Performance]
			SET [PerformanceName] = @PerformanceName, [PerformanceDate] = @PerformanceDate, [Description] = @Description
			WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END
GO
-- Created: 2019-3-06
print '' print '*** Creating sp_delete_performance'
GO
CREATE PROCEDURE [dbo].[sp_delete_performance]
	(
		@PerformanceID	[int]
	)
AS
	BEGIN
		DELETE
		FROM [Performance]
		WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END


/* Start Eric Bostwick */

-- Created: 2019-02-04
/*CREATE PROCEDURE [dbo].[sp_create_itemsupplier]
--Pending add per Austin D.
--The idea makes sense, but I don't think this should be treated as a transaction.
--I would like to break this up into 2 seperate actions if possible, allowing the failure
--of one part but not the other.
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money]
	)
AS
BEGIN
	BEGIN TRY
		-- We can only have one primary supplier for each itemid
		-- so if we are setting the primary supplier to this supplier
		-- we need to set the primary supplier to false for each itemsupplier record for
		-- this item before we set it to true for this one.
		-- This seems like a good place for a transaction.

		BEGIN TRANSACTION
			    DECLARE @ItemSupplierCount int
				SET @ItemSupplierCount = (SELECT COUNT(*) FROM ItemSupplier WHERE ItemID = @ItemID )
				IF (@PrimarySupplier = 1  AND @ItemSupplierCount > 0)
					BEGIN
						UPDATE [dbo].[ItemSupplier]
						SET [PrimarySupplier] = 0
					END

				IF (@ItemSupplierCount = 0)  --IF the record(s) was updated then insert the the itemsupplier OR the supplier count is 0 for this item
				BEGIN
					SET @PrimarySupplier = 1
				END
				BEGIN
					INSERT INTO [dbo].[ItemSupplier]
					([ItemID], [SupplierID], [PrimarySupplier], [LeadTimeDays], [UnitPrice])
					VALUES
					(@ItemID, @SupplierID, @PrimarySupplier, @LeadTimeDays, @UnitPrice)

					COMMIT
				END
	END TRY
	BEGIN CATCH
			ROLLBACK  --If anything went wrong rollback the transaction
	END CATCH

END
--GO*/

-- Created: 2019-02-04
/*CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_itemid]
--Pending add per Austin D.
--Why is this such a big return? There is no need to talk to all these tables
--Description says only "Selects ItemSupplers by ItemID"
(
	@ItemID [int]
)

AS
	BEGIN
		SELECT
		[ItemSupplier].[ItemID],
		[ItemSupplier].[SupplierID],
		[ItemSupplier].[PrimarySupplier],
		[ItemSupplier].[LeadTimeDays],
		[ItemSupplier].[UnitPrice],
		[ItemSupplier].[Active] as [ItemSupplierActive],
		[Supplier].[Name],
		[Supplier].[ContactFirstName],
		[Supplier].[ContactLastName],
		[Supplier].[PhoneNumber],
		[Supplier].[Email],
		[Supplier].[DateAdded],
		[Supplier].[Address],
		[Supplier].[City],
		[Supplier].[State],
		[Supplier].[Country],
		[Supplier].[PostalCode],
		[Supplier].[Description],
		[Supplier].[Active] AS [SupplierActive],
		[Item].[ItemTypeID],
		[Item].[Description] AS [ItemDescripton],
		[Item].[OnHandQty],
		[Item].[Name],
		[Item].[DateActive],
		[Item].[Active] AS [SupplierActive]
		FROM		[ItemSupplier]
					JOIN [Item] ON [Item].[ItemID] = [ItemSupplier].[ItemID]
					JOIN [Supplier] ON [Supplier].[SupplierID] = [ItemSupplier].[SupplierID]
		WHERE		[ItemSupplier].[itemID] = @ItemID
	END
--GO */

-- Created: 2019-02-04
/*CREATE PROCEDURE [dbo].[sp_select_itemsupplier_by_itemid_and_supplierid]
--Pending add per Austin D.
--Again, why is this so big?
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		SELECT
		[ItemSupplier].[ItemID],
		[ItemSupplier].[SupplierID],
		[ItemSupplier].PrimarySupplier,
		[ItemSupplier].[LeadTimeDays],
		[ItemSupplier].[UnitPrice],
		[ItemSupplier].[Active] as [ItemSupplierActive],
		[Supplier].[Name],
		[Supplier].[ContactFirstName],
		[Supplier].[ContactLastName],
		[Supplier].[PhoneNumber],
		[Supplier].[Email],
		[Supplier].[DateAdded],
		[Supplier].[Address],
		[Supplier].[City],
		[Supplier].[State],
		[Supplier].[Country],
		[Supplier].[PostalCode],
		[Supplier].[Description],
		[Supplier].[Active] AS SupplierActive,
		[Item].[ItemTypeID],
		[Item].[Description] AS [ItemDescripton],
		[Item].[OnHandQty],
		[Item].[Name],
		[Item].[DateActive],
		[Item].[Active] AS [ItemActive]
		FROM		[ItemSupplier]
					JOIN [Item] ON [Item].[ItemID] = [ItemSupplier].[ItemID]
					JOIN [Supplier] ON [Supplier].[SupplierID] = [ItemSupplier].[SupplierID]
		WHERE		[ItemSupplier].[ItemID] = @ItemID AND [ItemSupplier].[SupplierID] = @SupplierID
	END
--GO */

/* Start Austin */

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

--Ensure the following are backed up by default_data before adding statements

--ALTER TABLE [dbo].[Building] ADD  DEFAULT ((?)) FOR [BuildingStatusID]
--GO
--ALTER TABLE [dbo].[Event] ADD  DEFAULT ((?)) FOR [EventTypeID]
--GO
--ALTER TABLE [dbo].[Item] ADD  DEFAULT ((?)) FOR [ItemTypeID]
--GO
--ALTER TABLE [dbo].[MaintenanceWorkOrder] ADD  DEFAULT ((?)) FOR [MaintenanceTypeID]
--GO
--ALTER TABLE [dbo].[MaintenanceWorkOrder] ADD  DEFAULT ((?)) FOR [MaintenanceStatusID]
--GO
--ALTER TABLE [dbo].[Offering] ADD  DEFAULT ((?)) FOR [OfferingTypeID]
--GO
--ALTER TABLE [dbo].[Pet] ADD  DEFAULT ((?)) FOR [PetTypeID]
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



DROP TABLE [dbo].[Member]
go
CREATE TABLE [dbo].[Member](
	[MemberID]			[int] IDENTITY(100000, 1) 	  NOT NULL,
	[FirstName]			[nvarchar](50)				NOT NULL,
	[LastName]			[nvarchar](100)				NOT NULL,
	[PhoneNumber]		[nvarchar](11)				NOT NULL,
	[Email]				[nvarchar](250)				NOT NULL,
	[Password]			[nvarchar](100)				NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]			[bit]						NOT NULL DEFAULT 1
	
	CONSTRAINT [pk_MemberID] PRIMARY KEY([MemberID] ASC),
	CONSTRAINT [Email] UNIQUE([Email] ASC)
)
GO
print '' print '*** Inserting Event Type Records'
GO

--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, Appointment Type Table.>>

print '' print '*** Creating Appointment Type Table'
GO
CREATE TABLE [dbo].[AppointmentType] 
(
	[AppointmentTypeID]	[nvarchar](15)						NOT NULL,
	[Description]		[nvarchar](250)						NOT NULL
	
	CONSTRAINT [pk_AppointmentTypeID]	PRIMARY KEY([AppointmentTypeID] ASC)
)
GO
print '' print '*** Inserting Appointment Type Records'
GO

--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, creating Pet Table.>>

-------
-- this table was updated with the gender field.
----------look at this------------------------------------

print '' print '*** Creating Pet Table'
GO
CREATE TABLE [dbo].[Pet] (
	[PetID]	            [int] IDENTITY(100000, 1) 	  NOT NULL,
	[PetName]		    [nvarchar](50)			  	  NOT NULL,
	[Gender] 	    	[nvarchar](50)				  NOT NULL,
	[Species]		    [nvarchar](50)				  NOT NULL,
	[PetTypeID]			[nvarchar](50)			      NOT NULL,
	[GuestID]		    [int]				          NOT NULL

	CONSTRAINT [pk_PetID] PRIMARY KEY([PetID] ASC),
	/*INSERT GUEST ID FOREIGN KEY HERE*/
)
GO
print '' print '*** Inserting Pet Test Records'
GO




--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, Pet types Table.>>

--this foreign key contraint keeps active types that are being used from being deleted until there are no types. this could be added to all type look up tables.

-- ALTER TABLE [dbo].[Pet] WITH NOCHECK
-- 	ADD CONSTRAINT [fk_Pet_PetTypeID] FOREIGN KEY ([PetTypeID])
-- 	REFERENCES [dbo].[PetType]([PetTypeID])
-- 	ON UPDATE CASCADE
-- GO

--Stored Procedures.
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, stored procedure Insert Pet.>>

print '' print '*** Creating sp_insert_pet'
GO
CREATE PROCEDURE sp_insert_pet
	(
		@PetName				    [nvarchar](50),
		@Gender      				[nvarchar](50),
		@Species     				[nvarchar](50),
		@PetTypeID				    [nvarchar](25),
		@GuestID				    [int]		
	)
AS
	BEGIN
		INSERT INTO [dbo].[Pet]
			([PetName],[Gender], [Species], [PetTypeID],[GuestID])
			VALUES
			(@PetName, @Gender, @Species, @PetTypeID, @GuestID)
			
			RETURN @@ROWCOUNT
	END
GO
-- sp_retrieve_all_pets()
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, Creating sp_retrieve_all_pets.>>

print '' print '*** Creating sp_retrieve_all_pets'
GO
CREATE PROCEDURE sp_retrieve_all_pets
AS
    BEGIN
        SELECT [PetID],[PetName], [Gender], [Species], [PetTypeID], [GuestID]
        FROM   [Pet]
        ORDER BY [PetID]
    END
GO
-- sp_select_pet_by_id(id)
--  Author: <<Craig Barkley>>,Created:<<2/10/19>>, Updated<<2/17/2019>>, What/Why<<Adding comments, Creating sp_select_pet_by_id.>>

print '' print '*** Creating sp_select_pet_by_id'
GO
CREATE PROCEDURE sp_select_pet_by_id
AS
    BEGIN        
		SELECT 		[PetID]
		FROM		[Pet]
		ORDER BY 	[PetID]
	END
GO
--sp_update_pet
--  Author: <<Craig Barkley>>,Created:<<2/10/19>>, Updated<<2/17/2019>>, What/Why<<Adding comments, Creating sp_update_pet.>>

print '' print '*** Creating sp_update_pet'
GO
CREATE PROCEDURE [dbo].[sp_update_pet]
	(
		@PetID			 		    [int],				

		@oldPetName				    [nvarchar](50),
		@oldGender      			[nvarchar](50),
		@oldSpecies      			[nvarchar](50),
		@oldPetTypeID				[nvarchar](25),
		@oldGuestID				    [int],

		@newPetName				    [nvarchar](50),
		@newGender      			[nvarchar](50),
		@newSpecies      			[nvarchar](50),
		@newPetTypeID				[nvarchar](25),
		@newGuestID				    [int]
	)
AS
	BEGIN
		UPDATE [Pet]
			SET [PetName] = @newPetName,
				[Gender] = @newGender,
				[Species] = @newSpecies,
				[PetTypeID] = @newPetTypeID,
				[GuestID] = @newGuestID				
			WHERE 	
				[PetID] = @PetID
			AND[PetName] = @oldPetName
			AND	[Gender] = @oldGender
			AND [Species] = @oldSpecies
			AND	[PetTypeID] = @oldPetTypeID
			AND	[GuestID] = @oldGuestID			
		RETURN @@ROWCOUNT
	END
GO

--sp_delete_pet(int)
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments after having built the script.>>

print '' print '*** Creating sp_delete_pet'
GO						
CREATE PROCEDURE [dbo].[sp_delete_pet]
    (
        @PetID    [int]
    )
AS
    BEGIN
        DELETE
        FROM     [Pet]
        WHERE     [PetID] = @PetID

        RETURN @@ROWCOUNT
    END
GO

--  sp_create_pet_type
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments after having built the script.>>

print '' print ' Creating sp_create_pet_type'
GO
CREATE PROCEDURE sp_create_pet_type
    (
        @PetTypeID          [nvarchar](25),
        @Description        [nvarchar](1000)		
    )
AS
    BEGIN
        INSERT INTO [dbo].[PetType]
            ([PetTypeID], [Description])
        VALUES
            (@PetTypeID, @Description)

        RETURN @@ROWCOUNT
        SELECT SCOPE_IDENTITY()
    END
GO


-- sp_select_pet_type_by_id(id)
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments after having built the script.>>

print '' print ' Creating sp_select_pet_type_by_id'
GO
CREATE PROCEDURE sp_select_pet_type_by_id
AS
    BEGIN        
		SELECT 		[PetTypeID]
		FROM		[PetType]
		ORDER BY 	[PetTypeID]
	END
GO

--sp_delete_pet_type
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_delete_pet_type.>>

print '' print '*** Creating sp_delete_pet_type'
GO						
CREATE PROCEDURE [dbo].[sp_delete_pet_type_by_id]
    (
        @PetTypeID    [nvarchar](25)
    )
AS
    BEGIN
        DELETE
        FROM     [PetType]
        WHERE     [PetTypeID] = @PetTypeID

        RETURN @@ROWCOUNT
    END
GO
--  sp_create_event_type
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_create_event_type.>>

print '' print ' Creating sp_create_event_type'
GO
CREATE PROCEDURE sp_create_event_type
    (
        @EventTypeID        [nvarchar](15),
        @Description        [nvarchar](250) 
    )
AS
    BEGIN
        INSERT INTO [dbo].[EventType]
            ([EventTypeID], [Description])
        VALUES
            (@EventTypeID, @Description)

        RETURN @@ROWCOUNT
        SELECT SCOPE_IDENTITY()
    END
GO

-- sp_retrieve_all_event_type()
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_retrieve_all_event_type.>>

print '' print ' Creating sp_retrieve_all_event_type'
GO
CREATE PROCEDURE sp_retrieve_all_event_type
AS
    BEGIN
        SELECT [EventTypeID], [Description]
        FROM   [EventType]
        ORDER BY [EventTypeID]
    END
GO
-- sp_delete_event_type(id)
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_delete_event_type_by_id.>>

print '' print '*** Creating sp_delete_event_type_by_id'
GO				
CREATE PROCEDURE sp_delete_event_type_by_id
    (
        @EventTypeID        [nvarchar](17)
    )
AS
    BEGIN
        DELETE
        FROM    [EventType]
        WHERE    [EventTypeID] = @EventTypeID


        RETURN @@ROWCOUNT
    END
GO

-- sp_select_event_type_by_id(id)
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_select_event_type_by_id.>>

print '' print ' Creating sp_select_event_type_by_id'
GO
CREATE PROCEDURE sp_select_event_type_by_id
AS
    BEGIN        
		SELECT 		[EventTypeID]
		FROM		[EventType]
		ORDER BY 	[EventTypeID]
	END
GO



--  sp_delete_appointment_type_id
--  Author: <<Craig Barkley>>,Created:<<2/17/2019>>, Updated<<2/17/2019>>, What/Why<<Adding comments, sp_delete_appointment_type_id.>>

print '' print '*** Creating sp_delete_appointment_type_id'
GO						
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
	[Description]		[nvarchar](1000)	NULL
	
	CONSTRAINT [pk_AppointmentTypeID] PRIMARY KEY([AppointmentTypeID] ASC),
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


/* 
Start Gunardi Code
*/




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



/*
 * Author: Gunardi Saputra
 * Created: 2019/01/23
 */
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


/*
 * Author: Gunardi Saputra
 * Created: 2019/01/23
 * Run
 */
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

/*
Author: Gunardi Saputra
Created Date: 2019/02/20

This stored procedure updates a sponsor record in the sponsor table by its id.
*/
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
				[Address]				= @Address,
				[City]				= @City,
				[State]				= @State,
				[PhoneNumber]		= @PhoneNumber,
				[Email]				= @Email,
				[ContactFirstName]				= @ContactFirstName,
				[ContactLastName]				= @ContactLastName,
				[StatusID]				= @StatusID,
				[DateAdded]				= @DateAdded,
		
		[Active]				= @Active
				
			

		WHERE	[SponsorID] 	= @SponsorID
		AND [Name]				= @OldName
		AND [Address]		 =  @OldAddress
		AND [City]		 =  @OldCity
		AND [State]		 =  @OldState
		AND [PhoneNumber]		 =  @OldPhoneNumber
		AND [Email]		 =  @OldEmail
		AND [ContactFirstName]		 =  @OldContactFirstName
		AND [ContactLastName]		 =  @OldContactLastName
		AND [StatusID]		 =  @OldStatusID
		AND [DateAdded]		 =  @OldDateAdded
		AND [Active]		 =  @OldActive
			
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






/*  Name: Eduardo Colon
    Date: 2019-03-05 */

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



/*  Name: Eduardo Colon
    Date: 2019-03-05 */
print '' print '*** Inserting SetupList Test Records'
GO

INSERT INTO [dbo].[SetupList]
		([SetupID], [Completed], [Description], [Comments])
	VALUES
		(100000, 0, ' Prior to Guest Arrival: Registration Desk,signs,banners', 'Banners are not ready yet'),
		(100001, 0, ' Display Equipment: Prepares for display boards,tables,chairs,, printed material and names badges','Badges are not ready yet'),
		(100002, 1, ' Check Av Equipment: Laptop,projectors :Ensure all cables,leads,laptop,mic and mouse are presented and working', 'Av Equipment is ready'),
		(100003, 1, ' Confirm that all decor and linen is in place ', 'Decor and linen are  ready'),
		(100004, 1, ' Walk through to make sure bathrooms are clean and stocked ', 'Bathrooms are  ready')
GO


/*  Name: Eduardo Colon
    Date: 2019-03-05 */
print '' print '*** Creating sp_select_setuplists'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_setuplists]

AS
	BEGIN
		SELECT 	    [SetupListID], [SetupID], [Completed], [Description],	[Comments]
		FROM		[SetupList]
	END
GO

/*
  NAME:  Eduardo Colon'
  Date:   2019-03-05'
*/
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

/*
Eric Bostwick 3/8
*/
/*
 * Eric Bostwick
 * Created: 3/7/2019
 * Retrieves All SupplierOrders in the supplier order table
*/
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

/*
 * Eric Bostwick
 * Created: 3/7/2019
 * Retrieves All SupplierOrderLines for a supplier order 
*/
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

/*
 * Eric Bostwick
 * Created: 3/7/2019
 * Deletes All SupplierOrderLines for a supplier order 
*/
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

/*
 * Eric Bostwick
 * Created: 3/7/2019
 * Updates Supplier Order For the SupplierOrderID
 */
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

/*
 * Eric Bostwick
 * Created: 3/7/2019
 * Deletes A SupplierOrder
*/
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






















