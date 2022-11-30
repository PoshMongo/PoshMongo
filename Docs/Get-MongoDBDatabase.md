---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Get-MongoDBDatabase

## SYNOPSIS

Get a MongoDB Database

## SYNTAX

```powershell
Get-MongoDBDatabase [[-DatabaseName] <String>] [[-Client] <MongoClient>] [<CommonParameters>]
```

## DESCRIPTION

Get a MongoDB Database

## EXAMPLES

### Example 1

```powershell
PS C:\> Get-MongoDBDatabase -DatabaseName MyDB

Client            : MongoDB.Driver.MongoClient
DatabaseNamespace : MyDB
Settings          : GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Get a specific database from MongoDB

### Example 2

```powershell
PS C:\> Get-MongoDBDatabase

Client            : MongoDB.Driver.MongoClient
DatabaseNamespace : MyDB
Settings          : GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null

Client            : MongoDB.Driver.MongoClient
DatabaseNamespace : MyDB2
Settings          : GuidRepresentation=CSharpLegacy;ReadConcern={ };ReadEncoding=null;ReadPreference={ Mode : Primary };WriteConcern={ };WriteEncoding=null
```

Get a list of databases from MongoDB

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

The name of the Database to retrieve

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

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### MongoDB.Driver.MongoDatabaseBase

## NOTES

## RELATED LINKS
