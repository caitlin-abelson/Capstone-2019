USE [MillennialResort_DB]
GO
DROP TRIGGER generate_supplier_order
GO
/**
* Since EmployeeID isn't nullable we need to make an arbitrary employee for auto generated orders.
*/
SET IDENTITY_INSERT Employee ON
Insert Into Employee(EmployeeID, FirstName, LastName, PhoneNumber, Email, PasswordHash, Active)
VALUES (1, 'Auto', 'Generator', 1234567890, 'auto@company.com', '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e', 1)
SET IDENTITY_INSERT EMPLOYEE OFF
GO
/*
 * Updated: 2019/05/04
 * By: Richard Carroll
 * Made the thing actually insert orders properly.
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
	SET @PreviousSupplierID = -1;
    DECLARE @PreviousItemID as int;
	SET @PreviousItemID = -1;
    DECLARE @SupplierOrderQty as int;
    DECLARE @SupplierOrderID as int;
    
    Declare supply_order_line_cursor Cursor Local For
        Select InternalOrderLine.ItemID, ItemSupplier.UnitPrice,
                ItemSupplier.SupplierID,
                Item.ReorderQty - (Item.OnHandQty - InternalOrderLine.OrderQty) - (Select coalesce( sum(OrderQty), 0)
				from SupplierOrderLine
				 join SupplierOrder 
				on SupplierOrder.SupplierOrderID = SupplierOrderLine.SupplierOrderID 
				 and SupplierOrder.OrderComplete = 0
				join Item 
				on SupplierOrderLine.ItemID = Item.ItemID) AS SupplyOrderQty
               
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
            IF @SupplierID is not null and @ItemID is not null and @UnitPrice is not null and @SupplierOrderQty is not null and @SupplierOrderQty > 0
				BEGIN
                IF @PreviousSupplierID != @SupplierID
					BEGIN
                    SET @PreviousSupplierID = @SupplierID
                    Insert into dbo.SupplierOrder(EmployeeID, Description, OrderComplete, DateOrdered, SupplierID)
                    Values(1, 'Generated Supplier Order', 0, GetDate(), @SupplierID)
                    SET @SupplierOrderID = SCOPE_IDENTITY()
					END
				IF @PreviousItemID != @ItemID
					BEGIN
						Set @PreviousItemID = @ItemID
						Insert into dbo.SupplierOrderLine(ItemID, SupplierOrderID, Description, OrderQty, UnitPrice)
						Values(@ItemID, @SupplierOrderID, 'Generated Supplier Order Line', @SupplierOrderQty, @UnitPrice)
					END
				END
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