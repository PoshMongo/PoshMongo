using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Remove, "Document")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBDocument.md#remove-mongodbdocument", PositionalBinding = true)]
    public class RemoveDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string? DocumentId { get; set; }
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Filter")]
        public Hashtable? Filter { get; set; }
        protected override void ProcessRecord()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            switch (ParameterSetName)
            {
                case "Filter":
                    if (!(Filter == null))
                    {
                        RemoveDocument(Collection, Filter);
                    }
                    break;
                default:
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        RemoveDocument(Collection, DocumentId);
                    }
                    break;
            }
        }
        private void RemoveDocument(IMongoCollection<BsonDocument> Collection, string documentID)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", documentID);
            Collection.DeleteOne(id);
        }
        private void RemoveDocument(IMongoCollection<BsonDocument> Collection, Hashtable filter)
        {
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
            foreach (string key in filter.Keys)
            {
                filters.Add(Builders<BsonDocument>.Filter.Eq(key, filter[key]));
            }
            FilterDefinition<BsonDocument> result = Builders<BsonDocument>.Filter.And(filters);
            Collection.Find(result).FirstOrDefault().ToJson();
        }
    }
}