echo off

rem Adds developer written test data to the Database.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank.
<<<<<<< HEAD
sqlcmd -S localhost -E -i test_data_scripts/2019-03-29Wes_Richardson_TestData.sql
=======
sqlcmd -S localhost -E -i test_data_scripts/austinB.sql
sqlcmd -S localhost -E -i test_data_scripts/2019-04-03FrancisMinomba.sql
>>>>>>> 0880fb83a17f9e5bb7a35d72125bf9c5f766d912

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
PAUSE