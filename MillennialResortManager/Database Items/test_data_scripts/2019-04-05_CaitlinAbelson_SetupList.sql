USE [MillennialResort_DB]
GO

/*Done by Gunardi(?)*/
print '' print '*** Inserting Sponsor Record'
GO

INSERT INTO [dbo].[Sponsor]
        ([SponsorID], [Name], [Address], [City], [State], [PhoneNumber], [Email], 
			[ContactFirstName], [ContactLastName], [DateAdded], [Active])
    VALUES
        (1001, 'Best Resorts', '123 Seuss Street', 'Shell City', 'IA', '111-1111111', 'bestresorts@email.com', 'John', 'Doe', '2017-06-21', 1),
		(0, ' ', 'NA', 'NA', 'IA', 'NA', 'NA', 'NA', 'NA', '2010-01-01', 0)

		
GO


/*
print '' print '*** Inserting test records for Department'
GO
INSERT INTO [dbo].[Department]
		([DepartmentID], [Description])
	VALUES
		('Events','This employee handles the day to day for the events that happen at our resort.'),
		('Kitchen','This employee is one of our kitchen staff that prepared meals at our restaurant.'),
		('Catering','This employee works on getting food to and from our various events that we host at the resort.'),
		('Grooming','This employee tends to the salon needs of the pets that visit our resort.'),
		('Talent','This employee provides entertainment at events that are hosted at our resort.')
GO


print '' print '*** Inserting Employee Test Records'
GO

INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID])
	VALUES
		('Joanne', 'Smith', '1319551111', 'joanne@company.com', 'Events'),
		('Martin', 'Jones', '1319551111', 'martin@company.com', 'Kitchen'),
		('Leo', 'Williams', '1319551111', 'leo@company.com', 'Catering')
GO

print '' print '***Inserting a fake Offering Type record'
GO
INSERT INTO [dbo].[OfferingType]
			([OfferingTypeID], [Description])
		VALUES
			('Event','A Description if you dont know what an Event is')
GO
*/
print '' print '***Inserting a fake Offering record'
GO
INSERT INTO [dbo].[Offering]
			([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
			('Event',100000,'A description for a fake Offering',100.00)
GO

print '' print '*** Inserting Event Type Records'
GO

INSERT INTO [dbo].[EventType]
        ([EventTypeID], [Description])
    VALUES
        ('Concert Event', 'A concert is a live music performance in front of an audience.'),
        ('Beach Party', 'There are plenty of opportunities to have a great time at the beach.'),
        ('Wedding', 'Romantic Florals typically make up a romantic wedding also those who never been one to take the normal route?')
GO


/*@Author Phillip Hansen	
* @Created 1/23/2019
*/
print '' print '*** Inserting Event Records'
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

/*
Author: Caitlin Abelson
Created Date: 2/25/19

Creating the Foreign Key constraint for Setup
*/
ALTER TABLE [dbo].[Setup] WITH NOCHECK
	ADD CONSTRAINT [fk_EventID] FOREIGN KEY ([EventID])
	REFERENCES [dbo].[Event]([EventID])
	ON UPDATE CASCADE
GO

DROP TABLE [dbo].[SetupList]
GO
/*
Author: Caitlin Abelson
Created Date: 2/25/19

Creating the table for SetupList

Description is an informal list and information. It would contain the items needed 
to be completed as well as other instruction to make those be complete.

Comments are for those working on the setup that might have to state why it 
couldn't be completed or to state they did something different. 
*/
CREATE TABLE [dbo].[SetupList] (
	[SetupListID]		[int] IDENTITY(100000, 1) 	  NOT NULL,
	[SetupID]			[int]						  NOT NULL,
	[Completed]			[bit]					  	  NOT NULL DEFAULT 0,
	[Description]		[nvarchar](1000)			  NOT NULL,
	[Comments]			[nvarchar](1000)			  NOT NULL,

	CONSTRAINT [pk_SetupListID] PRIMARY KEY([SetupListID] ASC),
)
GO



/*
Author: Caitlin Abelson
Created Date: 2/25/19

Creating the Foreign Key constraint for SetupList
*/
print '' print '*** Adding Foreign Key for SetupList'

ALTER TABLE [dbo].[SetupList] WITH NOCHECK
	ADD CONSTRAINT [fk_SetupID] FOREIGN KEY ([SetupID])
	REFERENCES [dbo].[Setup]([SetupID])
	ON UPDATE CASCADE
GO



/*
Author: Caitlin Abelson
Created Date: 2/25/19

This is the stored procedure for create setup
*/
print '' print '*** Creating sp_insert_setup'
GO
CREATE PROCEDURE [sp_insert_setup]
	(
		@EventID		[int],
		@DateEntered	[date],
		@DateRequired 	[date],
		@Comments		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [Setup]
			([EventID], [DateEntered], [DateRequired], [Comments])
		VALUES
			(@EventID, @DateEntered, @DateRequired, @Comments)
	  
		SELECT SCOPE_IDENTITY()
	END
GO



/*
Author: Caitlin Abelson
Created Date: 2/25/19

This is the stored procedure for selecting a setupID from setup
*/
print '' print '*** Creating sp_select_setup_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_setup_by_id]
	(
		@SetupID				[int]
	)
AS
	BEGIN
		SELECT [SetupID], [EventID], [DateEntered], [DateRequired], [Comments]
		FROM [Setup]
		WHERE [SetupID] = @SetupID
	END
GO


/*
Author: Caitlin Abelson
Created Date: 2/28/19

This is the stored procedure for selecting all of the setups
for Setup
*/
print '' print '*** Creating sp_select_all_setups'
GO
CREATE PROCEDURE [dbo].[sp_select_all_setups]
AS
	BEGIN
		SELECT [SetupID], [EventID], [DateEntered], [DateRequired], [Comments]
		FROM [Setup]
	END
GO

/*
Author: Caitlin Abelson
Created Date: 3/6/19

This is the stored procedure for selecting all SetupIDs from Setup
and EventTitles from Event
*/
print '' print '*** Creating sp_select_setup_and_event_title'
GO
CREATE PROCEDURE [dbo].[sp_select_setup_and_event_title]
AS 
	SELECT [Setup].[SetupID], [Setup].[EventID], [Setup].[DateEntered], [Setup].[DateRequired],
			[Event].[EventTitle]
	FROM [Setup] inner join [Event] on
		[Setup].[EventID] = [Event].[EventID]

GO


/*
Author: Caitlin Abelson
Created Date: 3/11/19
The stored procedure for updating a setup
*/
print '' print '*** Creating sp_update_setup_by_id'
GO
CREATE PROCEDURE sp_update_setup_by_id
	(
		@SetupID			[int],
		
		@EventID			[int],
		@DateRequired		[date],
		@Comments			[nvarchar](1000),
		
		@OldEventID			[int],
		@OldDateRequired	[date],
		@OldComments		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [Setup]
		SET		[EventID] = @EventID,
				[DateRequired] = @DateRequired,
				[Comments] = @Comments
		WHERE	[SetupID] = @SetupID
		  AND	[EventID] = @OldEventID
		  AND	[DateRequired] = @OldDateRequired
		  AND	[Comments] = @OldComments
		
		RETURN @@ROWCOUNT
	END
GO

/*
Author: Caitlin Abelson
Date: 2019-03-19

Stored procedure to delete a setup
*/
print '' print '*** Creating sp_delete_setup'
GO
CREATE PROCEDURE [dbo].[sp_delete_setup]
	(
		@SetupID		[int]
	)
	
AS
	BEGIN
		DELETE
		FROM [Setup]
		WHERE [SetupID] = @SetupID
		RETURN @@ROWCOUNT
	END
GO


/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the DateEntereds from setup
*/
print '' print '*** Creating sp_select_date_entered'
GO
CREATE PROCEDURE [dbo].[sp_select_date_entered]
	(
		@DateEntered	[date]
	)
AS
	BEGIN
		SELECT [Setup].[SetupID], [Setup].[EventID], [Setup].[DateEntered], [Setup].[DateRequired],
			[Event].[EventTitle]
		FROM [Setup] inner join [Event] on
			[Setup].[EventID] = [Event].[EventID]
		WHERE	[Setup].[DateEntered] = @DateEntered
	END
GO


/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the DateRequireds from setup
*/
print '' print '*** Creating sp_select_date_required'
GO
CREATE PROCEDURE [dbo].[sp_select_date_required]
	(
		@DateRequired	[date]
	)
AS
	BEGIN
		SELECT [Setup].[SetupID], [Setup].[EventID], [Setup].[DateEntered], [Setup].[DateRequired],
			[Event].[EventTitle]
		FROM [Setup] inner join [Event] on
		[Setup].[EventID] = [Event].[EventID]
		WHERE	[Setup].[DateRequired] = @DateRequired
	END
GO



/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the Comments from setup
*/
print '' print '*** Creating sp_select_setup_event_title'
GO
CREATE PROCEDURE [dbo].[sp_select_setup_event_title]
	(
		@EventTitle	[nvarchar](50)
	)
AS
	BEGIN
		SELECT [Setup].[SetupID], [Setup].[EventID], [Setup].[DateEntered], [Setup].[DateRequired],
			[Event].[EventTitle]
		FROM [Setup] inner join [Event] on
		[Setup].[EventID] = [Event].[EventID]
		WHERE	[EventTitle] = @EventTitle
	END
GO


print '' print '*** Creating sp_delete_setup_and_setup_list'
GO
CREATE PROCEDURE [dbo].[sp_delete_setup_and_setup_list]
	(
		@SetupID		[int]
	)

AS
	BEGIN TRY
		BEGIN TRANSACTION
			DELETE FROM [SetupList] WHERE [SetupID] = @SetupID	
			DELETE FROM [Setup] WHERE [SetupID] = @SetupID	

	END TRY
	
	BEGIN CATCH
	
		ROLLBACK TRANSACTION
		
	END CATCH
	
	COMMIT

GO


/*
Author: Caitlin Abelson
Created Date: 2/25/19

This is the stored procedure for create setupList
*/
print '' print '*** Creating sp_insert_SetupList'
GO
CREATE PROCEDURE [sp_insert_SetupList]
	(
		@SetupID		[int],
		@Completed		[bit],
		@Description 	[nvarchar](1000),
		@Comments		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [SetupList]
			([SetupID], [Completed], [Description], [Comments])
		VALUES
			(@SetupID, @Completed, @Description, @Comments)
	  
		RETURN @@ROWCOUNT
	END
GO

/*
Author: Caitlin Abelson
Created Date: 3/5/19

This is the stored procedure for selecting a setupID from setupList
*/
print '' print '*** Creating sp_select_setupList_setup_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_setupList_setup_by_id]
	(
		@SetupID				[int]
	)
AS
	BEGIN
		SELECT [SetupID], [Completed], [Description], [Comments]
		FROM [SetupList]
		WHERE [SetupID] = @SetupID
	END
GO






/*
Author: Caitlin Abelson
Created Date: 3/11/19

The stored procedure for updating a setupList.
*/
print '' print '*** Creating sp_update_setupList_by_id'
GO
CREATE PROCEDURE sp_update_setupList_by_id
	(
		@SetupListID		[int],
		
		@Completed			[bit],
		@Description		[nvarchar](1000),
		@Comments			[nvarchar](1000),
		
		@OldCompleted		[bit],
		@OldDescription		[nvarchar](1000),
		@OldComments		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [SetupList]
		SET		[Completed] = @Completed,
				[Description] = @Description,
				[Comments] = @Comments
		WHERE	[Completed] = @OldCompleted
		AND		[SetupListID] = @SetupListID
		  AND	[Description] = @OldDescription
		  AND	[Comments] = @OldComments
		  
		RETURN @@ROWCOUNT
	END
GO


/*
Author: Caitlin Abelson
Date: 2019-03-14

Stored procedure to get the get all of the setupLists as well as the EventTitles
*/
print '' print '*** Creating sp_select_setupList_and_event_title'
GO
CREATE PROCEDURE [dbo].[sp_select_setupList_and_event_title]
AS 
	SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
			[SetupList].[Comments]
	FROM 	[SetupList] inner join [Setup] on
			[SetupList].[SetupID] = [Setup].[SetupID]
			inner join [Event] on
			[Setup].[EventID] = [Event].[EventID]

GO

/*
Author: Caitlin Abelson
Date: 2019-03-19

Stored procedure to delete a setupList
*/
print '' print '*** Creating sp_delete_setuplist'
GO
CREATE PROCEDURE [dbo].[sp_delete_setuplist]
	(
		@SetupListID		[int]
	)
	
AS
	BEGIN
		DELETE
		FROM [SetupList]
		WHERE [SetupListID] = @SetupListID
		RETURN @@ROWCOUNT
	END
GO

/*
Author: Caitlin Abelson
Date: 2019-03-19

Stored procedure to deactivate a setupList
*/
print '' print '*** Creating sp_deactivate_setuplist'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_setuplist]
	(
		@SetupListID				[int]
	)
AS
	BEGIN
		UPDATE [SetupList]
		SET [Completed] = 0
		WHERE [SetupListID] = @SetupListID
		
		RETURN @@ROWCOUNT
	END
GO	

/*
Author: Caitlin Abelson
Date: 2019-03-19

Stored procedure to select all of the completed SetupLists
*/
print '' print '*** Creating sp_select_all_completed_setuplists'
GO
CREATE PROCEDURE [dbo].[sp_select_all_completed_setuplists]
AS
	BEGIN
		SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
				[SetupList].[Comments]
		FROM 	[SetupList] inner join [Setup] on
				[SetupList].[SetupID] = [Setup].[SetupID]
							inner join [Event] on
				[Setup].[EventID] = [Event].[EventID]
		WHERE	[SetupList].[Completed] = 1
	END
GO


/*
Author: Caitlin Abelson
Date: 2019-03-19

Stored procedure to select all of the incomplete SetupLists
*/
print '' print '*** Creating sp_select_all_incomplete_setuplists'
GO
CREATE PROCEDURE [dbo].[sp_select_all_incomplete_setuplists]
AS
	BEGIN
		SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
				[SetupList].[Comments]
		FROM 	[SetupList] inner join [Setup] on
				[SetupList].[SetupID] = [Setup].[SetupID]
							inner join [Event] on
				[Setup].[EventID] = [Event].[EventID]
		WHERE	[SetupList].[Completed] = 0
	END
GO


/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the Completed from setupLists from setupList
*/
print '' print '*** Creating sp_select_completed'
GO
CREATE PROCEDURE [dbo].[sp_select_completed]
AS
	BEGIN
		SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
				[SetupList].[Comments]
		FROM 	[SetupList] inner join [Setup] on
				[SetupList].[SetupID] = [Setup].[SetupID]
							inner join [Event] on
				[Setup].[EventID] = [Event].[EventID]
		WHERE	[SetupList].[Completed] = 1
	END
GO



/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the Incomplete from setupLists from setupList
*/
print '' print '*** Creating sp_select_incomplete'
GO
CREATE PROCEDURE [dbo].[sp_select_incomplete]
AS
	BEGIN
		SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
				[SetupList].[Comments]
		FROM 	[SetupList] inner join [Setup] on
				[SetupList].[SetupID] = [Setup].[SetupID]
							inner join [Event] on
				[Setup].[EventID] = [Event].[EventID]
		WHERE	[SetupList].[Completed] = 0
	END
GO

/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the Descriptions from setupList
*/
print '' print '*** Creating sp_select_description'
GO
CREATE PROCEDURE [dbo].[sp_select_description]
	(
		@Description 	[nvarchar](1000)
	)
AS
	BEGIN
		SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
				[SetupList].[Comments]
		FROM 	[SetupList] inner join [Setup] on
				[SetupList].[SetupID] = [Setup].[SetupID]
							inner join [Event] on
				[Setup].[EventID] = [Event].[EventID]
		WHERE	[SetupList].[Description] = @Description
	END
GO

/*
Author: Caitlin Abelson
Created Date: 3-20-19

Selects all of the Comments from setupList
*/
print '' print '*** Creating sp_select_setup_list_comments'
GO
CREATE PROCEDURE [dbo].[sp_select_setup_list_comments]
	(
		@Comments 	[nvarchar](1000)
	)
AS
	BEGIN
		SELECT  [Event].[EventTitle], [SetupList].[SetupListID], [SetupList].[SetupID], [SetupList].[Completed], [SetupList].[Description], 
				[SetupList].[Comments]
		FROM 	[SetupList] inner join [Setup] on
				[SetupList].[SetupID] = [Setup].[SetupID]
							inner join [Event] on
				[Setup].[EventID] = [Event].[EventID]
		WHERE	[SetupList].[Comments] = @Comments
	END
GO


-- Name: Eduardo Colon
-- Date: 2019-03-05
print '' print '*** Creating sp_select_all_setuplists'
GO
CREATE PROCEDURE [dbo].[sp_select_all_setuplists]
AS
	BEGIN
		SELECT 	    [SetupListID], [SetupID], [Completed], [Description],	[Comments]
		FROM		[SetupList]
	END
GO
	
-- NAME:  Eduardo Colon'
-- Date:   2019-03-05'
print '' print '*** Creating sp_select_setuplist_by_id '
GO
CREATE PROCEDURE [dbo].[sp_select_setuplist_by_id]
	(
		@SetupListID				[int]
	)
AS
	BEGIN
		SELECT [SetupListID], [SetupID],[Completed],[Description], [Comments]
		FROM [SetupList]
		WHERE [SetupListID] = @SetupListID
	END
GO	


------------------------- TEST DATA ---------------------------------------



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