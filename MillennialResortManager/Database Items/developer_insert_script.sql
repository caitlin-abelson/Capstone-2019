USE [MillennialResort_DB]
GO
/*********************************************************************/
/* Developers place their test code here to be submitted to database */
/*********************************************************************/

/* Start Phil, submitted 2019-02-18 */

/* CREATE PROCEDURE sp_retrieve_all_events
--DQ'd for error 'SponsorName is not valid field'
AS
	BEGIN
		SELECT 	[EventID],[EventTitle],[Event].[EmployeeID],[Employee].[FirstName],[EventTypeID] AS [EventType],[Description],[EventStartDate],
				[EventEndDate],[KidsAllowed],[NumGuests],[Location],[Sponsored],[Event].[SponsorID],[Sponsor].[SponsorName], [Approved]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
				INNER JOIN [dbo].[Sponsor]
			ON		[Event].[SponsorID] = [Sponsor].[SponsorID]
	END
--GO*/

/* Start Eric Bostwick */

-- Created 2/4/19
-- Updated 2/14/19 to Add Active
GO
CREATE TABLE [dbo].[ItemSupplier] (
	[ItemID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[PrimarySupplier] [bit] NULL,
	[LeadTimeDays] [int] NULL DEFAULT 0,
	[UnitPrice] [money] NULL DEFAULT 0.0,
	[Active] [bit] NOT NULL DEFAULT 1

	CONSTRAINT [pk_ItemID_ItemID] PRIMARY KEY([ItemID] ASC, [SupplierID] ASC)
)

GO

-- Created 2/4/19
--Foreign Keys For ItemSupplier Join Table
ALTER TABLE [dbo].[ItemSupplier] WITH NOCHECK
	ADD CONSTRAINT [fk_ItemSupplier_ItemID] FOREIGN KEY ([ItemID])
	REFERENCES [dbo].[Item]([ItemID])
	ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[ItemSupplier] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierID] FOREIGN KEY ([SupplierID])
	REFERENCES [dbo].[Supplier]([SupplierID])
	ON UPDATE CASCADE
GO

-- Created: 2019-02-04
CREATE PROCEDURE [dbo].[sp_update_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@Active				[bit],

		@OldItemID 			[int],
		@OldSupplierID		[int],
		@OldPrimarySupplier	[bit],
		@OldLeadTimeDays	[int],
		@OldUnitPrice		[money],
		@OldActive			[bit]
	)
AS
BEGIN
		IF(@PrimarySupplier = 1)
			BEGIN
				UPDATE [dbo].[ItemSupplier]
				SET [PrimarySupplier]= 0
				WHERE [ItemID] = @ItemID
			END

		UPDATE [dbo].[ItemSupplier]
		SET [ItemID] = @ItemID,
			[SupplierID] = @SupplierID,
			[PrimarySupplier] = @PrimarySupplier,
			[LeadTimeDays] = @LeadTimeDays,
			[UnitPrice] = @UnitPrice,
			[Active] = @Active
		WHERE
			[ItemID] = @OldItemID AND
			[SupplierID] = @OldSupplierID AND
			[PrimarySupplier] = @OldPrimarySupplier AND
			[LeadTimeDays] = @OldLeadTimeDays AND
			[UnitPrice] = @OldUnitPrice AND
			[Active] = @OldActive
END

GO

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

-- Created: 2019-02-07
CREATE PROCEDURE [dbo].[sp_delete_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		DELETE
		FROM		[ItemSupplier]
		WHERE		[ItemID] = @ItemID AND [SupplierID] = @SupplierID
	END
GO

-- Created: 2019-02-07
CREATE PROCEDURE [dbo].[sp_deactivate_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN

		UPDATE		[ItemSupplier]
		SET [Active] = 0
		WHERE		[ItemID] = @ItemID AND [SupplierID] = @SupplierID
	END
GO

-- Created: 2019-02-07
-- Description: Returns all the suppliers not setup in the itemsupplier table for that item. This is so user doesn't get the option to add a supplier that will create a primary key violation on the item supplier table.
CREATE PROCEDURE [dbo].[sp_select_suppliers_for_itemsupplier_mgmt_by_itemid]
(
	@ItemID [int]
)
AS
	BEGIN
		SELECT		[Supplier].[SupplierID],
					[Supplier].[Name],
					[Supplier].[Address],
					[Supplier].[City],
					[Supplier].[State],
					[Supplier].[PostalCode],
					[Supplier].[Country],
					[Supplier].[PhoneNumber],
					[Supplier].[Email],
					[Supplier].[ContactFirstName],
					[Supplier].[ContactLastName],
					[Supplier].[DateAdded],
					[Supplier].[Description],
					[Supplier].[Active]

		FROM		[Supplier] LEFT OUTER JOIN [ItemSupplier] [isup] ON [isup].[SupplierID] = [Supplier].[SupplierID]
		WHERE		[isup].[Itemid] != @ItemID OR [isup].[Itemid] is Null
	END
GO

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

/* Start Carlos */

CREATE TABLE [dbo].[SupplierOrder]
	(
		[SupplierOrderID]   [int] IDENTITY(100005,1)   NOT NULL, 
		[EmployeeID]  		[int]					   NOT NULL,
		[Description]       [nvarchar](50)             NOT NULL,
		[OrderComplete]     [bit]                      NOT NULL,
        [DateOrdered]       [DateTime]                 NOT NULL,
        [SupplierID]        [int]				   	   NOT NULL,

        CONSTRAINT [pk SupplierOrderID] PRIMARY KEY ([SupplierOrderID] ASC),
	)
GO
CREATE TABLE[dbo].[SupplierOrderLine]
	(

		[ItemID]  			 [int]  			NOT NULL, 
		[SupplierOrderID]    [int]              NOT NULL, 
		[Description]        [nvarchar](1000)   NOT NULL,
		[OrderQty]           [int]              NOT NULL, 
		[QtyReceived] 		 [int]              NULL, 

	)
GO
CREATE PROCEDURE [dbo].[sp_create_supplierOrder]
	(
		@SupplierOrderID    [int],
		@EmployeeID    	    [int],
		@Description    	[nvarchar](50),
		@OrderComplete  	[bit],
		@DateOrdered 		[DateTime],
		@SupplierID			[int]

	)
AS
	BEGIN	
		
		SET IDENTITY_INSERT [dbo].[SupplierOrder] ON
		
		INSERT INTO [dbo].[SupplierOrder]
			([SupplierOrderID], [EmployeeID],  [Description], [OrderComplete], 
			 [DateOrdered], [SupplierID])
		VALUES
			(@SupplierOrderID, @EmployeeID, @Description, @OrderComplete,
			 @DateOrdered, @SupplierID)
				
		SET IDENTITY_INSERT [dbo].[SupplierOrder] OFF
			
		RETURN @@ROWCOUNT	

	END
GO	
CREATE PROCEDURE [dbo].[sp_retrieve_all_supplier_order]
	
AS
	BEGIN
				
		SELECT [SupplierOrderID],[EmployeeID],[SupplierID],[Description],[OrderComplete],
			   [DateOrdered] 
		FROM 	[dbo].[SupplierOrder]
	END	
GO	
CREATE PROCEDURE [dbo].[sp_update_SupplierOrder]
	(
		@SupplierOrderID    	[int],
		
		@EmployeeID  			[int],
		@Description      		[nvarchar](50),
		@OrderComplete          [bit],
		@DateOrdered 	        [DateTime],
		@SupplierID				[int],
		
		@OldEmployeeID  		[int],
		@OldDescription     	[nvarchar](50),
		@OldOrderComplete       [bit],   
		@OldDateOrdered 	    [DateTime], 
		@OldSupplierID			[int]
	)
AS
	BEGIN
		UPDATE		[SupplierOrder]
			SET		[EmployeeID]  		=	@EmployeeID,  
					[Description]      	=	@Description,      
					[OrderComplete]     =	@OrderComplete,                  
					[DateOrdered] 		=	@DateOrdered,
					[SupplierID]        =	@SupplierID
			FROM	[dbo].[SupplierOrder]
			WHERE	[SupplierOrderID]   =   @SupplierOrderID	
			  AND   [EmployeeID]  	    =	@OldEmployeeID  
			  AND   [Description]      	=	@OldDescription     
			  AND	[OrderComplete]     =	@OldOrderComplete 
			  AND	[DateOrdered] 	    =	@OldDateOrdered 
			  AND	[SupplierID]        =	@OldSupplierID
			  
			RETURN @@ROWCOUNT
    END
GO	
CREATE PROCEDURE [dbo].[sp_deactivate_SupplierOrder]
	(
		@SupplierOrderID		[int]	
	)
AS
	BEGIN
		UPDATE  [SupplierOrder]
		SET 	[OrderComplete] = 0
		WHERE   [SupplierOrderID] = @SupplierOrderID
		
		RETURN @@ROWCOUNT
	END
GO	
CREATE PROCEDURE [sp_read_all_internal_orders]
AS
	BEGIN
		SELECT *
		FROM SupplierOrder
	END
GO
CREATE PROCEDURE [dbo].[sp_retrieve_List_of_EmployeeID]
AS
	BEGIN
		SELECT [EmployeeID]			
		FROM [dbo].[EmployeeRole]	    			
		RETURN @@ROWCOUNT		
	END
GO
CREATE PROCEDURE [dbo].[sp_count_supplier_order]
	
AS
	BEGIN
		SELECT COUNT([SupplierOrderID])
		FROM [dbo].[SupplierOrder]
	END
GO

/* Start Dani */

-- Created: 2019-02-20
CREATE TABLE [dbo].[ResortPropertyType](
	[ResortPropertyTypeID]	[nvarchar](20) NOT NULL,

	CONSTRAINT[pk_ResortPropertyTypeID] PRIMARY KEY([ResortPropertyTypeID] ASC)
)
GO

-- Created: 2019-02-20
CREATE TABLE [dbo].[ResortProperty](
	[ResortPropertyID]		[int] IDENTITY(100000, 1) 	NOT NULL,
	[ResortPropertyTypeID]	[nvarchar](20) 				NOT NULL,

	CONSTRAINT[pk_ResortPropertyID] PRIMARY KEY ([ResortPropertyID] ASC),
	CONSTRAINT[fk_ResortPropertyTypeID] FOREIGN KEY ([ResortPropertyTypeID])
		REFERENCES [dbo].[ResortPropertyType]([ResortPropertyTypeID])
		ON UPDATE CASCADE
)
GO

-- Created: 2019-02-18
CREATE TABLE [dbo].[BuildingStatus](
	[BuildingStatusID]	[nvarchar](25)		NOT NULL,
	[Description]		[nvarchar](1000)	NOT NULL,

	CONSTRAINT[pk_BuildingStatusID] PRIMARY KEY([BuildingStatusID] ASC)
)
GO

-- Created: 2019-01-22
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Changed length for Description, added nulls to BuildingName, Address, & Description added BuildingSatusID field
-- Update 2019-02-20 Author: Dani
-- Update 2019-02-20 Desc: Removed Active field, added ResortPropertyID field
ALTER TABLE [dbo].[Building]
	add [BuildingStatusID]	[nvarchar](25)		NOT NULL
GO
ALTER TABLE [dbo].[Building]
	add [ResortPropertyID]	[int]				NOT NULL
GO
ALTER TABLE [dbo].[Building] WITH NOCHECK
	add CONSTRAINT[fk_BuildingStatusID_Building] FOREIGN KEY ([BuildingStatusID])
		REFERENCES [dbo].[BuildingStatus]([BuildingStatusID]) ON UPDATE CASCADE
GO

-- Created: 2019-02-20
ALTER TABLE [dbo].[Building] WITH NOCHECK
	ADD CONSTRAINT [fk_ResortPropertyID_Building] FOREIGN KEY ([ResortPropertyID])
	REFERENCES [dbo].[ResortProperty]([ResortPropertyID])
	ON UPDATE CASCADE
GO

-- Created: 2019-02-20
CREATE PROCEDURE [dbo].[sp_insert_resortproperty]
	(
		@ResortPropertyTypeID	[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			(@ResortPropertyTypeID)

		SELECT SCOPE_IDENTITY()
	END
GO

DROP PROCEDURE [dbo].[sp_insert_building]
GO
-- Created: 2019-01-22
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Changed length for Description, added BuildingStatusID parameter and field
-- Update 2019-02-20 Author: Dani
-- Update 2019-02-20 Desc: Added ResortPropertyID parameter and field
-- Update 2019-02-22 Author: Jared Greenfield and Jim Glasgow
-- Update 2019-02-22 Desc: Syntax update for the ResortProperty field and removed param
CREATE PROCEDURE [dbo].[sp_insert_building]
	(
		@BuildingID			[nvarchar](50),
		@BuildingName		[nvarchar](150),
		@Address			[nvarchar](150),
		@Description		[nvarchar](1000),
		@BuildingStatusID	[nvarchar](25)
	)
AS
	BEGIN

		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Building')

		SELECT @@IDENTITY AS [@@IDENTITY]

		INSERT INTO [Building]
			([BuildingID],[BuildingName], [Address], [Description], [BuildingStatusID], [ResortPropertyID])
		VALUES
			(@BuildingID, @BuildingName, @Address, @Description, @BuildingStatusID, @@IDENTITY)

		RETURN @@ROWCOUNT
	END
GO

-- Created: 2019-01-30
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Added BuildingStatusID field
-- Update 2019-02-20 Author: Dani
-- Update 2019-02-20 Desc: Removed Active field
CREATE PROCEDURE sp_select_building_by_id
	(
		@BuildingID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT	[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID]
		FROM	[Building]
		WHERE	[BuildingID] = @BuildingID
	END
GO

-- Created: 2019-02-02
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Added BuildingSatusID field
-- Update 2019-02-20 Author: Dani
-- Update 2019-02-20 Desc: Removed Active field, removed "Order by Active"
CREATE PROCEDURE sp_select_building_by_keyword_in_building_name
	(
		@Keyword		[nvarchar](150)
	)
AS
	BEGIN
		SELECT		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID]
		FROM		[Building]
		WHERE		[BuildingName] LIKE '%' + @Keyword + '%'
		ORDER BY	[BuildingID]
	END
GO

-- Created: 2019-02-19
-- Update 2019-02-20 Author: Dani
-- Update 2019-02-20 Desc: Removed Active field, removed "Order by Active"
CREATE PROCEDURE sp_select_building_by_buildingstatusid
	(
		@BuildingStatusID		[nvarchar](25)
	)
AS
	BEGIN
		SELECT		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID]
		FROM		[Building]
		WHERE		[BuildingStatusID] = @BuildingStatusID
		ORDER BY	[BuildingID]
	END
GO

-- Created: 2019-01-30
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Added BuildingStatusID field
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Removed Active field, removed "Order by Active"
CREATE PROCEDURE sp_select_buildings
AS
	BEGIN
		SELECT 		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID]
		FROM		[Building]
		ORDER BY	[BuildingID]
	END
GO

-- Created: 2019-02-20
CREATE PROCEDURE sp_select_all_statusids
AS
	BEGIN
		SELECT 		[BuildingStatusID]
		FROM		[BuildingStatus]
		ORDER BY	[BuildingStatusID]
	END
GO

-- Created: 2019-01-30
-- Update 2019-02-18 Author: Dani
-- Update 2019-02-18 Desc: Changed length of Description parameters, added BuildingStatusID field and parameters
CREATE PROCEDURE sp_update_building
	(
		@BuildingID				[nvarchar](50),

		@NewBuildingName 		[nvarchar](150),
		@NewAddress				[nvarchar](150),
		@NewDescription			[nvarchar](1000),
		@NewBuildingStatusID	[nvarchar](25),

		@OldBuildingName		[nvarchar](150),
		@OldAddress				[nvarchar](150),
		@OldDescription			[nvarchar](250),
		@OldBuildingStatusID	[nvarchar](25)
	)
AS
	BEGIN
		UPDATE	[Building]
			SET	[BuildingName] 		= @NewBuildingName,
				[Address]			= @NewAddress,
				[Description]		= @NewDescription,
				[BuildingStatusID]	= @NewBuildingStatusID
		WHERE	[BuildingID] 		= @BuildingID
			AND [BuildingName] 		= @OldBuildingName
			AND [Address]			= @OldAddress
			AND	[Description]		= @OldDescription
			AND	[BuildingStatusID]	= @OldBuildingStatusID

		Return @@ROWCOUNT
	END
GO


--print '' print '*** TO DO: Create Inspection Table ***'
--print '' print '*** TO DO: Create sp to find all maintance tickets for buildings ResortPropertyID ***'
--print '' print '*** TO DO: Create sp to find all maintance tickets for each building by rooms ResortPropertyID ***'
--print '' print '*** TO DO: Create sp to find all inspection records for buildings ResortPropertyID ***'

