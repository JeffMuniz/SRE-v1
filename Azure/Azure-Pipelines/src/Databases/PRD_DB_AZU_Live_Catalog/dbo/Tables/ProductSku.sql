CREATE TABLE [dbo].[ProductSku] (
    [ProductSkuId]  VARCHAR (26)  NOT NULL,
    [ProductId]     VARCHAR (26)  NOT NULL,
    [Description]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ProductSku] PRIMARY KEY CLUSTERED ([ProductSkuId] ASC),
    CONSTRAINT [FK_ProductSku_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId])
);

