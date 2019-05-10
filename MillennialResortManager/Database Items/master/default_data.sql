USE [MillennialResort_DB]
GO
--Default data that is required for the functional deployment of the database

print 'Guest Types'
GO
INSERT INTO [dbo].[GuestType]
           ([GuestTypeID],[Description])
     VALUES
           ('Basic Guest','Run of the mill guest.'),
		   ('Platinum Guest','Guest with 7+ stays.'),
		   ('Silver Guest','Guest with 3+ stays.'),
		   ('Gold Guest','Guest with 5+ stays.')
GO

print 'Pet Types'
GO
INSERT INTO [dbo].[PetType]
           ([PetTypeID],[Description])
     VALUES
           ('UNASSIGNED','Pets that have not been assigned a type'),
		   ('Fish','An aquatic companion.'),
		   ('Dog','Your best friend.'),
		   ('Cat','Feline friends.'),
		   ('Bird','Birdie buddy.'),
		   ('Lizard','Scaly friends.')
GO
print 'Event Types'
GO
INSERT INTO [dbo].[EventType]
           ([EventTypeID],[Description])
     VALUES
           ('UNASSIGNED','Event that has not been assigned a type'),
		   ('Concert','An event with a singer.'),
		   ('Party','An event with music and beverages.'),
		   ('Trade Show','An event where vendors sell their wares.'),
		   ('Competition','An event where people compete for prizes.'),
		   ('Wedding','An event where someone gets married.')
GO
print 'Item Types'
GO
INSERT INTO [dbo].[ItemType]
           ([ItemTypeID],[Description])
     VALUES
           ('Food','A consumable item.'),
		   ('Cleaning Items','An item for cleaning up messes.'),
		   ('Office Supplies','Supplies for office work.'),
		   ('Room Amenities','Things that make the room comfortable.'),
		   ('Clothing','A piece of Millennial Resort apparel.'),
		   ('Misc','Other un-specified type.')
GO
print 'Offering Types'
GO
INSERT INTO [dbo].[OfferingType]
           ([OfferingTypeID],[Description])
     VALUES
           ('UNASSIGNED','Offering that has not been assigned a type'),
		   ('Event','An experience for the event.'),
		   ('Service','Offering that has not been assigned a type'),
		   ('Item','Offering that has not been assigned a type'),
		   ('Room','A room is a space people stay in.')
GO

print 'Role'
GO
INSERT INTO [dbo].[Role]
	([RoleID], [Description])
	VALUES
	('Admin', 'Has access to everything'),
	('Manager', 'Makes sure parties go as planned'),
	('Worker', 'Gets stuff in, moves stuff out')
	GO
	
print 'Room Type' 	
 INSERT INTO [dbo].[RoomType]
([RoomTypeID],[Description],[Active])	
	VALUES
	('UNASSIGNED','Room type is unnassigned',1),
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

print 'Employee'
GO
INSERT INTO [Employee]
(
	[FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID]
)
VALUES
	("Bob", "Trap", "1234567890", "BTrapp@gmail.com", "Admin")
GO

print 'Resort Property Types'
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
print 'Employee Role'
GO
INSERT INTO [dbo].[EmployeeRole]
	([RoleID], [EmployeeID])
	VALUES
	('Admin', 100000)
	GO
/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/
print 'Luggage Status'
GO
INSERT INTO [dbo].[LuggageStatus]
		([LuggageStatusID])
	VALUES
		('In Lobby'),
		('In Room'),
		('In Transit')
GO

print 'Room Status'
GO
INSERT INTO [dbo].[RoomStatus]
	([RoomStatusID],[Description])
	VALUES
	('Available', 'Available'),
	('Occupied', 'Occupied'),
	('Under Maintenance', 'Being worked on.'),
	('UNASSIGNED', "The status is currently unknown after an update of status values.")
GO

print 'Building Status'
GO
INSERT INTO [dbo].[BuildingStatus]
		([BuildingStatusID], [Description])
	VALUES
		('Available', 'Building is good to go!'),
		('No Vacancy', 'All rooms are filled'),
		('Undergoing Maintanance', 'Some rooms available')
GO

print 'Resort Vehicle Status'
GO
INSERT INTO [dbo].[ResortVehicleStatus]
	([ResortVehicleStatusID], [Description])
	VALUES
		('In Use', 'Vehicle currently checked out'),
		('Decomissioned', 'Vehicle dead'),
		('Available', 'Vehicle available for use')
GO