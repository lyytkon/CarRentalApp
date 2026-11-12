using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Repositories;

var repository = new CarRepository();

while (true)
{
	Console.WriteLine();
	Console.WriteLine("Car Rental App");
	Console.WriteLine("1. Add car");
	Console.WriteLine("2. List cars");
	Console.WriteLine("3. Delete car");
	Console.WriteLine("0. Exit");
	Console.Write("Choose: ");

	var choice = Console.ReadLine();

	if (choice == "1")
	{
		Console.Write("Registration number: ");
		var registrationNumber = Console.ReadLine() ?? "";

		Console.Write("Brand: ");
		var brand = Console.ReadLine() ?? "";

		Console.Write("Model: ");
		var model = Console.ReadLine() ?? "";

		Console.Write("Year: ");
		var year = int.Parse(Console.ReadLine() ?? "0");

		Console.Write("Daily rate: ");
		var dailyRate = decimal.Parse(Console.ReadLine() ?? "0");

		var car = new Car(
			registrationNumber,
			brand,
			model,
			year,
			dailyRate);

		repository.Add(car);
		Console.WriteLine($"Car added with ID {car.Id}.");
	}
	else if (choice == "2")
	{
		var cars = repository.GetAll();

		if (cars.Count == 0)
		{
			Console.WriteLine("No cars found.");
		}
		else
		{
			foreach (var car in cars)
			{
				Console.WriteLine(
					$"{car.Id}: {car.RegistrationNumber} " +
					$"{car.Brand} {car.Model}, " +
					$"{car.Year}, {car.DailyRate:C}, " +
					$"Available: {car.IsAvailable}");
			}
		}
	}
	else if (choice == "3")
	{
		Console.Write("Car ID to delete: ");
		var id = int.Parse(Console.ReadLine() ?? "0");

		if (repository.Delete(id))
		{
			Console.WriteLine("Car deleted.");
		}
		else
		{
			Console.WriteLine("Car not found.");
		}
	}
	else if (choice == "0")
	{
		break;
	}
	else
	{
		Console.WriteLine("Unknown choice.");
	}
}