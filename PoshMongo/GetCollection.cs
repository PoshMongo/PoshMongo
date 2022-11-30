using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Get, "Collection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBCollection.md#get-mongodbcollection", PositionalBinding = true)]
    public class GetCollectionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Collection")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Database")]
        public string? CollectionName { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string? DatabaseName { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public MongoDatabaseBase? MongoDatabase { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNamespace")]
        public string? CollectionNamespace { get; set; }
        protected override void ProcessRecord()
        {
            if (MongoDatabase == null)
            {
                MongoDatabase = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            }
            if (!(string.IsNullOrEmpty(DatabaseName)))
            {
                MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
                MongoDatabase = (MongoDatabaseBase)Client.GetDatabase(DatabaseName);
            }
            switch (ParameterSetName)
            {
                case "CollectionNamespace":
                    if (!(string.IsNullOrEmpty(CollectionNamespace)))
                    {
                        // Get-MongoDbCollection -CollectionNameSpace foo.bar
                        WriteObject(GetCollection(CollectionNamespace.Split('.')[0], CollectionNamespace.Split('.')[1]));
                    }
                    break;
                case "Database":
                    if (!(MongoDatabase == null))
                    {
                        // Get-MongoDbCollection -MongoDatabase $DB
                        // $DB |Get-MongoDbCollection
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            WriteObject(GetCollection(MongoDatabase));
                        }
                        else
                        {
                            // Get-MongoDbCollection -MongoDatabase $DB -CollectionName bar
                            // $DB |Get-MongoDbCollection -CollectionName bar
                            WriteObject(GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(DatabaseName)))
                    {
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            // Get-MongoDbCollection -DatabaseName foo
                            WriteObject(GetCollection(MongoDatabase));
                        } else
                        {
                            // Get-MongoDbCollection -DatabaseName foo -CollectionName bar
                            WriteObject(GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
                case "Collection":
                    if (string.IsNullOrEmpty(CollectionName))
                    {
                        // Get-MongoDbCollection
                        WriteObject(GetCollection(MongoDatabase));
                    }
                    else
                    {
                        // Get-MongoDbCollection -CollectionName bar
                        WriteObject(GetCollection(MongoDatabase, CollectionName));
                    }
                    break;
            }
        }
        private List<IMongoCollection<BsonDocument>> GetCollection(MongoDatabaseBase mongoDatabase)
        {
            List<IMongoCollection<BsonDocument>> Collections = new List<IMongoCollection<BsonDocument>>();
            foreach (string collectionName in mongoDatabase.ListCollectionNames().ToEnumerable())
            {
                Collections.Add(mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings()));
            }
            return Collections;
        }
        private IMongoCollection<BsonDocument> GetCollection(MongoDatabaseBase mongoDatabase, string collectionName)
        {
            IMongoCollection<BsonDocument> Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            SetVariable("Collection", Collection);
            return Collection;
        }
        private IMongoCollection<BsonDocument> GetCollection(string DatabaseName, string collectionName)
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            IMongoDatabase Database = Client.GetDatabase(DatabaseName);
            SetVariable("Database", Database);
            IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            SetVariable("Collection", Collection);
            return Collection;
        }
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}