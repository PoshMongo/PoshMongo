using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo.Database
{
    [Cmdlet(VerbsCommon.Remove, "Database")]
    [OutputType("MongoDB.Driver.MongoDatabaseBase")]
    [CmdletBinding(HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/Remove-MongoDBDatabase0.md#remove-mongodbdatabase", PositionalBinding = true)]
    public class RemoveDatabase : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        public string? DatabaseName { get; set; }
        protected override void BeginProcessing()
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
            Client.DropDatabase(DatabaseName);
            foreach (string db in Client.ListDatabaseNames().ToEnumerable())
            {
                WriteObject(Client.GetDatabase(db));
            }
        }
    }
}