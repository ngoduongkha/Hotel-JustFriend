USE [Hotel_JustFriend]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IdAccount] [int] IDENTITY(1,1) NOT NULL,
	[IdTypeAccount] [int] NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Password] [char](64) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[IdBill] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[TotalMoney] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[NumberDay] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[IdBill] [int] NOT NULL,
	[IdRoom] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBill] ASC,
	[IdRoom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Constant]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Constant](
	[IdConstant] [int] NOT NULL,
	[MaxCustomer] [int] NOT NULL,
	[Percent] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdConstant] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[IdCustomer] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[IdCard] [varchar](20) NOT NULL,
	[IdTypeCustomer] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentInvoice]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentInvoice](
	[IdRentInvoice] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[IdRoom] [int] NOT NULL,
	[Purchase] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRentInvoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentInvoiceInfo]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentInvoiceInfo](
	[IdRentInvoiceInfo] [int] IDENTITY(1,1) NOT NULL,
	[IdRentInvoice] [int] NOT NULL,
	[IdCustomer] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRentInvoice] ASC,
	[IdCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Revenue]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Revenue](
	[IdRevenue] [int] IDENTITY(1,1) NOT NULL,
	[IdTypeRoom] [int] NULL,
	[RevenueType] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRevenue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RevenuePercentage]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RevenuePercentage](
	[IdrevenuePrecentage] [int] IDENTITY(1,1) NOT NULL,
	[Percent] [float] NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[IdRoom] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdrevenuePrecentage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[IdRoom] [int] IDENTITY(1,1) NOT NULL,
	[Floor] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[DisplayName] [nvarchar](20) NOT NULL,
	[IdTypeRoom] [int] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRoom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeAccount]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeAccount](
	[IdTypeAccount] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTypeAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeCustomer]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeCustomer](
	[IdTypeCustomer] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](20) NOT NULL,
	[CoefficientsObtained] [float] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTypeCustomer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeRoom]    Script Date: 6/20/2021 2:33:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeRoom](
	[IdTypeRoom] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](max) NOT NULL,
	[Price] [money] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTypeRoom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([IdAccount], [IdTypeAccount], [Username], [Password]) VALUES (1, 1, N'admin', N'38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017')
INSERT [dbo].[Account] ([IdAccount], [IdTypeAccount], [Username], [Password]) VALUES (2, 2, N'kha', N'38D180985D1B2E7A6014190E2CBD3C967408837188354EC93D27BFD86D09A017')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([IdBill], [Date], [TotalMoney]) VALUES (1, CAST(N'2021-05-25' AS Date), 1350000.0000)
INSERT [dbo].[Bill] ([IdBill], [Date], [TotalMoney]) VALUES (2, CAST(N'2021-05-25' AS Date), 2250000.0000)
INSERT [dbo].[Bill] ([IdBill], [Date], [TotalMoney]) VALUES (3, CAST(N'2021-05-25' AS Date), 1530000.0000)
INSERT [dbo].[Bill] ([IdBill], [Date], [TotalMoney]) VALUES (4, CAST(N'2021-05-30' AS Date), 2700000.0000)
INSERT [dbo].[Bill] ([IdBill], [Date], [TotalMoney]) VALUES (5, CAST(N'2021-05-30' AS Date), 3600000.0000)
INSERT [dbo].[Bill] ([IdBill], [Date], [TotalMoney]) VALUES (6, CAST(N'2021-05-30' AS Date), 5610000.0000)
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
INSERT [dbo].[BillInfo] ([NumberDay], [Price], [IdBill], [IdRoom]) VALUES (6, 1350000.0000, 1, 3)
INSERT [dbo].[BillInfo] ([NumberDay], [Price], [IdBill], [IdRoom]) VALUES (6, 2250000.0000, 2, 1)
INSERT [dbo].[BillInfo] ([NumberDay], [Price], [IdBill], [IdRoom]) VALUES (6, 1530000.0000, 3, 2)
INSERT [dbo].[BillInfo] ([NumberDay], [Price], [IdBill], [IdRoom]) VALUES (12, 2700000.0000, 4, 3)
INSERT [dbo].[BillInfo] ([NumberDay], [Price], [IdBill], [IdRoom]) VALUES (12, 3600000.0000, 5, 1)
INSERT [dbo].[BillInfo] ([NumberDay], [Price], [IdBill], [IdRoom]) VALUES (22, 5610000.0000, 6, 2)
GO
INSERT [dbo].[Constant] ([IdConstant], [MaxCustomer], [Percent]) VALUES (0, 3, 25)
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([IdCustomer], [FullName], [IdCard], [IdTypeCustomer], [Address]) VALUES (1, N'Phuc', N'19520225', 1, N'Quy Nhon')
INSERT [dbo].[Customer] ([IdCustomer], [FullName], [IdCard], [IdTypeCustomer], [Address]) VALUES (2, N'Kha nước ngoài', N'19520117', 2, N'Nước ngoài')
INSERT [dbo].[Customer] ([IdCustomer], [FullName], [IdCard], [IdTypeCustomer], [Address]) VALUES (3, N'Luân Khơ Me', N'19520702', 1, N'Khơ Me')
INSERT [dbo].[Customer] ([IdCustomer], [FullName], [IdCard], [IdTypeCustomer], [Address]) VALUES (4, N'Nam rừng đước', N'19520123', 1, N'Trên rừng')
INSERT [dbo].[Customer] ([IdCustomer], [FullName], [IdCard], [IdTypeCustomer], [Address]) VALUES (5, N'L.Anh', N'19720123', 2, N'UIT')
INSERT [dbo].[Customer] ([IdCustomer], [FullName], [IdCard], [IdTypeCustomer], [Address]) VALUES (6, N'Phuc', N'1', 1, N'1')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[RentInvoice] ON 

INSERT [dbo].[RentInvoice] ([IdRentInvoice], [Date], [IdRoom], [Purchase]) VALUES (1, CAST(N'2021-05-25' AS Date), 1, 1)
INSERT [dbo].[RentInvoice] ([IdRentInvoice], [Date], [IdRoom], [Purchase]) VALUES (2, CAST(N'2021-05-25' AS Date), 3, 1)
INSERT [dbo].[RentInvoice] ([IdRentInvoice], [Date], [IdRoom], [Purchase]) VALUES (3, CAST(N'2021-05-25' AS Date), 2, 1)
INSERT [dbo].[RentInvoice] ([IdRentInvoice], [Date], [IdRoom], [Purchase]) VALUES (4, CAST(N'2021-05-30' AS Date), 3, 1)
INSERT [dbo].[RentInvoice] ([IdRentInvoice], [Date], [IdRoom], [Purchase]) VALUES (5, CAST(N'2021-05-30' AS Date), 2, 1)
INSERT [dbo].[RentInvoice] ([IdRentInvoice], [Date], [IdRoom], [Purchase]) VALUES (6, CAST(N'2021-05-30' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[RentInvoice] OFF
GO
SET IDENTITY_INSERT [dbo].[RentInvoiceInfo] ON 

INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (1, 1, 1)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (2, 1, 2)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (3, 1, 3)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (5, 2, 3)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (4, 2, 4)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (7, 3, 2)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (6, 3, 5)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (8, 4, 3)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (9, 4, 4)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (10, 5, 6)
INSERT [dbo].[RentInvoiceInfo] ([IdRentInvoiceInfo], [IdRentInvoice], [IdCustomer]) VALUES (11, 6, 6)
SET IDENTITY_INSERT [dbo].[RentInvoiceInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([IdRoom], [Floor], [Number], [DisplayName], [IdTypeRoom], [Status], [Note], [IsDelete]) VALUES (1, 1, 1, N'Phòng 101', 1, N'Sẵn sàng', N'', 0)
INSERT [dbo].[Room] ([IdRoom], [Floor], [Number], [DisplayName], [IdTypeRoom], [Status], [Note], [IsDelete]) VALUES (2, 1, 2, N'Phòng 102', 2, N'Sẵn sàng', N'', 0)
INSERT [dbo].[Room] ([IdRoom], [Floor], [Number], [DisplayName], [IdTypeRoom], [Status], [Note], [IsDelete]) VALUES (3, 1, 3, N'Phòng 103', 3, N'Sẵn sàng', N'', 0)
SET IDENTITY_INSERT [dbo].[Room] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeAccount] ON 

INSERT [dbo].[TypeAccount] ([IdTypeAccount], [DisplayName]) VALUES (1, N'Admin')
INSERT [dbo].[TypeAccount] ([IdTypeAccount], [DisplayName]) VALUES (3, N'Kế toán')
INSERT [dbo].[TypeAccount] ([IdTypeAccount], [DisplayName]) VALUES (2, N'Nhân viên')
SET IDENTITY_INSERT [dbo].[TypeAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeCustomer] ON 

INSERT [dbo].[TypeCustomer] ([IdTypeCustomer], [DisplayName], [CoefficientsObtained], [IsDelete]) VALUES (1, N'Nội địa', 1, 0)
INSERT [dbo].[TypeCustomer] ([IdTypeCustomer], [DisplayName], [CoefficientsObtained], [IsDelete]) VALUES (2, N'Nước ngoài', 1.5, 0)
SET IDENTITY_INSERT [dbo].[TypeCustomer] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeRoom] ON 

INSERT [dbo].[TypeRoom] ([IdTypeRoom], [DisplayName], [Price], [IsDelete]) VALUES (1, N'VIP', 200000.0000, 0)
INSERT [dbo].[TypeRoom] ([IdTypeRoom], [DisplayName], [Price], [IsDelete]) VALUES (2, N'Thương gia', 170000.0000, 0)
INSERT [dbo].[TypeRoom] ([IdTypeRoom], [DisplayName], [Price], [IsDelete]) VALUES (3, N'Thường', 150000.0000, 0)
SET IDENTITY_INSERT [dbo].[TypeRoom] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Account__536C85E41B23783B]    Script Date: 6/20/2021 2:33:47 AM ******/
ALTER TABLE [dbo].[Account] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__3B7B33C378FC908F]    Script Date: 6/20/2021 2:33:47 AM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[IdCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TypeAcco__4E3E687D985A9B92]    Script Date: 6/20/2021 2:33:47 AM ******/
ALTER TABLE [dbo].[TypeAccount] ADD UNIQUE NONCLUSTERED 
(
	[DisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RentInvoice] ADD  DEFAULT ((0)) FOR [Purchase]
GO
ALTER TABLE [dbo].[Room] ADD  DEFAULT ('') FOR [Note]
GO
ALTER TABLE [dbo].[Room] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[TypeCustomer] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[TypeRoom] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([IdTypeAccount])
REFERENCES [dbo].[TypeAccount] ([IdTypeAccount])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([IdBill])
REFERENCES [dbo].[Bill] ([IdBill])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([IdRoom])
REFERENCES [dbo].[Room] ([IdRoom])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([IdTypeCustomer])
REFERENCES [dbo].[TypeCustomer] ([IdTypeCustomer])
GO
ALTER TABLE [dbo].[RentInvoice]  WITH CHECK ADD FOREIGN KEY([IdRoom])
REFERENCES [dbo].[Room] ([IdRoom])
GO
ALTER TABLE [dbo].[RentInvoiceInfo]  WITH CHECK ADD FOREIGN KEY([IdCustomer])
REFERENCES [dbo].[Customer] ([IdCustomer])
GO
ALTER TABLE [dbo].[RentInvoiceInfo]  WITH CHECK ADD FOREIGN KEY([IdRentInvoice])
REFERENCES [dbo].[RentInvoice] ([IdRentInvoice])
GO
ALTER TABLE [dbo].[Revenue]  WITH CHECK ADD FOREIGN KEY([IdTypeRoom])
REFERENCES [dbo].[TypeRoom] ([IdTypeRoom])
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD FOREIGN KEY([IdTypeRoom])
REFERENCES [dbo].[TypeRoom] ([IdTypeRoom])
GO
