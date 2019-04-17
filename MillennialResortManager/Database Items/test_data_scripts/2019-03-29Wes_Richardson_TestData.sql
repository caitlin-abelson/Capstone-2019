/*Start Wes Richardson 2019-03-01*/
USE [MillennialResort_DB]
GO

print '' print '*** Department Test Data' 
GO
INSERT INTO [dbo].[Department]
		([DepartmentID])
	VALUES
		('Main')
GO

print '' print '*** Employee Test Data' 
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [DepartmentID])
	VALUES
		('Jack', 'Doe', '5555555555', 'Jack@Resort.com', 'Main')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Member Test Records
 */
print '' print '*** Member Test Data' 
 GO
INSERT INTO [dbo].[Member]
		([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
		('John', 'Doe', '3195555555', 'John@Company.com')
GO

print '' print '*** MemberTab Test Data' 
 GO
INSERT INTO [dbo].[MemberTab]
		([MemberID])
	VALUES
		(100000)
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Offering Test Records
 */
print '' print '*** OfferingType Test Data' 
 GO
INSERT INTO [dbo].[OfferingType]
		([OfferingTypeID], [Description])
	VALUES
		('Appointment', 'Any Appointment')
GO

print '' print '*** Offering Test Data' 
 GO
INSERT INTO [dbo].[Offering]
		([OfferingTypeID], [EmployeeID], [Description], [Price])
	VALUES
		('Appointment', 100000, 'Spa', 1)
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert ServiceComponent Test Records
 */
print '' print '*** ServiceComponent Test Data' 
 GO
INSERT INTO [dbo].[ServiceComponent]
		([ServiceComponentID], [OfferingID], [Duration], [Description])
	VALUES
		('Spa', 100000, 1, 'Any Appointment')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Guest Test Data
 */
print '' print '*** Guest Test Data' 
GO
INSERT INTO [dbo].[Guest]
		([MemberID], [GuestTypeID], [FirstName], [LastName], [PhoneNumber], [Email], [ReceiveTexts], [EmergencyFirstName], [EmergencyLastName], [EmergencyPhoneNumber], [EmergencyRelation])
	VALUES
		(100000, 'Basic guest', 'John', 'Doe', '3195555555', 'John@Company.com', 1, 'Jane', 'Doe', '3195555556', 'Wife'),
		(100000, 'Basic guest', 'Jane', 'Doe', '3195555556', 'Jane@Company.com', 1, 'John', 'Doe', '3195555555', 'Husband')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Appointment Type Test Records
 */
 print '' print '*** AppointmentType Test Data' 
GO
INSERT INTO [dbo].[AppointmentType]
		([AppointmentTypeID], [Description])
	VALUES
		('Spa', 'Spa'),
		('Pet Grooming', 'Pet Grooming'),
		('Turtle Petting', 'Turtle Petting'),
		('Whale Watching', 'Whale Watching'),
		('Sand Castle', 'Sand Castle Building')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Appointment Test Data
 */
print '' print '*** Appointment Test Data' 
GO
INSERT INTO [dbo].[Appointment]
		([AppointmentTypeID], [GuestID], [StartDate], [EndDate], [Description])
	VALUES
		('Spa', 100001, '20200320 13:00', '20200320 14:00', 'Spa'),
		('Pet Grooming', 100001, '20200320 14:00', '20200320 15:00', 'Pet Grooming'),
		('Sand Castle', 100000, '20200320 13:00', '20200320 14:00', 'Sand Castle Building'),
		('Whale Watching', 100000, '20200320 14:00', '20200320 15:00', 'Whale Watching')
GO

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert Reservation Test Data
 */
print '' print '*** Reservation Test Data' 
GO
INSERT INTO [dbo].[Reservation]
		([MemberID], [NumberOfGuests], [NumberOfPets], [ArrivalDate], [DepartureDate])
	VALUES
		('100000', 2, 1, '20200320', '20200328')
GO

/* End Wes Richardson */