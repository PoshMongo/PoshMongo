using MongoDB.Driver;
using System.Management.Automation;
using System.Security.Authentication;
using MongoDB.Bson;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Collections.Generic;

namespace PoshMongo
{
    internal class Operations
    {
        public static string AddDocument(IMongoCollection<BsonDocument> Collection, string document)
        {
            BsonDocument bsonDocument = BsonDocument.Parse(document);
            Collection.InsertOne(bsonDocument);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument["_id"]);
            return Collection.Find(filter).FirstOrDefault().ToJson();
        }
        public static List<IMongoCollection<BsonDocument>> GetCollection(MongoDatabaseBase mongoDatabase)
        {
            List<IMongoCollection<BsonDocument>> Collections = new();
            foreach (string collectionName in mongoDatabase.ListCollectionNames().ToEnumerable())
            {
                Collections.Add(mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings()));
            }
            return Collections;
        }
        public static IMongoCollection<BsonDocument> GetCollection(MongoDatabaseBase mongoDatabase, string collectionName)
        {
            IMongoCollection<BsonDocument> Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            return Collection;
        }
        public static IMongoDatabase GetDatabase(MongoClient Client, string DatabaseName)
        {
            return Client.GetDatabase(DatabaseName);
        }
        public static List<IMongoDatabase> GetDatabase(MongoClient Client)
        {
            List<IMongoDatabase> mongoDatabases = new();
            foreach (string db in Client.ListDatabaseNames().ToEnumerable())
            {
                mongoDatabases.Add(Client.GetDatabase(db));
            }
            return mongoDatabases;
        }
    }
}