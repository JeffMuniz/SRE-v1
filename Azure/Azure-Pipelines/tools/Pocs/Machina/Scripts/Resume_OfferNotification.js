mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'OriginalCategory',
        toString("EnrichedOffer.CategoryId") as 'Category',
        "EnrichedOffer.SubcategoryIds" as 'Subcategory',
        Status,
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       GROUP BY OriginalCategory, Category, Subcategory, Status
       ORDER BY OriginalCategory, Status
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        Status,
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       WHERE
        "EnrichedOffer.ProductHash" IS NULL
       GROUP BY Category, Status
       ORDER BY Category, Status
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        Status,
        toString("EnrichedOffer.ProductHash") as 'ProductHash',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       WHERE
        "EnrichedOffer.ProductHash" IS NOT NULL
       GROUP BY Category, Status, ProductHash
       ORDER BY Category, Status
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        toString("EnrichedOffer.ProductHash") as 'ProductHash',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       GROUP BY Category, ProductHash
       HAVING Qtd > 1
       ORDER BY Category, ProductHash
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        toString("EnrichedOffer.SkuHash") as 'SkuHash',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       GROUP BY Category, SkuHash
       HAVING Qtd > 1
       ORDER BY Category, SkuHash
       
`)
.projection({"_id": 0})

//"EnrichedOffer" IS NULL AND
mb.runSQLQuery(`

       SELECT 
        Status,
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       WHERE 
        "Metadata.Category/Subcategory" = '5-Casa e cozinha -> 38-Eletrodomésticos'
       GROUP BY Status
       ORDER BY Status
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toInt("Offer.SellerId") as 'SellerId', 
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       GROUP BY SellerId
       ORDER BY Qtd DESC, SellerId
       
`)
.projection({"_id": 0})



//-----------------------------------------------------------------
mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'OriginalCategory',
        toString("EnrichedOffer.CategoryId") as 'Category',
        "EnrichedOffer.SubcategoryIds" as 'Subcategory',
        Status,
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       GROUP BY OriginalCategory, Category, Subcategory, Status
       ORDER BY OriginalCategory, Status
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'OriginalCategory',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       GROUP BY OriginalCategory
       ORDER BY OriginalCategory
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        toString("EnrichedOffer.ProductHash") as 'ProductHash',
        toInt(1) as 'Row',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       WHERE ProductHash IS NOT NULL
       GROUP BY Category, ProductHash, Row
       HAVING Qtd > 1
       ORDER BY Category, ProductHash
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        toString("EnrichedOffer.SkuHash") as 'SkuHash',
        toInt(1) as 'Row',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       WHERE SkuHash IS NOT NULL
       GROUP BY Category, SkuHash, Row
       HAVING Qtd > 1
       ORDER BY Category, SkuHash
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
        toString("Metadata.Category/Subcategory") as 'Category',
        COUNT(1) as 'Qtd'
       FROM OfferNotification
       WHERE toString("EnrichedOffer.SkuHash") IS NOT NULL
       GROUP BY Category
       HAVING Qtd > 1
       ORDER BY Category
       
`)
.projection({"_id": 0})

mb.runSQLQuery(`

       SELECT 
       *
       FROM OfferNotification
       WHERE "EnrichedOffer.ProductHash" = '4bdef8c232d7e6ce8d271dfd519c8e0d'
       
`)

