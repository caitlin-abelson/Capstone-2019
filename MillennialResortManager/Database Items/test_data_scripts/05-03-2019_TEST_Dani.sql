USE [MillennialResort_DB]
GO

print '' print'*** Inserting Test Data Into InternalOrder'
GO

INSERT INTO [dbo].[InternalOrder]

	([EmployeeID], [DepartmentID], [Description], [DateOrdered])
		
	VALUES
	(100000, "Admin" , "Admin Order", "1986-08-12"),
	(100001, "Events" , "Scrambles Birthday Bash", "2019-01-12"),
	(100002, "Kitchen" , "Scrambled Brunch Items", "2019-01-12"),
	(100003, "Catering" , "Detective Theme Cake", "2019-01-12"),
	(100004, "Grooming" , "Scrambles Shampoo", "2019-01-12"),
	(100005, "Talent" , "Reorder Scrambles Brand Tapshoes", "2019-01-12"),
	(100006, "Grooming" , "Kitty Massage Oil Reorder", "2019-01-12"),
	(100007, "Kitchen" , "Produce Order", "2019-01-12"),
	(100008, "Events" , "Scrambles Birthday Bash Supplies", "2019-01-12"),
GO
  