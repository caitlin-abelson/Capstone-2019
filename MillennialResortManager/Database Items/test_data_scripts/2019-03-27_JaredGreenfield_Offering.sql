USE [MillennialResort_DB]
GO

print '' print 'Inserting Departments'
GO
/*
INSERT INTO [Department]
(
	[DepartmentID], [Description]
)
VALUES
	("Everything", "They do everything.")
GO

print '' print 'Inserting OfferingTypes'
GO
INSERT INTO [OfferingType]
(
	[OfferingTypeID], [Description]
)
VALUES
	("Event", "Things people attend."),
	("Item", "Things people buy."),
	("Room", "Things people stay in."),
	("Service", "Things people pay to have someone do for them.")
GO
*/

print '' print 'Inserting Employee'
GO
INSERT INTO [Employee]
(
	[FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID]
)
VALUES
	("Dave", "Admin", "1234567890", "DaveAdmin@gmail.com", "Maintenance")
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
	
print '' print 'Inserting ItemTypes'
GO
INSERT INTO [ItemType]
(
	[ItemTypeID], [Description]
)
VALUES
	("Food", "Edible Stuff")
GO


print '' print 'Inserting Items'
GO
INSERT INTO [Item]
(
	[OfferingID], [ItemTypeID], [RecipeID], [CustomerPurchasable], [Description], 
	[OnHandQty], [Name], [ReOrderQty]
)
VALUES
	(100000, "Food", NULL, 1, "TURTLE OF DOOM!", 455, "TURTLE OF DOOM (Limited Edition)!", 23)
GO

print '' print 'Inserting EventType'
GO
INSERT INTO [EventType]
(
	[EventTypeID], [Description]
)
VALUES
	("Experience", "An event the customer will remember forever!")
GO

print '' print 'Inserting Event'
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
	"FIRE DANCING WITH TURTLES OF DOOM", 0, 100000, 1, 0)
GO
/*
print '' print 'Inserting ResortPropertyType'
GO
INSERT INTO [ResortPropertyType]
(
	[ResortPropertyTypeID]
)
VALUES
	("Building"),
	("Room")
GO
*/
print '' print 'Inserting ResortProperty'
GO
INSERT INTO [ResortProperty]
(
	[ResortPropertyTypeID]
) 
VALUES
	("Building"),
	("Room")
GO

-- print '' print 'Inserting BuildingStatusID'
-- GO
-- INSERT INTO [BuildingStatus]
-- (
-- 	[BuildingStatusID], [Description]
-- )
-- VALUES
-- 	("Active", "It works I guess.")
-- GO

print '' print 'Inserting Building'
GO
-- INSERT INTO [Building]
-- (
-- 	[BuildingID], [Address], [BuildingName], [Description], [BuildingStatusID], [ResortPropertyID]
-- )
-- VALUES
-- 	("Big Dave's Fire Shack", "300 On Da Beach Ave", "Fire Shack", "It's a shack on fire.", "Active", 100000)
-- GO

print '' print 'Inserting RoomStatus'
GO
INSERT INTO [RoomStatus]
(
	[RoomStatusID], [Description]
)
VALUES
	("Ready to rent.", "It's ready to do stuff.")
GO

print '' print 'Inserting RoomType'
GO
INSERT INTO [RoomType]
(
	[RoomTypeID], [Description]
)
VALUES
	("Big Room", "it's big and stuff.")
GO

print '' print 'Inserting Room'
GO

INSERT INTO [Room]
(
	[BuildingID], [RoomNumber], [RoomTypeID], [Description], 
	[Capacity], [OfferingID], [RoomStatusID], [ResortPropertyID]
)
VALUES
	("Big Dave's Fire Shack", 100, "Big Room",
	"A room with now doors, only windows.",
	4, 100002, "Ready to rent.", 100001)
GO

print '' print 'Inserting ServiceComponent'
GO
INSERT INTO [ServiceComponent]
(
	[ServiceComponentID], [OfferingID], [Duration], [Description], [Active]
)
VALUES
	("Turtle Massage", 100003, 30, "Get massaged by turtles.", 1)
GO

