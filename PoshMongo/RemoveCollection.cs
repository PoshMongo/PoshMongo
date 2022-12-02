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
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionName")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Collection", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? Collection { get; set; }
        private IMongoDatabase? MongoDatabase { get; set; }
        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                MongoDatabase = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            } else
            {
                MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
                if (Client != null)
                {
                    MongoDatabase = Client.GetDatabase(DatabaseName);
                }
            }
            if (Collection != null)
            {
                MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
                MongoDatabase = Client.GetDatabase(Collection.CollectionNamespace.DatabaseNamespace.DatabaseName);
            }
            switch (ParameterSetName)
            {
                case "CollectionName":
                    if (!(string.IsNullOrEmpty(CollectionName)))
                    {
                        if (MongoDatabase != null)
                        {
                            Operations.RemoveCollection(MongoDatabase, CollectionName);
                        }
                    }
                    break;
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(CollectionName)) && !(string.IsNullOrEmpty(DatabaseName)))
                    {
                        if (MongoDatabase != null)
                        {
                            Operations.RemoveCollection(MongoDatabase, CollectionName);
                        }
                    }
                    break;
                case "Collection":
                    if (Collection != null)
                    {
                        if (MongoDatabase != null)
                        {
                            Operations.RemoveCollection(MongoDatabase, CollectionName);
                        }
                    }
                    break;
            }
        }
    }
}