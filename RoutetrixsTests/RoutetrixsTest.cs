 
using Microsoft.Extensions.DependencyInjection;
using RouteTrixs.Application.Services;
using RouteTrixs.Domain.Services;
using System.Collections.Generic;

namespace RouteTrixsTests
{
    public class RouteServiceTests
    {
        private IRouteService _routeService;

        [SetUp]
        public void Setup()
        {
             // Set up dependency injection for testing
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRouteService, RouteService>()
                .BuildServiceProvider();

            // Sample routes
            var routes = new List<(string, string, int)>
            {
                ("A", "B", 5),
                ("B", "C", 4),
                ("C", "D", 8),
                ("D", "C", 8),
                ("D", "E", 6),
                ("A", "D", 5),
                ("C", "E", 2),
                ("E", "B", 3),
                ("A", "E", 7)
            };

            _routeService = new RouteService(routes);
        }

        [Test]
        public void Test_DistanceOfRoute_A()
        {
            // Assign 
            var distance = 9;

            // Act
            var result = int.Parse(_routeService.CalculateDistance("A", "B", "C"));

            // Assert
            Assert.AreEqual(distance, result);
        }

        [Test]
        public void Test_DistanceOfRoute_A_E_B_C_D()
        {
            var result = _routeService.CalculateDistance("A", "E", "B", "C", "D");
            Assert.AreEqual("22", result);
        }

        [Test]
        public void Test_DistanceOfRoute_A_E_D_ShouldReturnNoSuchRoute()
        {
            var result = _routeService.CalculateDistance("A", "E", "D");
            Assert.AreEqual("NO SUCH ROUTE", result);
        }

        [Test]
        public void Test_TripsFromCToC_Max3Stops()
        {
            var result = _routeService.CountTripsWithMaxStops("C", "C", 3);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test_TripsFromAToC_Exact4Stops()
        {
            var result = _routeService.CountTripsWithExactStops("A", "C", 4);
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test_ShortestRouteFromAToC()
        {
            var result = _routeService.FindShortestRoute("A", "C");
            Assert.AreEqual(9, result);
        }

        [Test]
        public void Test_ShortestRouteFromBToB()
        {
            var result = _routeService.FindShortestRoute("B", "B");
            Assert.AreEqual(9, result);
        }

        [Test]
        public void Test_RoutesFromCToC_LessThan30()
        {
            var result = _routeService.CountRoutesWithMaxDistance("C", "C", 30);
            Assert.AreEqual(7, result);
        }
    }
}