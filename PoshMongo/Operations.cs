﻿using MongoDB.Driver;
using System.Management.Automation;
using System.Security.Authentication;
using MongoDB.Bson;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Text.Json;

namespace PoshMongo
{
    internal class Operations
    {
        /// <summary>
        /// Create
        /// </summary>

        /// <summary>
        /// This method adds a Document to a MongoCollection
        /// </summary>
        /// <param name="Collection">The collection to add the document to</param>
        /// <param name="document">A json string document</param>
        /// <param name="isUpsert">Boolean to indicate if upsert should be performed</param>
        /// <returns></returns>
        public static string AddDocument(IMongoCollection<BsonDocument> Collection, string document, bool isUpsert)
        {
            BsonDocument bsonDocument = BsonDocument.Parse(document);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument["_id"]);

            if (isUpsert)
            {
                ReplaceOptions options = new ReplaceOptions { IsUpsert = true };
                Collection.ReplaceOne(filter, bsonDocument, options);
            }
            else
            {
                Collection.InsertOne(bsonDocument);
            }

            return Collection.Find(filter).FirstOrDefault().ToJson();
        }        /// <summary>
                 /// Create a Collection in a MongoDatabase
                 /// </summary>
                 /// <param name="collectionName">The name of the Collection to create</param>
                 /// <param name="mongoDatabase">The MongoDatabase to create the Collection in</param>
                 /// <returns></returns>
        public static IMongoCollection<BsonDocument> NewCollection(string collectionName, IMongoDatabase mongoDatabase)
        {
            mongoDatabase.CreateCollection(collectionName, new CreateCollectionOptions(), new CancellationToken());
            IMongoCollection<BsonDocument> Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            return Collection;
        }
        /// <summary>
        /// Create a MongoDatabse
        /// </summary>
        /// <param name="Client">A Connected MongoClient</param>
        /// <param name="DatabaseName">The name of the Database to create</param>
        /// <returns></returns>
        public static IMongoDatabase NewDatabase(IMongoClient Client, string DatabaseName)
        {
            return Client.GetDatabase(DatabaseName);
        }
        /// <summary>
        ///  Read
        /// </summary>

        /// <summary>
        /// Return a Collection from a MongoDatabase
        /// </summary>
        /// <param name="mongoDatabase">The MongoDatabase to a get Collection from</param>
        /// <param name="collectionName">The Collection to get</param>
        /// <returns></returns>
        public static IMongoCollection<BsonDocument> GetCollection(IMongoDatabase mongoDatabase, string collectionName)
        {
            IMongoCollection<BsonDocument> Collection = mongoDatabase.GetCollection<BsonDocument>(collectionName, new MongoCollectionSettings());
            return Collection;
        }
        /// <summary>
        /// Return a MongoDatabase from a MongoClient
        /// </summary>
        /// <param name="Client">A Connected MongoClient</param>
        /// <param name="DatabaseName">The name of the Database to return</param>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase(IMongoClient Client, string DatabaseName)
        {
            return Client.GetDatabase(DatabaseName);
        }
        /// <summary>
        /// Return a Document from a Collection by Id
        /// </summary>
        /// <param name="Collection">The Collection to get the Document from</param>
        /// <param name="Id">The ID of the Document to get</param>
        /// <param name="noId">Boolean to display ID field</param>
        /// <returns></returns>
        public static string GetDocument(IMongoCollection<BsonDocument> Collection, string Id, bool noId)
        {
            BsonDocument Result;
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", Id);
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                Result = Collection.Find(id).Project(projection).FirstOrDefault();
            }
            else
            {
                Result = Collection.Find(id).FirstOrDefault();
            }
            string JsonResult = JsonSerializer.Serialize(BsonTypeMapper.MapToDotNetValue(Result), new JsonSerializerOptions { WriteIndented = true });
            return JsonResult;
        }
        /// <summary>
        /// Return a Document from a Collection by Object ID
        /// </summary>
        /// <param name="Collection">The Collection to get the Document from</param>
        /// <param name="Id">The ObjectID of the Document to get</param>
        /// <param name="noId">Boolean to display ID field</param>
        /// <returns></returns>
        public static string GetDocument(IMongoCollection<BsonDocument> Collection, ObjectId Id, bool noId)
        {
            BsonDocument Result;
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(Id.ToString()));
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                Result = Collection.Find(id).Project(projection).FirstOrDefault();
            }
            else
            {
                Result = Collection.Find(id).FirstOrDefault();
            }
            string JsonResult = JsonSerializer.Serialize(BsonTypeMapper.MapToDotNetValue(Result), new JsonSerializerOptions { WriteIndented = true });
            return JsonResult;
        }
        /// <summary>
        /// Return a Document from a Collection using a Filter
        /// </summary>
        /// <param name="Collection">The Collection to get the Document from</param>
        /// <param name="filter">A filter for Documents</param>
        /// <param name="noId">Boolean to display ID field</param>
        /// <returns></returns>
        public static string GetDocument(IMongoCollection<BsonDocument> Collection, FilterDefinition<BsonDocument> filter, bool noId)
        {
            BsonDocument Result;
            if (noId == true)
            {
                ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Exclude("_id");
                Result = Collection.Find(filter).Project(projection).FirstOrDefault();
            }
            else
            {
                Result = Collection.Find(filter).FirstOrDefault();
            }
            string JsonResult = JsonSerializer.Serialize(BsonTypeMapper.MapToDotNetValue(Result), new JsonSerializerOptions { WriteIndented = true });
            return JsonResult;
        }
        /// <summary>
        /// Delete
        /// </summary>

        /// <summary>
        /// Remove a Collection from a MongoDatabase
        /// </summary>
        /// <param name="mongoDatabase">The MongoDatabase to remove the Collection from</param>
        /// <param name="collectionName">The name of the Collection to remove</param>
        public static void RemoveCollection(IMongoDatabase mongoDatabase, string collectionName)
        {
            mongoDatabase.DropCollection(collectionName);
        }
        /// <summary>
        /// Remove a MongoDatase
        /// </summary>
        /// <param name="Client">A Connected MongoClient</param>
        /// <param name="DatabaseName">The name of the MongoDatabase to remove</param>
        public static void RemoveDatabase(IMongoClient Client, string DatabaseName)
        {
            Client.DropDatabase(DatabaseName);
        }
        /// <summary>
        /// Remove a Document from a Collection
        /// </summary>
        /// <param name="Collection">The Collection to remove a Document from</param>
        /// <param name="documentID">The ID of the Document to remove</param>
        public static void RemoveDocument(IMongoCollection<BsonDocument> Collection, string documentID)
        {
            FilterDefinition<BsonDocument> id = Builders<BsonDocument>.Filter.Eq("_id", documentID);
            Collection.DeleteOne(id);
        }
        /// <summary>
        /// REmove a Document from a Collection
        /// </summary>
        /// <param name="Collection">The Collection to remove a Document from</param>
        /// <param name="filter">A filtered Document to remove</param>
        public static void RemoveDocument(IMongoCollection<BsonDocument> Collection, Hashtable filter)
        {
            List<FilterDefinition<BsonDocument>> filters = new List<FilterDefinition<BsonDocument>>();
            foreach (string key in filter.Keys)
            {
                filters.Add(Builders<BsonDocument>.Filter.Eq(key, filter[key]));
            }
            FilterDefinition<BsonDocument> result = Builders<BsonDocument>.Filter.And(filters);
            Collection.DeleteOne(result);
        }
        /// <summary>
        /// Run a command against a Database
        /// </summary>
        /// <param name="mongoDatabase">The Database to run the command against</param>
        /// <param name="CommandString">The Command</param>
        /// <returns></returns>
        public static BsonDocument RunCommand(IMongoDatabase mongoDatabase, string CommandString)
        {
            if (!(string.IsNullOrEmpty(CommandString)))
            {
                JsonCommand<BsonDocument> RunCommand = new JsonCommand<BsonDocument>(CommandString);
                return mongoDatabase.RunCommand(RunCommand);
            }
            return new BsonDocument();
        }
    }
}