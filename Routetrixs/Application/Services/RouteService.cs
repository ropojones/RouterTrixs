using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteTrixs.Domain.Entities;
using RouteTrixs.Domain.Services;

namespace RouteTrixs.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly Dictionary<(string, string), int> _routeMap = new Dictionary<(string, string), int>();

        public RouteService(IEnumerable<(string, string, int)> routes)
        {
            foreach (var route in routes)
            {
                _routeMap[(route.Item1, route.Item2)] = route.Item3;
            }
        }

        public string CalculateDistance(params string[] academies)
        {
            int totalDistance = 0;
            for (int i = 0; i < academies.Length - 1; i++)
            {
                var key = (academies[i], academies[i + 1]);
                if (_routeMap.ContainsKey(key))
                {
                    totalDistance += _routeMap[key];
                }
                else
                {
                    return "NO SUCH ROUTE";
                }
            }
            return totalDistance.ToString();
        }

        public int CountTripsWithMaxStops(string start, string end, int maxStops)
        {
            return CountTrips(start, end, 0, maxStops);
        }

        public int CountTripsWithExactStops(string start, string end, int exactStops)
        {
            return CountTrips(start, end, 0, exactStops, exactStops);
        }

        private int CountTrips(string current, string end, int stops, int maxStops, int? exactStops = null)
        {
            if (stops > maxStops) return 0;
            if (exactStops.HasValue && stops == exactStops && current == end) return 1;
            if (!exactStops.HasValue && stops <= maxStops && current == end && stops > 0) return 1;

            int count = 0;
            foreach (var route in _routeMap.Where(r => r.Key.Item1 == current))
            {
                count += CountTrips(route.Key.Item2, end, stops + 1, maxStops, exactStops);
            }

            return count;
        }

        public int FindShortestRoute(string start, string end)
        {
            return FindShortestRoute(start, end, new HashSet<string>(), 0);
        }

        private int FindShortestRoute(string current, string end, HashSet<string> visited, int currentDistance)
        {
            if (current == end && visited.Count > 0)
            {
                return currentDistance;
            }

            visited.Add(current);

            int shortest = int.MaxValue;
            foreach (var route in _routeMap.Where(r => r.Key.Item1 == current))
            {
                if (!visited.Contains(route.Key.Item2) || route.Key.Item2 == end)
                {
                    int distance = FindShortestRoute(route.Key.Item2, end, new HashSet<string>(visited), currentDistance + route.Value);
                    if (distance < shortest)
                    {
                        shortest = distance;
                    }
                }
            }

            return shortest;
        }

        public int CountRoutesWithMaxDistance(string start, string end, int maxDistance)
        {
            return CountRoutesWithMaxDistanceHelper(start, end, 0, maxDistance);
        }

        private int CountRoutesWithMaxDistanceHelper(string current, string end, int currentDistance, int maxDistance)
        {
            if (currentDistance >= maxDistance) return 0;

            int count = 0;
            if (current == end && currentDistance > 0)
            {
                count++;
            }

            foreach (var route in _routeMap.Where(r => r.Key.Item1 == current))
            {
                count += CountRoutesWithMaxDistanceHelper(route.Key.Item2, end, currentDistance + route.Value, maxDistance);
            }

            return count;
        }
    }
}