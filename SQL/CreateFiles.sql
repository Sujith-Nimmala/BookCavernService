
create database BookStore

use BookStore

 

create table BookDetails(

BookId int identity(101,1) primary key,

BookName varchar(150),

AuthorName varchar(50),

BookPrice money,

Stock int,

Category varchar(30)

)

 

create table Orders(

OrderId int identity(560020,1) primary key,

CustomerId int constraint custFk foreign key references AppUser(UserId),

PlacedOn datetime

)

 

create table OrderDetails(

OrderId int constraint orderfk foreign key references Orders(OrderId),

BookId int constraint bookfk foreign key references BookDetails(BookId),

Quantity int,

Cost money

)


create table AppUser(

UserId int identity(900,1) primary key,

UserName varchar(30),

UserAddress varchar(200),

UserEmail varchar(30) constraint EmailUn unique,

UserPass varchar(30) constraint PassUn unique,

UserContactNo varchar(10) constraint ContactUn unique,

UserRole varchar(10)

)

 