using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;
using System.Reflection.Metadata;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Add, "Document")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Add-MongoDBDocument.md#add-mongodbdocument", PositionalBinding = true)]
    public class AddDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? Document { get; set; }
        protected override void ProcessRecord()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            if (!(string.IsNullOrEmpty(Document)))
            {
                WriteObject(AddDocument(Collection, Document));
            }
        }
        private string AddDocument(IMongoCollection<BsonDocument> Collection, string document)
        {
            BsonDocument bsonDocument = BsonDocument.Parse(document);
            Collection.InsertOne(bsonDocument);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument["_id"]);
            return Collection.Find(filter).FirstOrDefault().ToJson();
        }
    }
}