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