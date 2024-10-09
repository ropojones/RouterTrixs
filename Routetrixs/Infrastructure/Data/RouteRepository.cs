using RouteTrixs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteTrixs.Infrastructure.Data
{
    public class RouteRepository
    {
        public static List<AcademyRoute> LoadRoutes(string input)
        {
            var routes = new List<AcademyRoute>();
            var routeStrings = input.Split(',');

            foreach (var routeString in routeStrings)
            {
                string start = routeString[0].ToString();
                string end = routeString[1].ToString();
                int distance = int.Parse(routeString.Substring(2));

                routes.Add(new AcademyRoute(start, end, distance));
            }

            return routes;
        }
    }
}