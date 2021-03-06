/****** Object:  Table [dbo].[keys] RAD PDF Version: 2.19+ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[keys](
	[keyID] [int] IDENTITY(1,1) NOT NULL,
	[docID] [int] NOT NULL,
	[keyData] [char](25) COLLATE Latin1_General_CS_AS NOT NULL,
	[keyExpires] [datetime] NOT NULL,
	[keySettings] [int] NOT NULL,
 CONSTRAINT [PK_keys] PRIMARY KEY CLUSTERED 
(
	[keyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
