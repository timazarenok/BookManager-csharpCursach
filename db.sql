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

create table Authors (
id int Identity(1,1) primary key,
[full_name] varchar(30)
)

create table Books (
id int Identity(1,1) primary key,
id_category int references BookCategories(id),
id_author int references Authors(id),
[name] varchar(60),
[description] varchar(300),
price decimal(7,2),
amount int
)

create table Products (
id int Identity(1,1) primary key,
id_category int references Categories(id),
[name] varchar(60),
[description] varchar(300),
price decimal(7,2), 
amount int
)

create table Orders (
id int Identity(1,1) primary key,
id_user int references Users(id),
[date] date,
result decimal(10,2)
)

create table OrdersBooks (
id int primary key,
id_order int references Orders(id),
id_book int references Books(id),
number int Identity(1000000, 1)
)

create table OrdersProducts (
id int primary key,
id_order int references Orders(id),
id_product int references Products(id),
number int Identity(1000000, 1)
)

insert into Users values ('tima2002', 'tima2002')
insert into Orders values(1, (select convert(varchar(10),(select getdate()), 120)), 0)
insert into OrdersProducts values (1,1,1)
select * from OrdersProducts
SELECT number, name, price FROM OrdersProducts join Products on Products.id = id_product WHERE id_order = 3