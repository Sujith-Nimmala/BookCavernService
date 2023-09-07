CREATE TABLE [dbo].[OrderDetails] (
    [OrderId]  INT   NULL,
    [BookId]   INT   NULL,
    [Quantity] INT   NULL,
    [Cost]     MONEY NULL,
    [Index] INT IDENTITY(1,1) NOT NULL, 
    CONSTRAINT [orderfk] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [bookfk] FOREIGN KEY ([BookId]) REFERENCES [dbo].[BookDetails] ([BookId]), 
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Index])
);

