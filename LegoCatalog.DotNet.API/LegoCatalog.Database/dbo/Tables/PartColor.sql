CREATE TABLE [dbo].[PartColor] (
    [PartColorId] INT            IDENTITY (10000, 1) NOT NULL,
    [ItemId]      NVARCHAR (50)  NOT NULL,
    [Color]       NVARCHAR (255) NOT NULL,
    [CodeName]    INT            NOT NULL,
    CONSTRAINT [PK_PartColor] PRIMARY KEY CLUSTERED ([PartColorId] ASC)
);

