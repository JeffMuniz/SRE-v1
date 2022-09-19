CREATE TABLE [Enriched].[Sku] (
    [SkuId]         VARCHAR (26)  NOT NULL,    
    [Name]          VARCHAR (200) NULL,    
    [Hash]          VARCHAR (36)  NULL,
    [CreatedDate]   DATETIME      NOT NULL CONSTRAINT [DF_Enriched.Sku_CreatedDate] DEFAULT (getdate()),
    [UpdatedDate]   DATETIME      NULL,
    CONSTRAINT [PK_Enriched.Sku] PRIMARY KEY CLUSTERED ([SkuId] ASC),
    CONSTRAINT [FK_Enriched.Sku_ProductSku] FOREIGN KEY ([SkuId]) REFERENCES [dbo].[ProductSku] ([ProductSkuId])
);

