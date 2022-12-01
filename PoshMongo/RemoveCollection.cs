using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Remove, "Collection", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBCollection.md#remove-mongodbcollection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(PositionalBinding = true)]
    public class RemoveCollectionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionName")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string? CollectionName { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        public string? DatabaseName { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Collection", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? Collection { get; set; }
        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "CollectionName":
                    if (!(string.IsNullOrEmpty(CollectionName)))
                    {
                        WriteObject(RemoveCollection(CollectionName));
                    }
                    break;
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(CollectionName)) && !(string.IsNullOrEmpty(DatabaseName)))
                    {
                        WriteObject(RemoveCollection(CollectionName, DatabaseName));
                    }
                    break;
                case "Collection":
                    if (Collection != null)
                    {
                        WriteObject(RemoveCollection(Collection, this.MyInvocation.ExpectingInput));
                    }
                    break;
            }

        }
        private List<IMongoCollection<BsonDocument>> RemoveCollection(string collectionName)
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            Database.DropCollection(collectionName);
            List<IMongoCollection<BsonDocument>> Collection = new List<IMongoCollection<BsonDocument>>();
            foreach (string collection in Database.ListCollectionNames().ToEnumerable())
            {
                Collection.Add(Database.GetCollection<BsonDocument>(collection, new MongoCollectionSettings()));
            }
            SetVariable("Collection", "");
            return Collection;
        }
        private List<IMongoCollection<BsonDocument>> RemoveCollection(string collectionName, string databaseName)
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            IMongoDatabase Database = Client.GetDatabase(databaseName);
            SetVariable("Database", Database);
            Database.DropCollection(collectionName);
            List<IMongoCollection<BsonDocument>> Collection = new List<IMongoCollection<BsonDocument>>();
            foreach (string collection in Database.ListCollectionNames().ToEnumerable())
            {
                Collection.Add(Database.GetCollection<BsonDocument>(collection, new MongoCollectionSettings()));
            }
            SetVariable("Collection", "");
            return Collection;
        }
        private List<IMongoCollection<BsonDocument>> RemoveCollection(IMongoCollection<BsonDocument> collection, bool pipeline)
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            Database.DropCollection(collection.CollectionNamespace.CollectionName);
            List<IMongoCollection<BsonDocument>> Collection = new List<IMongoCollection<BsonDocument>>();
            if (pipeline == false)
            {
                foreach (string collectionName in Database.ListCollectionNames().ToEnumerable())
                {
                    Collection.Add(Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings()));
                }
            }
            SetVariable("Collection", "");
            return Collection;
        }
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}