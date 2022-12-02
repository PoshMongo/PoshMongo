using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;
using System.Reflection.Metadata;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Add, "Document", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Add-MongoDBDocument.md#add-mongodbdocument")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(PositionalBinding = true, DefaultParameterSetName = "Default")]
    public class AddDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionName")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Collection")]
        public string Document { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionName")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Collection", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; }
        protected override void ProcessRecord()
        {
            if (MongoCollection == null)
            {
                if (!(string.IsNullOrEmpty(CollectionName)))
                {
                    MongoDatabaseBase MongoDatabase = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                    MongoCollection = MongoDatabase.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings());
                }
                else
                {
                    MongoCollection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
                }
            }
            WriteVerbose(ParameterSetName);
            switch (ParameterSetName)
            {
                case "CollectionName":
                    WriteObject(Operations.AddDocument(MongoCollection, Document));
                    break;
                default:
                    MongoCollection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
                    WriteObject(Operations.AddDocument(MongoCollection, Document));
                    break;
            }
        }
    }
}