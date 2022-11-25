using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Get, "Document")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBDocument.md#get-mongodbdocument", PositionalBinding = true)]
    public class GetDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameId")]
        public string? DocumentId { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Filter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameFilter")]
        public Hashtable? Filter { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameId")]
        public string? CollectionName { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionFilter", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; }
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Filter")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionNameFilter")]
        public SwitchParameter HideId { get; set; }
        protected override void ProcessRecord()
        {
            if (MongoCollection == null)
            {
                if (string.IsNullOrEmpty(CollectionName))
                {
                    WriteVerbose("Get Collection");
                    MongoCollection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
                }
                else
                {
                    WriteVerbose("Set Collection");
                    MongoDatabaseBase MongoDatabase = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                    MongoCollection = MongoDatabase.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings());
                }
            }
            ObjectId objectId;
            WriteVerbose(ParameterSetName);
            switch (ParameterSetName)
            {
                case "Filter":
                    // Get-MongoDBDocument -Filter @{}
                    WriteObject(GetDocument(MongoCollection, Filter, HideId)); break;
                case "DocumentId":
                    WriteVerbose(HideId.ToString());
                    // Get-MongoDBDocument -DocumentId 1
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        WriteObject(GetDocument(MongoCollection, objectId, HideId));
                    }
                    else
                    {
                        WriteObject(GetDocument(MongoCollection, DocumentId, HideId));
                    }
                    break;
                case "CollectionNameId":
                    WriteVerbose(HideId.ToString());
                    // Get-MongoDBDocument -CollectionName bar -DocumentId 1
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        WriteObject(GetDocument(MongoCollection, objectId, HideId));
                    }
                    else
                    {
                        WriteObject(GetDocument(MongoCollection, DocumentId, HideId));
                    }
                    break;
                case "CollectionNameFilter":
                    WriteVerbose(HideId.ToString());
                    WriteObject(GetDocument(MongoCollection, Filter, HideId));
                    break;
                case "CollectionId":
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        WriteObject(GetDocument(MongoCollection, objectId, HideId));
                    }
                    else
                    {
                        WriteObject(GetDocument(MongoCollection, DocumentId, HideId));
                    }
                    break;
                case "CollectionFilter":
                    WriteObject(GetDocument(MongoCollection, Filter, HideId));
                    break;
                default:
                    break;
            }
        }
        private List<string> GetDocument(IMongoCollection<BsonDocument> Collection, bool noId)
        {
            List<string>Documents = new List<string>();
            //return Collection.AsQueryable().ToList().ToJson();
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                //return Collection.Find(new BsonDocument()).Project(projection).ToList().ToJson();
                foreach (BsonDocument doc in Collection.Find(new BsonDocument()).Project(projection).ToList())
                {
                    Documents.Add(doc.ToJson());
                }
            }
            else
            {
                //return Collection.Find(new BsonDocument()).ToList().ToJson();
                WriteVerbose(Collection.CollectionNamespace.CollectionName);
                WriteVerbose(Collection.Find(new BsonDocument()).ToList().Count.ToString());
                foreach (BsonDocument doc in Collection.Find(new BsonDocument()).ToList())
                {
                    Documents.Add(doc.ToJson());
                }
            }
            return Documents;
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection, string Id, bool noId)
        {
            WriteVerbose(Id);
            WriteVerbose(Id.GetType().FullName);
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", Id);
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(id).Project(projection).FirstOrDefault().ToJson();
            }
            else
            {
                WriteVerbose(Collection.Find(id).FirstOrDefault().Count().ToString());
                return Collection.Find(id).FirstOrDefault().ToJson();
            }
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection, ObjectId Id, bool noId)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(Id.ToString()));
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(id).Project(projection).FirstOrDefault().ToJson();
            }
            else
            {
                return Collection.Find(id).FirstOrDefault().ToJson();
            }
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection,Hashtable filter, bool noId)
        {
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
            foreach (string key in filter.Keys)
            {
                filters.Add(Builders<BsonDocument>.Filter.Eq(key, filter[key]));
            }
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.And(filters);
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(id).Project(projection).FirstOrDefault().ToJson();
            }
            else
            {
                return Collection.Find(id).FirstOrDefault().ToJson();
            }
        }
    }
}