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
/*
Author: Kevin Broskow
Created Date: 1/28/19

The Product table is used to keep track of all the products the resort uses.
*/
print '' print '*** Creating Product Table'
GO

CREATE TABLE [dbo].[Product](
	[ItemID]		[int]	IDENTITY(100000, 1)						NOT NULL,
	[ItemTypeID]			[nvarchar](15)			NOT NULL,
	[Description]			[nvarchar](250),
	[OnHandQuantity]	[int]				NOT NULL,
	[Name]				[nvarchar](50)		NOT NULL,
	[ReOrderQuantity]	[int]				NOT NULL,
	[DateActive]		[date]				NOT NULL,
	[Active]			[bit]				NOT NULL	DEFAULT 1,
	[CustomerPurchasable]	[bit]			NOT NULL 	DEFAULT 1,
	[RecipeID]			[int]				,
	
	CONSTRAINT [pk_ItemID] PRIMARY KEY([ItemID] ASC)
)
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

The ItemType table is used to keep track of all the Item types for products that the resort uses.
*/
print '' print '*** Creating ItemType Table'
GO
CREATE TABLE [dbo].[ItemType] (
	[ItemTypeID]		[nvarchar](15)				NOT NULL,
	[Description]		[nvarchar](250)				,
	CONSTRAINT [pk_ItemTypeID] PRIMARY KEY([ItemTypeID] ASC)
)
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

Used to at the forgeign key constraint on the Product table for ItemTypes
*/
print '' print '*** Adding Foreign Key ItemTypeID for Product'
GO
ALTER TABLE [dbo].[Product] WITH NOCHECK
	ADD CONSTRAINT [fk_ItemTypeID] FOREIGN KEY([ItemTypeID])
	REFERENCES [dbo].[ItemType]([ItemTypeID])
	ON UPDATE CASCADE
GO

/*
Author: Kevin Broskow
Created Date: 1/28/19

Used to insert generic ItemTypes for testing purposees. Client has not given a list of item types they wish
to use.
*/
print '' print '*** Inserting ItemType Records'
GO

INSERT INTO [dbo].[ItemType]
		([ItemTypeID])
	VALUES
		('Food'),
		('Hot Sauce'),
		('Shoe'),
		('Hat'),
		('Tshirt'),
		('Pet'),
		('Beverage')
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

This stored procedure is used to retrieve all the item types that exist in the database
*/		
print '' print '*** Creating sp_retrieve_itemtypes' 
GO

CREATE PROCEDURE [dbo].[sp_retrieve_itemtypes]
AS
	BEGIN
		SELECT [ItemTypeID]
		FROM [ItemType]
	END
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

this stored procedure is used to insert new products into the database.
*/
print'' print '*** Creating sp_insert_product'
GO

CREATE PROCEDURE [dbo].[sp_insert_product]
(
	@ItemTypeID		[nvarchar](15),
	@Description	[nvarchar](250),
	@OnHandQuantity		[int],
	@Name				[nvarchar](50),
	@ReOrderQuantity	[int],
	@DateActive			[date],
	@CustomerPurchasable	[bit],
	@RecipeID		[int]
)
AS
	BEGIN
		INSERT INTO [Product]
			([ItemTypeID],[Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [CustomerPurchasable], [RecipeID])
		VALUES
			(@ItemTypeID, @Description, @OnHandQuantity, @Name, @ReOrderQuantity, @DateActive, @CustomerPurchasable, @RecipeID)
		SELECT SCOPE_IDENTITY()
	END
GO
/*
Author: Kevin Broskow
Created Date: 2/5/19

This stored procedure is used to deactivate a product that is currently active in the database.
*/
print '' print '*** Creating sp_deactivate_product'
GO

CREATE PROCEDURE [dbo].[sp_deactivate_product]
(
	@ItemID		[int]
)
AS
BEGIN
	UPDATE [Product]
		SET [Active] = 0
		WHERE [ItemID] = @ItemID
		RETURN @@ROWCOUNT
		END
GO
print '' print '*** Creating sp_delete_product'
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

This stored procedure is used to delete a product that is currently deactivated in the database. 
Can also be thought of as purging the product.
*/
CREATE PROCEDURE [dbo].[sp_delete_product]
(
	@ItemID		[int]
)
AS
BEGIN
	DELETE FROM [Product]
		WHERE 	[ItemID] = @ItemID
		END
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

This stored procedure is used to update products that already exist in the database.
*/
print'' print '**** Creating sp_update_product'
GO

CREATE PROCEDURE [dbo].[sp_update_product]
(
	@ItemID		[int],
	@oldItemTypeID		[nvarchar](15),
	@oldDescription	[nvarchar](250),
	@oldOnHandQuantity		[int],
	@oldName				[nvarchar](50),
	@oldReOrderQuantity	[int],
	@oldDateActive			[date],
	@oldActive				[bit],
	@oldCustomerPurchasable 	[bit],
	@oldRecipeID		[int],
	
	@newItemTypeID		[nvarchar](15),
	@newDescription	[nvarchar](250),
	@newOnHandQuantity		[int],
	@newName				[nvarchar](50),
	@newReOrderQuantity	[int],
	@newDateActive			[date],
	@newActive				[bit],
	@newCustomerPurchasable 	[bit],
	@newRecipeID		[int]
	
)
AS
	BEGIN
		UPDATE [Product]
				SET [ItemTypeID] = @newItemTypeID,
					[Description] = @newDescription,
					[OnHandQuantity] = @newOnHandQuantity,
					[Name] = @newName,
					[ReOrderQuantity] = @newReOrderQuantity,
					[DateActive] = @newDateActive,
					[Active] = @newActive
				WHERE [ItemID] = @ItemID
					AND [ItemTypeID] = @oldItemTypeID
					AND [Description] = @oldDescription
					AND	[OnHandQuantity] = @oldOnHandQuantity
					AND	[Name]	= @oldName
					AND [ReOrderQuantity] = @oldReOrderQuantity
					AND	[DateActive] = @oldDateActive
					AND [Active] =	@oldActive
			RETURN @@ROWCOUNT
	END
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

This stored procedure is used to select an item by it's ItemID. Used to return a single item from 
the database
*/
print '' print '*** Creating sp_select_item'
GO

CREATE PROCEDURE [dbo].[sp_select_item]
(
	@ItemID 	[int]
)
AS
	BEGIN
		SELECT [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product]
		WHERE 	[ItemID] = @ItemID
	END
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

This stored procedure is used to return all of the items within the database.

*/
print'' print '*** Creating procedure sp_select_all_items'
GO
CREATE PROCEDURE [dbo].[sp_select_all_items]			
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product] 
	END
GO
/*
Author: Kevin Broskow
Created Date: 1/28/19

This procedure is used to retrieve all active items from the database.
*/
print'' print '*** Creating procedure sp_select_all_active_items'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_items]			
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product] 
		WHERE [Active] =1
	END
GO	
/*
Author: Kevin Broskow
Created Date: 1/28/19

This stored procedure is used to retrieve all deactivated items from the database.
*/
print'' print '*** Creating procedure sp_select_all_deactivated_items'
GO
CREATE PROCEDURE [dbo].[sp_select_all_deactivated_items]			
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product] 
		WHERE [Active] =0
	END
GO			
			
			/*
Author: Kevin Broskow
Created Date: 1/28/19

This is used to insert data into the item table for purpose of testing. 
The client has not supplied any items for the working database.
*/
print '' print '*** Inserting Product Records'
GO

INSERT INTO [dbo].[Product]
		([ItemTypeID], [Description],[OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID])
	VALUES
		('Food', 'Its a food item', 4, 'Its a large taco', 1, '2019-02-01', 1, 1, 1051),
		('Shoe', 'Its a shoe item', 4, 'Its a small light up shoe', 1, '2019-02-01', 0, 1, 1051),
		('Hat', 'Its a hat item', 4, 'Its a large sombrero', 1, '2019-02-01', 1, 1, 1051),
		('Food', 'Its a fodd item', 4, 'Its a large burrito', 1, '2019-02-01', 0,1, 1051),
		('Hat', '', 4, 'Abe Lincoln Hat', 1, '2019-02-01', 1, 1, 1051),
		('Food', 'Wonderful & delicious', 9, 'Hickory smoked salt', 15, '2019-02-01', 1, 1, 1051),
		('Food', 'Its a food item', 4, 'Hamburger', 1, '2019-02-11', 1, 0, 1051),
		('Food', 'I wonder if its a steak', 4, '8 oz. New York Strip', 1, '2019-02-01', 1, 0, 1051),
		('Food', 'I am hungry right now', 25, '6 oz. Ahi Tuna Steak', 10, '2019-02-01', 1, 1, 1051),
		('Food', 'Its a food item', 4, 'Its popcorn', 1, '2019-02-01', 1, 1, 1051)
		
GO

/*
Author: Matt LaMarche
Created Date: 1/23/19

This is where I have the Reservation code
*/

/*Created by Matt L on 01/23/19. Updated by X on Y*/
print '' print '*** Creating Reservation Table'
GO
CREATE TABLE [dbo].[Reservation] (
	[ReservationID]		[int] IDENTITY(100000, 1)	NOT NULL,
	[MemberID]			[int] 						NOT NULL,
	[NumberOfGuests]	[int]						NOT NULL,
	[NumberOfPets]		[int]						NOT NULL,
	[ArrivalDate]		[Date]						NOT NULL,
	[DepartureDate]		[Date]						NOT NULL,
	[Notes]				[nvarchar](250)						,
	[Active]			[bit]						NOT NULL DEFAULT 1
	CONSTRAINT [pk_ReservationID] PRIMARY KEY([ReservationID] ASC)

)
GO


/*Created by Matt L on 01/24/19. Updated by X on Y*/
print '' print '*** Creating Member Table'
GO
CREATE TABLE [dbo].[Member] (
	[MemberID]			[int] IDENTITY(100000, 1)			NOT NULL,
	[FirstName]			[nvarchar](50)						NOT NULL,
	[LastName]			[nvarchar](100)						NOT NULL,
	[PhoneNumber]		[nvarchar](11)						NOT NULL,
	[Email]				[nvarchar](250)						NOT NULL,
	[PasswordHash]		[nvarchar](100)						NOT NULL 	DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]			[bit]											DEFAULT 1
	CONSTRAINT [pk_MemberID] PRIMARY KEY([MemberID] ASC)

)
GO	


/*Created by Matt L on 01/23/19. Updated by X on Y*/
print '' print '*** Inserting Reservation Records'
GO

INSERT INTO [dbo].[Reservation]
		([MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes])
	VALUES
		(100000,1,0,'2008-11-11','2008-11-12','test')
GO

/*Created by Matt L on 01/24/19. Updated by X on Y*/
print '' print '*** Inserting Reservation Records'
GO

INSERT INTO [dbo].[Member]
		([FirstName],[LastName],[PhoneNumber],[Email])
	VALUES
		('Spongebob','Squarepants','1112223333','bobswag@kk.com'),
		('Patrick','Star','2223334444','starboi@kk.com')
GO


/*Created by Matt L on 01/23/19. Updated by X on Y*/
print '' print '*** Creating sp_create_reservation'
GO

CREATE PROCEDURE [dbo].[sp_create_reservation]
	(
		@MemberID 			[int],
		@NumberOfGuests		[int],
		@NumberOfPets		[int],
		@ArrivalDate 		[Date],
		@DepartureDate 		[Date],
		@Notes 				[nvarchar](250)
	)
AS
	BEGIN
		INSERT INTO [Reservation]
		([MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes])
		VALUES 
		(@MemberID, @NumberOfGuests, @NumberOfPets, @ArrivalDate, @DepartureDate, @Notes)
	END
GO

/*Created by Matt L on 01/24/19. Updated by X on Y*/
print '' print '*** Creating sp_retrieve_all_members'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_members]
AS
	BEGIN
		SELECT [MemberID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
		FROM Member
	END
GO

/*Created by Matt L on 01/26/19. Updated by X on Y*/
print '' print '*** Creating sp_update_reservation'
GO

CREATE PROCEDURE [dbo].[sp_update_reservation]
	(
		@ReservationID 				[int],
		@oldMemberID				[int],
		@oldNumberOfGuests 			[int],
		@oldNumberOfPets 			[int],
		@oldArrivalDate 			[Date],
		@oldDepartureDate 			[Date],
		@oldNotes 					[nvarchar](250),
		@oldActive 					[bit],
		@newMemberID				[int],
		@newNumberOfGuests 			[int],
		@newNumberOfPets 			[int],
		@newArrivalDate 			[Date],
		@newDepartureDate 			[Date],
		@newNotes 					[nvarchar](250),
		@newActive					[bit]
	)
AS
	BEGIN
		UPDATE [Reservation]
			SET [MemberID] = @newMemberID,
				[NumberOfGuests] = @newNumberOfGuests,
				[NumberOfPets] = @newNumberOfPets,
				[ArrivalDate] = @newArrivalDate,
				[DepartureDate] = @newDepartureDate,
				[Notes] = @newNotes,
				[Active] = @newActive
			WHERE 	
				[ReservationID] = @ReservationID
			AND [MemberID] = @oldMemberID
			AND	[NumberOfGuests] = @oldNumberOfGuests
			AND	[NumberOfPets] = @oldNumberOfPets
			AND	[ArrivalDate] = @oldArrivalDate
			AND	[DepartureDate] = @oldDepartureDate
			AND	[Notes] = @oldNotes
			AND	[Active] = @oldActive
		RETURN @@ROWCOUNT
	END
GO

/*Created by Matt L on 01/26/19. Updated by X on Y*/
print '' print '*** Creating sp_retrieve_all_reservations'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_reservations]
AS
	BEGIN
		SELECT [ReservationID],[MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes],[Active]
		FROM Reservation
	END
GO


/*Created by Matt L on 01/26/19. Updated by X on Y*/
print '' print '*** Creating sp_retrieve_all_view_model_reservations'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_view_model_reservations]
AS
	BEGIN
		SELECT [Reservation].[ReservationID],
		[Reservation].[MemberID],
		[Reservation].[NumberOfGuests],
		[Reservation].[NumberOfPets],
		[Reservation].[ArrivalDate],
		[Reservation].[DepartureDate],
		[Reservation].[Notes],
		[Reservation].[Active], 
		[Member].[FirstName], 
		[Member].[LastName], 
		[Member].[PhoneNumber], 
		[Member].[Email]
		FROM Reservation INNER JOIN Member ON Reservation.MemberID = Member.MemberID

	END
GO

/*Created by Matt L on 01/26/19. Updated by X on Y*/
print '' print '*** Creating sp_select_reservation'
GO

CREATE PROCEDURE [dbo].[sp_select_reservation]
(
	@ReservationID 				[int]
)
AS
	BEGIN
		SELECT [ReservationID],[MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes],[Active]
		FROM Reservation
		WHERE [ReservationID] = @ReservationID

	END
GO

/*Created by Matt L on 01/26/19. Updated by X on Y*/
print '' print '*** Creating sp_deactivate_reservation'
GO

CREATE PROCEDURE [dbo].[sp_deactivate_reservation]
	(
		@ReservationID 				[int]
	)
AS
	BEGIN
		UPDATE [Reservation]
			SET [Active] = 0
			WHERE 	
				[ReservationID] = @ReservationID
		RETURN @@ROWCOUNT
	END
GO


/*Created by Matt L on 01/26/19. Updated by X on Y*/
print '' print '*** Creating sp_delete_reservation'
GO

CREATE PROCEDURE [dbo].[sp_delete_reservation]
	(
		@ReservationID 				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [Reservation]
		WHERE  [ReservationID] = @ReservationID
		RETURN @@ROWCOUNT
	END
GO

/*Created by Matt L on 02/07/19. Updated by X on Y*/
print '' print '*** Creating sp_select_member_by_id'
GO

CREATE PROCEDURE [dbo].[sp_select_member_by_id]
(
	@MemberID 				[int]
)
AS
	BEGIN
		SELECT [MemberID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
		FROM Member
		WHERE [MemberID] = @MemberID
	END
GO

/* Wes' Script starts here */
print '' print '*** Creating Building Table'
GO
CREATE TABLE [dbo].[Building](
	[BuildingID]	[nvarchar](15)			NOT NULL,
	[Description]  	[nvarchar](250)			NOT NULL,
	[Active]		[bit]					NOT NULL DEFAULT 1
	CONSTRAINT [pk_BuildingID] PRIMARY KEY([BuildingID] ASC)

)
GO

print '' print '*** Inserting Data into Building Table'
GO
INSERT INTO [dbo].[Building]
		([BuildingID], [Description])
	VALUES
		('Building 1', 'Building 1'),
		('Building 2', 'Building 2'),
		('Building 3', 'Building 3'),
		('Building 4', 'Building 4')
GO

print '' print '*** Creating Room Type Table'
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeID]	[nvarchar](15)			NOT NULL,
	[Description]  	[nvarchar](250)			NOT NULL,
	[Active]		[bit]					NOT NULL DEFAULT 1
	CONSTRAINT [pk_RoomTypeID] PRIMARY KEY([RoomTypeID] ASC)

)
GO

print '' print '*** Inserting Data into RoomType Table'
GO
INSERT INTO [dbo].[RoomType]
		([RoomTypeID], [Description])
	VALUES
		('RoomType 1', 'RoomType 1'),
		('RoomType 2', 'RoomType 2'),
		('RoomType 3', 'RoomType 3'),
		('RoomType 4', 'RoomType 4')
GO

print '' print '*** Creating Room Table'
GO
CREATE TABLE [dbo].[Room](
	[RoomID]		[int] IDENTITY(100000, 1) 	NOT NULL,
	[RoomNumber]	[nvarchar](15)				NOT NULL,
	[BuildingID]	[nvarchar](15)				NOT NULL,
	[RoomTypeID]	[nvarchar](15)				NOT NULL,
	[Description]  	[nvarchar](250)				NULL,
	[Capacity]		[int]						NOT NULL DEFAULT 2,
	[Available]		[bit]						NOT NULL,
	[Active]		[bit]						NOT NULL DEFAULT 1
	CONSTRAINT [pk_RoomID] PRIMARY KEY([RoomID] ASC),
	CONSTRAINT [ak_RoomNumber_BuildingID] UNIQUE (RoomNumber, BuildingID)

)
GO

print '' print '*** Inserting Data into Room Table'
GO
INSERT INTO [dbo].[Room]
		([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [Available])
	VALUES
		('101', 'Building 1', 'RoomType 1', 'Room 101 in Building 1', 2, 1),
		('102', 'Building 1', 'RoomType 2', 'Room 102 in Building 1', 3, 1),
		('101', 'Building 2', 'RoomType 3', 'Room 101 in Building 2', 4, 1),
		('102', 'Building 2', 'RoomType 4', 'Room 102 in Building 2', 5, 1),
		('101', 'Building 3', 'RoomType 1', 'Room 101 in Building 3', 2, 1),
		('102', 'Building 3', 'RoomType 2', 'Room 102 in Building 3', 3, 1),
		('101', 'Building 4', 'RoomType 1', 'Room 101 in Building 4', 2, 1),
		('102', 'Building 4', 'RoomType 4', 'Room 102 in Building 4', 5, 1),
		('103', 'Building 4', 'RoomType 3', 'Room 103 in Building 4', 5, 1)
GO

print '' print '*** Adding Foreign Key for BuildingID in Room Table'

ALTER TABLE [dbo].[Room] WITH NOCHECK
	ADD CONSTRAINT [fk_Room_BuildingID] FOREIGN KEY ([BuildingID])
	REFERENCES [dbo].[Building]([BuildingID])
	ON UPDATE CASCADE
GO

print '' print '*** Adding Foreign Key for RoomTypeID in Room Table'

ALTER TABLE [dbo].[Room] WITH NOCHECK
	ADD CONSTRAINT [fk_Room_RoomTypeID] FOREIGN KEY ([RoomTypeID])
	REFERENCES [dbo].[RoomType]([RoomTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating sp_retrieve_buildings'
GO

CREATE PROCEDURE [dbo].[sp_select_buildings]
AS
	BEGIN
		SELECT [BuildingID], [Description]
		FROM Building
	END
GO

print '' print '*** Creating sp_select_room_types'
GO

CREATE PROCEDURE [dbo].[sp_select_room_types]
AS
	BEGIN
		SELECT [RoomTypeID], [Description]
		FROM RoomType
	END
GO

print '' print'*** Creating sp_insert_room'
GO
CREATE PROCEDURE [dbo].[sp_insert_room]
	(
	@RoomNumber			[nvarchar](15),
	@BuildingID			[nvarchar](15),
	@RoomTypeID			[nvarchar](15),
	@Description		[nvarchar](250),
	@Capacity			[int],
	@Available			[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [Available])
		VALUES
			(@RoomNumber, @BuildingID, @RoomTypeID, @Description, @Capacity, @Available)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print'*** Creating sp_insert_room'
GO
CREATE PROCEDURE [dbo].[sp_update_room]
	(
	@RoomID				[int],
	@RoomNumber			[nvarchar](15),
	@BuildingID			[nvarchar](15),
	@RoomTypeID			[nvarchar](15),
	@Description		[nvarchar](250),
	@Capacity			[int],
	@Available			[bit]
	)
AS
	BEGIN
		UPDATE [Room]
			SET 	[RoomNumber] = @RoomNumber, 
					[BuildingID] = @BuildingID, 
					[RoomTypeID] = @RoomTypeID, 
					[Description] = @Description, 
					[Capacity] = @Capacity, 
					[Available] = @Available
			WHERE	[RoomID] = @RoomID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Adding sp_select_room_by_ID'
GO
CREATE PROCEDURE [dbo].[sp_select_room_by_ID]
(
	@RoomID		[int]
)
AS	
	BEGIN
		SELECT 		
					[RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [Available]
		FROM 		[Room]
		WHERE		[RoomID] = @RoomID
	END
GO

print '' print '*** Adding sp_select_room_list'
GO
CREATE PROCEDURE [dbo].[sp_select_room_list]

AS	
	BEGIN
		SELECT 		
					[RoomID], [RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [Available], [Active]
		FROM 		[Room]
		ORDER BY 	[BuildingID], [RoomNumber]
	END
GO

print '' print '*** Creating ItemSupplier Table'
--Eric Bostwick 
--Created 2/4/19
--Updated 2/14/19 to Add Active
GO
CREATE TABLE [dbo].[ItemSupplier] (
	[ItemID]			[int]                         NOT NULL,
	[SupplierID]		[int]					  	  NOT NULL,
	[PrimarySupplier]	[bit]						  NULL,
	[LeadTimeDays]		[int]						  NULL DEFAULT 0,
	[UnitPrice]			[money]						  NULL DEFAULT 0.0,
	[Active]			[bit]						  NOT NULL DEFAULT 1
	
	CONSTRAINT [pk_ItemID_ItemID] PRIMARY KEY([ItemID] ASC, [SupplierID] ASC)
)

GO

print '' print '*** Adding Foreign Key for ItemSupplier'
--Eric Bostwick 
--Created 2/4/19
--Foreign Keys For ItemSupplier Join Table

ALTER TABLE [dbo].[ItemSupplier] WITH NOCHECK
	ADD CONSTRAINT [fk_ItemID] FOREIGN KEY ([ItemID])
	REFERENCES [dbo].[Item]([ItemID])
	ON UPDATE CASCADE
GO

print '' print '*** Adding Foreign Key SupplierID for ItemSupplier'

ALTER TABLE [dbo].[ItemSupplier] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplierID] FOREIGN KEY ([SupplierID])
	REFERENCES [dbo].[Supplier]([SupplierID])
	ON UPDATE CASCADE
GO

/*
 * Eric Bostwick
 * Created: 2/4/2019
 * Creates ItemSupplier Table
 */
print '' print '*** Creating sp_create_itemsupplier ***'
GO
CREATE PROCEDURE [dbo].[sp_create_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money]		
	)
AS
BEGIN
	BEGIN TRY
		/* We can only have one primary supplier for each itemid
		*  so if we are setting the primary supplier to this supplier
		*  we need to set the primary supplier to false for each itemsupplier record for
		*  this item before we set it to true for this one.
		*  This seems like a good place for a transaction.
		*/

		BEGIN TRANSACTION 
			    DECLARE @ItemSupplierCount int
				SET @ItemSupplierCount = (SELECT COUNT(*) FROM ItemSupplier WHERE ItemID = @ItemID )
				IF (@PrimarySupplier = 1  AND @ItemSupplierCount > 0)					
					BEGIN
						UPDATE [dbo].[ItemSupplier] 
						SET [PrimarySupplier] = 0 					
					END	

				IF (@ItemSupplierCount = 0)  --IF the record(s) was updated then insert the the itemsupplier OR the supplier count is 0 for this item
				BEGIN
					SET @PrimarySupplier = 1
				END
				BEGIN					
					INSERT INTO [dbo].[ItemSupplier]
					([ItemID], [SupplierID], [PrimarySupplier], [LeadTimeDays], [UnitPrice])
					VALUES
					(@ItemID, @SupplierID, @PrimarySupplier, @LeadTimeDays, @UnitPrice)

					COMMIT
				END		
	END TRY
	BEGIN CATCH		
			ROLLBACK  --If anything went wrong rollback the transaction
	END CATCH
	
END
GO

/*
 * Eric Bostwick
 * Created: 2/4/2019
 * Updates Item Supplier Table
 */
print '' print '*** Creating sp_update_itemsupplier ***'
GO
CREATE PROCEDURE [dbo].[sp_update_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@Active				[bit],

		@OldItemID 			[int],
		@OldSupplierID		[int],
		@OldPrimarySupplier	[bit],
		@OldLeadTimeDays	[int],
		@OldUnitPrice		[money],
		@OldActive			[bit]
	)
AS
BEGIN
		IF(@PrimarySupplier = 1)
			BEGIN
				UPDATE [dbo].[ItemSupplier]
				SET [PrimarySupplier]= 0
				WHERE [ItemID] = @ItemID 			
			END	
					
		UPDATE [dbo].[ItemSupplier]
		SET [ItemID] = @ItemID,
			[SupplierID] = @SupplierID,
			[PrimarySupplier] = @PrimarySupplier,
			[LeadTimeDays] = @LeadTimeDays, 
			[UnitPrice] = @UnitPrice,
			[Active] = @Active
		WHERE
			[ItemID] = @OldItemID AND
			[SupplierID] = @OldSupplierID AND
			[PrimarySupplier] = @OldPrimarySupplier AND
			[LeadTimeDays] = @OldLeadTimeDays AND
			[UnitPrice] = @OldUnitPrice AND
			[Active] = @OldActive	
END
					
GO

/*
 * Author: Eric Bostwick
 * Created: 2/4/2019
 * Selects ItemSuppliers by Itemid
 */
print '' print '*** Creating sp_select_itemsuppliers_by_itemid'
GO
CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_itemid] 
(
	@ItemID [int]
)

AS
	BEGIN
		SELECT 	    [isup].[ItemID], [isup].[SupplierID], [isup].[PrimarySupplier], [isup].[LeadTimeDays], 
					[isup].[UnitPrice],[isup].[Active] as [ItemSupplierActive],
					[s].[Name], [s].[ContactFirstName], [s].[ContactLastName], [s].[PhoneNumber], 
					[s].[Email], [s].[DateAdded], [s].[Address], [s].[City], [s].[State], 
					[s].[Country], [s].[PostalCode], [s].[Description],[s].[Active] AS SupplierActive,
					[i].[ItemTypeID], [i].[Description] AS [ItemDescripton], [i].[OnHandQty], [i].[Name], 
					[i].[DateActive], [i].[Active] AS [SupplierActive]
		FROM		[ItemSupplier] [isup] 
					JOIN [Item] [i] ON [i].[ItemID] = [isup].[ItemID] 
					JOIN [Supplier] [s] ON [s].[SupplierID] = [isup].[SupplierID]
		WHERE		[isup].[itemID] = @ItemID
	END
GO

/*
 * Author: Eric Bostwick
 * Created: 2/4/2019
 * Selects an Item Supplier by ItemID and SupplierID
 */
print '' print '*** Creating sp_select_itemsupplier_by_itemid_and_supplierid'
GO
CREATE PROCEDURE [dbo].[sp_select_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		SELECT 	    [isup].[ItemID], [isup].[SupplierID], [isup].PrimarySupplier, [isup].[LeadTimeDays], 
					[isup].[UnitPrice], [isup].[Active] as [ItemSupplierActive],
					[s].[Name], [s].[ContactFirstName], [s].[ContactLastName], [s].[PhoneNumber], 
					[s].[Email], [s].[DateAdded], [s].[Address], [s].[City], [s].[State], [s].[Country], 
					[s].[PostalCode], [s].[Description],[s].[Active] AS SupplierActive,
					[i].[ItemTypeID], [i].[Description] AS [ItemDescripton], [i].[OnHandQty], [i].[Name], 
					[i].[DateActive], [i].[Active] AS [ItemActive]
		FROM		[ItemSupplier] [isup] 
					JOIN [Item] [i] ON [i].[ItemID] = [isup].[ItemID] 
					JOIN [Supplier] [s] ON [s].[SupplierID] = [isup].[SupplierID]
		WHERE		[isup].[ItemID] = @ItemID AND [isup].[SupplierID] = @SupplierID
	END
GO

/*
 * Author: Eric Bostwick
 * Created: 2/7/2019
 * deletes an itemsupplier record based upon the itemid and supplierid
 */
print '' print '*** Creating sp_delete_itemsupplier_by_itemid_and_supplierid'
GO
CREATE PROCEDURE [dbo].[sp_delete_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		DELETE
		FROM		[ItemSupplier]					
		WHERE		[ItemID] = @ItemID AND [SupplierID] = @SupplierID
	END
GO
/*
 * Author: Eric Bostwick
 * Created: 2/7/2019
 * deletes an itemsupplier record based upon the itemid and supplierid
 */
print '' print '*** Creating sp_deactivate_itemsupplier_by_itemid_and_supplierid'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		
		UPDATE		[ItemSupplier]
		SET [Active] = 0			
		WHERE		[ItemID] = @ItemID AND [SupplierID] = @SupplierID
	END
GO

/*
 * Author: Eric Bostwick
 * Created: 2/7/2019
 * Description: Returns all the suppliers not setup in the itemsupplier table for that item
 * This is so user doesn't get the option to add a supplier that will create a primary key
 * violation on the item supplier table.
 */
print '' print '*** Creating sp_select_suppliers_for_itemsupplier_mgmt_by_itemid'
GO
CREATE PROCEDURE [dbo].[sp_select_suppliers_for_itemsupplier_mgmt_by_itemid]
(
	@ItemID [int]
)
AS
	BEGIN
		SELECT		[s].[SupplierID], 
					[s].[Name],
					[s].[Address],
					[s].[City],
					[s].[State],
					[s].[PostalCode],
					[s].[Country],
					[s].[PhoneNumber],
					[s].[Email],
					[s].[ContactFirstName],
					[s].[ContactLastName],
					[s].[DateAdded],
					[s].[Description],
					[s].[Active]

		FROM		[Supplier] [s] LEFT OUTER JOIN [ItemSupplier] [isup] ON [isup].[SupplierID] = [s].[SupplierID]					
		WHERE		[isup].[Itemid] != @ItemID OR [isup].[Itemid] is Null
	END
GO