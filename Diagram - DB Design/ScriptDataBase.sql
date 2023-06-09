USE [master]
GO
/****** Object:  Database [DBTimerManagement]    Script Date: 23/4/2023 21:55:48 ******/
CREATE DATABASE [DBTimerManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBTimerManagement', FILENAME = N'/var/opt/mssql/data/DBTimerManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBTimerManagement_log', FILENAME = N'/var/opt/mssql/data/DBTimerManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBTimerManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBTimerManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBTimerManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBTimerManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBTimerManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBTimerManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBTimerManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBTimerManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBTimerManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBTimerManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBTimerManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBTimerManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBTimerManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBTimerManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBTimerManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBTimerManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBTimerManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBTimerManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBTimerManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBTimerManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBTimerManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBTimerManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBTimerManagement] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DBTimerManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBTimerManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [DBTimerManagement] SET  MULTI_USER 
GO
ALTER DATABASE [DBTimerManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBTimerManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBTimerManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBTimerManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBTimerManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBTimerManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBTimerManagement] SET QUERY_STORE = OFF
GO
USE [DBTimerManagement]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryItems]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryItems](
	[CategoryItemId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CategoryItems] PRIMARY KEY CLUSTERED 
(
	[CategoryItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriorityItems]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriorityItems](
	[PriorityItemId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PriorityItems] PRIMARY KEY CLUSTERED 
(
	[PriorityItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusItems]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusItems](
	[StatusItemId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_StatusItems] PRIMARY KEY CLUSTERED 
(
	[StatusItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskItems]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskItems](
	[TaskItemId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastEditionDate] [datetime2](7) NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[CategoryItemId] [int] NOT NULL,
	[StatusItemId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PriorityItemId] [int] NULL,
 CONSTRAINT [PK_TaskItems] PRIMARY KEY CLUSTERED 
(
	[TaskItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeItems]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeItems](
	[TimeItemId] [int] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[TaskItemId] [int] NOT NULL,
 CONSTRAINT [PK_TimeItems] PRIMARY KEY CLUSTERED 
(
	[TimeItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/4/2023 21:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsArchive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230423004703_mymigration', N'7.0.5')
GO
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (1, N'Informe')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (2, N'Cotizaciones')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (3, N'Ofertas')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (4, N'Diseños y Planos')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (5, N'Reuniones Internas')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (6, N'Visitas')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (7, N'Planeamiento')
INSERT [dbo].[CategoryItems] ([CategoryItemId], [Name]) VALUES (8, N'Otros')
GO
INSERT [dbo].[PriorityItems] ([PriorityItemId], [Name]) VALUES (1, N'Urgente')
INSERT [dbo].[PriorityItems] ([PriorityItemId], [Name]) VALUES (2, N'Prioridad Alta')
INSERT [dbo].[PriorityItems] ([PriorityItemId], [Name]) VALUES (3, N'Prioridad Media')
INSERT [dbo].[PriorityItems] ([PriorityItemId], [Name]) VALUES (4, N'Prioridad Baja')
INSERT [dbo].[PriorityItems] ([PriorityItemId], [Name]) VALUES (5, N'No es urgente')
GO
INSERT [dbo].[StatusItems] ([StatusItemId], [Name]) VALUES (1, N'Finalizado')
INSERT [dbo].[StatusItems] ([StatusItemId], [Name]) VALUES (2, N'Pendiente')
INSERT [dbo].[StatusItems] ([StatusItemId], [Name]) VALUES (3, N'En revisión')
INSERT [dbo].[StatusItems] ([StatusItemId], [Name]) VALUES (4, N'En revisión de terceros')
INSERT [dbo].[StatusItems] ([StatusItemId], [Name]) VALUES (5, N'Aplazado')
GO
INSERT [dbo].[TaskItems] ([TaskItemId], [Title], [Description], [CreationDate], [LastEditionDate], [IsEnable], [CategoryItemId], [StatusItemId], [UserId], [PriorityItemId]) VALUES (1, N'Prueba', N'Ejemplo', CAST(N'2020-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-02T00:00:00.0000000' AS DateTime2), 1, 1, 1, 1, 1)
INSERT [dbo].[TaskItems] ([TaskItemId], [Title], [Description], [CreationDate], [LastEditionDate], [IsEnable], [CategoryItemId], [StatusItemId], [UserId], [PriorityItemId]) VALUES (2, N'Ejemplo', N'Esto es una prueba de ejemplo', CAST(N'2023-04-21T00:00:00.0000000' AS DateTime2), CAST(N'2023-04-22T00:00:00.0000000' AS DateTime2), 1, 8, 1, 1, 5)
INSERT [dbo].[TaskItems] ([TaskItemId], [Title], [Description], [CreationDate], [LastEditionDate], [IsEnable], [CategoryItemId], [StatusItemId], [UserId], [PriorityItemId]) VALUES (3, N'Ejemplo', N'Esto es una prueba de ejemplo', CAST(N'2023-04-21T00:00:00.0000000' AS DateTime2), CAST(N'2023-04-22T00:00:00.0000000' AS DateTime2), 1, 1, 2, 1, 5)
INSERT [dbo].[TaskItems] ([TaskItemId], [Title], [Description], [CreationDate], [LastEditionDate], [IsEnable], [CategoryItemId], [StatusItemId], [UserId], [PriorityItemId]) VALUES (4, N'Informe Globalvia', N'Redacción Informe Globalvia', CAST(N'2023-04-21T00:00:00.0000000' AS DateTime2), CAST(N'2023-04-22T00:00:00.0000000' AS DateTime2), 1, 1, 1, 1, 3)
GO
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (1, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (2, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (3, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (4, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (5, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (6, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (7, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (8, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (9, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (10, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (11, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (12, CAST(N'2023-04-23T07:10:00.0000000' AS DateTime2), CAST(N'2023-04-23T09:00:00.0000000' AS DateTime2), N'Ejemplo', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (13, CAST(N'2023-04-23T02:05:00.0000000' AS DateTime2), CAST(N'2023-04-23T07:09:00.0000000' AS DateTime2), N'', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (14, CAST(N'2023-04-23T07:25:00.0000000' AS DateTime2), CAST(N'2023-04-23T17:08:00.0000000' AS DateTime2), N'pruebas', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (15, CAST(N'2023-04-23T01:00:00.0000000' AS DateTime2), CAST(N'2023-04-23T23:00:00.0000000' AS DateTime2), N'', 1)
INSERT [dbo].[TimeItems] ([TimeItemId], [StartTime], [EndTime], [Notes], [TaskItemId]) VALUES (16, CAST(N'2023-04-23T01:00:00.0000000' AS DateTime2), CAST(N'2023-04-23T02:30:00.0000000' AS DateTime2), N'Pruebas', 3)
GO
INSERT [dbo].[Users] ([UserId], [Password], [Name], [LastName], [IsEnable], [IsAdmin], [IsArchive]) VALUES (1, N'Pass123', N'Admin', N'', 1, 1, 0)
GO
/****** Object:  Index [IX_TaskItems_CategoryItemId]    Script Date: 23/4/2023 21:55:50 ******/
CREATE NONCLUSTERED INDEX [IX_TaskItems_CategoryItemId] ON [dbo].[TaskItems]
(
	[CategoryItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TaskItems_PriorityItemId]    Script Date: 23/4/2023 21:55:50 ******/
CREATE NONCLUSTERED INDEX [IX_TaskItems_PriorityItemId] ON [dbo].[TaskItems]
(
	[PriorityItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TaskItems_StatusItemId]    Script Date: 23/4/2023 21:55:50 ******/
CREATE NONCLUSTERED INDEX [IX_TaskItems_StatusItemId] ON [dbo].[TaskItems]
(
	[StatusItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TaskItems_UserId]    Script Date: 23/4/2023 21:55:50 ******/
CREATE NONCLUSTERED INDEX [IX_TaskItems_UserId] ON [dbo].[TaskItems]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeItems_TaskItemId]    Script Date: 23/4/2023 21:55:50 ******/
CREATE NONCLUSTERED INDEX [IX_TimeItems_TaskItemId] ON [dbo].[TimeItems]
(
	[TaskItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_CategoryItems_CategoryItemId] FOREIGN KEY([CategoryItemId])
REFERENCES [dbo].[CategoryItems] ([CategoryItemId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_CategoryItems_CategoryItemId]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_PriorityItems_PriorityItemId] FOREIGN KEY([PriorityItemId])
REFERENCES [dbo].[PriorityItems] ([PriorityItemId])
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_PriorityItems_PriorityItemId]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_StatusItems_StatusItemId] FOREIGN KEY([StatusItemId])
REFERENCES [dbo].[StatusItems] ([StatusItemId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_StatusItems_StatusItemId]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_Users_UserId]
GO
ALTER TABLE [dbo].[TimeItems]  WITH CHECK ADD  CONSTRAINT [FK_TimeItems_TaskItems_TaskItemId] FOREIGN KEY([TaskItemId])
REFERENCES [dbo].[TaskItems] ([TaskItemId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeItems] CHECK CONSTRAINT [FK_TimeItems_TaskItems_TaskItemId]
GO
USE [master]
GO
ALTER DATABASE [DBTimerManagement] SET  READ_WRITE 
GO
