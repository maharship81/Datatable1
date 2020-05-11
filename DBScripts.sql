USE [MVCTutorial]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 06-12-2016 00:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
 [DepartmentId] [int] IDENTITY(1,1) NOT NULL,
 [DepartmentName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
 [DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 06-12-2016 00:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
 [EmployeeId] [int] IDENTITY(1,1) NOT NULL,
 [Name] [varchar](50) NULL,
 [DepartmentId] [int] NULL,
 [Address] [varchar](150) NULL,
 [IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
 [EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sites]    Script Date: 06-12-2016 00:24:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
 [SiteId] [int] IDENTITY(1,1) NOT NULL,
 [EmployeeId] [int] NULL,
 [SiteName] [nvarchar](150) NULL,
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
 [SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Department] ON 

GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1, N'IT')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (2, N'QA')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (3, N'Development ')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (4, N'Marketing')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

GO
INSERT [dbo].[Employee] ([EmployeeId], [Name], [DepartmentId], [Address], [IsDeleted]) VALUES (1, N'Ashish', 1, N'India', 0)
GO
INSERT [dbo].[Employee] ([EmployeeId], [Name], [DepartmentId], [Address], [IsDeleted]) VALUES (2, N'John', 2, N'London', 0)
GO
INSERT [dbo].[Employee] ([EmployeeId], [Name], [DepartmentId], [Address], [IsDeleted]) VALUES (3, N'Methew', 3, N'NewYork', 0)
GO
INSERT [dbo].[Employee] ([EmployeeId], [Name], [DepartmentId], [Address], [IsDeleted]) VALUES (4, N'Brano', 4, N'France', 0)
GO
INSERT [dbo].[Employee] ([EmployeeId], [Name], [DepartmentId], [Address], [IsDeleted]) VALUES (5, N'Smith', 1, N'London', 0)
GO
INSERT [dbo].[Employee] ([EmployeeId], [Name], [DepartmentId], [Address], [IsDeleted]) VALUES (6, N'Sara', 4, N'New york', 0)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Sites] ON 

GO
INSERT [dbo].[Sites] ([SiteId], [EmployeeId], [SiteName]) VALUES (1, 1005, N'www.google.com')
GO
INSERT [dbo].[Sites] ([SiteId], [EmployeeId], [SiteName]) VALUES (2, 1006, N'www.facebook.com/technotipstutorial')
GO
INSERT [dbo].[Sites] ([SiteId], [EmployeeId], [SiteName]) VALUES (3, 1007, NULL)
GO
INSERT [dbo].[Sites] ([SiteId], [EmployeeId], [SiteName]) VALUES (4, 1008, N'www.youtube.com/user/aapkanigam')
GO
INSERT [dbo].[Sites] ([SiteId], [EmployeeId], [SiteName]) VALUES (5, 1009, N'www.google.com')
GO
INSERT [dbo].[Sites] ([SiteId], [EmployeeId], [SiteName]) VALUES (6, 1010, N'fb.com/technotipstutorial')
GO
SET IDENTITY_INSERT [dbo].[Sites] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Sites]  WITH CHECK ADD  CONSTRAINT [FK_Sites_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Sites] CHECK CONSTRAINT [FK_Sites_Employee]
GO