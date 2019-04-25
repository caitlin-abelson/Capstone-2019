USE [MillennialResort_DB]
GO

print '' print '*** INSERT INTO [Department]'
INSERT INTO [Department]
		([DepartmentID], [Description])
	VALUES
		("FrontDesk", "Front Desk Employee")

print '' print '*** INSERT INTO [Employee]'
INSERT INTO [Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID])
	VALUES
		("Bob", "Trapp", "1235551234", "bob@itsatrapp.com", "FrontDesk")

print '' print '*** INSERT INTO [ResortPropertyType]'
INSERT INTO [ResortPropertyType]
		([ResortPropertyTypeID])
	VALUES
		("Resort1")

print '' print '*** INSERT INTO [ResortProperty]'		
INSERT INTO [ResortProperty]
		([ResortPropertyTypeID])
	VALUES
		("Resort1")

print '' print '*** INSERT INTO [BuildingStatus]'
INSERT INTO [BuildingStatus]
		([BuildingStatusID], [Description])
	VALUES
		("Good", "I dont know")

print '' print '*** INSERT INTO [Building]'
INSERT INTO [Building]
		([BuildingID], [BuildingName], [BuildingStatusID], [ResortPropertyID])
	VALUES
		("Building 1", "Nielsen Hall", "Good", 100000)

print '' print '*** INSERT INTO [OfferingType]'		
INSERT INTO [OfferingType]
		([OfferingTypeID], [Description])
	VALUES
		("SingleQueenRoom", "It's just a hotel room")

print '' print '*** INSERT INTO [Offering]'		
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

print '' print '*** INSERT INTO [RoomType]'
INSERT INTO [RoomType]
			([RoomTypeID], [Description])
		VALUES
			("Single Queen", "A room with a single queen bed."),
			("Double Queen", "A room with two queen beds."),
			("Single King", "A room with a single king bed.")


print '' print '*** INSERT INTO [Room]'		
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

print '' print '*** INSERT INTO [Member]'
INSERT INTO [Member]
	([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
	("Dunder Mifflin", "", "5635551234", "info@dundermifflin.com"),
	("Emma", "Watson", "3195551234", "emma.watson@example.com"),
	("Master", "Chief", "6415551234", "john117@unsc.com")
	

print '' print '*** INSERT INTO [Guest]'
INSERT INTO [Guest]
	([MemberID], [FirstName], [LastName], [PhoneNumber], [Email],
	[ReceiveTexts], [EmergencyFirstName], [EmergencyLastName], 
	[EmergencyPhoneNumber], [EmergencyRelation], [CheckedIn])
	VALUES
	(100000, "Michael", "Scott", "5635551235", "michael.scott@dundermifflin.com",
	0, "Jan", "Levinson", "5635551236", "Complicated", 0),
	(100001, "Jack", "Ripper", "5635551237", "jacktheripper@gmail.com",
	0, "Joan", "Ripper", "5635551220", "Wife", 0),
	(100001, "Joan", "Ripper", "5635551220", "jacktheripperswife@gmail.com",
	0, "Jack", "Ripper", "5635551237", "Husband", 0)

print '' print '*** INSERT INTO [Reservation]'	
INSERT INTO [Reservation]
	([MemberID], [NumberOfGuests], [NumberOfPets], [ArrivalDate], [DepartureDate])
	VALUES
		(100000, 3, 0, "2019-04-04", "2019-04-07"),
		(100001, 3, 0, "2019-04-04", "2019-04-07")
		
		
print '' print '*** INSERT INTO [RoomReservation]'	
INSERT INTO [RoomReservation]
		([RoomID], [ReservationID])
	VALUES
		(100000, 100001),
		(100001, 100001),
		(100002, 100001)

		
print '' print '*** INSERT INTO [GuestRoomAssignment]'	
INSERT INTO [GuestRoomAssignment]
		([GuestID], [RoomReservationID])
	VALUES
		(100001, 100001),
		(100002, 100001)
