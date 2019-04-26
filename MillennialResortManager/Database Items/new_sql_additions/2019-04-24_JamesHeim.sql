USE [MillennialResort_DB]
GO


---------------------
-- Alter Scripts --
---------------------
-- James Heim
-- 2019-04-24
-- GuestID and EmployeeID needs to be nullable.
ALTER TABLE [dbo].[MemberTabLine]
ALTER COLUMN [GuestID] [int] NULL
GO

ALTER TABLE [dbo].[MemberTabLine]
ALTER COLUMN [EmployeeID] [int] NULL
GO

---------------------
-- NEW STUFF --
---------------------
-- James Heim
-- 2019-04-24
-- Activate a Member's Tab where the tab is deactivated.
CREATE PROCEDURE [dbo].[sp_update_set_active_membertab_by_id]
(
	@MemberTabID [int]
)
AS
	BEGIN
		UPDATE [MemberTab]
		SET [Active] = 1
		WHERE MemberTabID = @MemberTabID
		  AND Active = 0
		  
		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- 2019-04-24
-- Delete a Member's Tab where the tab is deactivated.
CREATE PROCEDURE [dbo].[sp_delete_membertab_by_id]
(
	@MemberTabID [int]
)
AS
	BEGIN
		DELETE FROM [MemberTab]
		WHERE MemberTabID = @MemberTabID
		  AND Active = 0
		  
		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- 2019-04-24
-- Insert a TabLine on a Member's Tab.
CREATE PROCEDURE [dbo].[sp_insert_membertabline]
(
	@MemberTabLineID [int] OUTPUT,

	@MemberTabID	[int],
	@OfferingID		[int], 
	@Quantity		[int],
	@Price			[money],
	@EmployeeID		[int],
	@Discount		[decimal],
	@GuestID		[int],
	@DatePurchased	[DATE]
	
)
AS
	BEGIN
		INSERT INTO [MemberTabLine]
				([MemberTabID], [OfferingID], [Quantity], [Price], [EmployeeID],
				 [Discount], [GuestID], [DatePurchased])
			VALUES
				(@MemberTabID, @OfferingID, @Quantity, @Price, @EmployeeID, 
				 @Discount, @GuestID, @DatePurchased)
				 
		SET @MemberTabLineID = SCOPE_IDENTITY()

		RETURN SCOPE_IDENTITY()
	END
GO

-- James Heim
-- 2019-04-24
-- Select the tablines for the specified Tab.
CREATE PROCEDURE [dbo].[sp_select_membertablines]
(
	@MemberTabID [int]
)
AS
	BEGIN
		SELECT [MemberTabLineID], [MemberTabID], [OfferingID], [Quantity], 
				[Price], [EmployeeID], [Discount], [GuestID], [DatePurchased]
		FROM [MemberTabLine]
		WHERE [MemberTabID] = @MemberTabID
	END
GO

-- James Heim
-- 2019-04-24
-- Select the details of a specific tabline.
CREATE PROCEDURE [dbo].[sp_select_membertabline]
(
	@MemberTabLineID [int]
)
AS
	BEGIN
		SELECT [MemberTabLineID], [MemberTabID], [OfferingID], [Quantity], 
				[Price], [EmployeeID], [Discount], [GuestID], [DatePurchased]
		FROM [MemberTabLine]
		WHERE [MemberTabLineID] = @MemberTabLineID
	END
GO

-- James Heim
-- 2019-04-24
-- Delete a TabLine.
CREATE PROCEDURE [dbo].[sp_delete_membertabline]
(
	@MemberTabLineID [int]
)
AS
	BEGIN
		DELETE FROM [MemberTabLine]
		WHERE [MemberTabLineID] = @MemberTabLineID
		
		RETURN @@ROWCOUNT
	END
GO


-- James Heim
-- 2019-04-25
-- Select all MemberTabs, active or otherwise.
CREATE PROCEDURE [dbo].[sp_select_membertabs]
AS
	BEGIN	
		SELECT MemberTabID, MemberID, Active, TotalPrice
		FROM MemberTab
	END
GO

-- James Heim
-- Created 2019-04-25
-- Select all Tabs the member has ever had.
CREATE PROCEDURE [dbo].[sp_select_membertabs_by_member_id]
(
	@MemberID [int]
)
AS
	BEGIN
		SELECT MemberTabID, MemberID, Active, TotalPrice
		FROM MemberTab
		WHERE MemberID = @MemberID
	END
GO

-- James Heim and Jared Greenfield
-- Created 2019-04-26
-- Update the Total on a MemberTab after a new TabLine was inserted.
print 'tr_membertab_membertabline_insert'
GO
CREATE TRIGGER [dbo].[tr_membertab_membertabline_insert]
ON [MemberTabLine]
AFTER INSERT
AS
    BEGIN
        DECLARE @MemberTabID [int]
        DECLARE @TotalPrice  [money]

        SELECT @MemberTabID = [MemberTabID] FROM Inserted
		
		SELECT @TotalPrice  = SUM([Price] * [Quantity])
							  FROM [MemberTabLine]
							  WHERE [MemberTabID] = @MemberTabID 

		UPDATE [MemberTab]
		SET [TotalPrice] = @TotalPrice
		WHERE [MemberTab].[MemberTabID] = @MemberTabID
		

    END
GO

-- James Heim and Jared Greenfield
-- Created 2019-04-26
-- Update the Total on a MemberTab after a TabLine was deleted.
print 'tr_membertab_membertabline_delete'
GO
CREATE TRIGGER [dbo].[tr_membertab_membertabline_delete]
ON [MemberTabLine]
AFTER DELETE
AS
    BEGIN
        DECLARE @MemberTabID [int]
        DECLARE @TotalPrice  [money]

        SELECT @MemberTabID = [MemberTabID] FROM Deleted
		
		SELECT @TotalPrice  = SUM([Price] * [Quantity]) 
							  FROM [MemberTabLine]
							  WHERE [MemberTabID] = @MemberTabID 

		UPDATE [MemberTab]
		SET [TotalPrice] = @TotalPrice
		WHERE [MemberTab].[MemberTabID] = @MemberTabID
		

    END
GO
