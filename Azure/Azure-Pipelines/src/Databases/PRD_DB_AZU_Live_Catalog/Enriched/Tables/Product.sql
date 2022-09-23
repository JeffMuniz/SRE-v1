CREATE TABLE [Enriched].[Product] (
    [ProductId]   VARCHAR (26)  NOT NULL,
    [Entity]      VARCHAR (100) NOT NULL,
    [Name]        VARCHAR (200) NULL,
    [Hash]        VARCHAR (36)  NULL,
    [CreatedDate] DATETIME      NOT NULL  CONSTRAINT [DF_Enriched.Product_CreatedDate] DEFAULT (getdate()),
    [UpdatedDate] DATETIME      NULL,    
    CONSTRAINT [PK_Enriched.Product] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_Enriched.Product_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId])
);

