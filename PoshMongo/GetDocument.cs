﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Management.Automation;
using System.Text.Json;

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
        public string Filter { get; set; } = string.Empty;

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
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; } = null;

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
        private IMongoDatabase? MongoDatabase { get; set; } = null;
        private IMongoClient? Client { get; set; } = null;
        private FilterDefinition<BsonDocument>? BFilter { get; set; }
        protected override void BeginProcessing()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            if (!(string.IsNullOrEmpty(DatabaseName)) && !(string.IsNullOrEmpty(CollectionName)))
            {
                MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                MongoCollection = Operations.GetCollection(MongoDatabase, CollectionName);
            }
        }
        protected override void ProcessRecord()
        {
            ObjectId objectId;
            switch (ParameterSetName)
            {
                case "CollectionNameId":
                    if (ObjectId.TryParse(DocumentId, out objectId))
                    {
                        if (MongoCollection != null)
                        {
                            WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                        }
                    }
                    else
                    {
                        if (MongoCollection != null)
                        {
                            WriteObject(Operations.GetDocument(MongoCollection, DocumentId, HideId));
                        }
                    }
                    break;
                case "CollectionNameFilter":
                    BFilter = BsonDocument.Parse(Filter);
                    WriteVerbose(BFilter.ToJson());
                    if (MongoCollection != null)
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, BFilter, HideId));
                    }
                    break;
                case "CollectionId":
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
                    BFilter = BsonDocument.Parse(Filter);
                    if (MongoCollection != null)
                    {
                        WriteObject(Operations.GetDocument(MongoCollection, BFilter, HideId));
                    }
                    break;
                case "CollectionList":
                    if (MongoCollection != null)
                    {
                        string? DocId;
                        foreach (BsonDocument doc in MongoCollection.Find(new BsonDocument()).ToList())
                        {
                            DocId = doc.GetValue("_id").ToString();
                            if (!(string.IsNullOrEmpty(DocId)))
                            {
                                if (ObjectId.TryParse(DocId, out objectId))
                                {
                                    WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                                }
                                else
                                {
                                    WriteObject(Operations.GetDocument(MongoCollection, DocId, HideId));
                                }
                            }
                        }
                    }
                    break;
                case "CollectionNameList":
                    if (MongoCollection != null)
                    {
                        string? DocId;
                        foreach (BsonDocument doc in MongoCollection.Find(new BsonDocument()).ToList())
                        {
                            DocId = doc.GetValue("_id").ToString();
                            if (!(string.IsNullOrEmpty(DocId)))
                            {
                                if (ObjectId.TryParse(DocId, out objectId))
                                {
                                    WriteObject(Operations.GetDocument(MongoCollection, objectId, HideId));
                                }
                                else
                                {
                                    WriteObject(Operations.GetDocument(MongoCollection, DocId, HideId));
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}