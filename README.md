# CarRentalApp

A simple C# console application for managing cars.

## Features

- Add car
- Display cars
- Update car
- Delete car
- Save cars to JSON file

## Projects

- `CarRentalApp` – console user interface
- `CarRentalApp.Domain` – `Car` class and business rules
- `CarRentalApp.Infrastructure` – `CarRepository` and JSON storage
- `CarRentalApp.Tests` – NUnit tests

## Getting Started

```cmd
dotnet build
dotnet run --project CarRentalApp/CarRentalApp.csproj