---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Get-MongoDBCollection

## SYNOPSIS

Get a MongoDB Collection

## SYNTAX

```powershell
Get-MongoDBCollection [[-CollectionName] <String>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet returns a specific MongoDB Collection from the selected MongoDB
Database, or all available collections.

## EXAMPLES

### Example 1

```powershell
PS C:\> Get-MongoDBCollection -CollectionName MyCollection

CollectionNamespace : MyDB.MyCollection
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

How to get a collection by name

### Example 2

```powershell
PS C:\> Get-MongoDBCollection

CollectionNamespace : MyDB.MyCollection
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null

CollectionNamespace : MyDB.MyCollection2
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Get a list of collections

## PARAMETERS

### -CollectionName

The name of the collection to retrieve from the Database

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### MongoDB.Driver.IMongoCollection

## NOTES

## RELATED LINKS
