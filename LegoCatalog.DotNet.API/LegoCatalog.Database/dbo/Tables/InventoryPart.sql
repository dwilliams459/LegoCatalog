CREATE TABLE [dbo].[InventoryPart]
(
    [InventoryPartId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemId] VARCHAR(255) NOT NULL, 
    [Color] VARCHAR(255) NULL, 
    [Quantity] INT NOT NULL DEFAULT 0, 
    [UserId] INT NOT NULL DEFAULT 1
)
