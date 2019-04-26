-- AUTHOR  : Francis Mingomba
-- CREATED : 04/26/2019

USE [MillennialResort_DB]
GO

print '' print 'BEGIN FRANCIS MINGOMBA INSERT'

-- Author: Francis Mingomba
-- Created 2019-04-26

print '' print 'inserting into ResortVehicleStatus - REQUIRED'
EXEC sp_create_resort_vehicle_status 'In Use', 'Vehicle currently checked out';
EXEC sp_create_resort_vehicle_status 'Decomissioned', 'Vehicle dead'                 ;
EXEC sp_create_resort_vehicle_status 'Available'    , 'Vehicle available for use'    ;

print '' print 'inserting into ResortProperty - REQUIRED'
EXEC sp_create_resort_property 'Vehicle' ;
EXEC sp_create_resort_property 'Building' ;
EXEC sp_create_resort_property 'Room' ;

print '' print 'END FRANCIS MINGOMBA INSERT'