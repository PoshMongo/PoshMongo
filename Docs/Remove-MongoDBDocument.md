---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Remove-MongoDBDocument

## SYNOPSIS

Remove a MongoDB Document

## SYNTAX

### DocumentId

```powershell
Remove-MongoDBDocument [-DocumentId] <String> [<CommonParameters>]
```

### CollectionNameId

```powershell
Remove-MongoDBDocument [-DocumentId] <String> [-CollectionName] <String> [<CommonParameters>]
```

### CollectionId

```powershell
Remove-MongoDBDocument [-DocumentId] <String>
 [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]> [<CommonParameters>]
```

### Filter

```powershell
Remove-MongoDBDocument [-Filter] <Hashtable> [<CommonParameters>]
```

### CollectionNameFilter

```powershell
Remove-MongoDBDocument [-Filter] <Hashtable> [-CollectionName] <String> [<CommonParameters>]
```

### CollectionFilter

```powershell
Remove-MongoDBDocument [-Filter] <Hashtable>
 [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]> [<CommonParameters>]
```

### DocumentCollectionName

```powershell
Remove-MongoDBDocument [-CollectionName] <String> [-Document] <String> [<CommonParameters>]
```

### DocumentCollection

```powershell
Remove-MongoDBDocument [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]>
 [-Document] <String> [<CommonParameters>]
```

## DESCRIPTION

Remove a MongoDB Document from the selected collection

## EXAMPLES

### Example 1

```powershell
PS C:\> Remove-MongoDBDocument -DocumentId '4e27b0f5-aaa0-4d4d-bdc8-43f811242d93'
```

Remove a document from the collection

### Example 2

```powershell
PS C:\> Remove-MongoDBDocument -Filter @{'Name'='FirstName'}
```

Remove a document from the collection using a filter

## PARAMETERS

### -CollectionName

The name of the Collection to remove a Document from

```yaml
Type: System.String
Parameter Sets: CollectionNameId, CollectionNameFilter, DocumentCollectionName
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Document

The Document to remove

```yaml
Type: System.String
Parameter Sets: DocumentCollectionName, DocumentCollection
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DocumentId

The Id of the Document to remove

```yaml
Type: System.String
Parameter Sets: DocumentId, CollectionNameId, CollectionId
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter

This is a hashtable of values that are converted into a simple equality type
filter. For example:

```powershell
@{'Name'='FirstName'}
```

Is translated into a FilterDefinition object

```csharp
Filter.Eq(key, value));
```

```yaml
Type: System.Collections.Hashtable
Parameter Sets: Filter, CollectionNameFilter, CollectionFilter
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MongoCollection

The Collection object to remove the Document from

```yaml
Type: MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]
Parameter Sets: CollectionId, CollectionFilter, DocumentCollection
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

### System.Text.Json

## NOTES

## RELATED LINKS
