using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo
{
    [Cmdlet(VerbsCommon.Remove, "Document")]
    public class RemoveDocument : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? DocumentId { get; set; }
        protected override void BeginProcessing()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", DocumentId);
            WriteObject(Collection.DeleteOne(filter));
        }
    }
}