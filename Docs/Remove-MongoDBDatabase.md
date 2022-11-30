---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version:
schema: 2.0.0
---

# Remove-MongoDBDatabase

## SYNOPSIS

Remove a MongoDB Database

## SYNTAX

### Default

```powershell
Remove-MongoDBDatabase [-DatabaseName] <String> [[-Client] <MongoClient>] [<CommonParameters>]
```

### Database

```powershell
Remove-MongoDBDatabase [[-Client] <MongoClient>] [-Database] <MongoDatabaseBase> [<CommonParameters>]
```

## DESCRIPTION

Remove a MongoDB Database

## EXAMPLES

### Example 1

```powershell
PS C:\> Remove-MongoDBDatabase -DatabaseName MyDB3
```

Remove a database from Mongo

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

### -Database

The MongoDatabase to remove

```yaml
Type: MongoDB.Driver.MongoDatabaseBase
Parameter Sets: Database
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DatabaseName

The name of the database to remove

```yaml
Type: System.String
Parameter Sets: Default
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
