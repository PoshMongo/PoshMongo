| Latest Version | PowerShell Gallery | Build Status | Project Board | Issues | License |
|-----------------|-----------------|----------------|----------------|----------------|----------------|
| [![Latest Version](https://img.shields.io/github/v/tag/SchemaModule/PowerShell)](https://github.com/SchemaModule/PowerShell/tags) | [![Powershell Gallery](https://img.shields.io/powershellgallery/dt/schema)](https://www.powershellgallery.com/packages/schema) | [![Build Status](https://dev.azure.com/patton-tech/SchemaModule/_apis/build/status/SchemaModule.PowerShell?repoName=SchemaModule%2FPowerShell&branchName=master)](https://dev.azure.com/patton-tech/SchemaModule/_build/latest?definitionId=9&repoName=SchemaModule%2FPowerShell&branchName=master) | [![Board Status](https://dev.azure.com/patton-tech/cbaf64d2-5310-475c-8874-fb2b809e3f3b/1138b36a-5cb9-4d7d-8163-32041295538f/_apis/work/boardbadge/d9f5bd6d-43e7-45ac-a1aa-f1afc7f0cb17?columnOptions=1)](https://dev.azure.com/patton-tech/cbaf64d2-5310-475c-8874-fb2b809e3f3b/_boards/board/t/1138b36a-5cb9-4d7d-8163-32041295538f/Microsoft.RequirementCategory/) | [![GitHub issues](https://img.shields.io/github/issues/SchemaModule/PowerShell)](https://github.com/SchemaModule/PowerShell/issues) | [![GitHub license](https://img.shields.io/github/license/SchemaModule/PowerShell)](https://github.com/SchemaModule/PowerShell/blob/master/LICENSE) |
# PoshMongo Module

## Description

A collection of PowerShell Cmdlets for working with MongoDB

## PoshMongo Cmdlets

### [Add-MongoDBDocument](docs/Add-MongoDBDocument.md)

Add a document to a MongoDB Collection

### [Connect-MongoDBInstance](docs/Connect-MongoDBInstance.md)

Connect to a MongoDB Instance

### [Get-MongoDBCollection](docs/Get-MongoDBCollection.md)

Get a MongoDB Collection

### [Get-MongoDBDatabase](docs/Get-MongoDBDatabase.md)

Get a MongoDB Database

### [Get-MongoDBDocument](docs/Get-MongoDBDocument.md)

Get a MongoDB Document

### [New-MongoDBCollection](docs/New-MongoDBCollection.md)

Create a MongoDB Collection

### [New-MongoDBDatabase](docs/New-MongoDBDatabase.md)

Create a MongoDB Database

### [Remove-MongoDBCollection](docs/Remove-MongoDBCollection.md)

Remove a MongoDB Collection

### [Remove-MongoDBDatabase](docs/Remove-MongoDBDatabase.md)

Remove a MongoDB Database

### [Remove-MongoDBDocument](docs/Remove-MongoDBDocument.md)

Remove a MongoDB Document

# PoshMongo
A PowerShell module for working with MongoDB, it provides cmdlets that cover the basic [CRUD](https://www.mongodb.com/developer/languages/csharp/csharp-crud-tutorial/) operations for MongoDb.

## Setup
In order to build this project on your own you will need to download the [.Net SDK](https://dotnet.microsoft.com/en-us/download), this will install the [dotnet](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet) command. This project was built with .Net 6.0 LTS, but that shouldn't impact which SDK you install.

1. Clone this repo
2. Download SDK
3. Enter the directory that contains the SLN or CSPROJ files
4. Build project

```powershell
# build the project file
dotnet build .\PoshMongo.csproj
# or build the solution file
dotnet build .\PoshMongo.sln
```

## Usage
Once you have built the dll, you will need to load into powershell

```powershell
import-module .\bin\Debug\net6.0\PoshMongo.dll

Get-Command -Module PoshMongo

CommandType     Name                                               Version    Source
-----------     ----                                               -------    ------
Cmdlet          Add-Document                                       1.0.0.0    PoshMongo
Cmdlet          Connect-Instance                                   1.0.0.0    PoshMongo
Cmdlet          Get-Collection                                     1.0.0.0    PoshMongo
Cmdlet          Get-Database                                       1.0.0.0    PoshMongo
Cmdlet          Get-Document                                       1.0.0.0    PoshMongo
Cmdlet          New-Collection                                     1.0.0.0    PoshMongo
Cmdlet          New-Database                                       1.0.0.0    PoshMongo
Cmdlet          Remove-Collection                                  1.0.0.0    PoshMongo
Cmdlet          Remove-Database                                    1.0.0.0    PoshMongo
Cmdlet          Remove-Document                                    1.0.0.0    PoshMongo
```

## Notes
If you are using AzureCosmos with MongoDB API you will want to connect using the -ForceTls12 switch for the Connect-Instance
