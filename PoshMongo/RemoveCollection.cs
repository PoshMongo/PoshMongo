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
        public IMongoCollection<BsonDocument>? Collection { get; set; } = null;
        private IMongoDatabase? MongoDatabase { get; set; } = null;
        private IMongoClient? Client { get; set; } = null;
        protected override void ProcessRecord()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            if (!(string.IsNullOrEmpty(CollectionName)))
            {
                MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
            }
            if (Collection != null)
            {
                MongoDatabase = Operations.GetDatabase(Client, Collection.CollectionNamespace.DatabaseNamespace.DatabaseName);
            }
            switch (ParameterSetName)
            {
                case "DatabaseName":
                    if (MongoDatabase != null)
                    {
                        Operations.RemoveCollection(MongoDatabase, CollectionName);
                    }
                    break;
                case "Collection":
                    if (MongoDatabase != null && Collection != null)
                    {
                        Operations.RemoveCollection(MongoDatabase, Collection.CollectionNamespace.CollectionName);
                    }
                    break;
            }
        }
    }
}