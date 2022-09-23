USE PRD_DB_AZU_Live_Catalog
GO

IF (OBJECT_ID('tempdb..#TMP_Skus') IS NOT NULL) BEGIN
	DROP TABLE #TMP_Skus
END

IF (OBJECT_ID('TMP_JSON') IS NOT NULL) BEGIN
	DROP TABLE TMP_JSON
END
	
SELECT
	[ProductSkuId],
	[Category/Subcategory] = CONCAT([CategoryId], '-', [CategoryName], ' -> ', [SubcategoryId], '-', [SubcategoryName]),
	[RowNumber] = ROW_NUMBER() OVER (ORDER BY (SELECT 1))
INTO #TMP_Skus
FROM (
	SELECT TOP 1500
		ps.[SupplierId],
		ps.[OriginalProductSkuId],
		ps.ProductSkuId,		
		ps.[Name],		
		[CategoryId] = c.[Id],
		[CategoryName] = c.[Name], 
		[SubcategoryId] = s.[Id],
		[SubcategoryName] = s.[Name],		
		ps.[CreateDate],
		[Ordem] = ROW_NUMBER() OVER(PARTITION BY ps.SupplierId, ps.OriginalProductSkuId ORDER BY ps.[CreateDate] DESC)	
	FROM Product (nolock) AS p
	INNER JOIN ProductSku (nolock) AS ps ON p.ProductId = ps.ProductId
	INNER JOIN ProductSubcategory (nolock) AS psu ON p.ProductId = psu.ProductId
	INNER JOIN SubCategory (nolock) AS s ON psu.SubcategoryId = s.Id
	INNER JOIN Category (nolock) AS c ON s.CategoryId = c.Id
	--INNER JOIN ###TMP_Export_Skus tmp (nolock) ON tmp.ProductSkuId = ps.ProductSkuId
	WHERE
		ps.SkuStatusId = 1 -- Disponível
		AND s.Id = 34 -- "4-Bem estar -> 34-Perfumaria"
		AND (ps.[Name] like 'Perfume%' /*OR ps.[Name] like 'Smart TV%' OR ps.[Name] like 'Celular%' OR ps.[Name] like 'Telefone%'*/)	
	--ORDER BY		
	--	ps.[CreateDate] DESC,
	--	ps.[Name]
) AS Sku
WHERE
	[Ordem] = 1
ORDER BY	
	[SupplierId], 
	[CategoryName], 
	[SubcategoryName], 
	[CreateDate], 
	[OriginalProductSkuId]

CREATE NONCLUSTERED INDEX [IX_#TMP_Skus_1]
	ON #TMP_Skus ([ProductSkuId])

SELECT 
	[Product] = p.OriginalProductId,
	[Sku] = ps.OriginalProductSkuId,
	[SellerId] = CONVERT(VARCHAR(11), ps.SupplierId),	
	[SkuTitle] = ps.[Name],	
	[Description] = p.[Description],
	[Ean] = ps.EAN,	
	[Price] = ps.PriceFrom,
	[ListPrice] = ps.PriceFor,
	[Url] = NULL,
	[Images] = (
		CONCAT('[',
			STUFF(
				(				
					SELECT 
						CONCAT(',"',  LTRIM(RTRIM(si.LargeImage)), '"')
					FROM (
						SELECT DISTINCT [Order], si.LargeImage
						FROM SkuImage si (nolock)
						WHERE 
							si.ProductSkuId = ps.ProductSkuId AND
							LTRIM(RTRIM(si.LargeImage)) <> ''
					) as si
					ORDER BY [Order]
					FOR XML PATH('')
				)
			, 1, 1, ''), 
		']')
	),	
	[ProductAttributes] = (
		CONCAT('{',
			STUFF(
				(				
					SELECT
						CONCAT(', "', LTRIM(RTRIM(STRING_ESCAPE(pf.[Name], 'json'))), '":"', LTRIM(RTRIM(STRING_ESCAPE(pf.[Value], 'json'))), '"')
					FROM (
						SELECT DISTINCT pf.[Name], pf.[Value]
						FROM ProductFeature pf (nolock)
						INNER JOIN FeatureType (nolock) AS pft ON pft.FeatureTypeId = pf.FeatureTypeId
						WHERE pf.ProductId = p.ProductId
					) as pf
					ORDER BY pf.[Name], pf.[Value]
					FOR XML PATH('')
				)
			, 1, 1, ''), 
		'}')
	),		
	[SkuAttributes] = (
		CONCAT('{',
			STUFF(
				(				
					SELECT						
						CONCAT(', "', LTRIM(RTRIM(STRING_ESCAPE(sf.[Name], 'json'))), '":"', LTRIM(RTRIM(STRING_ESCAPE(sf.[Value], 'json'))), '"')
					FROM (
						SELECT DISTINCT sft.[Name], sf.[Value]
						FROM SkuFeature sf (nolock)
						INNER JOIN FeatureType (nolock) AS sft ON sft.FeatureTypeId = sf.FeatureTypeId
						WHERE sf.ProductSkuId = ps.ProductSkuId
					) as sf
					ORDER BY sf.[Name], sf.[Value]
					FOR XML PATH('')
				)
			, 1, 1, ''), 
		'}')
	),	
	[Active] = CONVERT(BIT, 
		CASE ps.SkuStatusId 
			WHEN 1 THEN 1 -- Ativo
			ELSE 0 -- Inativo
		END
	),
	[Metadata.Category/Subcategory] = tmp.[Category/Subcategory],
	[RowNumber] = tmp.[RowNumber]
INTO TMP_JSON
FROM #TMP_Skus tmp (nolock)
INNER JOIN ProductSku (nolock) AS ps ON ps.ProductSkuId = tmp.ProductSkuId
INNER JOIN Product (nolock) AS p ON p.ProductId = ps.ProductId	

CREATE NONCLUSTERED INDEX [IX_TMP_JSON_1]
	ON TMP_JSON ([RowNumber])

-- Versão Inserir via Mongo
DECLARE @UTC_DATETIME_NOW_STRING VARCHAR(50) = CONCAT(CONVERT(VARCHAR(30), GETUTCDATE(), 127), 'Z')	
SELECT
	[Status] = 'Created',
	[Offer.Product] = [Product],
	[Offer.Sku] = [Sku],
	[Offer.SellerId] = [SellerId],	
	[Offer.SkuTitle] = [SkuTitle],
	[Offer.Description] = [Description],
	[Offer.Ean] = [Ean],	
	[Offer.Price] = [Price],
	[Offer.ListPrice] = [ListPrice],
	[Offer.Url] = [Url],
	[Offer.Images] = JSON_QUERY(CASE WHEN [Images] NOT IN ('[]', '[""]') THEN [Images] ELSE NULL END),
	[Offer.ProductAttributes] = JSON_QUERY(CASE WHEN [ProductAttributes] <> '{}' THEN [ProductAttributes] ELSE NULL END),
	[Offer.SkuAttributes] = JSON_QUERY(CASE WHEN [SkuAttributes] <> '{}' THEN [SkuAttributes] ELSE NULL END),
	[Offer.Active] = [Active],
	[EnrichedOffer] = null,
	[Metadata.Category/Subcategory],
	[CreatedAt] = @UTC_DATETIME_NOW_STRING,
	[LastModifiedAt] = @UTC_DATETIME_NOW_STRING
FROM TMP_JSON
ORDER BY [RowNumber]
OFFSET 300 ROWS
FETCH NEXT 300 ROWS ONLY
FOR JSON PATH, INCLUDE_NULL_VALUES --, WITHOUT_ARRAY_WRAPPER


/*
-- Versão Inserir via Api
SELECT
	[product] = [Product],
	[sku] = [Sku],
	[seller_id] = [SellerId],
	[sku_title] = [SkuTitle],
	[description] = [Description],
	[ean] = [Ean],	
	[price] = [Price],
	[list_price] = [ListPrice],
	[url] = [Url],
	[images] = JSON_QUERY(CASE WHEN [Images] NOT IN ('[]', '[""]') THEN [Images] ELSE NULL END),
  [product_attributes] = JSON_QUERY(CASE WHEN [ProductAttributes] <> '{}' THEN [ProductAttributes] ELSE NULL END),
	[sku_attributes] = JSON_QUERY(CASE WHEN [SkuAttributes] <> '{}' THEN [SkuAttributes] ELSE NULL END),
	[active] = [Active]
FROM TMP_JSON
ORDER BY [RowNumber]
FOR JSON PATH--, WITHOUT_ARRAY_WRAPPER
*/


/*
-- Categorias/Subcategorias
SELECT 
	[CatId] = cat.Id ,
	[CatName] = cat.Name,
	[SubId] = sub.Id,
	[SubName] = sub.Name,
	[Breadcrumb] = CONCAT(cat.Name, ' -> ', sub.Name)
FROM Category cat
INNER JOIN SubCategory sub ON sub.CategoryId = cat.Id
WHERE
	sub.Id IN (34, 36, 38, 46, 48)
ORDER BY
	cat.Name, sub.Name
*/

/*
--Status Macnaima
-44 = "Erro de publicação"
-6 = "Entidade errada"
-5 = "Informação divergente"
-4 = "Entidade inexistente"
-3 = "Ficha inexistente"
-2 = "Informação incompleta"
-1 = "Campo de nome vazio"
0 = "Ofertas não processadas"
9 = "Entidade não encontrada"
11 = "Fila de extração"
19 = "Ficha não encontrada"
21 = "Categoria não encontrada"
22 = "Múltiplas categorias pais"
23 = "Sem enriquecimento de metadados"
24 = "Barrada pelo blocklist"
40 = "Fila de publicação"
42 = "Fila de publicação de matching manual"
43 = "Enviadas para publicação"
44 = "Pronto para publicação"
53 = "Matching não publicado"
54 = "Matching publicado"
*/

/*
DROP TABLE IF EXISTS TMP_Skus_Exportar_Macnaima
GO

SELECT skus.*
INTO TMP_Skus_Exportar_Macnaima
FROM (
	SELECT
		ps.ProductSkuId,
		ps.SupplierId,
		ps.OriginalProductSkuId,
		[CategoryId] = c.Id ,
		[CategoryName] = c.[Name],
		[SubcategoryId] = s.Id,
		[SubcategoryName] = s.[Name],
		ps.CreateDate,
		ROW_NUMBER() OVER (PARTITION BY ps.SupplierId, ps.OriginalProductSkuId ORDER BY ps.CreateDate ASC) as [RowNumber]	
	FROM ProductSku (nolock) AS ps
	INNER JOIN Product (nolock) AS p ON p.ProductId = ps.ProductId
	INNER JOIN ProductSubcategory (nolock) AS psu ON p.ProductId = psu.ProductId
	INNER JOIN SubCategory (nolock) AS s ON psu.SubcategoryId = s.Id
	INNER JOIN Category (nolock) AS c ON s.CategoryId = c.Id
	WHERE ps.SkuStatusId = 1
) as skus
WHERE
	skus.[RowNumber] = 1

SELECT TOP 1000 *
FROM TMP_Skus_Exportar_Macnaima
ORDER BY	
	CategoryName, SubcategoryName, SupplierId, CreateDate, OriginalProductSkuId

SELECT TOP 1000 *
FROM TMP_Skus_Exportar_Macnaima
ORDER BY
	SupplierId, CategoryName, SubcategoryName, CreateDate, OriginalProductSkuId

--CREATE NONCLUSTERED INDEX [IX_ProductFeature_03]
--	ON ProductFeature (ProductId, FeatureTypeId)
--	INCLUDE ([Name], [Value])
--GO

--DROP INDEX [IX_SkuFeature_01] ON SkuFeature
--GO

--CREATE NONCLUSTERED INDEX [IX_SkuFeature_01]
--	ON SkuFeature (ProductSkuId, FeatureTypeId)
--	INCLUDE ([Value])
--GO

--CREATE NONCLUSTERED INDEX [IX_SkuImage_01]
--	ON SkuImage (ProductSkuId, [Order], LargeImage)	
--GO




ALTER PROCEDURE [dbo].[SP_TMP_Exportar_Skus_Macnaima] (
	@OFFSET INT = 0,
	@NEXT INT = 50
)
AS
BEGIN
	
	IF (OBJECT_ID('tempdb..#TMP_Skus') IS NOT NULL) BEGIN
		DROP TABLE #TMP_Skus
	END

	IF (OBJECT_ID('tempdb..#TMP_JSON') IS NOT NULL) BEGIN
		DROP TABLE #TMP_JSON
	END

	SELECT 
		ProductSkuId, SupplierId, OriginalProductSkuId, CategoryId, CategoryName, SubcategoryId, SubcategoryName, CreateDate,
		[RowNumber] = ROW_NUMBER() OVER (ORDER BY (SELECT 1))
	INTO #TMP_Skus
	FROM TMP_Skus_Exportar_Macnaima (nolock)
	ORDER BY 
		SupplierId, CategoryName, SubcategoryName, CreateDate, OriginalProductSkuId
	OFFSET @OFFSET ROWS
	FETCH NEXT @NEXT ROWS ONLY

	CREATE NONCLUSTERED INDEX [IX_#TMP_Skus_1]
		ON #TMP_Skus ([ProductSkuId])

	SELECT 
		[Product] = p.OriginalProductId,
		[Sku] = ps.OriginalProductSkuId,
		[SellerId] = CONVERT(VARCHAR(11), ps.SupplierId),	
		[SkuTitle] = ps.[Name],	
		[Description] = p.[Description],
		[Ean] = ps.EAN,	
		[Price] = ps.PriceFrom,
		[ListPrice] = ps.PriceFor,
		[Url] = NULL,
		[Images] = (
			CONCAT('[',
				STUFF(
					(				
						SELECT 
							CONCAT(',"',  LTRIM(RTRIM(si.LargeImage)), '"')
						FROM (
							SELECT DISTINCT [Order], si.LargeImage
							FROM SkuImage si (nolock)
							WHERE 
								si.ProductSkuId = ps.ProductSkuId AND
								LTRIM(RTRIM(si.LargeImage)) <> ''
						) as si
						ORDER BY [Order]
						FOR XML PATH('')
					)
				, 1, 1, ''), 
			']')
		),	
		[ProductAttributes] = (
			CONCAT('{',
				STUFF(
					(				
						SELECT
							CONCAT(', "', LTRIM(RTRIM(STRING_ESCAPE(pf.[Name], 'json'))), '":"', LTRIM(RTRIM(STRING_ESCAPE(pf.[Value], 'json'))), '"')
						FROM (
							SELECT DISTINCT pf.[Name], pf.[Value]
							FROM ProductFeature pf (nolock)
							INNER JOIN FeatureType (nolock) AS pft ON pft.FeatureTypeId = pf.FeatureTypeId
							WHERE pf.ProductId = p.ProductId
						) as pf
						ORDER BY pf.[Name], pf.[Value]
						FOR XML PATH('')
					)
				, 1, 1, ''), 
			'}')
		),		
		[SkuAttributes] = (
			CONCAT('{',
				STUFF(
					(				
						SELECT						
							CONCAT(', "', LTRIM(RTRIM(STRING_ESCAPE(sf.[Name], 'json'))), '":"', LTRIM(RTRIM(STRING_ESCAPE(sf.[Value], 'json'))), '"')
						FROM (
							SELECT DISTINCT sft.[Name], sf.[Value]
							FROM SkuFeature sf (nolock)
							INNER JOIN FeatureType (nolock) AS sft ON sft.FeatureTypeId = sf.FeatureTypeId
							WHERE sf.ProductSkuId = ps.ProductSkuId
						) as sf
						ORDER BY sf.[Name], sf.[Value]
						FOR XML PATH('')
					)
				, 1, 1, ''), 
			'}')
		),	
		[Active] = CONVERT(BIT, 
			CASE ps.SkuStatusId 
				WHEN 1 THEN 1 -- Ativo
				ELSE 0 -- Inativo
			END
		),
		[Metadata.Category/Subcategory] = CONCAT(tmp.CategoryId, '-', tmp.CategoryName, ' -> ',tmp.SubcategoryId, '-', tmp.SubcategoryName),
		[RowNumber] = tmp.[RowNumber]
	INTO #TMP_JSON
	FROM #TMP_Skus tmp (nolock)
	INNER JOIN ProductSku (nolock) AS ps ON ps.ProductSkuId = tmp.ProductSkuId
	INNER JOIN Product (nolock) AS p ON p.ProductId = ps.ProductId	

	CREATE NONCLUSTERED INDEX [IX_#TMP_JSON_1]
		ON #TMP_JSON ([RowNumber])

	-- Versão Inserir via Mongo
	DECLARE @UTC_DATETIME_NOW_STRING VARCHAR(50) = CONCAT(CONVERT(VARCHAR(30), GETUTCDATE(), 127), 'Z')
	DECLARE @JSON NVARCHAR(MAX) =
		(
			SELECT
				[Status] = 'Created',
				[Offer.Product] = [Product],
				[Offer.Sku] = [Sku],
				[Offer.SellerId] = [SellerId],	
				[Offer.SkuTitle] = [SkuTitle],
				[Offer.Description] = [Description],
				[Offer.Ean] = [Ean],	
				[Offer.Price] = [Price],
				[Offer.ListPrice] = [ListPrice],
				[Offer.Url] = [Url],
				[Offer.Images] = JSON_QUERY(CASE WHEN [Images] NOT IN ('[]', '[""]') THEN [Images] ELSE NULL END),
				[Offer.ProductAttributes] = JSON_QUERY(CASE WHEN [ProductAttributes] <> '{}' THEN [ProductAttributes] ELSE NULL END),
				[Offer.SkuAttributes] = JSON_QUERY(CASE WHEN [SkuAttributes] <> '{}' THEN [SkuAttributes] ELSE NULL END),
				[Offer.Active] = [Active],
				[EnrichedOffer] = null,
				[Metadata.Category/Subcategory],
				[CreatedAt] = @UTC_DATETIME_NOW_STRING,
				[LastModifiedAt] = @UTC_DATETIME_NOW_STRING
			FROM #TMP_JSON
			ORDER BY [RowNumber]
			FOR JSON PATH, INCLUDE_NULL_VALUES --, WITHOUT_ARRAY_WRAPPER
		)
		
	SELECT [Json] = @JSON
END
GO
*/