USE [MillennialResort_DB]
GO
/*********************************************************************/
/* Developers place their test code here to be submitted to database */
/*********************************************************************/

/* Start Phil, submitted 2019-02-18 */

/* CREATE PROCEDURE sp_retrieve_all_events
--DQ'd for error 'SponsorName is not valid field'
AS
	BEGIN
		SELECT 	[EventID],[EventTitle],[Event].[EmployeeID],[Employee].[FirstName],[EventTypeID] AS [EventType],[Description],[EventStartDate],
				[EventEndDate],[KidsAllowed],[NumGuests],[Location],[Sponsored],[Event].[SponsorID],[Sponsor].[SponsorName], [Approved]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
				INNER JOIN [dbo].[Sponsor]
			ON		[Event].[SponsorID] = [Sponsor].[SponsorID]
	END
--GO*/

/* Start Eric Bostwick */

-- Created 2/4/19
--Updated 2/14/19 to Add Active
GO
CREATE TABLE [dbo].[ItemSupplier] (
	[ItemID]			[int]                         NOT NULL,
	[SupplierID]		[int]					  	  NOT NULL,
	[PrimarySupplier]	[bit]						  NULL,
	[LeadTimeDays]		[int]						  NULL DEFAULT 0,
	[UnitPrice]			[money]						  NULL DEFAULT 0.0,
	[Active]			[bit]						  NOT NULL DEFAULT 1

	CONSTRAINT [pk_ItemID_ItemID] PRIMARY KEY([ItemID] ASC, [SupplierID] ASC)
)

GO

-- Created 2/4/19
--Foreign Keys For ItemSupplier Join Table
ALTER TABLE [dbo].[ItemSupplier] WITH NOCHECK
	ADD CONSTRAINT [fk_ItemID] FOREIGN KEY ([ItemID])
	REFERENCES [dbo].[Item]([ItemID])
	ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ItemSupplier] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierID] FOREIGN KEY ([SupplierID])
	REFERENCES [dbo].[Supplier]([SupplierID])
	ON UPDATE CASCADE
GO

-- Created: 2019-02-04
CREATE PROCEDURE [dbo].[sp_update_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@Active				[bit],

		@OldItemID 			[int],
		@OldSupplierID		[int],
		@OldPrimarySupplier	[bit],
		@OldLeadTimeDays	[int],
		@OldUnitPrice		[money],
		@OldActive			[bit]
	)
AS
BEGIN
		IF(@PrimarySupplier = 1)
			BEGIN
				UPDATE [dbo].[ItemSupplier]
				SET [PrimarySupplier]= 0
				WHERE [ItemID] = @ItemID
			END

		UPDATE [dbo].[ItemSupplier]
		SET [ItemID] = @ItemID,
			[SupplierID] = @SupplierID,
			[PrimarySupplier] = @PrimarySupplier,
			[LeadTimeDays] = @LeadTimeDays,
			[UnitPrice] = @UnitPrice,
			[Active] = @Active
		WHERE
			[ItemID] = @OldItemID AND
			[SupplierID] = @OldSupplierID AND
			[PrimarySupplier] = @OldPrimarySupplier AND
			[LeadTimeDays] = @OldLeadTimeDays AND
			[UnitPrice] = @OldUnitPrice AND
			[Active] = @OldActive
END

GO

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
		/* We can only have one primary supplier for each itemid
		*  so if we are setting the primary supplier to this supplier
		*  we need to set the primary supplier to false for each itemsupplier record for
		*  this item before we set it to true for this one.
		*  This seems like a good place for a transaction.
		* /

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

-- Created: 2019-02-07
CREATE PROCEDURE [dbo].[sp_delete_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		DELETE
		FROM		[ItemSupplier]
		WHERE		[ItemID] = @ItemID AND [SupplierID] = @SupplierID
	END
GO

-- Created: 2019-02-07
CREATE PROCEDURE [dbo].[sp_deactivate_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN

		UPDATE		[ItemSupplier]
		SET [Active] = 0
		WHERE		[ItemID] = @ItemID AND [SupplierID] = @SupplierID
	END
GO

-- Created: 2019-02-07
-- Description: Returns all the suppliers not setup in the itemsupplier table for that item. This is so user doesn't get the option to add a supplier that will create a primary key violation on the item supplier table.
CREATE PROCEDURE [dbo].[sp_select_suppliers_for_itemsupplier_mgmt_by_itemid]
(
	@ItemID [int]
)
AS
	BEGIN
		SELECT		[Supplier].[SupplierID],
					[Supplier].[Name],
					[Supplier].[Address],
					[Supplier].[City],
					[Supplier].[State],
					[Supplier].[PostalCode],
					[Supplier].[Country],
					[Supplier].[PhoneNumber],
					[Supplier].[Email],
					[Supplier].[ContactFirstName],
					[Supplier].[ContactLastName],
					[Supplier].[DateAdded],
					[Supplier].[Description],
					[Supplier].[Active]

		FROM		[Supplier] LEFT OUTER JOIN [ItemSupplier] [isup] ON [isup].[SupplierID] = [Supplier].[SupplierID]
		WHERE		[isup].[Itemid] != @ItemID OR [isup].[Itemid] is Null
	END
GO

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