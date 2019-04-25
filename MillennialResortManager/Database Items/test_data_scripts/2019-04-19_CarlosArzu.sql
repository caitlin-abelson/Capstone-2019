/* Author: Carlos Arzu Created Date: 1/26/19 */
print '' print '***  Creating Special order table'
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
CREATE PROCEDURE [dbo].[sp_retrieve_List_of_EmployeeID]

AS
	BEGIN
		SELECT [EmployeeID]
			
		FROM [dbo].[EmployeeRole]
	    			
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


