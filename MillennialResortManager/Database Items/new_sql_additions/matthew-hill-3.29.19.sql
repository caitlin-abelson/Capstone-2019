USE [MillennialResort_DB]
GO

-- *** Create/Insert/Alter Table SQL - Matthew Hill *** --
-- Author: <<Matthew Hill>>,Created:<<3/10/19>>

print '' print '*** Creating Pet Image FileName Table'
GO
CREATE TABLE [dbo].[PetImageFileName](
	[Filename]		[nvarchar](255)		NOT NULL,
	[PetID]			[int]				NOT NULL,
	
	CONSTRAINT [pk_Filename] PRIMARY KEY ([Filename] ASC)
)
GO

print '' print '*** Adding Foreign Key References for Pet Image FileName Table'
GO
ALTER TABLE [dbo].[PetImageFileName] WITH NOCHECK
	ADD CONSTRAINT [fk_PetID] FOREIGN KEY ([PetID])
	REFERENCES [dbo].[Pet]([PetID])
	ON DELETE CASCADE
GO

-- *** Stored Procedures - Matthew Hill *** --
-- Author: <<Matthew Hill>>,Created:<<3/10/19>>

print '' print '*** Creating sp_insert_pet_image_filename'
GO
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

print '' print '*** Creating sp_select_pet_image_filename_by_pet_id'
GO
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

print '' print '*** Creating sp_update_pet_image_filename'
GO
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

print '' print '*** Altering sp_insert_pet'
GO
ALTER PROCEDURE sp_insert_pet
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
			
			SELECT SCOPE_IDENTITY() -- Edited on 3/17/19 by Matt H.
	END
GO