/*Start Wes Richardson 2019-03-01*/

/*
 * Author: Wes Richardson
 * Created 2019-03-07
 *
 * Insert GuestType Test Records
 */
print '' print '*** GuestType Test Data' 
 GO
INSERT INTO [dbo].[GuestType]
		([GuestTypeID], [Description])
	VALUES
		('Basic guest', 'Basic guest')
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
		(100001, 'Basic guest', 'John', 'Doe', '3195555555', 'John@Company.com', 1, 'Jane', 'Doe', '3195555556', 'Wife'),
		(100001, 'Basic guest', 'Jane', 'Doe', '3195555556', 'Jane@Company.com', 1, 'John', 'Doe', '3195555555', 'Husband')
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

/* End Wes Richardson */