echo off

rem Adds developer written test data to the Database.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank.
sqlcmd -S localhost -E -i test_data_scripts/2019-03-27_JaredGreenfield_Offering.sql

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
PAUSE