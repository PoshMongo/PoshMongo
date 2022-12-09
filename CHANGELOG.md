# Changelog

All changes to this module should be reflected in this document.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.0] - 2022-12-08

This is a breaking change to the module all the cmdlets have been modified with most receiving new parameters. The Database and Collection global variables have been removed, so the cmdlets required some changes in their function.

### Sample Changes

The original Get-MongoDBCollection would run without any parameters, it would pull from the Database stored in the SessionState.

```powershell
Get-MongoDBCollection
```

The updated Get-MongoDBCollection requires at least the DatabaseName to be passed in.

```powershell
Get-MongoDBCollection -DatabaseName
```

Most Cmdlets have been updatd with DatabaseName and CollectionName, these replace the SessionState variables that had been used in the previous version.

---

The following changes have been applied:

- Functional code has been moved to a seperate namespace and file. The methods have been slightly modified to use the proper Interfaces for each Type
  - AddDocument method added to Operations
  - GetCollection method added to Operations
  - GetDatabase method added to Operations
  - GetDocument method added to Operations
  - NewCollection method added to Operations
  - NewDatabase method added to Operations
  - RemoveCollection method added to Operations
  - RemoveDatabase method added to Operations
  - RemoveDocument method added to Operations
- Cmdlet ParameterTypes have been updated to reflect the correct Interface Types
  - MongoDatabaseBase changed to IMongoDatabase
  - MongoClient changed to IMongoClient
  - MongoCollection changed to IMongoCollection
- HelpUri has been updated across Cmdlets which fixes the problem with Get-Help -Online
- OutputType has been updated with correct Interface Types
- DefaultParameterSet has been removed
- Corrected ordering of each ParameterSet
- Added Private properties to clean up instantiating the objects
- Updated Tests to accomodate all the changes to the Cmdlets
