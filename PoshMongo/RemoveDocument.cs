using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Remove, "Document", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBDocument.md#remove-mongodbdocument")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(PositionalBinding = true, DefaultParameterSetName = "DocumentId")]
    public class RemoveDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionId")]
        public string DocumentId { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Filter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionFilter")]
        public Hashtable Filter { get; set; } = new Hashtable();
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DocumentCollectionName")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DocumentCollection")]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentCollection", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentCollectionName", ValueFromPipeline = true)]
        public string Document { get; set; } = string.Empty;
        protected override void ProcessRecord()
        {
            if (MongoCollection == null)
            {
                if (string.IsNullOrEmpty(CollectionName))
                {
                    MongoCollection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
                }
                else
                {
                    MongoDatabaseBase MongoDatabase = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                    MongoCollection = MongoDatabase.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings());
                }
            }
            BsonDocument bsonDocument;
            if (!(string.IsNullOrEmpty(Document)))
            {
                bsonDocument = BsonDocument.Parse(Document);
                DocumentId = (string)bsonDocument.GetValue("_id");
            }
            switch (ParameterSetName)
            {
                case "Filter":
                    if (!(Filter == null))
                    {
                        RemoveDocument(MongoCollection, Filter);
                    }
                    break;
                case "CollectionNameId":
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
                case "CollectionNameFilter":
                    if (!(Filter == null))
                    {
                        RemoveDocument(MongoCollection, Filter);
                    }
                    break;
                case "CollectionId":
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
                case "CollectionFilter":
                    if (!(Filter == null))
                    {
                        RemoveDocument(MongoCollection, Filter);
                    }
                    break;
                default:
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
            }
        }
        private static void RemoveDocument(IMongoCollection<BsonDocument> Collection, string documentID)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", documentID);
            Collection.DeleteOne(id);
        }
        private static void RemoveDocument(IMongoCollection<BsonDocument> Collection, Hashtable filter)
        {
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
            foreach (string key in filter.Keys)
            {
                filters.Add(Builders<BsonDocument>.Filter.Eq(key, filter[key]));
            }
            FilterDefinition<BsonDocument> result = Builders<BsonDocument>.Filter.And(filters);
            Collection.DeleteOne(result);
        }
    }
}