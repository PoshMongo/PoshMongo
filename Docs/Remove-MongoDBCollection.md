---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Remove-MongoDBCollection

## SYNOPSIS

Remove a MongoDB Collection

## SYNTAX

### CollectionName

```powershell
Remove-MongoDBCollection [-CollectionName] <String> [<CommonParameters>]
```

### DatabaseName

```powershell
Remove-MongoDBCollection [-CollectionName] <String> [-DatabaseName] <String> [<CommonParameters>]
```

### Collection

```powershell
Remove-MongoDBCollection [-Collection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]>
 [<CommonParameters>]
```

## DESCRIPTION

Remove a MongoDB Collection from the selected database

## EXAMPLES

### Example 1

```powershell
PS C:\> Remove-MongoDBCollection -CollectionName MyCollection
```

Remove a collection from the database

### Example 2

```powershell
PS C:\> Remove-MongoDBCollection -CollectionName MyCollection -DatabaseName MyDB
```

Remove a collection from the database specified

### Example 4

```powershell
PS C:\> $Collection| Remove-MongoDBCollection
```

Remove a collection on the pipeline

## PARAMETERS

### -Collection

A collection object to remove

```yaml
Type: MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]
Parameter Sets: Collection
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -CollectionName

The name of the collection to remove

```yaml
Type: System.String
Parameter Sets: CollectionName, DatabaseName
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName

The name of the database to remove the collection from

```yaml
Type: System.String
Parameter Sets: DatabaseName
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### null

## NOTES

## RELATED LINKS
