using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteTrixs.Domain.Entities
{
    public class AcademyRoute
    {
        public string StartAcademy { get; private set; }
        public string EndAcademy { get; private set; }
        public int Distance { get; private set; }

        public AcademyRoute(string start, string end, int distance)
        {
            StartAcademy = start;
            EndAcademy = end;
            Distance = distance;
        }
    }
}