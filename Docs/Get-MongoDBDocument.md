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

```powershell
Get-MongoDBDocument [[-DocumentId] <String>] [[-Filter] <Hashtable>] [<CommonParameters>]
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

### -DocumentId

This is the _id of the document

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

[FilterDefinitionBuilder](https://mongodb.github.io/mongo-csharp-driver/2.18/apidocs/html/T_MongoDB_Driver_FilterDefinitionBuilder_1.htm)
