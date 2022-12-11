using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Get, "Collection", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBCollection.md#get-mongodbcollection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(PositionalBinding = true)]
    public class GetCollectionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Database")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public IMongoDatabase? MongoDatabase { get; set; } = null;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNamespace")]
        public string CollectionNamespace { get; set; } = string.Empty;
        private IMongoClient? Client { get; set; } = null;
        protected override void BeginProcessing()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            if (!(string.IsNullOrEmpty(DatabaseName)))
            {
                MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
            }
            if (!(string.IsNullOrEmpty(CollectionNamespace)))
            {
                DatabaseName = CollectionNamespace.Split('.')[0];
                CollectionName = CollectionNamespace.Split('.')[1];
                MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
            }
        }
        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "CollectionNamespace":
                    if (!(MongoDatabase == null))
                    {
                        if (!(string.IsNullOrEmpty(CollectionNamespace)))
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
                case "Database":
                    if (!(MongoDatabase == null))
                    {
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            foreach (string collectionName in MongoDatabase.ListCollectionNames().ToEnumerable())
                            {
                                WriteObject(Operations.GetCollection(MongoDatabase, collectionName));
                            }
                        }
                        else
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
                case "DatabaseName":
                    if (!(MongoDatabase == null))
                    {
                        if (!(string.IsNullOrEmpty(DatabaseName)))
                        {
                            if (string.IsNullOrEmpty(CollectionName))
                            {
                                foreach (string collectionName in MongoDatabase.ListCollectionNames().ToEnumerable())
                                {
                                    WriteObject(Operations.GetCollection(MongoDatabase, collectionName));
                                }
                            }
                            else
                            {
                                WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                            }
                        }
                    }
                    break;
                case "Collection":
                    if (!(MongoDatabase == null))
                    {
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            foreach (string collectionName in MongoDatabase.ListCollectionNames().ToEnumerable())
                            {
                                WriteObject(Operations.GetCollection(MongoDatabase, collectionName));
                            }
                        }
                        else
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
            }
        }
    }
}