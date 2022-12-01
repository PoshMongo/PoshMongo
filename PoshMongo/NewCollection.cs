using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.New, "Collection", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/New-MongoDBCollection.md#new-mongodbcollection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(PositionalBinding = true)]
    public class NewCollectionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionName")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Database")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public MongoDatabaseBase? MongoDatabase { get; set; }
        protected override void ProcessRecord()
        {
            WriteVerbose("ParameterSetName: " + ParameterSetName);
            switch (ParameterSetName)
            {
                case "CollectionName":
                    if (!(string.IsNullOrEmpty(CollectionName)))
                    {
                        WriteObject(NewCollection(CollectionName));
                    }
                    break;
                case "DatabaseName":
                    if ( !(string.IsNullOrEmpty(CollectionName)) && !(string.IsNullOrEmpty(DatabaseName)))
                    {
                        WriteObject(NewCollection(CollectionName, DatabaseName));
                    }
                    break;
                case "Database":
                    if (!(string.IsNullOrEmpty(CollectionName)) && !(MongoDatabase == null))
                    {
                        WriteObject(NewCollection(CollectionName, MongoDatabase));
                    }
                    break;
                default:
                    break;
            }
        }
        private IMongoCollection<BsonDocument> NewCollection(string collectionName)
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            Database.CreateCollection(collectionName, new CreateCollectionOptions(), new CancellationToken());
            IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            SetVariable("Collection", Collection);
            return Collection;

        }
        private IMongoCollection<BsonDocument> NewCollection(string collectionName, string databaseName)
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            IMongoDatabase Database = Client.GetDatabase(databaseName);
            SetVariable("Database", Database);
            Database.CreateCollection(collectionName, new CreateCollectionOptions(), new CancellationToken());
            IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            return Collection;
        }
        private IMongoCollection<BsonDocument> NewCollection(string collectionName, MongoDatabaseBase mongoDatabase)
        {
            mongoDatabase.CreateCollection(collectionName, new CreateCollectionOptions(), new CancellationToken());
            IMongoCollection<BsonDocument> Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            SetVariable("Collection", Collection);
            return Collection;
        }
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}