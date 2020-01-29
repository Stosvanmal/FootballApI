USE [FutbolAPI]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 29/01/2020 22:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NOT NULL,
	[teamName] [varchar](150) NOT NULL,
	[yellowCards] [int] NULL,
	[redCards] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Match]    Script Date: 29/01/2020 22:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Match](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NOT NULL,
	[idHomeManager] [int] NULL,
	[idAwayManager] [int] NULL,
	[idreferee] [int] NULL,
	[date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatchPlayerAway]    Script Date: 29/01/2020 22:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchPlayerAway](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idplayer] [int] NULL,
	[idmatch] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatchPlayerHome]    Script Date: 29/01/2020 22:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchPlayerHome](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idplayer] [int] NULL,
	[idmatch] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 29/01/2020 22:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NOT NULL,
	[teamName] [varchar](150) NOT NULL,
	[yellowCards] [int] NULL,
	[redCards] [int] NULL,
	[number] [int] NULL,
	[minutesPlayed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Referee]    Script Date: 29/01/2020 22:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](150) NOT NULL,
	[minutesPlayed] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Manager] FOREIGN KEY([idAwayManager])
REFERENCES [dbo].[Manager] ([id])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [FK_Match_Manager]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Manager1] FOREIGN KEY([idHomeManager])
REFERENCES [dbo].[Manager] ([id])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [FK_Match_Manager1]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Referee] FOREIGN KEY([idreferee])
REFERENCES [dbo].[Referee] ([id])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [FK_Match_Referee]
GO
ALTER TABLE [dbo].[MatchPlayerAway]  WITH CHECK ADD  CONSTRAINT [FK_MatchPlayerAway_Match] FOREIGN KEY([idmatch])
REFERENCES [dbo].[Match] ([id])
GO
ALTER TABLE [dbo].[MatchPlayerAway] CHECK CONSTRAINT [FK_MatchPlayerAway_Match]
GO
ALTER TABLE [dbo].[MatchPlayerAway]  WITH CHECK ADD  CONSTRAINT [FK_MatchPlayerAway_Player] FOREIGN KEY([idplayer])
REFERENCES [dbo].[Player] ([id])
GO
ALTER TABLE [dbo].[MatchPlayerAway] CHECK CONSTRAINT [FK_MatchPlayerAway_Player]
GO
ALTER TABLE [dbo].[MatchPlayerHome]  WITH CHECK ADD  CONSTRAINT [FK_MatchPlayerHome_Match] FOREIGN KEY([idmatch])
REFERENCES [dbo].[Match] ([id])
GO
ALTER TABLE [dbo].[MatchPlayerHome] CHECK CONSTRAINT [FK_MatchPlayerHome_Match]
GO
ALTER TABLE [dbo].[MatchPlayerHome]  WITH CHECK ADD  CONSTRAINT [FK_MatchPlayerHome_Player] FOREIGN KEY([idplayer])
REFERENCES [dbo].[Player] ([id])
GO
ALTER TABLE [dbo].[MatchPlayerHome] CHECK CONSTRAINT [FK_MatchPlayerHome_Player]
GO
