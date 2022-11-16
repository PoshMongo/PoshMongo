using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Remove, "Document")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/docs/Remove-MongoDBDocument.md#remove-mongodbdocument", PositionalBinding = true)]
    public class RemoveDocument : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string? DocumentId { get; set; }
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Filter")]
        public Hashtable? Filter { get; set; }
        protected override void BeginProcessing()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            switch (ParameterSetName)
            {
                case "Filter":
                    if (!(Filter == null))
                    {
                        List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
                        foreach (string key in Filter.Keys)
                        {
                            filters.Add(Builders<BsonDocument>.Filter.Eq(key, Filter[key]));
                        }
                        FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.And(filters);
                        WriteObject(Collection.Find(filter).FirstOrDefault().ToJson());
                    }
                    break;
                default:
                    FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", DocumentId);
                    WriteObject(Collection.DeleteOne(id));
                    break;
            }

        }
    }
}