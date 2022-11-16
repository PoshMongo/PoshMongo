using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Database
{
    [Cmdlet(VerbsCommon.New, "Database")]
    [OutputType("MongoDB.Driver.MongoDatabaseBase")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/New-MongoDBDatabase0.md#new-mongodbdatabase", PositionalBinding = true)]
    public class NewDatabase : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? DatabaseName { get; set; }
        protected override void BeginProcessing()
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            SessionState.PSVariable.Set("Database", Client.GetDatabase(DatabaseName));
            WriteObject(SessionState.PSVariable.Get("Database").Value);
        }
    }
}