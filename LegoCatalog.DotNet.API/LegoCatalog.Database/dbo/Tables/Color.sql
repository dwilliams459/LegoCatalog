CREATE TABLE [dbo].[Color] (
    [ColorId]   INT            NOT NULL,
    [ColorName] NVARCHAR (150) NOT NULL,
    [RGB]       NCHAR (10)     NULL,
    [Type]      NVARCHAR (50)  NOT NULL,
    [Parts]     INT            NULL,
    [InSets]    INT            NULL,
    [Wanted]    INT            NULL,
    [ForSale]   INT            NULL,
    [YearFrom]  INT            NULL,
    [YearTo]    INT            NULL,
    CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED ([ColorId] ASC)
);

