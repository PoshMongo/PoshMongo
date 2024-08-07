# Changelog

All changes to this module should be reflected in this document.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [[3.3.1]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.3.1) - 2024-07-19

This is a minor release that updated some of the internal documentation as it was outdated and confusing.

--

## [[3.3.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.3.0) - 2022-12-14

These changes allow the PowerShell cmdlet to leverage the `-Force` parameter to control upsert behavior when adding documents to a MongoDB collection.

### Operations Class

1. **Method Enhancement**:
   - Enhanced the `AddDocument` method to accept an additional boolean parameter for upsert functionality (`isUpsert`).

2. **Upsert Logic**:
   - Incorporated logic to replace an existing document if `isUpsert` is true, otherwise, it performs a standard insert.

#### Cmdlet Class

1. **Force Parameter Addition**:
   - Added a `Force` parameter to the cmdlet, enabling users to specify whether the document should be upserted.

2. **Determine Upsert Based on Force**:
   - Used the presence of the `Force` switch to determine the value of the `isUpsert` parameter.

3. **Integration with Operations Class**:
   - Passed the determined `isUpsert` value from the cmdlet to the `AddDocument` method in the `Operations` class.

--

## [[3.2.1]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.2.1) - 2022-12-14

This is a minor release to fix the Update-Help issue #25. The Manifest was missing the HelpInfoUri, and the HelpInfo.xml is generated based on values in the PoshMongo.md file. This file had an incorrect download location so this has been updated.

The following changes have been applied:

- PoshMongo.psd1
  - Added the missing value for HelpInfoURI
- PoshMongo.md
  - Added the correct value for Download Help Link
- Updated Help

--

## [[3.2.0]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.2.0) - 2022-12-12

This release resolves a bugfix for Get-MongoDBDocument, where it encounters an error when attempting to return BsonDocument ObjectID. This release also adds a feature to Get-MongoDBDocument, you can now pass in a filter as a string. For more details on specifics please reference this [document](https://mongodb.github.io/mongo-csharp-driver/2.18/reference/driver/definitions/#filters).

For example:

```powershell
Get-MongoDBDocument -Filter '{"_id" : "1" }' -CollectionName 'myCollection2' -Database 'MyDB'
{
  "_id": "1",
  "LastName": "Smith",
  "FirstName": "John"
}
```

The following changes have been applied:

- GetDocumentCmdlet
  - Updated GetDocument() methods to return pretty json
  - Filter Update
    - Filter now accepts a json string as a filter
- Updated Tests
- Updated Help

--

## [[3.1.1]](https://github.com/PoshMongo/PoshMongo/releases/tag/v3.1.1) - 2022-12-11

This release is a bugfix for the Cmdlets that return a List<T> of obejcts. In order for Cmdlets to process objects
on the cmdline they need to come in one at a time. If the output of one cmdlet is a List object, it must first be
expanded, either from GetEnumerator() or a Foreach-Object.

The following changes have been applied:

- GetDatabaseCmdlet
  - Instead of passing the client into a method and iterating there, we now iterate over the client and pass the Client and DatabaseName to the method
  - Removed the GetDatabase(Client) method as it's no longer required
- GetCollectionCmdlet
  - Instead of passing the Database into a method and iterating there, we now iterate over the Database and pass the Database and CollectionName to the method
  - Removed the GetCollection(Database) method as it's no longer required
- GetDocumentCmdlet
  - Instead of passing the Collection into a method and iterating there, we now iterate over the Collection and pass the Collection and DocumentId to the method
  - Removed the GetDocument(Collection) method as it's no longer required
- Updated Tests
- Updated Help

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
