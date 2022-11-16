using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Get, "Document")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBDocument.md#get-mongodbdocument", PositionalBinding = true)]
    public class GetDocument : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string? DocumentId { get; set; }
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Filter")]
        public Hashtable? Filter { get; set; }
        protected override void BeginProcessing()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            BsonDocument bsonDocument = new();
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
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", DocumentId);
                        WriteObject(Collection.Find(id).FirstOrDefault().ToJson());
                    }
                    else
                    {
                        foreach (BsonDocument document in Collection.Find(bsonDocument).ToList())
                        {
                            WriteObject(document.ToJson());
                        }
                    }
                    break;
            }
        }
    }
}