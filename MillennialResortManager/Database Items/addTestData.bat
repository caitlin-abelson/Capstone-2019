echo off

rem Adds developer written test data to the Database.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank.
sqlcmd -S localhost -E -i test_data_scripts/2019-04-10_TEST_Dani.sql
sqlcmd -S localhost -E -i test_data_scripts/JacobMillerSampleData.sql
sqlcmd -S localhost -E -i test_data_scripts/4_10_19CraigBarkley_TestDataRoom.sql
sqlcmd -S localhost -E -i test_data_scripts/2019-04-03FrancisMinomba.sql

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
PAUSE