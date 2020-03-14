CREATE TABLE [dbo].[Events] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Owner]       NVARCHAR (255) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [EventDate]   DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([ID] ASC)
);

