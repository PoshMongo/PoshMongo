using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using System.Management.Automation;

namespace PoshMongo.Database
{
    [Cmdlet(VerbsCommon.Remove, "Database", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBDatabase0.md#remove-mongodbdatabase")]
    [OutputType("null")]
    [CmdletBinding(PositionalBinding = true)]
    public class RemoveDatabase : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Database")]
        public IMongoClient? Client { get; set; }
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public IMongoDatabase? Database { get; set; }
        protected override void ProcessRecord()
        {
            if (Client == null)
            {
                Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            }
            else
            {
                ServerDescription server = Client.Cluster.Description.Servers[0];
                if (server != null)
                {
                    throw new MongoConnectionException(new MongoDB.Driver.Core.Connections.ConnectionId(server.ServerId), "Must be connected to a MongoDB instance.");
                }
            }
            if (Database == null)
            {
                Operations.RemoveDatabase(Client, DatabaseName);
            } else
            {
                Operations.RemoveDatabase(Client, Database.DatabaseNamespace.DatabaseName);
            }
        }
    }
}