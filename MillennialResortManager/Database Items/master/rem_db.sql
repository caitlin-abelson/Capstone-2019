IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'MillennialResort_DB')
BEGIN
	DROP DATABASE [MillennialResort_DB];
	print '' print '*** Dropping database MillennialResort_DB'
END
GO
CREATE DATABASE [MillennialResort_DB]
GO