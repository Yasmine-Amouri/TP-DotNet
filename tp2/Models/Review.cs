using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tp2.Models;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public int MovieId { get; set; }          
    public int Rating { get; set; }          
    public string? Comment { get; set; }
    public List<string> Tags { get; set; } = new();
    public bool? Spoiler { get; set; }       
    public string? Language { get; set; }    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}