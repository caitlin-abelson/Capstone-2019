SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
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
		('Guest Bld 102', 'Chlorine Nightmare', '667 Angler Circle ', 'Water Park', 'Available', 100002),
		('Shopping Center 101', 'The Coral Reef', '1202 Try n Save Drive', 'Shopping Center', 'Available', 100003),
		('Shopping Center 102', 'The Dead Sea', '1203 Try n Save Drive', 'Shopping Center', 'Available', 100003),
		('Food Center 101', 'Trout Hatch', '808 Turtle Pond Parkway', 'Food Court', 'Undergoing Maintanance', 100004),
		('Food Center 102', 'Beer Gardens', '809 Turtle Puk Parkway', 'Food Court', 'Working', 100004),
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



/* End Test Data */

/* JARED 
1.	Both Room table and Offering table both use price?
2.	OfferingID can be null?
3. 	Does Room really need a RoomStatusID, an Active, and Available field?

*/
/* 	Created By:  Wes 
	Created: 
	Updated: Danielle Russo
			03/28/2019
			Removed Price Field
			
*/ 
-- ALTER TABLE [dbo].[Room]
-- 	DROP CONSTRAINT DF__Room__Active__73BA3083
-- 	GO



/* ********************** TEST DATA NEEDED ********************************/

print '' print '***Inserting a fake Offering record'
GO
INSERT INTO [dbo].[Offering]
			([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00),
			('Room',100000,'A description for a fake Offering',1000.00)
GO

print '' print '*** Inserting Room Records ***'
GO
INSERT INTO [dbo].[Room]
		([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [ResortPropertyID], [OfferingID], [RoomStatusID])
	VALUES
		(101, 'North Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100007, 100000, 'Available'),
		(102, 'East Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100008, 100001, 'Occupied'),
		(103, 'West Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100009, 100002, 'Available'),
		(104, 'South Shore1', 'Beach House', 'Sleeps 10 to 20 guests.', 20, 100010, 100003, 'Occupied'),
		(105, 'North Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100011, 100004, 'Available'),
		(106, 'East Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100012, 100005, 'Occupied'),
		(107, 'West Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100013, 100006, 'Available'),
		(108, 'South Shore2', 'Bungalow Land', 'Queen Size Beds.', 2, 100014, 100007, 'Occupied'),
		(109, 'North Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100015, 100008, 'Available'),
		(110, 'East Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100016, 100009, 'Occupied'),
		(111, 'West Shore3', 'Bungalow Sea', 'Queen Size Beds', 2, 100017, 100010, 'Available'),
		(112, 'South Shore3', 'Bungalow Sea', 'Queen Size Beds.', 2, 100014, 100007, 'Occupied'),
		(113, 'North Shore4', 'Royal Suite', 'Kings and Queens, Sleep 2 - 5 guests.', 2, 100015, 100008, 'Available'),
		(114, 'East Shore4', 'Royal Suite', 'Kings and Queens, Sleep 2 - 5 guests.', 2, 100016, 100009, 'Occupied'),
		(115, 'West Shore4', 'Royal Suite', 'Kings and Queens, Sleep 2 - 5 guests.', 2, 100017, 100010, 'Available'),
		(116, 'South Shore4', 'Royal Suite', 'Kings and Queens, Sleep 2 - 5 guests.', 2, 100014, 100007, 'Occupied'),
		(117, 'East Shore5', 'Hostel Hut', 'Single Size Beds Sleeps 20 or more.', 5, 100018, 100011, 'Occupied'),
		(118, 'South Shore', 'Hostel Hut', 'Single Size Beds Sleeps 20 or more.', 20, 100020, 100013, 'Occupied'),

		(119, 'Guest Bld 101', 'Recreation Center', 'Video Games, Slots, Billiards, Water Park.', 40, 100011, 100004, 'Available'),
		(120, 'Guest Bld 102', 'Water Park.', 'Includes wave pool and bar.', 2, 100012, 100005, 'Occupied'),
		(121, 'Shopping Center 101', 'Shopping Center', 'Local Market Tourists', 4, 100013, 100006, 'Available'),
		(122, 'Shopping Center 102', 'Shopping Center', 'Local Market Tourists', 2, 100014, 100007, 'Occupied'),
		(123, 'Food Center 101', 'Food Town', 'Five Star Restuarant', 4, 100015, 100008, 'Available'),
		(124, 'Food Center 102', 'Food Town', 'Five Star Restuarant', 2, 100016, 100009, 'Occupied'),
		(125, 'Welcome Center', 'Welcome Wagon', 'Gift Store and Information.', 4, 100017, 100010, 'Available'),
		(126, 'GenBld01', 'Annex', 'Big Empty Reception Hall.', 4, 100019, 100012, 'Available'),
		(127, 'Hotel 101', 'Annex', 'Big Empty Reception Hall.', 4, 100019, 100012, 'Available'),
		(128, 'Hotel 102', 'Annex', 'Big Empty Reception Hall.', 4, 100019, 100012, 'Available')
GO



/* ********************** END OF TEST DATA  ********************************/







/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
print '' print '*** Insert Inspection Records ***'
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




/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
/*
print '' print '*** Creating sp_select_room_by_buildingid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_by_buildingid]
AS
	BEGIN
		SELECT	[RoomID],[RoomNumber],[Name],[DateInspected],
				[Rating],[ResortInspectionAffiliation],[InspectionProblemNotes],
				[InspectionFixNotes]
		FROM 	[Inspection]
	END
GO
*/
