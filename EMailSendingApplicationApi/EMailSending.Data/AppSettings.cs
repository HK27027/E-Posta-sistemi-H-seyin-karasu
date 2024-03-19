using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailSending.Data
{
    public class AppSettings
    {
        public Mongo Mongo { get; set; } = new Mongo();
      

    }
    public class Mongo
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
 
}
