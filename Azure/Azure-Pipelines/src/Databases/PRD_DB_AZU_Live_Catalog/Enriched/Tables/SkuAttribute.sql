CREATE TABLE [Enriched].[SkuAttribute] (
    [SkuAttributeId]  BIGINT          NOT NULL  IDENTITY (1, 1) ,
    [SkuId]           VARCHAR (26)    NOT NULL,    
    [Name]            VARCHAR (100)   NOT NULL,
    [Value]           VARCHAR (8000)  NOT NULL,
    [CreatedDate]     DATETIME        NOT NULL CONSTRAINT [DF_Enriched.SkuAttribute_CreatedDate] DEFAULT (getdate()),
    [UpdatedDate]     DATETIME        NULL,
    CONSTRAINT [PK_Enriched.SkuAttribute] PRIMARY KEY CLUSTERED ([SkuAttributeId] ASC),    
    CONSTRAINT [FK_Enriched.SkuAttribute_Enriched.Sku] FOREIGN KEY ([SkuId]) REFERENCES [Enriched].[Sku] ([SkuId]),
    CONSTRAINT [UK_Enriched.SkuAttribute_01] UNIQUE NONCLUSTERED ([SkuId] ASC, [Name] ASC)
);

