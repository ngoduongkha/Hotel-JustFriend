USE [master]
GO

CREATE DATABASE Hotel_JustFriend
GO

USE [Hotel_JustFriend]
GO

SET DATEFORMAT DMY
GO

CREATE TABLE [AccountRole] (
  [idAccountRole] int PRIMARY KEY IDENTITY(1, 1),
  [displayName] nvarchar(100) UNIQUE NOT NULL
)
GO

CREATE TABLE [Account] (
  [idAccount] int PRIMARY KEY IDENTITY(1, 1),
  [idAccountRole] int,
  [idEmployee] int,
  [username] varchar(100) UNIQUE NOT NULL,
  [password] char(64) NOT NULL
)
GO

CREATE TABLE [Product] (
  [idProduct] char(32) PRIMARY KEY,
  [displayName] nvarchar(max) NOT NULL,
  [unit] nvarchar(20) NOT NULL,
  [pricePerUnit] money NOT NULL,
  [image] varbinary(max) NOT NULL,
  [quantity] int DEFAULT (0),
  [status] nvarchar(20) NOT NULL CHECK ([status] IN (N'Hết hàng', N'Còn hàng', N'Sắp hết')),
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [EmployeeRole] (
  [idEmployeeRole] int PRIMARY KEY IDENTITY(1, 1),
  [displayName] nvarchar(100) UNIQUE NOT NULL
)
GO

CREATE TABLE [Employee] (
  [idEmployee] int PRIMARY KEY IDENTITY(1, 1),
  [idEmployeeRole] int,
  [fullName] nvarchar(max) NOT NULL,
  [idCard] varchar(20) UNIQUE NOT NULL,
  [gender] nvarchar(20) NOT NULL,
  [phone] varchar(20) UNIQUE NOT NULL,
  [dateOfBirth] date NOT NULL,
  [startDate] date NOT NULL,
  [endDate] date DEFAULT (null),
  [image] varbinary(max) NOT NULL,
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Room] (
  [idRoom] int PRIMARY KEY IDENTITY(1, 1),
  [displayName] nvarchar(max) NOT NULL,
  [floor] tinyint,
  [number] tinyint,
  [type] nvarchar(50) NOT NULL,
  [price] money NOT NULL,
  [status] nvarchar(20) NOT NULL CHECK ([status] IN (N'Đang thuê', N'Sẵn sàng', N'Không sẵn sàng', N'Hỏng')) DEFAULT N'Sẵn sàng',
  [note] nvarchar(max) DEFAULT '',
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Customer] (
  [idCustomer] int PRIMARY KEY IDENTITY(1, 1),
  [fullname] nvarchar(max) NOT NULL,
  [idCard] varchar(20) UNIQUE NOT NULL,
  [phone] varchar(20) UNIQUE NOT NULL,
  [type] nvarchar(50) NOT NULL,
  [gender] nvarchar(20) NOT NULL,
  [address] nvarchar(max) DEFAULT N'Không cung cấp',
  [dateOfBirth] date DEFAULT (null)
)
GO

CREATE TABLE [ProductImport] (
  [idImport] char(32) PRIMARY KEY,
  [idEmployee] int,
  [dateImport] datetime NOT NULL,
  [importPrice] money NOT NULL
)
GO

CREATE TABLE [ProductImportInfo] (
  [idImport] char(32),
  [idProduct] char(32),
  [quantity] int NOT NULL,
  [price] money NOT NULL,
  PRIMARY KEY ([idImport], [idProduct])
)
GO

CREATE TABLE [Bill] (
  [idBill] char(32) PRIMARY KEY,
  [idRoom] int,
  [idEmployee] int,
  [idCustomer] int,
  [checkIn] datetime NOT NULL,
  [checkOut] datetime NOT NULL,
  [totalMoney] money NOT NULL
)
GO

CREATE TABLE [Billinfo] (
  [idBill] char(32),
  [idProduct] char(32),
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
  [idSalaryRecord] char(32) PRIMARY KEY,
  [idAccount] int,
  [salaryRecordDate] datetime,
  [total] money NOT NULL
)
GO

CREATE TABLE [SalaryRecordInfo] (
  [idSalaryRecord] char(32),
  [idEmployee] int,
  [numOfShift] int DEFAULT (0),
  [numOfFault] int DEFAULT (0),
  [totalSalary] money,
  [salaryMonth] date,
  [coefficients] real DEFAULT (1),
  PRIMARY KEY ([idEmployee], [salaryMonth])
)
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([idEmployee]) REFERENCES [Employee] ([idEmployee])
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([idAccountRole]) REFERENCES [AccountRole] ([idAccountRole])
GO

ALTER TABLE [Employee] ADD FOREIGN KEY ([idEmployeeRole]) REFERENCES [EmployeeRole] ([idEmployeeRole])
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

INSERT INTO dbo.AccountRole (displayName) VALUES ('Administrator')
INSERT INTO dbo.AccountRole (displayName) VALUES ('Owner')
INSERT INTO dbo.AccountRole (displayName) VALUES ('Manager')
INSERT INTO dbo.AccountRole (displayName) VALUES ('Receptionist')
INSERT INTO dbo.AccountRole (displayName) VALUES ('Staff')

INSERT INTO dbo.Account (idAccountRole, username, password) VALUES (1, 'admin', '38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017')
