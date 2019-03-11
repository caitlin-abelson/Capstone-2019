USE [MillennialResort_DB]
GO

print '' '*** Role records'
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

print '' print '*** Employee role records'
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

print '' print '*** department records'
INSERT INTO [dbo].[Department]
		([DepartmentID], [Description])
	VALUES
		('Events','This employee handles the day to day for the events that happen at our resort.'),
		('Kitchen','This employee is one of our kitchen staff that prepared meals at our restaurant.'),
		('Catering','This employee works on getting food to and from our various events that we host at the resort.'),
		('Grooming','This employee tends to the salon needs of the pets that visit our resort.'),
		('Talent','This employee provides entertainment at events that are hosted at our resort.')
GO

print '' print '*** Employee records'
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active])
	VALUES
		('Joanne', 'Smith', '1319551111', 'joanne@company.com', 'Events', 1),
		('Martin', 'Jones', '1319551111', 'martin@company.com', 'Kitchen', 1),
		('Leo', 'Williams', '1319551111', 'leo@company.com', 'Catering', 1),
		('Joe', 'Shmoe', '1319551112', 'joe@company.com', 'Grooming', 0)
GO

print '' print '*** Item type records'
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

print '' print '*** Product records'
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

print '' print '*** Reservation records'
INSERT INTO [dbo].[Reservation]
		([MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes])
	VALUES
		(100000,1,0,'2008-11-11','2008-11-12','test')
GO

print '' print '*** Member records'
INSERT INTO [dbo].[Member]
		([FirstName],[LastName],[PhoneNumber],[Email])
	VALUES
		('Spongebob','Squarepants','1112223333','bobswag@kk.com'),
		('Patrick','Star','2223334444','starboi@kk.com')
GO

/*
 * Author: James Heim
 * Created 2019-02-27
 *
 * Insert Shop test records.
 */
 print '' print '*** Inserting test records for Shop'
 GO
 INSERT INTO [dbo].[Shop]
		([RoomID], [Name], [Description], [Active])
	VALUES
		( 100000, "Brawlmart", "Discounts you'll fight over", 1),
		( 100003, "Chungus Club", "r/FellowKids material", 1),
		( 100004, "Solar City", "Overpriced sun tan lotion", 1),
		( 100006, "Wavy Daisy", "Surf and Sun shop", 1),
		( 100007, "Millenial Resort Gift Shop", "I ran out of ideas", 1),
		( 100008, "Brokesville", "This shop is inactive", 0)
GO

print '' print '*** Pet type records'
INSERT INTO [dbo].[PetType]
		([PetTypeID], [Description])
	VALUES
		('Dog', 'Best Dog in the World'),
		('Cat', 'Best Cat in the World'),
		('CatDog', 'Best MonkeyCat in the World')
GO

print '' print '*** Pet records'
INSERT INTO [dbo].[Pet]
		([PetName], [Gender], [Species], [PetTypeID], [GuestID])
	VALUES
		('Bandit', 'Male', 'Labrador', 'Dog', '777777'),
		('ShitRock', 'Female', 'Tabby', 'Cat', '888888'),
		('Whiskers', 'Neutral', 'MonkeyCat', 'CatDog', '999999')
GO

print '' print '*** Appointment type records'
INSERT INTO [dbo].[AppointmentType]
		([AppointmentTypeID], [Description])
	VALUES
		('C# and Yoga', 'Modern Yoga infused with loose couplings and dependancy construction.'),
		('sql and Yoga', 'Modern Yoga infused with data redundancy.')
GO

print '' print '*** Event type records'
INSERT INTO [dbo].[EventType]
		([EventTypeID], [Description])
	VALUES
		('Concert Event', 'A concert is a live music performance in front of an audience.'),
		('Beach Party', 'There are plenty of opportunities to have a great time at the beach.'),
		('Wedding', 'Romantic Florals typically make up a romantic wedding also those who never been one to take the normal route?')
GO

/*Start Wes Richardson 2019-03-01*/

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert GuestType Test Records
 */
print '' print '*** GuestType Test Data' 
GO
INSERT INTO [dbo].[GuestType]
		([GuestTypeID], [Description])
	VALUES
		('Basic guest', 'Basic guest')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Guest Test Records
 */
print '' print '*** Guest Test Data' 
GO
INSERT INTO [dbo].[Guest]
		([MemberID], [GuestTypeID], [FirstName], [LastName], [PhoneNumber], [Email], [ReceiveTexts], [EmergencyFirstName], [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation])
	VALUES
		(100001, 'Basic guest', 'John', 'Doe', '3195555555', 'John@Company.com', 1, 'Jane', 'Doe', '3195555556', 'Wife'),
		(100001, 'Basic guest', 'Jane', 'Doe', '3195555556', 'Jane@Company.com', 1, 'John', 'Doe', '3195555555', 'Husband')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Appointment Type Test Records
 */
 print '' print '*** AppointmentType Test Data' 
GO
INSERT INTO [dbo].[AppointmentType]
		([AppointmentTypeID], [Description])
	VALUES
		('Spa', 'Spa'),
		('Pet Grooming', 'Pet Grooming'),
		('Turtle Petting', 'Turtle Petting'),
		('Whale Watching', 'Whale Watching'),
		('Sand Castle', 'Sand Castle Building')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Appointment Test Records
 */
 print '' print '*** Appointment Test Data' 
GO
INSERT INTO [dbo].[Appointment]
		([AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description])
	VALUES
		('Spa', 100001, '20200320 13:00', '20200320 14:00', 'Spa'),
		('Sand Castle', 100000, '20200320 13:00', '20200320 14:00', 'Sand Castle Building')
GO

		
/*  Name: Eduardo Colon
Date: 2019-03-05 */
print '' print '*** Inserting SetupList Test Records'
GO

INSERT INTO [dbo].[SetupList]
		([SetupID], [Completed], [Description], [Comments])
	VALUES
		(100000, 0, ' Prior to Guest Arrival: Registration Desk,signs,banners', 'Banners are not ready yet'),
		(100001, 0, ' Display Equipment: Prepares for display boards,tables,chairs,, printed material and names badges','Badges are not ready yet'),
		(100002, 1, ' Check Av Equipment: Laptop,projectors :Ensure all cables,leads,laptop,mic and mouse are presented and working', 'Av Equipment is ready'),
		(100003, 1, ' Confirm that all decor and linen is in place ', 'Decor and linen are  ready'),
		(100004, 1, ' Walk through to make sure bathrooms are clean and stocked ', 'Bathrooms are  ready')
GO

-- Created: 2019-3-06
print '' print '*** Inserting Performance Test Records'
GO
INSERT INTO [dbo].[Performance]
	([PerformanceName], [PerformanceDate], [Description])
	VALUES
		('Juggler', '2018-6-27', 'It is a juggler, not much else to say'),
		('Firebreather', '2018-5-15', 'This one is for Matt LaMarche')
GO


-- Name: Eduardo Colon
-- Date: 2019-03-05
print '' print '*** Inserting SetupList Test Records'
GO
INSERT INTO [dbo].[SetupList]
		([SetupID], [Completed], [Description], [Comments])
	VALUES
		(100000, 0, ' Prior to Guest Arrival: Registration Desk,signs,banners', 'Banners are not ready yet'),
		(100001, 0, ' Display Equipment: Prepares for display boards,tables,chairs,, printed material and names badges','Badges are not ready yet'),
		(100002, 1, ' Check Av Equipment: Laptop,projectors :Ensure all cables,leads,laptop,mic and mouse are presented and working', 'Av Equipment is ready'),
		(100003, 1, ' Confirm that all decor and linen is in place ', 'Decor and linen are  ready'),
		(100004, 1, ' Walk through to make sure bathrooms are clean and stocked ', 'Bathrooms are  ready')
GO