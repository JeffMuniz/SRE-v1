CREATE TABLE [Enriched].[Supplier] (
    [SupplierId]      BIGINT        NOT NULL,
    [Name]            VARCHAR (80)  NOT NULL,
    [SupplierTypeId]  INT           NOT NULL,
    [Active]          BIT           NOT NULL, 
    [CreatedDate]     DATETIME      NOT NULL CONSTRAINT [DF_Enriched.Supplier_CreatedDate] DEFAULT (getdate()),
    [UpdatedDate]     DATETIME      NULL,    
    CONSTRAINT [PK_Enriched.Supplier] PRIMARY KEY CLUSTERED ([SupplierId] ASC),
    CONSTRAINT [UK_Enriched.Supplier_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);
