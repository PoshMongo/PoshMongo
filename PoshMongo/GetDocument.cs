using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo
{
    [Cmdlet(VerbsCommon.Get, "Document")]
    public class GetDocument : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Default")]
        public string? DocumentId { get; set; }
        protected override void BeginProcessing()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            BsonDocument bsonDocument = new();
            if (!(string.IsNullOrEmpty(DocumentId)))
            {
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", DocumentId);
                WriteObject(Collection.Find(filter).FirstOrDefault().ToJson());
            }
            else
            {
                foreach (BsonDocument document in Collection.Find(bsonDocument).ToList())
                {
                    WriteObject(document.ToJson());
                }
            }
        }
    }
}