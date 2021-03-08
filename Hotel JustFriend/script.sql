USE [master]
GO

CREATE DATABASE Hotel_JustFriend
GO

USE [Hotel_JustFriend]
GO

CREATE TABLE [AccountRole] (
  [idAccountRole] int PRIMARY KEY IDENTITY(1, 1),
  [displayName] nvarchar(100) UNIQUE NOT NULL
)
GO

CREATE TABLE [Account] (
  [idAccount] int PRIMARY KEY IDENTITY(1, 1),
  [idAccountRole] int,
  [username] varchar(100) UNIQUE NOT NULL,
  [password] varchar(100) NOT NULL,
)
GO

CREATE TABLE [Product] (
  [idProduct] varchar(64) PRIMARY KEY,
  [displayName] nvarchar(max) NOT NULL,
  [unit] nvarchar(20) NOT NULL,
  [pricePerUnit] money NOT NULL,
  [image] varbinary(max),
  [quantity] int DEFAULT (0),
  [status] nvarchar(255) NOT NULL CHECK ([status] IN ('OutOfStock', 'InStock', 'RunningLow')),
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [EmployeeRole] (
  [idEmployeeRole] int PRIMARY KEY IDENTITY(1, 1),
  [displayName] nvarchar(100) UNIQUE NOT NULL
)
GO

CREATE TABLE [Employee] (
  [idEmployee] int PRIMARY KEY,
  [idEmployeeRole] int,
  [idAccountRole] int,
  [fullName] nvarchar(max) NOT NULL,
  [gender] nvarchar(20) NOT NULL,
  [phone] varchar(20) UNIQUE NOT NULL,
  [address] nvarchar(max) DEFAULT 'Not provided',
  [dateOfBirth] date NOT NULL,
  [startDate] date NOT NULL,
  [endDate] date DEFAULT (null),
  [image] varbinary(max),
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Room] (
  [idRoom] int PRIMARY KEY,
  [displayName] nvarchar(max) NOT NULL,
  [type] nvarchar(50) NOT NULL,
  [price] money NOT NULL,
  [status] nvarchar(255) NOT NULL CHECK ([status] IN ('Rented', 'Available', 'NotAvailable')),
  [note] nvarchar(max) DEFAULT '',
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Customer] (
  [idCustomer] int PRIMARY KEY,
  [phone] varchar(20) UNIQUE NOT NULL,
  [fullname] nvarchar(max) NOT NULL,
  [gender] nvarchar(20) DEFAULT 'Others',
  [address] nvarchar(max) DEFAULT 'Not provided',
  [dateOfBirth] date DEFAULT (null),
  [idCard] varchar(20) UNIQUE NOT NULL,
  [type] tinyint,
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [ProductImport] (
  [idImport] varchar(64) PRIMARY KEY,
  [idEmployee] int,
  [dateImport] datetime NOT NULL,
  [importPrice] money NOT NULL
)
GO

CREATE TABLE [ProductImportInfo] (
  [idImport] varchar(64),
  [idProduct] varchar(64),
  [quantity] int NOT NULL,
  [price] money NOT NULL,
  PRIMARY KEY ([idImport], [idProduct])
)
GO

CREATE TABLE [Bill] (
  [idBill] varchar(64) PRIMARY KEY,
  [idRoom] int,
  [idEmployee] int,
  [idCustomer] int,
  [checkIn] datetime NOT NULL,
  [checkOut] datetime NOT NULL,
  [totalMoney] money NOT NULL
)
GO

CREATE TABLE [Billinfo] (
  [idBill] varchar(64),
  [idProduct] varchar(64),
  [quantity] int NOT NULL,
  [price] money NOT NULL,
  PRIMARY KEY ([idBill], [idProduct])
)
GO

CREATE TABLE [Attendance] (
  [idEmployee] int,
  [dateAbsence] date,
  PRIMARY KEY ([dateAbsence], [idEmployee])
)
GO

CREATE TABLE [SalaryTable] (
  [idEmployeeRole] int PRIMARY KEY,
  [salaryBase] money NOT NULL,
  [moneyPerShift] money NOT NULL,
  [moneyPerFault] money NOT NULL,
  [standardWorkDays] tinyint NOT NULL
)
GO

CREATE TABLE [SalaryRecord] (
  [idSalaryRecord] varchar(64) PRIMARY KEY,
  [idAccount] int,
  [salaryRecordDate] datetime,
  [total] money NOT NULL
)
GO

CREATE TABLE [SalaryRecordInfo] (
  [idSalaryRecord] varchar(64),
  [idEmployee] int,
  [numOfShift] int DEFAULT (0),
  [numOfFault] int DEFAULT (0),
  [totalSalary] money,
  [salaryMonth] date,
  [coefficients] real DEFAULT (1),
  PRIMARY KEY ([idEmployee], [salaryMonth])
)
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([idAccountRole]) REFERENCES [AccountRole] ([idAccountRole])
GO

ALTER TABLE [Employee] ADD FOREIGN KEY ([idEmployeeRole]) REFERENCES [EmployeeRole] ([idEmployeeRole])
GO

ALTER TABLE [Employee] ADD FOREIGN KEY ([idAccountRole]) REFERENCES [AccountRole] ([idAccountRole])
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

ALTER TABLE [SalaryTable] ADD FOREIGN KEY ([idEmployeeRole]) REFERENCES [EmployeeRole] ([idEmployeeRole])
GO

ALTER TABLE [SalaryRecord] ADD FOREIGN KEY ([idAccount]) REFERENCES [Account] ([idAccount])
GO

ALTER TABLE [SalaryRecordInfo] ADD FOREIGN KEY ([idSalaryRecord]) REFERENCES [SalaryRecord] ([idSalaryRecord])
GO

ALTER TABLE [SalaryRecordInfo] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO


EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '0 for VIP, 1 for loyal, 2 for normal',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Customer',
@level2type = N'Column', @level2name = 'type';
GO
