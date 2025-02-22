USE [master]
GO
/****** Object:  Database [Hotel3]    Script Date: 11/14/2024 12:25:10 AM ******/
CREATE DATABASE [Hotel3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hotel3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL_ANHCUONG\MSSQL\DATA\Hotel3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hotel3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL_ANHCUONG\MSSQL\DATA\Hotel3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Hotel3] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hotel3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hotel3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hotel3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hotel3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hotel3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hotel3] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hotel3] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Hotel3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hotel3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hotel3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hotel3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hotel3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hotel3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hotel3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hotel3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hotel3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Hotel3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hotel3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hotel3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hotel3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hotel3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hotel3] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hotel3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hotel3] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Hotel3] SET  MULTI_USER 
GO
ALTER DATABASE [Hotel3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hotel3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hotel3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hotel3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hotel3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hotel3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Hotel3] SET QUERY_STORE = ON
GO
ALTER DATABASE [Hotel3] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Hotel3]
GO
/****** Object:  Table [dbo].[Booked]    Script Date: 11/14/2024 12:25:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booked](
	[RoomNumber] [int] NOT NULL,
	[GuestName] [varchar](40) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[BookStatus] [varchar](30) NOT NULL,
	[PhoneNumber] [varchar](15) NULL,
	[Email] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookedList]    Script Date: 11/14/2024 12:25:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookedList](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [int] NULL,
	[GuestName] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[PhoneNumber] [varchar](15) NULL,
	[Services] [text] NULL,
	[CheckIn] [datetime] NULL,
	[CheckOut] [datetime] NULL,
	[TotalBill] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookedService]    Script Date: 11/14/2024 12:25:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookedService](
	[ServiceDetailID] [int] IDENTITY(1,1) NOT NULL,
	[NameService] [varchar](40) NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[TaskID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 11/14/2024 12:25:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](30) NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Role] [varchar](15) NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11/14/2024 12:25:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomNumber] [int] NOT NULL,
	[RoomStatus] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/14/2024 12:25:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[NameService] [varchar](40) NOT NULL,
	[Price] [money] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NameService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Booked] ([RoomNumber], [GuestName], [DateCreate], [BookStatus], [PhoneNumber], [Email], [Address]) VALUES (303, N'Cao Lê Anh Cuong', CAST(N'2024-11-14T00:16:37.437' AS DateTime), N'Checked-in', N'0387683857', N'cuongcao@gmail.com', N'Phú Yên ')
INSERT [dbo].[Booked] ([RoomNumber], [GuestName], [DateCreate], [BookStatus], [PhoneNumber], [Email], [Address]) VALUES (501, N'Cuong', CAST(N'2024-11-13T17:20:45.680' AS DateTime), N'Checked-in', N'0387683857', N'cuongcaoleanh@gmail.co', N'dd')
GO
SET IDENTITY_INSERT [dbo].[BookedList] ON 

INSERT [dbo].[BookedList] ([BookingID], [RoomNumber], [GuestName], [Email], [PhoneNumber], [Services], [CheckIn], [CheckOut], [TotalBill]) VALUES (1, 500, N'cuong', N'cuong@gmail.com', NULL, N'NA', CAST(N'2024-11-13T21:38:05.507' AS DateTime), CAST(N'2024-11-13T21:38:31.113' AS DateTime), CAST(240000.00 AS Decimal(10, 2)))
INSERT [dbo].[BookedList] ([BookingID], [RoomNumber], [GuestName], [Email], [PhoneNumber], [Services], [CheckIn], [CheckOut], [TotalBill]) VALUES (2, 500, N'cuong', N'cuong@gmail.com', N'0123456789', N'["Gym Access","Room Cleaning"]', CAST(N'2024-11-13T21:46:39.033' AS DateTime), CAST(N'2024-11-13T21:47:03.957' AS DateTime), CAST(315000.00 AS Decimal(10, 2)))
INSERT [dbo].[BookedList] ([BookingID], [RoomNumber], [GuestName], [Email], [PhoneNumber], [Services], [CheckIn], [CheckOut], [TotalBill]) VALUES (3, 403, N'anhcuong', N'cuong@gmail.com', N'0123456789', N'["Gym Access","Room Service"]', CAST(N'2024-11-13T23:28:52.297' AS DateTime), CAST(N'2024-11-13T23:29:04.393' AS DateTime), CAST(275000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[BookedList] OFF
GO
SET IDENTITY_INSERT [dbo].[BookedService] ON 

INSERT [dbo].[BookedService] ([ServiceDetailID], [NameService], [RoomNumber], [Quantity], [DateCreated], [TaskID]) VALUES (39, N'Gym Access', 303, 1, CAST(N'2024-11-14T00:16:47.853' AS DateTime), 1)
INSERT [dbo].[BookedService] ([ServiceDetailID], [NameService], [RoomNumber], [Quantity], [DateCreated], [TaskID]) VALUES (40, N'Spa', 303, 1, CAST(N'2024-11-14T00:16:47.897' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[BookedService] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (1, N'cuong', N'cuong', N'Admin User', N'123 Admin St.', N'123-456-7890', N'Admin', 1)
INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (2, N'staff1@example.com', N'password456', N'Staff One', N'456 Staff Ave.', N'234-567-8901', N'Staff', 1)
INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (3, N'staff2@example.com', N'password789', N'Staff Two', N'789 Staff Rd.', N'345-678-9012', N'Staff', 1)
INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (4, N'tram', N'tram', N'tram', N'tay hoa', N'0987', N'Staff', 1)
INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (5, N'tram1', N'tram1', N'tram', N'dd', N'123', N'Admin', 1)
INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (6, N'abc', N'123', N'PRN212', N'dddd', N'123', N'Staff', 1)
INSERT [dbo].[Member] ([MemberID], [Email], [Password], [Name], [Address], [Phone], [Role], [Status]) VALUES (7, N'cuongcao@gmail.com', N'123', N'cuong', N'ddd', N'0345678999', N'Admin', 1)
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (100, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (101, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (102, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (103, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (104, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (200, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (201, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (202, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (203, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (204, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (300, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (301, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (302, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (303, N'Booked')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (304, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (400, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (401, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (402, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (403, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (404, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (500, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (501, N'Booked')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (502, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (503, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (504, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (600, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (601, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (602, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (603, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (604, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (700, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (701, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (702, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (703, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (704, N'Available')
INSERT [dbo].[Room] ([RoomNumber], [RoomStatus]) VALUES (705, N'Available')
GO
INSERT [dbo].[Service] ([NameService], [Price], [Status]) VALUES (N'Gym Access', 15000.0000, 1)
INSERT [dbo].[Service] ([NameService], [Price], [Status]) VALUES (N'Laundry', 25000.0000, 1)
INSERT [dbo].[Service] ([NameService], [Price], [Status]) VALUES (N'Room Cleaning', 50000.0000, 1)
INSERT [dbo].[Service] ([NameService], [Price], [Status]) VALUES (N'Room Service', 30000.0000, 1)
INSERT [dbo].[Service] ([NameService], [Price], [Status]) VALUES (N'Spa', 100000.0000, 1)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Member__A9D10534B996307D]    Script Date: 11/14/2024 12:25:10 AM ******/
ALTER TABLE [dbo].[Member] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Booked]  WITH CHECK ADD FOREIGN KEY([RoomNumber])
REFERENCES [dbo].[Room] ([RoomNumber])
GO
ALTER TABLE [dbo].[BookedService]  WITH CHECK ADD FOREIGN KEY([NameService])
REFERENCES [dbo].[Service] ([NameService])
GO
ALTER TABLE [dbo].[BookedService]  WITH CHECK ADD FOREIGN KEY([RoomNumber])
REFERENCES [dbo].[Booked] ([RoomNumber])
GO
ALTER TABLE [dbo].[Booked]  WITH CHECK ADD CHECK  (([BookStatus]='Checked-out' OR [BookStatus]='Checked-in'))
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD CHECK  (([Role]='Admin' OR [Role]='Staff'))
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD CHECK  (([RoomStatus]='Available' OR [RoomStatus]='Booked'))
GO
USE [master]
GO
ALTER DATABASE [Hotel3] SET  READ_WRITE 
GO
