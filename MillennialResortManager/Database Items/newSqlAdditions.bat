echo off

rem Runs a developer written addition to the DB.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank. ex. new_sql_additions/yourfile.sql


sqlcmd -S localhost -E -i new_sql_additions/2019-04-10_SQL_Dani.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-15_Francis_Mingomba.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04_25_Matt_H.sql

sqlcmd -S localhost -E -i new_sql_additions/2019-04-26_SQL_Dani.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-25JamesJaredCheckout.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-26FrancisMinomba.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-27Eduardo-Colon.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-05-03_Richard_Carroll.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-05-04_Richard_Carroll.sql



ECHO .
ECHO If no errors appeared, your new additions were created without error.
PAUSE