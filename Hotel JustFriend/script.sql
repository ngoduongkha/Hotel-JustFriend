CREATE DATABASE Hotel_JustFriend
GO

USE Hotel_JustFriend
GO

CREATE TABLE [Account] (
  [IdAccount] int PRIMARY KEY IDENTITY(1, 1),
  [IdTypeAccount] int NOT NULL,
  [Username] varchar(100) UNIQUE NOT NULL,
  [Password] char(64) NOT NULL
)
GO

CREATE TABLE [TypeAccount] (
  [IdTypeAccount] int PRIMARY KEY IDENTITY(1, 1),
  [DisplayName] nvarchar(50) UNIQUE NOT NULL
)
GO

CREATE TABLE [Room] (
  [IdRoom] int PRIMARY KEY IDENTITY(1, 1),
  [Floor] int NOT NULL,
  [Number] int NOT NULL,
  [DisplayName] nvarchar(20) NOT NULL,
  [IdTypeRoom] int NOT NULL,
  [Status] nvarchar(20) NOT NULL,
  [Note] nvarchar(max) DEFAULT '',
  [IsDelete] bit DEFAULT (0) NOT NULL,
)
GO

CREATE TABLE [TypeRoom] (
  [IdTypeRoom] int PRIMARY KEY IDENTITY(1, 1),
  [DisplayName] nvarchar(max) NOT NULL,
  [Price] money NOT NULL,
  [IsDelete] bit DEFAULT (0) NOT NULL,
)
GO

CREATE TABLE [Customer] (
  [IdCustomer] int PRIMARY KEY IDENTITY(1, 1),
  [FullName] nvarchar(max) NOT NULL,
  [IdCard] varchar(20) UNIQUE NOT NULL,
  [IdTypeCustomer] int NOT NULL,
)
GO

CREATE TABLE [TypeCustomer] (
  [IdTypeCustomer] int PRIMARY KEY IDENTITY(1, 1),
  [Displayname] nvarchar(20) NOT NULL,
  [CoefficientsObtained] float NOT NULL,
  [IsDelete] bit DEFAULT (0) NOT NULL
)
GO

CREATE TABLE [RentInvoice] (
  [IdRentInvoice] int PRIMARY KEY IDENTITY(1, 1),
  [Date] date NOT NULL,
  [IdRoom] int NOT NULL,
  [Purchase] bit DEFAULT (0) NOT NULL
)
GO

CREATE TABLE [RentInvoiceInfo] (
  [IdRentInvoiceInfo] int IDENTITY(1, 1),
  [IdRentInvoice] int NOT NULL,
  [IdCustomer] int NOT NULL,
  PRIMARY KEY ([IdRentInvoice], [IdCustomer])
)
GO

CREATE TABLE [Bill] (
  [IdBill] int PRIMARY KEY IDENTITY(1, 1),
  [Date] date NOT NULL,
  [TotalMoney] money NOT NULL
)
GO

CREATE TABLE [BillInfo] (
  [NumberDay] int NOT NULL,
  [Price] money NOT NULL,
  [IdBill] int NOT NULL,
  [IdRoom] int NOT NULL,
  PRIMARY KEY ([idBill], [idRoom])
)
GO

CREATE TABLE [RevenuePercentage] (
  [IdrevenuePrecentage] int PRIMARY KEY IDENTITY(1, 1),
  [Percent] float,
  [Month] int,
  [Year] int,
  [IdRoom] int
)
GO

CREATE TABLE [Constant] (
  [IdConstant] int PRIMARY KEY,
  [MaxCustomer] int NOT NULL,
  [Percent] float NOT NULL,
)
GO

CREATE TABLE [Revenue](
	[IdRevenue] int PRIMARY KEY IDENTITY(1,1),
	[IdTypeRoom] int,
	[RevenueType] money,
)
GO

ALTER TABLE [Revenue] ADD FOREIGN KEY ([idTypeRoom]) REFERENCES [TypeRoom] ([idTypeRoom])
GO

ALTER TABLE [Room] ADD FOREIGN KEY ([idTypeRoom]) REFERENCES [TypeRoom] ([idTypeRoom])
GO

ALTER TABLE [RentInvoice] ADD FOREIGN KEY ([idRoom]) REFERENCES [Room] ([idRoom])
GO

ALTER TABLE [BillInfo] ADD FOREIGN KEY ([idRoom]) REFERENCES [Room] ([idRoom])
GO

ALTER TABLE [Customer] ADD FOREIGN KEY ([idTypeCustomer]) REFERENCES [TypeCustomer] ([idTypeCustomer])
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([idTypeAccount]) REFERENCES [TypeAccount] ([idTypeAccount])
GO

ALTER TABLE [RentInvoiceInfo] ADD FOREIGN KEY ([idCustomer]) REFERENCES [Customer] ([idCustomer])
GO

ALTER TABLE [RentInvoiceInfo] ADD FOREIGN KEY ([idRentInvoice]) REFERENCES [RentInvoice] ([idRentInvoice])
GO

ALTER TABLE [BillInfo] ADD FOREIGN KEY ([idBill]) REFERENCES [Bill] ([idBill])
GO

INSERT INTO dbo.TypeAccount(DisplayName) VALUES (N'Admin')
INSERT INTO dbo.TypeAccount(DisplayName) VALUES (N'Nhân viên')
INSERT INTO dbo.TypeAccount(DisplayName) VALUES (N'Kế toán')
INSERT INTO dbo.Account (Username, Password, IdTypeAccount) VALUES ('admin', '38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017', 1)
INSERT INTO dbo.Account (Username, Password, IdTypeAccount) VALUES ('kha', '38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017', 2)
INSERT INTO dbo.TypeRoom (DisplayName, Price) VALUES ('VIP', 20000)
INSERT INTO dbo.TypeRoom (DisplayName, Price) VALUES (N'Thường', 10000)
INSERT INTO dbo.TypeCustomer(Displayname, CoefficientsObtained) VALUES (N'Nội địa', 1)
INSERT INTO dbo.TypeCustomer(Displayname, CoefficientsObtained) VALUES (N'Nước ngoài', 1.25)
INSERT INTO dbo.Constant VALUES (0, 3, 1.3)