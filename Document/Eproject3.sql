USE eProject3;
GO 
Drop table Calender

go
create table Labs
(
	Labs_ID int primary key identity,
	LabsName varchar(max),
	Quantity int
)
go

create table Suppliers
(
	Supplier_ID int primary key identity,
	SupplierName varchar(max) not null
)
go 
CREATE TABLE Devices (
Devices_ID varchar(50) PRIMARY KEY,
DeviceName NVARCHAR(MAX) NOT NULL,
DeviceType NVARCHAR(MAX) NULL,
SupplyFrom NVARCHAR(MAX) NULL,
Status NVARCHAR(MAX) NULL,
DateMaintance DATETIME NULL,
DeviceImg NVARCHAR(MAX) NULL,
Labs_ID int not null,
Supplier_ID int not null,
CONSTRAINT fk_labs_Labs_ID
FOREIGN KEY (Labs_ID)
REFERENCES Labs (Labs_ID), 
CONSTRAINT fk_supplier_Supplier_ID
FOREIGN KEY (Supplier_ID)
REFERENCES Suppliers (Supplier_ID),
);
go 
CREATE TABLE Admins (
ID nvarchar(50) PRIMARY KEY,
AdminName NVARCHAR(MAX) NOT NULL,
Password NVARCHAR(MAX) NOT NULL,
Role varchar(max) not null
);
go
create table users
(
	Users_ID int primary key identity,
	Username nvarchar(max) not null,
	Passwords nvarchar(max) not null,
	Email nvarchar(max) not null
)
go
create table Complain
(
	Complain_ID int primary key identity,
	description nvarchar(max) not null,
	Users_ID int not null,
	CONSTRAINT fk_users_Users_ID
	FOREIGN KEY (Users_ID)
	REFERENCES users (Users_ID)
)
go
create table report 
(
	Report_ID int primary key identity,
	Descriptions varchar(max) null,
	ReportDate datetime not null,
	Reciver varchar(max) not null,
	Complain_ID int not null,
	Devices_ID varchar(50) not null,
	CONSTRAINT fk_report_Report_ID
	FOREIGN KEY (Complain_ID)
	REFERENCES Complain (Complain_ID),
	CONSTRAINT fk_Devices_Devices_ID
	FOREIGN KEY (Devices_ID)
	REFERENCES Devices (Devices_ID)
)
go 
create table Calender
(
	Calen_ID int primary key identity,
	Event varchar(max) not null,
	Description varchar(max) null,
	StarTime datetime not null,	
        EndTime datetime null,
)
go
create table Evt
(
	Event_ID int primary key identity,
	Title varchar(max) null,
	Minititle varchar(max) not null,
	Img int not null,
	Content varchar(max) not null,
	Event_Date datetime not null,
	Calen_ID int not null,
	CONSTRAINT fk_Calender_Calen_ID
	FOREIGN KEY (Calen_ID)
	REFERENCES Calender (Calen_ID),
)
go 


