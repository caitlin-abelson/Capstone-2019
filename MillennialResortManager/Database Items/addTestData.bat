echo off

rem Adds developer written test data to the Database.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank.
<<<<<<< HEAD
sqlcmd -S localhost -E -i test_data_scripts/3-29-2019PhillipHansen.sql
=======
sqlcmd -S localhost -E -i test_data_scripts/matthew-hill-update-3.29.19.sql
>>>>>>> dev

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
PAUSE