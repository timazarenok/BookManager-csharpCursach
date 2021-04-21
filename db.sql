create database BookManager;
go
use BookManager;

create table Users (
id int Identity(1,1) primary key,
[login] varchar(15),
[password] varchar(20)
)

create table BookCategories (
id int Identity(1,1) primary key,
[name] varchar(30)
)

create table Categories (
id int Identity(1,1) primary key,
[name] varchar(30)
)

create table Books (
id int Identity(1,1) primary key,
id_category int references BookCategories(id),
[name] varchar(60),
[author] varchar(100),
[description] varchar(300),
price decimal(7,2)
)

create table Products (
id int Identity(1,1) primary key,
id_category int references Categories(id),
[name] varchar(60),
[description] varchar(300),
price decimal(7,2)
)
