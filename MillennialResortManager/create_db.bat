echo off

rem batch file to run a script to create a db
rem 8/27/2018

sqlcmd -S localhost -E -i MillennialResort_DB.sql
rem sqlcmd -S localhost\sqlexpress -E -i MillenialResort_DB.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE