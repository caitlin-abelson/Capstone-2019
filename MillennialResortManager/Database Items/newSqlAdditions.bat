echo off

rem Runs a developer written addition to the DB.
rem 2019-03-22

rem This is the name you change. _____.sql Your file name goes in the blank. ex. new_sql_additions/yourfile.sql
sqlcmd -S localhost -E -i new_sql_additions/matthew-hill-3.29.19.sql
sqlcmd -S localhost -E -i new_sql_additions/2019-04-03_Francis_Mingomba.sql
sqlcmd -S localhost -E -i new_sql_additions/4-3-2019PhillipHansen.sql


ECHO .
ECHO If no errors appeared, your new additions were created without error.
PAUSE