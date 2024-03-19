using EMailSending.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailSending.Data
{
    public class MongoContext : DbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IOptions<AppSettings> options)
        {
            var client = new MongoClient(options.Value.Mongo.ConnectionString);
            if (client != null) _database = client.GetDatabase(options.Value.Mongo.Database);
        }
        public IMongoCollection<Emails> Emails
        {
            get { return _database.GetCollection<Emails>("Emails"); }
        }
        public IMongoCollection<EmailSent> EmailSent
        {
            get { return _database.GetCollection<EmailSent>("EmailSent"); }
        }
        public IMongoCollection<Users> Users
        {
            get { return _database.GetCollection<Users>("Users"); }
        }
        public IMongoCollection<Account> Account
        {
            get { return _database.GetCollection<Account>("Account"); }
        }

    }
}
