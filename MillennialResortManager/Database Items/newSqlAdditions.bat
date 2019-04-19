echo off

rem Runs a developer written addition to the DB.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank. ex. new_sql_additions/yourfile.sql
<<<<<<< HEAD
sqlcmd -S localhost -E -i new_sql_additions/2019-04-17_Wes_Richardson.sql
=======
sqlcmd -S localhost -E -i new_sql_additions/Kevin_B_SupplierItem.sql

sqlcmd -S localhost -E -i new_sql_additions/2019-04-13_CaitlinAbelson.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-15_Francis_Mingomba.sql
>>>>>>> 0880fb83a17f9e5bb7a35d72125bf9c5f766d912


ECHO .
ECHO If no errors appeared, your new additions were created without error.
PAUSE