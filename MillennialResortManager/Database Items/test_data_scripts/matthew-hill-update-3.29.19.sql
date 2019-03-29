USE [MillennialResort_DB]

INSERT INTO [dbo].[Member]
		([FirstName], [LastName], [PhoneNumber], [Email], [PasswordHash])
	VALUES
		('Bobby', 'Smith', '3198888888', 'bsmith1@gmail.com', 'password123')
GO


print '' print '*** Inserting Guest Test Records'
GO
INSERT INTO [dbo].[Guest]
		([MemberID], [GuestTypeID], [FirstName], 
		[LastName], [PhoneNumber], [Email], 
		[Minor], [ReceiveTexts], [EmergencyFirstName], 
		[EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation])
	VALUES
		(100000, 'Basic guest', 'Sam', 'Andrews', '3199999999', 'sam1@gmail.com', 0, 1, 'Lana', 'Andrews', '3192222222', 'Aunt'),
		(100000, 'Basic guest', 'Kelly', 'Joe', '3199999998', 'kel1@gmail.com', 0, 1, 'Beth', 'Joe', '3192222225', 'Aunt'),
		(100000, 'Basic guest', 'Dave', 'Gray', '3199999992', 'greygrey@gmail.com', 0, 1, 'Mike', 'Andrews', '3192222228', 'Uncle'),
		(100000, 'Basic guest', 'Doug', 'Cars', '3199999944', 'dougg@gmail.com', 0, 1, 'Josh', 'Andrews', '3192222277', 'Brother')
GO

print '' print '*** Inserting Pet Test Records'
GO
INSERT INTO [dbo].[Pet]
		([PetName], [Gender], [Species], [PetTypeID], [GuestID])
	VALUES
		('Pup', 'Male', 'Lab', 'UNASSIGNED', 100000),
		('Ruff', 'Male', 'Terrier', 'UNASSIGNED', 100001),
		('Spot', 'Male', 'Lab', 'UNASSIGNED', 100002)
GO

print '' print '*** Inserting Pet Image FileName Test Records'
GO
INSERT INTO [dbo].[PetImageFileName]
		([Filename], [PetID])
	VALUES 
		('pet1.jpg', 100000),
		('pet2.jpg', 100001),
		('pet3.jpg', 100002)
GO

