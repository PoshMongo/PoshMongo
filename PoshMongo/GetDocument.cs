using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Get, "Document", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBDocument.md#get-mongodbdocument")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(PositionalBinding = true)]
    public class GetDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionId")]
        public string DocumentId { get; set; } = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameFilter")]
        public Hashtable Filter { get; set; } = new Hashtable();

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameList")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameList")]
        public string DatabaseName { get; set; } = string.Empty;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionFilter", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionList", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; }

        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "CollectionList")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionNameList")]
        public SwitchParameter HideId { get; set; }
        
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionList")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionNameList")]
        public SwitchParameter List { get; set; }
        private IMongoDatabase? MongoDatabase { get; set; }
        private IMongoClient? Client { get; set; } = null;
        protected override void ProcessRecord()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            ObjectId objectId;
            switch (ParameterSetName)
            {
                case "CollectionNameId":
                    //Get-MongoDBDocument [-DocumentId] <string> [-CollectionName] <string> [-DatabaseName] <string> [[-HideId]] [<CommonParameters>]
                    MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                    MongoCollection = Operations.GetCollection(MongoDatabase, CollectionName);
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                    }
                    else
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, DocumentId, HideId));
                    }
                    break;
                case "CollectionNameFilter":
                    //Get-MongoDBDocument [-Filter] <hashtable> [-CollectionName] <string> [-DatabaseName] <string> [[-HideId]] [<CommonParameters>]
                    MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                    MongoCollection = Operations.GetCollection(MongoDatabase, CollectionName);
                    WriteObject(Operations.GetDocument(MongoCollection, Filter, HideId));
                    break;
                case "CollectionId":
                    //Get-MongoDBDocument [-DocumentId] <string> [-MongoCollection] <IMongoCollection[BsonDocument]> [[-HideId]] [<CommonParameters>]
                    if (MongoCollection != null)
                    {
                        if (ObjectId.TryParse(DocumentId, out objectId))
                        {
                            WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                        }
                        else
                        {
                            WriteObject(Operations.GetDocument(MongoCollection, DocumentId, HideId));
                        }
                    }
                    break;
                case "CollectionFilter":
                    //Get-MongoDBDocument [-Filter] <hashtable> [-MongoCollection] <IMongoCollection[BsonDocument]> [[-HideId]] [<CommonParameters>]
                    if (MongoCollection != null)
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, Filter, HideId));
                    }
                    break;
                case "CollectionList":
                    //Get-MongoDBDocument [-MongoCollection] <IMongoCollection[BsonDocument]> [[-HideId]] [[-List]] [<CommonParameters>]
                    if (MongoCollection != null)
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, HideId));
                    }
                    break;
                case "CollectionNameList":
                    //Get-MongoDBDocument [-CollectionName] <string> [-DatabaseName] <string> [[-HideId]] [[-List]] [<CommonParameters>]
                    MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                    MongoCollection = Operations.GetCollection(MongoDatabase, CollectionName);
                    WriteObject(Operations.GetDocument(MongoCollection, HideId));
                    break;
                default:
                    break;
            }
        }
    }
}