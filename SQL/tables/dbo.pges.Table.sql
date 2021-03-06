/****** Object:  Table [dbo].[pges] RAD PDF Version: 2.19+ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pges](
	[pdfID] [int] NOT NULL,
	[docID] [int] NOT NULL,
	[keyID] [int] NOT NULL,
	[pgeDate] [datetime] NOT NULL,
	[pgeStatus] [int] NOT NULL,
	[pgeNumber] [int] NOT NULL,
	[pgeImageType] [int] NOT NULL,
	[pgeImageData] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
