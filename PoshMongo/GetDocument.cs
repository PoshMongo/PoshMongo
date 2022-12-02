﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Management.Automation;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Get, "Document", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBDocument.md#get-mongodbdocument")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(PositionalBinding = true, DefaultParameterSetName = "Default")]
    public class GetDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameId")]
        public string DocumentId { get; set; } = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Filter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionNameFilter")]
        public Hashtable Filter { get; set; } = new Hashtable();

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionNameList")]
        public string CollectionName { get; set; } = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionFilter", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionList", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; }

        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "DocumentId")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Filter")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionId")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionFilter")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionNameId")]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "CollectionNameFilter")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionList")]
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = "CollectionNameList")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")] 
        public SwitchParameter HideId { get; set; }
        
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Default")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "CollectionList")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "CollectionNameList")]
        public SwitchParameter List { get; set; }
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
            switch (ParameterSetName)
            {
                case "Filter":
                    WriteObject(Operations.GetDocument(MongoCollection, Filter, HideId)); break;
                case "DocumentId":
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                    }
                    else
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, DocumentId, HideId));
                    }
                    break;
                case "CollectionNameId":
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
                    WriteObject(Operations.GetDocument(MongoCollection, Filter, HideId));
                    break;
                case "CollectionId":
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                    }
                    else
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, DocumentId, HideId));
                    }
                    break;
                case "CollectionFilter":
                    WriteObject(Operations.GetDocument(MongoCollection, Filter, HideId));
                    break;
                default:
                    WriteObject(Operations.GetDocument(MongoCollection, HideId));
                    break;
            }
        }
    }
}