using MongoDB.Driver;
using System.Management.Automation;
using System.Security.Authentication;

namespace PoshMongo.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "Instance", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Connect-MongoDBInstance.md#connect-mongodbinstance")]
    [OutputType("MongoDB.Driver.IMongoClient")]
    [CmdletBinding(PositionalBinding = true)]
    public class ConnectInstance : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string ConnectionString { get; set; } = string.Empty;
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")]
        public SwitchParameter ForceTls12 { get; set; }
        protected override void BeginProcessing()
        {
            MongoClientSettings Settings = MongoClientSettings.FromConnectionString(ConnectionString);
            if (ForceTls12 == true)
            {
                Settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            }
            IMongoClient Client = new MongoClient(Settings);
            SessionState.PSVariable.Set("Client", Client);
            WriteObject(SessionState.PSVariable.Get("Client").Value);
        }
    }
}