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
 * Updated: 2019/05/03
 * By: Richard Carroll
 * Corrected some logic errors that would cause issues, such as checking
 * for nulls before inserting.
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
    
    Declare supply_order_line_cursor Cursor Local For
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
            IF @SupplierID is not null and @ItemID is not null and @UnitPrice is not null and @SupplierOrderQty is not null 
				BEGIN
                IF @PreviousSupplierID != @SupplierID
					BEGIN
                    SET @PreviousSupplierID = @SupplierID
                    Insert into dbo.SupplierOrder(EmployeeID, Description, OrderComplete, DateOrdered, SupplierID)
                    Values(1, 'Generated Supplier Order', 0, GetDate(), @SupplierID)
                    SET @SupplierOrderID = SCOPE_IDENTITY()
					END

                Insert into dbo.SupplierOrderLine(ItemID, SupplierOrderID, Description, OrderQty, UnitPrice)
                Values(@ItemID, @SupplierOrderID, 'Generated Supplier Order Line', @SupplierOrderQty, @UnitPrice)
				END
        End Try
        Begin Catch
            
            IF @@TRANCOUNT > 0
            print ERROR_MESSAGE()
                Rollback Transaction
        End Catch
        If @@Trancount > 0
            Commit Transaction
       
       END
   END
GO
-- Eric Bostwick
-- Created: 3/7/2019
-- Retrieves All SupplierOrders in the supplier order table
/*
 * Updated: 2019/04/26
 * By: Richard Carroll
 * Reason: Generated Orders need to be approved before they can be associated with the rest of the orders
*/
print '' print'*** Altering sp_select_all_supplier_orders'
GO
Alter Procedure [dbo].[sp_select_all_supplier_orders]
As
    BEGIN
        SELECT [so].[SupplierOrderID], [so].[SupplierID], [s].[Name] AS SupplierName, [so].[EmployeeID], [e].[FirstName], [e].[LastName], [so].[Description],
        [so].[DateOrdered], [so].[OrderComplete]
        FROM [SupplierOrder] so INNER JOIN [Employee] e ON [so].[EmployeeID] = [e].[EmployeeID]
		INNER JOIN [Supplier] s ON [s].[SupplierID] = [so].[SupplierID]
		Where [so].[EmployeeID] != 1
    END

/*
 * Updated: 2019/05/03
 * By: Richard Carroll
 * Retrieves All Generated SupplierOrders in the supplier order table
*/
print '' print'*** Altering sp_select_all_generated_orders'
GO
Alter Procedure [dbo].[sp_select_all_generated_orders]
As
    BEGIN
        SELECT [so].[SupplierOrderID], [so].[SupplierID], [s].[Name] AS SupplierName, [so].[EmployeeID], [e].[FirstName], [e].[LastName], [so].[Description],
        [so].[DateOrdered], [so].[OrderComplete]
        FROM [SupplierOrder] so INNER JOIN [Employee] e ON [so].[EmployeeID] = [e].[EmployeeID]
		INNER JOIN [Supplier] s ON [s].[SupplierID] = [so].[SupplierID]
		Where [so].[EmployeeID] = 1
    END
GO

/*
 * Created: 2019/05/03
 * By: Richard Carroll
 * Adds a normal employeeID to a generated order, approving it.
*/
print '' print'*** Altering sp_update_generated_order'
GO
Alter PROCEDURE [dbo].[sp_update_generated_order]
(
    @SupplierOrderID     [int],
    @EmployeeID			 [int]
)
AS 
    BEGIN
        Update SupplierOrder
        Set EmployeeID = @EmployeeID
        Where SupplierOrderID = @SupplierOrderID
        And EmployeeID = 1
        Return @@ROWCOUNT
    END
GO
