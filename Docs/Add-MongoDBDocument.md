---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Add-MongoDBDocument

## SYNOPSIS

This cmdlet adds a document (JSON) to a MongoDB Collection.

## SYNTAX

### CollectionName

```powershell
Add-MongoDBDocument [-Document] <String> [-CollectionName] <String> [-DatabaseName] <String> [-Force]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Collection

```powershell
Add-MongoDBDocument [-Document] <String>
 [-MongoCollection] <MongoDB.Driver.IMongoCollection`1[MongoDB.Bson.BsonDocument]> [-Force]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet adds a document to the selected MongoDB Collection. You must first
select the collection (Get-MongoDBCollection) before a document can be added.

## EXAMPLES

### Example 1

```powershell
PS C:\> Add-MongoDBDocument -Document '{"_id":"19511","FirstName":"Joe","LastName":"Friday"}' -CollectionName 'myCollection1' -DatabaseName 'MyDB'
{ "_id" : "19511", "FirstName" : "Joe", "LastName" : "Friday" }
```

An example showing how to add a simple json document to the collection

### Example 2

```powershell
PS C:\> $Collection1 | Add-MongoDBDocument -Document '{"_id":"19792","Luke":"Joe","LastName":"Duke"}'
{ "_id" : "19792", "Luke" : "Joe", "LastName" : "Duke" }
```

You can use the pipeline to also add documents to a collection

## PARAMETERS

### -CollectionName

The name of the collection to add the Document to

```yaml
Type: System.String
Parameter Sets: CollectionName
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName

The name of the MongoDBDatabase to add the Document to

```yaml
Type: System.String
Parameter Sets: CollectionName
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Document

This is a JSON string object. It can be a complex object that is converted to
JSON via ConvertTo-Json.

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

### -Force

{{ Fill Force Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MongoCollection

A Collection object to add the Document to

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

### System.Text.Json

## NOTES

## RELATED LINKS

[Get-MongoDBCollection](Get-MongoDBCollection.md)
