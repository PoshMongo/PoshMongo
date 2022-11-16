using MongoDB.Driver;
using System.Management.Automation;
using System.Security.Authentication;

namespace PoshMongo.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "Instance")]
    [OutputType("MongoDB.Driver.MongoClient")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/docs/Connect-MongoDBInstance.md#connect-mongodbinstance", PositionalBinding = true)]
    public class ConnectInstance : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? ConnectionString { get; set; }
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "Default")]
        public SwitchParameter ForceTls12 { get; set; }
        protected override void BeginProcessing()
        {
            MongoClientSettings Settings = MongoClientSettings.FromConnectionString(ConnectionString);
            if (ForceTls12 == true)
            {
                Settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            }
            SessionState.PSVariable.Set("Client", new MongoClient(Settings));
            WriteObject(SessionState.PSVariable.Get("Client").Value);
        }
    }
}