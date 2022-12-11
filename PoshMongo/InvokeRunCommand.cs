using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using MongoDB.Driver.Core.Servers;
using System.Collections;
using System.Text.Json;
using System.Management.Automation;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace PoshMongo.Invoke
{
    [Cmdlet(VerbsLifecycle.Invoke, "RunCommand", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Invoke-MongoDBRunCommand.md#invoke-mongodbruncommand")]
    [OutputType("System.Text.Json")]
    [CmdletBinding(PositionalBinding = true)]
    public class InvokeRunCommand : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public IMongoDatabase? MongoDatabase { get; set; } = null;
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Database")]
        public string CommandString { get; set; } = string.Empty;
        private IMongoClient? Client { get; set; } = null;
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
            if (!(string.IsNullOrEmpty(DatabaseName)))
            {
                MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
            }
        }
        protected override void ProcessRecord()
        {
            if (MongoDatabase != null)
            {
                if (!(string.IsNullOrEmpty(CommandString)))
                {
                    BsonDocument Result = Operations.RunCommand(MongoDatabase, CommandString);
                    string JsonResult = JsonSerializer.Serialize(BsonTypeMapper.MapToDotNetValue(Result), new JsonSerializerOptions { WriteIndented = true });
                    WriteObject(JsonResult);
                }
            }
        }
    }
}
