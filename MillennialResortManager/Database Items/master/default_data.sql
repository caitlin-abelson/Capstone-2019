USE [MillennialResort_DB]
GO
--Default data that is required for the functional deployment of the database
INSERT INTO [dbo].[RoomType]
           ([RoomTypeID],[Description],[Active])
     VALUES
           ('UNASSIGNED','Room type is unnassigned',1)
GO
INSERT INTO [dbo].[GuestType]
           ([GuestTypeID],[Description])
     VALUES
           ('Basic guest','')
GO
INSERT INTO [dbo].[PetType]
           ([PetTypeID],[Description])
     VALUES
           ('UNASSIGNED','Pets that have not been assigned a type')
GO
INSERT INTO [dbo].[EventType]
           ([EventTypeID],[Description])
     VALUES
           ('UNASSIGNED','Event that has not been assigned a type')
GO
INSERT INTO [dbo].[ItemType]
           ([ItemTypeID],[Description])
     VALUES
           ('UNASSIGNED','Item that has not been assigned a type')
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
INSERT INTO [dbo].[Department]
	([DepartmentID], [Description])
	VALUES
	('Admin', 'Has access to everything'),
	('Maintenance', 'Fixes everything'),
	('Events', 'Makes sure parties go as planned'),
    ('ResortOperations', 'Makes sure everything runs smoothly'),
    ('Pet', 'Makes every animal feel happy'),
    ('FoodService', 'Fills customer bellies'),
	('Ordering', 'Gets stuff in, moves stuff out'),
    ('NewEmployee', 'Has no duties')
	GO
INSERT INTO [dbo].[Role]
	([RoleID], [Description])
	VALUES
	('Admin', 'Has access to everything'),
	('Manager', 'Makes sure parties go as planned'),
	('Worker', 'Gets stuff in, moves stuff out')
	GO
	
INSERT INTO [RoomType]
(
    [RoomTypeID], [Description] 
)	
VALUES
    ('Bungalow Land', 'Queen Size Beds.'),
    ('Bungalow Sea', 'Queen Size Beds'),
    ('Beach House', 'Sleeps 10 to 20 guests.'),
	('Royal Suite', 'Kings and Queens, Sleep 2 - 5 guests.'),
	('Hostel Hut', 'Single Size Beds Sleeps 20 or more.')
GO

INSERT INTO [Employee]
(
	[FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID]
)
VALUES
	("Bob", "Trap", "1234567890", "BTrapp@gmail.com", "Admin")
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
INSERT INTO [dbo].[EmployeeRole]
	([RoleID], [EmployeeID])
	VALUES
	('Admin', 100000)
	GO
/*	Author: Jacob Miller
	Created: 3/28/19
	Updated:
*/

INSERT INTO [dbo].[LuggageStatus]
		([LuggageStatusID])
	VALUES
		('In Lobby'),
		('In Room'),
		('In Transit')
GO

INSERT INTO [dbo].[RoomStatus]
	([RoomStatusID],[Description])
	VALUES
	('Available', 'Available'),
	('Occupied', 'Occupied')
GO

INSERT INTO [dbo].[BuildingStatus]
		([BuildingStatusID], [Description])
	VALUES
		('Available', 'Building is good to go!'),
		('No Vacancy', 'All rooms are filled'),
		('Undergoing Maintanance', 'Some rooms available')
GO