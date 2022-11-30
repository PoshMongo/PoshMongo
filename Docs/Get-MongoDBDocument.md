---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Get-MongoDBDocument

## SYNOPSIS

Get a MongoDB Document

## SYNTAX

### DocumentId

```powershell
Get-MongoDBDocument [-DocumentId] <String> [-HideId] [<CommonParameters>]
```

### CollectionId

```powershell
Get-MongoDBDocument [-DocumentId] <String>
 [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]> [-HideId]
 [<CommonParameters>]
```

### CollectionNameId

```powershell
Get-MongoDBDocument [-DocumentId] <String> [-CollectionName] <String> [-HideId] [<CommonParameters>]
```

### Filter

```powershell
Get-MongoDBDocument [-Filter] <Hashtable> [-HideId] [<CommonParameters>]
```

### CollectionFilter

```powershell
Get-MongoDBDocument [-Filter] <Hashtable>
 [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]> [-HideId]
 [<CommonParameters>]
```

### CollectionNameFilter

```powershell
Get-MongoDBDocument [-Filter] <Hashtable> [-CollectionName] <String> [-HideId] [<CommonParameters>]
```

### CollectionNameList

```powershell
Get-MongoDBDocument [-CollectionName] <String> [-HideId] [-List] [<CommonParameters>]
```

### CollectionList

```powershell
Get-MongoDBDocument [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]> [-HideId]
 [-List] [<CommonParameters>]
```

### Default

```powershell
Get-MongoDBDocument [-HideId] [-List] [<CommonParameters>]
```

## DESCRIPTION

Get a MongoDB Document from the selected Database and Collection

## EXAMPLES

### Example 1

```powershell
PS C:\> Get-MongoDBDocument -DocumentId '4e27b0f5-aaa0-4d4d-bdc8-43f811242d93'

_id  : 4e27b0f5-aaa0-4d4d-bdc8-43f811242d93
Name : FirstName
```

Get a specific document by the id

### Example 2

```powershell
PS C:\> Get-MongoDBDocument -Filter @{'Name'='FirstName'}

_id  : 4e27b0f5-aaa0-4d4d-bdc8-43f811242d93
Name : FirstName
```

Get a document using a filter

### Example 3

```powershell
PS C:\> Get-MongoDBDocument

_id  : 4e27b0f5-aaa0-4d4d-bdc8-43f811242d93
Name : FirstName

_id  : 1777c473-15fa-44f6-9509-97c3d7847cc1
Name : SecondName

_id  : e6a4b5cc-9a1e-46df-b240-0136afdf1e24
Name : ThirdName
```

Get a list of available documents from the collection

## PARAMETERS

### -CollectionName

The name of the collection to get the Documnet from

```yaml
Type: System.String
Parameter Sets: CollectionNameId, CollectionNameFilter, CollectionNameList
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DocumentId

This is the _id of the document

```yaml
Type: System.String
Parameter Sets: DocumentId, CollectionId, CollectionNameId
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
Parameter Sets: Filter, CollectionFilter, CollectionNameFilter
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideId

A Switch to suppress the _id field

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List

A switch to list all Documents

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: CollectionNameList, CollectionList, Default
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MongoCollection

A Collection object to return Documents from

```yaml
Type: MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]
Parameter Sets: CollectionId, CollectionFilter, CollectionList
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

### System.Text.Json

## NOTES

## RELATED LINKS

[FilterDefinitionBuilder](https://mongodb.github.io/mongo-csharp-driver/2.18/apidocs/html/T_MongoDB_Driver_FilterDefinitionBuilder_1.htm)
