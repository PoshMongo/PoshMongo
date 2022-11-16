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

```powershell
Remove-MongoDBCollection -CollectionName <String> [<CommonParameters>]
```

## DESCRIPTION

Remove a MongoDB Collection from the selected database

## EXAMPLES

### Example 1

```powershell
PS C:\> Remove-MongoDBCollection -CollectionName MyCollection
```

Remove a collection from the database

## PARAMETERS

### -CollectionName

The name of the collection to remove

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
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
