USE [MillennialResort_DB]
GO
/*Start Kevin Broskow alterations and additions*/

ALTER TABLE ItemSupplier
	ADD		SupplierItemID	[int]
GO

DROP PROCEDURE [dbo].[sp_create_itemsupplier]
GO
CREATE PROCEDURE [dbo].[sp_create_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@SupplierItemID		[int]
	)
AS
BEGIN
    UPDATE [dbo].[ItemSupplier]
    SET [PrimarySupplier] = 0
    WHERE [ItemID] = @ItemID
    
    INSERT INTO [dbo].[ItemSupplier]
    ([ItemID], [SupplierID], [PrimarySupplier], [LeadTimeDays], [UnitPrice], [SupplierItemID])
    VALUES
    (@ItemID, @SupplierID, @PrimarySupplier, @LeadTimeDays, @UnitPrice, @SupplierItemID)
END
GO

DROP PROCEDURE [dbo].[sp_select_itemsupplier_by_itemid_and_supplierid]
GO

CREATE PROCEDURE [dbo].[sp_select_itemsupplier_by_itemid_and_supplierid]
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
		[Item].[Active] AS [ItemActive],
		[ItemSupplier].[SupplierItemID]
		FROM		[ItemSupplier]
					JOIN [Item] ON [Item].[ItemID] = [ItemSupplier].[ItemID]
					JOIN [Supplier] ON [Supplier].[SupplierID] = [ItemSupplier].[SupplierID]
		WHERE		[ItemSupplier].[ItemID] = @ItemID AND [ItemSupplier].[SupplierID] = @SupplierID
	END
GO
DROP PROCEDURE [dbo].[sp_select_itemsuppliers_by_itemid]
GO
CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_itemid]
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
		[Item].[Active] AS [SupplierActive],
		[ItemSupplier].[SupplierItemID]
		FROM		[ItemSupplier]
					JOIN [Item] ON [Item].[ItemID] = [ItemSupplier].[ItemID]
					JOIN [Supplier] ON [Supplier].[SupplierID] = [ItemSupplier].[SupplierID]
		WHERE		[ItemSupplier].[itemID] = @ItemID
	END
GO
DROP PROCEDURE [dbo].[sp_update_itemsupplier]
GO
CREATE PROCEDURE [dbo].[sp_update_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@Active				[bit],
		@SupplierItemID		[int],

		@OldItemID 			[int],
		@OldSupplierID		[int],
		@OldPrimarySupplier	[bit],
		@OldLeadTimeDays	[int],
		@OldUnitPrice		[money],
		@OldActive			[bit],
		@OldSupplierItemID	[int]
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
			[Active] = @Active,
			[SupplierItemID] = @SupplierItemID
		WHERE
			[ItemID] = @OldItemID AND
			[SupplierID] = @OldSupplierID AND
			[PrimarySupplier] = @OldPrimarySupplier AND
			[LeadTimeDays] = @OldLeadTimeDays AND
			[UnitPrice] = @OldUnitPrice AND
			[Active] = @OldActive
END

GO

DROP PROCEDURE [dbo].[sp_select_itemsuppliers_by_supplierid]
GO
CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_supplierid]
(
@SupplierID[int]
)
AS
BEGIN
SELECT [isup].[ItemID], [isup].[SupplierID], [isup].[PrimarySupplier],
[isup].[LeadTimeDays], [isup].[UnitPrice],
[p].[ItemTypeID], [p].[Description], [p].[OnHandQuantity], [p].[Name], [p].[ReOrderQuantity],
[p].[DateActive], [p].[Active], [p].[CustomerPurchasable], [p].[RecipeID], [p].[OfferingID], [isup].[SupplierItemID]
FROM   [dbo].[ItemSupplier] [isup] JOIN Product [p] on [p].[ItemID] = [isup].[ItemID]
WHERE  [isup].[SupplierID] = @SupplierID AND [isup].[Active] = 1

END
GO



CREATE PROCEDURE [dbo].[sp_select_supplier_item_by_item_and_supplier](
@ItemID	[int],
@SupplierID		[int]
)
AS
BEGIN
SELECT 
[SupplierItemID]
FROM [ItemSupplier]
WHERE [ItemID] = @ItemID
AND		[SupplierID] = @SupplierID
END
GO

CREATE PROCEDURE [dbo].[sp_update_special_order_lines](
@SpecialOrderID		[int],
@ItemID				[int],
@QtyReceived		[int]
)
AS
BEGIN
UPDATE[SpecialOrderLine]
SET [QtyReceived] = @QtyReceived
WHERE
[SpecialOrderID] = @SpecialOrderID
AND
[ItemID] = @ItemID
END
GO


/*End Kevin Broskow additions and alterations*/

