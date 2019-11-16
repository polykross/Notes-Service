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
    uuid     UNIQUEIDENTIFIER default NEWID(),
    login    varchar(255) not null,
    password varchar(255) not null,
    constraint customer_pk primary key (uuid),
    constraint customer_login_uq unique (login)
);

create table dbo.Note
(
    uuid           UNIQUEIDENTIFIER default NEWID(),
    title          varchar(255)     not null,
    text           varchar(max)     not null,
    creation_date  datetime         not null,
    last_edit_date datetime         not null,
    customer_uuid  UNIQUEIDENTIFIER not null,
    constraint note_pq primary key (uuid),
    constraint note_customer_fk foreign key (customer_uuid) references Customer (uuid)

);