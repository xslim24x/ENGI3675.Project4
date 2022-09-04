-- use this script inside sql shell

drop role if exists "Assignment4";
create role "Assignment4" login;
comment on role "Assignment4" is 'Restricted ISS app pool user';

drop database if exists "Assignment_4";
create database "Assignment_4";
comment on database "Assignment_4" is 'Database for Assignment4';

grant connect on database "Assignment_4" to "Assignment4";