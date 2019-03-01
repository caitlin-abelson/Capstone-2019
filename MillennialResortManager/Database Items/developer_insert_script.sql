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

/* Start Eduardo */

-- Created: 2019-01-23
CREATE PROCEDURE [dbo].[sp_update_role_by_id]
	(
		@RoleID			  [nvarchar](50), 
		@OldDescription   [nvarchar](1000),
		@NewDescription	  [nvarchar](1000)		
	)
AS
	BEGIN
	
		BEGIN
			UPDATE [Role]
				SET [Description] = @NewDescription
				
				WHERE [RoleID] = @RoleID
				AND	  [Description] = @OldDescription
					
			RETURN @@ROWCOUNT
		END
	END
GO

-- Created: 2019-01-23
-- Update 2019-02-28 Author: Austin Delaney
-- Update 2019-02-28 Desc: Removed active search.
CREATE PROCEDURE sp_retrieve_roles_by_term_in_description
	(
		@SearchTerm		[nvarchar](250)
	)
AS
	BEGIN
		SELECT [RoleID],  [Description]
		FROM 	[Role]
		WHERE 	[Description] LIKE '%' + @SearchTerm + '%'
	END
GO

/* Start Austin */

-- Event table needs corrected

ALTER PROCEDURE [dbo].[sp_insert_roles]
	(
		@RoleID				[nvarchar](50),
		@Description		[nvarchar](250)	
	)
AS
	BEGIN
		INSERT INTO [Role]
			([RoleID], [Description])
		VALUES
			(@RoleID, @Description)
			
		RETURN @@ROWCOUNT
	END

GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-02-28 Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-02-28 Desc'
	,@value = N'Removed scope identity return'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'Procedure', @level1name = 'sp_insert_roles'
GO

ALTER TABLE [dbo].[EmployeeRole]  DROP CONSTRAINT [fk_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeRole]  WITH NOCHECK ADD CONSTRAINT [fk_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeRole] CHECK CONSTRAINT [fk_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeRole]  DROP CONSTRAINT [fk_RoleID]
GO
ALTER TABLE [dbo].[EmployeeRole]  WITH NOCHECK ADD CONSTRAINT [fk_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
ON UPDATE CASCADE ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeRole] CHECK CONSTRAINT [fk_RoleID]
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-02-28 Author'
	,@value = N'Austin Delaney'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'TABLE', @level1name = 'EmployeeRole'
GO
EXEC sys.sp_addextendedproperty
	@name = N'Update 2019-02-28 Desc'
	,@value = N'Removed scope identity return'
	,@level0type = N'Schema', @level0name = 'dbo'
	,@level1type = N'TABLE', @level1name = 'EmployeeRole'
GO

ALTER TABLE [dbo].[MaintenanceWorkOrder] ALTER COLUMN [WorkingEmployeeID] [int] NULL;
GO

ALTER TABLE [dbo].[Guest] ADD [CheckedIn] [bit] NOT NULL DEFAULT 1;
GO
EXEC sys.sp_addextendedproperty @name = N'Update 2019-03-01 Author' ,@value = N'Austin Delaney' ,@level0type = N'Schema', @level0name = 'dbo' ,@level1type = N'TABLE', @level1name = 'Guest'
GO
EXEC sys.sp_addextendedproperty @name = N'Update 2019-03-01 Desc' ,@value = N'Added CheckedIn bit field' ,@level0type = N'Schema', @level0name = 'dbo' ,@level1type = N'TABLE', @level1name = 'Guest'
GO
EXEC sys.sp_addextendedproperty @name = N'Description' ,@value = N'If a guest is currently check into the resort grounds' ,@level0type = N'Schema', @level0name = 'dbo' ,@level1type = N'TABLE', @level1name = 'Guest' ,@level2type=N'COLUMN',@level2name=N'CheckedIn'
GO


ALTER TABLE [dbo].[Guest] ADD DEFAULT ((0)) FOR [Minor]
GO

ALTER TABLE [dbo].[ItemSupplier] ALTER COLUMN [PrimarySupplier] [bit] NOT NULL;
GO

print '' print '*** Creating SupplierOrder Table'
--Eric Bostwick 
--Created 2/26/19
--Updated
GO
CREATE TABLE [dbo].[SupplierOrder] (
	[SupplierOrderID]	[int] IDENTITY(100000, 1)     NOT NULL,
	[SupplierID]		[int]					  	  NOT NULL,
	[EmployeeID]		[int]						  NOT NULL,	
	[Description]       [nvarchar](1000)			  NULL,
	[DateOrdered]   	[DateTime]					  NOT NULL DEFAULT GetDate(),
	[OrderComplete]		[bit]						  NOT NULL DEFAULT 0
	
	CONSTRAINT [pk_SupplierOrderID_SupplierOrderID] PRIMARY KEY([SupplierOrderID] ASC)
)

GO

print '' print '*** Creating SupplierOrderLine Table'
--Eric Bostwick 
--Created 2/26/19
--Updated
GO
CREATE TABLE [dbo].[SupplierOrderLine] (
	[SupplierOrderID]	[int]                         NOT NULL,
	[ItemID]			[int]						  NOT NULL,	
	[Description]       [nvarchar](1000)			  NULL,
	[OrderQty]      	[int]	    				  NOT NULL,
	[UnitPrice]			[money]	  				      NOT NULL,
	[QtyReceived]		[int]						  NOT NULL DEFAULT 0
	
	CONSTRAINT [pk_SupplierOrderLine_SupplierOrderLineID] PRIMARY KEY([SupplierOrderID] ASC, [ItemID] ASC)
)

GO

print '' print '*** Adding Foreign Key for SupplierOrder'
--Eric Bostwick 
--Created 2/26/19
--Foreign Keys For SupplierOrder

ALTER TABLE [dbo].[SupplierOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierOrder_SupplierID] FOREIGN KEY ([SupplierID])
	REFERENCES [dbo].[Supplier]([SupplierID])
	ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[SupplierOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierOrder_EmployeeID] FOREIGN KEY ([EmployeeID])
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE CASCADE
GO


print '' print '*** Adding Foreign Keys for SupplierOrderLine'

ALTER TABLE [dbo].[SupplierOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierOrderLine_ItemID] FOREIGN KEY ([ItemID])
	REFERENCES [dbo].[Product]([ItemID])
	ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[SupplierOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierOrderID] FOREIGN KEY ([SupplierOrderID])
	REFERENCES [dbo].[SupplierOrder]([SupplierOrderID])
	ON UPDATE CASCADE
GO

/*
 * Eric Bostwick
 * Created: 2/26/2019
 * Creates Supplier Order Returns the Supplier Order ID 
 */
print '' print '*** Creating sp_insert_supplier_order ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier_order]
	(
		@SupplierOrderID	[int] OUTPUT,
		@SupplierID			[int],
		@EmployeeID			[int],
		@Description		[nvarchar](1000)		
	)
AS
BEGIN
		INSERT INTO [dbo].[SupplierOrder] ([SupplierID], [EmployeeID], [Description]) 
			 VALUES (@SupplierID,@EmployeeID, @Description)

		SET @SupplierOrderID = SCOPE_IDENTITY()
	
END
GO
/*
 * Eric Bostwick
 * Created: 2/26/2019
 * Creates Supplier Order Line  
 */
print '' print '*** Creating sp_insert_supplier_order_line ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier_order_line]
	(
		@SupplierOrderID	[int],
		@ItemID				[int],
		@Description		[nvarchar](1000),
		@OrderQty			[int],
	@UnitPrice			[money]
		
	)
AS
BEGIN
		INSERT INTO [dbo].[SupplierOrderLine] ([SupplierOrderID], [ItemID], [Description], [OrderQty], [UnitPrice]) 
			 VALUES (@SupplierOrderID, @ItemID, @Description, @OrderQty, @UnitPrice)
	
END
GO

/*
 * Eric Bostwick
 * Created: 2/26/2019
 * Retrieves All Active Items For a Supplier in the ItemSupplier Table
 */
print '' print '*** Creating sp_select_itemsuppliers_by_supplierID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_supplierid]
	(
		@SupplierID	[int]		
	)
AS
BEGIN
		SELECT [isup].[ItemID], [isup].[SupplierID], [isup].[PrimarySupplier], 
		[isup].[LeadTimeDays], [isup].[UnitPrice],
		[p].[ItemTypeID], [p].[Description], [p].[OnHandQuantity], [p].[Name], [p].[ReOrderQuantity],
		[p].[DateActive], [p].[Active], [p].[CustomerPurchasable], [p].[RecipeID], [p].[OfferingID]
		FROM   [dbo].[ItemSupplier] [isup] JOIN Product [p] on [p].[ItemID] = [isup].[ItemID]
		WHERE  [isup].[SupplierID] = @SupplierID AND [isup].[Active] = 1
	
END
GO
