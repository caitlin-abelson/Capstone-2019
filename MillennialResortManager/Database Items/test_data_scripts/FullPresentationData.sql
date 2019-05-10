USE [MillennialResort_DB]
GO
print' Member'
GO
INSERT INTO [Member]
	([FirstName], [LastName], [PhoneNumber], [Email], [Active])
VALUES
	('Frankie', 'Moneybags', '3194667834', 'FrankieMoneyBags@gmail.com', 0),
	('Alfonzo', 'Robero', '3194667835', 'AlfRobero@gmail.com', 0),
	('Scrooge', 'McDucksworth', '4144667836', 'ScroogeMC@gmail.com', 0)
GO

print' Guest'
GO	
INSERT INTO [dbo].[Guest]
		([MemberID], [GuestTypeID], [FirstName], 
		[LastName], [PhoneNumber], [Email], 
		[Minor], [ReceiveTexts], [EmergencyFirstName], 
		[EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation])
	VALUES
		(100000, 'Basic guest', 'Sam', 'Andrews', '3199999999', 'sam1@gmail.com', 0, 1, 'Lana', 'Andrews', '3192222222', 'Aunt'),
		(100000, 'Basic guest', 'Kelly', 'Joe', '3199999998', 'kel1@gmail.com', 0, 1, 'Beth', 'Joe', '3192222225', 'Aunt'),
		(100000, 'Basic guest', 'Dave', 'Gray', '3199999992', 'greygrey@gmail.com', 0, 1, 'Mike', 'Andrews', '3192222228', 'Uncle'),
		(100001, 'Basic guest', 'Doug', 'Cars', '3199999944', 'dougg@gmail.com', 0, 1, 'Josh', 'Andrews', '3192222277', 'Brother'),
		(100001, 'Basic guest', 'David', 'Gray', '3199999992', 'david@gmail.com', 0, 1, 'Hirk', 'Johnson', '3192222228', 'Son'),
		(100001, 'Basic guest', 'Tami', 'Selander', '3199999994', 'tamiS@gmail.com', 0, 1, 'John', 'Smith', '31922245858', 'Neighbor'),
		(100002, 'Basic guest', 'Kaitlyn', 'Ash', '3199999991', 'kaitlyn@gmail.com', 0, 1, 'Emmanuel', 'Verity', '3192448628', 'Grandmother'),
		(100002, 'Basic guest', 'Wray', 'Brauflowski', '3199999992', 'Wray@gmail.com', 0, 1, 'Earl', 'Shakespeare', '3196589228', 'sister'),
		(100002, 'Basic guest', 'Mica', 'Clemens', '3199999993', 'Mica@gmail.com', 0, 1, 'Candyce', 'Merrill', '3191548228', 'Brother'),
		(100002, 'Basic guest', 'Minta', 'Holland', '3199999921', 'Minta@gmail.com', 0, 1, 'Caryl', 'Barnett', '3196321228', 'Daughter'),
		(100002, 'Basic guest', 'Dom', 'Oliver', '3199912392', 'Dom@gmail.com', 0, 1, 'Abe', 'Harden', '3199841228', 'Niece'),
		(100002, 'Basic guest', 'Jeanne', 'Headley', '3199994212', 'Jeanne@gmail.com', 0, 1, 'Jeanette', 'Matthews', '3194899228', 'Aunt'),
		(100002, 'Basic guest', 'Roxana', 'Osborne', '3199916542', 'Roxana@gmail.com', 0, 1, 'Teresa', 'Cook', '3192456228', 'Uncle')
GO

print'Appointment Type'
GO
INSERT INTO [dbo].[AppointmentType]
		([AppointmentTypeID], [Description])
	VALUES
		('C# and Yoga', 'Modern Yoga infused with loose couplings and dependancy construction.'),
		('sql and Yoga', 'Modern Yoga infused with data redundancy.'),
		('Coal Walk', 'Ever wanted to walk on hot coals? Now is your chance!'),
		('Leg Waxing', 'It is as painful as it sounds.'),
		('Beach Massage', 'Get a relaxing massage on the beach.'),
		('Nature Walk', 'Explore the natural beauty surrounding the resort'),
		('Fishing Charter', 'Go catch the big fish and have it professionally filleted for your eating pleasure.'),
		('Spa', 'Spa'),
		('Pet Grooming', 'Pet Grooming'),
		('Turtle Petting', 'Turtle Petting'),
		('Whale Watching', 'Whale Watching'),
		('Sand Castle', 'Sand Castle Building')
GO
print'Employee'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active])
	VALUES
		('Joanne', 'Smith', '1319551111', 'joanne@company.com', 'Maintenance', 1),-- 100001
		('Martin', 'Jones', '1319551111', 'martin@company.com', 'Events', 1),
		('Leo', 'Williams', '1319551111', 'leo@company.com', 'ResortOperations', 1),
		('Joe', 'Shmoe', '1319551112', 'joe@company.com', 'Pet', 0),
		('Jon', 'Snow', '13198845112', 'jon@company.com', 'FoodService', 1),
		('Larry', 'Fitzgerald', '13194485488', 'larry@company.com', 'Ordering', 1),
		('Gordon', 'Ramsey', '13185447851', 'gordon@company.com', 'NewEmployee', 1) --100007
GO
print'Employee Role'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100001, 'Manager'),
		(100002, 'Manager'),
		(100003, 'Manager'),
		(100004, 'Worker'),
		(100005, 'Worker'),
		(100006, 'Worker'),
		(100007, 'Worker')
GO
print 'Resort Property'
GO
INSERT INTO [dbo].[ResortProperty]
([ResortPropertyTypeID])
VALUES
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"),
("Building"), --100024
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room"),
("Room")-- 100042
GO

print'Offering'
GO
INSERT INTO [dbo].[Offering]
		([OfferingTypeID], [EmployeeID], [Description], [Price], [Active])
	VALUES
		('Room', 100000, 'Sleeps 10 to 20 guests.', 30, 1),
		('Room', 100000, 'Sleeps 10 to 20 guests.', 30, 1),
		('Room', 100000, 'Sleeps 10 to 20 guests.', 30, 1),
		('Room', 100000, 'Sleeps 10 to 20 guests.', 30, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Queen Size Beds.', 150, 1),
		('Room', 100000, 'Sleeps up to 10 guests.', 30, 1),
		('Room', 100000, 'Sleeps up to 10 guests.', 30, 1),
		('Room', 100000, 'Sleeps up to 10 guests.', 30, 1),
		('Room', 100000, 'Sleeps up to 10 guests.', 30, 1),
		('Room', 100000, 'Sleeps up to 10 guests.', 30, 1),
		('Room', 100000, 'Sleeps up to 10 guests.', 30, 1), --100017 End Rooms
		
		('Item', 100000, 'Millenial Resorts ballcap', 6.00, 1),
		('Item', 100000, 'Socks with the Millenial Resorts logo.', 2.30, 1),
		('Item', 100000, 'Soap shaped like a slug', 2.50, 1),
		('Item', 100000, 'Millenial Resorts logo sweatpants.', 15.00, 1),
		('Item', 100000, 'Fresh atlantic salmon with a red wine reduction and salted potatoes', 23.00, 1),
		('Item', 100000, 'Millenial Resort sweatshirt.', 16.00, 1),
		('Item', 100000, 'Manage a resort just like the one you are staying in!', 60.00, 1),
		('Item', 100000, 'Millenial Resorts logo baby jumper.', 14.00, 1),
		('Item', 100000, '16oz Miller Lite Tall Boy', 4.30, 1),
		('Item', 100000, 'Millenial Resorts bumper sticker.', 1.30, 1),
		('Item', 100000, 'Chicken cooked with a beer can inside.', 12.00, 1),
		('Item', 100000, 'Chicken stuffed with salmon.', 25.00, 1),
		('Item', 100000, 'Chiquita banana', 1.30, 1),
		('Item', 100000, 'Roasted, smokey carrots', 2.30, 1),
		('Item', 100000, 'Nyquil cold and flu medicine.', 10.00, 1),--100032 End Item Offerings
		
		('Event', 100000, 'Fire dancing with turtles!', 10, 1),
		('Event', 100000, 'Splash and drink and play with foam launchers', 5, 1),
		('Event', 100000, 'Comedic Event', 20, 1),
		('Event', 100000, 'Penn and Teller', 45, 1),
		('Event', 100000, "Bob and Jim's Wedding", 0, 1),-- 100037 End Events
		
		('Service', 100000, "Get a relaxing massage on the beach.", 45, 1),
		('Service', 100000, "Explore the natural beauty surrounding the resort", 5.00, 1),
		('Service', 100000, "Go catch the big fish and have it professionally filleted for your eating pleasure.", 100, 1),
		('Service', 100000, "Relaxing spa  experience.", 30, 1),
		('Service', 100000, "Pet Grooming", 15, 1),
		('Service', 100000, "Turtle Petting", 0, 1),
		('Service', 100000, "Whale Watching", 25, 1),
		('Service', 100000, "Sand Castle Building", 3.00, 1) -- 100045 End Services
		
GO
print 'Building'
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
		('GenBld01', 'Sea Cow Storage', '812 South Padre', 'Storage', 'Available', 100006),
		('North Shore1', 'Beach House', '101 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100007),
		('North Shore2', 'Bungalow Land', '102 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100008),
		('North Shore3', 'Bungalow Sea', '103 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100009),
		('North Shore4', 'Royal Suite', '104 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100010),
		('East Shore1', 'Beach House', '105 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100011),
		('East Shore2', 'Bungalow Land', '106 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100012),
		('East Shore3', 'Bungalow Sea', '107 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100013),
		('East Shore4', 'Royal Suite', '108 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100014),
		('West Shore1', 'Beach House', '109 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100015),
		('West Shore2', 'Bungalow Land', '110 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100016),
		('West Shore3', 'Bungalow Sea', '111 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100017),
		('West Shore4', 'Royal Suite', '112 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100018),
		('South Shore1', 'Beach House', '109 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100019),
		('South Shore2', 'Bungalow Land', '110 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100020),
		('South Shore3', 'Bungalow Sea', '111 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100021),
		('South Shore4', 'Royal Suite', '112 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100022),
		('East Shore5', 'Hostel Hut', '113 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100023),
		('South Shore5', 'Hostel Hut', '114 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100024)
		
GO

print 'Rooms'
GO
INSERT INTO [dbo].[Room]
		([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [ResortPropertyID], [OfferingID], [RoomStatusID])
	VALUES
	

    	(114, 'North Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100025, 100000, 'Available'),
		(115, 'East Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100026, 100001, 'Occupied'),
		(116, 'West Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100027, 100002, 'Available'),
		(117, 'South Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100028, 100003, 'Occupied'),
		(118, 'North Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100029, 100004, 'Available'),
		(119, 'East Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100030, 100005, 'Occupied'),
		(120, 'West Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100031, 100006, 'Available'),
		(121, 'South Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100032, 100007, 'Occupied'),
		(122, 'North Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100033, 100008, 'Available'),
		(123, 'East Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100034, 100009, 'Occupied'),
		(124, 'West Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100035, 100010, 'Available'),
		(125, 'South Shore3', 'Bungalow Sea', 'Queen Size Beds.', 2, 100036, 100011, 'Occupied'),
		(126, 'North Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100037, 100012, 'Available'),
		(127, 'East Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100038, 100013, 'Occupied'),
		(128, 'West Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100039, 100014, 'Available'),
		(129, 'South Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100040, 100015, 'Occupied'),
		(130, 'East Shore5', 'Hostel Hut', 'Sleeps up to 10 guests.', 5, 100041, 100016, 'Occupied'),
		(131, 'South Shore5', 'Hostel Hut', 'Sleeps up to 10 guests.', 20, 100042, 100017, 'Occupied')
	GO
print 'Reservation'
GO
	INSERT INTO [Reservation]
	([MemberID], [NumberOfGuests], [NumberOfPets], [ArrivalDate], [DepartureDate], [Notes])
	VALUES 
	(100000, 3, 0, '2019-05-07', '2019-05-10', ''),
	(100001, 3, 0, '2019-05-09', '2019-05-17', ''),
	(100002, 7, 0, '2019-05-10', '2019-05-15', '')
GO

print 'Member Tab'
GO
	INSERT INTO [MemberTab]
	([MemberID], [TotalPrice], [Active])
	VALUES 
	(100000, 0, 1),
	(100001, 0, 1),
	(100002, 0, 1)
GO

print 'Recipe'
GO
INSERT INTO [dbo].[Recipe]
([Name],[Description],[DateAdded],[Active])
VALUES
('Red Wine Salmon and Potatoes','Salmon seared in a red wine reduction for 10 minutes and served with salty potatoes.','2019-03-05',1),
('Beer Can Chicken','Place chicken on grill with beer inside. Wait until cooked.','2019-06-09',1),
('Poultry and Pescado','Salt both salmon and chicken. Stuff chicken with salmon once both are cooked.','2019-05-03',1)

GO

print '' print '*** Adding Item Data'
GO
INSERT INTO [dbo].[Item]
(
	[OfferingID], [ItemTypeID], [RecipeID], [CustomerPurchasable], [Description], 
	[OnHandQty], [Name], [ReOrderQty]
)
VALUES
	(100018, 'Clothing', NULL, 1, 'Millenial Resorts ballcap', 50, 'Millenial Resorts Ballcap', 15),
	(NULL, 'Food', NULL, 1, 'Fresh Atlantic Salmon', 50, 'Fresh Atlantic Salmon', 15),
	(100019, 'Clothing', NULL, 1, 'Socks with the Millenial Resorts logo.', 50, 'Millenial Resorts Cozy Socks', 15),
	(NULL, 'Food', NULL, 1, '1lb Yukon Gold Potatoes in a plastic sack.', 50, '1lb Yukon Gold Potatoes', 15),
	(100020, 'Room Amenities', NULL, 1, 'Soap shaped like a slug', 50, 'Slug Soap', 15),
	(NULL, 'Food', NULL, 1, 'A red wine for cooking.', 50, 'Roland Red Cooking Wine', 15),
	(100021, 'Clothing', NULL, 1, 'Millenial Resorts logo sweatpants.', 50, 'Millenial Resorts Joggers', 15),
	(NULL, 'Food', NULL, 1, '1lb sea salt bag.', 50, '1lb Morton Sea Salt', 15),
	(100022, 'Food', 100000, 1, 'Fresh atlantic salmon with a red wine reduction and salted potatoes ', 50, 'Red Wine Salmon and Potatoes', 15),
	(NULL, 'Food', NULL, 1, 'Large whole chicken', 50, 'Hormel Whole Chicken', 15),
	(100023, 'Clothing', NULL, 1, 'Millenial Resort sweatshirt.', 50, 'Millenial Resort Sweatshirt ', 15),
	(NULL, 'Food', NULL, 1, '1lb of peppercorns', 50, '1lb Peppercorn', 15),
	(100024, 'Misc', NULL, 1, 'Manage a resort just like the one you are staying in.', 50, 'Millenial Resort Simulator', 15),
	(NULL, 'Food', NULL, 0, 'Lean Ground Beef 10 lb', 75, 'Lean Ground Beef 10 lb', 25),
	(100025, 'Clothing', NULL, 0, 'Millenial Resorts logo baby jumper.', 75, 'Millenial Resorts Baby Jumper', 25),
	(100026, 'Food', NULL, 1, '16oz Miller Lite Tall Boy', 5000, '16oz. Miller Lite', 1000),
	(100027, 'Misc', NULL, 1, 'Millenial Resorts bumper sticker.', 50, 'Bumper Sticker', 15),
	(NULL, 'Food', NULL, 1, '48 fluid oz. bottle of vegetable oil', 50, 'Crisco Vegetable Oil', 15),
	(100028, 'Food', 100001, 1, 'Chicken cooked with a beer can inside.', 50, 'Beer Can Chicken', 15),
	(100029, 'Food', 100002, 1, 'Chicken stuffed with salmon.', 50, 'Poultry and Pescado', 15),
	(100030, 'Food', NULL, 1, 'Chiquita banana', 50, 'Chiquita Banana', 15),
	(100031, 'Food', NULL, 1, 'Roasted, smokey carrots', 50, 'Roasted Carrots', 15),
	(100032, 'Misc', NULL, 1, 'Nyquil cold and flu medicine.', 50, 'Nyquil', 15), -- End of Offering Items
	(NULL, 'Office Supplies', NULL, 1, '3000 count staple container', 50, 'Staple Refill Package', 15),
	(NULL, 'Office Supplies', NULL, 1, '10 lbs of printer paper', 50, 'printer Paper Refill - 10lbs', 15),
	(NULL, 'Cleaning Items', NULL, 1, 'Large container of bleach.', 50, 'Clorox Bleach - Bargain Size', 15)
GO

print 'Recipe Item Lines'
GO
INSERT INTO [dbo].[RecipeItemLine]
	([RecipeID],[ItemID],[Quantity],[UnitOfMeasure])
	VALUES
		(100000,100001,1,"fillet"),
		(100000,100003,2,"potatoes"),
		(100000,100005,4,"fluid oz."),
		(100000,100007,2,"tablespoons"), -- Salmon and Potatoes

		(100001,100009,2,"whole"),
		(100001,100015,2,"can"),
		(100001,100007,2,"tablespoons"), -- Beer Can Chicken

		(100002,100009,1,"whole"),
		(100002,100001,1,"fillet"),
		(100002,100007,2,"tablespoons")
GO

print 'Maintanance'
GO
INSERT INTO [dbo].[MaintenanceStatus]
	([MaintenanceStatusID],[Description])
VALUES
	("Complete","Completed"),
	("Working","In the process of being completed"),
	("Waiting","Waiting to be started")
Go

print 'Maintenance Type'
GO
INSERT INTO [dbo].[MaintenanceType]
	([MaintenanceTypeID],[Description],[Active])
VALUES
	("Repair","Something needs to be repaired",1),
	("Replace","Something needs to be replaced",1),
	("Install","Something needs to be installed",1),
	("Remove","Something needs to be removed",1)
GO

print 'Maintenance Work Order'
GO
INSERT INTO [dbo].[MaintenanceWorkOrder]
	([MaintenanceTypeID],[DateRequested],[DateCompleted],[RequestingEmployeeID],[WorkingEmployeeID],[Complete],[Description],[Comments],[MaintenanceStatus],[ResortPropertyID])
VALUES
	("Repair","2018-10-10",NULL,100002, 100001,0,"The toilet is leaking",NULL,"Waiting",100025),
	("Replace","2018-01-10",NULL,100002,100001,0,"Faucet needs to be replaced",NULL,"Waiting",100026),
	("Install","2019-10-05",NULL,100002,100001,0,"Ceiling fan is needed",NULL,"Waiting",100027),
	("Repair","2018-10-10",NULL,100002, 100001,0,"Rug has a frayed end",NULL,"Waiting",100028),
	("Repair","2018-10-10",NULL,100002, 100001,0,"Mini Bar is not cold",NULL,"Waiting",100029),
	("Remove","2018-10-10",NULL,100002, 100001,0,"Extra towel rack in bathroom",NULL,"Waiting",100030),
	("Install","2018-10-10",NULL,100004,100001,0,"New air filter",NULL,"Waiting",100031),
	("Repair","2018-10-10",NULL,100007, 100001,0,"Window is not sealing fully",NULL,"Waiting",100032)
GO

print 'Inspection'
GO
INSERT INTO [dbo].[Inspection]
		([ResortPropertyID], [Name], [DateInspected], [Rating], 
		[ResortInspectionAffiliation], [InspectionProblemNotes], [InspectionFixNotes])
	VALUES
		(100032, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100032, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", ""),
		(100031, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100031, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", ""),
		(100030, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100030, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", ""),
		(100029, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100029, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", ""),
		(100028, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100028, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", ""),
		(100027, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100027, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", ""),
		(100026, 'Sprinker Systems' ,'2018-01-01', 'Pass', 
			"", "", ""),
		(100026, 'Elevator' ,'2018-08-12', 'Pass', 
			"", "", "")
GO

print 'Events'
GO
INSERT INTO [Event]
(
	[OfferingID],[EventTypeID], [EventStartDate], 
	[NumGuests], [SeatsRemaining], [PublicEvent], [Description], [KidsAllowed], 
	[Location], [EventEndDate], 
	[EventTitle], [Sponsored], [EmployeeID], [Approved], [Cancelled]
)
VALUES
	(100033, "Party", CURRENT_TIMESTAMP,
	40, 30, 1, "Fire dancing with turtles!", 1, 
	"On the beach by Frank's nasty shack.", CURRENT_TIMESTAMP, 
	"Fire Dancing with Turtles", 0, 100000, 1, 0),
	
	(100034, "Party", CURRENT_TIMESTAMP,
	40, 30, 1, "Splash and drink and play with foam launchers", 1, 
	"Main Pool", CURRENT_TIMESTAMP, 
	"Foam Pool Party", 0, 100000, 1, 0),
	
	(100035, "Trade Show", CURRENT_TIMESTAMP,
	40, 30, 1, "Comedic Event ", 1, 
	"Main Ballroom", CURRENT_TIMESTAMP, 
	"John Mulaney", 0, 100000, 1, 0),
	
	(100036, "Trade Show", CURRENT_TIMESTAMP,
	40, 30, 1, "Penn and Teller", 1, 
	"Beach Front Patio", CURRENT_TIMESTAMP, 
	"Penn and Teller's Famous Magic Show", 0, 100000, 1, 0),
	
	(100037, "Wedding", "20190606 10:00:00 AM",
	100, 20, 0, "Bob and Jim's Wedding", 1, 
	"Main Ballroom", "20190606 10:00:00 PM", 
	"Bob and Jim's Wedding", 0, 100000, 1, 0)
GO

print 'Sponsor'
GO
INSERT INTO [dbo].[Sponsor]
([SponsorID],[Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],[ContactLastName],[DateAdded],[Active])
VALUES
(100000,"We are happy to Help!","601 Circle St.","Cedar Rapids","IA","13195548876","val@here.com","Val","Walmsley","2019-01-01",1),
(100001,"Coca Cola","559 Riverview Dr.","Fuquay","NC","13191254876","tansy@here.com","Tansy","White","2019-01-01",1),
(100002,"Twitch","754 Fawn Ct.","Harrisonburg","VA","13195548876","rene@here.com","Rene","Spence","2019-01-01",1),
(100003,"Kars4Kids","343 Maple Ave","Santa Clara","CA","13195548876","lucile@here.com","Lucile","Atteberry","2019-01-01",1),
(100004,"Joe Biden","908 Pierce St","Marietta","GA","13195548876","joe@here.com","Joe","Biden","2019-01-01",1),
(100005,"AMC","88 Mayfair Rd.","Champlin","MN","13195548876","honour@here.com","Honour","Constable","2019-01-01",1)
GO

print 'Event Sponsor'
GO
INSERT INTO [dbo].[EventSponsor]
([EventID],[SponsorID])
VALUES
(100000,100000),
(100002, 100001),
(100004, 100004)
GO

print 'Setup'
GO
INSERT INTO [dbo].[Setup]
([EventID],[DateEntered],[DateRequired],[Comments])
VALUES
(100000,'2019-05-06',CURRENT_TIMESTAMP,'Please do this quickly'),
(100001,'2019-05-06',CURRENT_TIMESTAMP, ''),
(100002,'2019-05-06',CURRENT_TIMESTAMP,'Requires extensive remodeling'),
(100003,'2019-05-06',CURRENT_TIMESTAMP,'Use the forklift')
GO

print 'Setup List'
GO
INSERT INTO [dbo].[SetupList]
		([SetupID], [Completed], [Description], [Comments])
	VALUES
		(100000, 0, ' Prior to Guest Arrival: Registration Desk,signs,banners', 'Banners are not ready yet'),
		(100001, 0, ' Display Equipment: Prepares for display boards,tables,chairs,, printed material and names badges','Badges are not ready yet'),
		(100002, 1, ' Check Av Equipment: Laptop,projectors :Ensure all cables,leads,laptop,mic and mouse are presented and working', 'Av Equipment is ready'),
		(100002, 1, ' Walk through to make sure bathrooms are clean and stocked ', 'Bathrooms are  ready'),
		(100003, 1, ' Confirm that all decor and linen is in place ', 'Decor and linen are  ready')
		
GO

print 'Performance'
GO
INSERT INTO [dbo].[Performance]
	([PerformanceTitle], [PerformanceDate], [Description],[Cancelled])
	VALUES
		('Juggler', '2018-6-27', 'It is a juggler, not much else to say',0),
		('Firebreather', '2018-5-15', 'This one is for Matt LaMarche',0),
		('Sword Swallower', '2018-6-27', 'Try not to cringe',0),
		('Magician', '2018-6-20', 'Come be amazed by magic',0),
		('Michael Jackson', '2018-6-17', 'His hologram will be preforming hit songs by the iconic Michael',0),
		('John Moleny', '2018-6-7', 'You will laugh at this comedic act.',0),
		('Blue Man Group', '2018-6-4', 'World Famous Blue Man Group is coming. You will not want to miss out!',0),
		('Lion King', '2018-5-30', 'Come watch the broadway performance known the world over.',0)
GO

print 'Services'
GO
INSERT INTO [dbo].[ServiceComponent]
([ServiceComponentID],[OfferingID],[Duration],[Description],[Active])
VALUES
('Beach Massage', 100038, 30, 'Get a relaxing massage on the beach.',1),
('Nature Walk',  100039, 60,'Explore the natural beauty surrounding the resort',1),
('Fishing Charter', 100040, 240, 'Go catch the big fish and have it professionally filleted for your eating pleasure.',1),
('Spa', 100041, 30,'Relaxing spa experience.',1),
('Pet Grooming',  100042, 30,'Pet Grooming',1),
('Turtle Petting', 100043, 30, 'Turtle Petting',1),
('Whale Watching',  100044, 120,'Whale Watching',1),
('Sand Castle Building',  100045, 30,'Sand Castle Building',1)
GO

print 'Member Tab Line'
GO
	INSERT INTO [MemberTabLine]
	([MemberTabID], [OfferingID], [Quantity], [Price], [EmployeeID], [Discount], [GuestID], [DatePurchased])
	VALUES 
	-- Rooms for Member 100000
	(100000, 100000, 1, 30, null, 0, null, '2019-04-07'),
	(100000, 100001, 1, 30, null, 0, null, '2019-04-07'),
	(100000, 100002, 1, 30, null, 0, null, '2019-04-07'), 
	
	-- Purchases on Member 100000 Tab	05-07 -> 05-10 Guest IDs 100000 -> 100002
	(100000, 100018, 1, 6.00, null, 0, 100000, '2019-05-07'),
	(100000, 100020, 3, 2.50, null, 0, 100001, '2019-05-07'),
	(100000, 100022, 3, 23.00, null, 0, 100000, '2019-05-07'),
	(100000, 100033, 2, 10, null, 0, 100001, '2019-05-07'),
	(100000, 100026, 3, 4.30, null, 0, 100000, '2019-05-07'),
	(100000, 100032, 1, 10.00, null, 0, 100002, '2019-05-08'),
	(100000, 100021, 1, 15.00, null, 0, 100000, '2019-05-08'),
	(100000, 100026, 3, 4.30, null, 0, 100002, '2019-05-10'),
	(100000, 100032, 1, 10.00, null, 0, 100001, '2019-05-09'),

	-- Rooms for Member 100001
	(100000, 100004, 1, 150, null, 0, null, '2019-04-07'),
	(100000, 100005, 1, 150, null, 0, null, '2019-04-07'),
	(100000, 100006, 1, 150, null, 0, null, '2019-04-07'), 
	
	-- Purchases on Member 100001 Tab	05-09 -> 05-17 Guest IDs 100003 -> 100005
	(100001, 100018, 1, 6.00, null, 0, 100003, '2019-05-09'),
	(100001, 100020, 3, 2.50, null, 0, 100004, '2019-05-09'),
	(100001, 100022, 3, 23.00, null, 0, 100004, '2019-05-10'),
	(100001, 100033, 2, 10, null, 0, 100003, '2019-05-10'),
	(100001, 100026, 3, 4.30, null, 0, 100004, '2019-05-11'),
	(100001, 100032, 1, 10.00, null, 0, 100005, '2019-05-12'),
	(100001, 100021, 1, 15.00, null, 0, 100003, '2019-05-12'),
	(100001, 100026, 3, 4.30, null, 0, 100003, '2019-05-12'),
	(100001, 100032, 1, 10.00, null, 0, 100005, '2019-05-13'),
	
	(100001, 100038, 1, 45, null, 0, 100003, '2019-05-14'),
	(100001, 100040, 3, 100, null, 0, 100004, '2019-05-14'),
	(100001, 100028, 3, 12.00, null, 0, 100004, '2019-05-15'),
	(100001, 100033, 2, 10, null, 0, 100003, '2019-05-15'),
	(100001, 100028, 3, 12.00, null, 0, 100004, '2019-05-16'),
	(100001, 100018, 1, 6.00, null, 0, 100005, '2019-05-17'),
	(100001, 100028, 3, 12.00, null, 0, 100003, '2019-05-17'),
	(100001, 100026, 3, 4.30, null, 0, 100003, '2019-05-17'),
	(100001, 100032, 1, 10.00, null, 0, 100005, '2019-05-17'),
	
	
	-- Rooms for Member 100002
	(100002, 100017, 7, 30, null, 0, null, '2019-04-07'),
	
	-- Purchases on Member 100002 Tab	05-10 -> 05-15 Guest IDs 100006 -> 1000012
	(100002, 100018, 1, 6.00, null, 0, null, '2019-05-07'),
	(100002, 100019, 1, 2.30, null, 0, null, '2019-05-07'),
	(100002, 100021, 1, 15.00, null, 0, null, '2019-05-07'),
	(100002, 100023, 1, 16.00, null, 0, null, '2019-05-07'),
	(100002, 100026, 1, 4.30, null, 0, null, '2019-05-07'),
	(100002, 100030, 20, 1.30, null, 0, null, '2019-05-08'),
	
	(100002, 100038, 1, 45, null, 0, 100006, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100006, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100006, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100006, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100006, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100006, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100006, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100006, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100006, '2019-05-15'),
	
	(100002, 100038, 1, 45, null, 0, 100007, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100007, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100007, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100007, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100007, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100007, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100007, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100007, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100007, '2019-05-15'),
	
	(100002, 100038, 1, 45, null, 0, 100008, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100008, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100008, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100008, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100008, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100008, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100008, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100008, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100008, '2019-05-15'),
	
	(100002, 100038, 1, 45, null, 0, 100009, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100009, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100009, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100009, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100009, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100009, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100009, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100009, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100009, '2019-05-15'),
	
	(100002, 100038, 1, 45, null, 0, 100010, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100010, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100010, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100010, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100010, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100010, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100010, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100010, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100010, '2019-05-15'),
	
	(100002, 100038, 1, 45, null, 0, 100011, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100011, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100011, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100011, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100011, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100011, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100011, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100011, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100011, '2019-05-15'),
	
	(100002, 100038, 1, 45, null, 0, 100012, '2019-05-10'),
	(100002, 100040, 1, 100, null, 0, 100012, '2019-05-10'),
	(100002, 100028, 1, 12.00, null, 0, 100012, '2019-05-11'),
	(100002, 100033, 1, 10, null, 0, 100012, '2019-05-11'),
	(100002, 100028, 1, 12.00, null, 0, 100012, '2019-05-12'),
	(100002, 100018, 1, 6.00, null, 0, 100012, '2019-05-12'),
	(100002, 100028, 1, 12.00, null, 0, 100012, '2019-05-13'),
	(100002, 100026, 1, 4.30, null, 0, 100012, '2019-05-14'),
	(100002, 100032, 1, 10.00, null, 0, 100012, '2019-05-15')
GO

print '' print '*** INSERT INTO [RoomReservation]'	
INSERT INTO [RoomReservation]
		([RoomID], [ReservationID], [CheckinDate])
	VALUES
		(100000, 100000, '2019-05-07 10:00:00'),
		(100001, 100000, '2019-05-07 10:00:00'),
		(100002, 100000, '2019-05-07 10:00:00'), -- 1st tab
		
		(100004, 100001, '2019-05-09 9:30:00'),
		(100005, 100001, '2019-05-09 9:30:00'),
		(100006, 100001, '2019-05-09 9:30:00'), -- 2nd tab
		
		(100017, 100002, '2019-05-10 9:30:00') -- 3rd tab
		
GO	

print 'Guest Room Assignment'
GO
INSERT INTO [dbo].[GuestRoomAssignment]
	([GuestID],[RoomReservationID], [CheckinDate])
VALUES 
	(100000,100000, '2019-05-07 10:00:00'),
	(100001,100001, '2019-05-07 10:00:00'),
	(100002,100002, '2019-05-07 10:00:00'), -- Tab 1
	
	(100003,100003, '2019-05-09 9:30:00'),
	(100004,100004, '2019-05-09 9:30:00'),
	(100005,100005, '2019-05-09 9:30:00'), -- Tab 2
	
	(100006,100006, '2019-05-10 9:30:00'),
	(100007,100006, '2019-05-10 9:30:00'),
	(100008,100006, '2019-05-10 9:30:00'),
	(100009,100006, '2019-05-10 9:30:00'),
	(100010,100006, '2019-05-10 9:30:00'),
	(100011,100006, '2019-05-10 9:30:00'),
	(100012,100006, '2019-05-10 9:30:00')  -- Tab 3
GO

print '' print '*** Adding Supplier Data'
GO

INSERT INTO [dbo].[Supplier]
([Name],[Address],[City],[State],[PostalCode],[Country],[PhoneNumber],[Email],[ContactFirstName],[ContactLastName],
[DateAdded],[Description],[Active])
VALUES
('MySupply','123 Main St','Iowa City','IA','60013','USA','8155554488','place@place.com','John','Cena','2019-03-05','Description',1),
('Joes Supply Shed','149 E Pulaski St','Olive Branch','MA','38654','USA','8155554321','glenna@place.com','Glenna','Smalls','2019-03-05','Description',1),
('Grainger','141 Rockville Ave.','W Bloomfield','MI','48322','USA','8155554123','beau@place.com','Beau','Carpenter','2019-03-05','Description',1),
('Cisco','38 Cedar Swamp St','Aberdeen','SD','57401','USA','8155554654','angie@place.com','Angie','Wright','2019-03-05','Description',1),
('US Foods','9326 Rocky River Ave.','Lutherville','MD','21093','USA','8155554845','norman@place.com','Norman','Dorsey','2019-03-05','Description',1),
('Swift','2 Bald Hill Circle','Hanover Park','IL','60133','USA','8155554155','wilford@place.com','Wilford','Rye','2019-03-05','Description',1),
('Hungry Hobo','93 Talbot Dr','Hamtramck','MI','48212','USA','8154891566','nick@place.com','Nick','Wyndham','2019-03-05','Description',1),
('Food Supply','100 Hungry Court','Spoon River','WI','53534','USA','6085551566','archie@place.com','Archie','Manning','2019-03-05','Description',1),
('Millennial Resort','1 West Idyllic Avenue','Nirvana','FL','32000','USA','4854854850','jimg@place.com','Jim','Edinburgh','2019-03-05','Description',1)
GO
print '' print '*** Adding Supplier Order Data'
GO

print '' print '*** Adding ItemSupplier Data'
GO

INSERT INTO [dbo].[ItemSupplier]
([ItemID],[SupplierItemID],[SupplierID],[PrimarySupplier],[LeadTimeDays],[UnitPrice],[Active])
VALUES
(100000,0,100000,1,1, 50.00, 1),
(100001,0,100000,1,1, 400.00, 1),
(100002,0,100000,1,1, 50.00, 1),
(100003,0,100000,1,1, 100.00, 1),
(100004,0,100000,1,1, 100.00, 1),
(100005,0,100000,1,1, 100.00, 1),
(100006,0,100000,1,1, 200.00, 1),
(100007,0,100000,1,1, 50.00, 1),
(100008,0,100000,1,1, 100.00, 1),
(100009,0,100000,1,1, 50.00, 1),
(100010,0,100000,1,1, 25.00, 1),
(100011,0,100000,1,1, 100.00, 1),
(100012,0,100000,1,1, 50.00, 1),
(100013,0,100000,1,1, 25.00, 1),
(100014,0,100000,1,1, 100.00, 1),
(100015,0,100000,1,1, 50.00, 1),
(100016,0,100000,1,1, 25.00, 1),
(100017,0,100000,1,1, 100.00, 1),
(100018,0,100000,1,1, 50.00, 1),
(100019,0,100000,1,1, 25.00, 1),
(100020,0,100000,1,1, 100.00, 1),
(100021,0,100000,1,1, 50.00, 1),
(100022,0,100000,1,1, 25.00, 1),
(100023,0,100000,1,1, 100.00, 1),
(100024,0,100000,1,1, 50.00, 1),
(100025,0,100000,1,1, 25.00, 1)
GO
