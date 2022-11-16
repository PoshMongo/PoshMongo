using MongoDB.Driver;
using System.Management.Automation;

namespace PoshMongo
{
    [Cmdlet(VerbsCommon.Get, "Database")]
    public class GetDatabase : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "Default")]
        public string? DatabaseName { get; set; }
        protected override void BeginProcessing()
        {
            MongoClient Client = (MongoClient)SessionState.PSVariable.Get("Client").Value;
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