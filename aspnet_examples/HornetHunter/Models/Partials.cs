using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HornetHunter.Models
{
    public partial class Sighting
    {
        public override string ToString()
        {
            return base.ToString() + $"{Id},{FullName},{Phone},{Latitude},{Longitude},{SightingTime},{Description},{ReportTime}";
        }
    }
}
