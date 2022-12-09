using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Remove, "Collection", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBCollection.md#remove-mongodbcollection")]
    [OutputType("null")]
    [CmdletBinding(PositionalBinding = true)]
    public class RemoveCollectionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Collection", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? Collection { get; set; }
        private IMongoDatabase? MongoDatabase { get; set; }
        private IMongoClient? Client { get; set; } = null;
        protected override void ProcessRecord()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            WriteVerbose("ParameterSetName : " + ParameterSetName);
            switch (ParameterSetName)
            {
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(CollectionName)) && !(string.IsNullOrEmpty(DatabaseName)))
                    {
                        if (Client != null)
                        {
                            WriteVerbose("DatabaseName: " + DatabaseName);
                            MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                        }
                        if (MongoDatabase != null)
                        {
                            WriteVerbose("CollectionName: " + CollectionName);
                            Operations.RemoveCollection(MongoDatabase, CollectionName);
                        }
                    }
                    break;
                default:
                    if (Collection != null)
                    {
                        WriteVerbose("DatabaseName: " + Collection.CollectionNamespace.DatabaseNamespace.DatabaseName);
                        WriteVerbose("CollectionName: " + Collection.CollectionNamespace.CollectionName);
                        MongoDatabase = Operations.GetDatabase(Client, Collection.CollectionNamespace.DatabaseNamespace.DatabaseName);
                        if (MongoDatabase != null)
                        {
                            Operations.RemoveCollection(MongoDatabase, Collection.CollectionNamespace.CollectionName);
                        }
                    }
                    break;
            }
        }
    }
}