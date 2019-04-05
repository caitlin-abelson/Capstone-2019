USE [MillennialResort_DB]
GO

print '' print '***Inserting a fake Offering Type record'
GO
INSERT INTO [dbo].[OfferingType]
			([OfferingTypeID], [Description])
		VALUES
			('Event','A Description if you dont know what an Event is')
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