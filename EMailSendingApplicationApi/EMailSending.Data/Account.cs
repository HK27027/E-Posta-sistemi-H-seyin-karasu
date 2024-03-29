﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EMailSending.Data
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
