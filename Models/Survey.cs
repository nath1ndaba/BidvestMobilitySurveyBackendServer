using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace BidvestMobilitySurveyBackendServer.Models;

[BsonIgnoreExtraElements]
public class Survey
{
    [BsonId]
    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonRequired]
    public string? Name { get; set; } = null!;
    [BsonRequired]
    public string? Surname { get; set; } = null!;
    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string LanguageId { get; set; } = null!;
}
