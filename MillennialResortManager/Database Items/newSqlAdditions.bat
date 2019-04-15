echo off

rem Runs a developer written addition to the DB.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank. ex. new_sql_additions/yourfile.sql
sqlcmd -S localhost -E -i new_sql_additions/Kevin_B_SupplierItem.sql

sqlcmd -S localhost -E -i new_sql_additions/RichardSQL.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-03-27_JaredGreenfield.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-05_CaitlinAbelson_SetupList.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-10_SQL_Dani.sql
sqlcmd -S localhost -E -i new_sql_additions/room.sql


ECHO .
ECHO If no errors appeared, your new additions were created without error.
PAUSE