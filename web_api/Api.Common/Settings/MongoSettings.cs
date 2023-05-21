using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common.Settings
{
    public class MongoSettings
    {
        public string server { get; set; }
        public int port { get; set; }

        public string connectionstring => $"mongodb://{server}:{port}";
    }
}
