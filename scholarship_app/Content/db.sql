create database scholarship;
USE [scholarship]
GO
/****** Object:  Table [dbo].[scholarship_details]    Script Date: 9/7/2020 1:30:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[scholarship_details](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[student_id] [bigint] NOT NULL,
	[amount] [bigint] NULL,
	[address] [varchar](100) NULL,
	[reason] [varchar](200) NULL,
	[status] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[student_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[student]    Script Date: 9/7/2020 1:30:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[scholarship_details]  WITH CHECK ADD  CONSTRAINT [FK_scholarship_details] FOREIGN KEY([student_id])
REFERENCES [dbo].[student] ([Id])
GO
ALTER TABLE [dbo].[scholarship_details] CHECK CONSTRAINT [FK_scholarship_details]
GO
