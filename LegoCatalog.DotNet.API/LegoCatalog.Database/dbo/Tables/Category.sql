CREATE TABLE [dbo].[Category] (
    [CategoryId]  INT            NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [SubCategory] NVARCHAR (255) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

