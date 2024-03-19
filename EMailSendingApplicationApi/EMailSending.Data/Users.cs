using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EMailSending.Data
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Title { get; set; }
        public string? CompanyName { get; set; }
        public string? AccountId { get; set; }
        public bool? IsDeleted { get; set; } = false;
   

    }
}
