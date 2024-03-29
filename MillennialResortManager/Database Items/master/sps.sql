USE [MillennialResort_DB]
GO
/****** Object:  StoredProcedure [dbo].[sp_activate_guest_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_activate_guest_by_id]
	(
		@GuestID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Guest]
		SET 	[Active] = 1
		WHERE	[GuestID] = @GuestID
		AND [Active] = 0

		RETURN @@ROWCOUNT
	END

GO

/****** Object:  StoredProcedure [dbo].[sp_authenticate_user]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_count_supplier_order]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_count_supplier_order]
AS
	BEGIN
		SELECT COUNT([SupplierOrderID])
		FROM [dbo].[SupplierOrder]
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_create_event_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_create_event_type]
    (
        @EventTypeID        [nvarchar](15),
        @Description        [nvarchar](1000) 
    )
AS
    BEGIN
        INSERT INTO [dbo].[EventType]
            ([EventTypeID], [Description])
        VALUES
            (@EventTypeID, @Description)

        RETURN @@ROWCOUNT
    END
GO

-- Updated By : Kevin Broskow
-- Updated On: 2019-04-12
-- Added SupplierItemID
CREATE PROCEDURE [dbo].[sp_create_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@SupplierItemID		[int]
	)
AS
BEGIN
    UPDATE [dbo].[ItemSupplier]
    SET [PrimarySupplier] = 0
    WHERE [ItemID] = @ItemID
    
    INSERT INTO [dbo].[ItemSupplier]
    ([ItemID], [SupplierID], [PrimarySupplier], [LeadTimeDays], [UnitPrice], [SupplierItemID])
    VALUES
    (@ItemID, @SupplierID, @PrimarySupplier, @LeadTimeDays, @UnitPrice, @SupplierItemID)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_create_pet_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_create_pet_type]
    (
        @PetTypeID          [nvarchar](25),
        @Description        [nvarchar](1000)
    )
AS
    BEGIN
        INSERT INTO [dbo].[PetType]
            ([PetTypeID], [Description])
        VALUES
            (@PetTypeID, @Description)

        RETURN @@ROWCOUNT
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_create_reservation]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_create_reservation]
	(
		@MemberID 			[int],
		@NumberOfGuests		[int],
		@NumberOfPets		[int],
		@ArrivalDate 		[Date],
		@DepartureDate 		[Date],
		@Notes 				[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [Reservation]
		([MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes])
		VALUES
		(@MemberID, @NumberOfGuests, @NumberOfPets, @ArrivalDate, @DepartureDate, @Notes)
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_create_supplierOrder]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_create_supplierOrder]
	(
		@SupplierOrderID    [int],
		@EmployeeID    	    [int],
		@Description    	[nvarchar](50),
		@OrderComplete  	[bit],
		@DateOrdered 		[DateTime],
		@SupplierID			[int]

	)
AS
	BEGIN

		SET IDENTITY_INSERT [dbo].[SupplierOrder] ON

		INSERT INTO [dbo].[SupplierOrder]
			([SupplierOrderID], [EmployeeID],  [Description], [OrderComplete],
			 [DateOrdered], [SupplierID])
		VALUES
			(@SupplierOrderID, @EmployeeID, @Description, @OrderComplete,
			 @DateOrdered, @SupplierID)

		SET IDENTITY_INSERT [dbo].[SupplierOrder] OFF

		RETURN @@ROWCOUNT

	END

GO
/****** Object:  StoredProcedure [dbo].[sp_deactivate_employee]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_deactivate_guest_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_deactivate_guest_by_id]
	(
		@GuestID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Guest]
		SET 	[Active] = 0
		WHERE	[GuestID] = @GuestID

		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_deactivate_itemsupplier_by_itemid_and_supplierid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_deactivate_member]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_deactivate_member]
(
@MemberID[int]
)
AS
BEGIN
UPDATE[Member]
Set[Active] = 0
WHERE[MemberID] = @MemberID
AND [Active] = 1

RETURN @@ROWCOUNT
END


GO
/****** Object:  StoredProcedure [dbo].[sp_deactivate_recipe]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_deactivate_recipe]
(
	@RecipeID [int]
)
AS
	BEGIN
		UPDATE [Recipe]
		SET
			[Active] = 0
		WHERE

			[RecipeID] = @RecipeID
			RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_deactivate_reservation]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_deactivate_SupplierOrder]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_deactivate_SupplierOrder]
	(
		@SupplierOrderID		[int]
	)
AS
	BEGIN
		UPDATE  [SupplierOrder]
		SET 	[OrderComplete] = 0
		WHERE   [SupplierOrderID] = @SupplierOrderID

		RETURN @@ROWCOUNT
	END

GO
-- Eric Bostwick
-- Created: 3/7/2019
-- Deletes All SupplierOrderLines for a supplier order
CREATE Procedure [dbo].[sp_delete_supplier_order_lines]

	@SupplierOrderID [int]

As
    BEGIN
        DELETE FROM [SupplierOrderLine]
		WHERE [SupplierOrderID] = @SupplierOrderID
    END
GO

/****** Object:  StoredProcedure [dbo].[sp_delete_appointment_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_appointment_type]
	(
		@AppointmentTypeID	[nvarchar](15)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[AppointmentType]
		WHERE 	[AppointmentTypeID] = @AppointmentTypeID
	  
		RETURN @@ROWCOUNT
	END

GO
-- Craig Barkley
-- Created: 2019-02-17
CREATE PROCEDURE [dbo].[sp_delete_appointment_type_by_id]
    (
        @AppointmentTypeID    [nvarchar](15)
    )
AS
    BEGIN
        DELETE
        FROM     [AppointmentType]
        WHERE     [AppointmentTypeID] = @AppointmentTypeID

        RETURN @@ROWCOUNT
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_department]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_department]
	(
		@DepartmentID			[nvarchar](50)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[Department]
		WHERE 	[DepartmentID] = @DepartmentID
	  
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_employee]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_delete_employeeID_role]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_delete_event]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_event]
(
	@EventID		[int]
)
AS
	BEGIN
		DELETE
		FROM	[Event]
		WHERE	[EventID] = @EventID
		AND		[Approved] = 0

		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_delete_event_type_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_event_type_by_id]
    (
        @EventTypeID        [nvarchar](15)
    )
AS
    BEGIN
        DELETE
        FROM    [EventType]
        WHERE    [EventTypeID] = @EventTypeID

        RETURN @@ROWCOUNT
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_guest_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_guest_by_id]
	(
		@GuestID		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[Guest]
		WHERE	[GuestID] = @GuestID
		  AND	[Active] = 0

		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_guest_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_guest_type]
	(
		@GuestTypeID			[nvarchar](25)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[GuestType]
		WHERE 	[GuestTypeID] = @GuestTypeID
	  
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_item_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_item_type]
	(
		@ItemTypeID			[nvarchar](15)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[ItemType]
		WHERE 	[ItemTypeID] = @ItemTypeID
	  
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_itemsupplier_by_itemid_and_supplierid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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

		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_maintenance_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[sp_delete_maintenance_type]
	(
		@MaintenanceTypeID	[nvarchar](15)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[MaintenanceType]
		WHERE 	[MaintenanceTypeID] = @MaintenanceTypeID
	  
		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_delete_member]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_member]
(
@MemberID [int]
)
AS
BEGIN
DELETE
FROM [MEMBER]
WHERE [MemberID] = @MemberID
AND [Active] = 0

RETURN @@ROWCOUNT
END


GO
/****** Object:  StoredProcedure [dbo].[sp_delete_offering_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[sp_delete_offering_type]
	(
		@OfferingTypeID	[nvarchar](15)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[OfferingType]
		WHERE 	[OfferingTypeID] = @OfferingTypeID
	  
		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_delete_performance]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_performance]
	(
		@PerformanceID	[int]
	)
AS
	BEGIN
		DELETE
		FROM [Performance]
		WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_delete_pet]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_pet]
    (
        @PetID    [int]
    )
AS
    BEGIN
        DELETE
        FROM     [Pet]
        WHERE     [PetID] = @PetID

        RETURN @@ROWCOUNT
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_pet_type_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_pet_type_by_id]
    (
        @PetTypeID    [nvarchar](25)
    )
AS
    BEGIN
        DELETE
        FROM     [PetType]
        WHERE     [PetTypeID] = @PetTypeID

        RETURN @@ROWCOUNT
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_recipe]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_recipe]
(
	@RecipeID [int]
)
AS
	BEGIN
		DELETE FROM [Recipe]
		WHERE [RecipeID] = @RecipeID
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_recipe_item_lines]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_recipe_item_lines]
(
	@RecipeID [int]
)
AS
	BEGIN
		DELETE FROM [RecipeItemLine]
		WHERE [RecipeID] = @RecipeID
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_reservation]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_delete_roles]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_roles]
	(
		@RoleID				[nvarchar](50)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[Role]
		WHERE 	[RoleID] = @RoleID
	  
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_room_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_delete_room_type]
	(
		@RoomTypeID			[nvarchar](15)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[RoomType]
		WHERE 	[RoomTypeID] = @RoomTypeID
	  
		RETURN @@ROWCOUNT
	END


GO
-- Eric Bostwick
-- Created: 3/7/2019
-- Deletes A SupplierOrder
CREATE Procedure [dbo].[sp_delete_supplier_order]

	@SupplierOrderID [int]

As
    BEGIN
        DELETE FROM [SupplierOrder]
		WHERE [SupplierOrderID] = @SupplierOrderID
    END
GO

/*
 * Author: Wes Richardson
 * Created: 2019/04/12
 * This replaces the sp_insert_appointment
 */

-- Updated By: Wes Richardson
-- Updated On: 2019-04-26
CREATE PROCEDURE [dbo].[sp_insert_appointment_by_guest]
	(
	@AppointmentTypeID	[nvarchar](15),
	@GuestID			[int],
	@StartDate			[DateTime],
	@EndDate			[DateTime],
	@Description		[nvarchar](1000),
	@ServiceComponentID	[nvarchar](50)
	)
	AS
		BEGIN
		BEGIN TRANSACTION
			BEGIN Try
				Declare @AppointmentID[int]
				INSERT INTO [dbo].[Appointment]
					([AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description])
				VALUES
					(@AppointmentTypeID, @GuestID, @StartDate, @EndDate, @Description)
				SET @AppointmentID = SCOPE_IDENTITY();

				INSERT INTO [dbo].ScheduledItem
					([AppointmentID], [ServiceComponentID])
				VALUES
					(@AppointmentID, @ServiceComponentID)
				DECLARE @OfferingID[int]
				DECLARE @Price[money]
				DECLARE @MemberID[int]
				DECLARE @MemberTabID[int]

				SELECT @OfferingID = OfferingID
				FROM ServiceComponent
				WHERE ServiceComponentID = @ServiceComponentID

				SELECT @Price = Price
				FROM Offering
				WHERE OfferingID = @OfferingID

				SELECT @MemberID = MemberID
				FROM Guest
				WHERE GuestID = @GuestID

				SELECT @MemberTabID = MemberTabID
				FROM MemberTab
				WHERE MemberID = @MemberID
				AND 
				Active = 1

				INSERT INTO [dbo].[MemberTabLine]
					([MemberTabID], [OfferingID], [Quantity], [Price], [GuestID])
				VALUES
					(@MemberTabID, @OfferingID, 1, @Price, @GuestID)
				COMMIT
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
				SET @AppointmentID = -1;
			END CATCH

		SELECT @AppointmentID
	END
GO

/* Wes Richardson
 * Created 2019-04-18
 */

CREATE PROCEDURE [dbo].[sp_retrieve_reservation_by_guestID]
	(
		@GuestID			[int]
	)
	AS
		BEGIN
			DECLARE @MemberID[int]
			SELECT 	@MemberID = MemberID
			FROM 	Guest
			WHERE 	GuestID = @GuestID
			
			SELECT 		
					[ReservationID], [MemberID], [NumberOfGuests], [NumberOfPets], [ArrivalDate], [DepartureDate], [Notes]
			FROM 	[Reservation]
			WHERE	[MemberID] = @MemberID
			AND		[Active] = 1
		END
GO

/****** Object:  StoredProcedure [dbo].[sp_insert_appointment_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_insert_appointment_type]
	(
		@AppointmentTypeID	[nvarchar](15),
		@Description		[nvarchar](250)	
	)
AS
	BEGIN
		INSERT INTO [AppointmentType]
			([AppointmentTypeID], [Description])
		VALUES
			(@AppointmentTypeID, @Description)
			
		RETURN @@ROWCOUNT
		SELECT SCOPE_IDENTITY()
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_insert_building]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_insert_building]
	(
		@BuildingID			[nvarchar](50),
		@BuildingName		[nvarchar](150),
		@Address			[nvarchar](150),
		@Description		[nvarchar](1000),
		@BuildingStatusID	[nvarchar](25)
	)
AS
	BEGIN

		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Building')

		SELECT @@IDENTITY AS [@@IDENTITY]

		INSERT INTO [Building]
			([BuildingID],[BuildingName], [Address], [Description], [BuildingStatusID], [ResortPropertyID])
		VALUES
			(@BuildingID, @BuildingName, @Address, @Description, @BuildingStatusID, @@IDENTITY)

		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_department]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_department]
	(
		@DepartmentID		[nvarchar](50),
		@Description		[nvarchar](1000)	
	)
AS
	BEGIN
		INSERT INTO [Department]
			([DepartmentID], [Description])
		VALUES
			(@DepartmentID, @Description)
			
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_employee]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
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

		RETURN SCOPE_IDENTITY()
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_event]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Updated Phillip Hansen 2019-03-22
-- Update Author: Phillip Hansen
-- Date Updated: 2019-04-09
CREATE PROCEDURE [dbo].[sp_insert_event]
(
		@EventTitle 	[nvarchar](50),
		@EmployeeID		[int],
		@EventTypeID 	[nvarchar](15),
		@Description 	[nvarchar](1000),
		@EventStartDate [date],
		@EventEndDate 	[date],
		@KidsAllowed 	[bit],
		@SeatsRemaining [int],
		@NumGuests 		[int],
		@Location 		[nvarchar](50),
		@PublicEvent 	[bit],
		@Sponsored		[bit],
		@Approved 		[bit],
		@Price			[money]
)
AS
	BEGIN
		INSERT INTO [dbo].[Offering]
		([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
		('Event', @EmployeeID, @Description, @Price)
		DECLARE @NewOfferingID [int] = (SELECT @@IDENTITY)
	
		INSERT INTO [dbo].[Event]
			([OfferingID]
			,[EventTitle]
			,[EmployeeID]
			,[EventTypeID]
			,[Description]
			,[EventStartDate]
			,[EventEndDate]
			,[KidsAllowed]
			,[SeatsRemaining]
			,[NumGuests]
			,[Location]
			,[PublicEvent]
			,[Sponsored]
			,[Approved])
			VALUES
			(@NewOfferingID
			,@EventTitle
			,@EmployeeID
			,@EventTypeID
			,@Description
			,@EventStartDate
			,@EventEndDate
			,@KidsAllowed
			,@SeatsRemaining
			,@NumGuests
			,@Location
			,@PublicEvent
			,@Sponsored
			,@Approved
			)

			RETURN SCOPE_IDENTITY()
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_event_request]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_event_request]
	(
		@EventTitle					[nvarchar](50),
		@EmployeeID			 		[int],
		@EventTypeID				[nvarchar](15),
		@Description				[nvarchar](1000),
		@EventStartDate				[date],
		@EventEndDate				[date],
		@KidsAllowed				[bit],
		@NumGuests					[int],
		@Location					[nvarchar](50),
		@Sponsored					[bit],
		@Approved					[bit]

	)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
			([EventTitle],[EmployeeID],[EventTypeID],[Description],[EventStartDate],[EventEndDate],[KidsAllowed],[NumGuests], [Location], [Sponsored], [Approved])
			VALUES
			(@EventTitle, @EmployeeID, @EventTypeID, @Description, @EventStartDate, @EventEndDate, @KidsAllowed, @NumGuests, @Location, @Sponsored, @Approved )

			RETURN @@ROWCOUNT
	END


GO
CREATE PROCEDURE [dbo].[sp_insert_event_sponsor]
	(
		@EventID		[int],
		@SponsorID		[int]
	)
AS
	BEGIN	
		
		INSERT INTO 	[EventSponsor]
			([EventID], [SponsorID])
		VALUES
			(@EventID, @SponsorID)
		
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_event_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_event_type]
	(
		@EventTypeID	    [nvarchar](15),
		@Description		[nvarchar](1000)


	)
AS
	BEGIN
		INSERT INTO [dbo].[EventType]
			([EventTypeID], [Description])
		VALUES
			(@EventTypeID, @Description)

		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_insert_guest]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_insert_guest]
	(
		@MemberID				[int],
		@GuestTypeID			[nvarchar](25),
		@FirstName				[nvarchar](50),
		@LastName				[nvarchar](100)	,
		@PhoneNumber			[nvarchar](11),
		@Email					[nvarchar](250),
		@Minor					[bit],
		@ReceiveTexts			[bit],
		@EmergencyFirstName		[nvarchar](50),
		@EmergencyLastName		[nvarchar](100),
		@EmergencyPhoneNumber	[nvarchar](11),
		@EmergencyRelation		[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Guest]
			([MemberID], [GuestTypeID], [FirstName], 
			[LastName], [PhoneNumber], [Email], [Minor], 
			[ReceiveTexts], [EmergencyFirstName], [EmergencyLastName],
			[EmergencyPhoneNumber], [EmergencyRelation])
		VALUES
			(@MemberID, @GuestTypeID, @FirstName, @LastName,
			@PhoneNumber, @Email, @Minor, @ReceiveTexts, @EmergencyFirstName,
			@EmergencyLastName, @EmergencyPhoneNumber, @EmergencyRelation)
			
		RETURN @@ROWCOUNT
	END		

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_guest_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_guest_type]
	(
		@GuestTypeID		[nvarchar](25),
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [GuestType]
			([GuestTypeID], [Description])
		VALUES
			(@GuestTypeID, @Description)
			
		RETURN @@ROWCOUNT
	END

GO
/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
Create Procedure [dbo].[sp_insert_guest_vehicle]
(
    @GuestID           [int],
    @Make              [nvarchar](30),
    @Model             [nvarchar](30),
    @PlateNumber       [nvarchar](10),
    @Color             [nvarchar](30),
    @ParkingLocation   [nvarchar](50)
)
AS 
    BEGIN
        Insert into [GuestVehicle]
        ([GuestID], [Make], [Model], [PlateNumber], [Color], [ParkingLocation])
        Values (@GuestID, @Make, @Model, @PlateNumber, @Color, @ParkingLocation)
        Return @@Rowcount
    END
GO

-- Updated By: Jared Greenfield
-- Update Date: 2019/04/04
-- Added Offering support.
CREATE PROCEDURE	[dbo].[sp_insert_item]
(
	@ItemTypeID nvarchar(15),
	@Description nvarchar(1000),
	@OnHandQty int,
	@Name nvarchar(50),
	@ReorderQty int,
	@CustomerPurchasable [bit],
	@OfferingID [int],
	@RecipeID [int]
)
AS
BEGIN

	INSERT INTO [Item]
	(
	 [ItemTypeID],
	 [Description],
	 [RecipeID],
	 [OnHandQty],
	 [Name],
	 [ReorderQty],
	 [OfferingID],
	 [CustomerPurchasable])
VALUES
	(@ItemTypeID,
	 @Description,
	 @RecipeID,
	 @OnHandQty,
	 @Name,
	 @ReorderQty,
	 @OfferingID,
	 @CustomerPurchasable)


			  RETURN SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_item_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_item_type]
	(
		@ItemTypeID			[nvarchar](15),
		@Description		[nvarchar](1000)	
	)
AS
	BEGIN
		INSERT INTO [ItemType]
			([ItemTypeID], [Description])
		VALUES
			(@ItemTypeID, @Description)
			
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_maintenance_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_maintenance_type]
	(
		@MaintenanceTypeID	[nvarchar](15),
		@Description		[nvarchar](1000)	
	)
AS
	BEGIN
		INSERT INTO [MaintenanceType]
			([MaintenanceTypeID], [Description])
		VALUES
			(@MaintenanceTypeID, @Description)
			
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_member]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_member]
(
@FirstName[nvarchar](50),
@LastName[nvarchar](100),
@PhoneNumber[nvarchar](11),
@Email[nvarchar](250),
@Password[nvarchar](100)
)
AS
BEGIN
INSERT INTO [dbo].[Member]
([FirstName],[LastName],[PhoneNumber],[Email],[PasswordHash])
VALUES
(@FirstName,@LastName, @PhoneNumber, @Email,@Password)

RETURN @@ROWCOUNT
END


GO
/****** Object:  StoredProcedure [dbo].[sp_insert_offering]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_offering]
	(
		@OfferingTypeID [nvarchar](15),
		@EmployeeID 	[int],
		@Description 	[nvarchar](1000),
		@Price			[Money]
	)
AS
	BEGIN
		INSERT INTO [Offering]
		([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
		(@OfferingTypeID, @EmployeeID, @Description, @Price)
		SELECT SCOPE_IDENTITY();
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_offering_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_offering_type]
	(
		@OfferingTypeID		[nvarchar](15),
		@Description		[nvarchar](1000)	
	)
AS
	BEGIN
		INSERT INTO [OfferingType]
			([OfferingTypeID], [Description])
		VALUES
			(@OfferingTypeID, @Description)
			
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_performance]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_performance]
	(
		@PerformanceName	[nvarchar](100),
		@PerformanceDate	[date],
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Performance]
			([PerformanceTitle], [PerformanceDate], [Description])
		VALUES
			(@PerformanceName, @PerformanceDate, @Description)
	END
GO
-- Updated By: Matt Hill
-- Updated On: 2019-03-17
CREATE PROCEDURE sp_insert_pet
	(
		@PetName				    [nvarchar](50),
		@Gender      				[nvarchar](50),
		@Species     				[nvarchar](50),
		@PetTypeID				    [nvarchar](25),
		@GuestID				    [int]		
	)
AS
	BEGIN
		INSERT INTO [dbo].[Pet]
			([PetName],[Gender], [Species], [PetTypeID],[GuestID])
			VALUES
			(@PetName, @Gender, @Species, @PetTypeID, @GuestID)
			
			SELECT SCOPE_IDENTITY() 
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_product]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_product]
(
	@ItemTypeID			[nvarchar](15),
	@Description		[nvarchar](1000),
	@OnHandQuantity		[int],
	@Name				[nvarchar](50),
	@ReOrderQuantity	[int],
	@DateActive			[date]
)
AS
	BEGIN
		INSERT INTO [Item]
			([ItemTypeID],[Description], [OnHandQty], [Name], [ReOrderQty], [DateActive])
		VALUES
			(@ItemTypeID, @Description, @OnHandQuantity, @Name, @ReOrderQuantity, @DateActive)
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_recipe]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_recipe]
	(
		@Name 			[nvarchar](50),
		@Description 	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [Recipe]
		([Name], [Description])
		VALUES
		(@Name, @Description)
		SELECT SCOPE_IDENTITY();
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_recipe_item_line]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_recipe_item_line]
	(
		@RecipeID 		[int],
		@ItemID 		[int],
		@Quantity		[nvarchar](10),
		@UnitOfMeasure 	[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO [RecipeItemLine]
		([RecipeID], [ItemID], [Quantity], [UnitOfMeasure])
		VALUES
		(@RecipeID, @ItemID, @Quantity, @UnitOfMeasure)
		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_insert_roles]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_roles]
(
@RoleID[nvarchar](50),
@Description[nvarchar](1000)
)
AS
BEGIN
INSERT INTO [Role]
([RoleID], [Description])
VALUES
(@RoleID, @Description)

RETURN @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_room]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/* 	Created By:  Wes 
	Created: 
	Updated: Danielle Russo
			03/28/2019
			Added Price field
			Added ResortPropertyID 
			
	Updated: Danielle Russo
			04/11/2019
			Changed @RoomNumber datatype to int
*/ 
CREATE PROCEDURE [dbo].[sp_insert_room]
	(
	@RoomNumber			[int],
	@BuildingID			[nvarchar](50),
	@RoomTypeID			[nvarchar](15),
	@Description		[nvarchar](1000),
	@Capacity			[int],
	@RoomStatusID		[nvarchar](25),
	
	@OfferingTypeID		[nvarchar](15),
	@EmployeeID			[int],
	@Price				[Money]
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Room')
		DECLARE @NewResortProperyID [int] = (SELECT @@IDENTITY)

		INSERT INTO [Offering]	
		([OfferingTypeID],[EmployeeID],[Description],[Price])
		VALUES
		(@OfferingTypeID, @EmployeeID, @Description, @Price)	
		DECLARE @NewOfferingID [int] = (SELECT @@IDENTITY)
		
		
		INSERT INTO [dbo].[Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [RoomStatusID],[OfferingID],[ResortPropertyID])
		VALUES
			(@RoomNumber, @BuildingID, @RoomTypeID, @Description, @Capacity, @RoomStatusID, @NewOfferingID, @NewResortProperyID)	
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_room_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_room_type]
	(
		@RoomTypeID			[nvarchar](35),
		@Description		[nvarchar](1000)	
	)
AS
	BEGIN
		INSERT INTO [RoomType]
			([RoomTypeID], [Description])
		VALUES
			(@RoomTypeID, @Description)
			
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_shop]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_insert_shop]
(
@ShopID [int] OUTPUT,
@RoomID[int],
@Name[nvarchar](50),
@Description[nvarchar](100)
)
AS
BEGIN
INSERT INTO [dbo].[Shop]
([RoomID], [Name], [Description])
VALUES
(@RoomID, @Name, @Description)

SET @ShopID = SCOPE_IDENTITY()

RETURN SCOPE_IDENTITY()
END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_supplier]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_insert_supplier]
	(
		@Name 				nvarchar(50),
		@Address 			nvarchar(150),
		@City				nvarchar(50),
		@State				nchar(2),
		@PostalCode				nvarchar(12),
		@Country			nvarchar(25),
		@PhoneNumber		nvarchar(11),
		@Email				nvarchar(50),
		@ContactFirstName	nvarchar(50),
		@ContactLastName	nvarchar(100),
		@Description		nvarchar(1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Supplier]
		([Name], [Address], [City], [State], [PostalCode], [Country], [PhoneNumber], [Email], [ContactFirstName], [ContactLastName], [Description])
		VALUES
			(@Name, @Address, @City, @State, @PostalCode, @Country, @PhoneNumber, @Email, @ContactFirstName, @ContactLastName, @Description)
		RETURN SCOPE_IDENTITY();
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_insert_supplier_order]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier_order]
(
@SupplierOrderID[int] OUTPUT,
@SupplierID[int],
@EmployeeID[int],
@Description[nvarchar](1000)
)
AS
BEGIN
INSERT INTO [dbo].[SupplierOrder] ([SupplierID], [EmployeeID], [Description])
 VALUES (@SupplierID,@EmployeeID, @Description)

SET @SupplierOrderID = SCOPE_IDENTITY()
END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_supplier_order_line]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_supplier_order_line]
(
@SupplierOrderID[int],
@ItemID[int],
@Description[nvarchar](1000),
@OrderQty[int],
@UnitPrice[money]
)
AS
BEGIN
INSERT INTO [dbo].[SupplierOrderLine] ([SupplierOrderID], [ItemID], [Description], [OrderQty], [UnitPrice])
 VALUES (@SupplierOrderID, @ItemID, @Description, @OrderQty, @UnitPrice)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_insert_work_order]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_insert_work_order]
(
@MaintenanceWorkOrderID[int],
@MaintenanceTypeID[nvarchar](15),
@DateRequested[Date],
@DateCompleted [Date],
@RequestingEmployeeID[int],
@WorkingEmployeeID[int],
@Description[nvarchar](1000),
@Comments [nvarchar](1000),
@MaintenanceStatus[nvarchar](50),
@ResortPropertyID[int],
@Complete[bit]
)
AS
BEGIN
INSERT INTO [MaintenanceWorkOrder]
([MaintenanceWorkOrderID], [MaintenanceTypeID], [DateRequested], [DateCompleted], [RequestingEmployeeID], 
[WorkingEmployeeID], [Description], [Comments], [MaintenanceStatus], [ResortPropertyID], [Complete])
VALUES
(@MaintenanceWorkOrderID, @MaintenanceTypeID, @DateRequested, @DateCompleted, @RequestingEmployeeID, 
@WorkingEmployeeID, @Description, @Comments, @MaintenanceStatus, @ResortPropertyID, @Complete )
  
RETURN @@ROWCOUNT
END


GO
/****** Object:  StoredProcedure [dbo].[sp_reactivate_recipe]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_reactivate_recipe]
(
	@RecipeID [int]
)
AS
	BEGIN
		UPDATE [Recipe]
		SET
			[Active] = 1
		WHERE

			[RecipeID] = @RecipeID
			RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_retireve_all_event_requests]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retireve_all_event_requests]
AS
	BEGIN
		SELECT [EventTitle], [EventTypeID],[Description],[EventStartDate],
				[EventEndDate],[KidsAllowed],[NumGuests], [Location], [Sponsored], [Approved]
		FROM	[dbo].[Event]
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_event_type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_event_type]
AS
    BEGIN
        SELECT [EventTypeID], [Description]
        FROM   [EventType]
        ORDER BY [EventTypeID]
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_event_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_event_types]
AS
	BEGIN
		SELECT [EventTypeID]
		FROM	[dbo].[EventType]
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_events]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Updated Phillip Hansen 2019-03-22
CREATE PROCEDURE [dbo].[sp_retrieve_all_events]
AS
	BEGIN
		SELECT
		[EventID],
		[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[PublicEvent]
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
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


GO
CREATE PROCEDURE sp_retrieve_all_pets
AS
    BEGIN
        SELECT [PetID],[PetName], [Gender], [Species], [PetTypeID], [GuestID]
        FROM   [Pet]
        ORDER BY [PetID]
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_reservations]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_all_reservations]
AS
	BEGIN
		SELECT [ReservationID],[MemberID],[NumberOfGuests],[NumberOfPets],[ArrivalDate],[DepartureDate],[Notes],[Active]
		FROM Reservation
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_supplier_order]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_supplier_order]
AS
	BEGIN

		SELECT [SupplierOrderID],[EmployeeID],[SupplierID],[Description],[OrderComplete],
			   [DateOrdered]
		FROM [dbo].[SupplierOrder]
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_all_view_model_reservations]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_retrieve_appointment_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_appointment_types]
AS
	BEGIN
		SELECT [AppointmentTypeID], [Description]
		FROM AppointmentType
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_buildings]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_buildings]
AS
	BEGIN
		SELECT [BuildingID], [Description]
		FROM Building
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_departments]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_departments]
AS
	BEGIN
		SELECT [DepartmentID], [Description]
		FROM Department
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_departmentTypes]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_departmentTypes]
AS
	BEGIN
		SELECT [DepartmentID]
		FROM Department
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_employee_by_email]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_email]
(
	@Email 		[nvarchar](250)
)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Email], [PhoneNumber], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Email] = @Email
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_employee_roles]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_retrieve_event]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Updated Phillip Hansen 2019-03-22
-- Updated By: Phillip Hansen
-- Update Date: 2019-04-09
CREATE PROCEDURE [dbo].[sp_retrieve_event]
	(
		@EventID [int]
	)
AS
	BEGIN
		SELECT  [EventID],
				[Event].[OfferingID],
				[EventTitle],
				[Event].[EmployeeID],
				[Employee].[FirstName],
				[EventTypeID],
				[Event].[Description],
				[EventStartDate],
				[EventEndDate],
				[KidsAllowed],
				[NumGuests],
				[SeatsRemaining],
				[Location],
				[Sponsored],
				[Approved],
				[PublicEvent],
				[Price]
		FROM	[dbo].[Employee] INNER JOIN [dbo].[Event]
		ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
		INNER JOIN [dbo].[Offering]
		ON		[Offering].[OfferingID] = [Event].[OfferingID]
		WHERE	[EventID] = @EventID
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_guest_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_guest_types]
AS
	BEGIN
		SELECT [GuestTypeID], [Description]
		FROM GuestType
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_guests_by_email]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_guests_by_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active], [ReceiveTexts], [EmergencyFirstName], 
			   [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation]
		FROM   [Guest]
		WHERE 	[Email] = @Email
		AND		[Active] = 1
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_guests_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_guests_by_id]
	(
		@GuestID				[int]
	)
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active],[ReceiveTexts], [EmergencyFirstName], 
			   [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation], [CheckedIn]
		FROM   [Guest]
		WHERE 	[GuestID] = @GuestID
		AND		[Active] = 1
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_guests_by_name]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_guests_by_name]
	(
		@FirstName			[nvarchar](50),
		@LastName			[nvarchar](100)
	)
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active], [ReceiveTexts], [EmergencyFirstName], 
			   [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation]
		FROM   [Guest]
		WHERE	[FirstName] LIKE '%' + @FirstName + '%'
		  AND	[LastName] LIKE '%' + @LastName + '%'
		ORDER BY [GuestID], [Active]
	END

GO
/*
 * Author: Richard Carroll
 * Created: 2019/3/1
 */
Create Procedure [dbo].[sp_retrieve_guest_names_and_ids]
AS
    BEGIN
        Select [FirstName], [LastName], [GuestID]
        From Guest
        Where Active = 1
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_guestTypes]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_guestTypes]
AS
	BEGIN
		SELECT [GuestTypeID]
		FROM GuestType
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_item_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO






CREATE PROCEDURE [dbo].[sp_retrieve_item_types]
AS
	BEGIN
		SELECT [ItemTypeID], [Description]
		FROM ItemType
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_itemtypes]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_itemtypes]
AS
	BEGIN
		SELECT [ItemTypeID]
		FROM [ItemType]
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_List_of_EmployeeID]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_List_of_EmployeeID]
AS
	BEGIN
		SELECT [EmployeeID]
		FROM [dbo].[EmployeeRole]
		RETURN @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_maintenance_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_types]
AS
	BEGIN
		SELECT [MaintenanceTypeID], [Description]
		FROM MaintenanceType
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_member_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
 CREATE PROCEDURE [dbo].[sp_retrieve_member_by_id]
 (
@MemberID int
 )
 AS 
BEGIN
SELECT[MemberID], [FirstName], [LastName], [PhoneNumber], [Email],[Active]
FROM [MEMBER]
WHERE [MemberID] = @MemberID
END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_offering_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[sp_retrieve_offering_types]
AS
	BEGIN
		SELECT [OfferingTypeID], [Description]
		FROM OfferingType
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_roleID]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_roleID]
AS
	BEGIN
		SELECT [RoleID]
		FROM Role
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_roles]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_roles]
AS
	BEGIN
		SELECT [RoleID], [Description]
		FROM Role
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_roles_by_term_in_description]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_roles_by_term_in_description]
(
@SearchTerm[nvarchar](250)
)
AS
BEGIN
SELECT [RoleID],  [Description]
FROM [Role]
WHERE [Description] LIKE '%' + @SearchTerm + '%'
END

GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_room_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_room_types]
AS
	BEGIN
		SELECT [RoomTypeID], [Description]
		FROM RoomType
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_roomTypes]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_retrieve_roomTypes]
AS
	BEGIN
		SELECT [RoomTypeID]
		FROM RoomType
	END


GO
-- NAME:  Eduardo Colon
-- Date:   2019-03-05
CREATE PROCEDURE [dbo].[sp_retrieve_setuplist_by_id]
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
/****** Object:  StoredProcedure [dbo].[sp_retrieve_suppliers]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_retrieve_suppliers]

AS
	BEGIN
		SELECT 	    [SupplierID], [Name], [ContactFirstName], [ContactLastName],	[PhoneNumber], [Email], [DateAdded], [Address], [City], [State], [Country], [PostalCode], [Description], [Active]
		FROM		[Supplier]
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_retrieve_user_names_by_email]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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

CREATE PROCEDURE [dbo].[sp_search_performances]
	(
		@SearchTerm		[nvarchar](100)
	)
AS
	BEGIN
		SELECT [PerformanceID], [PerformanceTitle], [PerformanceDate], [Description]
		FROM [Performance]
		WHERE [PerformanceTitle] LIKE '%' + @SearchTerm + '%'
		OR [Description] LIKE '%' + @SearchTerm + '%'
	END

GO
CREATE PROCEDURE [dbo].[sp_select_all_active_items]
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name], [ReOrderQty], [DateActive], [Active], [CustomerPurchasable], [RecipeID], [OfferingID]
		FROM	[Item]
		WHERE [Active] =1
	END

GO

/****** Object:  StoredProcedure [dbo].[sp_select_all_active_items_extended]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_items_extended]
	AS
		BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQuantity], [Name], [ReOrderQuantity], [DateActive], [Active], [CustomerPurchasable], [RecipeID]
		FROM	[Product]
		WHERE [Active] =1
	END

GO
-- Updated By: Jared Greenfield
-- Updated On: 2019-04-03
-- Updated to Use Item instead if defunct Product
CREATE PROCEDURE [dbo].[sp_select_all_deactivated_items]
AS
	BEGIN
		SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name], [ReorderQty], [DateActive], [Active], [CustomerPurchasable], [RecipeID], [OfferingID]
		FROM [Item]
		WHERE [Active] = 0
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_select_all_employees]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
	BEGIN
		SELECT  [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM 	[Employee]
	END

GO

/*
 * Author: Richard Carroll
 * Created: 2019/3/8
 */
Create Procedure [dbo].[sp_select_all_guest_vehicles]
AS
    BEGIN
        Select [FirstName], [LastName], [Guest].[GuestID], [Make], [Model],
        [PlateNumber], [Color], [ParkingLocation]
        From GuestVehicle Inner Join Guest on 
        [Guest].[GuestID] = [GuestVehicle].[GuestID]
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_all_item_types]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_all_item_types]
AS
	BEGIN
		SELECT [ItemTypeID], [Description]
		FROM [ItemType]
	END


GO
-- Updated By: Jared Greenfield
-- Updated On: 2019-04-03
-- Updated to Use Item instead if defunct Product
CREATE PROCEDURE [dbo].[sp_select_all_items]
AS
	BEGIN
	SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name], [ReOrderQty], [DateActive], [Active], [OfferingID], [CustomerPurchasable], [RecipeID]
	FROM	[Item]
END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_all_itemtypes]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_all_itemtypes]

AS
	BEGIN
		SELECT 	[ItemTypeID]
		FROM	[ItemType]
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_select_all_performance]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_all_performance]
AS
	BEGIN
		SELECT [PerformanceID], [PerformanceTitle], [PerformanceDate], [Description]
		FROM [Performance]
	END
GO

/****** Object:  StoredProcedure [dbo].[sp_select_all_recipes]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_all_recipes]
AS
	BEGIN
		SELECT [RecipeID], [Name], [Description] , [DateAdded] , [Active]
		FROM [Recipe]
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_all_statusids]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_all_statusids]
AS
	BEGIN
		SELECT 		[BuildingStatusID]
		FROM		[BuildingStatus]
		ORDER BY	[BuildingStatusID]
	END

GO
-- Eric Bostwick
-- Created: 3/7/2019
-- Retrieves All SupplierOrders in the supplier order table
/*
 * Updated: 2019/04/26
 * By: Richard Carroll
 * Reason: Generated Orders need to be approved before they can be associated with the rest of the orders
*/
CREATE Procedure [dbo].[sp_select_all_supplier_orders]
As
    BEGIN
        SELECT [so].[SupplierOrderID], [so].[SupplierID], [s].[Name] AS SupplierName, [so].[EmployeeID], [e].[FirstName], [e].[LastName], [so].[Description],
        [so].[DateOrdered], [so].[OrderComplete]
        FROM [SupplierOrder] so INNER JOIN [Employee] e ON [so].[EmployeeID] = [e].[EmployeeID]
		INNER JOIN [Supplier] s ON [s].[SupplierID] = [so].[SupplierID]
		Where [so].[EmployeeID] != 1
    END
GO
-- Eric Bostwick
-- Created: 3/7/2019
-- Retrieves All SupplierOrderLines for a supplier order --
CREATE Procedure [dbo].[sp_select_all_supplier_order_lines]
	@SupplierOrderID [int]
As
    BEGIN
        SELECT [SupplierOrderID], [ItemID], [Description], [OrderQty], [UnitPrice], [QtyReceived]
        FROM [SupplierOrderLine]
		WHERE [SupplierOrderID] = @SupplierOrderID
    END
GO

--Created By: Wes Richardson
--Created On: 2019-03-01
--Select an appointment by appointment id

CREATE PROCEDURE [dbo].[sp_select_appointment_by_id]
	(
		@AppointmentID		[int]
	)
AS
	BEGIN
		SELECT	[AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description]
		FROM	[Appointment]
		WHERE 	[AppointmentID] = @AppointmentID
	END
GO

-- Created By: Wes Richardson
-- Created On: 2019-03-01
-- Select a list of guests for a appointment guest view model

CREATE PROCEDURE [dbo].[sp_select_appointment_guest_view_list]
AS
	BEGIN
		SELECT [GuestID], [FirstName], [LastName], [Email]
		FROM [Guest]
	END
GO

-- Created By: Wes Richardson
-- Created On: 2019-03-01
-- Select appointment type list

CREATE PROCEDURE [dbo].[sp_select_appointment_types]
AS
	BEGIN
		SELECT [AppointmentTypeID], [Description]
		FROM AppointmentType
	END
GO

/****** Object:  StoredProcedure [dbo].[sp_select_building_by_buildingstatusid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_building_by_buildingstatusid]
	(
		@BuildingStatusID		[nvarchar](25)
	)
AS
	BEGIN
		SELECT		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID]
		FROM		[Building]
		WHERE		[BuildingStatusID] = @BuildingStatusID
		ORDER BY	[BuildingID]
	END

GO

-- Updated By: James Heim
-- Updated 2019-04-19
-- Updated select list to include ResortPropertyID
CREATE PROCEDURE [dbo].[sp_select_building_by_id]
	(
		@BuildingID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT	[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID], 
				[ResortPropertyID]
		FROM	[Building]
		WHERE	[BuildingID] = @BuildingID
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_building_by_keyword_in_building_name]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_building_by_keyword_in_building_name]
	(
		@Keyword		[nvarchar](150)
	)
AS
	BEGIN
		SELECT		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID]
		FROM		[Building]
		WHERE		[BuildingName] LIKE '%' + @Keyword + '%'
		ORDER BY	[BuildingID]
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_buildings]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Updated By: Dani Russo
-- Updated On: 3/28/2019
-- Added StatusID Parameter
-- Added ResortPropertyID Parameter
CREATE PROCEDURE [dbo].[sp_select_buildings]
AS
	BEGIN
		SELECT 		[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID], [ResortPropertyID]
		FROM		[Building]
		ORDER BY	[BuildingID]
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_department]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_department]

AS
	BEGIN
		SELECT 	    [DepartmentID]
		FROM		[Department]
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_employee_active]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_employee_active]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Active] = 1
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_employee_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_select_employee_inactive]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_employee_inactive]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID], [Active]
		FROM [Employee]
		WHERE [Active] = 0
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_event_type_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_event_type_by_id]
AS
    BEGIN        
		SELECT 		[EventTypeID]
		FROM		[EventType]
		ORDER BY 	[EventTypeID]
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_item]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_select_item_by_itemid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_item_by_itemid]
	@ItemID int
AS
BEGIN

	SELECT [ItemID], [ItemTypeID], [Description], [OnHandQty], [Name],
			[ReOrderQty], [DateActive], [Active]
		   FROM [ITEM] WHERE [ItemID] = @ItemID

END


GO
/****** Object:  StoredProcedure [dbo].[sp_select_item_by_recipeid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_item_by_recipeid]
(
	@RecipeID [int]
)
AS
	BEGIN
		SELECT
		[ItemID],
		[ItemTypeID],
		[Description],
		[OnHandQty],
		[Name],
		[ReorderQty],
		[DateActive],
		[Active],
		[OfferingID],
		[CustomerPurchasable],
		[RecipeID]
		FROM [Item]
		WHERE [RecipeID] = @RecipeID
	END
GO
-- Updated By : Kevin Broskow
-- Updated On: 2019-04-12
-- Added SupplierItemID
CREATE PROCEDURE [dbo].[sp_select_itemsupplier_by_itemid_and_supplierid]
(
	@ItemID [int],
	@SupplierID [int]
)
AS
	BEGIN
		SELECT
		[ItemSupplier].[ItemID],
		[ItemSupplier].[SupplierID],
		[ItemSupplier].[PrimarySupplier],
		[ItemSupplier].[LeadTimeDays],
		[ItemSupplier].[UnitPrice],
		[ItemSupplier].[Active] as [ItemSupplierActive],
		[Supplier].[Name],
		[Supplier].[ContactFirstName],
		[Supplier].[ContactLastName],
		[Supplier].[PhoneNumber],
		[Supplier].[Email],
		[Supplier].[DateAdded],
		[Supplier].[Address],
		[Supplier].[City],
		[Supplier].[State],
		[Supplier].[Country],
		[Supplier].[PostalCode],
		[Supplier].[Description],
		[Supplier].[Active] AS SupplierActive,
		[Item].[ItemTypeID],
		[Item].[Description] AS [ItemDescripton],
		[Item].[OnHandQty],
		[Item].[Name],
		[Item].[DateActive],
		[Item].[Active] AS [ItemActive],
		[ItemSupplier].[SupplierItemID]
		FROM		[ItemSupplier]
					JOIN [Item] ON [Item].[ItemID] = [ItemSupplier].[ItemID]
					JOIN [Supplier] ON [Supplier].[SupplierID] = [ItemSupplier].[SupplierID]
		WHERE		[ItemSupplier].[ItemID] = @ItemID AND [ItemSupplier].[SupplierID] = @SupplierID
	END
GO

-- Updated By : Kevin Broskow
-- Updated On: 2019-04-12
-- Added SupplierItemID
CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_itemid]
(
	@ItemID [int]
)
AS
	BEGIN
		SELECT
		[ItemSupplier].[ItemID],
		[ItemSupplier].[SupplierID],
		[ItemSupplier].[PrimarySupplier],
		[ItemSupplier].[LeadTimeDays],
		[ItemSupplier].[UnitPrice],
		[ItemSupplier].[Active] as [ItemSupplierActive],
		[Supplier].[Name],
		[Supplier].[ContactFirstName],
		[Supplier].[ContactLastName],
		[Supplier].[PhoneNumber],
		[Supplier].[Email],
		[Supplier].[DateAdded],
		[Supplier].[Address],
		[Supplier].[City],
		[Supplier].[State],
		[Supplier].[Country],
		[Supplier].[PostalCode],
		[Supplier].[Description],
		[Supplier].[Active] AS [SupplierActive],
		[Item].[ItemTypeID],
		[Item].[Description] AS [ItemDescripton],
		[Item].[OnHandQty],
		[Item].[Name],
		[Item].[DateActive],
		[Item].[Active] AS [SupplierActive],
		[ItemSupplier].[SupplierItemID]
		FROM		[ItemSupplier]
					JOIN [Item] ON [Item].[ItemID] = [ItemSupplier].[ItemID]
					JOIN [Supplier] ON [Supplier].[SupplierID] = [ItemSupplier].[SupplierID]
		WHERE		[ItemSupplier].[itemID] = @ItemID
	END
GO
-- Updated By : Kevin Broskow
-- Updated On: 2019-04-12
-- Added SupplierItemID
CREATE PROCEDURE [dbo].[sp_select_itemsuppliers_by_supplierid]
(
@SupplierID[int]
)
AS
BEGIN
SELECT [isup].[ItemID], [isup].[SupplierID], [isup].[PrimarySupplier],
[isup].[LeadTimeDays], [isup].[UnitPrice],
[p].[ItemTypeID], [p].[Description], [p].[OnHandQty], [p].[Name], [p].[ReOrderQty],
[p].[DateActive], [p].[Active], [p].[CustomerPurchasable], [p].[RecipeID], [p].[OfferingID], [isup].[SupplierItemID]
FROM   [dbo].[ItemSupplier] [isup] JOIN Item [p] on [p].[ItemID] = [isup].[ItemID]
WHERE  [isup].[SupplierID] = @SupplierID AND [isup].[Active] = 1

END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_line_items_by_recipeid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_line_items_by_recipeid]
(
	@RecipeID [int]
)
AS
	BEGIN
		SELECT
		[Item].[ItemID],
		[Item].[ItemTypeID],
		[Item].[Description],
		[Item].[OnHandQty],
		[Item].[Name],
		[Item].[ReorderQty],
		[Item].[DateActive],
		[Item].[Active],
		[Item].[OfferingID],
		[Item].[CustomerPurchasable],
		[Item].[RecipeID]
		FROM [Item]
		INNER JOIN [RecipeItemLine] ON [RecipeItemLine].[ItemID] = [Item].[ItemID]
		WHERE [RecipeItemLine].[RecipeID] = @RecipeID
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_member_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_select_offering]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_offering]
(
	@OfferingID [int]
)
AS
	BEGIN
		SELECT [OfferingID], [OfferingTypeID], [EmployeeID], [Description], [Price], [Active]
		FROM [Offering]
		WHERE [OfferingID] = @OfferingID
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_performance_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_performance_by_id]
	(
		@PerformanceID	[int]
	)
AS
	BEGIN
		SELECT 	[PerformanceID], [PerformanceTitle], [PerformanceDate], [Description]
		FROM [Performance]
		WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_pet_type_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_pet_type_by_id]
AS
    BEGIN        
		SELECT 		[PetTypeID]
		FROM		[PetType]
		ORDER BY 	[PetTypeID]
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_recipe]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_recipe]
(
	@RecipeID [int]
)
AS
	BEGIN
		SELECT [RecipeID], [Name], [Description] , [DateAdded] , [Active]
		FROM [Recipe]
		WHERE [RecipeID] = @RecipeID
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_recipe_item_lines]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_recipe_item_lines]
(
	@RecipeID [int]
)
AS
	BEGIN
		SELECT [RecipeID], [ItemID], [Quantity], [UnitOfMeasure]
		FROM [RecipeItemLine]
		WHERE [RecipeID] = @RecipeID
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_reservation]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
-- Name: Eduardo Colon
-- Date: 2019-03-05
CREATE PROCEDURE [dbo].[sp_retrieve_all_setuplists]
AS
	BEGIN
		SELECT 	    [SetupListID], [SetupID], [Completed], [Description], [Comments]
		FROM		[SetupList]
	END
GO

/****** Object:  StoredProcedure [dbo].[sp_select_shop_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_shop_by_id]
(
@ShopID [int]
)
AS
BEGIN
SELECT [RoomID], [Name], [Description], [Active]
FROM [Shop]
WHERE [ShopID] = @ShopID
END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_shops]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_shops]
AS
BEGIN
SELECT [ShopID], [RoomID], [Name], [Description], [Active]
FROM [Shop]
ORDER BY [ShopID], [RoomID]
END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_suppliers]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_select_suppliers]
AS
	BEGIN
		SELECT 	    [Name], [ContactFirstName], [ContactLastName],	[PhoneNumber], [Email], [DateAdded], [Address], [City], [State], [Country], [PostalCode], [Description], [Active]
		FROM		[Supplier]
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_suppliers_for_itemsupplier_mgmt_by_itemid]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_suppliers_for_itemsupplier_mgmt_by_itemid]
(
	@ItemID [int]
)
AS
	BEGIN
		SELECT		[Supplier].[SupplierID],
					[Supplier].[Name],
					[Supplier].[Address],
					[Supplier].[City],
					[Supplier].[State],
					[Supplier].[PostalCode],
					[Supplier].[Country],
					[Supplier].[PhoneNumber],
					[Supplier].[Email],
					[Supplier].[ContactFirstName],
					[Supplier].[ContactLastName],
					[Supplier].[DateAdded],
					[Supplier].[Description],
					[Supplier].[Active]

		FROM		[Supplier] LEFT OUTER JOIN [ItemSupplier] ON [ItemSupplier].[SupplierID] = [Supplier].[SupplierID]
		WHERE		[ItemSupplier].[Itemid] != @ItemID OR [ItemSupplier].[Itemid] is Null
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_select_view_model_shops]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_select_view_model_shops]
AS
BEGIN
SELECT  [Shop].[ShopID],
[Shop].[RoomID],
[Shop].[Name],
[Shop].[Description],
[Shop].[Active],
[Room].[RoomNumber],
[Room].[BuildingID]
FROM [Shop] INNER JOIN [Room] ON [Shop].[RoomID] = [Room].[RoomID]

END

GO

-- Created By: Wes Richardson
-- Created On: 2019-03-01
-- Update an appointment
CREATE PROCEDURE [dbo].[sp_update_appointment]
	(
	@AppointmentID		[int],
	@AppointmentTypeID	[nvarchar](15),
	@GuestID			[int],
	@StartDate			[DateTime],
	@EndDate			[DateTime],
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE	[Appointment]
			SET	[AppointmentTypeID] = @AppointmentTypeID,
					[GuestID] = @GuestID,
					[StartDate] = @StartDate,
					[EndDate] = @EndDate,
					[Description] = @Description
			WHERE	[AppointmentID] = @AppointmentID
		RETURN @@ROWCOUNT
	END
GO

/****** Object:  StoredProcedure [dbo].[sp_update_building]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_update_building]
	(
		@BuildingID				[nvarchar](50),

		@NewBuildingName 		[nvarchar](150),
		@NewAddress				[nvarchar](150),
		@NewDescription			[nvarchar](1000),
		@NewBuildingStatusID	[nvarchar](25),

		@OldBuildingName		[nvarchar](150),
		@OldAddress				[nvarchar](150),
		@OldDescription			[nvarchar](250),
		@OldBuildingStatusID	[nvarchar](25)
	)
AS
	BEGIN
		UPDATE	[Building]
			SET	[BuildingName] 		= @NewBuildingName,
				[Address]			= @NewAddress,
				[Description]		= @NewDescription,
				[BuildingStatusID]	= @NewBuildingStatusID
		WHERE	[BuildingID] 		= @BuildingID
			AND [BuildingName] 		= @OldBuildingName
			AND [Address]			= @OldAddress
			AND	[Description]		= @OldDescription
			AND	[BuildingStatusID]	= @OldBuildingStatusID

		Return @@ROWCOUNT
	END

GO
/****** Object:  StoredProcedure [dbo].[sp_update_employee_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
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
/****** Object:  StoredProcedure [dbo].[sp_update_employee_email]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_update_event]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Updated Phillip Hansen 2019-03-22
-- Updated By: Phillip Hansen
-- Updated Date: 2019-04-09

CREATE PROCEDURE [dbo].[sp_update_event]
	(
		@EventID				[int],
		@EventTitle				[nvarchar](50),
		@EmployeeID			 	[int],
		@EventTypeID			[nvarchar](15),
		@Description			[nvarchar](1000),
		@EventStartDate			[date],
		@EventEndDate			[date],
		@KidsAllowed			[bit],
		@NumGuests				[int],
		@SeatsRemaining			[int],
		@Location				[nvarchar](50),
		@Sponsored				[bit],
		@Approved				[bit],
		@PublicEvent			[bit],

		@OldEventTitle			[nvarchar](50),
		@OldOfferingID			[int],
		@OldEmployeeID			[int],
		@OldEventTypeID			[nvarchar](15),
		@OldDescription			[nvarchar](1000),
		@OldEventStartDate		[date],
		@OldEventEndDate		[date],
		@OldKidsAllowed			[bit],
		@OldNumGuests			[int],
		@OldSeatsRemaining		[int],
		@OldLocation			[nvarchar](50),
		@OldSponsored			[bit],
		@OldApproved			[bit],
		@OldPublicEvent			[bit]

	)
AS
	BEGIN
		UPDATE [Event]
		SET		[EventTitle] = @EventTitle,
				[EmployeeID] = @EmployeeID,
				[EventTypeID] = @EventTypeID,
				[Description] = @Description,
				[EventStartDate] = @EventStartDate,
				[EventEndDate] = @EventEndDate,
				[KidsAllowed] = @KidsAllowed,
				[NumGuests] = @NumGuests,
				[Location] = @Location,
				[Sponsored] = @Sponsored,
				[Approved] = @Approved,
				[SeatsRemaining] = @SeatsRemaining,
				[PublicEvent] = @PublicEvent
		FROM 	[dbo].[Event]
		WHERE	[EventID] = @EventID
		AND		[OfferingID] = @OldOfferingID
		AND 	[EventTitle] = @OldEventTitle
		AND		[EmployeeID] = @OldEmployeeID
		AND		[EventTypeID] = @OldEventTypeID
		AND		[Description] = @OldDescription
		AND		[EventStartDate] = @OldEventStartDate
		AND		[EventEndDate] = @OldEventEndDate
		AND		[KidsAllowed] = @OldKidsAllowed
		AND		[SeatsRemaining] = @OldSeatsRemaining
		AND		[NumGuests] = @OldNumGuests
		AND		[Location] = @OldLocation
		AND 	[Sponsored] = @OldSponsored
		AND		[Approved] = @OldApproved
		AND		[PublicEvent] = @OldPublicEvent

			RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_guest_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_update_guest_by_id]
	(
		@GuestID					[int],
		
		@MemberID					[int],
		@GuestTypeID				[nvarchar](25),
		@FirstName					[nvarchar](50),
		@LastName					[nvarchar](100)	,
		@PhoneNumber				[nvarchar](11),
		@Email						[nvarchar](250),
		@Minor						[bit],
		@Active						[bit],
		@ReceiveTexts				[bit],
		@EmergencyFirstName			[nvarchar](50),
		@EmergencyLastName			[nvarchar](100),
		@EmergencyPhoneNumber		[nvarchar](11),
		@EmergencyRelation			[nvarchar](25),
		
		@OldMemberID				[int],
		@OldGuestTypeID				[nvarchar](25),
		@OldFirstName				[nvarchar](50),
		@OldLastName				[nvarchar](100)	,
		@OldPhoneNumber				[nvarchar](11),
		@OldEmail					[nvarchar](250),
		@OldMinor					[bit],
		@OldActive					[bit],
		@OldReceiveTexts			[bit],
		@OldEmergencyFirstName		[nvarchar](50),
		@OldEmergencyLastName		[nvarchar](100),
		@OldEmergencyPhoneNumber	[nvarchar](11),
		@OldEmergencyRelation		[nvarchar](25)		
	)
AS
	BEGIN
		UPDATE	[Guest]
		SET 	[MemberID] = @MemberID,
				[GuestTypeID] = @GuestTypeID,
				[FirstName] = @FirstName,
				[LastName] = @LastName,
				[PhoneNumber] = @PhoneNumber,
				[Email] = @Email,
				[Minor] = @Minor,
				[Active] = @Active,
				[ReceiveTexts] = @ReceiveTexts,
				[EmergencyFirstName] = @EmergencyFirstName,
				[EmergencyLastName] = @EmergencyLastName,
				[EmergencyPhoneNumber] = @EmergencyPhoneNumber,
				[EmergencyRelation] = @EmergencyRelation
		FROM	[dbo].[Guest]
		WHERE	[GuestID] = @GuestID
		  AND	[MemberID] = @OldMemberID
		  AND	[GuestTypeID] = @OldGuestTypeID
		  AND	[FirstName] = @OldFirstName
		  AND	[LastName] = @OldLastName
		  AND	[PhoneNumber] = @OldPhoneNumber
		  AND	[Email] = @OldEmail
		  AND	[Minor] = @OldMinor
		  AND	[Active] = @OldActive
		  AND	[ReceiveTexts] = @OldReceiveTexts
		  AND	[EmergencyFirstName] = @OldEmergencyFirstName
		  AND	[EmergencyLastName] = @OldEmergencyLastName
		  AND	[EmergencyPhoneNumber] = @OldEmergencyPhoneNumber
		  AND	[EmergencyRelation] = @OldEmergencyRelation
		  
		RETURN @@ROWCOUNT
	END

GO
/*
 * Author: Richard Carroll
 * Created: 2019/3/8
 */
Create Procedure [dbo].[sp_update_guest_vehicle]
(
    @OldGuestID           [int],
    @OldMake              [nvarchar](30),
    @OldModel             [nvarchar](30),
    @OldPlateNumber       [nvarchar](10),
    @OldColor             [nvarchar](30),
    @OldParkingLocation   [nvarchar](50),
    @GuestID              [int],
    @Make                 [nvarchar](30),
    @Model                [nvarchar](30),
    @PlateNumber          [nvarchar](10),
    @Color                [nvarchar](30),
    @ParkingLocation      [nvarchar](50)
)
AS 
    BEGIN
        Update GuestVehicle
        Set [GuestID] = @GuestID,
        [Make] = @Make,
        [Model] = @Model,
        [PlateNumber] = @PlateNumber,
        [Color] = @Color,
        [ParkingLocation] = @ParkingLocation
        Where [GuestID] = @OldGuestID AND
        [Make] = @OldMake AND
        [Model] = @OldModel AND
        [PlateNumber] = @OldPlateNumber AND
        [Color] = @OldColor AND
        [ParkingLocation] = @OldParkingLocation
        Return @@Rowcount
    END
GO

-- Updated By : Kevin Broskow
-- Updated On: 2019-04-12
-- Added SupplierItemID
CREATE PROCEDURE [dbo].[sp_update_itemsupplier]
	(
		@ItemID 			[int],
		@SupplierID			[int],
		@PrimarySupplier	[bit],
		@LeadTimeDays		[int],
		@UnitPrice			[money],
		@Active				[bit],
		@SupplierItemID		[int],

		@OldItemID 			[int],
		@OldSupplierID		[int],
		@OldPrimarySupplier	[bit],
		@OldLeadTimeDays	[int],
		@OldUnitPrice		[money],
		@OldActive			[bit],
		@OldSupplierItemID	[int]
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
			[Active] = @Active,
			[SupplierItemID] = @SupplierItemID
		WHERE
			[ItemID] = @OldItemID AND
			[SupplierID] = @OldSupplierID AND
			[PrimarySupplier] = @OldPrimarySupplier AND
			[LeadTimeDays] = @OldLeadTimeDays AND
			[UnitPrice] = @OldUnitPrice AND
			[Active] = @OldActive
END

GO

/****** Object:  StoredProcedure [dbo].[sp_update_offering]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_update_offering]
(
	@OfferingID [int],

	@OldOfferingTypeID [nvarchar](15),
	@OldEmployeeID [int],
	@OldDescription [nvarchar](1000),
	@OldPrice [Money],
	@OldActive [bit],

	@NewOfferingTypeID [nvarchar](15),
	@NewEmployeeID [int],
	@NewDescription [nvarchar](1000),
	@NewPrice [Money],
	@NewActive [bit]
)
AS
	BEGIN
		UPDATE [Offering]
		SET
			[OfferingTypeID] = @NewOfferingTypeID,
			[EmployeeID] = @NewEmployeeID,
			[Description] = @NewDescription,
			[Price]	= @NewPrice,
			[Active] = @NewActive
		WHERE
			[OfferingTypeID] = @OldOfferingTypeID AND
			[EmployeeID] = @OldEmployeeID AND
			[Description] = @OldDescription AND
			[Price]	= @OldPrice AND
			[Active] = @OldActive
			
		RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_password_hash]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_update_performance]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_update_performance]
	(
		@PerformanceID		[int],
		@PerformanceName	[nvarchar](100),
		@PerformanceDate	[date],
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [Performance]
			SET [PerformanceTitle] = @PerformanceName, [PerformanceDate] = @PerformanceDate, [Description] = @Description
			WHERE [PerformanceID] = @PerformanceID
		RETURN @@ROWCOUNT
	END
GO

CREATE PROCEDURE [dbo].[sp_update_pet]
	(
		@PetID			 		    [int],

		@oldPetName				    [nvarchar](50),
		@oldGender					[nvarchar](50),
		@oldSpecies      			[nvarchar](50),
		@oldPetTypeID				[nvarchar](25),
		@oldGuestID				    [int],

		@newPetName				    [nvarchar](50),
		@newGender					[nvarchar](50),
		@newSpecies      			[nvarchar](50),
		@newPetTypeID				[nvarchar](25),
		@newGuestID				    [int]
	)
AS
	BEGIN
		UPDATE [Pet]
			SET [PetName] = @newPetName,
				[Gender] = @newGender,
				[Species] = @newSpecies,
				[PetTypeID] = @newPetTypeID,
				[GuestID] = @newGuestID
			WHERE
				[PetID] = @PetID
			AND [PetName] = @oldPetName
			AND [Gender] = @oldGender
			AND [Species] = @oldSpecies
			AND	[PetTypeID] = @oldPetTypeID
			AND	[GuestID] = @oldGuestID
		RETURN @@ROWCOUNT
	END
GO

/****** Object:  StoredProcedure [dbo].[sp_update_Pet_Type]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_update_Pet_Type]
	(
		@PetTypeID	 [int],


		@Description	[nvarchar](1000),
		@Species   	    [nvarchar](50),


		@OldDescription		[nvarchar](1000),
		@OldSpecies   	    [nvarchar](50)

	)
	AS
	BEGIN

		UPDATE	[Pet_Type]
		SET 	[Description]	=	@Description,
				[Species]   	=	@Species
		FROM	[dbo].[Pet]
		WHERE	[PetTypeID] 	=   @PetTypeID
		  AND	[Description]	=	@OldDescription
		  AND	[Species]   	=	@OldSpecies


		RETURN @@ROWCOUNT
	END


GO
/****** Object:  StoredProcedure [dbo].[sp_update_recipe]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_update_recipe]
(
	@RecipeID [int],

	@OldName [nvarchar](50)	,
	@OldDescription [nvarchar](1000),
	@OldDateAdded [DateTime],
	@OldActive [bit],

	@NewName [nvarchar](50)	,
	@NewDescription [nvarchar](1000),
	@NewDateAdded [DateTime],
	@NewActive [bit]
)
AS
	BEGIN
		UPDATE [Recipe]
		SET
			[Name] = @NewName,
			[Description] = @NewDescription,
			[DateAdded] = @NewDateAdded,
			[Active] = @NewActive
		WHERE

			[RecipeID] = @RecipeID AND
			[Name] = @OldName AND
			[Description] = @OldDescription AND
			[DateAdded] = @OldDateAdded AND
			[Active] = @OldActive
			RETURN @@ROWCOUNT
	END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_reservation]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/****** Object:  StoredProcedure [dbo].[sp_update_role_by_id]    Script Date: 3/10/2019 6:38:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_update_role_by_id]
(
@RoleID  [nvarchar](50), 
@OldDescription   [nvarchar](1000),
@NewDescription  [nvarchar](1000)
)
AS
BEGIN

BEGIN
UPDATE [Role]
SET [Description] = @NewDescription

WHERE [RoleID] = @RoleID
AND  [Description] = @OldDescription

RETURN @@ROWCOUNT
END
END

GO
/*
 * Author: Kevin Broskow
 * Created 3-5-2019
 * 
 * Update a shop in the database
 */
CREATE PROCEDURE [dbo].[sp_update_shop]
(
	@ShopID 		[int],
	@oldRoomID		[int],
	@oldName		[nvarchar](50),
	@oldDescription		[nvarchar](1000),
	
	@newRoomID		[int],
	@newName		[nvarchar](50),
	@newDescription		[nvarchar](1000)
)
AS
BEGIN
	UPDATE [dbo].[Shop]
	SET [RoomID] = @newRoomID,
		[Name] = @newName,
		[Description] = @newDescription
	WHERE [ShopID] = @ShopID
	AND		[RoomID] = @oldRoomID
	AND 	[Name] = @oldName
	AND 	[Description] = @oldDescription
END
GO

--Eric Bostwick
--Added back cuz someone deleted it
--5/2/2019
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_update_supplier_order]
	(
		@SupplierOrderID    	[int],		
		@Description      		[nvarchar](50)		
	)
AS
	BEGIN
		UPDATE		[SupplierOrder]
			SET		[Description]      	=	@Description
			WHERE	[SupplierOrderID]   =   @SupplierOrderID
			  
			RETURN @@ROWCOUNT
    END

GO

CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles_by_employeeid]
(
	@EmployeeID [int]
)
AS
	SELECT [Role].[RoleID], [Role].[Description]
	FROM [Role]
	INNER JOIN [EmployeeRole] ON [Role].[RoleID] = [EmployeeRole].[RoleID]
	WHERE @EmployeeID = [EmployeeRole].[EmployeeID]
	GO
-- Created By: Phillip Hansen
-- Created On: 2019-04-09
	CREATE PROCEDURE [dbo].[sp_retrieve_all_event_sponsors]
AS
	BEGIN	
		SELECT 	[EventSponsor].[EventID], [Event].[EventTitle],
					[Sponsor].[Name], [EventSponsor].[SponsorID]
		FROM	[Event] INNER JOIN [EventSponsor]
				ON	[Event].[EventID] = [EventSponsor].[EventID]
		INNER JOIN [Sponsor] ON [EventSponsor].[SponsorID] = [Sponsor].[SponsorID]
		WHERE [Event].[EventID] = [EventSponsor].[EventID]
		AND 	[Sponsor].[SponsorID] = [EventSponsor].[SponsorID]
		AND		[Event].[Sponsored] = 1
	END
GO
-- Created By: Phillip Hansen
-- Created On: 2019-04-09
CREATE PROCEDURE [dbo].[sp_delete_event_sponsor_by_id]
	(
		@EventID		[int],
		@SponsorID		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[EventSponsor]
		WHERE 	[EventID] = @EventID
		AND		[SponsorID] = @SponsorID
	END
GO

-- Created By: Phillip Hansen
-- Created On: 2019-04-09
CREATE PROCEDURE [dbo].[sp_retrieve_all_events_uncancelled]
AS
	BEGIN
		SELECT
		[EventID],
		[Event].[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Event].[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[Cancelled],
		[PublicEvent],
		[Offering].[Price]
		
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
			INNER JOIN [dbo].[Offering] 
			ON [Offering].[OfferingID] = [Event].[OfferingID]
		WHERE [Cancelled] = 0
	END
GO

-- Created By: Phillip Hansen
-- Created On: 2019-04-09
CREATE PROCEDURE [dbo].[sp_retrieve_all_events_cancelled]
AS
	BEGIN
		SELECT
		[EventID],
		[Event].[OfferingID],
		[EventTitle],
		[Event].[EmployeeID],
		[Employee].[FirstName],
		[EventTypeID] AS [EventType],
		[Event].[Description],
		[EventStartDate],
		[EventEndDate],
		[KidsAllowed],
		[NumGuests],
		[SeatsRemaining],
		[Location],
		[Sponsored],
		[Approved],
		[Cancelled],
		[PublicEvent],
		[Offering].[Price]
		
		FROM	[dbo].[Event] INNER JOIN [dbo].[Employee]
			ON		[Employee].[EmployeeID] = [Event].[EmployeeID]
			INNER JOIN [dbo].[Offering] 
			ON [Offering].[OfferingID] = [Event].[OfferingID]
		WHERE [Cancelled] = 1
	END
GO

-- Created By: Phillip Hansen
-- Created On: 2019-04-09
CREATE PROCEDURE [dbo].[sp_update_event_to_uncancelled]
	(
		@EventID		[int]
	)
AS
	BEGIN
		UPDATE 	[Event]
		SET		[Cancelled] = 0
		WHERE	[EventID] = @EventID
		AND		[Cancelled] = 1
	END
GO
-- Created By: Phillip Hansen
-- Created On: 2019-04-09
CREATE PROCEDURE [dbo].[sp_update_event_to_cancelled]
	(
		@EventID		[int]
	)
AS
	BEGIN
		UPDATE 	[Event]
		SET		[Cancelled] = 1
		WHERE	[EventID] = @EventID
		AND		[Cancelled] = 0
	END
GO
-- Created By: Phillip Hansen
-- Created On: 2019-04-09
CREATE PROCEDURE [dbo].[sp_select_all_sponsors]
AS
	BEGIN
		SELECT 	[SponsorID],[Name],[Address],[City],[State],[PhoneNumber],
					[Email],[ContactFirstName],[ContactLastName],[DateAdded],[Active]
		FROM	[dbo].[Sponsor]
	END
GO

-- Created By: Alisa Roehr
-- Created On: 2019-03-29
CREATE PROCEDURE sp_insert_employee_role
	(
		@EmployeeID 	[int],
		@RoleID			[nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeRole]
			([EmployeeID], [RoleID])
		VALUES
			(@EmployeeID, @RoleID)
			
		RETURN @@ROWCOUNT
	END		
GO

-- Created By: Alisa Roehr
-- Created On: 2019-03-29
CREATE PROCEDURE sp_delete_employee_role
	(
		@EmployeeID 	[int],
		@RoleID			[nvarchar](50)
	)
AS
	BEGIN
		DELETE 	
		FROM	[EmployeeRole]
		WHERE	[EmployeeID] = @EmployeeID
		  AND	[RoleID] = @RoleID
		  
		RETURN @@ROWCOUNT
	END
GO



-- Created By: Wes Richardson
-- Created On: 2019-03-28
-- Select Appointments by GuestID
CREATE PROCEDURE [dbo].[sp_select_appointment_by_guest_id]
	(
		@GuestID		[int]
	)
AS
	BEGIN
		SELECT	[AppointmentID], [AppointmentTypeID], [StartDate], [EndDate], [Description]
		FROM	[Appointment]
		WHERE 	[GuestID] = @GuestID
	END
GO
-- Created By: Wes Richardson
-- Created On: 2019-03-28
-- Delete Appointment
CREATE PROCEDURE [dbo].[sp_delete_appointment_by_id]
	(
		@AppointmentID		[int]
	)

AS
	BEGIN
		DELETE
		FROM [Appointment]
		WHERE [AppointmentID] = @AppointmentID
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_insert_receiving]
(
	@SupplierOrderID		[int],
	@Description			[nvarchar](1000),
	@DateDelivered			[DateTime]
)
AS
	BEGIN
		INSERT INTO [dbo].[Receiving]([SupplierOrderID], [Description], [DateDelivered]) 
		VALUES(@SupplierOrderID, @Description, @DateDelivered)
	END
GO
-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_select_all_receiving]
AS
	BEGIN
		SELECT [ReceivingID],[SupplierOrderID],[Description],[DateDelivered],[Active]
		FROM [Receiving]
	END
GO

-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_select_receiving]
(
			@ReceivingID 	[int]
		)
AS
	BEGIN
		SELECT [ReceivingID],[SupplierOrderID],[Description],[DateDelivered],[Active]
		FROM [Receiving]
		WHERE [ReceivingID] = @ReceivingID
	END
GO

-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_update_receiving]
(
	@ReceivingID 		[int],
	@Description		[nvarchar](1000)
)
AS
	BEGIN
	UPDATE [Receiving]
	SET [Description] = @Description
	WHERE [ReceivingID] = @ReceivingID
END
GO

-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_deactivate_receiving]
(
	@ReceivingID		[int]
)
AS
	BEGIN
	UPDATE [Receiving]
	SET	[Active] = 0
	WHERE [ReceivingID] = @ReceivingID
END
GO
-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_select_supplier_order_by_id]
(
		@SupplierOrderID		[int]
)
AS
	BEGIN
	SELECT [EmployeeID], [Description], [OrderComplete], [DateOrdered], [SupplierID]
	FROM [SupplierOrder]
	WHERE [SupplierOrderID] = @SupplierOrderID
END 
GO

-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_complete_order_by_id]
(
		@SupplierOrderID		[int]
)
AS
	BEGIN
	UPDATE [SupplierOrder]
	SET [OrderComplete] = 1
	WHERE [SupplierOrderID] = @SupplierOrderID
END 
GO

-- Created By: Kevin Broskow
-- Created On: 2019-03-29
CREATE PROCEDURE [dbo].[sp_update_supplier_order_line]
(
@SupplierOrderID	[int],
@ItemID		[int],
@QtyReceived		[int]
)
AS
BEGIN
UPDATE [SupplierOrderLine]
SET [QtyReceived] = @QtyReceived
WHERE [SupplierOrderID] = @SupplierOrderID
AND		[ItemID] = @ItemID
END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_create_vehicle]
	(
		@Make				   [nvarchar](30),			
		@Model				   [nvarchar](30),
		@Year	               [int],					
		@License			   [nvarchar](10),			
		@Mileage			   [int],							
		@Capacity			   [int],					
		@Color				   [nvarchar](30),			
		@PurchaseDate		   [Date],					
		@Description		   [nvarchar](1000),		
		@Active				   [bit],					
		@DeactivationDate	   [Date],
		@Available			   [bit],
		@ResortVehicleStatusId [nvarchar] (25),
		@ResortPropertyId	   [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortVehicle] 
			(
				 [Make] 
				, [Model]
				, [Year]
				, [License]
				, [Mileage]
				, [Capacity]
				, [Color]
				, [PurchaseDate]
				, [Description]
				, [Active]
				, [DeactivationDate]
				, [Available]
				, [ResortVehicleStatusId]
				, [ResortPropertyId]
			) 
		VALUES
			(
				  @Make
				, @Model
				, @Year
				, @License
				, @Mileage
				, @Capacity
				, @Color
				, @PurchaseDate
				, @Description
				, @Active
				, @DeactivationDate
				, @Available
				, @ResortVehicleStatusId
				, @ResortPropertyId
			)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_vehicles]
AS
	BEGIN
		SELECT   [VehicleID]
		       , [Make]
			   , [Model]
			   , [Year]
			   , [License]
			   , [Mileage]
			   , [Capacity]
			   , [Color]
			   , [PurchaseDate]
			   , [Description]
			   , [Active]
			   , [DeactivationDate]
			   , [Available]
			   , [ResortVehicleStatusId]
			   , [ResortPropertyId]
		FROM [ResortVehicle]
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_vehicle_by_id]
	(
		@VehicleId [int]	
	)
AS
	BEGIN
		SELECT [VehicleID]
				, [Make]
				, [Model]
				, [Year]
				, [License]
				, [Mileage]
				, [Capacity]
				, [Color]
				, [PurchaseDate]
				, [Description]
				, [Active]
				, [DeactivationDate]
				, [Available]
				, [ResortVehicleStatusId]
				, [ResortPropertyId]
		FROM [ResortVehicle]
		WHERE [VehicleID] = @VehicleId
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_update_vehicle]
	(
		@VehicleId				  [int],
		@OldMake				  [nvarchar](30),			
		@OldModel				  [nvarchar](30),			
		@OldYear	              [int],					
		@OldLicense				  [nvarchar](10),			
		@OldMileage				  [int],		
		@OldCapacity			  [int],					
		@OldColor				  [nvarchar](30),			
		@OldPurchaseDate		  [Date],					
		@OldDescription			  [nvarchar](1000),		
		@OldActive				  [bit],					
		@DeactivationDate		  [Date],
		@OldAvailable			  [bit],
		@OldResortVehicleStatusId [nvarchar](25),
		@OldResortPropertyId	  [int],
		
		@NewMake				  [nvarchar](30),			
		@NewModel				  [nvarchar](30),			
		@NewYear	              [int],					
		@NewLicense				  [nvarchar](10),			
		@NewMileage				  [int],							
		@NewCapacity			  [int],					
		@NewColor				  [nvarchar](30),			
		@NewPurchaseDate		  [Date],					
		@NewDescription			  [nvarchar](1000),		
		@NewActive				  [bit],
		@NewAvailable			  [bit],
		@NewResortVehicleStatusId [nvarchar](25),
		@NewResortPropertyId	  [int]
	)
AS
	BEGIN
		UPDATE [ResortVehicle]
			SET [Make]				    = @NewMake,
				[Model]				    = @NewModel,					
				[Year]	                = @NewYear,				
				[License]			    = @NewLicense,					
				[Mileage]			    = @NewMileage,									
				[Capacity]			    = @NewCapacity,
				[Color]				    = @NewColor,				
				[PurchaseDate]		    = @NewPurchaseDate,
				[Description]		    = @NewDescription,				
				[Active]			    = @NewActive,			
				[DeactivationDate]      = @DeactivationDate,
				[Available]             = @NewAvailable,
				[ResortVehicleStatusId] = @NewResortVehicleStatusId,
				[ResortPropertyId]		= @NewResortPropertyId
			WHERE 	    [VehicleId]             = @VehicleId
					AND	[Make]				    = @OldMake
					AND [Model]				    = @OldModel
					AND [Year]	                = @OldYear
					AND [License]			    = @OldLicense
					AND [Mileage]			    = @OldMileage
					AND [Capacity]			    = @OldCapacity
					AND [Color]				    = @OldColor
					AND [PurchaseDate]		    = @OldPurchaseDate
					AND [Description]		    = @OldDescription
					AND [Active]			    = @OldActive
					AND [Available]			    = @OldAvailable
					AND [ResortVehicleStatusId] = @OldResortVehicleStatusId
					AND [ResortPropertyId]	 	= @OldResortPropertyId
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_delete_vehicle_by_id]
	(
		@VehicleId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[ResortVehicle]
		WHERE	[VehicleId] = @VehicleId
		  AND	[Active] = 0

		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_deactivate_vehicle_by_id]
	(
		@VehicleId		[int]
	)
AS
	BEGIN
		UPDATE 	[ResortVehicle]
		SET 	[Active] = 0
		WHERE	[VehicleId] = @VehicleId

		RETURN @@ROWCOUNT
	END
GO




-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_create_resort_vehicle_status]
	(
		@ResortVehicleStatusId	[nvarchar](20),
		@Description			[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortVehicleStatus] ([ResortVehicleStatusId],[Description]) 
		VALUES(@ResortVehicleStatusId, @Description)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_resort_vehicle_statuses]
AS
	BEGIN
		SELECT [ResortVehicleStatusId],[Description]
		FROM [ResortVehicleStatus]
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_resort_vehicle_status_by_id]
	(
		@ResortVehicleStatusId [int]	
	)
AS
	BEGIN
		SELECT [ResortVehicleStatusId], [Description]
		FROM [ResortVehicleStatus]
		WHERE [ResortVehicleStatusId] = @ResortVehicleStatusId
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_update_resort_vehicle_status]
	(
		@ResortVehicleStatusId	[nvarchar](25),
		
		@OldDescription	[nvarchar](1000),
		
		@NewDescription	[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE [ResortVehicleStatus]
			SET [Description]			 = @NewDescription
			WHERE       [ResortVehicleStatusId] = @ResortVehicleStatusId
					AND [Description] = @OldDescription
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_delete_resort_vehicle_status_by_id]
	(
		@ResortVehicleStatusId		[nvarchar](25)
	)
AS
	BEGIN
		DELETE
		FROM	[ResortVehicleStatus]
		WHERE	[ResortVehicleStatusId] = @ResortVehicleStatusId

		RETURN @@ROWCOUNT
	END
GO




-- AUTHOR  : Francis Mingomba
-- CREATED : 2019/04/15
CREATE PROCEDURE [dbo].[sp_create_vehicle_checkout]
	(
		@EmployeeId			[int],
		@DateCheckedOut		[Date],
		@DateReturned		[Date],
		@DateExpectedBack	[Date],
		@Returned			[bit],
		@ResortVehicleId	[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[VehicleCheckout] 
			(   [EmployeeId]
				, [DateCheckedOut]
				, [DateReturned]
				, [DateExpectedBack]
				, [Returned]
				, [ResortVehicleId]
			) 
		VALUES
			(
				  @EmployeeId			
				, @DateCheckedOut		
				, @DateReturned		
				, @DateExpectedBack 
				, @Returned			
				, @ResortVehicleId	
			)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_vehicle_checkouts]
AS
	BEGIN
		SELECT [VehicleCheckoutId]
		       , [EmployeeId]
			   , [DateCheckedOut]
			   , [DateReturned]
			   , [DateExpectedBack]
			   , [Returned]
			   , [ResortVehicleId]
		FROM [VehicleCheckout]
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_vehicle_checkout_by_id]
	(
		@VehicleCheckoutId [int]	
	)
AS
	BEGIN
		SELECT   [VehicleCheckoutId]
		       , [EmployeeId]
			   , [DateCheckedOut]
			   , [DateReturned]
			   , [DateExpectedBack]
			   , [Returned]
			   , [ResortVehicleId]
		FROM [VehicleCheckout]
		WHERE [VehicleCheckoutId] = @VehicleCheckoutId
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_update_vehicle_checkout]
	(
		@VehicleCheckoutId	[int],
		
		@OldEmployeeId			[int],
		@OldDateCheckedOut		[Date],
		@OldDateReturned		[Date],
		@OldDateExpectedBack	[Date],
		@OldReturned			[bit],
		@OldResortVehicleId		[int],
		
		@NewEmployeeId			[int],
		@NewDateCheckedOut		[Date],
		@NewDateReturned		[Date],
		@NewDateExpectedBack	[Date],
		@NewReturned			[bit],
		@NewResortVehicleId		[int]
	)
AS
	BEGIN
		UPDATE [VehicleCheckout]
			SET [EmployeeId]		 = @NewEmployeeId		
				, [DateCheckedOut]	 = @NewDateCheckedOut	
				, [DateReturned]	 = @NewDateReturned	
				, [DateExpectedBack] = @NewDateExpectedBack
				, [Returned]		 = @NewReturned		
				, [ResortVehicleId]	 = @NewResortVehicleId
			WHERE       [VehicleCheckoutId] = @VehicleCheckoutId
					AND [EmployeeId]	    = @OldEmployeeId		
					AND [DateCheckedOut]    = @OldDateCheckedOut		
					AND [DateExpectedBack]  = @OldDateExpectedBack
					AND [Returned]		    = @OldReturned		
					AND [ResortVehicleId]   = @OldResortVehicleId
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_delete_vehicle_checkout_by_id]
	(
		@VehicleCheckoutId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[VehicleCheckout]
		WHERE	[VehicleCheckoutId] = @VehicleCheckoutId

		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_create_resort_property]
	(
		@ResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortProperty] ([ResortPropertyTypeId]) 
		VALUES(@ResortPropertyTypeId)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_resort_properties]
AS
	BEGIN
		SELECT [ResortPropertyID], [ResortPropertyTypeId]
		FROM [ResortProperty]
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_resort_property_by_id]
	(
		@ResortPropertyId [int]	
	)
AS
	BEGIN
		SELECT [ResortPropertyID], [ResortPropertyTypeId]
		FROM [ResortProperty]
		WHERE [ResortPropertyID] = @ResortPropertyId
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_update_resort_property]
	(
		@ResortPropertyId			[int],
		@OldResortPropertyTypeId	[nvarchar](20),
		
		@NewResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		UPDATE [ResortProperty]
			SET [ResortPropertyTypeId]	= 	@NewResortPropertyTypeId
			WHERE 	    [ResortPropertyId]  = @ResortPropertyId
					AND	[ResortPropertyTypeId] = @OldResortPropertyTypeId
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_delete_resort_property_by_id]
	(
		@ResortPropertyId		[int]
	)
AS
	BEGIN
		DELETE
		FROM	[ResortProperty]
		WHERE	[ResortPropertyId] = @ResortPropertyId

		RETURN @@ROWCOUNT
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_create_resort_property_type]
	(
		@ResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResortPropertyType] ([ResortPropertyTypeId]) 
		VALUES(@ResortPropertyTypeId)
		SELECT SCOPE_IDENTITY() 
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_resort_property_types]
AS
	BEGIN
		SELECT [ResortPropertyTypeId]
		FROM [ResortPropertyType]
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_select_resort_property_type_by_id]
	(
		@ResortPropertyTypeId [int]	
	)
AS
	BEGIN
		SELECT [ResortPropertyTypeId]
		FROM [ResortPropertyType]
		WHERE [ResortPropertyTypeId] = @ResortPropertyTypeId
	END
GO

-- Created By: Francis Mingomba
-- Created On : 2019-04-03
CREATE PROCEDURE [dbo].[sp_update_resort_property_type]
	(
		@OldResortPropertyTypeId	[nvarchar](20),
		
		@NewResortPropertyTypeId	[nvarchar](20)
	)
AS
	BEGIN
		UPDATE [ResortPropertyType]
			SET [ResortPropertyTypeId]	 = @NewResortPropertyTypeId
			WHERE [ResortPropertyTypeId] = @OldResortPropertyTypeId
		RETURN @@ROWCOUNT
	END
GO



--Created By: Jacob Miller
--Created On: 3/2/19
--Updated:

CREATE PROCEDURE [dbo].[sp_select_all_luggage_status]
AS
	BEGIN
		SELECT [LuggageStatusID]
		FROM [LuggageStatus]
	END
GO
--Created By: Jacob Miller
--Created On: 3/28/19
--Updated:
CREATE PROCEDURE [dbo].[sp_insert_luggage]
	(
		@GuestID			[int],
		@LuggageStatusID	[nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO [Luggage]
			(
				[GuestID], [LuggageStatusID]
			)
		VALUES
			(
				@GuestID, @LuggageStatusID
			)
	END
GO

--Created By: Jacob Miller
--Created On: 3/28/19
--Updated:
CREATE PROCEDURE [dbo].[sp_select_luggage_by_id]
	(
		@LuggageID	[int]
	)
AS
	BEGIN
		SELECT	[LuggageID], [GuestID], [LuggageStatusID]
		FROM	[Luggage]
		WHERE	[LuggageID] = @LuggageID
	END
GO

--Created By: Jacob Miller
--Created On: 3/28/19
--Updated:

CREATE PROCEDURE [dbo].[sp_select_all_luggage]
AS
	BEGIN
		SELECT	[LuggageID], [GuestID], [LuggageStatusID]
		FROM	[Luggage]
	END
GO

--Created By: Jacob Miller
--Created On: 3/28/19
--Updated:

CREATE PROCEDURE [dbo].[sp_update_luggage_status]
	(
		@LuggageID			[int],
		@GuestID			[int],
		@OldLuggageStatus	[nvarchar](50),
		@NewLuggageStatus	[nvarchar](50)
	)
AS
	BEGIN
		UPDATE	[Luggage]
			SET	[LuggageStatusID] = @NewLuggageStatus
		WHERE	[GuestID] = @GuestID
			AND	[LuggageID] = @LuggageID
			AND	[LuggageStatusID] = @OldLuggageStatus
		RETURN @@ROWCOUNT
	END
GO

--Created By: Jacob Miller
--Created On: 3/28/19
--Updated:

CREATE PROCEDURE [dbo].[sp_update_luggage]
	(
		@LuggageID			[int],
		@OldGuestID			[int],
		@NewGuestID			[int],
		@OldLuggageStatusID	[nvarchar](50),
		@NewLuggageStatusID	[nvarchar](50)
	)
AS
	BEGIN
		UPDATE	[Luggage]
			SET	[LuggageStatusID] = @NewLuggageStatusID,
				[GuestID] = @NewGuestID
		WHERE	[LuggageID] = @LuggageID
			AND		[GuestID] = @OldGuestID
			AND		[LuggageStatusID] = @OldLuggageStatusID
		RETURN @@ROWCOUNT
	END
GO

--Created By: Jacob Miller
--Created On: 3/28/19
--Updated:

CREATE PROCEDURE [dbo].[sp_delete_luggage]
	(
		@LuggageID	[int]
	)
AS
	BEGIN
		DELETE FROM [Luggage]
		WHERE [LuggageID] = @LuggageID
		RETURN @@ROWCOUNT
	END
GO

--Created By: Jacob Miller
--Created On: 4/4/19
--Updated:
CREATE PROCEDURE [dbo].[sp_retrieve_all_guests]
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active],[ReceiveTexts], [EmergencyFirstName], 
			   [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation], [CheckedIn]
		FROM   [Guest]
	END

GO

/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
CREATE PROCEDURE sp_create_house_keeping_request
	(
	@BuildingNumber					[int],
	@RoomNumber						[int],
	@Description					[nvarchar](1000),
	@Active							[bit]
	)
AS
	BEGIN
		INSERT INTO [HouseKeepingRequest]
			([BuildingNumber], [RoomNumber], [Description],[Active])
		VALUES
			(@BuildingNumber, @RoomNumber, @Description, @Active )  
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/

CREATE PROCEDURE [dbo].[sp_select_all_house_keeping_requests] 
AS
	BEGIN
		SELECT 
			[HouseKeepingRequestID], 
			[BuildingNumber], 
			[RoomNumber],
			[Description],
			[WorkingEmployeeID],
			[Active]
		FROM HouseKeepingRequest
	END
GO

/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/

CREATE PROCEDURE [dbo].[sp_select_house_keeping_request_by_id] 
(
	@HouseKeepingRequestID		[int]
)
AS
	BEGIN
		SELECT 
			[HouseKeepingRequestID], 
			[BuildingNumber], 
			[RoomNumber],
			[Description],
			[WorkingEmployeeID],
			[Active]
		FROM HouseKeepingRequest
		WHERE [HouseKeepingRequestID] = @HouseKeepingRequestID
	END
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
CREATE PROCEDURE [dbo].[sp_update_house_keeping_request]
	(
	@HouseKeepingRequestID				[int],
	
	@oldBuildingNumber					[int],
	@oldRoomNumber						[int],
	@oldDescription						[nvarchar](1000),
	@oldActive							[bit],
	
	@newBuildingNumber					[int],
	@newRoomNumber						[int],
	@newDescription						[nvarchar](1000),
	@newWorkingEmployeeID				[int],
	@newActive							[bit]	
	)
AS
	BEGIN
		UPDATE [HouseKeepingRequest]
			SET [BuildingNumber] = @newBuildingNumber,
				[RoomNumber] = @newRoomNumber,
				[Description] = @newDescription,
				[Active] = @newActive,
				[WorkingEmployeeID] = @newWorkingEmployeeID
			WHERE [BuildingNumber] = @oldBuildingNumber
				AND [RoomNumber] = @oldRoomNumber
				AND [Description] = @oldDescription
				AND [Active] = @oldActive
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/

CREATE PROCEDURE [dbo].[sp_deactivate_house_keeping_request]
	(
		@HouseKeepingRequestID			[int]
	)
AS
	BEGIN
		UPDATE [HouseKeepingRequest]
			SET [Active] = 0
			WHERE 	
				[HouseKeepingRequestID] = @HouseKeepingRequestID
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 03-27-2019
*/
CREATE PROCEDURE [dbo].[sp_delete_house_keeping_request]
	(
		@HouseKeepingRequestID 				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [HouseKeepingRequest]
		WHERE  [HouseKeepingRequestID] = @HouseKeepingRequestID
		RETURN @@ROWCOUNT
	END
GO


/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
CREATE PROCEDURE [dbo].[sp_insert_inspection]
	(
		@ResortPropertyID				[int],
		@Name							[nvarchar](50),
		@DateInspected					[date],
		@Rating							[nvarchar](50),
		@ResortInspectionAffiliation	[nvarchar](25),
		@InspectionProblemNotes			[nvarchar](1000),
		@InspectionFixNotes				[nvarchar](1000)	
	)
AS
	BEGIN
		
		INSERT INTO [Inspection]
			(	
				[ResortPropertyID], 
				[Name], 
				[DateInspected], 
				[Rating], 
				[ResortInspectionAffiliation], 
				[InspectionProblemNotes], 
				[InspectionFixNotes]
			)
		VALUES
			(
				@ResortPropertyID,
				@Name,
				@DateInspected,
				@Rating,
				@ResortInspectionAffiliation,
				@InspectionProblemNotes,
				@InspectionFixNotes
			)
			
		SELECT [InspectionID] = @@IDENTITY
	END
GO

/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
CREATE PROCEDURE [dbo].[sp_select_inspection_by_resortpropertyid]
	(
		@ResortPropertyID	[int]
	)
AS
	BEGIN
		SELECT	[InspectionID],[Name],[DateInspected],[Rating],
				[ResortInspectionAffiliation],[InspectionProblemNotes],
				[InspectionFixNotes]
		FROM 	[Inspection]
		WHERE	[ResortPropertyID] = @ResortPropertyID
	END
GO

/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
CREATE PROCEDURE [dbo].[sp_select_all_inspections]
AS
	BEGIN
		SELECT	[InspectionID],[ResortPropertyID],[Name],[DateInspected],
				[Rating],[ResortInspectionAffiliation],[InspectionProblemNotes],
				[InspectionFixNotes]
		FROM 	[Inspection]
	END
GO

GO
/* 	Created By:  Dani Russo 
	Created: 03/29/2019
	
	Dani Russo
	Updated: 04/11/2019
	Added Inner Join
*/ 
CREATE PROCEDURE [dbo].[sp_select_room_list]
AS
	BEGIN
		SELECT 	[Room].[RoomID],
				[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity], 
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[OfferingID], 
				[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
	END
GO

/* 	Created By:  Dani Russo 
	Created: 03/29/2019
	
	Dani Russo
	Updated: 04/11/2019
	Added Inner Join
*/ 
CREATE PROCEDURE [dbo].[sp_select_room_by_id]
(
		@RoomID	[int]
)
AS
	BEGIN
		SELECT 	[Room].[RoomNumber], 
				[Room].[BuildingID], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity], 
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[OfferingID], 
				[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
		WHERE 	[RoomID] = @RoomID
	END
GO

/* 	Created By:  Dani Russo 
	Created: 03/29/2019
	
*/ 
CREATE PROCEDURE [dbo].[sp_select_room_types]

AS
	BEGIN
		SELECT 	[RoomTypeID]
		FROM 	[RoomType]
	END
GO

/* 	Created By:  Dani Russo 
	Created: 03/29/2019
	
*/ 
CREATE PROCEDURE [dbo].[sp_select_all_room_status]

AS
	BEGIN
		SELECT 	[RoomStatusID]
		FROM 	[RoomStatus]
	END
GO

-- *** Stored Procedures - Matthew Hill *** --
-- Author: <<Matthew Hill>>,Created:<<3/10/19>>

CREATE PROCEDURE [dbo].[sp_insert_pet_image_filename]
	(
		@Filename	[nvarchar](255),
		@PetID		[int]
	)
AS
	BEGIN
		INSERT INTO [PetImageFileName]
				([Filename], [PetID])
			VALUES
				(@Filename, @PetID)
		RETURN @@ROWCOUNT
	END
GO
-- *** Stored Procedures - Matthew Hill *** --
-- Author: <<Matthew Hill>>,Created:<<3/10/19>>

CREATE PROCEDURE [dbo].[sp_select_pet_image_filename_by_pet_id]
	(
		@PetID	[int]			
	)
AS
	BEGIN
		SELECT [Filename]
		FROM [PetImageFileName]
		WHERE [PetID] = @PetID
	END
GO
-- *** Stored Procedures - Matthew Hill *** --
-- Author: <<Matthew Hill>>,Created:<<3/10/19>>

CREATE PROCEDURE [dbo].[sp_update_pet_image_filename]
	(
		@PetID			[int],
		@OldFilename	[nvarchar](255),
		@NewFilename	[nvarchar](255)
	)
AS
	BEGIN
		UPDATE [dbo].[PetImageFileName]
		SET [Filename] = @NewFilename
		WHERE [PetID] = @PetID
			AND	[Filename] = @OldFilename
			
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Alisa Roehr / Caitlin Abelson
-- Created On: 2019-04-09

CREATE PROCEDURE sp_retrieve_all_guests_ordered
AS
	BEGIN
		SELECT [GuestID], [MemberID], [GuestTypeID], [FirstName], 
			   [LastName], [PhoneNumber], [Email], 
			   [Minor], [Active], [CheckedIn]
		FROM   [Guest]
		ORDER BY [GuestID], [Active]
	END
GO

-- Created By: Alisa Roehr / Caitlin Abelson
-- Created On: 2019-04-09
CREATE PROCEDURE sp_check_out_guest_by_id
	(
		@GuestID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Guest]
		SET 	[CheckedIn] = 0
		WHERE	[GuestID] = @GuestID
		  
		RETURN @@ROWCOUNT		
	END
GO

-- Created By: Alisa Roehr / Caitlin Abelson
-- Created On: 2019-04-09
CREATE PROCEDURE sp_check_in_guest_by_id
	(
		@GuestID		[nvarchar](17)
	)
AS
	BEGIN
		UPDATE 	[Guest]
		SET 	[CheckedIn] = 1
		WHERE	[GuestID] = @GuestID
		  
		RETURN @@ROWCOUNT		
	END
GO

-- Created By: Alisa Roehr / Caitlin Abelson
-- Created On: 2019-04-09
CREATE PROCEDURE sp_retrieve_all_guest_types
AS
	BEGIN
		SELECT 		[GuestTypeID]
		FROM		[GuestType]
		ORDER BY 	[GuestTypeID]
	END
GO

/*  Created By: Eduardo Colon
    Date: 2019-03-25 
*/

CREATE PROCEDURE [dbo].[sp_retrieve_all_shuttle_reservations]

AS
	BEGIN
		SELECT 	  [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime], [Active]
		FROM	  [ShuttleReservation]
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_shuttle_reservation_by_id]
	(
		@ShuttleReservationID			[int]
	)
AS
	BEGIN
		SELECT [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime]
		FROM [ShuttleReservation]
		WHERE ShuttleReservationID = @ShuttleReservationID
	END
GO
/*  Created By: Eduardo Colon
 Date:   2019-03-25
*/
CREATE PROCEDURE sp_insert_shuttle_reservation
	(
	@GuestID							[int],
	@EmployeeID							[int],
	@PickupLocation						[nvarchar](150),
	@DropoffDestination					[nvarchar](150),
	@PickupDateTime						[datetime],				
	@DropoffDateTime					[datetime]
	
	)
AS
	BEGIN
		INSERT INTO [ShuttleReservation]
			([GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime])
		VALUES
			(@GuestID, 	@EmployeeID,@PickupLocation, @DropoffDestination, @PickupDateTime,@DropoffDateTime)
	  
		RETURN @@ROWCOUNT
	END
GO



/*  Created By: Eduardo Colon
  Date:   2019-03-25
  */

CREATE PROCEDURE [dbo].[sp_update_shuttle_reservation_by_id]
	(
		
	@ShuttleReservationID			 		[int],
	@OldGuestID							    [int],
	@NewGuestID							    [int],
	@OldEmployeeID 							[int],
	@NewEmployeeID 							[int],
	@OldPickupLocation						[nvarchar](150),
	@NewPickupLocation						[nvarchar](150),
	@OldDropoffDestination					[nvarchar](150),
	@NewDropoffDestination					[nvarchar](150),
	@OldPickupDateTime						[datetime],	
	@NewPickupDateTime						[datetime]
	)
AS
	BEGIN
	
		BEGIN
			UPDATE [ShuttleReservation]
				SET   [GuestID]				 = 	@NewGuestID,
					  [EmployeeID]			 = 	@NewEmployeeID,
					  [PickupLocation]	 	 = 	@NewPickupLocation,
					  [DropoffDestination] 	 = 	@NewDropoffDestination,
					  [PickupDateTime]	 	 =  @NewPickupDateTime
				WHERE [ShuttleReservationID] =  @ShuttleReservationID
				AND	  [GuestID] 			 = 	@OldGuestID
				AND	  [EmployeeID] 			 = 	@OldEmployeeID
				AND   [PickupLocation]       = 	@OldPickupLocation
				AND	  [DropoffDestination]   = 	@OldDropoffDestination
				AND	  [PickupDateTime]       =  @OldPickupDateTime
			RETURN @@ROWCOUNT
		END
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
  */
CREATE PROCEDURE [dbo].[sp_shuttle_dropoff_time_now]
	(
	@ShuttleReservationID			 		[int],
	@OldGuestID							    [int],
	@OldEmployeeID 							[int],
	@OldPickupLocation						[nvarchar](150),
	@OldDropoffDestination					[nvarchar](150),
	@OldPickupDateTime						[datetime]
	)
AS
	BEGIN
	
		BEGIN
			UPDATE [ShuttleReservation]
				SET   [DropoffDateTime] = getutcdate()
				WHERE [ShuttleReservationID] =  @ShuttleReservationID
				AND	  [GuestID] 			 = 	@OldGuestID
				AND	  [EmployeeID] 			 = 	@OldEmployeeID
				AND   [PickupLocation]       = 	@OldPickupLocation
				AND	  [DropoffDestination]   = 	@OldDropoffDestination
				AND	  [PickupDateTime]       =  @OldPickupDateTime
			RETURN @@ROWCOUNT
		END
	END
GO


/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_delete_ShuttleReservationID ]
	(
		@ShuttleReservationID		[int]
	)
	
AS
	BEGIN
		DELETE
		FROM [ShuttleReservation]
		WHERE [ShuttleReservationID] = @ShuttleReservationID	
		RETURN @@ROWCOUNT
	END
GO

		
/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE sp_deactivate_shuttle_reservation_by_id 
	(
		@ShuttleReservationID		[int]
	)
AS
	BEGIN
		UPDATE [ShuttleReservation]
		SET 	[Active] = 0
		WHERE 	[ShuttleReservationID] = @ShuttleReservationID
	  
		RETURN @@ROWCOUNT
	END
GO



/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE sp_retrieve_shuttle_reservation_by_term_in_pickup_location
(
		@SearchTerm		[nvarchar](250)
	)
AS
	BEGIN
		SELECT   [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime]
		FROM 	[ShuttleReservation]
		WHERE 	[PickupLocation] LIKE '%' + @SearchTerm + '%'
		
	END
GO


/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE sp_retrieve_guest_by_term_in_last_name
(
		@SearchTerm		[nvarchar](250)
	)
AS
	BEGIN
		SELECT   [GuestID],[FirstName],[LastName],[PhoneNumber]
		FROM 	[Guest]
		WHERE 	[LastName] LIKE '%' + @SearchTerm + '%'
		
	END
GO


/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_employee_info_by_id]
(
	@EmployeeID [int]
)
AS
	BEGIN
		SELECT [EmployeeID],[FirstName],[LastName]
		FROM [Employee]
		WHERE [EmployeeID] = @EmployeeID
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_employee_info]

AS
	BEGIN
		SELECT [EmployeeID],[FirstName],[LastName]
		FROM [Employee]
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_guest_info_by_id]
(
	@GuestID [int]
)
AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[PhoneNumber]
		FROM [Guest]
		WHERE [GuestID] = @GuestID
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_guest_info]

AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[PhoneNumber]
		FROM [Guest]
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_active_shuttle_reservation]
AS
	BEGIN
		SELECT [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime], [Active]
		FROM [ShuttleReservation]
		WHERE [Active] = 1
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-03-25
*/
CREATE PROCEDURE [dbo].[sp_retrieve_inactive_shuttle_reservation]
AS
	BEGIN
		SELECT [ShuttleReservationID],[GuestID],[EmployeeID],[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime], [Active]
		FROM [ShuttleReservation]
		WHERE [Active] = 0
	END
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
CREATE PROCEDURE [dbo].[sp_retrieve_user_by_email]
(
		@Email				[nvarchar](250)
)
AS
    Begin
        Select [EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID]
        From Employee
        Where [Email] = @Email
    End
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
CREATE PROCEDURE [dbo].[sp_insert_internal_order]
    (
        @EmployeeID         [int],
        @DepartmentID       [nvarchar](50),
        @Description        [nvarchar](250),
        @OrderComplete      [bit],
        @DateOrdered        [DateTime]
    )
AS
    BEGIN
        Insert INTO [InternalOrder]

        ([EmployeeID], [DepartmentID], [Description], [OrderComplete], [DateOrdered])
        VALUES (@EmployeeID, @DepartmentID, @Description, @OrderComplete, @DateOrdered)
        Select Scope_Identity()
    END
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
CREATE Procedure [dbo].[sp_insert_internal_order_line]
    (
        @ItemID             [int],
        @InternalOrderID    [int],
        @OrderQty           [int],
        @QtyReceived        [int]
    )
AS
    BEGIN
        INSERT INTO [InternalOrderLine]
        ([ItemID], [InternalOrderID], [OrderQty], [QtyReceived])
        VALUES (@ItemID, @InternalOrderID, @OrderQty, @QtyReceived)
        RETURN @@ROWCOUNT
    END
GO


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
Create Procedure [dbo].[sp_select_all_item_names_and_ids]
AS
    Begin   
        Select [Name], [ItemID]
        From [Item]
    End
Go


/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
Create Procedure [dbo].[sp_select_all_internal_orders]
As
    Begin
        Select [InternalOrder].[InternalOrderID], [InternalOrder].[EmployeeID], [Employee].[FirstName], [Employee].[LastName], [InternalOrder].[DepartmentID], [InternalOrder].[Description],
        [InternalOrder].[OrderComplete], [InternalOrder].[DateOrdered]
        From InternalOrder Inner Join Employee On InternalOrder.EmployeeID = Employee.EmployeeID
        Inner Join Department on InternalOrder.DepartmentID = Department.DepartmentID
    END
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
Create Procedure [dbo].[sp_select_order_lines_by_id]
(
    @InternalOrderID    [int]
)
AS
    Begin   
        Select [InternalOrderLine].[ItemID], [Item].[Name], [InternalOrderLine].[OrderQty],
        [InternalOrderLine].[QtyReceived]
        From InternalOrderLine inner Join Item on InternalOrderLine.ItemID = Item.ItemID
        Where InternalOrderID = @InternalOrderID
    End
GO

/*
 * Author: Richard Carroll
 * Created: 2019/01/29
 */
CREATE Procedure [dbo].[sp_update_order_status_to_complete]
(
    @InternalOrderID    [int],
    @OrderComplete      [bit]
)
AS
    Begin
        Update InternalOrder
        Set OrderComplete = 1
        Where InternalOrderID = @InternalOrderID
        AND OrderComplete = @OrderComplete
    END
GO


-- Created By: Phillip Hansen
-- Created On: 2019-04-12
CREATE PROCEDURE [dbo].[sp_insert_event_performance]
	(
		@EventID		[int],
		@PerformanceID	[int]
	)
AS
	BEGIN
		INSERT INTO [EventPerformance]
			([EventID], [PerformanceID])
		VALUES
			(@EventID, @PerformanceID)
		
		RETURN @@ROWCOUNT
	END
GO

-- Created By: Phillip Hansen
-- Created On: 2019-04-12
CREATE PROCEDURE [dbo].[sp_retrieve_all_event_performances]
AS
	BEGIN
		SELECT [EventPerformance].[EventID]
					, [Event].[EventTitle]
					,[Performance].[PerformanceTitle]
					, [EventPerformance].[PerformanceID]
		FROM	[Event] INNER JOIN [EventPerformance]
					ON [Event].[EventID] = [EventPerformance].[EventID]
				INNER JOIN [Performance] 
				ON [EventPerformance].[PerformanceID] = [Performance].[PerformanceID]
		WHERE 	[Event].[EventID] = [EventPerformance].[EventID]
		AND		[Performance].[PerformanceID] = [EventPerformance].[PerformanceID]
	END
GO

-- Created By : Kevin Broskow
-- Created On: 2019-04-12
CREATE PROCEDURE [dbo].[sp_select_supplier_item_by_item_and_supplier](
@ItemID	[int],
@SupplierID		[int]
)
AS
BEGIN
SELECT 
[SupplierItemID]
FROM [ItemSupplier]
WHERE [ItemID] = @ItemID
AND		[SupplierID] = @SupplierID
END
GO

-- Created By: Austin Berquam
-- Created On: 2019-04-12
CREATE PROCEDURE [dbo].[sp_select_member_by_email]
(
	@Email			[nvarchar](50)
)
AS
	BEGIN
		SELECT 	[MemberID]  
		FROM	[Member]
		WHERE	[Email] = @Email
	END
GO



/*
Author: Caitlin Abelson
Created Date: 4/13/2019

This is the stored procedure for selecting all Guests and their Members
*/
CREATE PROCEDURE [dbo].[sp_select_guest_member]
AS 
	SELECT	[Guest].[GuestID], [Guest].[MemberID], [Guest].[GuestTypeID], [Guest].[FirstName], [Guest].[LastName], [Guest].[PhoneNumber], 
			[Guest].[Email], [Guest].[Minor], [Guest].[Active], [Guest].[ReceiveTexts], [Guest].[EmergencyFirstName],  
			[Guest].[EmergencyLastName], [Guest].[EmergencyPhoneNumber], [Guest].[EmergencyRelation], [Guest].[CheckedIn], 
			[Member].[FirstName], [Member].[LastName]
	FROM	[Guest] inner join [Member] on
			[Guest].[MemberID] = [Member].[MemberID]
GO


-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Retrieve View Model of all Offerings
-- Updated By: Jared Greenfield
-- Updated On: 2019-05-04
-- Updated to use building name instead of ID
CREATE PROCEDURE [sp_select_all_offeringvms]
AS
-- Retrieve all Events
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   [Event].[EventTitle] AS 'OfferingName'
FROM [Offering]
INNER JOIN [Event] ON [Event].[OfferingID] = [Offering].[OfferingID]
UNION
-- Retrieve all Items
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   [Item].[Name] AS 'OfferingName'
FROM [Offering]
INNER JOIN [Item] ON [Item].[OfferingID] = [Offering].[OfferingID]
UNION
-- Retrieve all Services
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   [ServiceComponent].[ServiceComponentID] AS 'OfferingName'
FROM [Offering]
INNER JOIN [ServiceComponent] ON [ServiceComponent].[OfferingID] = [Offering].[OfferingID]
UNION
-- Retrieve all Rooms
SELECT [Offering].[OfferingID],
	   [Offering].[OfferingTypeID],
	   [Offering].[Description],
	   [Offering].[Price],
	   [Offering].[Active],
	   CONCAT([Building].[BuildingName], ' ', [Room].[RoomNumber]) AS 'OfferingName'
FROM [Offering]
INNER JOIN [Room] ON [Room].[OfferingID] = [Offering].[OfferingID]
INNER JOIN [Building] ON [Building].[BuildingID] = [Room].[BuildingID]
ORDER BY [OfferingTypeID] ASC
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-27
-- Retrieve all offering types
CREATE PROCEDURE [sp_select_all_offeringtypes]
AS
SELECT [OfferingTypeID]
FROM [OfferingType]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-28
-- Delete an Offering
CREATE PROCEDURE [sp_delete_offering]
(
	@OfferingID [int]
)
AS
DELETE FROM [Offering]
WHERE [OfferingID] = @OfferingID
RETURN @@ROWCOUNT
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-28
-- Deactivate an offering
CREATE PROCEDURE [sp_deactivate_offering]
(
	@OfferingID [int]
)
AS
UPDATE [Offering]
SET [Active] = 0
WHERE [OfferingID] = @OfferingID
RETURN @@ROWCOUNT
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-03-28
-- Reactivate an offering
CREATE PROCEDURE [sp_reactivate_offering]
(
	@OfferingID [int]
)
AS
UPDATE [Offering]
SET [Active] = 1
WHERE [OfferingID] = @OfferingID
RETURN @@ROWCOUNT
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Select an item/event/room/service based on their Offering ID and TYPE
CREATE PROCEDURE [sp_select_offeringsubitemid_by_idandtype]
(
	@OfferingID [int],
	@OfferingType [nvarchar](15)
)
AS
	IF @OfferingType = 'Room'
	BEGIN 
		SELECT 
		[RoomID],
		[BuildingID],
		[RoomNumber],
		[RoomTypeID], 
		[Description],
		[Capacity],
		[RoomStatusID],
		[ResortPropertyID]		
		FROM [Room]
		WHERE @OfferingID = [OfferingID]
	END
	ELSE
	BEGIN
		IF @OfferingType = 'Service'
		BEGIN
			SELECT 'Service'
		END
		ELSE
		BEGIN
			IF @OfferingType = 'Event'
			BEGIN
				SELECT 
				[EventID],
				[EventTypeID],
				[EventStartDate],
				[NumGuests],
				[SeatsRemaining],
				[PublicEvent],
				[Description],
				[KidsAllowed],
				[Location],
				[EventEndDate],
				[EventTitle],
				[Sponsored],
				[EmployeeID],
				[Approved],
				[Cancelled]
				FROM [Event]
				WHERE OfferingID = [OfferingID]
			END
			ELSE
			BEGIN
				IF @OfferingType = 'Item'
				BEGIN
					SELECT 
					[ItemID],
					[ItemTypeID],
					[RecipeID],
					[CustomerPurchasable],
					[Description],
					[OnHandQty],
					[Name],
					[ReOrderQty],
					[DateActive],
					[Active]
					FROM [Item]
					WHERE @OfferingID = [OfferingID]
				END
			END
		END
	END
GO
	
-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Deletes an item
CREATE PROCEDURE [sp_delete_item]
(
	@ItemID [int]
)
AS
DELETE FROM [Item] 
WHERE @ItemID = [ItemID]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Deactivate an item
CREATE PROCEDURE [sp_deactivate_item]
(
	@ItemID [int]
)
AS
UPDATE [Item] 
SET [Active] = 0
WHERE @ItemID = [ItemID]
GO

-- Created By: Jared Greenfield
-- Created Date: 2019-04-03
-- Update an item
CREATE PROCEDURE [sp_update_item]
(
	@ItemID	[int],	
	
	@OldOfferingID	[int],	
	@OldItemTypeID	[nvarchar]	(15),
	@OldRecipeID	[int],	
	@OldCustomerPurchasable	[bit],	
	@OldDescription	[nvarchar]	(1000),
	@OldOnHandQty	[int],	
	@OldName	[nvarchar]	(50),
	@OldReOrderQty	[int],	
	@OldActive	[bit],
	
	@NewOfferingID	[int],	
	@NewItemTypeID	[nvarchar]	(15),
	@NewRecipeID	[int],	
	@NewCustomerPurchasable	[bit],	
	@NewDescription	[nvarchar]	(1000),
	@NewOnHandQty	[int],	
	@NewName	[nvarchar]	(50),
	@NewReOrderQty	[int],	
	@NewActive	[bit]
)
AS
UPDATE [Item]
SET 
	[OfferingID] = @NewOfferingID,	
	[ItemTypeID] = @NewItemTypeID,
	[RecipeID] = @NewRecipeID,	
	[CustomerPurchasable] = @NewCustomerPurchasable,	
	[Description] = @NewDescription,
	[OnHandQty] = @NewOnHandQty,	
	[Name] = @NewName,
	[ReOrderQty] = @NewReOrderQty,	
	[Active] = @NewActive
	
	WHERE 
	[ItemID] = @ItemID AND
	[OfferingID] = @OldOfferingID OR [OfferingID] IS NULL AND
	[ItemTypeID] = @OldItemTypeID AND
	[RecipeID] = @OldRecipeID OR [RecipeID] IS NULL AND	
	[CustomerPurchasable] = @OldCustomerPurchasable AND	
	[Description] = @OldDescription OR [Description] IS NULL AND
	[OnHandQty] = @OldOnHandQty AND	
	[Name] = @OldName AND
	[ReOrderQty] = @OldReOrderQty AND	
	[Active] = @OldActive
	RETURN @@ROWCOUNT
GO

/* 	Created By:  Dani Russo 
	Created: 04/10/2019
	
	Dani Russo
	Updated: 04/11/2019
	Added Inner Join
	
*/ 
CREATE PROCEDURE [dbo].[sp_select_room_list_by_buildingid]
	(
		@BuildingID	[nvarchar](50)
	)
AS
	BEGIN
		SELECT  [Room].[RoomID],
				[Room].[RoomNumber], 
				[Room].[RoomTypeID], 
				[Room].[Description], 
				[Room].[Capacity],
				[Offering].[OfferingID],	
				[Offering].[Price],
				[Room].[ResortPropertyID], 
				[Room].[RoomStatusID]
		FROM 	[Room] INNER JOIN [Offering]
				ON [Room].[OfferingID] = [Offering].[OfferingID]
		WHERE [Room].[BuildingID] = @BuildingID
	END
GO

/*
Author: Caitlin Abelson
Created Date: 2/25/19

This is the stored procedure for create setup
*/
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
CREATE PROCEDURE [dbo].[sp_select_all_setuplists]
AS
	BEGIN
		SELECT 	    [SetupListID], [SetupID], [Completed], [Description],	[Comments]
		FROM		[SetupList]
	END
GO
	
-- NAME:  Eduardo Colon'
-- Date:   2019-03-05'
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

/*
 * Author: Gunardi Saputra
 * Created: 2019/01/23
 * This stored procedure create a sponsor record in the sponsor table 
 * Updated: 2019/04/03
 * Remove statusID
 */
CREATE PROCEDURE sp_insert_sponsor
	(
		@Name				[nvarchar](50),
		@Address			[nvarchar](25),
		@City				[nvarchar](50),
		@State				[nvarchar](2),
		@PhoneNumber		[nvarchar](11),
		@Email				[nvarchar](50),
		@ContactFirstName	[nvarchar](50),
		@ContactLastName	[nvarchar](100)

	)
AS
	BEGIN
		INSERT INTO [dbo].[Sponsor]
			( [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
			[ContactLastName],[DateAdded])
		VALUES
			(@Name, @Address, @City, @State,
			@PhoneNumber, @Email, @ContactFirstName, @ContactLastName, Convert(Varchar(10), GetDate(), 101))
			
		RETURN @@ROWCOUNT
	END		
GO

/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure updates a sponsor record in the sponsor table by its id.
* Updated: 2019/04/03
* Remove statusID
*/
CREATE PROCEDURE sp_update_sponsor
	(
		@SponsorID			[int],
		
		@OldName			[nvarchar](50),
		@OldAddress			[nvarchar](25),
		@OldCity			[nvarchar](50),
		@OldState			[nvarchar](2),
		@OldPhoneNumber		[nvarchar](11),
		@OldEmail			[nvarchar](50),
		@OldContactFirstName	[nvarchar](50),
		@OldContactLastName	[nvarchar](100),
		@OldDateAdded		[datetime],
		@OldActive			[bit],
		
		
		@Name				[nvarchar](50),
		@Address			[nvarchar](25),
		@City			[nvarchar](50),
		@State			[nvarchar](2),
		@PhoneNumber		[nvarchar](11),
		@Email			[nvarchar](50),
		@ContactFirstName [nvarchar](50),
		@ContactLastName [nvarchar](100),
		@DateAdded		[datetime],
		@Active			[bit]
		
	)

AS
	BEGIN
		UPDATE	[Sponsor]
		SET 	
				[Name]				= @Name,
				[Address]				= @Address,
				[City]				= @City,
				[State]				= @State,
				[PhoneNumber]		= @PhoneNumber,
				[Email]				= @Email,
				[ContactFirstName]				= @ContactFirstName,
				[ContactLastName]				= @ContactLastName,
				[DateAdded]				= @DateAdded,
		
		[Active]				= @Active
				
			

		WHERE	[SponsorID] 	= @SponsorID
		AND [Name]				= @OldName
		AND [Address]		 =  @OldAddress
		AND [City]		 =  @OldCity
		AND [State]		 =  @OldState
		AND [PhoneNumber]		 =  @OldPhoneNumber
		AND [Email]		 =  @OldEmail
		AND [ContactFirstName]		 =  @OldContactFirstName
		AND [ContactLastName]		 =  @OldContactLastName
		AND [DateAdded]		 =  @OldDateAdded
		AND [Active]		 =  @OldActive
			
		RETURN @@ROWCOUNT
	END
GO


/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure activate a sponsor record in the sponsor table by its id.
*/
CREATE PROCEDURE sp_activate_sponsor_by_id
	(
		@SponsorID		[int]
	)
AS
	BEGIN
		UPDATE 	[Sponsor]
		SET 	[Active] = 1
		WHERE	[SponsorID] = @SponsorID
		  
		RETURN @@ROWCOUNT		
	END
GO


/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure deactivate a sponsor record in the sponsor table by its id.
*/
CREATE PROCEDURE sp_deactivate_sponsor
	(
		@SponsorID		[int]
	)
AS
	BEGIN
		UPDATE 	[Sponsor]
		SET 	[Active] = 0
		WHERE	[SponsorID] = @SponsorID
		  
		RETURN @@ROWCOUNT		
	END
GO



/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure delete a sponsor record in the sponsor table by its id.
*/

CREATE PROCEDURE [dbo].[sp_delete_sponsor]
	(
		@SponsorID 				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [Sponsor]
		WHERE  [SponsorID] = @SponsorID
		RETURN @@ROWCOUNT
	END
GO


/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure retrieve a sponsor record in the sponsor table by its id.
*/
CREATE PROCEDURE [dbo].[sp_select_sponsor]
(
	@SponsorID 				[int]
)
AS
	BEGIN
		SELECT [SponsorID], [Name],[Address],[City],[State],[PhoneNumber],[Email],[ContactFirstName],
			[ContactLastName],[DateAdded],[Active]		
			FROM [Sponsor]
		WHERE [SponsorID] = @SponsorID
	END
GO


/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure populate sponsors record in the sponsor table by its id.
*/
CREATE PROCEDURE [dbo].[sp_retrieve_all_view_model_sponsors]
AS
	BEGIN
		SELECT [Sponsor].[SponsorID],
		[Sponsor].[SponsorID],
		[Sponsor].[Name],
		[Sponsor].[Address],
		[Sponsor].[City],
		[Sponsor].[State],
		[Sponsor].[PhoneNumber],
		[Sponsor].[Email],
		[Sponsor].[ContactFirstName],
		[Sponsor].[ContactLastName],
		[Sponsor].[DateAdded],
		[Sponsor].[Active]
		FROM Sponsor 
	END
GO


--Eric Bostwick 
--Created 3/26/19
--Updated
CREATE PROCEDURE [dbo].[sp_select_all_picksheets]			
	AS
		BEGIN
		SELECT [PickSheetID], [PickSheetCreatedBy], [eCreatedBy].[FirstName] + ' ' + [eCreatedBy].[LastName] AS PickCreatedByName,
		[CreateDate], [PickCompletedBy], [eCompletedBy].[FirstName] + ' ' +  [eCompletedBy].[LastName] AS PickCompletedByName,
		[PickCompletedDate], [PickDeliveredBy], [eDeliveredBy].[FirstName] + ' ' +  [eDeliveredBy].[LastName] AS PickDeliveredByName,
		[PickDeliveryDate], [NumberOfOrders]
		FROM	[PickSheet] p 
		LEFT OUTER JOIN [Employee] eCreatedBy ON eCreatedBy.EmployeeID = p.PickSheetCreatedBy 
		LEFT OUTER JOIN [Employee] eCompletedBy ON eCompletedBy.EmployeeID = p.PickCompletedBy
		LEFT OUTER JOIN [Employee] eDeliveredBy ON eDeliveredBy.EmployeeID = p.PickDeliveredBy
		
	END
GO

--Eric Bostwick 
--Created 3/26/19
--Updated
CREATE PROCEDURE [dbo].[sp_select_all_picksheets_by_date]	

	@StartDate DATETIME
			
	AS
		BEGIN
		SELECT [PickSheetID], [PickSheetCreatedBy], [eCreatedBy].[FirstName] + ' ' + [eCreatedBy].[LastName] AS PickCreatedByName,
		[CreateDate], [PickCompletedBy], [eCompletedBy].[FirstName] + ' ' +  [eCompletedBy].[LastName] AS PickCompletedByName,
		[PickCompletedDate], [PickDeliveredBy], [eDeliveredBy].[FirstName] + ' ' +  [eDeliveredBy].[LastName] AS PickDeliveredByName,
		[PickDeliveryDate], [NumberOfOrders]
		FROM	[PickSheet] p 
		LEFT OUTER JOIN [Employee] eCreatedBy ON eCreatedBy.EmployeeID = p.PickSheetCreatedBy 
		LEFT OUTER JOIN [Employee] eCompletedBy ON eCompletedBy.EmployeeID = p.PickCompletedBy
		LEFT OUTER JOIN [Employee] eDeliveredBy ON eDeliveredBy.EmployeeID = p.PickDeliveredBy
		WHERE CreateDate >= @StartDate
	END
GO

--Eric Bostwick 
--Created 3/26/19
--Updated
Create PROCEDURE [dbo].[sp_insert_tmppicksheet_to_picksheet]      
	--Moves Records from the tmpPickSheet to the Picksheet table
	--And Updates InternalOrderLine 
	@OrdersAffected int OUTPUT,
	@PickSheetID nvarchar(25)   
AS
BEGIN
	BEGIN TRY

		BEGIN TRAN 

		UPDATE [InternalOrderLine] SET PickSheetID = NULL, [OrderReceivedDate] = NULL, [OrderStatus] = 1 WHERE [PickSheetID] = @PickSheetID

		UPDATE [InternalOrderLine] SET [PicksheetID] = t.[PickSheetID],  [OrderReceivedDate] = getdate(), [OrderStatus] = 2
		FROM [tmpPickTable] AS t JOIN [InternalOrderLine] AS o ON o.[InternalOrderID] = t.[InternalOrderID] AND o.[ItemID] = t.[ItemID]
		WHERE t.PickSheetID = @PickSheetID

		DECLARE @RecordCount int
		DECLARE @NumberOfOrders int
		DECLARE @PickedBy int

		SET @NumberOfOrders = (SELECT Count(*) FROM [InternalOrderLine] WHERE PickSheetID = @PickSheetID)

		SET @RecordCount = (SELECT COUNT(*) FROM PickSheet WHERE PickSheetID = @PickSheetID)

		SET @PickedBy = (SELECT TOP 1 PickedBy FROM tmpPickTable WHERE PickSheetID = @PickSheetID)

		IF @RecordCount > 0   --Update
		  BEGIN
			UPDATE PickSheet SET UpdateDate = getdate(), NumberofOrders = @NumberOfOrders WHERE PickSheetID = @PickSheetID
		  END
		Else
		  BEGIN
			INSERT INTO PickSheet(PickSheetID, PickSheetCreatedBy, NumberofOrders) VALUES(@PickSheetid, @PickedBy, @NumberOfOrders)
		  END

		DELETE FROM tmpPickTable WHERE PickSheetID = @PickSheetID
		
		SET @OrdersAffected = @NumberOfOrders

		COMMIT TRAN

		RETURN @RecordCount 

	END TRY
	BEGIN CATCH
			--Select @@ERROR as Error
			ROLLBACK TRAN
			RETURN 0
	END CATCH
END
GO
--Eric Bostwick 
--Created 3/26/19
--Updated
CREATE PROCEDURE [dbo].[sp_insert_tmp_picksheet]  
     
     @PickSheetID nvarchar(25),  
     @InternalOrderID int,
     @ItemID int,
	 @PickedBy int
AS
BEGIN	

BEGIN TRY
	BEGIN TRAN		

        Update [InternalOrderLine] SET PickSheetID = @PickSheetID, OrderStatus = 2 WHERE InternalOrderID = @InternalOrderID AND ItemID = @ItemID AND OrderStatus = 1
		INSERT INTO tmpPickTable(PickSheetID, InternalOrderID, ItemID, PickedBy)
						  VALUES(@PickSheetID, @InternalOrderID, @ItemID, @Pickedby)
		
		COMMIT TRAN

		return @@ROWCOUNT

END TRY
BEGIN CATCH
		
		ROLLBACK TRAN
		
		RETURN 0

END CATCH
END
GO
--Eric Bostwick 
--Created 3/26/19
--Updated

CREATE PROCEDURE [dbo].[sp_delete_tmp_picksheet] 
	      
     @PickSheetID nvarchar(25)   
AS

BEGIN	

DELETE FROM tmpPickTable WHERE PickSheetID = @PickSheetID

UPDATE [InternalOrderLine] SET PickSheetID = NULL, OrderStatus = 1 WHERE PickSheetID = @PickSheetID

END
GO

--Eric Bostwick 
--Created 3/26/19
--Updated

CREATE PROCEDURE [dbo].[sp_delete_tmp_picksheet_item]       
     @PickSheetID nvarchar(25),
     @InternalOrderID int,
	 @ItemID int   
AS
BEGIN	
DELETE FROM [tmpPickTable] WHERE [PickSheetID] = @PickSheetID AND [InternalOrderID] = @InternalOrderID AND ItemID = @ItemID 

Update [InternalOrderLine] SET PickSheetID = NULL, OrderStatus = 1 WHERE InternalOrderID = @InternalOrderID AND ItemID = @ItemID 

return @@ROWCOUNT

END

GO

--Eric Bostwick 
--Created 4/2/2019
--Updated
CREATE PROCEDURE [dbo].[sp_select_orders_for_acknowledgement] 

@StartDate datetime 

AS
BEGIN

SELECT 
o.[DateOrdered] as [OrderDate],
e.[EmployeeID], e.[FirstName] + ' ' + e.[LastName] as [Orderer],
i.[ItemID], i.[Description] as ItemDescription,
ol.InternalOrderID,
ol.[OrderQty], 
ol.[OrderReceivedDate], 
ol.[PickCompleteDate], 
ol.[PickSheetID], 
ol.[DeliveryDate], 
d.[DepartmentID],
d.[Description],
ol.[OrderStatus],
[OrderStatusView] = CASE ol.[OrderStatus] 
WHEN 1 THEN 'ORDERED' 
WHEN 2 THEN 'ORDER ACKNOWLEDGED'
WHEN 3 THEN 'ORDER PICKED' 
WHEN 4 THEN 'ORDER DELIVERED' END
FROM [InternalOrder] o LEFT OUTER JOIN [dbo].[InternalOrderLine] ol ON o.[InternalOrderID] = ol.[InternalOrderID]
LEFT OUTER JOIN [Employee] e ON e.[EmployeeID] = o.[EmployeeID]
LEFT OUTER JOIN [Department] d ON d.[DepartmentID] = o.[DepartmentID]
LEFT OUTER JOIN [Item] i ON i.[ItemID] = ol.[ItemID]
WHERE  o.[DateOrdered] > @StartDate 

END
GO

--Eric Bostwick 
--Created 4/2/2019
--Updated
CREATE PROCEDURE [dbo].[sp_select_orders_for_acknowledgement_hidepicked] 

@StartDate datetime 

AS
BEGIN

SELECT 
o.[DateOrdered] as [OrderDate],
e.[EmployeeID], e.[FirstName] + ' ' + e.[LastName] as [Orderer],
i.[ItemID], i.[Description] as ItemDescription,
ol.InternalOrderID,
ol.[OrderQty], 
ol.[OrderReceivedDate], 
ol.[PickCompleteDate], 
ol.[PickSheetID], 
ol.[DeliveryDate], 
d.[DepartmentID],
d.[Description],
ol.[OrderStatus],
[OrderStatusView] = CASE ol.[OrderStatus] 
WHEN 1 THEN 'ORDERED' 
WHEN 2 THEN 'ORDER ACKNOWLEDGED'
WHEN 3 THEN 'ORDER PICKED' 
WHEN 4 THEN 'ORDER DELIVERED' END
FROM [InternalOrder] o LEFT OUTER JOIN [dbo].[InternalOrderLine] ol ON o.[InternalOrderID] = ol.[InternalOrderID]
LEFT OUTER JOIN [Employee] e ON e.[EmployeeID] = o.[EmployeeID]
LEFT OUTER JOIN [Department] d ON d.[DepartmentID] = o.[DepartmentID]
LEFT OUTER JOIN [Item] i ON i.[ItemID] = ol.[ItemID]
WHERE  o.[DateOrdered] > @StartDate 
AND [OrderStatus] = 1
ORDER BY o.[DateOrdered]

END
GO

--Eric Bostwick 
--Created 4/2/2019
--Updated 4/6/2019
CREATE PROCEDURE [dbo].[sp_select_next_picksheet_number] 

@PickSheetNumber nvarchar(25) OUTPUT

AS
BEGIN
	if exists(select 1 from [PickSheet] where [PickSheetID] is not NULL)
	BEGIN
		--SELECT @PickSheetNumber as PickSheetNumber1
		SET @PickSheetNumber = (SELECT MAX(PickSheetNumber) + 1 FROM [PickSheet])
		--SELECT @PickSheetNumber as PickSheetNumber2
		SET @PickSheetNumber = CAST(@PickSheetNumber AS [varchar]) + HOST_ID()
		--SELECT @PickSheetNumber as PickSheetNumber3
		
	END

	ELSE
	BEGIN
		--No Pick Sheets in the pick sheet table 
		SET @PickSheetNumber = '100000' + HOST_ID()
	END
END
GO

--Eric Bostwick 
--Created 4/6/2019
--Updated 

CREATE PROCEDURE [dbo].[sp_select_picksheet_by_picksheetid] 
	
@PickSheetID char(25)

AS
BEGIN

SELECT  p.[PickSheetID], 
o.[DateOrdered] as OrderDate,
RTRIM(eo.[FirstName]) + ' ' + RTRIM(eo.[LastName]) as Orderer,
RTRIM(epc.[FirstName]) + ' ' + RTRIM(epc.[LastName]) AS PickSheetCreator, 
RTRIM(ep.[FirstName]) + ' ' + RTRIM(ep.[LastName]) AS PickedBy, 
RTRIM(ep.[FirstName]) + ' ' + RTRIM(ep.[LastName]) AS PickDeliveredBy, 
p.[CreateDate] AS PickSheetDate, p.[PickDeliveryDate] AS DeliveryDate, 
p.[PickDeliveredBy] AS DeliveredBy, 
ol.[InternalOrderID],
o.[EmployeeID],
i.[ItemID], i.[Description] as ItemDescription,
i.[Description],
ol.[OrderQty], 'EA' as OrderUM, ol.[QtyReceived], ol.[OrderReceivedDate], 
o.[DepartmentID], ol.[OrderStatus], ol.[PickCompleteDate], 
ol.[OutOfStock],
[StatusView] = CASE ol.[OrderStatus] 
WHEN 1 THEN 'ORDERED' 
WHEN 2 THEN 'ORDER ACKNOWLEDGED'
WHEN 3 THEN 'ORDER PICKED' 
WHEN '4' THEN 'ORDER DELIVERED' END,
p.[NumberofOrders]

FROM [InternalOrderLine] AS ol 
LEFT OUTER JOIN [InternalOrder] AS o on o.[InternalOrderID] = ol.[InternalOrderID]
LEFT OUTER JOIN [PickSheet] AS p ON ol.[PickSheetID] = p.[PickSheetID] 
LEFT OUTER JOIN [Employee] AS eo ON eo.[EmployeeID] = o.[EmployeeID]
LEFT OUTER JOIN [Employee] AS epc ON epc.[EmployeeID] = p.[PickSheetCreatedBy]
LEFT OUTER JOIN [Employee] as ep ON ep.[EmployeeID] = p.[PickCompletedBy]
LEFT OUTER JOIN [Employee] AS ed ON ed.[EmployeeID] = p.[PickDeliveredBy]
LEFT OUTER JOIN [Item] AS i ON i.ItemID = ol.ItemID 

WHERE ol.[PickSheetID] = @PickSheetID   
                 
END

GO

--Eric Bostwick 
--Created 4/7/2019
--Updated 4/12/2019

CREATE PROCEDURE [dbo].[sp_update_pick_order]

@ItemID int,
@InternalOrderID int,
@OrderQty int,
@QtyReceived int,
@OrderReceivedDate datetime,
@PickSheetID nvarchar(25),
@PickCompleteDate datetime,
@OutOfStock bit,
@DeliveryDate datetime,
@OrderStatus int,

@OldItemID int,
@OldInternalOrderID int,
@OldOrderQty int,
@OldQtyReceived int,
@OldOrderReceivedDate datetime,
@OldPickSheetID nvarchar(25),
@OldPickCompleteDate datetime,
@OldDeliveryDate datetime,
@OldOutOfStock bit,
@OldOrderStatus int

AS

BEGIN	

UPDATE [InternalOrderLine]
SET [QtyReceived] =  @QtyReceived,
	[PickCompleteDate] = @PickCompleteDate,
	[OutOfStock] = @OutOfStock,		
	[OrderStatus] = @OrderStatus
WHERE 
	[ItemID] = @OldItemID
	AND [InternalOrderID] = @OldInternalOrderID
	AND [OrderQty] = @OldOrderQty	
	AND [QtyReceived] = @OldQtyReceived
	AND [OrderReceivedDate] = @OldOrderReceivedDate
	AND [PickSheetID] = @OldPickSheetID
	----AND [PickCompleteDate] = @OldPickCompleteDate
	----AND [DeliveryDate] = @OldDeliveryDate
	AND [OutOfStock] = @OldOutOfStock
	AND [OrderStatus] = @OldOrderStatus

Return @@RowCount
END

GO

--Eric Bostwick 
--Created 4/8/2019
--Updated 

CREATE PROCEDURE [dbo].[sp_update_picksheet]

@PickSheetID nvarchar(25),
@PickCompletedBy int,
@PickCompletedDate datetime,
@PickDeliveredBy int,
@PickDeliveryDate datetime,
@NumberofOrders int,
@OldPickCompletedBy int,
@OldPickCompletedDate datetime,
@OldPickDeliveredBy int,
@OldPickDeliveryDate datetime,
@OldNumberofOrders int

AS

BEGIN

IF @PickCompletedBy = 0
	BEGIN
		SET @PickCompletedBy = NULL
	END
IF @PickDeliveredby = 0
	BEGIN
		SET @PickDeliveredby = NULL
	END
IF @OldPickDeliveredby = 0
	BEGIN		
		SET @OldPickDeliveredBy = NULL
	END
IF @PickCompletedby = 0
	BEGIN
		SET @PickCompletedBy = NULL
	END
IF @OldPickCompletedby = 0
	BEGIN		
		SET @OldPickCompletedBy = NULL
	END

UPDATE PickSheet 
SET 
	[PickCompletedBy] = @PickCompletedBy, 
	[PickCompletedDate] = @PickCompletedDate,
	[PickDeliveryDate] = @PickDeliveryDate,
	[PickDeliveredBy] = @PickDeliveredBy,
	[NumberofOrders] = @NumberofOrders

	WHERE [PickSheetID] = @PickSheetID	
	--AND [PickCompletedBy] = @OldPickCompletedBy	
	--AND PickDeliveredBy = @OldPickDeliveredBy
	AND [NumberofOrders] = @OldNumberofOrders

	--Select @@ROWCOUNT as rc
	
IF @@ROWCOUNT = 1 
BEGIN
	
	IF @PickDeliveredby IS NULL AND @PickCompletedBy IS NULL
		BEGIN 
			--Select '2'
			UPDATE [InternalOrderLine] SET OrderStatus = 2, [PickCompleteDate] = NULL, [DeliveryDate] = NULL WHERE [PickSheetID] = @PickSheetID
		END
	IF @PickDeliveredBy IS NULL AND @PickCompletedBy > 0
		BEGIN 
			--Select '3'
			UPDATE [InternalOrderLine] SET OrderStatus = 3, [DeliveryDate] = NULL, [PickCompleteDate] = @PickCompletedDate WHERE [PickSheetID] = @PickSheetID
		END 	
	IF @PickDeliveredby > 0
		BEGIN		
			--Select '4'	
			UPDATE [InternalOrderLine] SET OrderStatus = 4, [DeliveryDate] = @PickDeliveryDate WHERE [PickSheetID] = @PickSheetID
		END
		
END


RETURN @@ROWCOUNT
END
GO

--Eric Bostwick 
--Created 4/9/2019
--Updated 

CREATE PROCEDURE [dbo].[sp_select_all_closed_pickSheets_by_date]
	@StartDate DATETIME
AS
BEGIN
		Select p.[PickSheetID], 		
		e.[FirstName] + ' ' + e.[LastName] AS [PickCompletedByName],
		ed.[FirstName] + ' ' + ed.[LastName] AS [PickDeliveredByName],		
		p.[NumberOfOrders] as OrderCount,		
		p.[CreateDate],
		p.[PickCompletedDate], 		
		p.[PickDeliveryDate],
		ISNULL(p.[PickSheetCreatedBy],0) AS [PickSheetCreator], 
		ISNULL(p.[PickDeliveredBy], 0) AS [PickDeliveredBy],
		ISNULL(p.[PickCompletedBy], 0) AS [PickCompletedBy]
		FROM [PickSheet] as p
		Left Outer Join [Employee] as e on e.[EmployeeID] = p.[PickCompletedBy]
		Left Outer Join [Employee] as ed on ed.[EmployeeID] = p.[PickDeliveredBy]
		WHERE CreateDate >= @StartDate
		AND PickCompletedBy > 0
END
GO


-- James Heim
-- Created 2019-04-16
-- Selects the Member's only open reservation.
CREATE PROCEDURE [dbo].[sp_select_active_reservation_by_member_id]
(
	@MemberID	 [int]
)
AS
	BEGIN
		SELECT  [ReservationID], [MemberID], [NumberOfGuests], [NumberOfPets], 
				[ArrivalDate], [DepartureDate], [Notes], [Active]
		FROM 	Reservation
		WHERE 	[MemberID] = @MemberID
		  AND 	[Active] = 1
	END
GO

-- James Heim
-- Created 2019-04-04
-- Find the RoomReservation associated with a guest via the GuestRoomAssignment table.
CREATE PROCEDURE [dbo].[sp_select_roomreservation_by_guest_id]
(
	@GuestID		[int]
)
AS
	BEGIN
		SELECT	RoomReservation.RoomReservationID, 
				RoomReservation.RoomID, 
				RoomReservation.ReservationID, 
				RoomReservation.CheckInDate, RoomReservation.CheckOutDate
		FROM	GuestRoomAssignment
			INNER JOIN RoomReservation ON GuestRoomAssignment.RoomReservationID = RoomReservation.RoomReservationID
		WHERE	GuestRoomAssignment.GuestID = @GuestID
	END
GO

-- James Heim
-- Created 2019-04-17
-- Select all rooms and the number of guests assigned to that room so long that they 
-- are on the supplied ReservationID and they have not reached capacity.
CREATE PROCEDURE [dbo].[sp_select_room_roomreservation_viewmodel_by_reservation_id]
(
    @ReservationID [int]
)
AS
    BEGIN
    
    WITH CTE (RoomReservationID, RoomID, RoomNumber, BuildingName, CurrentlyAssigned, Capacity, CheckInDate, CheckOutDate)
    AS
    (
		SELECT RoomReservation.RoomReservationID,
				Room.RoomID, 
				Room.RoomNumber,
				Building.BuildingName, 
				(SELECT COUNT(GuestID) 
				 FROM GuestRoomAssignment GRA
				 WHERE RoomReservationID = RoomReservation.RoomReservationID) 
				 AS CurrentlyAssigned,
				 Room.Capacity,
				RoomReservation.CheckInDate,
				RoomReservation.CheckOutDate

		FROM	RoomReservation 
				LEFT JOIN GuestRoomAssignment ON GuestRoomAssignment.RoomReservationID = RoomReservation.RoomReservationID
				INNER JOIN Room ON RoomReservation.RoomID = Room.RoomID
				INNER JOIN Building ON Room.BuildingID = Building.BuildingID

		WHERE RoomReservation.ReservationID = @ReservationID
    )
    
    SELECT  RoomReservationID,
			RoomNumber,		
			BuildingName, 
			CurrentlyAssigned, 
			CheckInDate, 
			CheckOutDate
	FROM CTE
	GROUP BY RoomReservationID, CurrentlyAssigned, Capacity, RoomID, 
				 RoomNumber, BuildingName,
				 CheckInDate, CheckOutDate
		HAVING CurrentlyAssigned  < Capacity
            
            
    END
GO

-- James Heim
-- Created 2019-04-04
CREATE PROCEDURE [dbo].[sp_assign_room_reservation]
(
	@GuestID 			[int],
	@RoomReservationID 	[int]
)
AS
	BEGIN
		INSERT INTO GuestRoomAssignment([GuestID], [RoomReservationID])
			VALUES
			(@GuestID, @RoomReservationID)
	END
GO


-- James Heim
-- Created 2019-04-04
CREATE PROCEDURE [dbo].[sp_unassign_room_reservation]
(
	@GuestID 			[int],
	@RoomReservationID 	[int]
)
AS
	BEGIN
		DELETE 
		FROM GuestRoomAssignment
		WHERE GuestID = @GuestID
		  AND RoomReservationID = @RoomReservationID
		  
		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- Created 2019-04-11
-- Set CheckInDate to now.
CREATE PROCEDURE [dbo].[sp_update_checkindate]
(
	@RoomReservationID [int]
)
AS
	BEGIN
		UPDATE RoomReservation
			SET CheckInDate = GETDATE()
			
			WHERE RoomReservationID = @RoomReservationID
		RETURN @@ROWCOUNT
	END
GO


-- James Heim
-- Created 2019-04-18
-- Select the MemberTab by its MemberTabID
CREATE PROCEDURE [dbo].[sp_select_membertab_by_id]
(
	@MemberTabID [int]
)
AS
	BEGIN
		SELECT MemberTabID, MemberID, Active, TotalPrice
		FROM MemberTab
		WHERE MemberTabID = @MemberTabID
	END
GO

-- James Heim
-- Created 2019-04-18
-- Select the only active MemberTab by MemberID.
CREATE PROCEDURE [dbo].[sp_select_active_membertab_by_member_id]
(
	@MemberID [int]
)
AS
	BEGIN
		SELECT MemberTabID, MemberID, Active, TotalPrice
		FROM MemberTab
		WHERE MemberID = @MemberID
		  AND Active = 1
	END
GO

-- James Heim
-- Created 2019-04-18
-- Create a MemberTab if no MemberTab is active for the specified MemberID.
CREATE PROCEDURE [dbo].[sp_insert_membertab]
(
	@MemberID 	[int]
)
AS
	DECLARE @MemberTabActive [int] = 0
	BEGIN
		SET @MemberTabActive = 
			(SELECT COUNT(*) 
			FROM [MemberTab] 
			WHERE [MemberID] = @MemberID 
			AND [Active] = 1)

		IF @MemberTabActive = 0
			INSERT INTO [MemberTab]
				([MemberID], [Active], [TotalPrice])
			VALUES
				(@MemberID, 1, 0)
		ELSE 
			-- Error Severity 16 Indicates general errors that can be corrected by the user.
			RAISERROR ('Active MemberTab Already Exists', 16, 0 )
	END
GO

-- James Heim
-- Created 2019-04-18
-- Select the only active MemberTab by MemberID.
-- This should only work if all guests are checked out.
CREATE PROCEDURE [dbo].[sp_update_set_inactive_membertab_by_id]
(
	@MemberTabID [int]
)
AS
	BEGIN
		UPDATE [MemberTab]
		SET [Active] = 0
		WHERE MemberTabID = @MemberTabID
		  AND Active = 1
	END
GO


/* Author: Carlos Arzu Created Date: 2/08/19 */
CREATE PROCEDURE [sp_read_Special_orders_by_ID]
	(
		@SpecialOrderID		[int]	
	)
AS
	BEGIN
		SELECT [SpecialOrderID],[EmployeeID],[Description],
			   [DateOrdered], [Supplier] 
		FROM   SpecialOrder
		WHERE  [SpecialOrderID] = @SpecialOrderID
	END
GO

CREATE PROCEDURE [dbo].[sp_count_Special_order]
	
AS
	BEGIN
		SELECT COUNT([SpecialOrderID])
		FROM [dbo].[SpecialOrder]
	END
GO

/* Author: Carlos Arzu Created Date: 2/08/19 */
CREATE PROCEDURE [sp_retrieve_List_of_SpecialOrderLine_by_SpecialOrderID]
	(
		@SpecialOrderID		[int]	
	)
AS
	BEGIN
		SELECT  [NameID],[SpecialOrderID], [Description], [OrderQty], 
		        [QtyReceived]
		FROM   SpecialOrderLine
		WHERE  [SpecialOrderID] = @SpecialOrderID
	END
GO

/* Author: Carlos Arzu Created Date: 2/18/19 */

CREATE PROCEDURE [dbo].[sp_search_Special_orders]
	(
		@SearchTerm		[nvarchar](100)
	)
AS
	BEGIN
		SELECT [SpecialOrderID],[EmployeeID],[Description],
			   [DateOrdered], [Supplier] 
		FROM  [SpecialOrder]
		WHERE [Description] LIKE '%' + @SearchTerm + '%'
	END
GO

/* Author: Carlos Arzu Created Date: 4/6/19 */

CREATE PROCEDURE [dbo].[sp_delete_Item_from_SpecialOrder]
	(
		@SpecialOrderID  [int],
		@NameID	     	 [nvarchar](100)
	)
AS
	BEGIN
		DELETE
		FROM  [SpecialOrderLine]
		WHERE [SpecialOrderID] = @SpecialOrderID 
		AND [NameID] = @NameID
	END
GO

/* Author: Carlos Arzu Created Date: 4/10/19 */
CREATE PROCEDURE [dbo].[sp_retrieve_last_specialOrderID_created]
	(
		@EmployeeID    	    [int],
		@Description    	[nvarchar](1000),
		@DateOrdered 		[DateTime],
		@Supplier			[nvarchar](1000)
	)
AS
	BEGIN
		SELECT    [SpecialOrderID] 
		FROM      [SpecialOrder]
		WHERE     [EmployeeID]     = @EmployeeID
			  AND [Description]    = @Description
			  AND [DateOrdered]    = @DateOrdered
			  AND [Supplier]     = @Supplier
	END
GO

/* Author: Carlos Arzu Created Date: 4/6/19 */

CREATE PROCEDURE [dbo].[sp_update_AuthenticatedBy]
	(
		@SpecialOrderID  [int],
		@Authorized    	 [nvarchar](100)
	)
AS
	BEGIN
		UPDATE    [SpecialOrder]
			SET   [Authorized] = @Authorized 
			FROM  [SpecialOrder]
			WHERE [SpecialOrderID] = @SpecialOrderID 
			
			RETURN @@ROWCOUNT
	END
GO



/* Author: Carlos Arzu Created Date: 1/26/19 */
CREATE PROCEDURE [dbo].[sp_create_SpecialOrder]
	(
		
		@EmployeeID    	    [int],
		@Description    	[nvarchar](1000),
		@DateOrdered 		[DateTime],
		@Supplier			[nvarchar](1000)

	)
AS
	BEGIN	
		
		INSERT INTO [dbo].[SpecialOrder]
			( [EmployeeID],  [Description],
			 [DateOrdered], [Supplier])
		VALUES
			( @EmployeeID, @Description, 
			 @DateOrdered, @Supplier)
				
		RETURN @@ROWCOUNT	

	END
GO	

/* Author: Carlos Arzu Created Date: 1/26/19 */
CREATE PROCEDURE [dbo].[sp_create_SpecialOrderLine]
	(
		@NameID   	        [nvarchar](1000),
		@SpecialOrderID     [int],
		@Description    	[nvarchar](1000),
		@OrderQty  	        [int],
		@QtyReceived 		[int]
			
	)
AS
	BEGIN	
					
		INSERT INTO [dbo].[SpecialOrderLine]
			( [NameID], [SpecialOrderID], [Description], [OrderQty], 
			 [QtyReceived])
		VALUES
			(@NameID, @SpecialOrderID, @Description, @OrderQty,
			 @QtyReceived)
		
		RETURN @@ROWCOUNT	

	END
GO

/* Author: Carlos Arzu Created Date: 1/26/19 */
CREATE PROCEDURE [dbo].[sp_retrieve_all_Special_order]
	
AS
	BEGIN
				
		SELECT       *
		FROM 	   [SpecialOrder] 
	END	
GO	

/* Author: Carlos Arzu Created Date: 2/06/19 */
CREATE PROCEDURE [dbo].[sp_update_SpecialOrder]
	(
		@SpecialOrderID    	[int],
		
		@EmployeeID  			[int],
		@Description      		[nvarchar](1000),
		@Supplier				[nvarchar](1000),
		
		@OldEmployeeID  		[int],
		@OldDescription     	[nvarchar](1000),
		@OldSupplier			[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE		[SpecialOrder]
			SET		[EmployeeID]  		=	@EmployeeID,  
					[Description]      	=	@Description,      
					[Supplier]        =	@Supplier
			FROM	[dbo].[SpecialOrder]
			WHERE	[SpecialOrderID]   =   @SpecialOrderID	
			  AND   [EmployeeID]  	    =	@OldEmployeeID  
			  AND   [Description]      	=	@OldDescription 
              AND	[Supplier]        =	@OldSupplier
			  
			RETURN @@ROWCOUNT
    END
GO	

/* Author: Carlos Arzu Created Date: 4/05/19 */
CREATE PROCEDURE [dbo].[sp_update_SpecialOrderLine]
	(
		@SpecialOrderID    	[int],
		
		@NameID  		    	[nvarchar](1000),
		@Description      		[nvarchar](1000),
		@OrderQty               [int],
		@QtyReceived			[int],
		
		@OldNameID  	    	[nvarchar](1000),
		@OldDescription     	[nvarchar](1000),
		@OldOrderQty            [int],   
		@OldQtyReceived			[int]
	)
AS
	BEGIN
		UPDATE		[SpecialOrderLine]
			SET		[NameID]  		    =	@NameID,  
					[Description]      	=	@Description,      
					[OrderQty]          =	@OrderQty,                  
					[QtyReceived]       =	@QtyReceived
			FROM	[dbo].[SpecialOrderLine]
			WHERE	[SpecialOrderID]    =   @SpecialOrderID	
			  AND   [NameID]  	        =	@OldNameID 
			  AND   [Description]      	=	@OldDescription 
              AND   [OrderQty]          =	@OldOrderQty		  
			  AND	[QtyReceived]        =	@OldQtyReceived
			  
			RETURN @@ROWCOUNT
    END
GO	

/* 	Created By:  Dani Russo 
	Created: 04/11/2019
	
	Does not create an offering when new room is created
*/
CREATE PROCEDURE [dbo].[sp_insert_room_with_no_price]
	(
		@RoomNumber			[int],
		@BuildingID			[nvarchar](50),
		@RoomTypeID			[nvarchar](15),
		@Description		[nvarchar](1000),
		@Capacity			[int],
		@RoomStatusID		[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO [ResortProperty]
			([ResortPropertyTypeID])
		VALUES
			('Room')
		DECLARE @NewResortProperyID [int] = (SELECT @@IDENTITY)
		
		
		INSERT INTO [dbo].[Room]
			([RoomNumber], [BuildingID], [RoomTypeID], [Description], [Capacity], [RoomStatusID], [ResortPropertyID])
		VALUES
			(@RoomNumber, @BuildingID, @RoomTypeID, @Description, @Capacity, @RoomStatusID, @NewResortProperyID)	
		RETURN @@ROWCOUNT
	END
GO 

/* 	Created By:  Dani Russo 
	Created: 04/11/2019
	
*/
CREATE PROCEDURE [dbo].[sp_update_room]
	(
		@RoomID				[int],
		@OfferingID			[int],
		
		@OldRoomNumber			[int],
		@OldBuildingID			[nvarchar](50),
		@OldDescription			[nvarchar](1000),
		@OldRoomTypeID			[nvarchar](15),
		@OldRoomStatusID		[nvarchar](25),
		@OldPrice				[Money],
		@OldCapacity			[int],
		
		@NewRoomNumber			[int],
		@NewBuildingID			[nvarchar](50),
		@NewDescription			[nvarchar](1000),
		@NewRoomTypeID			[nvarchar](15),
		@NewRoomStatusID		[nvarchar](25),
		@NewPrice				[Money],
		@NewCapacity			[int]
		
	)
AS
	BEGIN
		UPDATE 	[Room]
			SET	[RoomNumber] = @NewRoomNumber,
				[BuildingID] = @NewBuildingID,
				[Description] = @NewDescription,
				[RoomStatusID] = @NewRoomStatusID,
				[RoomTypeID] = @NewRoomTypeID,
				[Capacity] = @NewCapacity
		WHERE 	[RoomID] = @RoomID
		AND		[RoomNumber] = @OldRoomNumber
		AND		[BuildingID] = @OldBuildingID
		AND		[Description] = @OldDescription
		AND		[RoomStatusID] = @OldRoomStatusID
		AND		[RoomTypeID] = @OldRoomTypeID
		AND		[Capacity] = @OldCapacity
			
		UPDATE	[Offering]
			SET	[Price] = @NewPrice
		WHERE	[OfferingID] = @OfferingID
		AND		[Price] = @OldPrice
		
		RETURN @@ROWCOUNT
	END
GO


-- AUTHOR  : Francis Mingomba
-- CREATED : 2019/04/15
CREATE PROCEDURE [dbo].[sp_delete_resort_property_type_by_id] 
	(
		@ResortPropertyTypeId		[nvarchar](20)
	)
AS
	BEGIN
		DELETE
		FROM	[ResortPropertyType]
		WHERE	[ResortPropertyTypeId] = @ResortPropertyTypeId

		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- 2019-04-24
-- Activate a Member's Tab where the tab is deactivated.
CREATE PROCEDURE [dbo].[sp_update_set_active_membertab_by_id]
(
	@MemberTabID [int]
)
AS
	BEGIN
		UPDATE [MemberTab]
		SET [Active] = 1
		WHERE MemberTabID = @MemberTabID
		  AND Active = 0
		  
		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- 2019-04-24
-- Delete a Member's Tab where the tab is deactivated.
CREATE PROCEDURE [dbo].[sp_delete_membertab_by_id]
(
	@MemberTabID [int]
)
AS
	BEGIN
		DELETE FROM [MemberTab]
		WHERE MemberTabID = @MemberTabID
		  AND Active = 0
		  
		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- 2019-04-24
-- Insert a TabLine on a Member's Tab.
CREATE PROCEDURE [dbo].[sp_insert_membertabline]
(
	@MemberTabLineID [int] OUTPUT,

	@MemberTabID	[int],
	@OfferingID		[int], 
	@Quantity		[int],
	@Price			[money],
	@EmployeeID		[int],
	@Discount		[decimal],
	@GuestID		[int],
	@DatePurchased	[DATE]
	
)
AS
	BEGIN
		INSERT INTO [MemberTabLine]
				([MemberTabID], [OfferingID], [Quantity], [Price], [EmployeeID],
				 [Discount], [GuestID], [DatePurchased])
			VALUES
				(@MemberTabID, @OfferingID, @Quantity, @Price, @EmployeeID, 
				 @Discount, @GuestID, @DatePurchased)
				 
		SET @MemberTabLineID = SCOPE_IDENTITY()

		RETURN SCOPE_IDENTITY()
	END
GO

-- James Heim
-- 2019-04-24
-- Select the tablines for the specified Tab.
CREATE PROCEDURE [dbo].[sp_select_membertablines]
(
	@MemberTabID [int]
)
AS
	BEGIN
		SELECT [MemberTabLineID], [MemberTabID], [OfferingID], [Quantity], 
				[Price], [EmployeeID], [Discount], [GuestID], [DatePurchased]
		FROM [MemberTabLine]
		WHERE [MemberTabID] = @MemberTabID
	END
GO

-- James Heim
-- 2019-04-24
-- Select the details of a specific tabline.
CREATE PROCEDURE [dbo].[sp_select_membertabline]
(
	@MemberTabLineID [int]
)
AS
	BEGIN
		SELECT [MemberTabLineID], [MemberTabID], [OfferingID], [Quantity], 
				[Price], [EmployeeID], [Discount], [GuestID], [DatePurchased]
		FROM [MemberTabLine]
		WHERE [MemberTabLineID] = @MemberTabLineID
	END
GO

-- James Heim
-- 2019-04-24
-- Delete a TabLine.
CREATE PROCEDURE [dbo].[sp_delete_membertabline]
(
	@MemberTabLineID [int]
)
AS
	BEGIN
		DELETE FROM [MemberTabLine]
		WHERE [MemberTabLineID] = @MemberTabLineID
		
		RETURN @@ROWCOUNT
	END
GO


-- James Heim
-- 2019-04-25
-- Select all MemberTabs, active or otherwise.
CREATE PROCEDURE [dbo].[sp_select_membertabs]
AS
	BEGIN	
		SELECT MemberTabID, MemberID, Active, TotalPrice
		FROM MemberTab
	END
GO

-- James Heim
-- Created 2019-04-25
-- Select all Tabs the member has ever had.
CREATE PROCEDURE [dbo].[sp_select_membertabs_by_member_id]
(
	@MemberID [int]
)
AS
	BEGIN
		SELECT MemberTabID, MemberID, Active, TotalPrice
		FROM MemberTab
		WHERE MemberID = @MemberID
	END
GO

/*
 * Created: 2019/04/26
 * By: Richard Carroll
 * Retrieves All Generated SupplierOrders in the supplier order table
*/
/*
 * Updated: 2019/05/03
 * By: Richard Carroll
 * Retrieves All Generated SupplierOrders in the supplier order table
*/
CREATE Procedure [dbo].[sp_select_all_generated_orders]
As
    BEGIN
        SELECT [so].[SupplierOrderID], [so].[SupplierID], [s].[Name] AS SupplierName, [so].[EmployeeID], [e].[FirstName], [e].[LastName], [so].[Description],
        [so].[DateOrdered], [so].[OrderComplete]
        FROM [SupplierOrder] so INNER JOIN [Employee] e ON [so].[EmployeeID] = [e].[EmployeeID]
		INNER JOIN [Supplier] s ON [s].[SupplierID] = [so].[SupplierID]
		Where [so].[EmployeeID] = 1
    END
GO

/*
 * Created: 2019/04/26
 * By: Richard Carroll
 * Adds an employeeID to a generated order, approving it.
*/

CREATE PROCEDURE [dbo].[sp_update_generated_order]
(
    @SupplierOrderID     [int],
    @EmployeeID			 [int]
)
AS 
    BEGIN
        Update SupplierOrder
        Set EmployeeID = @EmployeeID
        Where SupplierOrderID = @SupplierOrderID
        And EmployeeID = 1
        Return @@ROWCOUNT
    END
GO

/* 	Created By:  Danielle Russo 
	Created: 02/27/2019
	Updated: 
*/ 
CREATE PROCEDURE [dbo].[sp_update_inspection]
	(
		@ResortPropertyID					[int],
		@InspectionID						[int],
		
		@OldName							[nvarchar](50),
		@OldDateInspected					[date],
		@OldRating							[nvarchar](50),
		@OldResortInspectionAffiliation		[nvarchar](25),
		@OldInspectionProblemNotes			[nvarchar](1000),
		@OldInspectionFixNotes				[nvarchar](1000),

		@NewName							[nvarchar](50),
		@NewDateInspected					[date],
		@NewRating							[nvarchar](50),
		@NewResortInspectionAffiliation		[nvarchar](25),
		@NewInspectionProblemNotes			[nvarchar](1000),
		@NewInspectionFixNotes				[nvarchar](1000)		
	)
AS
	BEGIN
		
		UPDATE 	[Inspection]
		
		SET		[Name] = @NewName,
				[DateInspected] = @NewDateInspected,
				[Rating] = @NewRating,
				[ResortInspectionAffiliation] = @NewResortInspectionAffiliation,
				[InspectionProblemNotes] = @NewInspectionProblemNotes,
				[InspectionFixNotes] = @NewInspectionFixNotes
		
		WHERE	[ResortPropertyID] = @ResortPropertyID
		AND		[InspectionID] = @InspectionID
		AND		[Name] = @OldName
		AND		[DateInspected] = @OldDateInspected
		AND		[Rating] = @OldRating
		AND		[ResortInspectionAffiliation] = @OldResortInspectionAffiliation
		AND		[InspectionProblemNotes] = @OldInspectionProblemNotes
		AND		[InspectionFixNotes] = @OldInspectionFixNotes
		
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
CREATE PROCEDURE sp_create_work_order
	(
	@MaintenanceTypeID				[nvarchar](50),
	@DateRequested					[Date],
	@RequestingEmployeeID			[int],
	@WorkingEmployeeID				[int],
	@Description					[nvarchar](1000),
	@MaintenanceStatus			[nvarchar](50),
	@ResortPropertyID				[int]
	)
AS
	BEGIN
		INSERT INTO [MaintenanceWorkOrder]
			([MaintenanceTypeID], [DateRequested], [RequestingEmployeeID], 
			[WorkingEmployeeID], [Description], [MaintenanceStatus], [ResortPropertyID])
		VALUES
			(@MaintenanceTypeID, @DateRequested, @RequestingEmployeeID, 
			@WorkingEmployeeID, @Description, @MaintenanceStatus, @ResortPropertyID )
	  
		RETURN @@ROWCOUNT
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/

CREATE PROCEDURE [dbo].[sp_select_all_work_orders] 
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/
CREATE PROCEDURE [dbo].[sp_select_work_order_by_id] 
(
	@MaintenanceWorkOrderID		[int]
)
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
		WHERE [MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
	END
GO


/* 
Created By: Dalton Cleveland
Date: 02-20-2019
*/
CREATE PROCEDURE [dbo].[sp_select_work_order_by_type] 
(
	@MaintenanceTypeID		[nvarchar](50)
)
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
		WHERE [MaintenanceTypeID] = @MaintenanceTypeID
	END
GO

/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
CREATE PROCEDURE [dbo].[sp_update_work_order]
	(
	@MaintenanceWorkOrderID				[int],
	
	@oldMaintenanceTypeID				[nvarchar](50),
	@oldDateRequested					[Date],
	@oldRequestingEmployeeID			[int],
	@oldWorkingEmployeeID				[int],
	@oldDescription						[nvarchar](1000),
	@oldMaintenanceStatusID				[nvarchar](50),
	@oldResortPropertyID				[int],
	@oldComplete						[bit],

	@newMaintenanceTypeID				[nvarchar](50),
	@newRequestingEmployeeID			[int],
	@newWorkingEmployeeID				[int],
	@newDescription						[nvarchar](1000),
	@newComments 						[nvarchar](1000),
	@newMaintenanceStatusID				[nvarchar](50),
	@newResortPropertyID				[int],
	@newComplete						[bit]
	)
AS
	BEGIN
		UPDATE [MaintenanceWorkOrder]
			SET [MaintenanceTypeID] = @newMaintenanceTypeID,
				[RequestingEmployeeID] = @newRequestingEmployeeID,
				[WorkingEmployeeID] = @newWorkingEmployeeID,
				[Description] = @newDescription,
				[Comments] = @newComments,
				[MaintenanceStatus] = @newMaintenanceStatusID,
				[ResortPropertyID] = @newResortPropertyID,
				[Complete] = @newComplete
			WHERE 	
				[MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
			AND [MaintenanceTypeID] = @oldMaintenanceTypeID
			AND	[DateRequested] = @oldDateRequested
			AND	[RequestingEmployeeID] = @oldRequestingEmployeeID
			AND	[WorkingEmployeeID] = @oldWorkingEmployeeID
			AND	[Description] = @oldDescription
			AND	[MaintenanceStatus] = @oldMaintenanceStatusID
			AND	[ResortPropertyID] = @oldResortPropertyID
			AND	[Complete] = @oldComplete 	
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/
CREATE PROCEDURE [dbo].[sp_deactivate_work_order]
	(
		@MaintenanceWorkOrderID			[int]
	)
AS
	BEGIN
		UPDATE [MaintenanceWorkOrder]
			SET [Complete] = 0
			WHERE 	
				[MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
		RETURN @@ROWCOUNT
	END
GO


/* 
Created By: Dalton Cleveland
Date: 02-13-2019
*/

CREATE PROCEDURE [dbo].[sp_delete_work_order]
	(
		@MaintenanceWorkOrderID 				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [MaintenanceWorkOrder]
		WHERE  [MaintenanceWorkOrderID] = @MaintenanceWorkOrderID
		RETURN @@ROWCOUNT
	END
GO

/*
Created By: Dalton Cleveland
Date:  2-13-201
*/

CREATE PROCEDURE [dbo].[sp_select_all_view_model_work_orders]
AS
	BEGIN
		SELECT [MaintenanceWorkOrder].[MaintenanceWorkOrderID],
		[MaintenanceWorkOrder].[MaintenanceTypeID],
		[MaintenanceWorkOrder].[DateRequested],
		[MaintenanceWorkOrder].[DateCompleted],
		[MaintenanceWorkOrder].[RequestingEmployeeID],
		[MaintenanceWorkOrder].[WorkingEmployeeID],
		[MaintenanceWorkOrder].[Description],
		[MaintenanceWorkOrder].[Comments], 
		[MaintenanceWorkOrder].[Complete],
		[MaintenanceStatus].[MaintenanceStatusID], 
		[MaintenanceStatus].[Description], 
		[MaintenanceType].[MaintenanceTypeID], 
		[MaintenanceType].[Description]
		FROM MaintenanceWorkOrder 
		INNER JOIN MaintenanceType ON MaintenanceWorkOrder.MaintenanceTypeID = MaintenanceType.MaintenanceTypeID
		INNER JOIN MaintenanceStatus ON MaintenanceWorkOrder.MaintenanceStatus = MaintenanceStatus.MaintenanceStatusID
		

	END
GO

-- Created By: Matt Hill
-- Created On: 2019-04-25
-- Retrieves a member tab line
CREATE PROCEDURE [dbo].[sp_select_member_tab_line_by_member_tab_id]
	(
		@MemberTabID	[int]
	)
AS
	BEGIN
		SELECT [MemberTabLineID], [MemberTabID], [Offering].[OfferingTypeID], [Offering].[Description], [Quantity], [MemberTabLine].[Price], [MemberTabLine].[EmployeeID], [GuestID], [DatePurchased]
		FROM [dbo].[MemberTabLine] 
		INNER JOIN [Offering] ON [MemberTabLine].[OfferingID] = [Offering].[OfferingID]
		WHERE [MemberTabID] = @MemberTabID
		AND [Offering].[OfferingID] = [MemberTabLine].[OfferingID]
	END
GO

CREATE PROCEDURE [dbo].[sp_get_thread_messages]
(
	@ThreadID [int]
)
AS
	BEGIN
		
		SELECT [MessageID],[SenderEmail],[DateTimeSent],[Subject],[Body]
		FROM [dbo].[Message]
		WHERE [ThreadID] = @ThreadID
	
	END
GO

CREATE PROCEDURE [dbo].[sp_new_message]
(
	@ThreadID [int] OUTPUT,
	@SenderEmail [nvarchar](250),
	@DateTimeSent [datetime],
	@Subject [nvarchar](100),
	@Body [nvarchar](1000)
)
AS
	BEGIN
		
		INSERT INTO [MessageThread]
		([Archived])
		VALUES
		('0')

		SET @ThreadID = @@IDENTITY
		
		INSERT INTO [Message]
		([SenderEmail], [DateTimeSent], [Subject], [Body], [ThreadID])
		VALUES
		(@SenderEmail, @DateTimeSent, @Subject, @Body, @ThreadID)
		
		DECLARE @MessageID [int] = @@IDENTITY

		UPDATE [MessageThread]
		SET [FirstMessage] = @MessageID
		WHERE [ThreadID] = @ThreadID

		SELECT @ThreadID

	END
GO

CREATE PROCEDURE [dbo].[sp_select_newest_thread_message]
(
	@ThreadID [int]
)
AS
	BEGIN
		SELECT [MessageID],[SenderEmail],[DateTimeSent],[Subject],[Body]
		FROM [dbo].[Message]
		WHERE [ThreadID] = @ThreadID
		AND [DateTimeSent] = (SELECT MAX(DateTimeSent) FROM [dbo].[Message] WHERE [ThreadID] = @ThreadID)
	END
GO

CREATE PROCEDURE [dbo].[sp_new_reply]
(
	@ThreadID [int],
	@SenderEmail [nvarchar](250),
	@DateTimeSent [datetime],
	@Subject [nvarchar](100),
	@Body [nvarchar](1000)
)
AS
	BEGIN
		
		INSERT INTO [Message]
		([ThreadID], [SenderEmail], [DateTimeSent], [Subject], [Body])

		VALUES
		(@ThreadID, @SenderEmail, @DateTimeSent, @Subject, @Body)

		DECLARE @Output [int] = @@ROWCOUNT

		UPDATE [ThreadParticipant]
		SET [HasUnreadMessages] = '1'
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] != @SenderEmail
		AND [SilenceNewMessages] != '1'

		RETURN @Output
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_thread_archived_state]
(
	@ThreadID [int],
	@Archived [bit]
)
AS
	BEGIN
		
		UPDATE [MessageThread]
		SET [Archived] = @Archived
		WHERE [ThreadID] = @ThreadID

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_participant_thread_hidden_state]
(
	@ThreadID [int],
	@Hidden [bit],
	@ParticipantEmail [nvarchar](250)
)  
AS
	BEGIN
		
		UPDATE [ThreadParticipant]
		SET [HideThread] = @Hidden
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @ParticipantEmail

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_participant_thread_alert_user_new_messages]
(
	@ThreadID [int],
	@AlertStatus [bit],
	@ParticipantEmail [nvarchar](250)
)
AS
	BEGIN
	
		UPDATE [ThreadParticipant]
		SET [SilenceNewMessages] = @AlertStatus
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @ParticipantEmail

		RETURN @@ROWCOUNT
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_thread_data_by_participant_email]
(
	@Email [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
	
		SELECT [ParticipantAlias],[SilenceNewMessages],[HasUnreadMessages],[HideThread]
		FROM [ThreadParticipant]
		WHERE [ParticipantEmail] = @Email
		AND [ThreadID] = @ThreadID
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_small_threads_by_participant_email_and_archived]
(
	@Email [nvarchar](250),
	@Archived [bit]
)
AS
	BEGIN
	
		SELECT [HasUnreadMessages],[HideThread],[Archived],[ParticipantAlias],[Message].[SenderEmail],[Message].[Subject],[Message].[DateTimeSent], [ThreadParticipant].[ThreadID]
		FROM [ThreadParticipant] INNER JOIN [MessageThread]
			ON [ThreadParticipant].[ThreadID] = [MessageThread].[ThreadID]
			INNER JOIN [Message]
			ON [MessageThread].[FirstMessage] = [Message].[MessageID]
		WHERE [ParticipantEmail] = @Email
		AND [Archived] = @Archived
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_thread_participant_has_new_messages]
(
	@Email [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		SELECT [HasUnreadMessages]
		FROM [ThreadParticipant]
		WHERE [ParticipantEmail] = @Email
		AND [ThreadID] = @ThreadID
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_thread_participant_alias]
(
	@Email [nvarchar](250),
	@ThreadID [int],
	@NewAlias [nvarchar](250)
)
AS
	BEGIN
	
		UPDATE [ThreadParticipant]
		SET [ParticipantAlias] = @NewAlias
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @Email

		RETURN @@ROWCOUNT
	
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_all_thread_participants]
(
	@ThreadID [int]
)
AS
	BEGIN
	
		SELECT [ParticipantEmail], [ParticipantAlias]
		FROM [ThreadParticipant]
		WHERE [ThreadID] = @ThreadID
	
	END
GO

CREATE PROCEDURE [dbo].[sp_delete_participant_from_thread]
(
	@Email [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN

		DELETE FROM [ThreadParticipant]
		WHERE [ParticipantEmail] = @Email
		AND [ThreadID] = @ThreadID

		RETURN @@ROWCOUNT		
		
	END
GO

CREATE PROCEDURE [dbo].[sp_update_thread_new_messages_status_for_participant]
(
	@ThreadID [int],
	@Email [nvarchar](250)
)
AS
	BEGIN
		
		UPDATE [ThreadParticipant]
		SET [HasUnreadMessages] = '0'
		WHERE [ThreadID] = @ThreadID
		AND [ParticipantEmail] = @Email
		
		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_employee_to_thread]
(
	@EmployeeEmail [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		VALUES
		(@ThreadID,@EmployeeEmail,@EmployeeEmail)

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_member_to_thread]
(
	@MemberEmail [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		VALUES
		(@ThreadID,@MemberEmail,@MemberEmail)

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_guest_to_thread]
(
	@GuestEmail [nvarchar](250),
	@ThreadID [int]
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		VALUES
		(@ThreadID,@GuestEmail,@GuestEmail)

		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_employee_participants_by_role]
(
	@ThreadID [int],
	@RoleID [nvarchar](250)
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		SELECT @ThreadID,[Employee].[Email],@RoleID
		FROM [Employee] INNER JOIN [EmployeeRole]
			ON [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
		WHERE [RoleID]= @RoleID
		
		RETURN @@ROWCOUNT
		
	END
GO

CREATE PROCEDURE [dbo].[sp_add_employee_participants_by_department]
(
	@ThreadID [int],
	@DepartmentID [nvarchar](50)
)
AS
	BEGIN
		
		INSERT INTO [ThreadParticipant]
		([ThreadID],[ParticipantEmail],[ParticipantAlias])
		SELECT @ThreadID,[Email],@DepartmentID
		FROM [Employee]
		WHERE [DepartmentID] = @DepartmentID
		
		RETURN @@ROWCOUNT
		
	END
GO

/*

 NAME:  Eduardo Colon
 Date:   2019-04-23
-- May be redundant and deleted later. */

CREATE PROCEDURE [dbo].[sp_appointment_type_by_id]
AS
	BEGIN
		SELECT [AppointmentTypeID]
		FROM AppointmentType
	END
GO

/* Eduardo Colon Script 
   Date: 2019-04-23 
*/



CREATE PROCEDURE [dbo].[sp_retrieve_appointments]
AS
	BEGIN
		SELECT [AppointmentID], [AppointmentTypeID], [GuestID], 
		[StartDate], [EndDate], [Description]
		FROM Appointment
	END
GO




/*  Created By: Eduardo Colon
  Date:   2019-04-23
*/
GO
CREATE PROCEDURE [dbo].[sp_retrieve_guest_appointment_info]

AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[Email]
		FROM [Guest]
	END
GO

/*  Created By: Eduardo Colon
  Date:   2019-04-23
*/
CREATE PROCEDURE [dbo].[sp_retrieve_guest_appointment_info_by_id]
(
	@GuestID [int]
)
AS
	BEGIN
		SELECT [GuestID],[FirstName],[LastName],[Email]
		FROM [Guest]
		WHERE [GuestID] = @GuestID
	END
GO

-- Created By: Jared Greenfield
-- Created On: 2019-04-25
-- Selects currently inhabited reservations
CREATE PROCEDURE [dbo].[sp_select_all_active_reservationvms]
AS
	BEGIN
		SELECT DISTINCT [Reservation].[ReservationID],
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
		INNER JOIN [RoomReservation] ON [Reservation].[ReservationID] = [RoomReservation].[ReservationID]
		WHERE [RoomReservation].[CheckInDate] IS NOT NULL AND 
			[RoomReservation].[CheckOutDate] IS NULL
	END

GO

-- Created By: Jared Greenfield
-- Created On: 2019-04-25
-- Selects room assignment view models by a provided reservation ID
CREATE PROCEDURE [dbo].[sp_select_all_guestroomassignvms_by_reservationID]
(
	@ReservationID [int]
)
AS
	BEGIN
		SELECT DISTINCT
			[Guest].[GuestID],
			[Guest].[FirstName],
			[Guest].[LastName],
			[Building].[BuildingName],
			[Room].[RoomNumber],
			[GuestRoomAssignment].[RoomReservationID],
			[GuestRoomAssignment].[CheckinDate],
			[GuestRoomAssignment].[CheckoutDate]
			FROM [Reservation]
			INNER JOIN [RoomReservation] ON [RoomReservation].[ReservationID] = @ReservationID
			INNER JOIN [Room] ON [Room].[RoomID] = [RoomReservation].[RoomID]
			INNER JOIN [Building] ON [Building].[BuildingID] = [Room].[BuildingID]
			INNER JOIN [GuestRoomAssignment] ON [RoomReservation].[RoomReservationID] = [GuestRoomAssignment].[RoomReservationID]
			INNER JOIN [Guest] ON [GuestRoomAssignment].[GuestID] = [Guest].[GuestID]
	END

GO

-- Created By: Jared Greenfield
-- Created On: 2019-04-25
-- Updates a guest room's checkout date
CREATE PROCEDURE [dbo].[sp_update_guestroomassignment_checkoutdate]
(
	@GuestID [int],
	@RoomReservationID [int]
)
AS
	BEGIN
		UPDATE [GuestRoomAssignment]
		SET [CheckoutDate] = CURRENT_TIMESTAMP
		WHERE [GuestID] = @GuestID AND [RoomReservationID] = @RoomReservationID
		RETURN @@ROWCOUNT
	END

GO

-- Jared Greenfield
-- Created 2019-04-30
-- Select the last opened membertab
CREATE PROCEDURE [dbo].[sp_select_last_membertab_by_member_id]
(
	@MemberID [int]
)
AS
	BEGIN
		SELECT TOP 1 
		MemberTabID,
		MemberID,
		Active,
		TotalPrice
		FROM MemberTab
		WHERE MemberID = @MemberID
		ORDER BY [MemberTabID] DESC
	END
GO


CREATE PROCEDURE [dbo].[sp_select_work_order_by_employee_id] 
(
	@WorkingEmployeeID		[int]
)
AS
	BEGIN
		SELECT 
			[MaintenanceWorkOrderID], 
			[MaintenanceTypeID], 
			[DateRequested],
			[DateCompleted], 
			[RequestingEmployeeID], 
			[WorkingEmployeeID],
			[Description],
			[Comments], 
			[MaintenanceStatus],
			[ResortPropertyID], 
			[Complete]
		FROM MaintenanceWorkOrder
		WHERE [WorkingEmployeeID] = @WorkingEmployeeID
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_maintenanceTypes]
AS
	BEGIN
		SELECT [MaintenanceTypeID]
		FROM [MaintenanceType]
	END
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_status_types]
AS
	BEGIN
		SELECT [MaintenanceStatusID], [Description]
		FROM MaintenanceStatus
	END
GO

CREATE PROCEDURE [dbo].[sp_insert_maintenance_status_type]
	(
		@MaintenanceStatusID	[nvarchar](50),
		@Description			[nvarchar](500)	
	)
AS
	BEGIN
		INSERT INTO [MaintenanceStatus]
			([MaintenanceStatusID], [Description])
		VALUES
			(@MaintenanceStatusID, @Description)
			
		RETURN @@ROWCOUNT
		SELECT SCOPE_IDENTITY()
	END
GO

CREATE PROCEDURE [dbo].[sp_delete_maintenance_status_type]
	(
		@MaintenanceStatusID	[nvarchar](50)	
	)
AS
	BEGIN
		DELETE  
		FROM 	[MaintenanceStatus]
		WHERE 	[MaintenanceStatusID] = @MaintenanceStatusID
	  
		RETURN @@ROWCOUNT
	END
GO

CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_statusTypes]
AS
	BEGIN
		SELECT [MaintenanceStatusID]
		FROM [MaintenanceStatus]
	END
GO

EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-05 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_activate_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-05 Desc', @value=N'Added inactive search to where clause' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_activate_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_activate_sponsor_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_activate_sponsor_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_employee_participants_by_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_employee_participants_by_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_employee_participants_by_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_employee_participants_by_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_employee_to_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_employee_to_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_guest_to_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_guest_to_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_member_to_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_add_member_to_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_appointment_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_appointment_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_assign_room_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_assign_room_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr / Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_check_in_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_check_in_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr / Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_check_out_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_check_out_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_complete_order_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_complete_order_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_count_Special_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-08' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_count_Special_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_count_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'Unknown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_count_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-09 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-09 Desc', @value=N'Removed transaction and re-wrote SP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Desc', @value=N'Added SupplierItemID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_pet_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_pet_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_resort_property'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_resort_property'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_resort_property_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_resort_property_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_resort_vehicle_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_resort_vehicle_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_SpecialOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_SpecialOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_SpecialOrderLine'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_SpecialOrderLine'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_supplierOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'Unknown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_supplierOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_vehicle_checkout'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_vehicle_checkout'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_create_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Ramesh Adhikari' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_setuplist'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_setuplist'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_shuttle_reservation_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_shuttle_reservation_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_SupplierOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'Unknown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_SupplierOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_vehicle_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_vehicle_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_deactivate_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_appointment_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_appointment_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_appointment_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_appointment_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_appointment_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_appointment_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_employee_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_employee_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_employeeID_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_employeeID_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_event_sponsor_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_event_sponsor_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_event_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_event_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Corrected param from an nvarchar to an int' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_guest_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_guest_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_Item_from_SpecialOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_Item_from_SpecialOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_item_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_item_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Added rowcount return' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_maintenance_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_maintenance_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Ramesh Adhikari' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_membertabline'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_membertabline'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_offering_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_offering_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_participant_from_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_participant_from_thread'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_pet_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_pet_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_recipe_item_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_recipe_item_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_resort_property_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_resort_property_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_resort_property_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_resort_property_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_resort_vehicle_status_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_resort_vehicle_status_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_room_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_room_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_setup'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_setup'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_setuplist'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_setuplist'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_ShuttleReservationID '
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_ShuttleReservationID '
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_supplier_order_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_supplier_order_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_tmp_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_tmp_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_tmp_picksheet_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_tmp_picksheet_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_vehicle_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_vehicle_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_vehicle_checkout_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_vehicle_checkout_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_get_thread_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_get_thread_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_appointment_by_guest'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_appointment_by_guest'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Replaces the sp_insert_appointment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_appointment_by_guest'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-26 Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_appointment_by_guest'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_appointment_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_appointment_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Desc', @value=N'Changed length for Description, added BuildingStatusID parameter and field' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Desc', @value=N'Added ResortPropertyID parameter and field' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-22 Author', @value=N'Jared Greenfield and Jim Glasgow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-22 Desc', @value=N'Syntax update for the ResortProperty field and removed param' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Removed scope identity select. Updated description field to correct size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_employee'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_employee_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_employee_role'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-09', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_event_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_event_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Desc', @value=N'Added fields for Emergency contact info and Receive Texts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Removed scope identity select. Updated description field to correct size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_guest_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_inspection'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_inspection'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_internal_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_internal_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_internal_order_line'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_internal_order_line'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-04 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-04 Desc', @value=N'Added Offering support' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_item_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_item_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_item_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Removed scope identity select. Updated description field to correct size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_item_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_maintenance_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_maintenance_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_maintenance_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Removed scope identity select. Updated description field to correct size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_maintenance_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Ramesh Adhikari' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_membertab'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_membertab'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_membertabline'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_membertabline'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-10 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-10 Desc', @value=N'Replaced row count return with scope identity return' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Removed scope identity select. Updated description field to correct size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_offering_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Added PerformanceTitle support' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-09 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Hill' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_pet_image_filename'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_pet_image_filename'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_product'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_product'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-10 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-10 Desc', @value=N'Removed OfferingID insert, added scope identity return' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-28 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-28 Desc', @value=N'Removed scope identity return' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Updated description length' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'Unknown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-28 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-28 Desc', @value=N'Added price and ResortPropertyID fields' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Desc', @value=N'Changed room number field to an int' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Removed scope identity select. Updated description field to correct size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room_with_no_price'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_room_with_no_price'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_setup'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_setup'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_SetupList'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_SetupList'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_shop'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_shop'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_shuttle_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_shuttle_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Desc', @value=N'Remove statusID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Creates Supplier Order Returns the Supplier Order ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_supplier_order_line'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_supplier_order_line'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_tmp_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_tmp_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_tmppicksheet_to_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_tmppicksheet_to_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_new_message'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_new_message'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_new_reply'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_new_reply'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_reactivate_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_reactivate_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_reactivate_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_reactivate_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_read_Special_orders_by_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-08' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_read_Special_orders_by_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_active_shuttle_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_active_shuttle_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_event_performances'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_event_performances'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_event_sponsors'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_event_sponsors'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-22 Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events_cancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events_cancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events_uncancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_events_uncancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr / Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_guest_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_guest_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_guests'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_guests'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr / Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_guests_ordered'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_guests_ordered'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_pets'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_pets'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-09 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_pets'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_reservations'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_reservations'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_shuttle_reservations'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_shuttle_reservations'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_Special_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_Special_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'Unknown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_thread_participants'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_thread_participants'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_view_model_reservations'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_view_model_reservations'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_view_model_sponsors'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_all_view_model_sponsors'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_appointment_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_appointment_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_appointments'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_appointments'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_departmentTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_departmentTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_employee_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_employee_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_employee_info'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_employee_info'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_employee_info_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_employee_info_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-22 Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-09 Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_appointment_info'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_appointment_info'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_appointment_info_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_appointment_info_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_by_term_in_last_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_by_term_in_last_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_info'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_info'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_info_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_info_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_names_and_ids'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_names_and_ids'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guest_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Desc', @value=N'Added fields for Emergency contact info and Receive Texts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Desc', @value=N'Added fields for Emergency contact info and Receive Texts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Desc', @value=N'Updated where cause to use a LIKE operator instead of an =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guests_by_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guestTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_guestTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_inactive_shuttle_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_inactive_shuttle_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_item_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_item_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_last_specialOrderID_created'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_last_specialOrderID_created'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_List_of_EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'Unknown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_List_of_EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_List_of_SpecialOrderLine_by_SpecialOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-08' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_List_of_SpecialOrderLine_by_SpecialOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenance_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenance_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Ramesh Adhikari' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_member_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-21' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_member_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_offering_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_offering_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_reservation_by_guestID'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_reservation_by_guestID'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roleID'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roleID'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roles_by_term_in_description'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roles_by_term_in_description'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-28 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roles_by_term_in_description'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-28 Desc', @value=N'Removed active search' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roles_by_term_in_description'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roomTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Date Created', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_roomTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_setuplist_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_setuplist_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_shuttle_reservation_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_shuttle_reservation_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_shuttle_reservation_by_term_in_pickup_location'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_shuttle_reservation_by_term_in_pickup_location'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_small_threads_by_participant_email_and_archived'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_small_threads_by_participant_email_and_archived'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_thread_data_by_participant_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_thread_data_by_participant_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_thread_participant_has_new_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_thread_participant_has_new_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_user_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_user_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-09 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-09 Desc', @value=N'Added searching description support' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_search_performances'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_search_Special_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_search_Special_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_active_membertab_by_member_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_active_membertab_by_member_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_active_reservation_by_member_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-16' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_active_reservation_by_member_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_active_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Desc', @value=N'Updated to Use Item instead if defunct Product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_active_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_active_items_extended'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_active_items_extended'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_active_reservationvms'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_active_reservationvms'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_closed_pickSheets_by_date'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_closed_pickSheets_by_date'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_completed_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_completed_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_deactivated_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_deactivated_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_deactivated_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Desc', @value=N'Updated to Use Item instead if defunct Product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_deactivated_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_employees'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_employees'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_generated_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_generated_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_guest_vehicles'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-08' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_guest_vehicles'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_guestroomassignvms_by_reservationID'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_guestroomassignvms_by_reservationID'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_house_keeping_requests'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_house_keeping_requests'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_incomplete_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_incomplete_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_inspections'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_inspections'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_item_names_and_ids'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_item_names_and_ids'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Desc', @value=N'Updated to Use Item instead if defunct Product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_items'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_luggage_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_luggage_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_offeringtypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_offeringtypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_offeringvms'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_offeringvms'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-05-04 Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_offeringvms'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-05-04 Desc', @value=N'Updated to use building name instead of ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_offeringvms'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Added PerformanceTitle support' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_picksheets'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_picksheets'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_picksheets_by_date'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_picksheets_by_date'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_recipes'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_recipes'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_room_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_room_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_setuplists'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_setups'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_setups'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_sponsors'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_sponsors'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_statusids'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_statusids'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_supplier_order_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_supplier_order_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_supplier_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_supplier_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-26 Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_supplier_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-26 Desc', @value=N'Generated Orders need to be approved before they can be associated with the rest of the orders' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_supplier_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_view_model_work_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_view_model_work_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_work_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_all_work_orders'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_by_guest_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_by_guest_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_guest_view_list'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_guest_view_list'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_appointment_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_buildingstatusid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-19' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_buildingstatusid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_buildingstatusid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Desc', @value=N'Removed Active field, removed "Order by Active"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_buildingstatusid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Desc', @value=N'Added BuildingStatusID field' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Desc', @value=N'Removed Active field' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-19 Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-19 Desc', @value=N'Updated select list to include ResortPropertyID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_keyword_in_building_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_keyword_in_building_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_keyword_in_building_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Desc', @value=N'Added BuildingSatusID field' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_keyword_in_building_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_keyword_in_building_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-20 Desc', @value=N'Removed Active field, removed "Order by Active"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_building_by_keyword_in_building_name'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_buildings'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_buildings'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_buildings'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Desc', @value=N'Removed Active field, removed "Order by Active"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_buildings'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-28 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_buildings'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-28 Desc', @value=N'Added StatusID and ResortPropertyID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_buildings'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_completed'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_completed'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_date_entered'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_date_entered'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_date_required'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_date_required'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_department'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_description'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_description'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_employee_active'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_employee_active'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_employee_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_employee_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_employee_inactive'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_employee_inactive'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_event_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_event_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_guest_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_guest_member'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_house_keeping_request_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_house_keeping_request_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_incomplete'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_incomplete'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_inspection_by_resortpropertyid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_inspection_by_resortpropertyid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_item_by_recipeid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_item_by_recipeid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Desc', @value=N'Added SupplierItemID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsupplier_by_itemid_and_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsuppliers_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsuppliers_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsuppliers_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Desc', @value=N'Added SupplierItemID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsuppliers_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsuppliers_by_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Desc', @value=N'Added SupplierItemID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_itemsuppliers_by_supplierid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_line_items_by_recipeid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_line_items_by_recipeid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_luggage_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_luggage_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_member_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_member_by_email'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_member_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_member_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Hill' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_member_tab_line_by_member_tab_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_member_tab_line_by_member_tab_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertabline'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertabline'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertablines'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertablines'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertabs'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertabs'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertabs_by_member_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_membertabs_by_member_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_newest_thread_message'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_newest_thread_message'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_next_picksheet_number'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_next_picksheet_number'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_offeringsubitemid_by_idandtype'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_offeringsubitemid_by_idandtype'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_order_lines_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_order_lines_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_orders_for_acknowledgement'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_orders_for_acknowledgement'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_orders_for_acknowledgement_hidepicked'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_orders_for_acknowledgement_hidepicked'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_performance_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Added PerformanceTitle support' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_performance_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Hill' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_pet_image_filename_by_pet_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_pet_image_filename_by_pet_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_pet_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_pet_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_picksheet_by_picksheetid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_picksheet_by_picksheetid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_recipe_item_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_recipe_item_lines'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_properties'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_properties'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_property_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_property_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_property_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_property_type_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_property_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_property_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_vehicle_status_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_vehicle_status_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_vehicle_statuses'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_resort_vehicle_statuses'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Desc', @value=N'Added inner join' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Desc', @value=N'Added inner join' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list_by_buildingid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list_by_buildingid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list_by_buildingid'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-11 Desc', @value=N'Added inner join' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_list_by_buildingid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_roomreservation_viewmodel_by_reservation_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_roomreservation_viewmodel_by_reservation_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_room_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_roomreservation_by_guest_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_roomreservation_by_guest_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_and_event_title'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_and_event_title'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_event_title'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_event_title'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_list_comments'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setup_list_comments'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setupList_and_event_title'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-14' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setupList_and_event_title'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setuplist_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setuplist_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setupList_setup_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_setupList_setup_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_shop_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_shop_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_shops'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_shops'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_supplier_item_by_item_and_supplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-12' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_supplier_item_by_item_and_supplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_supplier_order_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_supplier_order_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_suppliers'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_suppliers'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_suppliers_for_itemsupplier_mgmt_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_suppliers_for_itemsupplier_mgmt_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Returns all the suppliers not setup in the itemsupplier table for that item. This is so user doesn''t get the option to add a sup', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_suppliers_for_itemsupplier_mgmt_by_itemid'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicle_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicle_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicle_checkout_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicle_checkout_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicle_checkouts'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicle_checkouts'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicles'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_vehicles'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_view_model_shops'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_view_model_shops'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_work_order_by_employee_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_work_order_by_employee_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_work_order_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_work_order_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_work_order_by_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_select_work_order_by_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_shuttle_dropoff_time_now'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_shuttle_dropoff_time_now'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_unassign_room_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_unassign_room_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Wes Richardson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_appointment'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_appointment'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_AuthenticatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_AuthenticatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-18 Desc', @value=N'Changed length of Description parameters, added BuildingStatusID field and parameters' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_building'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_checkindate'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_checkindate'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_employee_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_employee_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-22 Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-09 Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_event'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_event_to_cancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_event_to_cancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Phillip Hansen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_event_to_uncancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-09' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_event_to_uncancelled'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_generated_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_generated_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Author', @value=N'Alisa Roehr' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-02-23 Desc', @value=N'Added fields for Emergency contact info and Receive Texts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_guest_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_guest_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-08' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_guest_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_guestroomassignment_checkoutdate'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_guestroomassignment_checkoutdate'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_house_keeping_request'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_inspection'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-27' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_inspection'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_item'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-12 Desc', @value=N'Added SupplierItemID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_itemsupplier'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_luggage'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_luggage_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-28' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_luggage_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-22' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_offering'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Richard Carroll' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_order_status_to_complete'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_order_status_to_complete'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_participant_thread_alert_user_new_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_participant_thread_alert_user_new_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_participant_thread_hidden_state'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_participant_thread_hidden_state'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Author', @value=N'Jacob Miller' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-03-06 Desc', @value=N'Added PerformanceTitle support' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_performance'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Craig Barkley' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-17' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Updated 2019-03-09 Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Hill' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pet_image_filename'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pet_image_filename'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pick_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_pick_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-08' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_picksheet'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_receiving'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Jared Greenfield' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-07' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_recipe'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Matt Lamarche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-26' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_reservation'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_resort_property'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_resort_property'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_resort_property_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_resort_property_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_resort_vehicle_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_resort_vehicle_status'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_role_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-01-23' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_role_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Danielle Russo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_room'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_set_active_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_set_active_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'James Heim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_set_inactive_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-18' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_set_inactive_membertab_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_setup_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_setup_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Caitlin Abelson' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_setupList_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_setupList_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_shop'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_shop'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eduardo Colon' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_shuttle_reservation_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-25' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_shuttle_reservation_by_id'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_SpecialOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-06' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_SpecialOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Carlos Arzu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_SpecialOrderLine'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_SpecialOrderLine'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-20' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Author', @value=N'Gunardi Saputra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Update 2019-04-03 Desc', @value=N'Remove statusID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_sponsor'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Eric Bostwick' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-05-02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_supplier_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Kevin Broskow' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_supplier_order_line'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-03-29' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_supplier_order_line'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_thread_archived_state'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_thread_archived_state'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_thread_new_messages_status_for_participant'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_thread_new_messages_status_for_participant'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Delaney' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_thread_participant_alias'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-24' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_thread_participant_alias'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_vehicle'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Francis Mingomba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_vehicle_checkout'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-04-03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_vehicle_checkout'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'2019-02-13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_update_work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Austin Berquam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenanceTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'3-5-2019' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenanceTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenance_status_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'3-5-2019' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenance_status_types'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_maintenance_status_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'3-5-2019' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_insert_maintenance_status_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_maintenance_status_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'3-5-2019' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_delete_maintenance_status_type'
GO
EXEC sys.sp_addextendedproperty @name=N'Author', @value=N'Dalton Cleveland' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenance_statusTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'Created Date', @value=N'3-5-2019' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'sp_retrieve_maintenance_statusTypes'
GO