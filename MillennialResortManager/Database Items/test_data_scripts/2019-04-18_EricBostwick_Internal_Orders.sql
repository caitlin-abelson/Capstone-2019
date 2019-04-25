USE [MillennialResort_DB]

print '' print '*** Creating PickSheet Table'
--Eric Bostwick 
--Created 4/19/19
--Updated

INSERT INTO InternalOrder(EmployeeID, DepartmentID, Description, DateOrdered)
VALUES(100000, 'Events', 'Order 1', getdate()),
	  (100001, 'FoodService', 'Order 2', getdate()),
	  (100002, 'Maintenance', 'Order 3', getdate())

INSERT INTO [InternalOrderLine] (ItemID, InternalOrderID, OrderQty)
VALUES(100000, 100000, 50),
      (100001, 100000, 100),
	  (100002, 100000, 150),
	  (100003, 100000, 200),
	  (100000, 100001, 50),
      (100001, 100001, 100),
	  (100002, 100001, 150),
	  (100003, 100001, 200),
	  (100000, 100002, 50),
      (100001, 100002, 100),
	  (100002, 100002, 150),
	  (100003, 100002, 200)
	  
	  


