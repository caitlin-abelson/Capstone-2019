USE [MillennialResort_DB]

print '' print '*** Creating PickSheet Table'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO
CREATE TABLE [dbo].[PickSheet] (
	[PickSheetID]			[nvarchar](25)                NOT NULL,
	[PickSheetCreatedBy]	[int]						  NOT NULL,
	[CreateDate]			[DateTime] DEFAULT getdate()  NOT NULL,
	[PickCompletedBy]		[int]						  NULL,	
	[PickCompletedDate]		[DateTime]					  NULL,
	[PickDeliveredBy]		[int]						  NULL,
	[PickDeliveryDate]		[DateTime]					  NULL,
	[UpdateDate]			[DateTime]				      NULL,
	[NumberOfOrders]		[int]		DEFAULT 0		  NULL,
	[PickSheetNumber]		[int] DEFAULT 100000		  NOT NULL					  				
	
	CONSTRAINT [pk_PickSheet_PickSheetID] PRIMARY KEY([PickSheetID] ASC)
)

GO

print ''print '*** Creating trigger tr_PickSheetIncrement '
-- Author:		Eric Bostwick
-- Create date: 4/6/2019
-- Description:	Increments the PickSheetNumber Field in the PickSheet Table
-- =============================================
GO
CREATE TRIGGER tr_PickSheetIncrement
   ON  PickSheet 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;
	DECLARE @PickSheetNumber [int]
	DECLARE @PickSheetID [nvarchar](50)
	
	--Get the Picksheet ID of the record we just inserted
	SET @PickSheetID = (Select PickSheetID From inserted)
	--Get The Highest Picksheet Number from the table
	SET @PickSheetNumber = (SELECT MAX(PickSheetNumber) From PickSheet) + 1

	--If this is the first record Seed it with 100000
	SET @PickSheetNumber = ISNULL(@PickSheetNumber, 100000)	

	--Set the New picksheetNumber Value in the PickSheet table
	UPDATE PickSheet SET PickSheetNumber = @PickSheetNumber WHERE PickSheetID = @PickSheetID
	
END
GO

print '' print '*** Creating tmpPickSheet Table'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO
CREATE TABLE [dbo].[tmpPickTable] (
	[PickSheetID]			[nvarchar](25)                NOT NULL,
	[InternalOrderID]   	[nvarchar](25) 				  NOT NULL,
	[ItemID]				[int]						  NOT NULL,
	[PickedBy]				[int]						  NOT NULL,
	[PickDateTime]			[DateTime] DEFAULT getdate()  NOT NULL
	
	CONSTRAINT [pk_TmpPickSheet_PickSheetID] PRIMARY KEY([PickSheetID] ASC, [InternalOrderID] ASC, [ItemID] ASC)
)

GO

--print ''print '**** Add PickSheet Column to InternalOrderTable'
----Eric Bostwick
----Created 3/26/19
----Updated 

print '' print'*** Creating InternalOrder Table'
----Eric Bostwick
----Created 3/26/19
----Updated 
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

print '' print'*** Creating InternalOrderLine Table'
----Eric Bostwick
----Created 3/26/19
----Updated 
GO
CREATE Table [dbo].[InternalOrderLine](
    [ItemID]            [int]								NOT NULL,
    [InternalOrderID]   [int]								NOT NULL,
    [OrderQty]          [int]								NOT NULL,
    [QtyReceived]       [int] 	DEFAULT 0					NOT NULL,
	[PickSheetID]		[nvarchar](25) 						NULL,
	[OrderReceivedDate] [DateTime]							NULL,
	[PickCompleteDate]	[DateTime]							NULL,
	[DeliveryDate]		[DateTime]						    NULL,
	[OrderStatus]		[int] DEFAULT 1						NOT NULL,
	[OutOfStock]		[bit] DEFAULT 0						NOT NULL
    
    CONSTRAINT [pk_ItemID_InternalOrderID] Primary Key([ItemID] ASC, [InternalOrderID] ASC)
)

print '' print '*** Adding Foreign Key for InternalOrder.DepartmentID'
GO
Alter Table [dbo].[InternalOrder] With Nocheck
    Add Constraint [fk_InternalOrder_DepartmentID] Foreign Key ([DepartmentID])
    References [dbo].[Department]([DepartmentID]) On Update Cascade
GO


print '' print '*** Adding Foreign Key for InternalOrderLine.ItemID'
GO
Alter Table [dbo].[InternalOrderLine] With Nocheck
    Add Constraint [fk_InternalOrderLine_ItemID] Foreign Key ([ItemID]) References [dbo].[Item]([ItemID])
    On Update Cascade
GO


print '' print '*** Adding Foreign Key for InternalOrder.EmployeeID'
GO
Alter Table [dbo].[InternalOrder] With Nocheck 
    ADD Constraint [fk_InternalOrder_EmployeeID] Foreign Key ([EmployeeID])
	REFERENCES [dbo].[Employee]([EmployeeID])
GO

print '' print '*** Adding Foreign Key for InternalOrderLine.InternalOrderID'
GO
Alter Table [dbo].[InternalOrderLine] With Nocheck 
    ADD Constraint [fk_InternalOrderLine_InternalOrderID] Foreign Key ([InternalOrderID])
	REFERENCES [dbo].[InternalOrder]([InternalOrderID])
    On Update Cascade
GO


print '' print '*** Adding Foreign Keys for PickSheet Table'
--Eric Bostwick 
--Created 3/26/19
--Foreign Keys For PickSheet Table
GO

ALTER TABLE [dbo].[InternalOrderLine]  WITH NOCHECK ADD  CONSTRAINT [fk_InternalOrderLinePickSheetID] FOREIGN KEY([PickSheetID])
REFERENCES [dbo].[PickSheet] ([PickSheetID])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[InternalOrderLine] NOCHECK CONSTRAINT [fk_InternalOrderLinePickSheetID]
GO

ALTER TABLE [dbo].[PickSheet] WITH NOCHECK
	ADD CONSTRAINT [fk_PickSheet_PickCreatedBy] FOREIGN KEY ([PickSheetCreatedBy])
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[PickSheet] WITH NOCHECK
	ADD CONSTRAINT [fk_PickSheet_PickSheetCompletedBy] FOREIGN KEY ([PickCompletedBy])
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[PickSheet] WITH NOCHECK
	ADD CONSTRAINT [fk_PickSheet_PickDeliveredBy] FOREIGN KEY ([PickDeliveredBy])
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[tmpPickTable]  WITH NOCHECK ADD  CONSTRAINT [fk_tmpPickSheetPickSheetID] FOREIGN KEY([PickSheetID])
REFERENCES [dbo].[PickSheet] ([PickSheetID])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[tmpPickTable] NOCHECK CONSTRAINT [fk_tmpPickSheetPickSheetID]
GO


print '' print '*** Adding sp_all_picksheets'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO

CREATE PROCEDURE [dbo].[sp_select_all_picksheets]			
	AS
		BEGIN
		SELECT [PickSheetID], [PickSheetCreatedBy], [eCreatedBy].[FirstName] + ' ' + [eCreatedBy].[LastName] AS PickCreatedByName,
		[CreateDate], [PickCompletedBy], [eCompletedBy].[FirstName] + ' ' +  [eCompletedBy].[LastName] AS PickCompletedByName,
		[PickCompletedDate], [PickDeliveredBy], [eDeliveredBy].[FirstName] + ' ' +  [eDeliveredBy].[LastName] AS PickDeliveredByName,
		[PickDeliveryDate], [NumberOfOrders]
		FROM	[PickSheet] p 
		LEFT OUTER JOIN [Employee] eCreatedBy ON eCreatedBy.EmployeeID = p.PickSheetCreatedBy 
		LEFT OUTER JOIN [Employee] eCompletedBy ON eCompletedBy.EmployeeID = p.PickCompletedBy
		LEFT OUTER JOIN [Employee] eDeliveredBy ON eDeliveredBy.EmployeeID = p.PickDeliveredBy
		
	END
GO

print '' print '*** Adding sp_all_picksheets_by_date'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO

CREATE PROCEDURE [dbo].[sp_select_all_picksheets_by_date]	

	@StartDate DATETIME
			
	AS
		BEGIN
		SELECT [PickSheetID], [PickSheetCreatedBy], [eCreatedBy].[FirstName] + ' ' + [eCreatedBy].[LastName] AS PickCreatedByName,
		[CreateDate], [PickCompletedBy], [eCompletedBy].[FirstName] + ' ' +  [eCompletedBy].[LastName] AS PickCompletedByName,
		[PickCompletedDate], [PickDeliveredBy], [eDeliveredBy].[FirstName] + ' ' +  [eDeliveredBy].[LastName] AS PickDeliveredByName,
		[PickDeliveryDate], [NumberOfOrders]
		FROM	[PickSheet] p 
		LEFT OUTER JOIN [Employee] eCreatedBy ON eCreatedBy.EmployeeID = p.PickSheetCreatedBy 
		LEFT OUTER JOIN [Employee] eCompletedBy ON eCompletedBy.EmployeeID = p.PickCompletedBy
		LEFT OUTER JOIN [Employee] eDeliveredBy ON eDeliveredBy.EmployeeID = p.PickDeliveredBy
		WHERE CreateDate >= @StartDate
	END
GO

print '' print '*** sp_insert_tmppicksheet_to_picksheet'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO

Create PROCEDURE [dbo].[sp_insert_tmppicksheet_to_picksheet]      
	--Moves Records from the tmpPickSheet to the Picksheet table
	--And Updates InternalOrderLine 
	@OrdersAffected int OUTPUT,
	@PickSheetID nvarchar(25)   
AS
BEGIN
	BEGIN TRY

		BEGIN TRAN 

		UPDATE [InternalOrderLine] SET PickSheetID = NULL, [OrderReceivedDate] = NULL, [OrderStatus] = 1 WHERE [PickSheetID] = @PickSheetID

		UPDATE [InternalOrderLine] SET [PicksheetID] = t.[PickSheetID],  [OrderReceivedDate] = getdate(), [OrderStatus] = 2
		FROM [tmpPickTable] AS t JOIN [InternalOrderLine] AS o ON o.[InternalOrderID] = t.[InternalOrderID] AND o.[ItemID] = t.[ItemID]
		WHERE t.PickSheetID = @PickSheetID

		DECLARE @RecordCount int
		DECLARE @NumberOfOrders int
		DECLARE @PickedBy int

		SET @NumberOfOrders = (SELECT Count(*) FROM [InternalOrderLine] WHERE PickSheetID = @PickSheetID)

		SET @RecordCount = (SELECT COUNT(*) FROM PickSheet WHERE PickSheetID = @PickSheetID)

		SET @PickedBy = (SELECT TOP 1 PickedBy FROM tmpPickTable WHERE PickSheetID = @PickSheetID)

		IF @RecordCount > 0   --Update
		  BEGIN
			UPDATE PickSheet SET UpdateDate = getdate(), NumberofOrders = @NumberOfOrders WHERE PickSheetID = @PickSheetID
		  END
		Else
		  BEGIN
			INSERT INTO PickSheet(PickSheetID, PickSheetCreatedBy, NumberofOrders) VALUES(@PickSheetid, @PickedBy, @NumberOfOrders)
		  END

		DELETE FROM tmpPickTable WHERE PickSheetID = @PickSheetID
		
		SET @OrdersAffected = @NumberOfOrders

		COMMIT TRAN

		RETURN @RecordCount 

	END TRY
	BEGIN CATCH
			--Select @@ERROR as Error
			ROLLBACK TRAN
			RETURN 0
	END CATCH
END

print '' print '*** up_insert_tmp_picksheet'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO

CREATE PROCEDURE [dbo].[sp_insert_tmp_picksheet]  
     
     @PickSheetID nvarchar(25),  
     @InternalOrderID int,
     @ItemID int,
	 @PickedBy int
AS
BEGIN	

BEGIN TRY
	BEGIN TRAN		

        Update [InternalOrderLine] SET PickSheetID = @PickSheetID, OrderStatus = 2 WHERE InternalOrderID = @InternalOrderID AND ItemID = @ItemID AND OrderStatus = 1
		INSERT INTO tmpPickTable(PickSheetID, InternalOrderID, ItemID, PickedBy)
						  VALUES(@PickSheetID, @InternalOrderID, @ItemID, @Pickedby)
		
		COMMIT TRAN

		return @@ROWCOUNT

END TRY
BEGIN CATCH
		
		ROLLBACK TRAN
		
		RETURN 0

END CATCH
END

print '' print '*** sp_DeleteTmpPickSheet'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO

CREATE PROCEDURE [dbo].[sp_delete_tmp_picksheet] 
	      
     @PickSheetID nvarchar(25)   
AS

BEGIN	

DELETE FROM tmpPickTable WHERE PickSheetID = @PickSheetID

UPDATE [InternalOrderLine] SET PickSheetID = NULL, OrderStatus = 1 WHERE PickSheetID = @PickSheetID

END

print '' print '*** sp_delete_tmp_picksheet_item'
--Eric Bostwick 
--Created 3/26/19
--Updated
GO

CREATE PROCEDURE [dbo].[sp_delete_tmp_picksheet_item]       
     @PickSheetID nvarchar(25),
     @InternalOrderID int,
	 @ItemID int   
AS
BEGIN	
DELETE FROM [tmpPickTable] WHERE [PickSheetID] = @PickSheetID AND [InternalOrderID] = @InternalOrderID AND ItemID = @ItemID 

Update [InternalOrderLine] SET PickSheetID = NULL, OrderStatus = 1 WHERE InternalOrderID = @InternalOrderID AND ItemID = @ItemID 

return @@ROWCOUNT

END

GO

print '' print '*** sp_select_orders_for_acknowledgement'
--Eric Bostwick 
--Created 4/2/2019
--Updated
GO
CREATE PROCEDURE [dbo].[sp_select_orders_for_acknowledgement] 

@StartDate datetime 

AS
BEGIN

SELECT 
o.[DateOrdered] as [OrderDate],
e.[EmployeeID], e.[FirstName] + ' ' + e.[LastName] as [Orderer],
i.[ItemID], i.[Description] as ItemDescription,
ol.InternalOrderID,
ol.[OrderQty], 
ol.[OrderReceivedDate], 
ol.[PickCompleteDate], 
ol.[PickSheetID], 
ol.[DeliveryDate], 
d.[DepartmentID],
d.[Description],
ol.[OrderStatus],
[OrderStatusView] = CASE ol.[OrderStatus] 
WHEN 1 THEN 'ORDERED' 
WHEN 2 THEN 'ORDER ACKNOWLEDGED'
WHEN 3 THEN 'ORDER PICKED' 
WHEN 4 THEN 'ORDER DELIVERED' END
FROM [InternalOrder] o LEFT OUTER JOIN [dbo].[InternalOrderLine] ol ON o.[InternalOrderID] = ol.[InternalOrderID]
LEFT OUTER JOIN [Employee] e ON e.[EmployeeID] = o.[EmployeeID]
LEFT OUTER JOIN [Department] d ON d.[DepartmentID] = o.[DepartmentID]
LEFT OUTER JOIN [Item] i ON i.[ItemID] = ol.[ItemID]
WHERE  o.[DateOrdered] > @StartDate 

END



print '' print '*** sp_select_orders_for_acknowledgement_hidepicked'
--Eric Bostwick 
--Created 4/2/2019
--Updated
GO
CREATE PROCEDURE [dbo].[sp_select_orders_for_acknowledgement_hidepicked] 

@StartDate datetime 

AS
BEGIN

SELECT 
o.[DateOrdered] as [OrderDate],
e.[EmployeeID], e.[FirstName] + ' ' + e.[LastName] as [Orderer],
i.[ItemID], i.[Description] as ItemDescription,
ol.InternalOrderID,
ol.[OrderQty], 
ol.[OrderReceivedDate], 
ol.[PickCompleteDate], 
ol.[PickSheetID], 
ol.[DeliveryDate], 
d.[DepartmentID],
d.[Description],
ol.[OrderStatus],
[OrderStatusView] = CASE ol.[OrderStatus] 
WHEN 1 THEN 'ORDERED' 
WHEN 2 THEN 'ORDER ACKNOWLEDGED'
WHEN 3 THEN 'ORDER PICKED' 
WHEN 4 THEN 'ORDER DELIVERED' END
FROM [InternalOrder] o LEFT OUTER JOIN [dbo].[InternalOrderLine] ol ON o.[InternalOrderID] = ol.[InternalOrderID]
LEFT OUTER JOIN [Employee] e ON e.[EmployeeID] = o.[EmployeeID]
LEFT OUTER JOIN [Department] d ON d.[DepartmentID] = o.[DepartmentID]
LEFT OUTER JOIN [Item] i ON i.[ItemID] = ol.[ItemID]
WHERE  o.[DateOrdered] > @StartDate 
AND [OrderStatus] = 1
ORDER BY o.[DateOrdered]

END
GO

print '' print '*** sp_select_next_picksheet_number'
--Eric Bostwick 
--Created 4/2/2019
--Updated 4/6/2019
GO
CREATE PROCEDURE [dbo].[sp_select_next_picksheet_number] 

@PickSheetNumber nvarchar(25) OUTPUT

AS
BEGIN
	if exists(select 1 from [PickSheet] where [PickSheetID] is not NULL)
	BEGIN
		--SELECT @PickSheetNumber as PickSheetNumber1
		SET @PickSheetNumber = (SELECT MAX(PickSheetNumber) + 1 FROM [PickSheet])
		--SELECT @PickSheetNumber as PickSheetNumber2
		SET @PickSheetNumber = CAST(@PickSheetNumber AS [varchar]) + HOST_ID()
		--SELECT @PickSheetNumber as PickSheetNumber3
		
	END

	ELSE
	BEGIN
		--No Pick Sheets in the pick sheet table 
		SET @PickSheetNumber = '100000' + HOST_ID()
	END
END
GO

print '' print '*** up_select_picksheet_by_picksheetid'
--Eric Bostwick 
--Created 4/6/2019
--Updated 
GO

CREATE PROCEDURE [dbo].[sp_select_picksheet_by_picksheetid] 
	
@PickSheetID char(25)

AS
BEGIN

SELECT  p.[PickSheetID], 
o.[DateOrdered] as OrderDate,
RTRIM(eo.[FirstName]) + ' ' + RTRIM(eo.[LastName]) as Orderer,
RTRIM(epc.[FirstName]) + ' ' + RTRIM(epc.[LastName]) AS PickSheetCreator, 
RTRIM(ep.[FirstName]) + ' ' + RTRIM(ep.[LastName]) AS PickedBy, 
RTRIM(ep.[FirstName]) + ' ' + RTRIM(ep.[LastName]) AS PickDeliveredBy, 
p.[CreateDate] AS PickSheetDate, p.[PickDeliveryDate] AS DeliveryDate, 
p.[PickDeliveredBy] AS DeliveredBy, 
ol.[InternalOrderID],
o.[EmployeeID],
i.[ItemID], i.[Description] as ItemDescription,
i.[Description],
ol.[OrderQty], 'EA' as OrderUM, ol.[QtyReceived], ol.[OrderReceivedDate], 
o.[DepartmentID], ol.[OrderStatus], ol.[PickCompleteDate], 
ol.[OutOfStock],
[StatusView] = CASE ol.[OrderStatus] 
WHEN 1 THEN 'ORDERED' 
WHEN 2 THEN 'ORDER ACKNOWLEDGED'
WHEN 3 THEN 'ORDER PICKED' 
WHEN '4' THEN 'ORDER DELIVERED' END,
p.[NumberofOrders]

FROM [InternalOrderLine] AS ol 
LEFT OUTER JOIN [InternalOrder] AS o on o.[InternalOrderID] = ol.[InternalOrderID]
LEFT OUTER JOIN [PickSheet] AS p ON ol.[PickSheetID] = p.[PickSheetID] 
LEFT OUTER JOIN [Employee] AS eo ON eo.[EmployeeID] = o.[EmployeeID]
LEFT OUTER JOIN [Employee] AS epc ON epc.[EmployeeID] = p.[PickSheetCreatedBy]
LEFT OUTER JOIN [Employee] as ep ON ep.[EmployeeID] = p.[PickCompletedBy]
LEFT OUTER JOIN [Employee] AS ed ON ed.[EmployeeID] = p.[PickDeliveredBy]
LEFT OUTER JOIN [Item] AS i ON i.ItemID = ol.ItemID 

WHERE ol.[PickSheetID] = @PickSheetID   
                 
END

GO

print '' print '*** sp_update_pick_order'
--Eric Bostwick 
--Created 4/7/2019
--Updated 4/12/2019
GO

CREATE PROCEDURE [dbo].[sp_update_pick_order]

@ItemID int,
@InternalOrderID int,
@OrderQty int,
@QtyReceived int,
@OrderReceivedDate datetime,
@PickSheetID nvarchar(25),
@PickCompleteDate datetime,
@OutOfStock bit,
@DeliveryDate datetime,
@OrderStatus int,

@OldItemID int,
@OldInternalOrderID int,
@OldOrderQty int,
@OldQtyReceived int,
@OldOrderReceivedDate datetime,
@OldPickSheetID nvarchar(25),
@OldPickCompleteDate datetime,
@OldDeliveryDate datetime,
@OldOutOfStock bit,
@OldOrderStatus int

AS

BEGIN	

UPDATE [InternalOrderLine]
SET [QtyReceived] =  @QtyReceived,
	[PickCompleteDate] = @PickCompleteDate,
	[OutOfStock] = @OutOfStock,		
	[OrderStatus] = @OrderStatus
WHERE 
	[ItemID] = @OldItemID
	AND [InternalOrderID] = @OldInternalOrderID
	AND [OrderQty] = @OldOrderQty	
	AND [QtyReceived] = @OldQtyReceived
	AND [OrderReceivedDate] = @OldOrderReceivedDate
	AND [PickSheetID] = @OldPickSheetID
	----AND [PickCompleteDate] = @OldPickCompleteDate
	----AND [DeliveryDate] = @OldDeliveryDate
	AND [OutOfStock] = @OldOutOfStock
	AND [OrderStatus] = @OldOrderStatus

Return @@RowCount
END

GO
print '' print '*** sp_update_picksheet'
--Eric Bostwick 
--Created 4/8/2019
--Updated 
GO

GO
CREATE PROCEDURE [dbo].[sp_update_picksheet]

@PickSheetID nvarchar(25),
@PickCompletedBy int,
@PickCompletedDate datetime,
@PickDeliveredBy int,
@PickDeliveryDate datetime,
@NumberofOrders int,
@OldPickCompletedBy int,
@OldPickCompletedDate datetime,
@OldPickDeliveredBy int,
@OldPickDeliveryDate datetime,
@OldNumberofOrders int

AS

BEGIN

IF @PickCompletedBy = 0
	BEGIN
		SET @PickCompletedBy = NULL
	END
IF @PickDeliveredby = 0
	BEGIN
		SET @PickDeliveredby = NULL
	END
IF @OldPickDeliveredby = 0
	BEGIN		
		SET @OldPickDeliveredBy = NULL
	END
IF @PickCompletedby = 0
	BEGIN
		SET @PickCompletedBy = NULL
	END
IF @OldPickCompletedby = 0
	BEGIN		
		SET @OldPickCompletedBy = NULL
	END

UPDATE PickSheet 
SET 
	[PickCompletedBy] = @PickCompletedBy, 
	[PickCompletedDate] = @PickCompletedDate,
	[PickDeliveryDate] = @PickDeliveryDate,
	[PickDeliveredBy] = @PickDeliveredBy,
	[NumberofOrders] = @NumberofOrders

	WHERE [PickSheetID] = @PickSheetID	
	--AND [PickCompletedBy] = @OldPickCompletedBy	
	--AND PickDeliveredBy = @OldPickDeliveredBy
	AND [NumberofOrders] = @OldNumberofOrders

	--Select @@ROWCOUNT as rc
	
IF @@ROWCOUNT = 1 
BEGIN
	
	IF @PickDeliveredby IS NULL AND @PickCompletedBy IS NULL
		BEGIN 
			--Select '2'
			UPDATE [InternalOrderLine] SET OrderStatus = 2, [PickCompleteDate] = NULL, [DeliveryDate] = NULL WHERE [PickSheetID] = @PickSheetID
		END
	IF @PickDeliveredBy IS NULL AND @PickCompletedBy > 0
		BEGIN 
			--Select '3'
			UPDATE [InternalOrderLine] SET OrderStatus = 3, [DeliveryDate] = NULL, [PickCompleteDate] = @PickCompletedDate WHERE [PickSheetID] = @PickSheetID
		END 	
	IF @PickDeliveredby > 0
		BEGIN		
			--Select '4'	
			UPDATE [InternalOrderLine] SET OrderStatus = 4, [DeliveryDate] = @PickDeliveryDate WHERE [PickSheetID] = @PickSheetID
		END
		
END


RETURN @@ROWCOUNT
END
GO

print '' print '*** sp_select_all_closed_pickSheets_by_date'
--Eric Bostwick 
--Created 4/9/2019
--Updated 

GO
CREATE PROCEDURE [dbo].[sp_select_all_closed_pickSheets_by_date]
	@StartDate DATETIME
AS
BEGIN
		Select p.[PickSheetID], 		
		e.[FirstName] + ' ' + e.[LastName] AS [PickCompletedByName],
		ed.[FirstName] + ' ' + ed.[LastName] AS [PickDeliveredByName],		
		p.[NumberOfOrders] as OrderCount,		
		p.[CreateDate],
		p.[PickCompletedDate], 		
		p.[PickDeliveryDate],
		ISNULL(p.[PickSheetCreatedBy],0) AS [PickSheetCreator], 
		ISNULL(p.[PickDeliveredBy], 0) AS [PickDeliveredBy],
		ISNULL(p.[PickCompletedBy], 0) AS [PickCompletedBy]
		FROM [PickSheet] as p
		Left Outer Join [Employee] as e on e.[EmployeeID] = p.[PickCompletedBy]
		Left Outer Join [Employee] as ed on ed.[EmployeeID] = p.[PickDeliveredBy]
		WHERE CreateDate >= @StartDate
		AND PickCompletedBy > 0
END



--ALTER TABLE [PickSheet] 
--ADD  [PickSheetNumber] [int] DEFAULT 100000



--PICKSTUFF