USE [MillennialResort_DB]
GO



print '' print '*** Inserting Member Records'
GO
INSERT INTO [dbo].[Member]
		([FirstName], [LastName], [PhoneNumber], [Email], [PasswordHash], [Active])
	VALUES
		('Joe', 'Blow', 13191231234, 'blowj@domain.com', '7', 1),
		('John', 'Doe', 13194443333, 'Doej@domain.com', '8', 1),
		('Tom', 'Cat', 13193214321, 'catt@domain.com', '9', 1),
		('Bill', 'Bob', 13193333333, 'bobb@domain.com', '10', 1)
GO

print '' print '*** Inserting Guest Records'
GO
INSERT INTO [dbo].[Guest]
		([MemberID],	
		[GuestTypeID], 
		[FirstName], 
		[LastName], 
		[PhoneNumber],
		[Email],	
		[Minor], 
		[Active],	
		[ReceiveTexts],	
		[EmergencyFirstName], 
		[EmergencyLastName], 
		[EmergencyPhoneNumber], 
		[EmergencyRelation],	
		[CheckedIn])
	VALUES
		(100000, 'Basic guest', 'Joe', 'Blow', 13191231234, 'blowj@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1),
		(100001, 'Basic guest', 'John', 'Doe', 13194443333, 'doej@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1),
		(100002, 'Basic guest', 'Tom', 'Cat', 13193214321, 'catt@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1),
		(100003, 'Basic guest', 'Bill', 'Bob', 13193333333, 'bobb@domain.com', 0, 1, 0, 'nonya', 'business', 13191111111, 'Satire', 1)
GO
/*
print '' print '*** Inserting Luggage Records'
GO
INSERT INTO [dbo].[Luggage]
		([GuestID], [LuggageStatusID])
	VALUES
		(100000, 'In Lobby'),
		(100001, 'In Room'),
		(100002, 'In Room'),
		(100003, 'In Transit')
GO*/

print '' print '*** Inserting Performance Records'
GO
INSERT INTO [dbo].[Performance]
		([PerformanceDate], [Description], [PerformanceTitle], [Cancelled])
	VALUES
		('2019-06-27', 'Firebreather, Nuf said', 'Firebreather', 0),
		('2019-08-15', 'Jason the just alright Juggler', 'Juggler', 0),
		('2019-09-02', 'Timmy the two ton terror', 'Sumo Championship', 0)
GO