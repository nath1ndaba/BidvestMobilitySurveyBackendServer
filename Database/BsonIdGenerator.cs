using BidvestMobilitySurveyBackendServer.Services;
using MongoDB.Bson;

namespace BidvestMobilitySurveyBackendServer.Database;

public class BsonIdGenerator : IIDGenerator
{
    public string GenerateNewId()
        => ObjectId.GenerateNewId().ToString();
}
