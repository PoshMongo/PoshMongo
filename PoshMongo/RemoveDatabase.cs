using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo
{
    [Cmdlet(VerbsCommon.Remove, "Database")]
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