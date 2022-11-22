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
        protected override void ProcessRecord()
        {
            IMongoCollection<BsonDocument> Collection = (IMongoCollection<BsonDocument>)SessionState.PSVariable.Get("Collection").Value;
            switch (ParameterSetName)
            {
                case "Filter":
                    if (!(Filter == null))
                    {
                        WriteObject(GetDocument(Collection, Filter));
                    }
                    break;
                case "DocumentId":
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        WriteObject(GetDocument(Collection, DocumentId));
                    }
                    else
                    {
                        WriteObject(GetDocument(Collection));
                    }
                    break;
                case "Collection":
                    MongoDatabaseBase Database = (MongoDatabaseBase)SessionState.PSVariable.Get("Database").Value;
                    if (!(string.IsNullOrEmpty(DocumentId)))
                    {
                        ObjectId objectId;
                        if (ObjectId.TryParse(DocumentId, out objectId))
                        {
                            WriteObject(GetDocument(Database.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings()), objectId));
                        } else
                        {
                            WriteObject(GetDocument(Database.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings()), DocumentId));
                        }
                    }
                    else
                    {
                        WriteObject(GetDocument(Database.GetCollection<BsonDocument>(CollectionName, new MongoCollectionSettings())));
                    }
                    break;
            }
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection)
        {
            return Collection.AsQueryable().ToList().ToJson();
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection, string Id)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", Id);
            return Collection.Find(id).FirstOrDefault().ToJson();
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection, ObjectId Id)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(Id.ToString()));            
            return Collection.Find(id).FirstOrDefault().ToJson();
        }
        private string GetDocument(IMongoCollection<BsonDocument> Collection,Hashtable filter)
        {
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
            foreach (string key in filter.Keys)
            {
                filters.Add(Builders<BsonDocument>.Filter.Eq(key, filter[key]));
            }
            FilterDefinition<BsonDocument> result = Builders<BsonDocument>.Filter.And(filters);
            return Collection.Find(result).FirstOrDefault().ToJson();
        }
        private void SetVariable(string VariableName, object Value)
        {
            SessionState.PSVariable.Set(VariableName, Value);
        }
    }
}