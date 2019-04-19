USE [MillennialResort_DB]
GO


-- The Money field on MemberTab should NOT be Nullable. Instead, 0 by default.
print '' print '*** Altering Member Tab MONEY column.' 
GO

UPDATE [dbo].[MemberTab] SET [TotalPrice] = 0 WHERE [TotalPrice] IS NULL

ALTER TABLE [dbo].[MemberTab]
ALTER COLUMN TotalPrice [Money] NOT NULL

ALTER TABLE [dbo].[MemberTab] ADD CONSTRAINT DF_MemberTab_TotalPrice DEFAULT 0 FOR TotalPrice





-- Let's get to work.


PRINT '' PRINT '*** Creating sp_select_membertab_by_id'
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

PRINT '' PRINT '*** Creating sp_select_active_membertab_by_member_id'
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


PRINT '' PRINT '*** Creating sp_insert_membertab'
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

PRINT '' PRINT '*** Creating sp_update_set_inactive_membertab_by_id'
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

