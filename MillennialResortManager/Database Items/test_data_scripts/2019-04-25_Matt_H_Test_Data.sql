USE [MillennialResort_DB]
GO

-- Matt H. Test Data 4/25/19 --

-- [NOTE: 4/27/19 - Matt H.] --
-- Due to the fact that registering a new user on the web site adds a member entry to the
-- Member table in the DB, but there is currently no way that i or other web team members know to log in or access already existing members
-- from the web site, there is a certain order i've had to run db scripts to properly test my feature.

-- I have to run the working db builder, then run new sql additions to add my membertabline stored procedure, then
-- run just the existing test data via the fulltestdata.sql file to get the first six members into the db, and then open VS and register a user via
-- the site. Registering a user on the site then adds a new member to the member table, as well as membertab table, and currently that entry has a membertabid 
-- of 100007, which matches the membertabid foreign key in all of my test data entries. pretty much, what this means is that come presentation day, in order
-- for my mebertabline web feature to be tested and have actual test data, a new user will need to be created on the website, and then my insert statements below will 
-- need to be ran, in that order. will also have to ensure that the memberid and membertabid's being inserted below are 100007, or match whatever is inserted when the 
-- new user is created.

print '' print '*** Inserting MemberTab Test Records'
GO
INSERT INTO [dbo].[MemberTab]
		([MemberID], [TotalPrice])
	VALUES
		(100007, 583.35)
GO

print '' print '*** Inserting MemberTabLine Test Records'
GO		
INSERT INTO [dbo].[MemberTabLine]
		([MemberTabID], [OfferingID], [Quantity], [Price], [EmployeeID], [GuestID])
	VALUES
		(100007, 100002, 1, 130.45, 100000, 100000),
		(100007, 100010, 1, 25, 100000, 100000),
		(100007, 100004, 2, 25.45, 100000, 100000),
		(100007, 100006, 2, 1, 100000, 100000),
		(100007, 100013, 1, 350, 100000, 100000),
		(100007, 100010, 1, 25, 100000, 100000),
		(100007, 100004, 2, 25.45, 100000, 100000),
		(100007, 100005, 1, 85.45, 100000, 100000),
		(100007, 100011, 1, 15, 100000, 100000),
		(100007, 100001, 1, 13.45, 100000, 100000),
		(100007, 100000, 1, 10, 100000, 100000),
		(100007, 100009, 1, 65, 100000, 100000)
GO		