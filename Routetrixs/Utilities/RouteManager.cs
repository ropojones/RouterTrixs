using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteTrixs.Utilities
{
    public class RouteManager
    {
        // Here I define a tuple to store the route information
        private List<(string, string, int)> routes;

        // Constructor to initialize the routes list
        public RouteManager()
        {
            routes = new List<(string, string, int)>();
        }
         public void SeedRoutes()
        {
            //AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
            routes = new List<(string, string, int)>(){

                new ("A", "B" , 5),
                new ("B", "C" , 4),
                new ("C", "D" , 8),
                new ("D", "C" , 8),
                new ("D", "E" , 6),
                new ("A", "D" , 5),
                new ("C", "E" , 2),
                new ("E", "B" , 3),
                new ("A", "E" , 7),
            };

        }

        // Method to add a route to the list
        public void AddRoute(string start, string end, int distance)
        {
            // Check that the start and end academies are not the same
            if (start == end)
            {
                Console.WriteLine($"Invalid route: {start}{end}{distance}. Start and end academies cannot be the same.");
                return;
            }

            // Check if the route already exists in the list
            foreach (var route in routes)
            {
                if (route.Item1 == start && route.Item2 == end)
                {
                    Console.WriteLine($"Route {start}{end}{distance} already exists.");
                    return;
                }
            }

            // Add the route as a tuple if it's valid and unique
            routes.Add((start, end, distance));

        }

        // Method to print all the routes
        public void DisplayRoutes()
        {
            Console.WriteLine("List of Routes:\n");
            foreach (var route in routes)
            {
                Console.WriteLine($"{route.Item1} -> {route.Item2}, Distance: {route.Item3}");
            }
        }

        // Method to parse and add a route from a string like "AB5"
        public void AddRouteFromString(string routeStr)
        {
            if (routeStr.Length < 3)
            {
                Console.WriteLine("Invalid route format.");
                return;
            }

            string start = routeStr[0].ToString();
            string end = routeStr[1].ToString();

            if (int.TryParse(routeStr.Substring(2), out int distance))
            {
                AddRoute(start, end, distance);
            }
            else
            {
                Console.WriteLine("Invalid distance in route.");
            }
        }
        public List<(string, string, int)> GetRoutes(){
            return routes;
        }

        public int CountRoutes()
        {
            return routes.Count;
        }


    }
}