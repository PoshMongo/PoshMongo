using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;
using System.Reflection.Metadata;

namespace PoshMongo
{
    [Cmdlet(VerbsCommon.Add, "Document")]
    public class AddDocument : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? Document { get; set; }
        protected override void BeginProcessing()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            BsonDocument bsonDocument = BsonDocument.Parse(Document);
            Collection.InsertOne(bsonDocument);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument["_id"]);
            WriteObject(Collection.Find(filter).FirstOrDefault().ToJson());

        }
    }
}