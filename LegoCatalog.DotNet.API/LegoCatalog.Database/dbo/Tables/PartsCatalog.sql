CREATE TABLE [dbo].[PartsCatalog] (
    [PartId]         INT             IDENTITY (10000, 1) NOT NULL,
    [ItemId]         NVARCHAR (255)  NOT NULL,
    [ItemTypeId]     NCHAR (10)      NOT NULL,
    [ItemName]       NVARCHAR (2000) NOT NULL,
    [CategoryId]     INT             NULL,
    [ItemWeight]     DECIMAL (10, 3) NULL,
    [ItemDimensionX] DECIMAL (10, 3) NULL,
    [ItemDimensionY] DECIMAL (10, 3) NULL,
    [ItemDimensionZ] DECIMAL (10, 3) NULL,
    [ImageLink]      NVARCHAR (500)  NULL,
    [IconLink]       NVARCHAR (500)  NULL,
    [Quantity] INT NOT NULL DEFAULT 0, 
    [IconLinkJpeg] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_PartsCatalog] PRIMARY KEY CLUSTERED ([ItemId] ASC)
);

