Created By: Jared Greenfield
Created On: 2019-03-22
Workflow for adding to the Database:
1. Create a new file for your table definitions and stored procedures in the (new_sql_additions). Check the HowTo file for more information.
	-Make sure 'USE [MillennialResort_DB]
GO' is at the top of your file.
2. Create a new file for your test data in the (test_data_scripts). Check the HowTo for naming reccomendations and more details.
	-Make sure 'USE [MillennialResort_DB]
GO' is at the top of your file.
3. Edit the batch files to use your new scripts. You just have to change the names. 
	addTestData --(Runs)--> test_data_scripts
	newSqlAdditions --(Runs)--> new_sql_additions
4. If you have any questions, let me know. Information on each folder can be found in their HowTo's.
5. The dev_insert and add_Test_code will be removed when this new system is fully in place.

ORDER TO RUN FILES
1. working_db_builder
2. newSqlAdditions
3. addTestData

Jared will try to add the newest updates on the weekends after classes if possible. Saturday/Sunday.