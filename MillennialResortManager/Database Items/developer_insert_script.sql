USE [MillennialResort_DB]
GO

/*********************************************************************/
/*                          How to comment                           */

/* Start {Your name} */

-- Created: {Date you wrote the script 
/* The first line of your code here */


/*********************************************************************/
/* Developers place their test code here to be submitted to database */
/* vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv */
/*********************************************************************/

/* Start Phil, submitted 2019-02-18 */

/* CREATE PROCEDURE sp_retrieve_all_events
--DQ'd for error 'SponsorName is not valid field'
AS
	BEGIN
		SELECT
		[EventID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Description],[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[Location],
		[Sponsored],
		[Event].[SponsorID],
		[Sponsor].[SponsorName],
		[Approved]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
				INNER JOIN [dbo].[Sponsor]
			ON		[Event].[SponsorID] = [Sponsor].[SponsorID]
	END
--GO*/

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

/* Start Carlos */

/*CREATE PROCEDURE [sp_read_all_internal_orders]
-- Pending add per Austin D.
-- Does this do something different than sp_retrieve_all_supplier_order?
-- Also does not follow naming conventions
AS
	BEGIN
		SELECT *
		FROM SupplierOrder
	END
--GO */

/* Start Dani */

-- TABLE NOTATION TO BE ADDED BY AUSTIN D.
-- Created: 2019-01-22
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Changed length for Description, added nulls to BuildingName, Address, & Description added BuildingSatusID field
-- Update 2019-02-20 Author: Dani
-- Update 2019-02-20 Desc: Removed Active field, added ResortPropertyID field

-- Created: 2019-02-20
/* CREATE PROCEDURE [dbo].[sp_insert_resortproperty]
-- Pending add per Austin D.
-- Could be used as a shortcut from inside other SP's, but should not be called from the app.
	(
		@ResortPropertyTypeID	[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			(@ResortPropertyTypeID)

		SELECT SCOPE_IDENTITY()
	END
GO */

--print '' print '*** TO DO: Create Inspection Table ***'
--print '' print '*** TO DO: Create sp to find all maintance tickets for buildings ResortPropertyID ***'
--print '' print '*** TO DO: Create sp to find all maintance tickets for each building by rooms ResortPropertyID ***'
--print '' print '*** TO DO: Create sp to find all inspection records for buildings ResortPropertyID ***'


/* Start Matt */
-- Retrieves an employee based on email
CREATE PROCEDURE [sp_retrieve_employee_by_email]
(
	@Email 		[nvarchar](250)
)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Email] = @Email
	END
GO
