create database eProject3
go
USE eProject3;
GO 
create table Labs
(
	Labs_ID int primary key identity,
	LabsName varchar(max),
	Quantity int,
)
go

create table Suppliers
(
	Supplier_ID int primary key identity,
	SupplierName varchar(max) not null,
)
go 
CREATE TABLE Devices (
Devices_ID int PRIMARY KEY identity,
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
create table Complain
(
	Complain_ID int primary key identity,
	description nvarchar(max) not null,
	Reason nvarchar(max) not null,
	Status_CP nvarchar(max) not null,
	Date_CP datetime not null,
	Category nvarchar(max) not null,
	ID nvarchar(50) not null,
	Reply nvarchar(max) null
)
go  
create table Event
(
	Id int primary key identity,
	Name nvarchar(max) null,
	Description nvarchar(max) null,
	StartTime datetime null,
	EndTime datetime null,
	Labs_ID int not null,
	CONSTRAINT fk_Labs
	FOREIGN KEY (Labs_ID)
	REFERENCES Labs (Labs_ID),
)
go
create table MaintainceDevices
(
	Maintn_ID int primary key identity,
	Descriptions varchar(MAX),
	Reason varchar(MAX),
	Date datetime,
	Creater varchar(MAX),
	Devices_ID int not null,
	ID nvarchar(50) not null,
	Status varchar(MAX),
	Step int,
	isFinished bit
	CONSTRAINT fk_Device_Devices_ID
	FOREIGN KEY (Devices_ID)
	REFERENCES Devices (Devices_ID),
)
go
CREATE TABLE Slot
(
   Slot_ID INT PRIMARY KEY IDENTITY,
   Day DATETIME NULL,
   slot NVARCHAR(MAX) NOT NULL,
   Lab_ID int NOT NULL,
   Admins_ID nvarchar(50) NOT NULL,
   CONSTRAINT FK_labs_Lab_ID
      FOREIGN KEY (Lab_ID)
      REFERENCES Labs (Labs_ID),
   CONSTRAINT FK_admins_ID
      FOREIGN KEY (Admins_ID)
      REFERENCES Admins (ID)
)
go