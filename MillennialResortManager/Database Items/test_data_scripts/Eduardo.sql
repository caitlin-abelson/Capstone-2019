USE [MillennialResort_DB]
GO

/*  Name: Eduardo Colon
    Date: 2019-03-25
*/

/*  Created By: Eduardo Colon
    Date: 2019-03-25
*/
CREATE TABLE [dbo].[ShuttleReservation](
	[ShuttleReservationID]				[int] IDENTITY(100000, 1) 			  NOT NULL,
	[GuestID]                           [int]                                 NOT NULL,
	[EmployeeID] 						[int]						  	 	  NOT NULL, 
	[PickupLocation]					[nvarchar](150)						  NOT NULL,
	[DropoffDestination]				[nvarchar](150)						  NOT NULL,
	[PickupDateTime]					[datetime]		   			    	  NOT NULL,				
	[DropoffDateTime]					[datetime]			  				  NULL,
	[Active]							[bit]                                 NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_ShuttleReservationID] PRIMARY KEY([ShuttleReservationID] ASC)

)
GO




/*  Created By: Eduardo Colon
    Date: 2019-03-25
*/

INSERT INTO [ShuttleReservation]
		([GuestID],[EmployeeID] ,[PickupLocation], [DropoffDestination],[PickupDateTime], [DropoffDateTime])
	VALUES
		(100000,100000 , '1700 Millenium Resort Avenue' , '900 Kirkwood Avenue'  ,'2019-05-01' , '2019-05-01'),
		(100001,100001 , '1800 Millenium Resort Avenue' , '500 Plaze Center'  ,'2019-05-03' , '2019-05-03'),
		(100002,100002 , '1500 Millenium Resort Avenue' , '500 Plaze Center'  ,'2019-05-04' , '2019-05-04')
	





