SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
GO

print '' print 'Inserting Departments'
GO
INSERT INTO [Department]
(
	[DepartmentID], [Description]
)
VALUES
	("Everything", "They do everything.")
GO

print '' print 'Inserting Employee'
GO
INSERT INTO [Employee]
(
	[FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID]
)
VALUES
	("Dave", "Admin", "1234567890", "DaveAdmin@gmail.com", "Everything")
GO

/*
print '' print 'Inserting OfferingType'
GO
INSERT INTO [OfferingType]
(
    [OfferingTypeID], [Description] 
)	
VALUES
    ('Room', 'IDK')
GO
*/

/*
print '' print 'Inserting RoomType'
GO
INSERT INTO [RoomType]
(
    [RoomTypeID], [Description] 
)	
VALUES
    ('Double', 'Double beds'),
    ('Queen', 'Single Queen'),
    ('King', 'Single King'),
	('Shop', 'Shop')
GO
*/
/*
print '' print 'Inserting ResortPropertyType'
GO
INSERT INTO [ResortPropertyType]
(
    [ResortPropertyTypeID]
)
VALUES
    ('Building'),
    ('Room'),
    ('Vehicle')
GO
*/
/*
print '' print '*** Insert BuildingStatus Records ***'
GO
INSERT INTO [dbo].[BuildingStatus]
		([BuildingStatusID], [Description])
	VALUES
		('Available', 'Building is good to go!'),
		('No Vacancy', 'All rooms are filled'),
		('Undergoing Maintanance', 'Some rooms available')
GO
*/
print '' print '*** Insert ResortProperty Records ***'
GO
INSERT INTO [dbo].[ResortProperty]
		([ResortPropertyTypeID])
	VALUES
		('Building'),
		('Building'),
		('Building'),
		('Building'),
		('Building'),
		('Building'),
		('Building'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room'),
		('Room')
GO

print '' print '*** Inserting Building Records ***'
GO
INSERT INTO [dbo].[Building]
		([BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID], [ResortPropertyID])
	VALUES
		('Hotel 101', 'The Mud Burrow', '1202 Turtle Pond Parkway', 'Guest Hotel Rooms', 'Available', 100000),
		('Hotel 102', 'Shell Shack', '1302 Turtle Pond Parkway', 'Guest Hotel Rooms', 'No Vacancy', 100001),
		('Guest Bld 101', 'Chlorine Dreams', '666 Angler Circle ', 'Water Park', 'Available', 100002),
		('Shopping Center 101', 'The Coral Reef', '1202 Try n Save Drive', 'Shopping Center', 'Available', 100003),
		('Food Center 101', 'Trout Hatch', '808 Turtle Pond Parkway', 'Food Court', 'Undergoing Maintanance', 100004),
		('Welcome Center', 'Canopy Center', '1986 Tsunami Trail', 'Main Guest Center', 'Available', 100005),
		('GenBld01', 'Sea Cow Storage', '812 South Padre', 'Storage', 'Available', 100006)
GO
print '' print '***Inserting a fake Offering record'
GO
INSERT INTO [dbo].[Offering]
			([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00),
			('Room',100000,'A description for a fake Offering',100.00)
GO


print '' print '*** Inserting Room Records ***'
GO
INSERT INTO [dbo].[Room]
		([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [ResortPropertyID], [OfferingID], [RoomStatusID])
	VALUES
		(101, 'Hotel 101', 'Double', 'Double beds', 4, 100007, 100000, 'Available'),
		(102, 'Hotel 101', 'King', 'Single king', 2, 100008, 100001, 'Occupied'),
		(101, 'Hotel 102', 'Double', 'Double beds', 4, 100009, 100002, 'Available'),
		(102, 'Hotel 102', 'King', 'Single king', 2, 100010, 100003, 'Occupied'),
		(101, 'Guest Bld 101', 'Double', 'Double beds', 4, 100011, 100004, 'Available'),
		(102, 'Guest Bld 101', 'King', 'Single king', 2, 100012, 100005, 'Occupied'),
		(101, 'Shopping Center 101', 'Double', 'Double beds', 4, 100013, 100006, 'Available'),
		(102, 'Shopping Center 101', 'King', 'Single king', 2, 100014, 100007, 'Occupied'),
		(101, 'Food Center 101', 'Double', 'Double beds', 4, 100015, 100008, 'Available'),
		(102, 'Food Center 101', 'King', 'Single king', 2, 100016, 100009, 'Occupied'),
		(101, 'Welcome Center', 'Double', 'Double beds', 4, 100017, 100010, 'Available'),
		(102, 'Welcome Center', 'King', 'Single king', 2, 100018, 100011, 'Occupied'),
		(101, 'GenBld01', 'Double', 'Double beds', 4, 100019, 100012, 'Available'),
		(102, 'GenBld01', 'King', 'Single king', 2, 100020, 100013, 'Occupied')
GO

