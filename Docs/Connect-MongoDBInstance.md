---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Connect-MongoDBInstance

## SYNOPSIS

Connect to a MongoDB Instance

## SYNTAX

```powershell
Connect-MongoDBInstance [-ConnectionString] <String> [-ForceTls12] [<CommonParameters>]
```

## DESCRIPTION

Connect to a MongoDB Instance

## EXAMPLES

### Example 1

```powershell
PS C:\> Connect-MongoDBInstance -ConnectionString $settings
```

A simple example showing a connectionstring stored in a variable

## PARAMETERS

### -ConnectionString

A proper MongoDB connection string

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

### -ForceTls12

This switch forces the TLS 1.2 Protocol, this is required for Azure CosmosDB
with a MongoDB API backend.

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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### MongoDB.Driver.IMongoClient

## NOTES

## RELATED LINKS

[MongoDB Connection String](https://www.mongodb.com/docs/manual/reference/connection-string/)
