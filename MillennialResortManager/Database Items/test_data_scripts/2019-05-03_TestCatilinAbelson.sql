USE [MillennialResort_DB]
GO

INSERT INTO [dbo].[InternalOrderLine]
	
	([ItemID], [InternalOrderID], [OrderQty], [QtyReceived], [PickSheetID], [OrderReceivedDate], [PickCompleteDate], [DeliveryDate], [OrderStatus], [OutOfStock])
	
	VALUES
	(100000, 100001, 35, 0, NULL, "2019-03-25", NULL, "2019-04-04", 1, 0),
	(100001, 100002, 100, 0, NULL, "2019-01-20", NULL, "2019-02-15", 1, 0),
	(100002, 100003, 43, 0, NULL, "2019-01-13", NULL, "2019-01-19", 1, 0),
	(100003, 100004, 75, 0, NULL, "2019-02-19", NULL, "2019-03-10", 1, 0),
	(100004, 100005, 10, 0, NULL, "2019-02-28", NULL, "2019-03-18", 1, 0),
	(100005, 100006, 20, 0, NULL, "2019-04-05", NULL, "2019-04-10", 1, 0),
	(100006, 100007, 35, 0, NULL, "2019-03-05", NULL, "2019-03-20", 1, 0),
	(100007, 100008, 40, 0, NULL, "2019-02-22", NULL, "2019-03-15", 1, 0)
		

	
GO