/****** Object:  Table [dbo].[objs] RAD PDF Version: 2.19+ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[objs](
	[objID] [int] IDENTITY(1,1) NOT NULL,
	[docID] [int] NOT NULL,
	[objType] [int] NOT NULL,
	[objSettings] [int] NOT NULL,
	[objData] [varbinary](max) NOT NULL,
	[objDataLength] [int] NOT NULL,
 CONSTRAINT [PK_objs] PRIMARY KEY CLUSTERED 
(
	[objID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
