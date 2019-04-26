/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
print '' print '*** Inserting House Keeping Request Test Records'
GO

INSERT INTO [HouseKeepingRequest]
			([BuildingNumber], [RoomNumber], [Description], [WorkingEmployeeID], [Active])
		VALUES
			(1, 1, "Timmy Threw Up All Over The Floor.", 100000, 1),
			(2, 2, "We Need Extra Towels For Tomorrow Morning.", 100000, 1)
GO