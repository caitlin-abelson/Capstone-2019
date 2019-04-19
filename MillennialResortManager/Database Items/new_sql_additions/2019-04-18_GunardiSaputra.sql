


-- Sponsor	Missing all stored procedures	<done>

print '' print '*** Creating sp_retrieve_all_states'
GO
CREATE PROCEDURE sp_retrieve_all_states
AS
	BEGIN
		SELECT [StateCode]	

		FROM   [State]
		ORDER BY [StateCode]
	END
GO







/*
 * Author: Gunardi Saputra
 * Created: 2019/01/23
 * This stored procedure create a sponsor record in the sponsor table 
 * Updated: 2019/04/03
 * Remove statusID
 */
print '' print '*** Creating sp_insert_sponsor'
GO
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
print '' print '*** Creating sp_update_sponsor'

GO
CREATE PROCEDURE sp_update_sponsor
	(
		@SponsorID			[int],
		
		@oldName			[nvarchar](50),
		@oldAddress			[nvarchar](25),
		@oldCity			[nvarchar](50),
		@oldState			[nvarchar](2),
		@oldPhoneNumber		[nvarchar](11),
		@oldEmail			[nvarchar](50),
		@oldContactFirstName	[nvarchar](50),
		@oldContactLastName	[nvarchar](100),
		@oldDateAdded		[datetime],
		@oldActive			[bit],
		
		
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
		AND [Name]				= @oldName
		AND [Address]		 =  @oldAddress
		AND [City]		 =  @oldCity
		AND [State]		 =  @oldState
		AND [PhoneNumber]		 =  @oldPhoneNumber
		AND [Email]		 =  @oldEmail
		AND [ContactFirstName]		 =  @oldContactFirstName
		AND [ContactLastName]		 =  @oldContactLastName
		AND [DateAdded]		 =  @oldDateAdded
		AND [Active]		 =  @oldActive
			
		RETURN @@ROWCOUNT
	END
GO


/*
* Author: Gunardi Saputra
* Created Date: 2019/02/20
* This stored procedure activate a sponsor record in the sponsor table by its id.
*/

print '' print '*** Creating sp_activate_sponsor_by_id'
GO
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
print '' print '*** Creating sp_deactivate_sponsor'
GO
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
print '' print '*** Creating sp_delete_sponsor'
GO

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
print '' print '*** Creating sp_select_sponsor'
GO

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
print '' print '*** Creating sp_retrieve_all_view_model_sponsors'
GO

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




