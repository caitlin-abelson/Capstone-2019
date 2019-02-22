echo off

rem Runs a developer written test script.
rem 2019-02-08


sqlcmd -S localhost -E -i developer_insert_script.sql
rem sqlcmd -S localhost\sqlexpress -E -i developer_insert_script.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE