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
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Database")]
        public string? CollectionName { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        public string? DatabaseName { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Database", ValueFromPipeline = true)]
        public MongoDatabaseBase? MongoDatabase { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNamespace")]
        public string? CollectionNamespace { get; set; }
        protected override void ProcessRecord()
        {
            WriteVerbose("ParameterSetName: " + ParameterSetName);
            switch (ParameterSetName)
            {
                case "CollectionNamespace":
                    if (!(string.IsNullOrEmpty(CollectionNamespace)))
                    {
                        WriteObject(SessionState.PSVariable.Get("Collection").Value);
                    }
                    break;
                case "Database":
                    if (!(MongoDatabase == null))
                    {
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            foreach (string collectionName in MongoDatabase.ListCollectionNames().ToEnumerable())
                            {
                                WriteObject(GetCollection(collectionName, MongoDatabase));
                            }
                        }
                        else
                        {
                            WriteObject(GetCollection(CollectionName, MongoDatabase));
                        }
                    }
                    break;
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(CollectionName) || DatabaseName == null))
                    {
                        WriteObject(GetCollection(CollectionName, DatabaseName));
                    }
                    break;
                case "Collection":
                    if (!(string.IsNullOrEmpty(CollectionName)))
                    {
                        WriteObject(GetCollection(CollectionName));
                    }
                    else
                    {
                        MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                        foreach (string collectionName in Database.ListCollectionNames().ToEnumerable())
                        {
                            WriteObject(GetCollection(collectionName));
                        }
                    }
                    break;
            }
        }
        private IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            SetVariable("Collection", Collection);
            return Collection;
        }
        private IMongoCollection<BsonDocument> GetCollection(string collectionName, string DatabaseName)
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            IMongoDatabase Database = Client.GetDatabase(DatabaseName);
            SetVariable("Database", Database);
            IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            SetVariable("Collection", Collection);
            return Collection;
        }
        private IMongoCollection<BsonDocument> GetCollection(string collectionName, MongoDatabaseBase mongoDatabase)
        {
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