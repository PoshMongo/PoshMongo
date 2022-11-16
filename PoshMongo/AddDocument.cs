using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;
using System.Reflection.Metadata;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Add, "Document")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/docs/Add-MongoDBDocument.md#add-mongodbdocument", PositionalBinding = true)]
    public class AddDocument : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? Document { get; set; }
        protected override void BeginProcessing()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            BsonDocument bsonDocument = BsonDocument.Parse(Document);
            Collection.InsertOne(bsonDocument);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument["_id"]);
            WriteObject(Collection.Find(filter).FirstOrDefault().ToJson());

        }
    }
}