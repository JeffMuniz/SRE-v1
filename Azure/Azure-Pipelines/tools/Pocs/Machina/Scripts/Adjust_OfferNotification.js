db.getCollection("OfferNotification")
    .find({})
    .projection({})
    .sort({"LastModifiedAt": -1})

db.OfferNotification
    .find({
        "Metadata.Category/Subcategory": '6-Eletrônicos -> 48-Telefones, Celulares e Tablets',
        //"Metadata.Category/Subcategory": '6-Eletrônicos -> 46-TV',
        //"Metadata.Category/Subcategory": '5-Casa e cozinha -> 38-Eletrodomésticos',
        "Status": {
            //$in: ['AwaitingEnrichment']
            //$in: ['Enriched']
            $in: ['AwaitingCompleteEnrichment', 'Enriched']
        }
    })
   .projection({})
   .sort({"LastModifiedAt":-1}) 

/*
60edb3491d9f7108a8c017fa
60e5e6c0bb5faf3c9c763ee0
60e5e6c0bb5faf3c9c763ee2
*/

db.getCollection("OfferNotification")
    .find({})
    .projection({ "_id": 1, "Offer.SellerId": 1, "Offer.Sku": 1, }})
    .sort({_id: -1})
    .limit(100)

db.getCollection("OfferNotification").find({ _id: ObjectID("60e5e6c0bb5faf3c9c763ee3") })
   .projection({})
   .sort({_id:-1})
   .limit(100)

db.OfferNotification.find({
        "EnrichedOffer.SubcategoryIds": {
            $in: ["46"]
        }
    })
    .sort({
        "LastModifiedAt": -1
    })
    .limit(100)

/*
NotificationStatus
{
    Created,
    PendingNotification,
    AwaitingGetDetail,
    AwaitingEnrichment,
    AwaitingCompleteEnrichment
    Enriched
}
*/

db.getCollection("OfferNotification").insertMany([])


db.getCollection("OfferNotification").updateMany(
    {
        "Status": "Enriched",
        "EnrichedOffer.SubcategoryIds": {
            $in: ["46"]
        }
    },
    { $set: { "Status": "WaitingEnrich", "LastModifiedAt": ISODate() } }
)

/*
db.getCollection("OfferNotification").updateMany(
    { "Status": "Created" },
    { $set: { "Status": "PendingNotification" } }
)
*/

{
    var cursor = db.getCollection("OfferNotification")
        .find({ "CreatedAt": { $type: "string" } })
        .projection({})
        .sort({_id:-1});
    var batch = [];
    cursor
        .forEach(doc => {
            batch.push( { 
                'updateOne': {
                    'filter': { '_id': doc._id },
                    'update': { 
                        $set: 
                        {
                            "CreatedAt": ISODate(doc.CreatedAt),
                            "LastModifiedAt": ISODate(doc.LastModifiedAt)
                        }
                    }
                }
            });
            
            if (batch.length === 50) {
                //Execute per 50 operations and re-init
                db.getCollection("OfferNotification").bulkWrite(batch);
                batch = [];
            }
        });
    if(batch.length > 0) {
         db.getCollection("OfferNotification").bulkWrite(batch);
    }
}