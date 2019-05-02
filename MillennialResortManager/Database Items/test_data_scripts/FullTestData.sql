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
		('test', 'test1')
GO

INSERT INTO [dbo].[Department]
		([DepartmentID], [Description])
	VALUES
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
		('Joe', 'Shmoe', '1319551112', 'joe@company.com', 'Grooming', 0),
		('Jon', 'Snow', '13198845112', 'jon@company.com', 'Talent', 1),
		('Larry', 'Fitzgerald', '13194485488', 'larry@company.com', 'Grooming', 1),
		('Gordon', 'Ramsey', '13185447851', 'gordon@company.com', 'Kitchen', 1),
		('Jim', 'Glasgow', '13194445135', 'jim@company.com', 'Events', 1)
		
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
		(100003, 'Admin'),
		(100004, 'Maintenance'),
		(100004, 'Procurement'),
		(100005, 'Manager'),
		(100005, 'Maintenance'),
		(100006, 'Procurement'),
		(100006, 'Manager'),
		(100007, 'Maintenance')
GO

INSERT INTO [dbo].[ItemType]
		([ItemTypeID],[Description])
	VALUES
		('Food','Food'),
		('Hot Sauce','Hot Sauce'),
		('Shoe','Shoes'),
		('Hat','Hats'),
		('Tshirt','Tshirts'),
		('Pet','Pets'),
		('Beverage','Drinks')
GO


INSERT INTO [dbo].[PetType]
		([PetTypeID], [Description])
	VALUES
		('Dog', 'Best Dog in the World'),
		('Cat', 'Best Cat in the World'),
		('CatDog', 'Best MonkeyCat in the World')
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

INSERT INTO [dbo].[EventType]
		([EventTypeID], [Description])
	VALUES
		('Concert Event', 'A concert is a live music performance in front of an audience.'),
		('Beach Party', 'There are plenty of opportunities to have a great time at the beach.'),
		('Wedding', 'Romantic Florals typically make up a romantic wedding also those who never been one to take the normal route?'),
		('Pool Party', 'Join everyone in the pool for a fun party including foam blasters!'),
		('Beach Lunch', 'Enjoy a buffet style lunch right on the beach'),
		('Board Games', 'Relax and play some fun board games. Until grandma lands on Boardwalk and flips the board.')
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

INSERT INTO [dbo].[Member]
([FirstName],[LastName],[PhoneNumber],[Email],[PasswordHash],[Active])
VALUES
("Graham","Jepson",13194485541,"graham@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1),
("Barb","Sudworth",18485160965,"barb@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1),
("Cleo","Burrell",18151478513,"cleo@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1),
("Selma","Spencer",13194448481,"selma@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1),
("Nikolas","Bennet",13146885541,"nick@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1),
("Aliah","Clemens",13194412341,"aliah@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1),
("Cierra","Southgate",13649485541,"southward1899@hotmail.com","5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",1)
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
		(100000, 'Basic guest', 'Doug', 'Cars', '3199999944', 'dougg@gmail.com', 0, 1, 'Josh', 'Andrews', '3192222277', 'Brother'),
		(100000, 'Basic guest', 'David', 'Gray', '3199999992', 'david@gmail.com', 0, 1, 'Hirk', 'Johnson', '3192222228', 'Son'),
		(100000, 'Basic guest', 'Tami', 'Selander', '3199999994', 'tamiS@gmail.com', 0, 1, 'John', 'Smith', '31922245858', 'Neighbor'),
		(100000, 'Basic guest', 'Kaitlyn', 'Ash', '3199999991', 'kaitlyn@gmail.com', 0, 1, 'Emmanuel', 'Verity', '3192448628', 'Grandmother'),
		(100000, 'Basic guest', 'Wray', 'Brauflowski', '3199999992', 'Wray@gmail.com', 0, 1, 'Earl', 'Shakespeare', '3196589228', 'sister'),
		(100000, 'Basic guest', 'Mica', 'Clemens', '3199999993', 'Mica@gmail.com', 0, 1, 'Candyce', 'Merrill', '3191548228', 'Brother'),
		(100000, 'Basic guest', 'Minta', 'Holland', '3199999921', 'Minta@gmail.com', 0, 1, 'Caryl', 'Barnett', '3196321228', 'Daughter'),
		(100000, 'Basic guest', 'Dom', 'Oliver', '3199912392', 'Dom@gmail.com', 0, 1, 'Abe', 'Harden', '3199841228', 'Niece'),
		(100000, 'Basic guest', 'Jeanne', 'Headley', '3199994212', 'Jeanne@gmail.com', 0, 1, 'Jeanette', 'Matthews', '3194899228', 'Aunt'),
		(100000, 'Basic guest', 'Roxana', 'Osborne', '3199916542', 'Roxana@gmail.com', 0, 1, 'Teresa', 'Cook', '3192456228', 'Uncle')
GO
INSERT INTO [dbo].[Pet]
		([PetName], [Gender], [Species], [PetTypeID], [GuestID])
	VALUES
		('Bandit', 'Male', 'Labrador', 'Dog', 100000),
		('Birdo', 'Female', 'Tabby', 'Cat', 100001),
		('Whiskers', 'Neutral', 'MonkeyCat', 'CatDog', 100002),
		('Spike', 'Male', 'Flowerhorn', 'Cat', 100003),
		('Gus', 'Male', 'Yellow Lab', 'Dog', 100004),
		('Onna', 'Female', 'Mixed', 'Cat', 100003)
GO
INSERT INTO [dbo].[PetImageFileName]
		([Filename], [PetID])
	VALUES 
		('pet1.jpg', 100000),
		('pet2.jpg', 100001),
		('pet3.jpg', 100002)
GO


INSERT INTO [dbo].[Performance]
		([PerformanceDate], [Description], [PerformanceTitle], [Cancelled])
	VALUES
		('2019-06-27', 'Firebreather, Nuf said', 'Firebreather', 0),
		('2019-08-15', 'Jason the just alright Juggler', 'Juggler', 0),
		('2019-09-02', 'Timmy the two ton terror', 'Sumo Championship', 0)
GO

INSERT INTO [ShuttleReservation]
		([GuestID],[EmployeeID] ,[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime])
	VALUES
		(100000,100000 , '1700 Millenium Resort Avenue' , '900 Kirkwood Avenue'  ,'2019-05-01' , '2019-05-01'),
		(100001,100001 , '1800 Millenium Resort Avenue' , '5150 Place Center'  ,'2019-05-03' , '2019-05-03'),
		(100002,100002 , '1500 Millenium Resort Avenue' , '500 Plaza Center'  ,'2019-05-04' , '2019-05-04')
GO

INSERT INTO [dbo].[ResortPropertyType]
([ResortPropertyTypeID])
VALUES
("Shack"),
("Garage")
GO



INSERT INTO [dbo].[ResortProperty]
([ResortPropertyTypeID])
VALUES
("Building"),
("Building"),
("Building"),
("Shack"),
("Building"),
("Garage"),
("Building"),
("Building"),
("Garage"),
("Building"),
("Building"),
("Garage"),
("Building"),
("Building"),
("Shack"),
("Building")
GO

INSERT INTO [Offering]
(
	[OfferingTypeID], [EmployeeID], [Description], [Price]
)
VALUES
	("Item", 100000, "Turtle of Doom", 10),
	("Event", 100000, "Fire Dancing", 13.45),
	("Room", 100000, "Turtle Room", 130.45),
	("Service", 100000, "Turtle Massage", 130.45),
	("Item", 100000, "Big ol Steak", 25.45),
	("Event", 100000, "Fishing Charter", 85.45),
	("Service", 100000, "Bed turn down", 1),
	("Item", 100000, "Pack of cards", 5.99),
	("Room", 100000, "Ocean View 2 Queen", 250),
	("Room", 100000, "Garbage View Single", 65),
	("Event", 100000, "Comedic Act", 25),
	("Event", 100000, "Magician", 15),
	("Room", 100000, "Ocean View 2 Queen", 250),
	("Room", 100000, "Ocean View 2 Queen", 350),
	("Room", 100000, "Ocean View 2 Queen", 450),
	("Room", 100000, "Ocean View 2 Queen", 850),
	("Room", 100000, "Ocean View 2 Queen", 150)
GO

INSERT INTO [dbo].[RoomStatus]
([RoomStatusID],[Description])
VALUES
('Maintenance','Needs maintenance')
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

		
		('North Shore1', 'Beach House', '101 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('North Shore2', 'Bungalow Land', '102 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('North Shore3', 'Bungalow Sea', '103 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('North Shore4', 'Royal Suite', '104 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('East Shore1', 'Beach House', '105 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('East Shore2', 'Bungalow Land', '106 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('East Shore3', 'Bungalow Sea', '107 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('East Shore4', 'Royal Suite', '108 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('West Shore1', 'Beach House', '109 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('West Shore2', 'Bungalow Land', '110 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('West Shore3', 'Bungalow Sea', '111 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('West Shore4', 'Royal Suite', '112 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('South Shore1', 'Beach House', '109 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('South Shore2', 'Bungalow Land', '110 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('South Shore3', 'Bungalow Sea', '111 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('South Shore4', 'Royal Suite', '112 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('East Shore5', 'Hostel Hut', '113 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006),
		('South Shore5', 'Hostel Hut', '114 Beach Road Way', 'Guest Hotel Rooms', 'Available', 100006)













		
GO

INSERT INTO [dbo].[RoomType]
([RoomTypeID],[Description],[Active])
VALUES
('Double','Two double beds',1),
('Honeymoon Suite','Biggest, best room we have',1),
('Single','One single bed',1),
('Family','Two Queen beds',1),
('King','King bed and a bath tub',1),
('Fishing Suite','King bed and a fish tank',1),
('Presidential Suite','Penthouse Suite',1),

('Bungalow Land', 'Queen Size Beds.',1),
('Bungalow Sea', 'Queen Size Beds',1),
('Beach House', 'Sleeps 10 to 20 guests.',1),
('Royal Suite', 'Kings and Queens, Sleep 2 - 5 guests.',1),
('Hostel Hut', 'Single Size Beds Sleeps 20 or more.',1),
('Recreation Center', 'Video Games, Slots, Billiards, Water Park.',1),
('Water Park.', 'Includes wave pool and bar.',1),
('Shopping Center', 'Local Market Tourists',1),
('Food Town', 'Five Star Restuarant',1),
('Welcome Wagon', 'Gift Store and Information.',1),
('Annex', 'Big Empty Reception Hall.',1)





GO

INSERT INTO [dbo].[Room]
		([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [ResortPropertyID], [OfferingID], [RoomStatusID])
	VALUES
		-- (101, 'Hotel 101', 'Double', 'Double beds', 4, 100001, 100000, 'Available'),
		-- (102, 'Hotel 101', 'King', 'Single king', 2, 100000, 100001, 'Occupied'),
		-- (104, 'Hotel 102', 'Double', 'Double beds', 4, 100001, 100002, 'Available'),
		-- (105, 'Hotel 102', 'King', 'Single king', 2, 100001, 100003, 'Occupied'),
		-- (103, 'Guest Bld 101', 'Double', 'Double beds', 4, 100001, 100004, 'Available'),
		-- (106, 'Guest Bld 101', 'King', 'Single king', 2, 100002, 100005, 'Occupied'),
		-- (107, 'Shopping Center 101', 'Double', 'Double beds', 4, 100003, 100006, 'Available'),
		-- (108, 'Shopping Center 101', 'King', 'Single king', 2, 100004, 100006, 'Occupied'),
		-- (109, 'Food Center 101', 'Double', 'Double beds', 4, 100005, 100005, 'Available'),
		-- (110, 'Food Center 101', 'King', 'Single king', 2, 100006, 100005, 'Occupied'),
		-- (111, 'Welcome Center', 'Double', 'Double beds', 4, 100007, 100000, 'Available'),
		-- (112, 'Welcome Center', 'King', 'Single king', 2, 100008, 100001, 'Occupied'),
		-- (114, 'GenBld01', 'Double', 'Double beds', 4, 100009, 100002, 'Available'),
		-- (113, 'GenBld01', 'King', 'Single king', 2, 100002, 100000, 'Occupied')

    	(114, 'North Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100001, 100012, 'Available'),
		(115, 'East Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100001, 100012, 'Occupied'),
		(116, 'West Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100001, 100012, 'Available'),
		(117, 'South Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100001, 100012, 'Occupied'),
		(118, 'North Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100001, 100013, 'Available'),
		(119, 'East Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100001, 100013, 'Occupied'),
		(120, 'West Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100001, 100013, 'Available'),
		(121, 'South Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100001, 100013, 'Occupied'),
		(122, 'North Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100001, 100014, 'Available'),
		(123, 'East Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100001, 100014, 'Occupied'),
		(124, 'West Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100001, 100014, 'Available'),
		(125, 'South Shore3', 'Bungalow Sea', 'Queen Size Beds.', 2, 100001, 100014, 'Occupied'),
		(126, 'North Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100001, 100015, 'Available'),
		(127, 'East Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100001, 100015, 'Occupied'),
		(128, 'West Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100001, 100015, 'Available'),
		(129, 'South Shore4', 'Royal Suite', 'Sleeps up to 10 guests.', 2, 100001, 100015, 'Occupied'),
		(130, 'East Shore5', 'Hostel Hut', 'Sleeps up to 10 guests.', 5, 100001, 100016, 'Occupied'),
		(131, 'South Shore5', 'Hostel Hut', 'Sleeps up to 10 guests.', 20, 100001, 100016, 'Occupied')

		
GO
 INSERT INTO [dbo].[Shop]
		([RoomID], [Name], [Description], [Active])
	VALUES
		( 100000, "Brawlmart", "Discounts you'll fight over", 1),
		( 100003, "Chug Club", "Fun place to binge drink", 1),
		( 100004, "Solar City", "Overpriced sun tan lotion", 1),
		( 100006, "Wavy Daisy", "Surf and Sun shop", 1),
		( 100007, "Millenial Resort Gift Shop", "I ran out of ideas", 1),
		( 100008, "Brokesville", "This shop is inactive", 0)
GO
INSERT INTO [dbo].[Inspection]
		([ResortPropertyID], [Name], [DateInspected], [Rating], 
		[ResortInspectionAffiliation], [InspectionProblemNotes], [InspectionFixNotes])
	VALUES
		(100000, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100000, 'Elevator' ,'2018-08-12', 'Pass', "", "", ""),
		(100001, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100001, 'Elevator' ,'2018-08-12', 'Pass', "", "", ""),
		(100002, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100002, 'Elevator' ,'2018-08-12', 'Pass', "", "", ""),
		(100003, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100003, 'Elevator' ,'2018-08-12', 'Pass', "", "", ""),
		(100004, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100004, 'Elevator' ,'2018-08-12', 'Pass', "", "", ""),
		(100005, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100005, 'Elevator' ,'2018-08-12', 'Pass', "", "", ""),
		(100006, 'Sprinker Systems' ,'2018-01-01', 'Pass', "", "", ""),
		(100006, 'Elevator' ,'2018-08-12', 'Pass', "", "", "")
GO

EXEC sp_create_resort_vehicle_status 'In Use', 'Vehicle currently checked out';
EXEC sp_create_resort_vehicle_status 'Decomissioned', 'Vehicle dead'                 ;
EXEC sp_create_resort_vehicle_status 'Available'    , 'Vehicle available for use'    ;

EXEC sp_create_vehicle 'Dodge'  , 'Charger', 2015, 'IA 523', 50000, 5, 'Gray', '2015-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Audi'   , 'A4'     , 2017, 'IA 523', 50000, 5, 'Gray', '2010-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Toyota' , 'Camry'  , 2018, 'IA 523', 50000, 5, 'Gray', '2012-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Nissan' , 'Maxima' , 2019, 'IA 523', 50000, 5, 'Gray', '2011-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Hyundai', 'Excel'  , 2010, 'IA 523', 50000, 5, 'Gray', '2009-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Skoda'  , 'Octavia', 1920, 'IA 523', 50000, 5, 'Gray', '2008-05-05', 'A car', 1, '', 1, 'Available', 100000 ;




INSERT INTO [ItemType]
(
	[ItemTypeID], [Description]
)
VALUES
	("Toiletries", "Bathroom Stuff"),
	("Misc", "miscellaneous"),
	("Hydraulic", "Hydraulic Stuff"),
	("Parts", "Parts to repair equipment"),
	("Consumables", "Daily use")
GO

INSERT INTO [dbo].[Item]
(
	[OfferingID], [ItemTypeID], [RecipeID], [CustomerPurchasable], [Description], 
	[OnHandQty], [Name], [ReOrderQty]
)
VALUES
	(100000, "Food", NULL, 1, "TURTLE OF DOOM!", 455, "TURTLE OF DOOM (Limited Edition)!", 23),
	(NULL, "Food", NULL, 1, "12 OZ Porterhouse with a red wine reduction, served with finglering potatoes and a warm green salad.", 45, "12 oz. Porterhouse meal", 30),
	(NULL, "Misc", NULL, 1, "Deck of cards", 455, "Bicycle deck of 52", 200),
	(NULL, "Parts", NULL, 0, "Ring burner for GE model 4800 Gas Stove", 8, "Replacement burner for GE4800", 4),
	(NULL, "Consumables", NULL, 1, "Shampoo Bottle", 45500, "", 2500),
	(NULL, "Food", NULL, 1, "16oz Miller Lite Tall Boy", 5000, "16oz. Miller Lite", 1000),
	(NULL, "Food", NULL, 1, "'Fresh' Atlantic Salmon, packed in ice, ready to be filleted", 50, "Fresh Atlantic Salmon", 15),
	(NULL, "Misc", NULL, 1, "Resort Tshirt", 156, "Millenial Resort Branded Tshirt", 50),
	(NULL, "Hydraulic", NULL, 0, "DCVAM-200 10223 Hydraulic control Valve for loading bay lifts.", 4, "10233 Hydraulic Control Valve", 1),
	(NULL, "Consumables", NULL, 1, "250 count of napkins", 15612, "250 Napkins", 500),
	(NULL, "Parts", NULL, 0, "Toilet plunger for tank in back of toilet", 50, "", 25)
GO


INSERT INTO [EventType]
(
	[EventTypeID], [Description]
)
VALUES
	("Experience", "An event the customer will remember forever!")
GO

INSERT INTO [Event]
(
	[OfferingID],[EventTypeID], [EventStartDate], [NumGuests], [SeatsRemaining], 
	[PublicEvent], [Description], [KidsAllowed], [Location], [EventEndDate], [EventTitle], 
	[Sponsored], [EmployeeID], [Approved], [Cancelled]
)
VALUES
	(100001, "Experience", CURRENT_TIMESTAMP,
	40, 30, 1, "Fire dancing with turtles!", 1, 
	"On the beach by Frank's nasty shack.", CURRENT_TIMESTAMP, 
	"FIRE DANCING WITH TURTLES OF DOOM", 0, 100000, 1, 0),
	(100005, "Experience", CURRENT_TIMESTAMP,
	40, 30, 1, "Splash and drink and play with foam launchers", 1, 
	"Main Pool", CURRENT_TIMESTAMP, 
	"Foam Pool Party", 0, 100000, 1, 0),
	(100010, "Experience", CURRENT_TIMESTAMP,
	40, 30, 1, "Comedic Event ", 1, 
	"Main Ballroom", CURRENT_TIMESTAMP, 
	"John Mulaney", 0, 100000, 1, 0),
	(100011, "Experience", CURRENT_TIMESTAMP,
	40, 30, 1, "Penn and Teller", 1, 
	"Beach Front Patio", CURRENT_TIMESTAMP, 
	"Penn and Teller's Famous Magic Show", 0, 100000, 1, 0)
GO

INSERT INTO [dbo].[Setup]
([EventID],[DateEntered],[DateRequired],[Comments])
VALUES
(100000,'2019-05-06','2019-05-08','Please do this quickly'),
(100001,'2019-05-06','2019-08-08',NULL),
(100002,'2019-05-06','2019-06-08','Requires extensive remodeling'),
(100003,'2019-05-06','2019-09-11','Use the forklift')
GO

INSERT INTO [dbo].[SetupList]
		([SetupID], [Completed], [Description], [Comments])
	VALUES
		(100000, 0, ' Prior to Guest Arrival: Registration Desk,signs,banners', 'Banners are not ready yet'),
		(100001, 0, ' Display Equipment: Prepares for display boards,tables,chairs,, printed material and names badges','Badges are not ready yet'),
		(100002, 1, ' Check Av Equipment: Laptop,projectors :Ensure all cables,leads,laptop,mic and mouse are presented and working', 'Av Equipment is ready'),
		(100003, 1, ' Confirm that all decor and linen is in place ', 'Decor and linen are  ready'),
		(100002, 1, ' Walk through to make sure bathrooms are clean and stocked ', 'Bathrooms are  ready')
GO

INSERT INTO [ResortProperty]
(
	[ResortPropertyTypeID]
) 
VALUES
	("Building"),
	("Room")
GO


INSERT INTO [dbo].[Recipe]
([Name],[Description],[DateAdded],[Active])
VALUES
('Pancakes','12 oz flour, salt, sugar, butter, milk','2019-03-05',1),
('Bruschetta','Crostini, Motzerella, Tomato, Balsamic Glaze','2019-06-09',1),
('Seared Tuna','Soy marinade, soak then sear on each side ~45 seconds.','2019-05-03',1),
('Stuffed Peppers','hollow out pepper, insert beef and rice mixture, top with cheese bake 25 min at 450','2019-04-01',1),
('Chicken Squares','Slow cook chicken, then shred, melt cream cheese insert mixture into cresent rolls bake 15 min at 350','2019-07-04',1),
('Rum Runner','3 kinds of rum, triple sec, pineapple, orange juice and a splash of grenadine','2019-08-05',0)

GO

INSERT INTO [dbo].[Supplier]
([Name],[Address],[City],[State],[PostalCode],[Country],[PhoneNumber],[Email],[ContactFirstName],[ContactLastName],
[DateAdded],[Description],[Active])
VALUES
('MySupply','123 Main St','Iowa City','IA','60013','USA','8155554488','place@place.com','John','Cena','2019-03-05','Description',1),
('Joes Supply shed','149 E Pulaski St','Olive Branch','MA','38654','USA','8155554321','glenna@place.com','Glenna','Smalls','2019-03-05','Description',1),
('Grainger','141 Rockville Ave.','W Bloomfield','MI','48322','USA','8155554123','beau@place.com','Beau','Carpenter','2019-03-05','Description',1),
('Cisco','38 Cedar Swamp St','Aberdeen','SD','57401','USA','8155554654','angie@place.com','Angie','Wright','2019-03-05','Description',1),
('US Foods','9326 Rocky River Ave.','Lutherville','MD','21093','USA','8155554845','norman@place.com','Norman','Dorsey','2019-03-05','Description',1),
('Swift','2 Bald Hill Circle','Hanover Park','IL','60133','USA','8155554155','wilford@place.com','Wilford','Rye','2019-03-05','Description',1),
('Hungry Hobo','93 Talbot Dr','Hamtramck','MI','48212','USA','8154891566','nick@place.com','Nick','Wyndham','2019-03-05','Description',1)
GO

INSERT INTO [dbo].[SupplierOrder]
([EmployeeID],[Description],[OrderComplete],[DateOrdered],[SupplierID])
VALUES
(100000,'Order For laundry',1,'2019-03-05',100000),
(100000,'Order For Paul',1,'2019-03-05',100000),
(100000,'Order For Housekeeping',1,'2019-03-05',100000),
(100000,'Order For special event',1,'2019-03-05',100000)
GO

INSERT INTO [dbo].[SupplierOrderLine]
([ItemID],[SupplierOrderID],[Description],[OrderQty],[QtyReceived],[UnitPrice])
VALUES 
(100000,100005,'Description',25,0,10),
(100000,100006,'Description',25,0,10),
(100001,100007,'Description',25,0,10),
(100001,100005,'Description',25,0,10),
(100001,100006,'Description',25,0,10),
(100003,100007,'Description',25,0,10),
(100002,100005,'Description',25,0,10),
(100002,100006,'Description',25,0,10),
(100002,100007,'Description',25,0,10),
(100003,100005,'Description',25,0,10)
GO


INSERT INTO [dbo].[Appointment]
([AppointmentTypeID],[GuestID],[StartDate],[EndDate],[Description])
VALUES
("Spa",100000,'2019-06-05','2019-06-05','Joey is going to the spa'),
("Nature Walk",100000,'2019-06-05','2019-06-05','Walk with the seals'),
("Fishing Charter",100001,'2019-06-05','2019-06-05','Trying to catch the elusive Dolphin fish'),
("Leg Waxing",100002,'2019-06-05','2019-06-05','Try not to scream or cry'),
("Spa",100003,'2019-06-05','2019-06-05','John is going to the spa'),
("Spa",100004,'2019-06-05','2019-06-05','Mrs. Trump is going to the spa')
GO

INSERT INTO [dbo].[EventEmployee]
([EventID],[EmployeeID])
VALUES
(100000,100000),
(100000,100001),
(100000,100002),
(100000,100003),
(100001,100000),
(100001,100001),
(100001,100002),
(100001,100003),
(100002,100004),
(100002,100003)
GO

INSERT INTO [dbo].[EventPerformance]
([EventID],[PerformanceID])
VALUES
(100000,100000),
(100003,100002),
(100003,100005),
(100001,100006),
(100001,100003),
(100002,100004),
(100002,100003)
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

INSERT INTO [dbo].[EventSponsor]
([EventID],[SponsorID])
VALUES
(100000,100000),
(100002, 100001)
GO

INSERT INTO [dbo].[Reservation]
([MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes],[Active])
VALUES
(100000,2,0,"2019-05-06","2019-05-12",NULL,1),
(100001,4,0,"2019-06-06","2019-06-12",NULL,1),
(100002,1,0,"2019-07-06","2019-07-12",NULL,1),
(100003,3,0,"2019-08-06","2019-08-12",NULL,1)
GO


INSERT INTO [dbo].[RoomReservation]
([RoomID],[ReservationID],[CheckInDate],[CheckOutDate])
VALUES
(100000,100000,NULL,NULL),
(100005,100001,NULL,NULL),
(100007,100002,NULL,NULL),
(100003,100003,NULL,NULL)
GO


INSERT INTO [dbo].[GuestRoomAssignment]
([GuestID],[RoomReservationID])
VALUES 
(100000,100000),
(100004,100001),
(100001,100002),
(100002,100003)
GO

INSERT INTO [dbo].[GuestType]
([GuestTypeID],[Description])
VALUES
("Normal","Average Guest"),
("VIP","Very important Person"),
("Group","Part of a large group"),
("Business","Here for business")
GO

INSERT INTO [dbo].[GuestVehicle]
([GuestID],[Make],[Model],[PlateNumber],[Color],[ParkingLocation])
VALUES
(100000, "Saturn","SL1","Q23H345","Grey","A12"),
(100000, "McLaren","MPC-12","L091928","Sunburst Orange","B25"),
(100001, "Ford","Fusion","TE1957","Blue","C32"),
(100003, "Toyota","Camry","IF1421","Red","A4"),
(100004, "Range Rover","Discovery","BEAST","Silver","On the hilltop"),
(100002, "Tesla","Roadster","SPEEDI","Black",NULL),
(100005, "Pontiac","Grand Am","L540206","Tan",NULL)
GO

INSERT INTO [dbo].[HousekeepingRequest]
([BuildingNumber],[RoomNumber],[Description],[WorkingEmployeeID])
VALUES
(45,2005,"Spill in aisleway",NULL),
(12,05,"Guest reported leaking ceiling",NULL),
(10,512,"Guest reported moldy smell",NULL),
(54,225,"Elevator has sticky buttons",NULL),
(13,25,"Fire Extinguisher was deployed",NULL),
(2,385,"Gum on doorknob",NULL)
Go

INSERT INTO [dbo].[ItemSupplier]
([ItemID],[SupplierID],[PrimarySupplier],[LeadTimeDays],[UnitPrice],[Active])
VALUES
(100000,100000,NULL,NULL,NULL,1),
(100001,100000,NULL,NULL,NULL,1),
(100002,100001,NULL,NULL,NULL,1),
(100003,100001,NULL,NULL,NULL,1),
(100004,100002,NULL,NULL,NULL,1),
(100005,100002,NULL,NULL,NULL,1),
(100006,100003,NULL,NULL,NULL,1),
(100007,100003,NULL,NULL,NULL,1)
GO

INSERT INTO [dbo].[Luggage]
([GuestID],[LuggageStatusID])
VALUES
(100000,'In Room'),
(100002,'In Lobby'),
(100001,'In Room'),
(100001,'In Transit'),
(100002,'In Room'),
(100000,'In Room'),
(100003,'In Room')
GO

INSERT INTO [dbo].[MaintenanceStatus]
([MaintenanceStatusID],[Description])
VALUES
("Complete","Completed"),
("Working","In the process of being completed"),
("Waiting","Waiting to be started")
Go

INSERT INTO [dbo].[MaintenanceType]
([MaintenanceTypeID],[Description],[Active])
VALUES
("Repair","Something needs to be repaired",1),
("Replace","Something needs to be replaced",1),
("Install","Something needs to be installed",1),
("Remove","Something needs to be removed",1)
GO

INSERT INTO [dbo].[MaintenanceWorkOrder]
([MaintenanceTypeID],[DateRequested],[DateCompleted],[RequestingEmployeeID],[WorkingEmployeeID],[Complete],[Description],[Comments],[MaintenanceStatus],[ResortPropertyID])
VALUES
("Repair","2018-10-10",NULL,100000,100001,0,"The toilet is leaking",NULL,"Waiting",100000),
("Replace","2018-01-10",NULL,100001,100000,0,"Faucet needs to be replaced",NULL,"Waiting",100000),
("Install","2019-10-05",NULL,100000,100001,0,"Ceiling fan is needed",NULL,"Waiting",100001),
("Repair","2018-10-10",NULL,100000,100003,0,"Rug has a frayed end",NULL,"Waiting",100002),
("Repair","2018-10-10",NULL,100001,100001,0,"Mini Bar is not cold",NULL,"Waiting",100003),
("Remove","2018-10-10",NULL,100002,100001,0,"Extra towel rack in bathroom",NULL,"Waiting",100003),
("Install","2018-10-10",NULL,100004,100002,0,"New air filter",NULL,"Waiting",100003),
("Repair","2018-10-10",NULL,100000,100004,0,"Window is not sealing fully",NULL,"Waiting",100003)
GO



INSERT INTO [dbo].[MemberTab]
([MemberID],[Active],[TotalPrice])
VALUES
(100000,0,1500),
(100001,1,75),
(100002,1,150),
(100003,1,45000),
(100004,1,2751),
(100005,1,1589),
(100006,1,3489),
(100000,1,1500)
GO

-- Has to be done separately for each Member because
-- the trigger that updates the MemberTab.TotalPrice
-- can only be done on one member at a time.
INSERT INTO [dbo].[MemberTabLine]

([MemberTabID],[OfferingID],[Quantity],[Price],[EmployeeID],[Discount],[GuestID])
VALUES
(100000,100000,1,150,100000,NULL,100000),
(100000,100003,1,10,100000,NULL,100000),
(100000,100002,1,25,100000,NULL,100000),
(100001,100005,1,2000,100000,NULL,100000),
(100001,100003,1,10,100000,NULL,100000),
(100001,100001,1,150,100000,NULL,100000),
(100002,100000,1,150,100000,NULL,100000),
(100002,100005,1,2000,100000,NULL,100000),
(100002,100002,1,25,100000,NULL,100000),
(100003,100003,1,10,100000,NULL,100000),
(100003,100001,1,150,100000,NULL,100000),
(100004,100003,1,150,100000,NULL,100000),
(100004,100005,1,2000,100000,NULL,100000),
(100005,100002,1,25,100000,NULL,100000),
(100005,100001,1,150,100000,NULL,100000),
(100006,100002,1,25,100000,NULL,100000),
(100006,100001,1,150,100000,NULL,100000),
(100006,100000,1,150,100000,NULL,100000),
(100006,100004,1,150,100000,NULL,100000)
GO


INSERT INTO [dbo].[Receiving]
([SupplierOrderID],[Description],[DateDelivered])
VALUES
(100005,"Was delivered","2019-05-05")
GO

INSERT INTO [dbo].[RecipeItemLine]
([RecipeID],[ItemID],[Quantity],[UnitOfMeasure])
VALUES
(100000,100000,2,"oz"),
(100000,100001,2,"oz"),
(100000,100002,2,"oz"),
(100001,100000,2,"oz"),
(100001,100001,2,"oz"),
(100001,100004,2,"oz"),
(100002,100005,2,"oz"),
(100002,100001,2,"oz"),
(100003,100000,2,"oz"),
(100004,100000,2,"oz")
GO





INSERT INTO [dbo].[ServiceComponent]
([ServiceComponentID],[OfferingID],[Duration],[Description],[Active])
VALUES
("Pipe Fitting",100000,25,"Part of this component",1),
("Super Glue",100002,25,"Part of this component",1),
("Duct Tape",100003,25,"Part of this component",1),
("Knife",100004,25,"Part of this component",1),
("Hose Fitting",100005,25,"Part of this component",1),
('C# and Yoga', 100000, 30,'Modern Yoga infused with loose couplings and dependancy construction.',1),
('sql and Yoga', 100000, 30, 'Modern Yoga infused with data redundancy.',1),
('Coal Walk',  100000, 30,'Ever wanted to walk on hot coals? Now is your chance!',1),
('Leg Waxing',  100000, 30,'It is as painful as it sounds.',1),
('Beach Massage', 100000, 30, 'Get a relaxing massage on the beach.',1),
('Nature Walk',  100000, 30,'Explore the natural beauty surrounding the resort',1),
('Fishing Charter', 100000, 30, 'Go catch the big fish and have it professionally filleted for your eating pleasure.',1),
('Spa', 100000, 30,'Spa',1),
('Pet Grooming',  100000, 30,'Pet Grooming',1),
('Turtle Petting', 100000, 30, 'Turtle Petting',1),
('Whale Watching',  100000, 30,'Whale Watching',1),
('Sand Castle',  100000, 30,'Sand Castle Building',1)






GO




INSERT INTO [dbo].[ScheduledItem]
([AppointmentID],[ServiceComponentID])
VALUES
(100000,"Knife"),
(100000,"Duct Tape"),
(100000,"Pipe Fitting"),
(100002,"Knife"),
(100002,"Super Glue"),
(100003,"Hose Fitting"),
(100001,"Super Glue"),
(100001,"Duct Tape")
GO

INSERT INTO [dbo].[ShopOffering]
([ShopID],[OfferingID])
VALUES
(100000,100001),
(100000,100002),
(100001,100004),
(100001,100005),
(100002,100002),
(100003,100003),
(100004,100001),
(100005,100000),
(100005,100007)
GO

INSERT INTO [dbo].[VehicleCheckout]
([EmployeeID],[DateCheckedOut],[DateReturned],[DateExpectedBack],[Returned],[ResortVehicleID])
VALUES
(100000,"2019-05-05",NULL,"2019-05-07",1,100000),
(100001,"2019-05-08",NULL,"2019-05-09",1,100000),
(100000,"2019-05-05",NULL,"2019-05-08",1,100001),
(100001,"2019-05-01",NULL,"2019-05-06",1,100002),
(100002,"2019-05-03",NULL,"2019-05-04",1,100003)
GO

SET IDENTITY_INSERT [dbo].[SpecialOrder] ON

INSERT INTO [dbo].[SpecialOrder]
		([SpecialOrderID], [EmployeeID], [Description],[DateOrdered], [Supplier],[Authorized])
	VALUES
		(2000001, 100001, 'Full Synthetic Engine Oil','2/8/2019','Megaproducts','Erater'),
		(2000002, 100002, 'Full Synthetic Engine Oil', '2/6/2011','Sam electrics',''),
		(2000003, 100003, 'Synthectic blend Engine Oil','6/8/2012','Slantic','')
		
SET IDENTITY_INSERT [dbo].[SpecialOrder] OFF		
GO


INSERT INTO [dbo].[SpecialOrderLine]
    ([NameID], [SpecialOrderID], [Description], [OrderQty], [QtyReceived])
    VALUES
        ('Tomato Soup', 2000001, 'Tomato soup with green pepper',1, 1),
        ('Paper', 2000002, 'White paper', 5, 0),
        ('Pencil', 2000003, 'Pencil 2B for designer', 6, 6)
    
GO









