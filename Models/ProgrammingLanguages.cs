using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace BidvestMobilitySurveyBackendServer.Models;

[BsonIgnoreExtraElements]
public class ProgrammingLanguages
{
    [BsonId]
    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string LanguageShortName { get; set; } = null!;
    public string LanguageFullName { get; set; } = null!;
    public string LanguageShortDescription { get; set; } = null!;
}
