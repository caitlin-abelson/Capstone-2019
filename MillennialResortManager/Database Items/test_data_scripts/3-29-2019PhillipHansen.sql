USE [MillennialResort_DB]
GO

print '' print '*Inserting a fake Offering Type record'
GO
INSERT INTO [dbo].[OfferingType]
			([OfferingTypeID], [Description])
		VALUES
			('FakeOffType','A Fake Offering Type Description')
GO

print '' print '*Inserting a fake Offering record'
GO
INSERT INTO [dbo].[Offering]
			([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
			('FakeOffType',100000,'A description for a fake Offering',100.00)
GO
		
print '' print '*Inserting a fake Sponsor record'
GO
INSERT INTO [dbo].[Sponsor]
			([SponsorID],[Name],[Address],[City],[State],[PhoneNumber],
				[Email],[ContactFirstName],[ContactLastName],[DateAdded],[Active])
		VALUES
			(110000,'FakeSponsor','123 Seasame Street','Detroit','MI','999 9999999',
				'fakeSpons@sponsor.com', 'Fake','Fakerson', '2019-01-01', 1)
GO

print '' print '*Inserting a fake Event record'
GO
INSERT INTO [dbo].[Event]
			([OfferingID],[EventTitle],[EmployeeID],[EventTypeID],[Description],
				[EventStartDate],[EventEndDate],[KidsAllowed],[NumGuests],[Location],
				[Sponsored],[Approved],[Cancelled],[SeatsRemaining],[PublicEvent])
		VALUES
			(100000,'Fake Event Title',100000,'Beach Party','Fake Event Description',
				'2019-01-02','2019-01-04',0,500,'Beach',1,1,0,100,1)
GO	