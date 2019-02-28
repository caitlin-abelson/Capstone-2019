USE [MillennialResort_DB]
GO

INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Reservation', 'Reserves Rooms'),
		('RoomCheckout', 'Checks Rooms out'),
		('EventRequest', 'Requests an event '),
		('Offering', 'Offers items to be purchased from us'),
		('Maintenance', 'Repairs and Maintains Rooms'),
		('Procurement', ' Manages items ordered for inventory'),
		('VehicleCheckout', 'Check Vehicle out'),
		('test', 'test1'),
		('Admin', 'Administers Employee Roles')
GO

INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100000, 'Reservation'),
		(100001, 'RoomCheckout'),
		(100001, 'VehicleCheckout'),
		(100001, 'EventRequest'),
		(100002, 'Offering'),
		(100002, 'Maintenance'),
		(100002, 'Procurement'),
		(100003, 'Manager'),
		(100003, 'Admin')
GO

INSERT INTO [dbo].[Department]
		([DepartmentID], [Description])
	VALUES
		('Events','This employee handles the day to day for the events that happen at our resort.'),
		('Kitchen','This employee is one of our kitchen staff that prepared meals at our restaurant.'),
		('Catering','This employee works on getting food to and from our various events that we host at the resort.'),
		('Grooming','This employee tends to the salon needs of the pets that visit our resort.'),
		('Talent','This employee provides entertainment at events that are hosted at our resort.')
GO

INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active])
	VALUES
		('Joanne', 'Smith', '1319551111', 'joanne@company.com', 'Events', 1),
		('Martin', 'Jones', '1319551111', 'martin@company.com', 'Kitchen', 1),
		('Leo', 'Williams', '1319551111', 'leo@company.com', 'Catering', 1),
		('Joe', 'Shmoe', '1319551112', 'joe@company.com', 'Grooming', 0)
GO

INSERT INTO [dbo].[ItemType]
		([ItemTypeID])
	VALUES
		('Food'),
		('Hot Sauce'),
		('Shoe'),
		('Hat'),
		('Tshirt'),
		('Pet'),
		('Beverage')
GO

INSERT INTO [dbo].[Product]
		([ItemTypeID], [Description],[OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID], [OfferingID])
	VALUES
		('Food', 'Its a food item', 4, 'Its a large taco', 1, '2019-02-01', 1, 1, 1051,1001),
		('Shoe', 'Its a shoe item', 4, 'Its a small light up shoe', 1, '2019-02-01', 0, 1, 1051,1001),
		('Hat', 'Its a hat item', 4, 'Its a large sombrero', 1, '2019-02-01', 1, 1, 1051,1001),
		('Food', 'Its a fodd item', 4, 'Its a large burrito', 1, '2019-02-01', 0,1, 1051,1001),
		('Hat', '', 4, 'Abe Lincoln Hat', 1, '2019-02-01', 1, 1, 1051,1001),
		('Food', 'Wonderful & delicious', 9, 'Hickory smoked salt', 15, '2019-02-01', 1, 1, 1051,1001),
		('Food', 'Its a food item', 4, 'Hamburger', 1, '2019-02-11', 1, 0, 1051,1001),
		('Food', 'I wonder if its a steak', 4, '8 oz. New York Strip', 1, '2019-02-01', 1, 0, 1051,1001),
		('Food', 'I am hungry right now', 25, '6 oz. Ahi Tuna Steak', 10, '2019-02-01', 1, 1, 1051,1001),
		('Food', 'Its a food item', 4, 'Its popcorn', 1, '2019-02-01', 1, 1, 1051,1001)
		
GO

INSERT INTO [dbo].[Reservation]
		([MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes])
	VALUES
		(100000,1,0,'2008-11-11','2008-11-12','test')
GO

INSERT INTO [dbo].[Member]
		([FirstName],[LastName],[PhoneNumber],[Email])
	VALUES
		('Spongebob','Squarepants','1112223333','bobswag@kk.com'),
		('Patrick','Star','2223334444','starboi@kk.com')
GO