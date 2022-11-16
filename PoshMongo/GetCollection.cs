using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Get, "Collection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/docs/Get-MongoDBCollection.md#get-mongodbcollection", PositionalBinding = true)]
    public class GetCollection : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Default")]
        public string? CollectionName { get; set; }
        protected override void BeginProcessing()
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            MongoCollectionSettings settings = new();
            if (!(string.IsNullOrEmpty(CollectionName)))
            {
                SessionState.PSVariable.Set("Collection", Database.GetCollection<BsonDocument>(CollectionName, settings));
                WriteObject(SessionState.PSVariable.Get("Collection").Value);
            }
            else
            {
                foreach (string collectionName in Database.ListCollectionNames().ToEnumerable())
                {
                    WriteObject(Database.GetCollection<BsonDocument>(collectionName, settings));
                }
            }
        }
    }
}