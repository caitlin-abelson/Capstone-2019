USE [MillennialResort_DB]
GO
/* Created by Richard Carroll*/
/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print'*** Creating InternalOrder Table'
GO
CREATE Table [dbo].[InternalOrder](
    [InternalOrderID]   [int] IDENTITY(100000, 1) 	  NOT NULL,
    [EmployeeID]        [int]                         NOT NULL,
    [DepartmentID]      [nvarchar](50)                NOT NULL,  
    [Description]       [nvarchar](1000)              NOT NULL,
    [OrderComplete]     [bit]                         NOT NULL,
    [DateOrdered]       [DateTime]                    NOT NULL

    CONSTRAINT [pk_InternalOrderID] Primary Key([InternalOrderID] ASC)
)
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print'*** Creating InternalOrderLine Table'
GO
CREATE Table [dbo].[InternalOrderLine](
    [ItemID]            [int]           NOT NULL,
    [InternalOrderID]   [int]           NOT NULL,
    [OrderQty]          [int]           NOT NULL,
    [QtyReceived]       [int] 

    CONSTRAINT [pk_ItemID_InternalOrderID] Primary Key([ItemID] ASC, [InternalOrderID] ASC)
)

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print'*** Inserting InternalOrder Record'
GO
Insert INTO [InternalOrder]

    ([EmployeeID], [DepartmentID], [Description], [OrderComplete], [DateOrdered])
    VALUES ('100001', 'Events', 'An order for Fruit', 0, GetDate())
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print'*** Inserting InternalOrderLine Records'
GO
INSERT INTO [InternalOrderLine]
    ([ItemID], [InternalOrderID], [OrderQty], [QtyReceived])
    VALUES ('100001', '100000', 300, 300)
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Adding Foreign Key for InternalOrder.DepartmentID'
GO
Alter Table [dbo].[InternalOrder] With Nocheck
    Add Constraint [fk_InternalOrder_DepartmentID] Foreign Key ([DepartmentID])
    References [dbo].[Department]([DepartmentID]) On Update Cascade
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Adding Foreign Key for InternalOrderLine.ItemID'
GO
Alter Table [dbo].[InternalOrderLine] With Nocheck
    Add Constraint [fk_InternalOrderLine_ItemID] Foreign Key ([ItemID]) References [dbo].[Item]([ItemID])
    On Update Cascade
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Adding Foreign Key for InternalOrder.EmployeeID'
GO
Alter Table [dbo].[InternalOrder] With Nocheck 
    ADD Constraint [fk_InternalOrder_EmployeeID] Foreign Key ([EmployeeID])
	REFERENCES [dbo].[Employee]([EmployeeID])
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Adding Foreign Key for InternalOrderLine.InternalOrderID'
GO
Alter Table [dbo].[InternalOrderLine] With Nocheck 
    ADD Constraint [fk_InternalOrderLine_InternalOrderID] Foreign Key ([InternalOrderID])
	REFERENCES [dbo].[InternalOrder]([InternalOrderID])
    On Update Cascade
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Creating sp_retrieve_user_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_user_by_email]
(
		@Email				[nvarchar](250)
)
AS
    Begin
        Select [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID]
        From Employee
        Where [Email] = @Email
    End
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Creating sp_insert_internal_order'
GO
CREATE PROCEDURE [dbo].[sp_insert_internal_order]
    (
        @EmployeeID         [int],
        @DepartmentID       [nvarchar](50),
        @Description        [nvarchar](250),
        @OrderComplete      [bit],
        @DateOrdered        [DateTime]
    )
AS
    BEGIN
        Insert INTO [InternalOrder]

        ([EmployeeID], [DepartmentID], [Description], [OrderComplete], [DateOrdered])
        VALUES (@EmployeeID, @DepartmentID, @Description, @OrderComplete, @DateOrdered)
        Select Scope_Identity()
    END
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Creating sp_insert_internal_order_line'
GO
CREATE Procedure [dbo].[sp_insert_internal_order_line]
    (
        @ItemID             [int],
        @InternalOrderID    [int],
        @OrderQty           [int],
        @QtyReceived        [int]
    )
AS
    BEGIN
        INSERT INTO [InternalOrderLine]
        ([ItemID], [InternalOrderID], [OrderQty], [QtyReceived])
        VALUES (@ItemID, @InternalOrderID, @OrderQty, @QtyReceived)
        RETURN @@ROWCOUNT
    END
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Creating sp_select_all_item_names_and_ids'
GO
Create Procedure [dbo].[sp_select_all_item_names_and_ids]
AS
    Begin   
        Select [Name], [ItemID]
        From [Item]
    End
Go


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Creating sp_select_all_internal_orders'
GO
Create Procedure [dbo].[sp_select_all_internal_orders]
As
    Begin
        Select [InternalOrder].[InternalOrderID], [InternalOrder].[EmployeeID], [Employee].[FirstName], [Employee].[LastName], [InternalOrder].[DepartmentID], [InternalOrder].[Description],
        [InternalOrder].[OrderComplete], [InternalOrder].[DateOrdered]
        From InternalOrder Inner Join Employee On InternalOrder.EmployeeID = Employee.EmployeeID
        Inner Join Department on InternalOrder.DepartmentID = Department.DepartmentID
    END
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print '*** Creating sp_select_order_lines_by_id'
GO
Create Procedure [dbo].[sp_select_order_lines_by_id]
(
    @InternalOrderID    [int]
)
AS
    Begin   
        Select [InternalOrderLine].[ItemID], [Item].[Name], [InternalOrderLine].[OrderQty],
        [InternalOrderLine].[QtyReceived]
        From InternalOrderLine inner Join Item on InternalOrderLine.ItemID = Item.ItemID
        Where InternalOrderID = @InternalOrderID
    End
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
print '' print'*** Creating sp_update_order_status_to_complete'
GO
CREATE Procedure [dbo].[sp_update_order_status_to_complete]
(
    @InternalOrderID    [int],
    @OrderComplete      [bit]
)
AS
    Begin
        Update InternalOrder
        Set OrderComplete = 1
        Where InternalOrderID = @InternalOrderID
        AND OrderComplete = @OrderComplete
    END
GO