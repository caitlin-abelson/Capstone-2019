SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
GO

/*
 * Created: 2019/04/25
 * By: Richard Carroll
 * Triggers when an InternalOrder is made. 
 * Checks for Items that we're low on, and aggregates them into a result set, 
 * the result set is then used to Create SupplierOrders and the lines associated
 * with them.
*/
print '' print'*** Creating trigger generate_supplier_order'
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
               inner join ItemSupplier on InternalOrderLine.ItemID = ItemSupplier.ItemID
               Where ItemSupplier.PrimarySupplier = 1 AND (Item.OnHandQty - InternalOrderLine.OrderQty <= (Item.ReorderQty / 4))
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
		Where [so].[EmployeeID] Is not null
    END
GO

/*
 * Created: 2019/04/26
 * By: Richard Carroll
 * Retrieves All Generated SupplierOrders in the supplier order table
*/
print '' print'*** Creating sp_select_all_generated_orders'
GO
CREATE Procedure [dbo].[sp_select_all_generated_orders]
As
    BEGIN
        SELECT [so].[SupplierOrderID], [so].[SupplierID], [s].[Name] AS SupplierName, [so].[EmployeeID], [e].[FirstName], [e].[LastName], [so].[Description],
        [so].[DateOrdered], [so].[OrderComplete]
        FROM [SupplierOrder] so INNER JOIN [Employee] e ON [so].[EmployeeID] = [e].[EmployeeID]
		INNER JOIN [Supplier] s ON [s].[SupplierID] = [so].[SupplierID]
		Where [so].[EmployeeID] Is null
    END
GO

/*
 * Created: 2019/04/26
 * By: Richard Carroll
 * Adds an employeeID to a generated order, approving it.
*/
print '' print'*** Creating sp_update_generated_order'
GO
CREATE PROCEDURE [dbo].[sp_update_generated_order]
(
    @SupplierOrderID     [int],
    @EmployeeID			 [int]
)
AS 
    BEGIN
        Update SupplierOrder
        Set EmployeeID = @EmployeeID
        Where SupplierOrderID = @SupplierOrderID
        And EmployeeID is Null
        Return @@ROWCOUNT
    END
GO
