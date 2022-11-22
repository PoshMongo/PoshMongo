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
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Collection")]
        public string? DocumentId { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Filter")]
        public Hashtable? Filter { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Collection")]
        public string? CollectionName { get; set; }
        [Parameter(Mandatory = false, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = false, ParameterSetName = "Collection")]
        [Parameter(Mandatory = false, ParameterSetName = "Filter")]
        public SwitchParameter HideId { get; set; }
        protected override void ProcessRecord()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            switch (ParameterSetName)
            {
                case "Filter":
                    if (!(Filter == null))
                    {
                        WriteObject(GetDocument(Collection, Filter, HideId));
                    }
                    break;
                case "DocumentId":
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        WriteObject(GetDocument(Collection, DocumentId, HideId));
                    }
                    else
                    {
                        WriteObject(GetDocument(Collection, HideId));
                    }
                    break;
                case "Collection":
                    MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        ObjectId objectId;
                        if (ObjectId.TryParse(DocumentId, out objectId))
                        {
                            WriteObject(GetDocument(Database.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings()), objectId, HideId));
                        } else
                        {
                            WriteObject(GetDocument(Database.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings()), DocumentId, HideId));
                        }
                    }
                    else
                    {
                        WriteObject(GetDocument(Database.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings()), HideId));
                    }
                    break;
            }
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection, bool noId)
        {
            //return Collection.AsQueryable().ToList().ToJson();
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(new BsonDocument()).Project(projection).ToList().ToJson();
            }
            else
            {
                return Collection.Find(new BsonDocument()).ToList().ToJson();
            }
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection, string Id, bool noId)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", Id);
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
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}