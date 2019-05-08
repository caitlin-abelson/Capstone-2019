echo off

rem batch file to run a script to create a db
rem 2019-02-08

rem Removes the database if it exists
sqlcmd -S localhost -E -i master\rem_db.sql
rem Use the SQL Studio generated script files to build the working database.
sqlcmd -S localhost -E -i master\tables.sql
sqlcmd -S localhost -E -i master\sps.sql
rem Add required default data for full DB functionality
sqlcmd -S localhost -E -i master\default_data.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE