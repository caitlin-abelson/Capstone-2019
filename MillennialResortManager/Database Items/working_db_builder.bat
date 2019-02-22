echo off

rem batch file to run a script to create a db
rem 2019-02-08

rem Removes the database if it exists
sqlcmd -S localhost -E -i master\rem_db.sql
rem Use the SQL Studio generated script file to build the working database.
sqlcmd -S localhost -E -i master\tables.sql
rem Apply the extended properties to fields and tables
sqlcmd -S localhost -E -i master\sps.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE