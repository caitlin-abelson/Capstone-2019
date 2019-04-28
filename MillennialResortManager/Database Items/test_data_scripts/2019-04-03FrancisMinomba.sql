-- AUTHOR  : Francis Mingomba
-- CREATED : 04/03/2019

USE [MillennialResort_DB]
GO

print '' print 'BEGIN FRANCIS MINGOMBA INSERT'

-- Author: Francis Mingomba
-- Created 2019-03-19

print '' print 'inserting into ResortProperty'
EXEC sp_create_resort_property 'Vehicle' ;
EXEC sp_create_resort_property 'Building' ;
EXEC sp_create_resort_property 'Room' ;

print '' print '*** Inserting sample resort vehicles'
EXEC sp_create_vehicle 'Dodge'  , 'Charger', 2015, 'IA 523', 50000, 5, 'Gray', '2015-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Audi'   , 'A4'     , 2017, 'IA 523', 50000, 5, 'Gray', '2010-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Toyota' , 'Camry'  , 2018, 'IA 523', 50000, 5, 'Gray', '2012-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Nissan' , 'Maxima' , 2019, 'IA 523', 50000, 5, 'Gray', '2011-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Hyundai', 'Excel'  , 2010, 'IA 523', 50000, 5, 'Gray', '2009-05-05', 'A car', 1, '', 1, 'Available', 100000 ;
EXEC sp_create_vehicle 'Skoda'  , 'Octavia', 1920, 'IA 523', 50000, 5, 'Gray', '2008-05-05', 'A car', 1, '', 1, 'Available', 100000 ;

print '' print 'END FRANCIS MINGOMBA INSERT'