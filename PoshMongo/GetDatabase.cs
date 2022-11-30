﻿using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using System.Management.Automation;

namespace PoshMongo.Database
{
    [Cmdlet(VerbsCommon.Get, "Database", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Get-MongoDBDatabase0.md#get-mongodbdatabase")]
    [OutputType("MongoDB.Driver.MongoDatabaseBase")]
    [CmdletBinding(PositionalBinding = true)]
    public class GetDatabase : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Default")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")]
        public MongoClient? Client { get; set; }
        protected override void BeginProcessing()
        {
            if (Client == null)
            {
                Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            }
            else
            {
                ServerDescription? server = Client.Cluster.Description.Servers.FirstOrDefault();
                if (server != null)
                {
                    throw new MongoConnectionException(new MongoDB.Driver.Core.Connections.ConnectionId(server.ServerId), "Must be connected to a MongoDB instance.");
                }
            }
            if (!(string.IsNullOrEmpty(DatabaseName)))
            {
                SessionState.PSVariable.Set("Database", Client.GetDatabase(DatabaseName));
                WriteObject(SessionState.PSVariable.Get("Database").Value);
            }
            else
            {
                foreach (string db in Client.ListDatabaseNames().ToEnumerable())
                {
                    WriteObject(Client.GetDatabase(db));
                }
            }
        }
    }
}