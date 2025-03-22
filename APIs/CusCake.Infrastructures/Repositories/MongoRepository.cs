using CusCake.Application;
using CusCake.Application.Repositories;
using CusCake.Application.Services.IServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CusCake.Infrastructures.Repositories;

class CsharpLegacyGuidSerializationProvider : IBsonSerializationProvider
{
    public IBsonSerializer GetSerializer(Type type)
    {
        if (type == typeof(Guid))
            return new GuidSerializer(GuidRepresentation.CSharpLegacy);

        return null!;
    }
}
public class MongoRepository : IMongoRepository
{
    private readonly IMongoDatabase _db;
    private readonly IClaimsService _claims;
    public MongoRepository(AppSettings appSettings, IClaimsService claims)
    {
        var client = new MongoClient(appSettings.ConnectionStrings.MongoDbConnection);
        BsonSerializer.RegisterSerializationProvider(new CsharpLegacyGuidSerializationProvider());

        _db = client.GetDatabase("CUS_CAKE");
        _claims = claims;
    }
    public async Task<bool> DeleteAsync<T>(string table, Guid id)
    {
        var collection = _db.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task<List<T>> GetAllAsync<T>(string table)
    {
        var collection = _db.GetCollection<T>(table);
        return await collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetByIdAsync<T>(string table, Guid id)
    {
        var collection = _db.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("_id", id);

        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task InsertAsync<T>(string table, T record) where T : class
    {
        var collection = _db.GetCollection<T>(table);
        await collection.InsertOneAsync(record);
    }

    [Obsolete]
    public async Task UpsertAsync<T>(string table, Guid id, T record) where T : class
    {
        var collection = _db.GetCollection<T>(table);

        var filter = Builders<T>.Filter.Eq("_id", id);

        await collection.ReplaceOneAsync(
            filter,
            record,
            new UpdateOptions { IsUpsert = true }
        );
    }
}
