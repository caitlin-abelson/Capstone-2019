/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '***  Creating Special order table'
GO
CREATE TABLE [dbo].[SpecialOrder]
	(

    	[SpecialOrderID]    [int] IDENTITY(200004,1)   NOT NULL, 
		[EmployeeID]  		[int]					   NOT NULL,
		[Description]       [nvarchar](1000)           NOT NULL,
		[DateOrdered]       [DateTime]                 NOT NULL,
		[Supplier]          [nvarchar](1000)	   	   NOT NULL,
		[Authorized]        [nvarchar](1000)           NULL DEFAULT '',

CONSTRAINT [pk SpecialOrderID] PRIMARY KEY ([SpecialOrderID] ASC),
	)
GO

print '' print '*** Inserting testing Special Order records '
GO
SET IDENTITY_INSERT [dbo].[SpecialOrder] ON

INSERT INTO [dbo].[SpecialOrder]
		([SpecialOrderID], [EmployeeID], [Description],[DateOrdered], [Supplier],[Authorized])
	VALUES
		(2000001, 100001, 'Full Synthetic Engine Oil','2/8/2019','Megaproducts','Erater'),
		(2000002, 100002, 'Full Synthetic Engine Oil', '2/6/2011','Sam electrics',''),
		(2000003, 100003, 'Synthectic blend Engine Oil','6/8/2012','Slantic','')
		
SET IDENTITY_INSERT [dbo].[SpecialOrder] OFF
			
GO

/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print'''Creating Special Order line Table'
GO
CREATE TABLE[dbo].[SpecialOrderLine]
	(

		[NameID]  			 [nvarchar](1000)	NOT NULL, 
		[SpecialOrderID]     [int]              NOT NULL, 
		[Description]        [nvarchar](1000)   NOT NULL,
		[OrderQty]           [int]              NOT NULL, 
		[QtyReceived] 		 [int]              NULL, 

	)
GO

print '' print '*** Adding Foreign Key for InternalOrderLine.ItemID'
GO
Alter Table [dbo].[SpecialOrderLine] With Nocheck
    Add Constraint [fk_SpecialOrderLine_SpecialOrderID] Foreign Key ([SpecialOrderID]) References [dbo].[SpecialOrder]([SpecialOrderID])
    On Update Cascade
GO


print '' print'*** Inserting Special Order line Table Records'
GO


INSERT INTO [dbo].[SpecialOrderLine]
    ([NameID], [SpecialOrderID], [Description], [OrderQty], [QtyReceived])
    VALUES
        ('Tomato Soup', 2000001, 'Tomato soup with green pepper',1, 1),
        ('Paper', 2000002, 'White paper', 5, 0),
        ('Pencil', 2000003, 'Pencil 2B for designer', 6, 6)
    
GO


/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '''Creating sp_create_SpecialOrder'
GO
CREATE PROCEDURE [dbo].[sp_create_SpecialOrder]
	(
		
		@EmployeeID    	    [int],
		@Description    	[nvarchar](1000),
		@DateOrdered 		[DateTime],
		@Supplier			[nvarchar](1000)

	)
AS
	BEGIN	
		
		INSERT INTO [dbo].[SpecialOrder]
			( [EmployeeID],  [Description],
			 [DateOrdered], [Supplier])
		VALUES
			( @EmployeeID, @Description, 
			 @DateOrdered, @Supplier)
				
		RETURN @@ROWCOUNT	

	END
GO	

/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '''Creating sp_create_SpecialOrderLine'
GO
CREATE PROCEDURE [dbo].[sp_create_SpecialOrderLine]
	(
		@NameID   	        [nvarchar](1000),
		@SpecialOrderID     [int],
		@Description    	[nvarchar](1000),
		@OrderQty  	        [int],
		@QtyReceived 		[int]
			
	)
AS
	BEGIN	
					
		INSERT INTO [dbo].[SpecialOrderLine]
			( [NameID], [SpecialOrderID], [Description], [OrderQty], 
			 [QtyReceived])
		VALUES
			(@NameID, @SpecialOrderID, @Description, @OrderQty,
			 @QtyReceived)
		
		RETURN @@ROWCOUNT	

	END
GO

/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '''Creating sp_retrieve_all_SpecialOrder'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_Special_order]
	
AS
	BEGIN
				
		SELECT       *
		FROM 	   [SpecialOrder] 
	END	
GO	

/* Author: Carlos Arzu Created Date: 2/06/19 */
print '' print ''' Creating sp_update_SpecialOrder'

GO
CREATE PROCEDURE [dbo].[sp_update_SpecialOrder]
	(
		@SpecialOrderID    	[int],
		
		@EmployeeID  			[int],
		@Description      		[nvarchar](1000),
		@Supplier				[nvarchar](1000),
		
		@OldEmployeeID  		[int],
		@OldDescription     	[nvarchar](1000),
		@OldSupplier			[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE		[SpecialOrder]
			SET		[EmployeeID]  		=	@EmployeeID,  
					[Description]      	=	@Description,      
					[Supplier]        =	@Supplier
			FROM	[dbo].[SpecialOrder]
			WHERE	[SpecialOrderID]   =   @SpecialOrderID	
			  AND   [EmployeeID]  	    =	@OldEmployeeID  
			  AND   [Description]      	=	@OldDescription 
              AND	[Supplier]        =	@OldSupplier
			  
			RETURN @@ROWCOUNT
    END
GO	

/* Author: Carlos Arzu Created Date: 4/05/19 */
print '' print ''' Creating sp_update_SpecialOrderLine'

GO
CREATE PROCEDURE [dbo].[sp_update_SpecialOrderLine]
	(
		@SpecialOrderID    	[int],
		
		@NameID  		    	[nvarchar](1000),
		@Description      		[nvarchar](1000),
		@OrderQty               [int],
		@QtyReceived			[int],
		
		@OldNameID  	    	[nvarchar](1000),
		@OldDescription     	[nvarchar](1000),
		@OldOrderQty            [int],   
		@OldQtyReceived			[int]
	)
AS
	BEGIN
		UPDATE		[SpecialOrderLine]
			SET		[NameID]  		    =	@NameID,  
					[Description]      	=	@Description,      
					[OrderQty]          =	@OrderQty,                  
					[QtyReceived]       =	@QtyReceived
			FROM	[dbo].[SpecialOrderLine]
			WHERE	[SpecialOrderID]    =   @SpecialOrderID	
			  AND   [NameID]  	        =	@OldNameID 
			  AND   [Description]      	=	@OldDescription 
              AND   [OrderQty]          =	@OldOrderQty		  
			  AND	[QtyReceived]        =	@OldQtyReceived
			  
			RETURN @@ROWCOUNT
    END
GO	

print '' print '''Creating sp_deactivate_SpecialOrder'
/*
GO
CREATE PROCEDURE [dbo].[sp_deactivate_SpecialOrder]
	(
		@SpecialOrderID		[int]	
	)
AS
	BEGIN
		UPDATE  [SpecialOrder]
		WHERE   [SpecialOrderID] = @SpecialOrderID
		
		RETURN @@ROWCOUNT
	END
GO	*/

/* Author: Carlos Arzu Created Date: 2/08/19 */
print '' print''' Creating sp_readall_Special_orders'
GO
CREATE PROCEDURE [sp_read_Special_orders_by_ID]
	(
		@SpecialOrderID		[int]	
	)
AS
	BEGIN
		SELECT [SpecialOrderID],[EmployeeID],[Description],
			   [DateOrdered], [Supplier] 
		FROM   SpecialOrder
		WHERE  [SpecialOrderID] = @SpecialOrderID
	END
GO

/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '*** Creating sp_retrieve_List_of_EmployeeID'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_List_of_EmployeeID]

AS
	BEGIN
		SELECT [EmployeeID]
			
		FROM [dbo].[EmployeeRole]
	    			
		RETURN @@ROWCOUNT
		
	END
GO

/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '*** Creating sp_count_Special_order'
GO
CREATE PROCEDURE [dbo].[sp_count_Special_order]
	
AS
	BEGIN
		SELECT COUNT([SpecialOrderID])
		FROM [dbo].[SpecialOrder]
	END
GO

/* Author: Carlos Arzu Created Date: 2/08/19 */
print '' print''' sp_retrieve_List_of_SpecialOrderLine_by_SpecialOrderID'
GO
CREATE PROCEDURE [sp_retrieve_List_of_SpecialOrderLine_by_SpecialOrderID]
	(
		@SpecialOrderID		[int]	
	)
AS
	BEGIN
		SELECT  [NameID],[SpecialOrderID], [Description], [OrderQty], 
		        [QtyReceived]
		FROM   SpecialOrderLine
		WHERE  [SpecialOrderID] = @SpecialOrderID
	END
GO

/* Author: Carlos Arzu Created Date: 2/18/19 */
print '' print '*** Creating sp_search_Special_orders'
GO

CREATE PROCEDURE [dbo].[sp_search_Special_orders]
	(
		@SearchTerm		[nvarchar](100)
	)
AS
	BEGIN
		SELECT [SpecialOrderID],[EmployeeID],[Description],
			   [DateOrdered], [Supplier] 
		FROM  [SpecialOrder]
		WHERE [Description] LIKE '%' + @SearchTerm + '%'
	END
GO

/* Author: Carlos Arzu Created Date: 4/6/19 */
print '' print '*** Creating sp_delete_Item_from_SpecialOrder'
GO

CREATE PROCEDURE [dbo].[sp_delete_Item_from_SpecialOrder]
	(
		@SpecialOrderID  [int],
		@NameID	     	 [nvarchar](100)
	)
AS
	BEGIN
		DELETE
		FROM  [SpecialOrderLine]
		WHERE [SpecialOrderID] = @SpecialOrderID 
		AND [NameID] = @NameID
	END
GO

/* Author: Carlos Arzu Created Date: 4/10/19 */
print '' print''' sp_retrieve_Last_SpecialOrderID_created'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_last_specialOrderID_created]
	(
		@EmployeeID    	    [int],
		@Description    	[nvarchar](1000),
		@DateOrdered 		[DateTime],
		@Supplier			[nvarchar](1000)
	)
AS
	BEGIN
		SELECT    [SpecialOrderID] 
		FROM      [SpecialOrder]
		WHERE     [EmployeeID]     = @EmployeeID
			  AND [Description]    = @Description
			  AND [DateOrdered]    = @DateOrdered
			  AND [Supplier]     = @Supplier
	END
GO

/* Author: Carlos Arzu Created Date: 4/6/19 */
print '' print '*** Creating sp_update_AuthenticatedBy'
GO

CREATE PROCEDURE [dbo].[sp_update_AuthenticatedBy]
	(
		@SpecialOrderID  [int],
		@Authorized    	 [nvarchar](100)
	)
AS
	BEGIN
		UPDATE    [SpecialOrder]
			SET   [Authorized] = @Authorized 
			FROM  [SpecialOrder]
			WHERE [SpecialOrderID] = @SpecialOrderID 
			
			RETURN @@ROWCOUNT
	END
GO

