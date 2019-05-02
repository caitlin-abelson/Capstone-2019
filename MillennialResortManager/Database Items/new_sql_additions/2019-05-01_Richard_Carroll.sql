USE [MillennialResort_DB]
GO
DROP TRIGGER generate_supplier_order
GO
/*
 * Updated: 2019/05/01
 * By: Richard Carroll
 * Reason: It would be a bad idea to try to take lines from 
 * internal orders that have already been filled, as they 
 * are no longer taking from internal inventory.
*/
print '' print'*** Altering trigger generate_supplier_order'
GO
CREATE TRIGGER generate_supplier_order on InternalOrder after Insert
AS
    BEGIN
    
    DECLARE @ItemID as int;
    DECLARE @UnitPrice as money;
    DECLARE @SupplierID as int;
    DECLARE @PreviousSupplierID as int;
    DECLARE @SupplierOrderQty as int;
    DECLARE @SupplierOrderID as int;
    
    Declare supply_order_line_cursor Cursor For
        Select InternalOrderLine.ItemID, ItemSupplier.UnitPrice,
                ItemSupplier.SupplierID,
                (Item.ReorderQty - (Item.OnHandQty - InternalOrderLine.OrderQty)) - (Select sum(OrderQty) 
from SupplierOrderLine join SupplierOrder on SupplierOrder.SupplierOrderID = SupplierOrderLine.SupplierOrderID
 where SupplierOrderLine.ItemID = Item.ItemID AND SupplierOrder.OrderComplete = 0) AS SupplyOrderQty
               
               From Item Inner Join InternalOrderLine on Item.ItemID = InternalOrderLine.ItemID
			   inner join InternalOrder on InternalOrder.InternalOrderID = InternalOrderLine.InternalOrderID
               inner join ItemSupplier on InternalOrderLine.ItemID = ItemSupplier.ItemID
               Where ItemSupplier.PrimarySupplier = 1 AND (Item.OnHandQty - InternalOrderLine.OrderQty <= (Item.ReorderQty / 4))
			   And InternalOrder.OrderComplete = 0
               Order By SupplierID
       OPEN supply_order_line_cursor
       WHILE @@FETCH_STATUS = 0
       BEGIN 
       
        FETCH NEXT FROM supply_order_line_cursor into @ItemID, @UnitPrice, @SupplierID, @SupplierOrderQty
        Begin Transaction
        Begin Try
            IF @PreviousSupplierID != @SupplierID
                SET @PreviousSupplierID = @SupplierID
                Insert into dbo.SupplierOrder(Description, OrderComplete, DateOrdered, SupplierID)
                Values('Generated Supplier Order', 0, GetDate(), @SupplierID)
                SET @SupplierOrderID = SCOPE_IDENTITY()
            
            Insert into dbo.SupplierOrderLine(ItemID, SupplierOrderID, Description, OrderQty, UnitPrice)
            Values(@ItemID, @SupplierOrderID, 'Generated Supplier Order Line', @SupplierOrderQty, @UnitPrice)
            
        End Try
        Begin Catch
            IF @@TRANCOUNT > 0
                Rollback Transaction
        End Catch
        If @@Trancount > 0
            Commit Transaction
       
       END
   END
GO