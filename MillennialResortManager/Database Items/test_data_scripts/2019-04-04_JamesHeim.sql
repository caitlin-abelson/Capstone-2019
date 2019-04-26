USE [MillennialResort_DB]
GO

print '' print '*** INSERT INTO [Department]'
GO
INSERT INTO [Department]
		([DepartmentID], [Description])
	VALUES
		("FrontDesk", "Front Desk Employee")
GO
print '' print '*** INSERT INTO [Employee]'
GO
INSERT INTO [Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID])
	VALUES
		("Bob", "Trapp", "1235551234", "bob@itsatrapp.com", "FrontDesk")
GO
print '' print '*** INSERT INTO [ResortPropertyType]'
GO
INSERT INTO [ResortPropertyType]
		([ResortPropertyTypeID])
	VALUES
		("Resort1")
GO
print '' print '*** INSERT INTO [ResortProperty]'	
GO	
INSERT INTO [ResortProperty]
		([ResortPropertyTypeID])
	VALUES
		("Resort1")
GO
print '' print '*** INSERT INTO [BuildingStatus]'
GO
INSERT INTO [BuildingStatus]
		([BuildingStatusID], [Description])
	VALUES
		("Good", "I dont know")
GO
print '' print '*** INSERT INTO [Building]'
GO
INSERT INTO [Building]
		([BuildingID], [BuildingName], [BuildingStatusID], [ResortPropertyID])
	VALUES
		("Building 1", "Nielsen Hall", "Good", 100000)
GO
print '' print '*** INSERT INTO [OfferingType]'	
GO	
INSERT INTO [OfferingType]
		([OfferingTypeID], [Description])
	VALUES
		("SingleQueenRoom", "It's just a hotel room")
GO
print '' print '*** INSERT INTO [Offering]'	
GO	
INSERT INTO [Offering]
		([OfferingTypeID], [EmployeeID], [Description], [Price], [Active])
	VALUES
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1),
		("SingleQueenRoom", 100000, "Room", 299.99, 1)
GO
print '' print '*** INSERT INTO [RoomType]'
GO
INSERT INTO [RoomType]
			([RoomTypeID], [Description])
		VALUES
			("Single Queen", "A room with a single queen bed."),
			("Double Queen", "A room with two queen beds."),
			("Single King", "A room with a single king bed.")
GO

print '' print '*** INSERT INTO [Room]'	
GO	
INSERT INTO [Room]
		([RoomNumber], [BuildingID], [RoomTypeID], [Description],  
		 [Capacity], [OfferingID], [ResortPropertyID])
	 VALUES
		("100", "Building 1", "Single Queen", "It's just a room", 2, 100004, 100000),
		("101", "Building 1", "Single Queen", "It's just a room", 2, 100005, 100000),
		("102", "Building 1", "Single Queen", "It's just a room", 2, 100006, 100000),
		("103", "Building 1", "Single Queen", "It's just a room", 2, 100007, 100000),
		("104", "Building 1", "Single Queen", "It's just a room", 2, 100008, 100000),
		("105", "Building 1", "Single Queen", "It's just a room", 2, 100009, 100000)
GO
print '' print '*** INSERT INTO [Member]'
GO
INSERT INTO [Member]
	([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
	("Dunder Mifflin", "", "5635551234", "info@dundermifflin.com"),
	("Emma", "Watson", "3195551234", "emma.watson@example.com"),
	("Master", "Chief", "6415551234", "john117@unsc.com")
GO	

print '' print '*** INSERT INTO [Guest]'
GO
INSERT INTO [Guest]
	([MemberID], [FirstName], [LastName], [PhoneNumber], [Email],
	[ReceiveTexts], [EmergencyFirstName], [EmergencyLastName], 
	[EmergencyPhoneNumber], [EmergencyRelation], [CheckedIn])
	VALUES
	(100001, "Michael", "Scott", "5635551235", "michael.scott@dundermifflin.com",
	0, "Jan", "Levinson", "5635551236", "Complicated", 0),
	(100001, "Jack", "Ripper", "5635551237", "jacktheripper@gmail.com",
	0, "Joan", "Ripper", "5635551220", "Wife", 0),
	(100001, "Joan", "Ripper", "5635551220", "jacktheripperswife@gmail.com",
	0, "Jack", "Ripper", "5635551237", "Husband", 0)
GO
print '' print '*** INSERT INTO [Reservation]'	
GO
INSERT INTO [Reservation]
	([MemberID], [NumberOfGuests], [NumberOfPets], [ArrivalDate], [DepartureDate])
	VALUES
		(100000, 3, 0, "2019-04-04", "2019-04-07"),
		(100001, 3, 0, "2019-04-04", "2019-04-07")
GO		
		
print '' print '*** INSERT INTO [RoomReservation]'	
INSERT INTO [RoomReservation]
		([RoomID], [ReservationID], [CheckinDate])
	VALUES
		(100000, 100000, CURRENT_TIMESTAMP),
		(100001, 100000, CURRENT_TIMESTAMP)/*,
		//(100002, 100000, CURRENT_TIMESTAMP)*/
GO	
print '' print '*** INSERT INTO [GuestRoomAssignment]'	
GO
INSERT INTO [GuestRoomAssignment]
		([GuestID], [RoomReservationID], [CheckinDate])
	VALUES
		(100001, 100000,CURRENT_TIMESTAMP),
		(100002, 100000,CURRENT_TIMESTAMP),
		(100000, 100001,CURRENT_TIMESTAMP)
GO
print '' print '*** INSERT INTO [MemberTab]'	
INSERT INTO [MemberTab]
		([MemberID])
	VALUES
		(100000)
	GO	
print '' print 'Inserting Offering'
GO
INSERT INTO [Offering]
(
	[OfferingTypeID], [EmployeeID], [Description], [Price]
)
VALUES
	("Item", 100000, "Turtle of Doom", 130.45),
	("Event", 100000, "Fire Dancing", 130.45),
	("Room", 100000, "Turtle Room", 130.45),
	("Service", 100000, "Turtle Massage", 130.45)
GO
		
print '' print '*** INSERT INTO [MemberTabLine]'
GO	
INSERT INTO [MemberTabLine]
		([MemberTabID], [OfferingID], [Quantity], [Price], [EmployeeID], [Discount], [GuestID])
	VALUES
		(100000, 100000, 3, 45.00, 100000, 0, 100000),
		(100000, 100001, 5, 56.00, 100000, 0, 100000),
		(100000, 100000, 7, 32.00, 100000, 0, 100000),
		(100000, 100000, 1, 30.00, 100000, 0, 100000)
		GO