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
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Collection")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Database")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public MongoDatabaseBase? MongoDatabase { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNamespace")]
        public string CollectionNamespace { get; set; } = string.Empty;
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
            if (!(string.IsNullOrEmpty(CollectionNamespace)))
            {
                MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
                DatabaseName = CollectionNamespace.Split('.')[0];
                CollectionName = CollectionNamespace.Split('.')[1];
                MongoDatabase = (MongoDatabaseBase)Client.GetDatabase(DatabaseName);
            }
            switch (ParameterSetName)
            {
                case "CollectionNamespace":
                    if (!(string.IsNullOrEmpty(CollectionNamespace)))
                    {
                        WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                    }
                    break;
                case "Database":
                    if (!(MongoDatabase == null))
                    {
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase));
                        }
                        else
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(DatabaseName)))
                    {
                        if (string.IsNullOrEmpty(CollectionName))
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase));
                        } else
                        {
                            WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                        }
                    }
                    break;
                case "Collection":
                    if (string.IsNullOrEmpty(CollectionName))
                    {
                        WriteObject(Operations.GetCollection(MongoDatabase));
                    }
                    else
                    {
                        WriteObject(Operations.GetCollection(MongoDatabase, CollectionName));
                    }
                    break;
            }
        }
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}