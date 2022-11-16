# PoshMongo

## about_PoshMongo

# SHORT DESCRIPTION

PoshMongo is a collection of PowerShell Cmdlets that uses the MongoDB.Driver to
allow you to work with MongoDB.

# LONG DESCRIPTION

The module provides basic CRUD operations on a MongoDB Instance. It allows you
to create Databases and Collections and add documents to those collections. It
also provides the ability to find specific documents via _id, or a simple
filter. Additionally you can remove Documents, Collections and Databases as
needed.

## EXAMPLES

Using the module is very straightforward, after you have installed the module
from the PowerShell Gallery or directly from Github, you will need to import it.

```powershell
#
# Import Module
#
Import-Module PoshMongo
#
# Connect to Mongo
#
Connect-MongoDBInstance -ConnectionString $StoredConnectionString
#
# Get a database
#
Get-MongoDBDatabase -DatabaseName MyDB
#
# Get a collection
#
Get-MongoDBCollection -CollectionName MyCollection
#
# Add a document to the collection
#
Add-MongoDBDocument -Document '{"_id":"4e27b0f5-aaa0-4d4d-bdc8-43f811242d93","Name":"FirstName"}'
#
# Get a list of collections
#
Get-MongoDBCollection
#
# Get a list of documents
#
Get-MongoDBDocument
#
# Get a specific Document
#
Get-MongoDBDocument -DocumentId "4e27b0f5-aaa0-4d4d-bdc8-43f811242d93"
#
# Remove a Document
#
Remove-MongoDBDocument -DocumentId "4e27b0f5-aaa0-4d4d-bdc8-43f811242d93"
#
# Remove a collection
#
Remove-MongoDBCollection -CollectionName MyCollection
#
# Remove a database
#
Remove-MongoDBDatabase -DatabaseName MyDB
```

# NOTE

If you are connecting to Azure CosmosDB with a MongoDB Backend, you will need
to provide the -ForceTLS12 switch in the Connect-MongoDBInstance cmdlet.

A New Database/Collection are not written until data is stored in them, so when
you create a new database it is not stored in Mongo until _after_ you have
added a collection. Likewise a collection is not written to the database until
after a document is stored within the collection.

# TROUBLESHOOTING NOTE

Nothing to see here currently

# SEE ALSO

[Reader](https://github.com/PoshMongo/PoshMongo/blob/main/README.md)

[MongoDB CRUD Operations](https://www.mongodb.com/basics/crud)
