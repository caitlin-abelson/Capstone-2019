SELECT 'This is a test script to test new workflow.'
GO

USE [MillennialResort_DB]
GO

/* Start Test Data */

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

print '' print '*** Insert BuildingStatus Records ***'
GO
INSERT INTO [dbo].[BuildingStatus]
		([BuildingStatusID], [Description])
	VALUES
		('Available', 'Building is good to go!'),
		('No Vacancy', 'All rooms are filled'),
		('Undergoing Maintanance', 'Some rooms available')
GO

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



/* End Test Data */

/* JARED 
1.	Both Room table and Offering table both use price?
2.	OfferingID can be null?
3. 	Does Room really need a RoomStatusID, an Active, and Available field?

*/
/* 	Author:  Wes 
	Created: 
	Updated: Danielle Russo
			03/28/2019
			Removed Price Field
			
*/ 
ALTER TABLE [dbo].[Room]
	DROP CONSTRAINT DF__Room__Active__73BA3083
	GO

print '' print '*** Altering Room Table ***'
GO
ALTER TABLE [dbo].[Room]
	DROP COLUMN [Active], [Available], [Price]
GO

 
print '' print '*** Altering Room Table ***'
GO
ALTER TABLE [dbo].[Room]
	ADD [OfferingID]	[int],
		[RoomStatusID]	[nvarchar](25)
		
GO

/* ********************** TEST DATA NEEDED ********************************/

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



/* ********************** END OF TEST DATA  ********************************/

/* 	Author:  Danielle Russo 
	Created: 03/29/2019
*/ 
print '' print '*** Creating RoomStatus Table ***'
GO
CREATE TABLE [dbo].[RoomStatus]
(
	[RoomStatusID]	[nvarchar](25),
	[Description]	[nvarchar](1000),
	
	CONSTRAINT [pk_RoomStatusID] PRIMARY KEY([RoomStatusID] ASC)
)
GO

print '' print '*** Inserting RoomStatus Table ***'
GO
INSERT INTO [dbo].[RoomStatus]
	([RoomStatusID],[Description])
	VALUES
	('Available', 'Available'),
	('Occupied', 'Occupied')
GO

print '' print '*** Dropping Procedure sp_insert_room ***'
GO
DROP PROCEDURE [dbo].[sp_insert_room]
GO

/* 	Author:  Wes 
	Created: 
	Updated: Danielle Russo
			03/28/2019
			Added Price field
			Added ResortPropertyID 
*/ 
print '' print '*** Create Procedure sp_insert_room ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_room]
	(
	@RoomNumber			[nvarchar](15),
	@BuildingID			[nvarchar](50),
	@RoomTypeID			[nvarchar](15),
	@Description		[nvarchar](1000),
	@Capacity			[int],
	@RoomStatusID		[nvarchar](25),
	
	@OfferingTypeID		[nvarchar](15),
	@EmployeeID			[int],
	@Price				[Money]
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Room')
		DECLARE @NewResortProperyID [int] = (SELECT @@IDENTITY)

		INSERT INTO [Offering]	
		([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
		(@OfferingTypeID, @EmployeeID, @Description, @Price)	
		DECLARE @NewOfferingID [int] = (SELECT @@IDENTITY)
		
		
		INSERT INTO [dbo].[Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [RoomStatusID],[OfferingID],[ResortPropertyID])
		VALUES
			(@RoomNumber, @BuildingID, @RoomTypeID, @Description, @Capacity, @RoomStatusID, @NewOfferingID, @NewResortProperyID)	
		RETURN @@ROWCOUNT
	END
GO

/*
	Updated: Danielle Russo
	Date: 3/28/2019
	Added StatusID Parameter
	Added ResortPropertyID Parameter
*/
print '' print '*** Drop procedure sp_select_buildings ***'
GO
DROP PROCEDURE [sp_select_buildings]
GO

print '' print '*** Create procedure sp_select_buildings ***'
GO
CREATE PROCEDURE [dbo].[sp_select_buildings]
AS
	BEGIN
		SELECT 		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID], [ResortPropertyID]
		FROM		[Building]
		ORDER BY	[BuildingID]
	END

GO

/* 	Author:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/
print '' print '*** Create Inspection Table ***'
GO
CREATE TABLE [dbo].[Inspection](
	[InspectionID]					[int]IDENTITY(100000, 1) 	NOT NULL,
	[ResortPropertyID]				[int]						NOT NULL,
	[Name]							[nvarchar](50)				NOT NULL,
	[DateInspected]					[date]						NOT NULL,
	[Rating]						[nvarchar](50)				NOT NULL,
	[ResortInspectionAffiliation]	[nvarchar](25)						,
	[InspectionProblemNotes]		[nvarchar](1000)					,
	[InspectionFixNotes]			[nvarchar](1000)					,
	
	CONSTRAINT[pk_InspectionID] PRIMARY KEY ([InspectionID] ASC),
	CONSTRAINT[fk_ResortPropertyID_Inspection] FOREIGN KEY ([ResortPropertyID])
		REFERENCES [dbo].[ResortProperty]([ResortPropertyID])
		ON UPDATE CASCADE
)
GO

/* 	Author:  Danielle Russo 
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

/* 	Author:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
print '' print '*** Creating sp_insert_inspection ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_inspection]
	(
		@ResortPropertyID				[int],
		@Name							[nvarchar](50),
		@DateInspected					[date],
		@Rating							[nvarchar](50),
		@ResortInspectionAffiliation	[nvarchar](25),
		@InspectionProblemNotes			[nvarchar](1000),
		@InspectionFixNotes				[nvarchar](1000)	
	)
AS
	BEGIN
		
		INSERT INTO [Inspection]
			(	
				[ResortPropertyID], 
				[Name], 
				[DateInspected], 
				[Rating], 
				[ResortInspectionAffiliation], 
				[InspectionProblemNotes], 
				[InspectionFixNotes]
			)
		VALUES
			(
				@ResortPropertyID,
				@Name,
				@DateInspected,
				@Rating,
				@ResortInspectionAffiliation,
				@InspectionProblemNotes,
				@InspectionFixNotes
			)
			
		SELECT [InspectionID] = @@IDENTITY
	END
GO

/* 	Author:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
print '' print '*** Creating sp_select_inspection_by_resortpropertyid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_inspection_by_resortpropertyid]
	(
		@ResortPropertyID	[int]
	)
AS
	BEGIN
		SELECT	[InspectionID],[Name],[DateInspected],[Rating],
				[ResortInspectionAffiliation],[InspectionProblemNotes],
				[InspectionFixNotes]
		FROM 	[Inspection]
		WHERE	[ResortPropertyID] = @ResortPropertyID
	END
GO

/* 	Author:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
print '' print '*** Creating sp_select_all_inspections ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_inspections]
AS
	BEGIN
		SELECT	[InspectionID],[ResortPropertyID],[Name],[DateInspected],
				[Rating],[ResortInspectionAffiliation],[InspectionProblemNotes],
				[InspectionFixNotes]
		FROM 	[Inspection]
	END
GO

/* 	Author:  Dani Russo 
	Created: 03/29/2019
	
	I  believe sp was created by Wes, but cannot find in the Master.
	Probably updated feilds
*/ 
print '' print '*** Create Procedure sp_select_room_list ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_list]
AS
	BEGIN
		SELECT 	[Room].[RoomID],
				[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity], 
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[OfferingID], 
				[Room].[RoomStatusID]
		FROM 	[Room], [Offering]
	END
GO

/* 	Author:  Dani Russo 
	Created: 03/29/2019
	
	I  believe sp was created by Wes, but cannot find in the Master.
	Probably updated feilds
*/ 
print '' print '*** Create Procedure sp_select_room_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_by_id]
(
		@RoomID	[int]
)
AS
	BEGIN
		SELECT 	[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity], 
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[OfferingID], 
				[Room].[RoomStatusID]
		FROM 	[Room], [Offering]
		WHERE 	[RoomID] = @RoomID
	END
GO

/* 	Author:  Dani Russo 
	Created: 03/29/2019
	
	I  believe sp was created by Wes, but cannot find in the Master.
*/ 
print '' print '*** Create Procedure sp_select_room_types ***'
GO
CREATE PROCEDURE [dbo].[sp_select_room_types]

AS
	BEGIN
		SELECT 	[RoomTypeID]
		FROM 	[RoomType]
	END
GO

/* 	Author:  Dani Russo 
	Created: 03/29/2019
	
*/ 
print '' print '*** Create Procedure sp_select_all_room_status ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_room_status]

AS
	BEGIN
		SELECT 	[RoomStatusID]
		FROM 	[RoomStatus]
	END
GO



/* 	Author:  Danielle Russo 
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
