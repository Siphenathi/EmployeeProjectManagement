﻿/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO
/****** Object:  Database [CodeWorks]    Script Date: 2017/09/29 11:39:38 AM ******/
CREATE DATABASE [CodeWorks]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CodeWorks', FILENAME = N'C:\DATA\CodeWorks.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CodeWorks_log', FILENAME = N'C:\DATA\CodeWorks_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CodeWorks] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CodeWorks].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CodeWorks] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CodeWorks] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CodeWorks] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CodeWorks] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CodeWorks] SET ARITHABORT OFF 
GO
ALTER DATABASE [CodeWorks] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CodeWorks] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CodeWorks] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CodeWorks] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CodeWorks] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CodeWorks] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CodeWorks] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CodeWorks] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CodeWorks] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CodeWorks] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CodeWorks] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CodeWorks] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CodeWorks] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CodeWorks] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CodeWorks] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CodeWorks] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CodeWorks] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CodeWorks] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CodeWorks] SET  MULTI_USER 
GO
ALTER DATABASE [CodeWorks] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CodeWorks] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CodeWorks] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CodeWorks] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CodeWorks] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CodeWorks] SET QUERY_STORE = OFF
GO
USE [CodeWorks]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CodeWorks]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Surname] [varchar](150) NOT NULL,
	[JobTitleId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeSkill]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSkill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[SkillID] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeSkill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobTitle]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTitle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[JobTitle] [varchar](150) NOT NULL,
 CONSTRAINT [PK_JobTitle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Startdate] [datetime] NOT NULL,
	[Enddate] [datetime] NULL,
	[Cost] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectEmployee]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEmployee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
 CONSTRAINT [PK_ProjectEmployee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2017/09/29 11:39:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Surname] [varchar](150) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (3, N'Dani', N'Welbeck', 4)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (4, N'Alexis', N'Sanchez', 2)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (5, N'Mesut', N'Ozil', 1)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (6, N'Musa', N'Dembele', 2)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (7, N'Harry', N'Kane', 3)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (8, N'David', N'Silva', 1)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (9, N'Roy', N'Keane', 2)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (10, N'Dele', N'Alli', 1)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (11, N'Romelu', N'Lukaku', 1)
GO
INSERT [dbo].[Employee] ([id], [Name], [Surname], [JobTitleId]) VALUES (12, N'Anthony', N'Martial', 2)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[JobTitle] ON 
GO
INSERT [dbo].[JobTitle] ([id], [JobTitle]) VALUES (1, N'Developer')
GO
INSERT [dbo].[JobTitle] ([id], [JobTitle]) VALUES (2, N'DBA')
GO
INSERT [dbo].[JobTitle] ([id], [JobTitle]) VALUES (3, N'Tester')
GO
INSERT [dbo].[JobTitle] ([id], [JobTitle]) VALUES (4, N'Business Analyst')
GO
SET IDENTITY_INSERT [dbo].[JobTitle] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 
GO
INSERT [dbo].[Project] ([id], [Name], [Startdate], [Enddate], [Cost]) VALUES (1, N'Arsenal Playground', CAST(N'2017-01-01T00:00:00.000' AS DateTime), NULL, CAST(100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Project] ([id], [Name], [Startdate], [Enddate], [Cost]) VALUES (2, N'Aston Villa Training Facility', CAST(N'2017-04-02T00:00:00.000' AS DateTime), CAST(N'2017-05-01T00:00:00.000' AS DateTime), CAST(1200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Project] ([id], [Name], [Startdate], [Enddate], [Cost]) VALUES (3, N'Manchester Foundation', CAST(N'2016-04-02T00:00:00.000' AS DateTime), CAST(N'2017-01-01T00:00:00.000' AS DateTime), CAST(20000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Project] ([id], [Name], [Startdate], [Enddate], [Cost]) VALUES (4, N'Chelsea''s Funeral', CAST(N'2015-01-01T00:00:00.000' AS DateTime), CAST(N'2015-02-02T00:00:00.000' AS DateTime), CAST(1000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectEmployee] ON 
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (1, 1, 3)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (2, 1, 4)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (3, 1, 5)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (4, 2, 3)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (5, 2, 12)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (6, 3, 12)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (7, 3, 11)
GO
INSERT [dbo].[ProjectEmployee] ([id], [ProjectID], [EmployeeID]) VALUES (8, 3, 9)
GO
SET IDENTITY_INSERT [dbo].[ProjectEmployee] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([id], [Name], [Surname], [Username], [Password], [Role], [Active]) VALUES (7, N'David', N'Beckham', N'davidb', N'goldenballs', 1, 1)
GO
INSERT [dbo].[User] ([id], [Name], [Surname], [Username], [Password], [Role], [Active]) VALUES (8, N'Ryan', N'Giggs', N'ryang', N'runningdownthewing', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_E_JTID] FOREIGN KEY([JobTitleId])
REFERENCES [dbo].[JobTitle] ([id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_E_JTID]
GO
ALTER TABLE [dbo].[EmployeeSkill]  WITH CHECK ADD  CONSTRAINT [FK_ES_E_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([id])
GO
ALTER TABLE [dbo].[EmployeeSkill] CHECK CONSTRAINT [FK_ES_E_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeSkill]  WITH CHECK ADD  CONSTRAINT [FK_ES_S_SkillID] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([id])
GO
ALTER TABLE [dbo].[EmployeeSkill] CHECK CONSTRAINT [FK_ES_S_SkillID]
GO
ALTER TABLE [dbo].[ProjectEmployee]  WITH CHECK ADD  CONSTRAINT [FK_PE_E_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([id])
GO
ALTER TABLE [dbo].[ProjectEmployee] CHECK CONSTRAINT [FK_PE_E_EmployeeID]
GO
ALTER TABLE [dbo].[ProjectEmployee]  WITH CHECK ADD  CONSTRAINT [FK_PE_P_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([id])
GO
ALTER TABLE [dbo].[ProjectEmployee] CHECK CONSTRAINT [FK_PE_P_ProjectID]
GO
USE [master]
GO
ALTER DATABASE [CodeWorks] SET  READ_WRITE 
GO
