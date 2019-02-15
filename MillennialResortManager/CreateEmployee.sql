/*
Author: Caitlin Abelson
Created Date: 1/27/19

The department table is for the various departments that the employees work in at the resort.
*/
print '' print '*** Creating table for Department'
GO
CREATE TABLE [dbo].[Department] (
	[DepartmentID]  [nvarchar](50)				  NOT NULL,
	[Description]	[nvarchar](100)				  NULL,
	
	CONSTRAINT [pk_DepartmentID] PRIMARY KEY ([DepartmentID] ASC)
)
GO


/*
Author: Caitlin Abelson
Updated Date: 1/27/19

Changes Made: DepartmentID was added to the employee table
*/
print '' print '*** Creating Employee Table'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]	[int] IDENTITY(100000, 1) 	  NOT NULL,
	[FirstName]		[nvarchar](50)			  	  NOT NULL,
	[LastName]		[nvarchar](100)				  NOT NULL,
	[PhoneNumber] 	[nvarchar](11)				  NOT NULL,
	[Email]			[nvarchar](250)				  NOT NULL,
	[PasswordHash]	[nvarchar](100)				  NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active] 		[bit]						  NOT NULL DEFAULT 1,
	[DepartmentID]  [nvarchar](50)				  NOT NULL,
	
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

/*
Author: Caitlin Abelson
Created Date: 1/27/19

This adds a foreign key constraint of DepartmentID to the table Employee from the Deparment table.
*/
print '' print '*** Adding Foreign Key for Employee'

ALTER TABLE [dbo].[Employee] WITH NOCHECK
	ADD CONSTRAINT [fk_DepartmentID] FOREIGN KEY ([DepartmentID])
	REFERENCES [dbo].[Department]([DepartmentID])
	ON UPDATE CASCADE
GO


/*
Author: Caitlin Abelson
Created Date: 1/27/19

These are the records for the departments that the employees work in at the resort.
*/
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

/*
Author: Caitlin Abelson
Updated Date: 1/27/19

Changes Made: DepartmentID was added to the employee test records since it is now a field in the employee table
*/
print '' print '*** Inserting Employee Test Records'
GO

INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID])
	VALUES
		('Joanne', 'Smith', '1319551111', 'joanne@company.com', 'Events'),
		('Martin', 'Jones', '1319551111', 'martin@company.com', 'Kitchen'),
		('Leo', 'Williams', '1319551111', 'leo@company.com', 'Catering')
GO

/*
Author: Caitlin Abelson
Created Date: 1/27/19

This stored procedure inserts a new employee record into the employee table.
*/
print '' print '*** Creating sp_insert_employee'
GO
CREATE PROCEDURE [sp_insert_employee]
	(
		@FirstName		[nvarchar](50),
		@LastName		[nvarchar](100),
		@PhoneNumber 	[nvarchar](11),
		@Email			[nvarchar](250),
		@DepartmentID	[nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO [Employee]
			([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID])
		VALUES
			(@FirstName, @LastName, @PhoneNumber, @Email, @DepartmentID)
	  
		RETURN @@ROWCOUNT
	END
GO


/*
Author: Caitlin Abelson
Created Date: 1/29/19

This stored procedure retrieves all of the departmentID's from the department table so that 
they can be used in the combo drop down box for inserting a new employee.
*/
print '' print '*** Creating sp_retrieve_department'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_department]

AS
	BEGIN
		SELECT 	    [DepartmentID]
		FROM		[Department]
	END
GO