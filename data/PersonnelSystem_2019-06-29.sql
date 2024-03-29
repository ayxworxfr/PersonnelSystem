USE [master]
GO
/****** Object:  Database [PersonnelSystem]    Script Date: 07/02/2019 21:00:32 ******/
CREATE DATABASE [PersonnelSystem] ON  PRIMARY 
( NAME = N'PersonnelSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\PersonnelSystem.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PersonnelSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\PersonnelSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PersonnelSystem] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PersonnelSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PersonnelSystem] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [PersonnelSystem] SET ANSI_NULLS OFF
GO
ALTER DATABASE [PersonnelSystem] SET ANSI_PADDING OFF
GO
ALTER DATABASE [PersonnelSystem] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [PersonnelSystem] SET ARITHABORT OFF
GO
ALTER DATABASE [PersonnelSystem] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [PersonnelSystem] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [PersonnelSystem] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [PersonnelSystem] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [PersonnelSystem] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [PersonnelSystem] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [PersonnelSystem] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [PersonnelSystem] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [PersonnelSystem] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [PersonnelSystem] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [PersonnelSystem] SET  DISABLE_BROKER
GO
ALTER DATABASE [PersonnelSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [PersonnelSystem] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [PersonnelSystem] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [PersonnelSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [PersonnelSystem] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [PersonnelSystem] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [PersonnelSystem] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [PersonnelSystem] SET  READ_WRITE
GO
ALTER DATABASE [PersonnelSystem] SET RECOVERY FULL
GO
ALTER DATABASE [PersonnelSystem] SET  MULTI_USER
GO
ALTER DATABASE [PersonnelSystem] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [PersonnelSystem] SET DB_CHAINING OFF
GO
USE [PersonnelSystem]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Address] [varchar](250) NULL,
	[Phone] [varchar](50) NULL,
	[WebSite] [varchar](50) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](50) NULL,
	[Info] [nvarchar](max) NULL,
	[Address] [nvarchar](50) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Position]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionId] [bigint] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](50) NULL,
	[Info] [nvarchar](max) NULL,
	[Number] [bigint] NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Sex] [varchar](10) NULL,
	[DepartmentId] [bigint] NULL,
	[PositionId] [bigint] NULL,
	[DateIntoCompany] [datetime] NULL,
	[Phone] [varchar](50) NULL,
	[QQ] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[IDCard] [varchar](50) NULL,
	[School] [varchar](50) NULL,
	[Remark] [varchar](max) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseCode] [varchar](50) NOT NULL,
	[CourseName] [varchar](50) NULL,
	[DepartmentId] [bigint] NULL,
	[StudentType] [varchar](50) NULL,
	[Hours] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[AccruedCount] [int] NULL,
	[AttendedCount] [int] NULL,
	[CourseRemark] [nvarchar](max) NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Wage]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wage](
	[WageId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NULL,
	[DepartmentId] [bigint] NULL,
	[PositionId] [bigint] NULL,
	[BasicWage] [float] NULL,
	[Subsidise] [float] NULL,
	[AwardMoney] [float] NULL,
	[FinedMoney] [float] NULL,
	[FinalWage] [float] NULL,
 CONSTRAINT [PK_Wage] PRIMARY KEY CLUSTERED 
(
	[WageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 07/02/2019 21:00:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[ContractId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NULL,
	[SignTime] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[RenewCount] [int] NULL,
	[ProbationarySalary] [float] NULL,
	[OfficialSalary] [float] NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Employee_Department]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
/****** Object:  ForeignKey [FK_Employee_Position]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([PositionId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Position]
GO
/****** Object:  ForeignKey [FK_Course_Department]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Department]
GO
/****** Object:  ForeignKey [FK_Wage_Department]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Wage]  WITH CHECK ADD  CONSTRAINT [FK_Wage_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Wage] CHECK CONSTRAINT [FK_Wage_Department]
GO
/****** Object:  ForeignKey [FK_Wage_Employee]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Wage]  WITH CHECK ADD  CONSTRAINT [FK_Wage_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Wage] CHECK CONSTRAINT [FK_Wage_Employee]
GO
/****** Object:  ForeignKey [FK_Wage_Position]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Wage]  WITH CHECK ADD  CONSTRAINT [FK_Wage_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([PositionId])
GO
ALTER TABLE [dbo].[Wage] CHECK CONSTRAINT [FK_Wage_Position]
GO
/****** Object:  ForeignKey [FK_Contract_Employee]    Script Date: 07/02/2019 21:00:33 ******/
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Employee]
GO
