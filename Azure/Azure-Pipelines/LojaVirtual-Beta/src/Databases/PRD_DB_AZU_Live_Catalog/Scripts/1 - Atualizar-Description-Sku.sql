USE PRD_DB_AZU_Live_Catalog
GO

IF OBJECT_ID('TMPSkuDescription') IS NOT NULL
	DROP TABLE TMPSkuDescription
GO
CREATE TABLE TMPSkuDescription([ProductSkuId] VARCHAR(26), [Description] VARCHAR(MAX))
GO
CREATE CLUSTERED INDEX [PK_TMPSkuDescription] ON TMPSkuDescription ([ProductSkuId])
GO

IF OBJECT_ID('tempdb..#SkuDescriptionPaginado') IS NOT NULL
	DROP TABLE #SkuDescriptionPaginado
GO
CREATE TABLE #SkuDescriptionPaginado([ProductSkuId] VARCHAR(26), [Description] VARCHAR(MAX))
GO
CREATE CLUSTERED INDEX [PK_#SkuDescriptionPaginado] ON #SkuDescriptionPaginado ([ProductSkuId])
GO

DECLARE 
	@MessageConcat VARCHAR(255)

SET @MessageConcat = CONCAT(CHAR(13), CHAR(10), CONVERT(VARCHAR(50), GETDATE(), 127), 
  ' - Obtendo os Skus com o campo Description nulo')
RAISERROR(@MessageConcat, 0, 1) WITH NOWAIT;

INSERT INTO TMPSkuDescription([ProductSkuId], [Description])
  SELECT sku.[ProductSkuId], p.[Description]
  FROM [dbo].[ProductSku] sku (nolock)
  INNER JOIN [dbo].[Product] p (nolock) ON p.ProductId = sku.ProductId
  WHERE sku.[Description] IS NULL

SET NOCOUNT ON;

SET @MessageConcat = CONCAT(CHAR(13), CHAR(10), CONVERT(VARCHAR(50), GETDATE(), 127), 
  ' - INICIO! -> Atualização do campo Description dos Skus com o do Produto')
RAISERROR(@MessageConcat, 0, 1) WITH NOWAIT;
RAISERROR('-----------------------------------------------------------------------------------------------------------------------------', 0, 1) WITH NOWAIT;	

DECLARE 
	@Paginacao INT = 1000,
	@IndicePaginacao INT = 0,
	@MaxPaginacao INT = (SELECT COUNT(1) FROM TMPSkuDescription (nolock))

SET NOCOUNT OFF;

WHILE (@IndicePaginacao < @MaxPaginacao)
BEGIN
	SET NOCOUNT ON;	

	SET @MessageConcat = CONCAT(CONVERT(VARCHAR(50), GETDATE(), 127), 
		' - [', @IndicePaginacao + 1, ' até ', @IndicePaginacao + @Paginacao,' de ', @MaxPaginacao, '] '		
	)
	RAISERROR(@MessageConcat, 0, 1) WITH NOWAIT

	DELETE FROM #SkuDescriptionPaginado

	INSERT INTO #SkuDescriptionPaginado([ProductSkuId], [Description])
		SELECT ProductSkuId, [Description]
		FROM TMPSkuDescription (nolock)
		ORDER BY 1 OFFSET @IndicePaginacao
		ROWS FETCH NEXT @Paginacao ROWS ONLY	

	-----------------------------------------------------------------------------------------------------------------------------

	UPDATE sku SET
		sku.[Description] = pag.[Description]
	FROM [dbo].[ProductSku] sku
	INNER JOIN #SkuDescriptionPaginado pag
		ON	pag.[ProductSkuId] = sku.[ProductSkuId]

	-----------------------------------------------------------------------------------------------------------------------------

  SET NOCOUNT OFF;

	SET @IndicePaginacao = @IndicePaginacao + @Paginacao
END

RAISERROR('-----------------------------------------------------------------------------------------------------------------------------', 0, 1) WITH NOWAIT;	
SET @MessageConcat = CONCAT(CONVERT(VARCHAR(50), GETDATE(), 127), ' - FIM! -> Atualização do campo Description dos Skus com o do Produto')
RAISERROR(@MessageConcat, 0, 1) WITH NOWAIT;
