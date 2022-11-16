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

```powershell
Remove-MongoDBDocument [-DocumentId] <String> [[-Filter] <Hashtable>] [<CommonParameters>]
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

### -DocumentId

The document to remove

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

### System.Text.Json

## NOTES

## RELATED LINKS
