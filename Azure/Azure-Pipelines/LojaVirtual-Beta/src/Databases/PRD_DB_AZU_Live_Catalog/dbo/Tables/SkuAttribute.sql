CREATE TABLE [dbo].[SkuAttribute] (
    [SkuAttributeId]  BIGINT          NOT NULL  IDENTITY (1, 1) ,
    [ProductSkuId]    VARCHAR (26)    NOT NULL,    
    [Name]            VARCHAR (100)   NOT NULL,
    [Value]           VARCHAR (8000)  NOT NULL,
    [CreatedDate]     DATETIME        NOT NULL  CONSTRAINT [DF_SkuAttribute_CreatedDate] DEFAULT (getdate()),
    [UpdatedDate]     DATETIME        NULL,
    CONSTRAINT [PK_SkuAttribute] PRIMARY KEY CLUSTERED ([SkuAttributeId] ASC),    
    CONSTRAINT [FK_SkuAttribute_ProductSku] FOREIGN KEY ([ProductSkuId]) REFERENCES [dbo].[ProductSku] ([ProductSkuId]),
    CONSTRAINT [UK_SkuAttribute_01] UNIQUE NONCLUSTERED ([ProductSkuId] ASC, [Name] ASC)
)
