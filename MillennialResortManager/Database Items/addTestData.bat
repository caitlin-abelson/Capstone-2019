echo off

rem Adds developer written test data to the Database.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank.
REM   sqlcmd -S localhost -E -i test_data_scripts/2019-04-10_TEST_Dani.sql
sqlcmd -S localhost -E -i test_data_scripts/FullTestData.sql
sqlcmd -S localhost -E -i test_data_scripts/05-03-2019_TEST_Dani.sql

REM sqlcmd -S localhost -E -i test_data_scripts/2019-04-25_Matt_H_Test_Data.sql


REM sqlcmd -S localhost -E -i test_data_scripts/2019-04-04_JamesHeim.sql
REM sqlcmd -S localhost -E -i test_data_scripts/2019-04-03FrancisMinomba.sql


REM sqlcmd -S localhost -E -i test_data_scripts/04-10-2019GunardiSaputra.sql

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
PAUSE