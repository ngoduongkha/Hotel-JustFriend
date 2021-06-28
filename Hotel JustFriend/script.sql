CREATE DATABASE Hotel_JustFriend
GO
USE Hotel_JustFriend
GO
CREATE TABLE [Account] (
  [idAccount] int PRIMARY KEY IDENTITY(1, 1),
  [idTypeAccount] int,
  [username] varchar(100) UNIQUE NOT NULL,
  [password] char(64) NOT NULL,
)
GO

CREATE TABLE [TypeAccount] (
  [idTypeAccount] int PRIMARY KEY IDENTITY(1, 1),
  [displayname] nvarchar(100) UNIQUE NOT NULL,
)

CREATE TABLE [Room] (
  [idRoom] int PRIMARY KEY,
  [floor] int NOT NULL,
  [displayName] nvarchar(max) NOT NULL,
  [idType] int NOT NULL,
  [status] bit NOT NULL DEFAULT (1),
  [note] nvarchar(max) DEFAULT '',
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [Customer] (
  [idCustomer] int PRIMARY KEY IDENTITY(1, 1),
  [fullname] nvarchar(max) NOT NULL,
  [idCard] varchar(20) UNIQUE NOT NULL,
  [idType] int NOT NULL,
  [address] nvarchar(max) DEFAULT 'Not provided'
)
GO

CREATE TABLE [TypeCustomer] (
  [idType] int PRIMARY KEY IDENTITY(1, 1),
  [displayname] nvarchar(max),
  [number] float,
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [TypeRoom] (
  [idType] int PRIMARY KEY IDENTITY(1, 1),
  [fullname] nvarchar(max),
  [price] money,
  [isDelete] bit DEFAULT (0)
)
GO

CREATE TABLE [RentInvoice] (
  [idRentInvoice] int PRIMARY KEY IDENTITY(1, 1),
  [date] date,
  [idRoom] int,
  [purchase] bit DEFAULT (0)
)
GO

CREATE TABLE [RentInvoiceInfo] (
  [idRentInvoiceInfo] int ,
  [idRentInvoice] int,
  [idCustomer] int,
  PRIMARY KEY ([idRentInvoice], [idCustomer])
)
GO

CREATE TABLE [Bill] (
  [idBill] int PRIMARY KEY IDENTITY(1, 1),
  [date] date,
  [totalMoney] money
)
GO

CREATE TABLE [BillInfo] (
  [numberDay] int,
  [price] money,
  [idBill] int,
  [idRoom] int,
  PRIMARY KEY ([idBill], [idRoom])
)
GO

CREATE TABLE [RevenuePercentage] (
  [idrevenuePrecentage] int PRIMARY KEY IDENTITY(1, 1),
  [percent] float,
  [month] int,
  [year] int,
  [idRoom] int
)
GO

CREATE TABLE [Constant] (
  [idConstant] int PRIMARY KEY,
  [maxCustomer] int,
  [percent] float,
)
GO
CREATE TABLE [Revenue](
	[idRevenue] int PRIMARY KEY IDENTITY(1,1),
	[idType] int,
	[RevenueType] money,
)
GO

ALTER TABLE [Revenue] ADD FOREIGN KEY ([idType]) REFERENCES [TypeRoom] ([idType])

ALTER TABLE [Room] ADD FOREIGN KEY ([idType]) REFERENCES [TypeRoom] ([idType])
GO

ALTER TABLE [RentInvoice] ADD FOREIGN KEY ([idRoom]) REFERENCES [Room] ([idRoom])
GO

ALTER TABLE [BillInfo] ADD FOREIGN KEY ([idRoom]) REFERENCES [Room] ([idRoom])
GO

ALTER TABLE [Customer] ADD FOREIGN KEY ([idType]) REFERENCES [TypeCustomer] ([idType])
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([idTypeAccount]) REFERENCES [TypeAccount] ([idTypeAccount])
GO

ALTER TABLE [RentInvoiceInfo] ADD FOREIGN KEY ([idCustomer]) REFERENCES [Customer] ([idCustomer])
GO

ALTER TABLE [RentInvoiceInfo] ADD FOREIGN KEY ([idRentInvoice]) REFERENCES [RentInvoice] ([idRentInvoice])
GO

ALTER TABLE [BillInfo] ADD FOREIGN KEY ([idBill]) REFERENCES [Bill] ([idBill])
GO

INSERT INTO dbo.TypeAccount(displayname) VALUES (N'Admin')
INSERT INTO dbo.TypeAccount(displayname) VALUES (N'Nhân viên')
INSERT INTO dbo.TypeAccount(displayname) VALUES (N'Kế toán')
INSERT INTO dbo.Account (username, password, idTypeAccount) VALUES ('admin', '38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017', 1)
INSERT INTO dbo.Account (username, password, idTypeAccount) VALUES ('kha', '38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017', 2)
INSERT INTO dbo.TypeRoom (fullname, price) VALUES ('VIP',20000)
INSERT INTO dbo.TypeRoom (fullname, price) VALUES (N'Thường',10000)
INSERT INTO dbo.TypeCustomer(displayname, number) VALUES (N'Nội địa',1)
INSERT INTO dbo.TypeCustomer(displayname, number) VALUES (N'Nước ngoài',1.25)
INSERT INTO dbo.Constant VALUES (0,3,1.3)