using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.Remove, "Collection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/docs/Remove-MongoDBCollection.md#remove-mongodbcollection", PositionalBinding = true)]
    public class RemoveCollection : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Default")]
        public string? CollectionName { get; set; }
        protected override void BeginProcessing()
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            MongoCollectionSettings settings = new();
            Database.DropCollection(CollectionName);
            foreach (string collectionName in Database.ListCollectionNames().ToEnumerable())
            {
                WriteObject(Database.GetCollection<BsonDocument>(collectionName, settings));
            }
        }
    }
}