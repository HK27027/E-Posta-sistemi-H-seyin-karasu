using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EMailSending.Data
{
    public class Emails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? ServerName { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? PortNumber { get; set; }
        public string? AccountId { get; set; }
        public bool? IsDeleted { get; set; } = false;

    }
}
