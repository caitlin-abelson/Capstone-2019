USE [MillennialResort_DB]
GO

print '' print '***Inserting a fake Department record'
GO
INSERT INTO [dbo].[Department]
			([DepartmentID],[Description])
		VALUES
			('Admin','Admin Department')
GO

print '' print '***Inserting a fake Employee record'
GO
INSERT INTO [dbo].[Employee]
			([FirstName],[LastName],[PhoneNumber],[Email],[PasswordHash],[Active],[DepartmentID])
		VALUES
			('Admin','Admin',999-9999999,'admin@place.com'
			,'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e'
			,1,'Admin')
GO

print '' print '***Inserting a fake Offering Type record'
GO
INSERT INTO [dbo].[OfferingType]
			([OfferingTypeID],[Description])
		VALUES
			('Event','A description of what an Event is')
GO

print '' print '***Inserting a fake Offering record'
GO
INSERT INTO [dbo].[Offering]
			([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
			('Event',100000,'A description for a fake Offering',100.00)
GO
		
print '' print '***Inserting a fake Sponsor record'
GO
INSERT INTO [dbo].[Sponsor]
			([SponsorID],[Name],[Address],[City],[State],[PhoneNumber],
				[Email],[ContactFirstName],[ContactLastName],[DateAdded],[Active])
		VALUES
			(110000,'FakeSponsor','123 Seasame Street','Detroit','MI','999 9999999',
				'fakeSpons@sponsor.com', 'Fake','Fakerson', '2019-01-01', 1)
GO

print '' print '***Inserting fake Event records'
GO
INSERT INTO [dbo].[Event]
			([OfferingID],[EventTitle],[EmployeeID],[EventTypeID],[Description],
				[EventStartDate],[EventEndDate],[KidsAllowed],[NumGuests],[Location],
				[Sponsored],[Approved],[Cancelled],[SeatsRemaining],[PublicEvent])
		VALUES
			(100000,'Fake Event Title',100000,'Beach Party','Fake Event Description',
				'2020-01-02','2020-01-04',0,500,'Beach',1,1,0,100,1),
			(100000,'Fake Cancelled Event',100000,'Beach Party','Fake Event Description',
				'2020-01-02','2020-01-04',0,500,'Beach',0,0,1,100,0)
GO	

print '' print '***Inserting fake EventSponsor record'
GO
INSERT INTO [dbo].[EventSponsor]
			([EventID], [SponsorID])
		VALUES
			(100000, 110000)
GO

print '' print '***Inserting a fake Performance record'
GO
INSERT INTO [dbo].[Performance]
			([PerformanceDate],[Description],[PerformanceTitle],[Cancelled])
		VALUES
			('01-01-2020','Fake Performance Description','Performancers',0)
GO

