USE [master]
GO
/****** Object:  Database [UserManipulation]    Script Date: 5/24/2024 5:03:40 PM ******/
CREATE DATABASE [UserManipulation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UserManipulation', FILENAME = N'C:\DATA\UserManipulation.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UserManipulation_log', FILENAME = N'C:\DATA\UserManipulation_log.ldf' , SIZE = 5120KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [UserManipulation] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UserManipulation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UserManipulation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UserManipulation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UserManipulation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UserManipulation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UserManipulation] SET ARITHABORT OFF 
GO
ALTER DATABASE [UserManipulation] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UserManipulation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UserManipulation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UserManipulation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UserManipulation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UserManipulation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UserManipulation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UserManipulation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UserManipulation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UserManipulation] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UserManipulation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UserManipulation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UserManipulation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UserManipulation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UserManipulation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UserManipulation] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UserManipulation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UserManipulation] SET RECOVERY FULL 
GO
ALTER DATABASE [UserManipulation] SET  MULTI_USER 
GO
ALTER DATABASE [UserManipulation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UserManipulation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UserManipulation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UserManipulation] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UserManipulation] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UserManipulation] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [UserManipulation] SET QUERY_STORE = OFF
GO
USE [UserManipulation]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/24/2024 5:03:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 5/24/2024 5:03:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AccessToken] [varchar](2000) NOT NULL,
	[RefreshToken] [varchar](2000) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[AccessTokenExpiration] [datetime] NOT NULL,
	[RefreshTokenExpiration] [datetime] NOT NULL,
 CONSTRAINT [PK_UserToken_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [CreatedAt], [Modified]) VALUES (1, N'09195512635', N'ALN1xui0FuwKrNrCJAIArf8/evLe01S37hGI1XgvnEgvbxTAwksmnDlHvztQGKCHcA==', N'Afshin', N'Razaghi', CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [CreatedAt], [Modified]) VALUES (2, N'09093366523', N'ALN1xui0FuwKrNrCJAIArf8/evLe01S37hGI1XgvnEgvbxTAwksmnDlHvztQGKCHcA==', N'Ali', N'Afshar', CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [CreatedAt], [Modified]) VALUES (3, N'09065541251', N'ALN1xui0FuwKrNrCJAIArf8/evLe01S37hGI1XgvnEgvbxTAwksmnDlHvztQGKCHcA==', N'Mehdi', N'Sharbati', CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[User] ([Id], [UserName], [Password], [FirstName], [LastName], [CreatedAt], [Modified]) VALUES (4, N'09125566859', N'ALN1xui0FuwKrNrCJAIArf8/evLe01S37hGI1XgvnEgvbxTAwksmnDlHvztQGKCHcA==', N'Morteza', N'Yazdani', CAST(N'2024-05-24T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[UserToken] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
/****** Object:  StoredProcedure [dbo].[GetFirstUser]    Script Date: 5/24/2024 5:03:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Afshin
-- Create date: 5/24/2024
-- Description:	Get First User Of Users
-- =============================================
CREATE PROCEDURE [dbo].[GetFirstUser]
	
AS
BEGIN
	SELECT Top(1) * FROM [User]
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserList]    Script Date: 5/24/2024 5:03:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Afshin Razaghi
-- Create date: 5/24/2024
-- Description:	Get All Users List
-- =============================================
CREATE PROCEDURE [dbo].[GetUserList]
	
AS
BEGIN
	Select * From [User]
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserWithUserName]    Script Date: 5/24/2024 5:03:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Afshin Razaghi
-- Create date: 05/23/2024
-- Description:	Get User With User Name
-- =============================================
CREATE PROCEDURE [dbo].[GetUserWithUserName](
	@UserName varchar(255) 
)
AS
BEGIN
	Select * From [User] where UserName = @UserName
END
GO
USE [master]
GO
ALTER DATABASE [UserManipulation] SET  READ_WRITE 
GO
