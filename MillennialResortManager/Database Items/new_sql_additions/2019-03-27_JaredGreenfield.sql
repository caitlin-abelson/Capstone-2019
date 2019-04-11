USE [MillennialResort_DB]
GO
/*
-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Room Status Table
CREATE TABLE [dbo].[RoomStatus]
	(
		[RoomStatusID][nvarchar](25) NOT NULL,
		[Description][nvarchar](1000) NOT NULL
	CONSTRAINT [pk_RoomStatusID] PRIMARY KEY CLUSTERED 
(
	[RoomStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Updates for room
ALTER TABLE [Room]
	ADD [RoomStatusID][nvarchar](25), 
		[OfferingID] [int] NULL
GO

ALTER TABLE [dbo].[Room]  WITH NOCHECK ADD  CONSTRAINT [fk_Room_RoomStatusID] FOREIGN KEY([RoomStatusID])
REFERENCES [dbo].[RoomStatus] ([RoomStatusID])
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[Room] ADD  DEFAULT (('UNASSIGNED')) FOR [RoomStatusID]
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [fk_Room_RoomStatusID]
GO

ALTER TABLE [dbo].[Room]  WITH NOCHECK ADD  CONSTRAINT [fk_Room_OfferingID] FOREIGN KEY([OfferingID])
REFERENCES [dbo].[Offering] ([OfferingID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [fk_Room_OfferingID]
GO
*/
-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Default data for RoomStatus constraint
INSERT INTO [RoomStatus]
(
	[RoomStatusID], [Description]
)
VALUES
	('UNASSIGNED', "The status is currently unknown after an update of status values.")
GO


-- Stored Procedures

-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Retrieve View Model of all Offerings
print '' print'Creating sp_select_all_offeringvms'
GO
CREATE PROCEDURE [sp_select_all_offeringvms]
AS
-- Retrieve all Events
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   [Event].[EventTitle] AS 'OfferingName'
FROM [Offering]
INNER JOIN [Event] ON [Event].[OfferingID] = [Offering].[OfferingID]
UNION
-- Retrieve all Items
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   [Item].[Name] AS 'OfferingName'
FROM [Offering]
INNER JOIN [Item] ON [Item].[OfferingID] = [Offering].[OfferingID]
UNION
-- Retrieve all Services
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   [ServiceComponent].[ServiceComponentID] AS 'OfferingName'
FROM [Offering]
INNER JOIN [ServiceComponent] ON [ServiceComponent].[OfferingID] = [Offering].[OfferingID]
UNION
-- Retrieve all Rooms
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   CONCAT([Room].[BuildingID], ' ', [Room].[RoomNumber]) AS 'OfferingName'
FROM [Offering]
INNER JOIN [Room] ON [Room].[OfferingID] = [Offering].[OfferingID]
ORDER BY [OfferingTypeID] ASC
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Retrieve all offering types
print '' print'Creating sp_select_all_offeringtypes'
GO
CREATE PROCEDURE [sp_select_all_offeringtypes]
AS
SELECT [OfferingTypeID]
FROM [OfferingType]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-28
-- Delete an Offering
print '' print'Creating sp_delete_offering'
GO
CREATE PROCEDURE [sp_delete_offering]
(
	@OfferingID [int]
)
AS
DELETE FROM [Offering]
WHERE [OfferingID] = @OfferingID
RETURN @@ROWCOUNT
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-28
-- Deactivate an offering
print '' print'Creating sp_deactivate_offering'
GO
CREATE PROCEDURE [sp_deactivate_offering]
(
	@OfferingID [int]
)
AS
UPDATE [Offering]
SET [Active] = 0
WHERE [OfferingID] = @OfferingID
RETURN @@ROWCOUNT
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-28
-- Reactivate an offering
print '' print'Creating sp_reactivate_offering'
GO
CREATE PROCEDURE [sp_reactivate_offering]
(
	@OfferingID [int]
)
AS
UPDATE [Offering]
SET [Active] = 1
WHERE [OfferingID] = @OfferingID
RETURN @@ROWCOUNT
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Select an item/event/room/service based on their Offering ID and TYPE
print '' print'Creating sp_select_offeringsubitem_by_idandtype'
GO
CREATE PROCEDURE [sp_select_offeringsubitemid_by_idandtype]
(
	@OfferingID [int],
	@OfferingType [nvarchar](15)
)
AS
	IF @OfferingType = 'Room'
	BEGIN 
		SELECT 
		[RoomID],
		[BuildingID],
		[RoomNumber],
		[RoomTypeID], 
		[Description],
		[Capacity],
		[RoomStatusID],
		[ResortPropertyID]		
		FROM [Room]
		WHERE @OfferingID = [OfferingID]
	END
	ELSE
	BEGIN
		IF @OfferingType = 'Service'
		BEGIN
			SELECT 'Service'
		END
		ELSE
		BEGIN
			IF @OfferingType = 'Event'
			BEGIN
				SELECT 
				[EventID],
				[EventTypeID],
				[EventStartDate],
				[NumGuests],
				[SeatsRemaining],
				[PublicEvent],
				[Description],
				[KidsAllowed],
				[Location],
				[EventEndDate],
				[EventTitle],
				[Sponsored],
				[EmployeeID],
				[Approved],
				[Cancelled]
				FROM [Event]
				WHERE OfferingID = [OfferingID]
			END
			ELSE
			BEGIN
				IF @OfferingType = 'Item'
				BEGIN
					SELECT 
					[ItemID],
					[ItemTypeID],
					[RecipeID],
					[CustomerPurchasable],
					[Description],
					[OnHandQty],
					[Name],
					[ReOrderQty],
					[DateActive],
					[Active]
					FROM [Item]
					WHERE @OfferingID = [OfferingID]
				END
			END
		END
	END
GO
	
-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Deletes an item
print '' print'Creating sp_delete_item'
GO
CREATE PROCEDURE [sp_delete_item]
(
	@ItemID [int]
)
AS
DELETE FROM [Item] 
WHERE @ItemID = [ItemID]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Deactivate an item
print '' print'Creating sp_deactivate_item'
GO
CREATE PROCEDURE [sp_deactivate_item]
(
	@ItemID [int]
)
AS
UPDATE [Item] 
SET [Active] = 0
WHERE @ItemID = [ItemID]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Update an item
print '' print'Creating sp_update_item'
GO
CREATE PROCEDURE [sp_update_item]
(
	@ItemID	[int],	
	
	@OldOfferingID	[int],	
	@OldItemTypeID	[nvarchar]	(15),
	@OldRecipeID	[int],	
	@OldCustomerPurchasable	[bit],	
	@OldDescription	[nvarchar]	(1000),
	@OldOnHandQty	[int],	
	@OldName	[nvarchar]	(50),
	@OldReOrderQty	[int],	
	@OldActive	[bit],
	
	@NewOfferingID	[int],	
	@NewItemTypeID	[nvarchar]	(15),
	@NewRecipeID	[int],	
	@NewCustomerPurchasable	[bit],	
	@NewDescription	[nvarchar]	(1000),
	@NewOnHandQty	[int],	
	@NewName	[nvarchar]	(50),
	@NewReOrderQty	[int],	
	@NewActive	[bit]
)
AS
UPDATE [Item]
SET 
	[OfferingID] = @NewOfferingID,	
	[ItemTypeID] = @NewItemTypeID,
	[RecipeID] = @NewRecipeID,	
	[CustomerPurchasable] = @NewCustomerPurchasable,	
	[Description] = @NewDescription,
	[OnHandQty] = @NewOnHandQty,	
	[Name] = @NewName,
	[ReOrderQty] = @NewReOrderQty,	
	[Active] = @NewActive
	
	WHERE 
	[ItemID] = @ItemID AND
	[OfferingID] = @OldOfferingID OR [OfferingID] IS NULL AND
	[ItemTypeID] = @OldItemTypeID AND
	[RecipeID] = @OldRecipeID OR [RecipeID] IS NULL AND	
	[CustomerPurchasable] = @OldCustomerPurchasable AND	
	[Description] = @OldDescription OR [Description] IS NULL AND
	[OnHandQty] = @OldOnHandQty AND	
	[Name] = @OldName AND
	[ReOrderQty] = @OldReOrderQty AND	
	[Active] = @OldActive
	RETURN @@ROWCOUNT
GO
-- Updated By: Jared Greenfield
-- Update Date: 2019/04/04
-- Added Offering support.
ALTER PROCEDURE	[dbo].[sp_insert_item]
(
	@ItemTypeID nvarchar(15),
	@Description nvarchar(1000),
	@OnHandQty int,
	@Name nvarchar(50),
	@ReorderQty int,
	@CustomerPurchasable [bit],
	@OfferingID [int],
	@RecipeID [int]
)
AS
BEGIN

	INSERT INTO [Item]
	(
	 [ItemTypeID],
	 [Description],
	 [RecipeID],
	 [OnHandQty],
	 [Name],
	 [ReorderQty],
	 [OfferingID],
	 [CustomerPurchasable])
VALUES
	(@ItemTypeID,
	 @Description,
	 @RecipeID,
	 @OnHandQty,
	 @Name,
	 @ReorderQty,
	 @OfferingID,
	 @CustomerPurchasable)


			  RETURN SCOPE_IDENTITY()
END
GO
ALTER PROCEDURE [dbo].[sp_select_all_items]
AS
	BEGIN
	SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name], [ReOrderQty], [DateActive], [Active], [OfferingID], [CustomerPurchasable], [RecipeID]
	FROM	[Item]
END

GO

ALTER PROCEDURE [dbo].[sp_select_all_active_items]
AS
	BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name], [ReorderQty], [DateActive], [Active], [CustomerPurchasable], [RecipeID], [OfferingID]
		FROM [Item]
		WHERE [Active] = 1
	END


GO

ALTER PROCEDURE [dbo].[sp_select_all_deactivated_items]
AS
	BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name], [ReorderQty], [DateActive], [Active], [CustomerPurchasable], [RecipeID], [OfferingID]
		FROM [Item]
		WHERE [Active] = 0
	END


GO