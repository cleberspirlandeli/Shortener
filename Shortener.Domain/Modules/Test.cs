using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Domain.Modules
{
    public class Test : Entity
    {
        public string MainDestinationUrl { get; set; }
        public string KeyUrl { get;  set; }
        public int DayCounter { get;  set; }
        public int WeekCounter { get;  set; }
        public int AmountCounter { get;  set; }
        public int YearCounter { get;  set; }
    }
}
