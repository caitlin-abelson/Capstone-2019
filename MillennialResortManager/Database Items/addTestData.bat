echo off

rem Adds developer written test data to the Database.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank.
REM   sqlcmd -S localhost -E -i test_data_scripts/2019-04-10_TEST_Dani.sql
sqlcmd -S localhost -E -i test_data_scripts/FullPresentationData.sql


REM sqlcmd -S localhost -E -i test_data_scripts/04-10-2019GunardiSaputra.sql

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
PAUSE