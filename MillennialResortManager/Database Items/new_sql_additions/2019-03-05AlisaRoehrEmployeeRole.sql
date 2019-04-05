USE [MillennialResort_DB]
GO
/* Alisa Roehr 2019/03/29 */
print '' print '*** Creating sp_insert_employee_role'
GO
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

/* Alisa Roehr 2019/03/29 */
print '' print '*** Creating sp_delete_employee_role'
GO
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