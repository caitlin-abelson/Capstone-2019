/* Check whether the database exists and drop it if it does */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'MillennialResort_DB')
BEGIN
	DROP DATABASE [MillennialResort_DB];
	print '' print '*** Dropping database MillennialResort_DB'
END
GO

print '' print '*** Creating database MillennialResort_DB'
GO
CREATE DATABASE [MillennialResort_DB]
GO

print '' print'*** Using database MillennialResort_DB'
GO

USE [MillennialResort_DB]
GO

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

print '' print '*** Inserting Inactive Employee Test Records'
GO

INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active])
	VALUES
		('Joe', 'Shmoe', '1319551112', 'joe@company.com', 'Grooming', 0)
GO





print '' print '*** Creating Role Table'
GO

CREATE TABLE [dbo].[Role] (
	[RoleID]		[nvarchar](50)						NOT NULL,
	[Description]	[nvarchar](250)						,
	
	CONSTRAINT [pk_RoleID]	PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** Inserting Role Records'
GO

INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Rental', 'Rents Boats'),
		('Checkout', 'Checks Boats out'),
		('Inspection', 'Checks Boats In and Inspects Them'),
		('Maintenance', 'Repairs and Maintains Boats'),
		('Prep', 'Prepares Boats for Rental'),
		('Manager', 'Manages Boat Inventory'),
		('Admin', 'Administers Employee Roles')
GO

print '' print '*** Creating EmployeeRole Table'
GO

CREATE TABLE [dbo].[EmployeeRole](
	[EmployeeID]		[int]							NOT NULL,
	[RoleID]			[nvarchar](50)					NOT NULL
	
	CONSTRAINT [pk_EmployeeID_RoleID] PRIMARY KEY([EmployeeID] ASC, [RoleID] ASC)
)
GO

print '' print '*** Inserting EmployeeRole Records'
GO

INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100000, 'Rental'),
		(100001, 'Checkout'),
		(100001, 'Inspection'),
		(100001, 'Prep'),
		(100002, 'Maintenance'),
		(100002, 'Manager'),
		(100002, 'Admin')
GO

print '' print '*** Adding Foreign Key for EmployeeRole'

ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeID] FOREIGN KEY ([EmployeeID])
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE CASCADE
GO

print '' print '*** Adding Foreign Key RoleID for EmployeeRole'

ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_RoleID] FOREIGN KEY ([RoleID])
	REFERENCES [dbo].[Role]([RoleID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating sp_update_employee_email'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_email]
	(
		@EmployeeID			[int],
		@Email				[nvarchar](250),
		@OldEmail			[nvarchar](250),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [Email] = @Email
			WHERE [EmployeeID] = @EmployeeID
				AND [Email] = @OldEmail
				AND [PasswordHash] = @PasswordHash
			
		RETURN @@ROWCOUNT
	END
GO		 

print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
		@Email				[nvarchar](250),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([EmployeeID])
		FROM [Employee]
		WHERE [Email] = @Email 
			AND [PasswordHash] = @PasswordHash
			AND [Active] = 1
	END
GO

print '' print '*** Creating sp_retrieve_employee_roles' 
GO

CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles]
	(
		@Email					[nvarchar](250)
		
	)
AS
	BEGIN
		SELECT [RoleID]
		FROM [EmployeeRole]
		INNER JOIN [Employee] 
			ON [EmployeeRole].[EmployeeID] = [Employee].[EmployeeID]
		WHERE [Email] = @Email
	END
GO

print '' print '*** Creating sp_update_password_hash' 
GO

CREATE PROCEDURE [dbo].[sp_update_password_hash]
	(
		@Email				[nvarchar](250),
		@NewPasswordHash	[nvarchar](100),
		@OldPasswordHash	[nvarchar](100)
		
	)
AS
	BEGIN
		IF @NewPasswordHash != @OldPasswordHash 
		BEGIN
			UPDATE [Employee]
				SET [PasswordHash] = @NewPasswordHash
				WHERE [Email] = @Email
					AND [PasswordHash] = @OldPasswordHash
			RETURN @@ROWCOUNT
		END
	END
GO

print '' print '*** Creating sp_retrieve_user_names_by_email'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_user_names_by_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName]
		FROM Employee
		WHERE [Email] = @Email
	END
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
Created Date: 1/27/19

This stored procedure selects an employee by their ID.
*/
print '' print '*** Creating sp_select_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_id]
	(
		@EmployeeID				[int]
	)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [EmployeeID] = @EmployeeID
	END
GO

/*
Author: Caitlin Abelson
Created Date: 2/6/19

This stored procedure gets all of the employees from the employee table
*/
print '' print '*** Creating sp_select_all_employees'
GO
CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
	BEGIN
		SELECT  [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM 	[Employee]
	END
GO


/*
Author: Caitlin Abelson
Created Date: 2/14/19

This stored procedure gets all of the employees that are active
*/
print '' print '*** Creating sp_select_employee_active'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_active]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Active] = 1
	END
GO

/*
Author: Caitlin Abelson
Created Date: 2/14/19

This stored procedure gets all of the employees that are inactive
*/
print '' print '*** Creating sp_select_employee_inactive'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_inactive]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Active] = 0
	END
GO


/*
Author: Caitlin Abelson
Created Date: 2/1/19

This stored procedure updates an employee record in the employee table.
*/
print '' print '*** Creating sp_update_employee_by_id'
GO
CREATE PROCEDURE sp_update_employee_by_id
	(
		@EmployeeID			[int],
		
		@FirstName			[nvarchar](50),
		@LastName			[nvarchar](100),
		@PhoneNumber 		[nvarchar](11),
		@Email				[nvarchar](250),
		@DepartmentID		[nvarchar](50),
		@Active				[bit],
		
		@OldFirstName		[nvarchar](50),
		@OldLastName		[nvarchar](100),
		@OldPhoneNumber 	[nvarchar](11),
		@OldEmail			[nvarchar](250),
		@OldDepartmentID	[nvarchar](50),
		@OldActive			[bit]
	)
AS
	BEGIN
		UPDATE [Employee]
		SET		[FirstName] = @FirstName,
				[LastName] = @LastName,
				[PhoneNumber] = @PhoneNumber,
				[Email] = @Email,
				[DepartmentID] = @DepartmentID,
				[Active] = @Active
		WHERE	[EmployeeID] = @EmployeeID
		  AND	[FirstName] = @OldFirstName
		  AND	[LastName] = @OldLastName
		  AND	[PhoneNumber] = @OldPhoneNumber
		  AND	[Email] = @OldEmail
		  AND	[DepartmentID] = @OldDepartmentID
		  AND	[Active] = @OldActive
				
		RETURN @@ROWCOUNT
	END
GO
		
		
/*
Author: Caitlin Abelson
Created Date: 2/2/19

This stored procedure deactivates an employee record in the employee table.
*/
print '' print '*** Creating sp_deactivate_employee'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee]
	(
		@EmployeeID				[int]
	)
AS
	BEGIN
		UPDATE [Employee]
		SET [Active] = 0
		WHERE [EmployeeID] = @EmployeeID
		
		RETURN @@ROWCOUNT
	END
GO




/*
Author: Caitlin Abelson
Created Date: 2/12/19

This stored procedure deletes an employee record in the EmployeeRole table.
*/
print '' print '*** Creating sp_delete_employeeID_role'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeeID_role]
	(
		@EmployeeID		[int]
	)
	
AS
	BEGIN
		DELETE
		FROM [EmployeeRole]
		WHERE [EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO


/*
Author: Caitlin Abelson
Created Date: 2/2/19

This stored procedure deletes an employee record in the employee table.
*/
print '' print '*** Creating sp_delete_employee'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee]
	(
		@EmployeeID		[int]
	)
	
AS
	BEGIN
		DELETE
		FROM [Employee]
		WHERE [EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO
		
	
/*
Author: Caitlin Abelson
Created Date: 1/29/19

This stored procedure retrieves all of the departmentID's from the department table so that 
they can be used in the combo drop down box for inserting a new employee.
*/
print '' print '*** Creating sp_select_department'
GO
CREATE PROCEDURE [dbo].[sp_select_department]

AS
	BEGIN
		SELECT 	    [DepartmentID]
		FROM		[Department]
	END
GO





/*
Author: Caitlin Abelson
Created Date: 1/23/19

A table that holds all of the companies that supply goods for the rerort.
*/
print '' print '*** Creating Suppliers Table'
GO
CREATE TABLE [dbo].[Suppliers] (
	[SupplierID]		[int] IDENTITY(100000, 1) 	  NOT NULL,
	[Name]				[nvarchar](50)			  	  NOT NULL,
	[PhoneNumber] 		[nvarchar](11)				  NOT NULL,
	[SupplierEmail]		[nvarchar](250)				  NOT NULL,
	[DateAdded]			[date]						  NOT NULL DEFAULT
		'1900-01-01',
	[Address]			[nvarchar](100)			  	  NOT NULL,
	[City]				[nvarchar](100)			  	  NOT NULL,
	[StateCode]			[nvarchar](2)			  	  NOT NULL,
	[Country]			[nvarchar](25)				  NOT NULL,
	[ZipCode]			[nvarchar](6)			  	  NOT NULL,
	[ContactFirstName]	[nvarchar](50)			  	  NOT NULL,
	[ContactLastName]	[nvarchar](100)				  NOT NULL,
	[Description]  		[nvarchar](1000)			  NULL,
	[Active] 			[bit]						  NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_SupplierID] PRIMARY KEY([SupplierID] ASC),
	CONSTRAINT [ak_SupplierEmail] UNIQUE([SupplierEmail] ASC)
)
GO


/*
Author: Caitlin Abelson
Created Date: 1/23/19

These are the records for the suppliers that provide goods for the resort.
*/
print '' print '*** Inserting Supplier Test Records'
GO

INSERT INTO [dbo].[Suppliers]
		([Name], [ContactFirstName], [ContactLastName], [PhoneNumber], [SupplierEmail], [DateAdded], [Address], [City], [StateCode], [Country], [ZipCode], [Description])
	VALUES
		('Dunder Soaps', 'Jim', 'Halpert', '1319551111', 'dunder@soaps.com', '2002-03-14', '1234 Washington St.', 'Cedar Rapids', 'IA', 'USA', '52242', 'All of the tiny soaps for the hotel rooms are supplied by them.'),
		('Bob Vance Fruit', 'Bob', 'Vance', '1319551112', 'Bob-Vance@fruit.com', '1998-08-27', '1234 Washington St.', 'Iowa City', 'IA', 'USA', '52242', 'ALl of our fruits for the kitchen and catering come from them.'),
		('Plates and Silverware', 'Carly', 'Jones', '1319551116', 'plates@silverware.com', '2000-07-02', '1234 Washington St.', 'Cedar Rapids', 'IA', 'USA', '52242', 'The finest plates and silverware you will ever find.'),
		('Pets Plus', 'Kevin', 'Bentley', '1319551117', 'pets@plus.com', '1997-05-30', '1234 Washington St.', 'Des Moines', 'IA', 'USA', '52242', 'They give us the pet supplies so we can feed them and make them look nice.'),
		('Vending Machines', 'Harry', 'Plarth', '1319551118', 'vending@machines.com', '2016-12-20', '1234 Washington St.', 'North Liberty', 'IA', 'USA', '52242', 'They put the snack in the vending machines.'),
		('Alcohol Whole Supply', 'Frank', 'Welsh', '1319551119', 'alcohol@supply.com', '2018-01-19', '1234 Washington St.', 'Cedar Rapids', 'IA', 'USA', '52242', 'They supply us with all of our alcohol for the bars and the kitchen.')

GO

/*
Author: Caitlin Abelson
Created Date: 1/23/19

This stored procedure selects all of the fields from the suppliers table.
*/
print '' print '*** Creating sp_select_suppliers'
GO
CREATE PROCEDURE [dbo].[sp_select_suppliers]

AS
	BEGIN
		SELECT 	    [Name], [ContactFirstName], [ContactLastName],	[PhoneNumber], [SupplierEmail], [DateAdded], [Address], [City], [StateCode], [Country], [ZipCode], [Description], [Active]
		FROM		[Suppliers]
	END
GO



	




