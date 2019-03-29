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
			