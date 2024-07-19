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

### DatabaseName

```powershell
Get-MongoDBCollection [[-CollectionName] <String>] [-DatabaseName] <String>
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Database

```powershell
Get-MongoDBCollection [[-CollectionName] <String>] [-MongoDatabase] <IMongoDatabase>
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### CollectionNamespace

```powershell
Get-MongoDBCollection [-CollectionNamespace] <String> [-ProgressAction <ActionPreference>] [<CommonParameters>]
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

### Example 3

```powershell
PS C:\> Get-MongoDBCollection -CollectionNamespace 'MyDB.MyCollection'

CollectionNamespace : MyDB.MyCollection
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

How to get a collection by CollectionNamespace

### Example 4

```powershell
PS C:\> Get-MongoDBCollection -CollectionName MyCollection -DatabaseName MyDB

CollectionNamespace : MyDB.MyCollection
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

How to get a collection using CollectionName and DatabaseName

### Example 5

```powershell
PS C:\> $Database| Get-MongoDBCollection -CollectionName MyCollection

CollectionNamespace : MyDB.MyCollection
Database            : MongoDB.Driver.MongoDatabaseImpl
DocumentSerializer  : MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer
Indexes             : MongoDB.Driver.MongoCollectionImpl`1+MongoIndexManager[MongoDB.Bson.BsonDocument]
Settings            : AssignIdOnInsert=True;GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

How to get a collection using a databse on the pipeline

## PARAMETERS

### -CollectionName

The name of the collection to retrieve from the Database

```yaml
Type: System.String
Parameter Sets: DatabaseName, Database
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CollectionNamespace

This is a string value in the following format

databasename.collectionname

```yaml
Type: System.String
Parameter Sets: CollectionNamespace
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName

This is the database name of the database that holds the collection to return

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

This is a database object that holds the collection to return

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

### -ProgressAction

{{ Fill ProgressAction Description }}

```yaml
Type: System.Management.Automation.ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
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
