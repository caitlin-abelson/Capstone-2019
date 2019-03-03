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

/* Start James Heim */
-- Created: 2019-02-27

/*
 * Author: James Heim
 * Created 2019-02-27
 *
 * Create the Shop Table.
 */
print '' print '*** Creating Shop Table'
GO
CREATE TABLE [dbo].[Shop] (
	[ShopID]		[int] IDENTITY(100000, 1)	NOT NULL,
	[RoomID]		[int]						NOT NULL,
	[Name]			[nvarchar](50)				NOT NULL,
	[Description]	[nvarchar](100)				NOT NULL,
	[Active]		[bit]						NOT NULL DEFAULT 1
	
	CONSTRAINT [pk_ShopID] PRIMARY KEY([ShopID] ASC),
	CONSTRAINT [ak_RoomID] UNIQUE([RoomID] ASC)
)

/*
 * Author: James Heim
 * Created 2019-02-27
 *
 * Add RoomID as a foreign key to Shop table.
 */
print '' print '*** Adding Foreign Key constraint for Shop'
GO
ALTER TABLE [dbo].[Shop] WITH NOCHECK
	ADD CONSTRAINT [fk_RoomID] FOREIGN KEY ([RoomID])
	REFERENCES [dbo].[Room]([RoomID])
	ON UPDATE CASCADE
GO

		
/*
 * Author: James Heim
 * Created 2019-03-01
 *
 * Insert Shop.
 */	
print '' print'*** Creating sp_insert_shop'
GO
CREATE PROCEDURE [dbo].[sp_insert_shop]
	(
		@ShopID 			[int] 				OUTPUT,
		@RoomID				[int],
		@Name				[nvarchar](50),
		@Description		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Shop]
			([RoomID], [Name], [Description])
		VALUES
			(@RoomID, @Name, @Description)
			
		SET @ShopID = SCOPE_IDENTITY()
		
		RETURN SCOPE_IDENTITY()
	END
GO

/*
 * Author: James Heim
 * Created 2019-03-01
 *
 * Select shop by ID.
 */
print '' print'*** Creating sp_select_shop_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_shop_by_id]
	(
		@ShopID 			[int]
	)
AS
	BEGIN
		SELECT [RoomID], [Name], [Description], [Active]
		FROM [Shop]
		WHERE [ShopID] = @ShopID
	END
GO
		
/*
 * Author: James Heim
 * Created 2019-02-27
 *
 * Select all shops in the table.
 */
print '' print '*** Creating sp_select_shops'
GO
CREATE PROCEDURE [dbo].[sp_select_shops]
AS
	BEGIN
		SELECT [ShopID], [RoomID], [Name], [Description], [Active]
		FROM [Shop]
		ORDER BY [ShopID], [RoomID]
	END
GO

/*
 * Author: James Heim
 * Created 2019-02-28
 * 
 * Select all shops for the View Model.
 */
print '' print '*** Creating sp_select_view_model_shops'
GO

CREATE PROCEDURE [dbo].[sp_select_view_model_shops]
AS
	BEGIN
		SELECT  [Shop].[ShopID],
				[Shop].[RoomID],
				[Shop].[Name],
				[Shop].[Description],
				[Shop].[Active],
				[Room].[RoomNumber],
				[Room].[BuildingID]
		FROM [Shop] INNER JOIN [Room] ON [Shop].[RoomID] = [Room].[RoomID]

	END
GO

