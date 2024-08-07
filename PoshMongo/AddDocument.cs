﻿using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;
using System.Reflection.Metadata;

namespace PoshMongo.Document
{
    [Cmdlet(VerbsCommon.Add, "Document", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Add-MongoDBDocument.md#add-mongodbdocument")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(PositionalBinding = true)]
    public class AddDocumentCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "CollectionName")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Collection")]
        public string Document { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "CollectionName")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "CollectionName")] 
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Collection", ValueFromPipeline = true)]
        public IMongoCollection<BsonDocument>? MongoCollection { get; set; } = null;
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }
        private IMongoDatabase? MongoDatabase { get; set; } = null;
        private IMongoClient? Client { get; set; } = null;
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
            bool isUpsert = Force.IsPresent;
            switch (ParameterSetName)
            {
                case "CollectionName":
                    if (MongoCollection != null)
                    {
                        WriteObject(Operations.AddDocument(MongoCollection, Document, isUpsert));
                    }
                    break;
                case "Collection":
                    if (MongoCollection != null)
                    {
                        WriteObject(Operations.AddDocument(MongoCollection, Document, isUpsert));
                    }
                    break;
            }
        }
    }
}