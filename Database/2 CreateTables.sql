drop table if exists students cascade;

create table students
(
	id serial primary key,
	name text not null check (length(name) > 0),
	GPA real not null check(GPA between 0 and 4)
);

comment on table  students is 'A database of students.';
comment on column students.id is 'Student IDs of all students within database.';
comment on column students.name is 'Names of all students within database.';
comment on column students.GPA is 'GPA of all students within database';
--grant select, insert, update, delete on table students to "Assignment4";
