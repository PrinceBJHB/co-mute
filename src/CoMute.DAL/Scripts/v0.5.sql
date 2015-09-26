USE [master]
GO
/****** Object:  Database [CoMute]    Script Date: 2015/09/26 17:29:35 ******/
CREATE DATABASE [CoMute]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoMute', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CoMute.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CoMute_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CoMute_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CoMute] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoMute].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoMute] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoMute] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoMute] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoMute] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoMute] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoMute] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CoMute] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CoMute] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoMute] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoMute] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoMute] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoMute] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoMute] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoMute] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoMute] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoMute] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CoMute] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoMute] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoMute] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoMute] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoMute] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoMute] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoMute] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoMute] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CoMute] SET  MULTI_USER 
GO
ALTER DATABASE [CoMute] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoMute] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoMute] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoMute] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [CoMute]
GO
/****** Object:  StoredProcedure [dbo].[usp_CarPool_Create]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robin Peens
-- Create date: 2015-09-25
-- Description:	Insert Car Pool
-- =============================================
CREATE PROCEDURE [dbo].[usp_CarPool_Create]
	@UserID						INT,
	@departureTime				TIME(7),
	@expectedArrivalTime		TIME(7),
	@origin						VARCHAR(MAX),
	@destination				VARCHAR(MAX),
	@seatsAvailable				INT,
	@notes						VARCHAR(MAX),
	@days						VARCHAR(12)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CarPoolID			INT;

	INSERT INTO CarPools
		(
			UserID,
			departureTime,
			expectedArrivalTime,
			origin,
			destination,
			seatsAvailable,
			notes
		)
	VALUES
		(
			@UserID,
			@departureTime,
			@expectedArrivalTime,
			@origin,
			@destination,
			@seatsAvailable,
			@notes
		);

	SET @CarPoolID = SCOPE_IDENTITY();

	INSERT INTO CarPoolDays
		(
			CarPoolID,
			DayOfWeekID
		)
	SELECT
		@CarPoolID,
		dw.DayOfWeekID
	FROM
		DaysOfWeek dw
	WHERE
		dw.DayOfWeekID IN (
			SELECT
				CAST(d.Item as INT)
			FROM
				Split(@days, ',') d
		);

	SELECT CAST(CASE WHEN @@rowcount > 0 THEN 1 ELSE 0 END AS BIT) as Success;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_User_Login]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robin Peens
-- Create date: 2015-09-25
-- Description:	Login User
-- =============================================
CREATE PROCEDURE [dbo].[usp_User_Login] 
	@emailAddress VARCHAR(128), 
	@password VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		u.UserID,
		u.firstName,
		u.lastName,
		u.emailAddress,
		u.phoneNumber,
		u.active,
		u.created
	FROM
		Users u
	WHERE
		u.emailAddress LIKE @emailAddress
	AND
		u.password LIKE CONVERT(VARCHAR(32),HashBytes('MD5', @password),2);
END

GO
/****** Object:  StoredProcedure [dbo].[usp_User_Register]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robin Peens
-- Create date: 2015-09-25
-- Description:	Register a new User
-- =============================================
CREATE PROCEDURE [dbo].[usp_User_Register] 
	-- Add the parameters for the stored procedure here
	@firstName			VARCHAR(30), 
	@lastName			VARCHAR(30),
	@emailAddress		VARCHAR(128),
	@phoneNumber		VARCHAR(12),
	@password			VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Users
		(
			firstName,
			lastName,
			emailAddress,
			phoneNumber,
			password
		)
	VALUES
		(
			@firstName,
			@lastName,
			@emailAddress,
			@phoneNumber,
			CONVERT(VARCHAR(32),HashBytes('MD5', @password),2)
		);

	SELECT CAST(SCOPE_IDENTITY() AS INT) as UserID;
	
END

GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robin Peens
-- Create date: 2015-09-25
-- Description:	Split String
-- =============================================
CREATE FUNCTION [dbo].[Split] (
      @InputString                  VARCHAR(8000),
      @Delimiter                    VARCHAR(50)
)

RETURNS @Items TABLE (
      Item                          VARCHAR(8000)
)

AS
BEGIN
      IF @Delimiter = ' '
      BEGIN
            SET @Delimiter = ','
            SET @InputString = REPLACE(@InputString, ' ', @Delimiter)
      END

      IF (@Delimiter IS NULL OR @Delimiter = '')
            SET @Delimiter = ','

--INSERT INTO @Items VALUES (@Delimiter) -- Diagnostic
--INSERT INTO @Items VALUES (@InputString) -- Diagnostic

      DECLARE @Item                 VARCHAR(8000)
      DECLARE @ItemList       VARCHAR(8000)
      DECLARE @DelimIndex     INT

      SET @ItemList = @InputString
      SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      WHILE (@DelimIndex != 0)
      BEGIN
            SET @Item = SUBSTRING(@ItemList, 0, @DelimIndex)
            INSERT INTO @Items VALUES (@Item)

            -- Set @ItemList = @ItemList minus one less item
            SET @ItemList = SUBSTRING(@ItemList, @DelimIndex+1, LEN(@ItemList)-@DelimIndex)
            SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      END -- End WHILE

      IF @Item IS NOT NULL -- At least one delimiter was encountered in @InputString
      BEGIN
            SET @Item = @ItemList
            INSERT INTO @Items VALUES (@Item)
      END

      -- No delimiters were encountered in @InputString, so just return @InputString
      ELSE INSERT INTO @Items VALUES (@InputString)

      RETURN

END -- End Function


GO
/****** Object:  Table [dbo].[CarPoolDays]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarPoolDays](
	[CarPoolDayID] [int] IDENTITY(1,1) NOT NULL,
	[CarPoolID] [int] NOT NULL,
	[DayOfWeekID] [int] NOT NULL,
 CONSTRAINT [PK_CarPoolDays] PRIMARY KEY CLUSTERED 
(
	[CarPoolDayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CarPools]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CarPools](
	[CarPoolID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[departureTime] [time](7) NOT NULL,
	[expectedArrivalTime] [time](7) NOT NULL,
	[origin] [varchar](max) NOT NULL,
	[destination] [varchar](max) NOT NULL,
	[seatsAvailable] [int] NOT NULL,
	[notes] [varchar](max) NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_CarPools] PRIMARY KEY CLUSTERED 
(
	[CarPoolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DaysOfWeek]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DaysOfWeek](
	[DayOfWeekID] [int] NOT NULL,
	[nameOfDay] [varchar](10) NOT NULL,
 CONSTRAINT [PK_DayOfWeek] PRIMARY KEY CLUSTERED 
(
	[DayOfWeekID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserCarPools]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCarPools](
	[UserCarPoolID] [int] IDENTITY(1,1) NOT NULL,
	[CarPoolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserCarPools] PRIMARY KEY CLUSTERED 
(
	[UserCarPoolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2015/09/26 17:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](30) NOT NULL,
	[lastName] [varchar](30) NOT NULL,
	[emailAddress] [varchar](128) NOT NULL,
	[phoneNumber] [varchar](12) NOT NULL,
	[password] [varchar](32) NOT NULL,
	[active] [bit] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CarPoolDays] ON 

GO
INSERT [dbo].[CarPoolDays] ([CarPoolDayID], [CarPoolID], [DayOfWeekID]) VALUES (1, 1, 0)
GO
INSERT [dbo].[CarPoolDays] ([CarPoolDayID], [CarPoolID], [DayOfWeekID]) VALUES (2, 1, 1)
GO
INSERT [dbo].[CarPoolDays] ([CarPoolDayID], [CarPoolID], [DayOfWeekID]) VALUES (3, 1, 6)
GO
SET IDENTITY_INSERT [dbo].[CarPoolDays] OFF
GO
SET IDENTITY_INSERT [dbo].[CarPools] ON 

GO
INSERT [dbo].[CarPools] ([CarPoolID], [UserID], [departureTime], [expectedArrivalTime], [origin], [destination], [seatsAvailable], [notes], [created]) VALUES (1, 1, CAST(0x07007AD4BC5D0000 AS Time), CAST(0x0700F4A879BB0000 AS Time), N'asd', N'asd', 2, NULL, CAST(0x0000A51F00E77D4C AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CarPools] OFF
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (0, N'Sunday')
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (1, N'Monday')
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (2, N'Tuesday')
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (3, N'Wednesday')
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (4, N'Thursday')
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (5, N'Friday')
GO
INSERT [dbo].[DaysOfWeek] ([DayOfWeekID], [nameOfDay]) VALUES (6, N'Saturday')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([UserID], [firstName], [lastName], [emailAddress], [phoneNumber], [password], [active], [created]) VALUES (1, N'Robin', N'Peens', N'robinpeens@gmail.com', N'+27848155562', N'5F4DCC3B5AA765D61D8327DEB882CF99', 1, CAST(0x0000A51E00D72B8C AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_CarPoolDays]    Script Date: 2015/09/26 17:29:36 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CarPoolDays] ON [dbo].[CarPoolDays]
(
	[CarPoolID] ASC,
	[DayOfWeekID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserCarPools]    Script Date: 2015/09/26 17:29:36 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserCarPools] ON [dbo].[UserCarPools]
(
	[CarPoolID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_Email]    Script Date: 2015/09/26 17:29:36 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users_Email] UNIQUE NONCLUSTERED 
(
	[emailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CarPools] ADD  CONSTRAINT [DF_CarPools_seatsAvailable]  DEFAULT ((1)) FOR [seatsAvailable]
GO
ALTER TABLE [dbo].[CarPools] ADD  CONSTRAINT [DF_CarPools_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[CarPoolDays]  WITH CHECK ADD  CONSTRAINT [FK_CarPoolDays_CarPools] FOREIGN KEY([CarPoolID])
REFERENCES [dbo].[CarPools] ([CarPoolID])
GO
ALTER TABLE [dbo].[CarPoolDays] CHECK CONSTRAINT [FK_CarPoolDays_CarPools]
GO
ALTER TABLE [dbo].[CarPoolDays]  WITH CHECK ADD  CONSTRAINT [FK_CarPoolDays_DayOfWeek] FOREIGN KEY([DayOfWeekID])
REFERENCES [dbo].[DaysOfWeek] ([DayOfWeekID])
GO
ALTER TABLE [dbo].[CarPoolDays] CHECK CONSTRAINT [FK_CarPoolDays_DayOfWeek]
GO
ALTER TABLE [dbo].[CarPools]  WITH CHECK ADD  CONSTRAINT [FK_CarPools_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[CarPools] CHECK CONSTRAINT [FK_CarPools_Users]
GO
ALTER TABLE [dbo].[UserCarPools]  WITH CHECK ADD  CONSTRAINT [FK_UserCarPools_CarPools] FOREIGN KEY([CarPoolID])
REFERENCES [dbo].[CarPools] ([CarPoolID])
GO
ALTER TABLE [dbo].[UserCarPools] CHECK CONSTRAINT [FK_UserCarPools_CarPools]
GO
ALTER TABLE [dbo].[UserCarPools]  WITH CHECK ADD  CONSTRAINT [FK_UserCarPools_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserCarPools] CHECK CONSTRAINT [FK_UserCarPools_Users]
GO
USE [master]
GO
ALTER DATABASE [CoMute] SET  READ_WRITE 
GO
