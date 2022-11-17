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
