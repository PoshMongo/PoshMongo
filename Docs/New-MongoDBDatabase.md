---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# New-MongoDBDatabase

## SYNOPSIS

Create a MongoDB Database

## SYNTAX

```powershell
New-MongoDBDatabase [-DatabaseName] <String> [[-Client] <MongoClient>] [<CommonParameters>]
```

## DESCRIPTION

Create a MongoDB Database, the database is only written to Mongo when
a collection is stored in it.

## EXAMPLES

### Example 1

```powershell
PS C:\> New-MongoDBDatabase -DatabaseName MyDB3

Client            : MongoDB.Driver.MongoClient
DatabaseNamespace : MyDB3
Settings          : GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Create a new database

## PARAMETERS

### -Client

The MongoClient to use for a connection

```yaml
Type: MongoDB.Driver.MongoClient
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName

The name of the database to create

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

### MongoDB.Driver.MongoDatabaseBase

## NOTES

## RELATED LINKS
