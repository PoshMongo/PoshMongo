using MongoDB.Bson;
using MongoDB.Driver;
using System.Management.Automation;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace PoshMongo.Collection
{
    [Cmdlet(VerbsCommon.New, "Collection", HelpUri = "https://github.com/PoshMongo/PoshMongo/blob/master/Docs/New-MongoDBCollection.md#new-mongodbcollection")]
    [OutputType("MongoDB.Driver.IMongoCollection")]
    [CmdletBinding(PositionalBinding = true)]
    public class NewCollectionCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "DatabaseName")]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Database")]
        public string CollectionName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatabaseName")]
        public string DatabaseName { get; set; } = string.Empty;
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Database", ValueFromPipeline = true)]
        public IMongoDatabase? MongoDatabase { get; set; }
        private IMongoClient? Client { get; set; } = null;
        protected override void ProcessRecord()
        {
            Client = (IMongoClient)SessionState.PSVariable.Get("Client").Value;
            WriteVerbose("ParameterSetName: " + ParameterSetName);
            switch (ParameterSetName)
            {
                case "DatabaseName":
                    if (!(string.IsNullOrEmpty(CollectionName)))
                    {
                        MongoDatabase = Operations.GetDatabase(Client, DatabaseName);
                        WriteObject(Operations.NewCollection(CollectionName, MongoDatabase));
                    }
                    break;
                case "Database":
                    if (!(string.IsNullOrEmpty(CollectionName)) && (MongoDatabase != null))
                    {
                        WriteObject(Operations.NewCollection(CollectionName, MongoDatabase));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}