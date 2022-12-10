using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using System.Management.Automation;

namespace PoshMongo.Database
{
    [Cmdlet(VerbsCommon.New, "Database", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/New-MongoDBDatabase0.md#new-mongodbdatabase")]
    [OutputType("MongoDB.Driver.IMongoDatabase")]
    [CmdletBinding(PositionalBinding = true)]
    public class NewDatabase : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")]
        public IMongoClient? Client { get; set; } = null;
        protected override void BeginProcessing()
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
            WriteObject(Operations.NewDatabase(Client, DatabaseName));
        }
    }
}