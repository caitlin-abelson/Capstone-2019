USE [MillennialResort_DB]
GO
/*********************************************************************/
/* Developers place their test code here to be submitted to database */
/*********************************************************************/

/* Start Ramesh, submitted 2019-02-21 */

/* CREATE PROCEDURE [dbo].[sp_retrieve_members]
--Currently DQ'd for sp_retrieve_all_members
--Also, why do we retrieve the password?
AS
	BEGIN
		SELECT 	    [MemberID], [FirstName], [LastName], [PhoneNumber],	[Email], [Password]
		, [Active]
		FROM		[Member]
	END
--GO*/

/* Start Phil, submitted 2019-02-18 */

/* CREATE PROCEDURE sp_retrieve_all_events
--DQ'd for error 'SponsorName is not valid field'
AS
	BEGIN
		SELECT 	[EventID],[EventTitle],[Event].[EmployeeID],[Employee].[FirstName],[EventTypeID] AS [EventType],[Description],[EventStartDate],
				[EventEndDate],[KidsAllowed],[NumGuests],[Location],[Sponsored],[Event].[SponsorID],[Sponsor].[SponsorName], [Approved]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
				INNER JOIN [dbo].[Sponsor]
			ON		[Event].[SponsorID] = [Sponsor].[SponsorID]
	END
--GO*/

/* Start Caitlin Abelson*/

-- Created: 2019-01-27
ALTER TABLE [dbo].[Employee] WITH NOCHECK
	ADD CONSTRAINT [fk_DepartmentID] FOREIGN KEY ([DepartmentID])
	REFERENCES [dbo].[Department]([DepartmentID])
	ON UPDATE CASCADE
GO

-- Created: 2019-02-14
CREATE PROCEDURE [dbo].[sp_select_employee_inactive]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Active] = 0
	END
GO

-- Created: 2019-01-23
CREATE PROCEDURE [dbo].[sp_select_suppliers]
AS
	BEGIN
		SELECT 	    [Name], [ContactFirstName], [ContactLastName],	[PhoneNumber], [SupplierEmail], [DateAdded], [Address], [City], [StateCode], [Country], [ZipCode], [Description], [Active]
		FROM		[Suppliers]
	END
GO

-- Created: 2019-02-14
CREATE PROCEDURE [dbo].[sp_select_employee_active]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Active] = 1
	END
GO

-- Created: 2019-02-06
CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
	BEGIN
		SELECT  [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM 	[Employee]
	END
GO

-- Created: 2019-01-27
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

-- Created: 2019-01-27
CREATE PROCEDURE [dbo].[sp_insert_employee]
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

-- Created: 2019-02-01
CREATE PROCEDURE [dbo].[sp_update_employee_by_id]
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

-- Created: 2019-02-02
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

-- Created: 2019-02-12
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

-- Created: 2019-02-02
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

-- Created: 2019-01-29
CREATE PROCEDURE [dbo].[sp_select_department]

AS
	BEGIN
		SELECT 	    [DepartmentID]
		FROM		[Department]
	END
GO

/* Start Kevin */

-- Created: 2019-01-28
CREATE PROCEDURE [dbo].[sp_select_all_deactivated_items]			
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product] 
		WHERE [Active] =0
	END
GO	

-- Created: 2019-01-28
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

-- Created: 2019-01-28
CREATE PROCEDURE [dbo].[sp_select_all_items]			
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product] 
	END
GO

-- Created: 2019-01-28
CREATE PROCEDURE [dbo].[sp_select_all_active_items_extended]			
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product] 
		WHERE [Active] =1
	END
GO

/* Start Matt L. */

-- Created: 2019-01-26
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

-- Created: 2019-01-26
CREATE PROCEDURE [dbo].[sp_retrieve_all_reservations]
AS
	BEGIN
		SELECT [ReservationID],[MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes],[Active]
		FROM Reservation
	END
GO

-- Created: 2019-01-26
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

-- Created: 2019-01-26
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

-- Created: 2019-02-07
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

-- Created: 2019-01-26
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

-- Created: 2019-01-26
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
EXEC sys.sp_addextendedproperty 
	@name=N'Author', @value=N'Austin Berquam',
	@level0type=N'SCHEMA',@level0name=N'dbo',
	@level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roomTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roomTypes'
GO