using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Management.Automation;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Remove, "Document", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBDocument.md#remove-mongodbdocument")]
    [OutputType("null")]
    [CmdletBinding(PositionalBinding = true)]
    public class RemoveDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionId")]
        public string DocumentId { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionFilter")]
        public Hashtable Filter { get; set; } = new Hashtable();
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DocumentCollectionName")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "DocumentCollectionName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DocumentCollection")]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; } = null;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentCollection", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentCollectionName", ValueFromPipeline = true)]
        public string Document { get; set; } = string.Empty;
        private IMongoClient? Client { get; set; } = null;
        private IMongoDatabase? MongoDatabase { get; set; } = null;
        private BsonDocument? bsonDocument { get; set; } = null;
        protected override void ProcessRecord()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            if (!(string.IsNullOrEmpty(Document)))
            {
                bsonDocument = BsonDocument.Parse(Document);
                DocumentId = (string)bsonDocument.GetValue("_id");
            }
            if (!(string.IsNullOrEmpty(DatabaseName)) && !(string.IsNullOrEmpty(CollectionName)))
            {
                MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                MongoCollection = Operations.GetCollection(MongoDatabase, CollectionName);
            }
            switch (ParameterSetName)
            {
                case "CollectionNameId":
                    if (MongoCollection != null)
                    {
                        Operations.RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
                case "CollectionNameFilter":
                    if (MongoCollection != null)
                    {
                        Operations.RemoveDocument(MongoCollection, Filter);
                    }
                    break;
                case "CollectionId":
                    if (MongoCollection != null)
                    {
                        Operations.RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
                case "CollectionFilter":
                    if (MongoCollection != null)
                    {
                        Operations.RemoveDocument(MongoCollection, Filter);
                    }
                    break;
                case "DocumentCollection":
                    if (MongoCollection != null)
                    {
                        Operations.RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
                case "DocumentCollectionName":
                    if (MongoCollection != null)
                    {
                        Operations.RemoveDocument(MongoCollection, DocumentId);
                    }
                    break;
            }
        }
    }
}