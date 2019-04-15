USE [MillennialResort_DB]
GO
print '' print '***Inserting a fake Room record'
GO
INSERT INTO [dbo].[Room]
			([RoomNumber],[BuildingID],[RoomTypeID],[Description],[Price],[Active],[Capacity],[Available],[ResortPropertyID])
		VALUES
			(10,100000, 'Single','This is a single room', 2500 ,1, 2,1, 227)
GO

