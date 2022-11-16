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

```powershell
New-MongoDBCollection [-CollectionName] <String> [<CommonParameters>]
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

## PARAMETERS

### -CollectionName

The name of the collection to create

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
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
