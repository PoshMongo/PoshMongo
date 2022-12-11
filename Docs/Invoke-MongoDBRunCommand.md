---
external help file: PoshMongo.dll-Help.xml
Module Name: PoshMongo
online version: https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Invoke-MongoDBRunCommand.md#invoke-mongodbruncommand
schema: 2.0.0
---

# Invoke-MongoDBRunCommand

## SYNOPSIS

This Cmdlet enables running commands directly against the MongoDB Instance.

## SYNTAX

### DatabaseName

```powershell
Invoke-MongoDBRunCommand [-DatabaseName] <String> [-CommandString] <String> [<CommonParameters>]
```

### Database

```powershell
Invoke-MongoDBRunCommand [-MongoDatabase] <IMongoDatabase> [-CommandString] <String> [<CommonParameters>]
```

## DESCRIPTION

This Cmdlet enables running commands directly against the MongoDB Instance. When you run a database command, you specify
the command as a document to db.runCommand(). The document's key is the command to run, and the value is typically
supplied as 1.

## EXAMPLES

### Example 1

```powershell
PS C:\> Invoke-MongoDBRunCommand -DatabaseName MyDB -CommandString '{ dbStats: 1 }'
{
  "db": "MyDB",
  "collections": 1,
  "objects": 3,
  "avgObjSize": 76458,
  "dataSize": 229376,
  "storageSize": 229376,
  "ok": 1
}
```

Example running the dbStats command against a single Database.

### Example 2

```powershell
PS C:\> Get-MongoDBDatabase |ForEach-Object {$_ |Invoke-MongoDBRunCommand -CommandString '{ dbStats: 1 }'}
{
  "db": "MyDB1",
  "collections": 0,
  "objects": 0,
  "avgObjSize": 0,
  "dataSize": 0,
  "storageSize": 0,
  "ok": 1
}
{
  "db": "MyDB2",
  "collections": 4,
  "objects": 138,
  "avgObjSize": 2864,
  "dataSize": 395264,
  "storageSize": 427008,
  "ok": 1
}
{
  "db": "MyDB",
  "collections": 1,
  "objects": 3,
  "avgObjSize": 76458,
  "dataSize": 229376,
  "storageSize": 229376,
  "ok": 1
}
```

Example running the dbStats command against a single Database.

## PARAMETERS

### -CommandString

A string as a document the document's key is the command to run, and the value is typically supplied as 1.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName

The name of the database to run the command against

```yaml
Type: System.String
Parameter Sets: DatabaseName
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MongoDatabase

The Database to run the command against

```yaml
Type: MongoDB.Driver.IMongoDatabase
Parameter Sets: Database
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

### MongoDB.Driver.IMongoDatabase

## OUTPUTS

### System.Text.Json

## NOTES

## RELATED LINKS

[MongoDB Commands](https://www.mongodb.com/docs/manual/reference/command/)
