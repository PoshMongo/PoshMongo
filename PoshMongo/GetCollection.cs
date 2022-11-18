using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Get, "Collection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBCollection.md#get-mongodbcollection", PositionalBinding = true)]
    public class GetCollection : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Collection")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
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
                        SetVariable("Collection", DatabaseNameCollection(CollectionNamespace.Split('.')[1], CollectionNamespace.Split('.')[0]));
                        WriteObject(SessionState.PSVariable.Get("Collection").Value);
                    }
                    break;
                case "Database":
                    if (!(MongoDatabase == null))
                    {
                        WriteVerbose("Database Switch");
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            WriteVerbose("No ColectionName");
                            foreach (string collectionName in MongoDatabase.ListCollectionNames().ToEnumerable())
                            {
                                WriteObject(DatabaseCollection(collectionName, MongoDatabase));
                            }
                        }
                        else
                        {
                            SetVariable("Collection", DatabaseCollection(CollectionName, MongoDatabase));
                            WriteObject(SessionState.PSVariable.Get("Collection").Value);
                        }
                    }
                    break;
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(CollectionName) || DatabaseName == null))
                    {
                        SetVariable("Collection", DatabaseNameCollection(CollectionName, DatabaseName));
                        WriteObject(SessionState.PSVariable.Get("Collection").Value);
                    }
                    break;
                case "Collection":
                    if (!(string.IsNullOrEmpty(CollectionName)))
                    {
                        SetVariable("Collection", DefaultCollection(CollectionName));
                        WriteObject(SessionState.PSVariable.Get("Collection").Value);
                    }
                    else
                    {
                        MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                        foreach (string collectionName in Database.ListCollectionNames().ToEnumerable())
                        {
                            WriteObject(DefaultCollection(collectionName));
                        }
                    }
                    break;
            }
        }
        private IMongoCollection<BsonDocument> DatabaseCollection(string collectionName, MongoDatabaseBase Database)
        {
            return Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
        }
        private IMongoCollection<BsonDocument> DefaultCollection(string collectionName)
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            return Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
        }
        private IMongoCollection<BsonDocument> DatabaseNameCollection(string collectionName, string DatabaseName)
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            IMongoDatabase Database = Client.GetDatabase(DatabaseName);
            SetVariable("Database", Database);
            return Database.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
        }
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}