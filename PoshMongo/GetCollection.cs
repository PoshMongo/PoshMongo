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
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Default")]
        public string? CollectionName { get; set; }
        protected override void BeginProcessing()
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            if (!(string.IsNullOrEmpty(CollectionName)))
            {
                SessionState.PSVariable.Set("Collection", DefaultCollection(CollectionName));
                WriteObject(SessionState.PSVariable.Get("Collection").Value);
            }
            else
            {
                foreach (string collectionName in Database.ListCollectionNames().ToEnumerable())
                {
                    WriteObject(DefaultCollection(collectionName));
                }
            }
        }
        private IMongoCollection<BsonDocument> DefaultCollection(string collectionName)
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            MongoCollectionSettings settings = new();
            return Database.GetCollection<BsonDocument>(collectionName, settings);
        }
    }
}