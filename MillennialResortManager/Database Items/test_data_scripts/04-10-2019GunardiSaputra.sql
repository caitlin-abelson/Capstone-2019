
USE [MillennialResort_DB]
GO

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
	[DepartmentID]  [nvarchar](50)				  NULL,
	
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

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


print '' print '*** Inserting Employee Test Records'
GO

INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
		('Joanne', 'Smith', '1319551111', 'joanne@company.com'),
		('Martin', 'Jones', '1319551111', 'martin@company.com'),
		('Leo', 'Williams', '1319551111', 'leo@company.com')
GO

print '' print '*** Inserting Inactive Employee Test Records'
GO

INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [Active])
	VALUES
		('Joe', 'Shmoe', '1319551112', 'joe@company.com', 0)
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




/*Created by Gunardi Saputra on 04/11/19*/
print '' print '*** Creating sp_select_member_by_name'
GO

CREATE PROCEDURE [dbo].[sp_retrieve_member_by_name]
	(
		@FirstName			[nvarchar](50),
		@LastName			[nvarchar](100)
	)
AS
	BEGIN
		SELECT [MemberID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
		FROM Member
		WHERE	[FirstName] LIKE '%' + @FirstName + '%'
		  AND	[LastName] LIKE '%' + @LastName + '%'
		ORDER BY [MemberID], [Active]
	END

GO

/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_members]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_members]
AS
	BEGIN
		SELECT [MemberID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
		FROM Member
		WHERE [Active] = 1
	END