USE [master]
GO

CREATE DATABASE Hotel_JustFriend
GO

USE [Hotel_JustFriend]
GO

CREATE TABLE [Account] (
  [idAccount] int PRIMARY KEY IDENTITY(1, 1),
  [username] varchar(50) UNIQUE NOT NULL,
  [password] varchar(50) NOT NULL,
  [type] tinyint,
  [createdAt] timestamp
)
GO

CREATE TABLE [Product] (
  [idProduct] int PRIMARY KEY,
  [name] nvarchar(50) NOT NULL,
  [unit] nvarchar(20) NOT NULL,
  [pricePerUnit] money NOT NULL,
  [image] varbinary(max),
  [quantity] int DEFAULT (0),
  [status] nvarchar(255) NOT NULL CHECK ([status] IN ('outOfStock', 'inStock', 'runningLow')),
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Employee] (
  [idEmployee] int PRIMARY KEY,
  [fullName] nvarchar(50) NOT NULL,
  [gender] nvarchar(5) NOT NULL,
  [phone] varchar(11) UNIQUE NOT NULL,
  [address] nvarchar(50) DEFAULT 'Not provided',
  [dateOfBirth] date NOT NULL,
  [position] nvarchar(20) NOT NULL,
  [startDate] date NOT NULL,
  [endDate] date DEFAULT (null),
  [idAccount] int,
  [image] varbinary(max),
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Room] (
  [idRoom] int PRIMARY KEY,
  [name] nvarchar(50) NOT NULL,
  [type] nvarchar(50) NOT NULL,
  [price] money NOT NULL,
  [status] nvarchar(255) NOT NULL CHECK ([status] IN ('rented', 'available', 'notAvailable')),
  [note] nvarchar(100) DEFAULT (null),
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Customer] (
  [idCustomer] int PRIMARY KEY,
  [phone] varchar(11) UNIQUE NOT NULL,
  [name] nvarchar(50) NOT NULL,
  [gender] nvarchar(5),
  [address] nvarchar(50) DEFAULT 'Not provided',
  [dateOfBirth] date DEFAULT (null),
  [idCard] varchar(10) NOT NULL,
  [type] tinyint,
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [ProductImport] (
  [idImport] int PRIMARY KEY,
  [dateImport] datetime NOT NULL,
  [idEmployee] int,
  [importPrice] money NOT NULL
)
GO

CREATE TABLE [ProductImportInfo] (
  [idImport] int,
  [idProduct] int,
  [quantity] int NOT NULL,
  [price] money NOT NULL
)
GO

CREATE TABLE [Bill] (
  [idBill] int PRIMARY KEY,
  [idRoom] int,
  [idEmployee] int,
  [idCustomer] int,
  [checkIn] datetime NOT NULL,
  [checkOut] datetime NOT NULL,
  [totalMoney] money NOT NULL
)
GO

CREATE TABLE [Billinfo] (
  [idBill] int,
  [idProduct] int,
  [quantity] int NOT NULL,
  [price] money NOT NULL
)
GO

CREATE TABLE [Attendance] (
  [dateAbsence] date,
  [idEmployee] int,
  PRIMARY KEY ([dateAbsence], [idEmployee])
)
GO

CREATE TABLE [SalaryTable] (
  [salaryBase] money NOT NULL,
  [moneyPerShift] money NOT NULL,
  [moneyPerFault] money NOT NULL,
  [typeEmployee] nvarchar(20) NOT NULL,
  [standardWorkDays] tinyint NOT NULL
)
GO

CREATE TABLE [Salary] (
  [idSalaryRecord] int,
  [idEmployee] int,
  [numOfShift] int DEFAULT (0),
  [numOfFault] int DEFAULT (0),
  [totalSalary] money,
  [salaryMonth] date,
  [coefficients] real DEFAULT (1),
  PRIMARY KEY ([idEmployee], [salaryMonth])
)
GO

CREATE TABLE [SalaryRecord] (
  [idSalaryRecord] int PRIMARY KEY,
  [idEmployee] int,
  [salaryRecordDate] datetime,
  [total] money NOT NULL
)
GO

ALTER TABLE [Employee] ADD FOREIGN KEY ([idAccount]) REFERENCES [Account] ([idAccount])
GO

ALTER TABLE [ProductImport] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO

ALTER TABLE [ProductImportInfo] ADD FOREIGN KEY ([idImport]) REFERENCES [ProductImport] ([idImport])
GO

ALTER TABLE [ProductImportInfo] ADD FOREIGN KEY ([idProduct]) REFERENCES [Product] ([idProduct])
GO

ALTER TABLE [Bill] ADD FOREIGN KEY ([idRoom]) REFERENCES [Room] ([idRoom])
GO

ALTER TABLE [Bill] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO

ALTER TABLE [Bill] ADD FOREIGN KEY ([idCustomer]) REFERENCES [Customer] ([idCustomer])
GO

ALTER TABLE [Billinfo] ADD FOREIGN KEY ([idBill]) REFERENCES [Bill] ([idBill])
GO

ALTER TABLE [Billinfo] ADD FOREIGN KEY ([idProduct]) REFERENCES [Product] ([idProduct])
GO

ALTER TABLE [Attendance] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO

ALTER TABLE [Salary] ADD FOREIGN KEY ([idSalaryRecord]) REFERENCES [SalaryRecord] ([idSalaryRecord])
GO

ALTER TABLE [Salary] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO

ALTER TABLE [SalaryRecord] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO


EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '0 for admin, 1 for manager, 2 for receptionist, 3 for staff, 4 for security',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Account',
@level2type = N'Column', @level2name = 'type';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '0 for VIP, 1 for loyal, 2 for normal',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Customer',
@level2type = N'Column', @level2name = 'type';
GO
