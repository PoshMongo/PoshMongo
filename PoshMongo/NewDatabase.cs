﻿using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Database
{
    [Cmdlet(VerbsCommon.New, "Database", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/New-MongoDBDatabase0.md#new-mongodbdatabase")]
    [OutputType("MongoDB.Driver.MongoDatabaseBase")]
    [CmdletBinding(PositionalBinding = true)]
    public class NewDatabase : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")]
        public MongoClient? Client { get; set; }
        protected override void BeginProcessing()
        {
            if (Client == null)
            {
                Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            } else
            {
                throw new PSArgumentNullException("Client", "Must be connected to a MongoDB instance.");
            }
            SessionState.PSVariable.Set("Database", Client.GetDatabase(DatabaseName));
            WriteObject(SessionState.PSVariable.Get("Database").Value);
        }
    }
}