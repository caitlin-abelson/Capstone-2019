USE [MillennialResort_DB]
GO
/* Start Kevin Broskow */

print '' print '*** Creating Receiving Table'
GO
CREATE TABLE [dbo].[Receiving](
	[ReceivingID]			[int]IDENTITY(100000, 1)	NOT NULL,
	[SupplierOrderID]		[int]				NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[DateDelivered]			[DateTime]				NOT NULL,
	[Active]				[bit]						NOT NULL 	DEFAULT 1

CONSTRAINT [pk_ReceivingID] PRIMARY KEY([ReceivingID] ASC)
)
GO
print '' print '*** creating sp_insert_receiving'
GO
GO
CREATE PROCEDURE [dbo].[sp_insert_receiving]
(
	@SupplierOrderID		[int],
	@Description			[nvarchar](1000),
	@DateDelivered			[DateTime]
)
AS
	BEGIN
		INSERT INTO [dbo].[Receiving]([SupplierOrderID], [Description], [DateDelivered]) 
		VALUES(@SupplierOrderID, @Description, @DateDelivered)
	END
GO
print '' print '*** creating sp_select_all_receiving'
GO
CREATE PROCEDURE [dbo].[sp_select_all_receiving]
AS
	BEGIN
		SELECT [ReceivingID],[SupplierOrderID],[Description],[DateDelivered],[Active]
		FROM [Receiving]
	END
GO

print '' print '*** creating sp_select_receiving'
GO
CREATE PROCEDURE [dbo].[sp_select_receiving]
(
			@ReceivingID 	[int]
		)
AS
	BEGIN
		SELECT [ReceivingID],[SupplierOrderID],[Description],[DateDelivered],[Active]
		FROM [Receiving]
		WHERE [ReceivingID] = @ReceivingID
	END
GO
print '' print '*** creating sp_update_receiving'
GO 
CREATE PROCEDURE [dbo].[sp_update_receiving]
(
	@ReceivingID 		[int],
	@Description		[nvarchar](1000)
)
AS
	BEGIN
	UPDATE [Receiving]
	SET [Description] = @Description
	WHERE [ReceivingID] = @ReceivingID
END
GO

print '' print '*** creating sp_deactivate_receiving'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_receiving]
(
	@ReceivingID		[int]
)
AS
	BEGIN
	UPDATE [Receiving]
	SET	[Active] = 0
	WHERE [ReceivingID] = @ReceivingID
END
GO



print '' print '*** creating sp_select_supplier_order_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_supplier_order_by_id]
(
		@SupplierOrderID		[int]
)
AS
	BEGIN
	SELECT [EmployeeID], [Description], [OrderComplete], [DateOrdered], [SupplierID]
	FROM [SupplierOrder]
	WHERE [SupplierOrderID] = @SupplierOrderID
END 
GO

print '' print '*** creating sp_complete_order_by_id'
GO
CREATE PROCEDURE [dbo].[sp_complete_order_by_id]
(
		@SupplierOrderID		[int]
)
AS
	BEGIN
	UPDATE [SupplierOrder]
	SET [OrderComplete] = 1
	WHERE [SupplierOrderID] = @SupplierOrderID
END 
GO

print'' print'*** creating sp_update_supplier_order_line'
GO
CREATE PROCEDURE [dbo].[sp_update_supplier_order_line]
(
@SupplierOrderID	[int],
@ItemID		[int],
@QtyReceived		[int]
)
AS
BEGIN
UPDATE [SupplierOrderLine]
SET [QtyReceived] = @QtyReceived
WHERE [SupplierOrderID] = @SupplierOrderID
AND		[ItemID] = @ItemID
END
GO
/*Ending Kevin */

















