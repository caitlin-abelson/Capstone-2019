/*@Author Phillip Hansen	
* @Created 1/23/2019
*/
print '' print '*** Inserting Event Records'
GO

INSERT INTO [dbo].[Event]
        ([EventTitle],[EmployeeID],[EventTypeID],[Description],[EventStartDate],
				[EventEndDate],[KidsAllowed],[NumGuests], [Location], [Sponsored], [SponsorID], [Approved])
    VALUES
        ('Opening Event', 100001, 'Concert Event', 'An opening event with live music.', '2019-05-13',
			'2019-05-20', 1, 100, 'Everywhere', 1, 1001, 1),
		('Steve Party', 100001, 'Concert Event', 'An opening event with live music.', '2019-05-13',
			'2019-05-20', 1, 100, 'Everywhere', 1, 1001, 1),
		('Boat Party', 100001, 'Concert Event', 'An opening event with live music.', '2019-05-13',
			'2019-05-20', 1, 100, 'Everywhere', 1, 1001, 1)
			
GO

/*
Author: Caitlin Abelson
Created Date: 2/25/19

Inserting the records into the Setup table
DateEntered is the date that the setup was created. 
DateRequired is the date that the setup needs to be done by
DateRequired needs to be after the date DateEntered.
*/
print '' print '*** Inserting Setup Test Records'
GO

INSERT INTO [dbo].[Setup]
		([EventID], [DateEntered], [DateRequired], [Comments])
	VALUES
	(100000, '2019-05-25', '2019-07-03', 'This is for a 4th of July Event.'),
	(100001, '2019-06-23', '2019-08-12', 'This is for a party.')
GO

/*
Author: Caitlin Abelson
Created Date: 2/25/19

Creating the Foreign Key constraint for SetupList
*/
print '' print '*** Inserting SetupList Test Records'
GO

INSERT INTO [dbo].[SetupList]
		([SetupID], [Completed], [Description], [Comments])
	VALUES
	(100000, 0, '4 Tables seating 5 people each. 20 chairs.', 'Jack, Jill, Gary. Needed to change seating per guest request.'),
	(100001, 0, 'Lots of people and lots of things.', 'Harry, Mark, Tracy. Heeds more cowbell.')
GO