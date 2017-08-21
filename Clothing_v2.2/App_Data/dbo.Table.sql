CREATE TABLE [dbo].[Profile]
(
	[Id]         INT        NOT NULL,
    [Growth]     FLOAT (53) NULL,
    [Weight]     FLOAT (53) NULL,
    [Bust]       FLOAT (53) NULL,
    [Waist]      FLOAT (53) NULL,
    [Hip]        FLOAT (53) NULL,
    [Shoes_size] FLOAT (53) NULL,
    [Data]       DATETIME   NULL,
    [SkinColor]  NCHAR (10) NULL,
    [HairColor]  NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
