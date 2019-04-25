echo off

rem Runs a developer written test script.
rem 2019-02-08

rem sqlcmd -S localhost -E -i developer_insert_script.sql
sqlcmd -S localhost -E -i developer_insert_script.sql

rem sqlcmd -S localhost -E -i test_data.sql
sqlcmd -S localhost -E -i test_data.sql

ECHO .
ECHO If no errors appeared, your test script and/or data was inserted
