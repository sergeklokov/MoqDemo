use AdventureWorks2016CTP3; -- LAPTOP-TVCRG0TN\MSSQLSERVER01

drop table if exists People;

create table People(
	PersonID int Primary KEY,
	Name varchar(30),  
	CellPhoneID int	);

insert People values (1, 'Serge',		1);
insert People values (2, 'Tristan',	1);
insert People values (3, 'Dedushka',	2);
insert People values (4, 'McKayla',	Null);
insert People values (5, 'Babushka',		3);
insert People values (6, 'Serge',		8);

