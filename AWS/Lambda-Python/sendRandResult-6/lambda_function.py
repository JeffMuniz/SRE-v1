import json
import boto3
import uuid
from uuid import uuid4
from boto3.dynamodb.conditions import Key


def lambda_handler(event, context):
    dynamo = boto3.client("dynamodb")
    URL = "https://3w0zg1oeh0.execute-api.us-east-2.amazonaws.com/production"
    client = boto3.client("apigatewaymanagementapi", endpoint_url = URL)
    dynamodb = boto3.resource('dynamodb')
    
    
    table1 = dynamodb.Table('random')
    response = table1.scan()
    data = response['Items']
    
    while 'LastEvaluatedKey' in response:
        response = table.scan(ExclusiveStartKey=response['LastEvaluatedKey'])
        data.extend(response['Items'])
 
    dynamodb = boto3.resource('dynamodb')
    table = dynamodb.Table('pk-table')

    # Create 100 records with a random partition key
    #for i in range(10):
        #item = {'pk': str(uuid4()), 'text': f"question-{i}"}
    item = {'pk': str(uuid4()), 'word': "Car"}
    table.put_item(Item=item)
    print(f"Inserted {item}")
    
    item = {'pk': str(uuid4()), 'word': "Bag"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Dog"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Bed"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Lane"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Cat"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Linen"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Gabage"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Orange"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Cactus"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Lake"}
    table.put_item(Item=item)
    print(f"Inserted {item}")

    item = {'pk': str(uuid4()), 'word': "Data"}
    table.put_item(Item=item)
    print(f"Inserted {item}")


    # Read  and print records
    for i in range(12):
        response = table.scan(
            Limit=1,
            ExclusiveStartKey={
                'pk': str(uuid4())
            },
            ReturnConsumedCapacity='TOTAL'
        )
        if response['Items']:
            print({
                "Item": response['Items'][0],
                "Capacity": response['ConsumedCapacity']['CapacityUnits'],
                "ScannedCount": response['ScannedCount']
            })
    data = response['Items']
    
    response  = dynamo.scan(TableName="randwebsock")
    for connection in response["Items"]:
        response = client.post_to_connection(ConnectionId=connection["connectionId"]["S"], Data=json.dumps(data))
    
    
    return {
        'statusCode': 200,
        'body': json.dumps('____________________________________________,')
    }