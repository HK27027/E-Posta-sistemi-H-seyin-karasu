using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EMailSending.Data
{
    public class EmailSent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? EMailId { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public string? AccountId { get; set; }

    }
}
