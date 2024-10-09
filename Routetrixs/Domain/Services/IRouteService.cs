using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteTrixs.Domain.Services
{
    public interface IRouteService
    {
        string CalculateDistance(params string[] academies);
        int CountTripsWithMaxStops(string start, string end, int maxStops);
        int CountTripsWithExactStops(string start, string end, int exactStops);
        int FindShortestRoute(string start, string end);
        int CountRoutesWithMaxDistance(string start, string end, int maxDistance);
    }
}