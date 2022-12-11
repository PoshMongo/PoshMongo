# Changelog

All changes to this module should be reflected in this document.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [[3.1.1]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.1.1) - 2022-12-10

This release is a bugfix for the Cmdlets that return a List<T> of obejcts. In order for Cmdlets to process objects
on the cmdline they need to come in one at a time. If the output of one cmdlet is a List object, it must first be
expanded, either from GetEnumerator() or a Foreach-Object.

The following changes have been applied:

- GetDatabaseCmdlet
- GetCollectionCmdlet

--

## [[3.1.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.1.0) - 2022-12-11

This is release adds a new Cmdlet, Invoke-MongoDBRunCommand, which allows you to run [Commands](https://www.mongodb.com/docs/manual/reference/command/) against your MongoDB.

The following changes have been applied:

- InvokeRunCommand
  - Added New Cmdlet
  - Added Help
  - Added Tests
- Fixed null reference that I missed in the previous release

--

## [[3.0.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.0.0) - 2022-12-08

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
- Updated Help
- OutputType has been updated with correct Interface Types
- DefaultParameterSet has been removed
- Corrected ordering of each ParameterSet
- Added Private properties to clean up instantiating the objects
- Updated Tests to accomodate all the changes to the Cmdlets
- Set defaults for parameters
- Moved Database and Collection code into the BeginProcessing method

---

## [[2.4.2]](https://github.com/PoshMongo/PoshMongo/releases/tag/v2.4.2) - 2022-12-01

This change applies an update to how the RemoveCollectionCmdlet works, it should now use the Namespace for deletes.

The following cahnges have been applied:

- RemoveCollectionCmdlet
  - CollectionNamespace is used for delete operations
  - Return null

---

## [[2.4.1]](https://github.com/PoshMongo/PoshMongo/releases/tag/v2.4.1) - 2022-12-01

This change applies minor updates to have the Cmdlets work.

The following changes have been applied:

- Project Changes
  - Updates to psake file for working locally
  - Updated Documentation
- Updated Cmdlets
  - Adjusted ordering of Parameters
  - Resolved some null reference issues
  - Moved HelpUri to the correct location
- Added remaining Tests

---

## [[2.4.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v2.4.0) - 2022-11-30

This change continues adding support for the pipeline.

The following changes have been applied:

- Project Changes
  - Updates to psake file for working locally
  - Updated Documentation
- Added Pipeline Support
  - NewDatabaseCmdlet supports a Client on the pipeline
  - GetDatabaseCmdlet supports a Client on the pipeline
  - RemoveDatabaseCmdlet support a Database the pipeline
- Updated Tests for pipelines

---

## [[2.3.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v2.3.0) - 2022-11-21

This change continues adding support for pipeline to cmdlets. Pester tests have been added to validate Cmdlet functionality. Fixed up the Document Cmdlets with additional options for ID's.

The following changes have been applied:

- Project Changes
  - Updates to psake file for working locally
  - Updated Documentation
- Added Pipeline Support
  - AddDocumentCmdlet supports a Document on the pipeline
  - GetDocumentCmdlet supports a Colletion on the pipeline
  - RemoveDocumentCmdlet support a Document on the pipeline
- DocumentCmdlet updates
  - Added the ability to pass an ObjectID
  - Added a switch to supress the ID field
  - Added a switch to list all documents
- Pester Tests have been added

---

## [[2.2.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v2.2.0) - 2022-11-17

This change adds pipeline support for the Collection Cmdlets.

The following changes have been applied:

- Project Changes
  - Updates to psake file for working locally
  - Updated Documentation
- Added Pipeline Support
  - RemoveCollectionCmdlet supports a Collection on the pipeline
  - NewCollectionCmdlet supports a Database on the pipeline
  - GetCollectionCmdlet supports a Database on the pipeline

---

## [[1.1.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v1.1.0) - 2022-11-16

Minor Release

The following changes have been applied:

- Project Changes
  - Changes to psake file
  - Added local directories to .gitignore
  - Corrected issues with README.md
- Needed to include all the DLLs for the module to work
- Added Online Help Support

---

## [[1.0.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v1.0.0) - 2022-11-16

First release of PowerShell Module for MongoDB, this module provides basic CRUD operations against a MongoDB or Azure CosmosDB with the MongoDB API backend.

The following changes have been applied:

- Project Setup
  - Added .gitignore
  - Added README.md
  - Added Module Manifest
  - Added several local files to .gitignore
- Added Cmdlets
  - ConnectInstanceCmdlet
  - NewDatabaseCmdlet
  - GetDatabaseCmdlet
  - RemoveDatabaseCmdlet
  - NewCollectionCmdlet
  - GetCollectionCmdlet
  - RemoveCollectionCmdlet
  - AddDocumentCmdlet
  - GetDocumentCmdlet
  - RemoveDocumentCmdlet
