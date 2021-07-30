using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.API.Configuration
{
    public class Appsettings
    {
        public QueueSettings QueueSettings { get; set; }

        public Appsettings() { }

        public Appsettings(QueueSettings queueSettings)
        {
            QueueSettings = queueSettings;
        }
    }
}
