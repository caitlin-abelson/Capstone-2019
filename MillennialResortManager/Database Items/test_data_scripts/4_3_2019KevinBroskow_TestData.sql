USE [MillennialResort_DB]
GO
/*Start Kevin */

print '*** inserting test data into Department'
GO
INSERT INTO [dbo].[Department]
([DepartmentID],[Description])
VALUES
('Department','Description')
GO





print '' print '*** Inserting test data into Employee'
GO
INSERT INTO [dbo].[Employee]
([FirstName],[LastName],[PhoneNumber],[Email],[PasswordHash],[Active],[DepartmentID])
VALUES
('Joe','Smith','3335554888','joe@here.com','DLKFJAPWIEFHSADIBNVPAOWEYHTADSNADLKSGJDAFIB',1,'Department'),
('Joe','Smith','3335554888','joe@COMPANY.com','DLKFJAPWIEFHSADIBNVPAOWEYHTADSNADLKSGJDAFIB',1,'Department'),
('Joe','Smith','3335554888','joe@place.com','DLKFJAPWIEFHSADIBNVPAOWEYHTADSNADLKSGJDAFIB',1,'Department')
GO




print '' print '*** Inserting test data into OfferingType'
GO
INSERT INTO [dbo].[OfferingType]
([OfferingTypeID],[Description])
VALUES
('OfferingType','Description')
GO




print'' print '*** Inserting test data into Offering'
GO
INSERT INTO [dbo].[Offering]
([OfferingTypeID],[EmployeeID],[Description],[Price],[Active])
VALUES
	('OfferingType',100000,'Food Item',12.50,1),
	('OfferingType',100000,'Event',12.50,1),
	('OfferingType',100000,'',12.50,1)
GO
print '' print '***Inserting test data into Recipe'
GO
INSERT INTO [dbo].[Recipe]
([Name],[Description],[DateAdded],[Active])
VALUES
('Name','Description','2019-03-05',1)
GO

print'' print '*** Inserting test data into Supplier'
GO
INSERT INTO [dbo].[Supplier]
([Name],[Address],[City],[State],[PostalCode],[Country],[PhoneNumber],[Email],[ContactFirstName],[ContactLastName],
[DateAdded],[Description],[ACtive])
VALUES
('Name','123 Main St','Iowa City','IA','60013','USA','8155554488','place@place.com]','Joe','Schmoe','2019-03-05','Description',1)

/*
 * Author: Kevin Broskow
 * Created 2019-04-03
 *
 * Insert Item Test Records
 */
print '' print '*** Item Test Data' 
GO
INSERT INTO [dbo].[Item]
		([ItemTypeID], [OfferingID], [Name], [Description], [DateActive],[Active],[OnHandQty],[ReorderQty],[CustomerPurchasable], [RecipeID])
	VALUES
		('UNASSIGNED',100000,'Roast Beef','UNASSIGNED','2019-03-05',1,15,35,1,100000),
		('UNASSIGNED',100001,'Taco','UNASSIGNED','2019-03-05',1,15,35,1,100000),
		('UNASSIGNED',100001,'Beef','UNASSIGNED','2019-03-05',1,15,35,1,100000),
		('UNASSIGNED',100002,'Fish','UNASSIGNED','2019-03-05',1,15,35,1,100000)
GO


print '' print '*** Inseting Test SupplierOrderData'
INSERT INTO [dbo].[SupplierOrder]
([EmployeeID],[Description],[OrderComplete],[DateOrdered],[SupplierID])
VALUES
(100000,'Order For laundry',1,'2019-03-05',100000),
(100000,'Order For laundry',1,'2019-03-05',100000),
(100000,'Order For laundry',1,'2019-03-05',100000)
GO


print'' print 'Inserting test data into SupplierOrderLine'
GO
INSERT INTO [dbo].[SupplierOrderLine]
([ItemID],[SupplierOrderID],[Description],[OrderQty],[QtyReceived],[UnitPrice])
VALUES 
(100000,100005,'Description',25,0,10),
(100000,100006,'Description',25,0,10),
(100001,100007,'Description',25,0,10),
(100001,100005,'Description',25,0,10),
(100001,100006,'Description',25,0,10),
(100003,100007,'Description',25,0,10),
(100002,100005,'Description',25,0,10),
(100002,100006,'Description',25,0,10),
(100002,100007,'Description',25,0,10),
(100003,100005,'Description',25,0,10)
GO






/*End Kevin */








