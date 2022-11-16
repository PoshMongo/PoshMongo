using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.New, "Collection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/New-MongoDBCollection.md#new-mongodbcollection", PositionalBinding = true)]
    public class NewCollection : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? CollectionName { get; set; }
        protected override void BeginProcessing()
        {
            MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
            Database.CreateCollection(CollectionName, new CreateCollectionOptions(), new CancellationToken());
            MongoCollectionSettings settings = new();
            SessionState.PSVariable.Set("Collection", Database.GetCollection<BsonDocument>(CollectionName, settings));
            WriteObject(SessionState.PSVariable.Get("Collection").Value);
        }
    }
}