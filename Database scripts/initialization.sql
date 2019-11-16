/*
To drop database execute:
drop database if exists Notes;

To create database execute:
create database Notes;
*/

drop table if exists Note;
drop table if exists Customer;

create table dbo.Customer
(
    id       UNIQUEIDENTIFIER default NEWID(),
    login    varchar(255) not null,
    password varchar(max) not null,
    constraint customer_pk primary key (id),
    constraint customer_login_uq unique (login)
);

create table dbo.Note
(
    id             UNIQUEIDENTIFIER default NEWID(),
    title          varchar(255)     not null,
    text           varchar(max)     not null,
    creation_date  datetime2        not null,
    last_edit_date datetime2        not null,
    customer_id    UNIQUEIDENTIFIER not null,
    constraint note_pq primary key (id),
    constraint note_customer_fk foreign key (customer_id) references Customer (id)

);