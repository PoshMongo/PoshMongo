---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# New-MongoDBCollection

## SYNOPSIS

Create a MongoDB Collection

## SYNTAX

### DatabaseName

```powershell
New-MongoDBCollection [-CollectionName] <String> [-DatabaseName] <String> [<CommonParameters>]
```

### Database

```powershell
New-MongoDBCollection [-CollectionName] <String> [-MongoDatabase] <IMongoDatabase> [<CommonParameters>]
```

## DESCRIPTION

Create a MongoDB Collection in the selected Database, the collection is only
written back to the database when a document is added to it.

## EXAMPLES

### Example 1

```powershell
PS C:\> New-MongoDBCollection -CollectionName MyCollection3

CollectionNamespace : MyDB.MyCollection3
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Create a new collection

### Example 2

```powershell
PS C:\> New-MongoDBCollection -CollectionName MyCollection4 -DatabaseName MyDB

CollectionNamespace : MyDB.MyCollection4
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Create a new collection in a different database

### Example 3

```powershell
PS C:\> $Database| New-MongoDBCollection -CollectionName MyCollection5

CollectionNamespace : MyDB.MyCollection5
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Create a new collection in the database on the pipeline

## PARAMETERS

### -CollectionName

The name of the collection to create

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName

The name of the database to create the collection in

```yaml
Type: System.String
Parameter Sets: DatabaseName
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MongoDatabase

A database object to create the collection in

```yaml
Type: MongoDB.Driver.IMongoDatabase
Parameter Sets: Database
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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
