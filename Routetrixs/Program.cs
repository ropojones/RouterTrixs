/*
    Application: Routetrix 
    Current Version: Version 1.0.0
    Developed Date:  22/10/2024
    Developer By: Oluropo Akinbolade-Jones
    Email: ropojones@gmail.com
    Github: https://github.com/ropojones

*/
using RouteTrixs.Application.Services;
using RouteTrixs.Domain.Entities;
using RouteTrixs.Infrastructure.Data;
using RouteTrixs.Utilities;
using System;
using System.Net;

namespace RouteTrixs
{
    class Program
    {
        static void Main(string[] args)
        {

            string option;
            string cont;

            RouteService routeService = null;
            RouteManager routeManager = new RouteManager();

            var routes = new List<(string, string, int)>();

            while (true)
            {

                Console.Clear();
                Console.WriteLine(
                                    "*************************\n" +
                                    "Welcome to RouteTrix v1.0\n" +
                                    "*************************\n"
                                );
                Console.Beep();

                Console.WriteLine("Please select an option:\n\n" +
                                "1. Seed Sample Routes (AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7)\n" +
                                "2. Manage Routes (Add, Update or Delete)\n" +
                                "3. Display Routes\n" +
                                "4. Compute Route Distances\n" +
                                "5. Compute Number of Trips in Academy Routes with N-Max Stops\n" +
                                "6. Compute Number of Trips in Academy Routes with N-Exact Stops\n" +
                                "7. Find Routes with Distance Less than N\n" +
                                "8. Exit RouteTrix\n");

                int opt;
                Console.Write("Option: ");
                while (true)
                {
                    option = Console.ReadLine();
                    if (!Int32.TryParse(option, out opt))
                    {
                        Console.WriteLine("\nInvalid menu selecction entry. Enter an option from 1-7.");
                        Console.Write("\nPress enter to continue...\n");
                        Console.ReadLine();
                    }
                    break;
                }

                switch (opt)
                {
                    case 1:
                        //Seed sample route data
                        {
                            Console.Beep();
                            Console.WriteLine("Seeding sample data.....");
                            routeManager.SeedRoutes();
                            routeService = new RouteService(routeManager.GetRoutes());
                            Console.WriteLine("Adding routes: AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
                            Console.WriteLine("Routes added to Routrixs datastore.");
                            Console.Write("\nPress enter to continue...\n");
                            Console.ReadLine();
                        }
                        break;
                    case 2:
                        /*       Add routes to RouteTrix datastore. This data is a non-persistent,
                                 and theprogram will require you to recreate routes when the application 
                                 is being run.
                             */
                        {
                            string routeString = String.Empty;
                            Console.WriteLine();
                            Console.Clear();
                            Console.WriteLine("RouteTrix v1.0 - Route Manager (Add Routes)\n" +
                                             "-------------------------------------------\n" +
                                             "Please enter routes (e.g AB3) or enter done to go back to menu.\n" +
                                             "AB3 means A to B with a distance of 3. Leave blank \nand press enter to continue.\n");
                            while (true)
                            {
                                Console.Write("Enter Route: ");
                                routeString = Console.ReadLine().ToUpper();
                                if (routeString.ToLower() != "done")
                                {
                                    routeManager.AddRouteFromString(routeString);
                                }
                                else
                                {
                                    break;
                                }
                                routeService = new RouteService(routeManager.GetRoutes());
                            }
                        }
                        break;
                    case 3:
                        // Display Routes
                        {
                            //Check if there are existing routes
                            if (checkRoutes(routeManager.CountRoutes()))
                            {
                                Console.Clear();
                                Console.WriteLine("RouteTrix v1.0 - Route Manager (Display Routes)");
                                Console.WriteLine("----------------------------------------------\n");
                                routeManager.DisplayRoutes();
                                Console.Write("\nPress enter to continue...");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                Console.Beep();
                                Console.Write("\nPress enter to continue...");
                                Console.ReadLine();
                                break;
                            }
                        }
                    case 4:
                        //Compute Route Distances
                        {
                            //Check if there are existing routes
                            if (checkRoutes(routeManager.CountRoutes()))
                            {
                                Console.Clear();
                                Console.WriteLine("RouteTrix v1.0 - Route Manager (Compute Route Distances)");
                                Console.WriteLine("-------------------------------------------------------\n");
                                Console.WriteLine("Enter route path (e.g., A-B-C) or enter done to go back to menu .\n");
                                while (true)
                                {
                                    
                                    Console.Write("Enter Route Path: ");
                                    string routeString = Console.ReadLine().ToUpper();
                                    if (routeString.ToLower() != "done")
                                    {
                                        var routePath = routeString?.Split('-');
                                        if (routePath != null && routePath.Length > 1)
                                        {
                                            try
                                            {
                                                var distance = routeService.CalculateDistance(routePath);
                                                Console.WriteLine($"Distance: {distance}\n");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                                Console.ReadLine();
                                            }
                                        }
                                        else if (routePath.Length == 1)
                                        {
                                            Console.WriteLine("Invalid route path entry. Enter a valid route with '-' (e.g. A-B)");
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                Console.WriteLine();
                                Console.Write("\nPress enter to continue...");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                Console.Beep();
                                Console.Write("\nPress enter to continue...");
                                Console.ReadLine();
                                break;
                            }
                        }
                    case 5:
                        //Compute Number of Trips in Academy Routes with N-Max Stops
                        {
                            while (true)
                            {
                                //Check if there are existing routes
                                if (checkRoutes(routeManager.CountRoutes()))
                                {
                                    Console.Clear();
                                    Console.WriteLine("RouteTrix v1.0 - Route Manager (Compute Number of Trips in Routes with N-Max Stops)");
                                    Console.WriteLine("------------------------------------------------------------------------------------------\n");

                                    try
                                    {
                                        string start, end;
                                        int stops;
                                        Console.WriteLine("Enter start, end, and max stops:");

                                        Console.Write("Enter Starting Point: ");
                                        start = Console.ReadLine();
                                        Console.Write("Enter Ending Point: ");
                                        end = Console.ReadLine();
                                        Console.Write("Enter Max. Stops: ");
                                        bool stp = int.TryParse(Console.ReadLine(), out stops);

                                        var count = routeService.CountTripsWithMaxStops(start, end, stops);
                                        Console.WriteLine($"Routes with max {stops} stops: {count}");


                                    }
                                    catch (Exception Ex)
                                    {
                                        Console.WriteLine("There was an error in your entries. Please enter valid inputs.\n");
                                    }


                                    Console.WriteLine("Do you want to continue Yes or No (Y/N)");
                                    string contTask = Console.ReadLine();

                                    if (contTask.ToLower() == "no" || contTask.ToLower() == "n")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.WriteLine("Press enter to continue...");
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.Write("Press enter to continue...");
                                    Console.ReadLine();
                                    break;
                                }
                            };
                        }
                        break;
                    case 6:
                        //Compute Number of Trips in Academy Routes with N-Exact Stops
                        {
                            while (true)
                            {
                                //Check if there are existing routes
                                if (checkRoutes(routeManager.CountRoutes()))
                                {
                                    Console.Clear();
                                    Console.WriteLine("RouteTrix v1.0 - Route Manager (Compute Number of Trips in Routes with N-Exact Stops)");
                                    Console.WriteLine("------------------------------------------------------------------------------------------\n");

                                    try
                                    {
                                        string start, end;
                                        int stops;
                                        Console.WriteLine("Enter start, end, and exact stops:");
                                        Console.Write("Enter Starting Point: ");
                                        start = Console.ReadLine();
                                        Console.Write("Enter Ending Point: ");
                                        end = Console.ReadLine();
                                        Console.Write("Enter Exact. Stops: ");
                                        stops = int.Parse(Console.ReadLine());
                                        var count = routeService.CountTripsWithExactStops(start, end, stops);
                                        Console.WriteLine($"Routes with exact {stops} stops: {count}");

                                    }
                                    catch (Exception Ex)
                                    {
                                        Console.WriteLine("There was an error in your entries. Please enter valid inputs.\n");
                                    }

                                    Console.WriteLine("Do you want to continue Yes or No (Y/N)");
                                    string contTask = Console.ReadLine();

                                    if (contTask.ToLower() == "no" || contTask.ToLower() == "n")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.WriteLine("Press enter to continue...");
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.Write("Press enter to continue...");
                                    Console.ReadLine();
                                    break;
                                }
                            };
                        }
                        break;

                    case 7:
                        {
                            //The length of the shortest route 
                            while (true)
                            {
                                //Check if there are existing routes
                                if (checkRoutes(routeManager.CountRoutes()))
                                {

                                    Console.Clear();
                                    Console.WriteLine("RouteTrix v1.0 - Route Manager (Compute the Length of Shortest Route)");
                                    Console.WriteLine("---------------------------------------------------------------------------------\n");
                                    try
                                    {
                                        string firstLocation, secondLocation;
                                        int stops;
                                        Console.WriteLine("Enter locations:");
                                        Console.Write("Enter Location I: ");
                                        firstLocation = Console.ReadLine();
                                        Console.Write("Enter Location II: ");
                                        secondLocation = Console.ReadLine();

                                        var count = routeService.FindShortestRoute(firstLocation, secondLocation);
                                        Console.WriteLine($"Length of Shortest Route from {firstLocation} to {secondLocation} is {count}");

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("There was an error in your entries. Please enter valid inputs.\n");
                                    }
                                    Console.WriteLine("Do you want to continue Yes or No (Y/N)");
                                    string contTask = Console.ReadLine();

                                    if (contTask.ToLower() == "no" || contTask.ToLower() == "n")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.WriteLine("Press enter to continue...");
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.Write("Press enter to continue...");
                                    Console.ReadLine();
                                    break;
                                }
                            }
                        }
                        break;
                    case 8:
                        {
                            //number of different routes from one to another with a distance of less than N.
                            while (true)
                            {
                                //Check if there are existing routes
                                if (checkRoutes(routeManager.CountRoutes()))
                                {
                                    Console.Clear();
                                    Console.WriteLine("RouteTrix v1.0 - Route Manager (Routes with Distance Less Than N)");
                                    Console.WriteLine("---------------------------------------------------------------------------------\n");
                                    try
                                    {
                                        string firstLocation, secondLocation;
                                        int maxDistance;
                                        Console.WriteLine("Enter locations:");
                                        Console.Write("Enter Location I: ");
                                        firstLocation = Console.ReadLine();
                                        Console.Write("Enter Location II: ");
                                        secondLocation = Console.ReadLine();
                                        Console.Write("Enter the Maximum Distance: ");
                                        maxDistance = int.Parse(Console.ReadLine());
                                        var count = routeService.CountRoutesWithMaxDistance(firstLocation, secondLocation, maxDistance);
                                        Console.WriteLine($"Route from {firstLocation} to {secondLocation} with distance less than {maxDistance} is {count}");

                                    }
                                    catch (Exception Ex)
                                    {
                                        Console.WriteLine("There was an error in your entries. Please enter valid inputs.\n");
                                    }

                                    Console.WriteLine("Do you want to continue Yes or No (Y/N)");
                                    string contTask = Console.ReadLine();

                                    if (contTask.ToLower() == "no" || contTask.ToLower() == "n")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.WriteLine("Press enter to continue...");
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.Write("Press enter to continue...");
                                    Console.ReadLine();
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            }

        }

        /*
            This method checks if there are routes created. To be able to run and tests this program, 
            the user create routes in datastore. A production level implementation of the routeService will
            store data to a database.  
        */
        static bool checkRoutes(int counter)
        {
            if (counter > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Routrixs route datastore is empty. Select option 1, to add routes to datastore.");
                return false;
            }
        }
    }
}
