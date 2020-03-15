CREATE TABLE [dbo].[ItemTypes] (
    [ItemTypeId]   NCHAR (10)    NOT NULL,
    [ItemTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ItemTypes] PRIMARY KEY CLUSTERED ([ItemTypeId] ASC)
);

