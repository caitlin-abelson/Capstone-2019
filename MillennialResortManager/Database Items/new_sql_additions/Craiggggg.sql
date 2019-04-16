USE [MillennialResort_DB]
GO
print '' print '*** Creating inserting room data'
GO
		INSERT INTO [Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capactiy], 
			[RoomStatusID], [Capacity], [ResortPropertyID])
		VALUES
			(1, "Big Building", "Big Room", "The Biggest Room", "Big", 80, 100000)
GO