using MongoDB.Driver;
using System.Management.Automation;
using System.Security.Authentication;
using MongoDB.Bson;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

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
        public static List<string> GetDocument(IMongoCollection<BsonDocument> Collection, bool noId)
        {
            List<string> Documents = new();
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                foreach (BsonDocument doc in Collection.Find(new BsonDocument()).Project(projection).ToList())
                {
                    Documents.Add(doc.ToJson());
                }
            }
            else
            {
                foreach (BsonDocument doc in Collection.Find(new BsonDocument()).ToList())
                {
                    Documents.Add(doc.ToJson());
                }
            }
            return Documents;
        }
        public static string GetDocument(IMongoCollection<BsonDocument> Collection, string Id, bool noId)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", Id);
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(id).Project(projection).FirstOrDefault().ToJson();
            }
            else
            {
                return Collection.Find(id).FirstOrDefault().ToJson();
            }
        }
        public static string GetDocument(IMongoCollection<BsonDocument> Collection, ObjectId Id, bool noId)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(Id.ToString()));
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(id).Project(projection).FirstOrDefault().ToJson();
            }
            else
            {
                return Collection.Find(id).FirstOrDefault().ToJson();
            }
        }
        public static string GetDocument(IMongoCollection<BsonDocument> Collection, Hashtable filter, bool noId)
        {
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
            foreach (string key in filter.Keys)
            {
                filters.Add(Builders<BsonDocument>.Filter.Eq(key, filter[key]));
            }
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.And(filters);
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                return Collection.Find(id).Project(projection).FirstOrDefault().ToJson();
            }
            else
            {
                return Collection.Find(id).FirstOrDefault().ToJson();
            }
        }
        public static IMongoCollection<BsonDocument> NewCollection(string collectionName, IMongoDatabase mongoDatabase)
        {
            mongoDatabase.CreateCollection(collectionName, new CreateCollectionOptions(), new CancellationToken());
            IMongoCollection<BsonDocument> Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            return Collection;
        }
        public static IMongoDatabase NewDatabase(MongoClient Client, string DatabaseName)
        {
            return Client.GetDatabase(DatabaseName);
        }
    }
}