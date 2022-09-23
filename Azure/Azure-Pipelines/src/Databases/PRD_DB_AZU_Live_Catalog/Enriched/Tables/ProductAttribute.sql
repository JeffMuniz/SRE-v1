CREATE TABLE [Enriched].[ProductAttribute] (
    [ProductAttributeId]  BIGINT          NOT NULL  IDENTITY (1, 1) ,
    [ProductId]           VARCHAR (26)    NOT NULL,    
    [Name]                VARCHAR (100)   NOT NULL,
    [Value]               VARCHAR (8000)  NOT NULL,
    [CreatedDate]         DATETIME        NOT NULL CONSTRAINT [DF_Enriched.ProductAttribute_CreatedDate] DEFAULT (getdate()),
    [UpdatedDate]         DATETIME        NULL,
    CONSTRAINT [PK_Enriched.ProductAttribute] PRIMARY KEY CLUSTERED ([ProductAttributeId] ASC),    
    CONSTRAINT [FK_Enriched.ProductAttribute_Enriched.Product] FOREIGN KEY ([ProductId]) REFERENCES [Enriched].[Product] ([ProductId]),
    CONSTRAINT [UK_Enriched.ProductAttribute_01] UNIQUE NONCLUSTERED ([ProductId] ASC, [Name] ASC)
);

