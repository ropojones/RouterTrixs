# RouteTrixs
RouteTrixs is an operational support tool that helps your organization provision a route optimization service, to help your team deliver support to your focal points effectively. It was created to proffer a solution to the New Globe Teacher Computer Retrieval technical challenge assignment. The name given to the solution "RouteTrixs", attempts to adopt conventional naming techniques for Projects and a reflection of the absractions from the problem domain. "Route" is adopted to characterise the problem domain, that mainly focuses of computing the distances between routes, number of different routes and shortest routes."Trixs", which is a techie name for tricks, is adopted to capture the interesting and interactive user focused features of the solution. This solution can be refactored for production use.

# Requirements: 
.NET Core 8.0.1

NUnit Framework

NUnit3 Test Adapter 

# Get Started with RouteTrix
Install all the required frameworks. Extract the program folder from thhe shared zip file or clone the project from github repositiory. The zip file shared comprises of a NewGlobe folder,  which contains **NewGlobe.sln** file, and 2 projects folders, **RouteTrixs** and **RouteTrixsTest**.

# How to run RouteTrix
Open the project folder from your IDE Visual StudioCode or open the NewGlobe.sln solution file in Visual Studio.

## Run RouteTrixs Project
### To run the RouteTrixs program in Visual Studio Code, 
- Navigate to project folder and containing RouteTrixs.csproj. 
- Open the folder in a new terminal and type 
> C:\Development\NewGlobe\RouteTrixs> dotnet run

```
*************************
Welcome to RouteTrix v1.0
*************************

Please select an option:

1. Seed Sample Routes (AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7)
2. Manage Routes (Add, Update or Delete)
3. Display Routes
4. Compute Route Distances
5. Compute Number of Trips in Academy Routes with N-Max Stops
6. Compute Number of Trips in Academy Routes with N-Exact Stops
7. Length/Distance of Shortest Route between to points
8. Find Routes with Distance Less than N
9. Exit RouteTrix

```
- Select an option from 1 - 9. To run the program you must select option 1 or 2 to provide sample data to the program. Option 1, seeds the sample data from the assignment instruction. Option 2, will allow you to add the sample data manually. Please note, it only implements Add Routes, which allows you add route data to the program.

## Run RouteTrixs Test Project
### To run the RouteTrixsTests program in Visual Studio Code 
- Navigate to project folder and containing RouteTrixsTests.csproj. 
- Open the folder in a new terminal and type dotnet test.
> C:\Development\NewGlobe\RouteTrixsTest> dotnet test

### To run the RouteTrixsTests program in Visual Studio 
- Navigate to the "View" menu and select "Test Explorer" to open the Test Exporer pane. 
- Click on run test icon to run the unit tests in the project.

```
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     8, Skipped:     0, Total:     8, Duration: 100 ms - RoutetrixsTests.dll (net8.0)

```


