using Microsoft.Extensions.DependencyInjection;
using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Repositories;
using CarRentalApp.Application;

// Set up Dependency Injection container
var services = new ServiceCollection();
services.AddSingleton<ICarRepository, CarRepository>();
services.AddSingleton<ICustomerRepository, CustomerRepository>();
services.AddSingleton<IRentalRepository, RentalRepository>();
services.AddSingleton<RentalService>();

var serviceProvider = services.BuildServiceProvider();
var rentalService = serviceProvider.GetRequiredService<RentalService>();

// Main application loop
while (true)
{
	Console.WriteLine("\n=== Car Rental Management System ===");
	Console.WriteLine("1. Manage Cars");
	Console.WriteLine("2. Manage Customers");
	Console.WriteLine("3. Manage Rentals");
	Console.WriteLine("0. Exit");
	Console.Write("Choose: ");

	var mainChoice = Console.ReadLine();

	if (mainChoice == "1")
	{
		ManageCars(rentalService);
	}
	else if (mainChoice == "2")
	{
		ManageCustomers(rentalService);
	}
	else if (mainChoice == "3")
	{
		ManageRentals(rentalService);
	}
	else if (mainChoice == "0")
	{
		break;
	}
	else
	{
		Console.WriteLine("Unknown choice.");
	}
}

// ========== CAR MANAGEMENT ==========
void ManageCars(RentalService service)
{
	while (true)
	{
		Console.WriteLine("\n--- Car Management ---");
		Console.WriteLine("1. Add car");
		Console.WriteLine("2. List cars");
		Console.WriteLine("3. Update car");
		Console.WriteLine("4. Delete car");
		Console.WriteLine("0. Back to main menu");
		Console.Write("Choose: ");

		var choice = Console.ReadLine();

		if (choice == "1")
		{
			Console.Write("Registration number: ");
			var regNum = Console.ReadLine() ?? "";

			Console.Write("Brand: ");
			var brand = Console.ReadLine() ?? "";

			Console.Write("Model: ");
			var model = Console.ReadLine() ?? "";

			Console.Write("Year: ");
			var year = ReadInt("Year: ");

			Console.Write("Daily rate: ");
			var dailyRate = ReadDecimal("Daily rate: ");

			try
			{
				var car = new Car(regNum, brand, model, year, dailyRate);
				service.AddCar(car);
				Console.WriteLine($"✓ Car added with ID {car.Id}.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"✗ Error: {ex.Message}");
			}
		}
		else if (choice == "2")
		{
			var cars = service.GetAllCars();
			if (cars.Count == 0)
			{
				Console.WriteLine("No cars found.");
			}
			else
			{
				Console.WriteLine("\n--- All Cars ---");
				foreach (var car in cars)
				{
					Console.WriteLine(
						$"{car.Id}: {car.RegistrationNumber} | {car.Brand} {car.Model} ({car.Year}) | " +
						$"{car.DailyRate:C}/day | Available: {(car.IsAvailable ? "Yes" : "No")}");
				}
			}
		}
		else if (choice == "3")
		{
			Console.Write("Car ID to update: ");
			var id = ReadInt("Car ID: ");

			var car = service.GetCarById(id);
			if (car == null)
			{
				Console.WriteLine("Car not found.");
				continue;
			}

			Console.Write("New registration number (leave empty to keep): ");
			var regNum = Console.ReadLine() ?? "";

			Console.Write("New brand (leave empty to keep): ");
			var brand = Console.ReadLine() ?? "";

			Console.Write("New model (leave empty to keep): ");
			var model = Console.ReadLine() ?? "";

			var year = car.Year;
			Console.Write($"New year (leave empty to keep {year}): ");
			var yearInput = Console.ReadLine();
			if (!string.IsNullOrEmpty(yearInput) && int.TryParse(yearInput, out var parsedYear))
			{
				year = parsedYear;
			}

			var dailyRate = car.DailyRate;
			Console.Write($"New daily rate (leave empty to keep {dailyRate}): ");
			var rateInput = Console.ReadLine();
			if (!string.IsNullOrEmpty(rateInput) && decimal.TryParse(rateInput, out var parsedRate))
			{
				dailyRate = parsedRate;
			}

			try
			{
				var updatedCar = new Car(
					regNum ?? car.RegistrationNumber,
					brand ?? car.Brand,
					model ?? car.Model,
					year,
					dailyRate)
				{
					Id = car.Id,
					IsAvailable = car.IsAvailable
				};

				service.UpdateCar(updatedCar);
				Console.WriteLine("✓ Car updated.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"✗ Error: {ex.Message}");
			}
		}
		else if (choice == "4")
		{
			Console.Write("Car ID to delete: ");
			var id = ReadInt("Car ID: ");

			if (service.DeleteCar(id))
			{
				Console.WriteLine("✓ Car deleted.");
			}
			else
			{
				Console.WriteLine("✗ Car not found.");
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
}

// ========== CUSTOMER MANAGEMENT ==========
void ManageCustomers(RentalService service)
{
	while (true)
	{
		Console.WriteLine("\n--- Customer Management ---");
		Console.WriteLine("1. Add customer");
		Console.WriteLine("2. List customers");
		Console.WriteLine("3. Update customer");
		Console.WriteLine("4. Delete customer");
		Console.WriteLine("0. Back to main menu");
		Console.Write("Choose: ");

		var choice = Console.ReadLine();

		if (choice == "1")
		{
			Console.Write("Name: ");
			var name = Console.ReadLine() ?? "";

			Console.Write("Email: ");
			var email = Console.ReadLine() ?? "";

			Console.Write("Phone number: ");
			var phone = Console.ReadLine() ?? "";

			Console.Write("License number: ");
			var license = Console.ReadLine() ?? "";

			try
			{
				var customer = new Customer(name, email, phone, license);
				service.AddCustomer(customer);
				Console.WriteLine($"✓ Customer added with ID {customer.Id}.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"✗ Error: {ex.Message}");
			}
		}
		else if (choice == "2")
		{
			var customers = service.GetAllCustomers();
			if (customers.Count == 0)
			{
				Console.WriteLine("No customers found.");
			}
			else
			{
				Console.WriteLine("\n--- All Customers ---");
				foreach (var customer in customers)
				{
					Console.WriteLine(
						$"{customer.Id}: {customer.Name} | {customer.Email} | {customer.PhoneNumber} | License: {customer.LicenseNumber}");
				}
			}
		}
		else if (choice == "3")
		{
			Console.Write("Customer ID to update: ");
			var id = ReadInt("Customer ID: ");

			var customer = service.GetCustomerById(id);
			if (customer == null)
			{
				Console.WriteLine("Customer not found.");
				continue;
			}

			Console.Write("New name (leave empty to keep): ");
			var name = Console.ReadLine() ?? "";

			Console.Write("New email (leave empty to keep): ");
			var email = Console.ReadLine() ?? "";

			Console.Write("New phone (leave empty to keep): ");
			var phone = Console.ReadLine() ?? "";

			Console.Write("New license number (leave empty to keep): ");
			var license = Console.ReadLine() ?? "";

			try
			{
				var updatedCustomer = new Customer(
					name ?? customer.Name,
					email ?? customer.Email,
					phone ?? customer.PhoneNumber,
					license ?? customer.LicenseNumber)
				{
					Id = customer.Id
				};

				service.UpdateCustomer(updatedCustomer);
				Console.WriteLine("✓ Customer updated.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"✗ Error: {ex.Message}");
			}
		}
		else if (choice == "4")
		{
			Console.Write("Customer ID to delete: ");
			var id = ReadInt("Customer ID: ");

			if (service.DeleteCustomer(id))
			{
				Console.WriteLine("✓ Customer deleted.");
			}
			else
			{
				Console.WriteLine("✗ Customer not found.");
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
}

// ========== RENTAL MANAGEMENT ==========
void ManageRentals(RentalService service)
{
	while (true)
	{
		Console.WriteLine("\n--- Rental Management ---");
		Console.WriteLine("1. Create rental");
		Console.WriteLine("2. List active rentals");
		Console.WriteLine("3. List all rentals");
		Console.WriteLine("4. Complete rental");
		Console.WriteLine("5. Cancel rental");
		Console.WriteLine("0. Back to main menu");
		Console.Write("Choose: ");

		var choice = Console.ReadLine();

		if (choice == "1")
		{
			Console.Write("Customer ID: ");
			var customerId = ReadInt("Customer ID: ");

			Console.Write("Car ID: ");
			var carId = ReadInt("Car ID: ");

			Console.Write("Rental start date (yyyy-MM-dd): ");
			var dateInput = Console.ReadLine() ?? "";
			if (!DateTime.TryParse(dateInput, out var startDate))
			{
				Console.WriteLine("✗ Invalid date format.");
				continue;
			}

			try
			{
				var rental = new Rental(customerId, carId, startDate);
				service.CreateRental(rental);
				Console.WriteLine($"✓ Rental created with ID {rental.Id}.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"✗ Error: {ex.Message}");
			}
		}
		else if (choice == "2")
		{
			var rentals = service.GetActiveRentals();
			if (rentals.Count == 0)
			{
				Console.WriteLine("No active rentals found.");
			}
			else
			{
				Console.WriteLine("\n--- Active Rentals ---");
				foreach (var rental in rentals)
				{
					var car = service.GetCarById(rental.CarId);
					var customer = service.GetCustomerById(rental.CustomerId);
					Console.WriteLine(
						$"{rental.Id}: {customer?.Name} rented {car?.RegistrationNumber} " +
						$"from {rental.RentalStartDate:yyyy-MM-dd}");
				}
			}
		}
		else if (choice == "3")
		{
			var rentals = service.GetAllRentals();
			if (rentals.Count == 0)
			{
				Console.WriteLine("No rentals found.");
			}
			else
			{
				Console.WriteLine("\n--- All Rentals ---");
				foreach (var rental in rentals)
				{
					var car = service.GetCarById(rental.CarId);
					var customer = service.GetCustomerById(rental.CustomerId);
					Console.WriteLine(
						$"{rental.Id}: {customer?.Name} | {car?.RegistrationNumber} | " +
						$"{rental.RentalStartDate:yyyy-MM-dd} - {rental.RentalEndDate?.ToString("yyyy-MM-dd") ?? "Active"} | Status: {rental.Status}");
				}
			}
		}
		else if (choice == "4")
		{
			Console.Write("Rental ID to complete: ");
			var id = ReadInt("Rental ID: ");

			Console.Write("End date (yyyy-MM-dd): ");
			var dateInput = Console.ReadLine() ?? "";
			if (!DateTime.TryParse(dateInput, out var endDate))
			{
				Console.WriteLine("✗ Invalid date format.");
				continue;
			}

			if (service.CompleteRental(id, endDate))
			{
				Console.WriteLine("✓ Rental completed.");
			}
			else
			{
				Console.WriteLine("✗ Rental not found or already completed.");
			}
		}
		else if (choice == "5")
		{
			Console.Write("Rental ID to cancel: ");
			var id = ReadInt("Rental ID: ");

			if (service.CancelRental(id))
			{
				Console.WriteLine("✓ Rental cancelled.");
			}
			else
			{
				Console.WriteLine("✗ Rental not found or already completed.");
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
}

// ========== UTILITY FUNCTIONS ==========
int ReadInt(string message)
{
	while (true)
	{
		Console.Write(message);
		if (int.TryParse(Console.ReadLine(), out var value))
		{
			return value;
		}
		Console.WriteLine("✗ Please enter a valid whole number.");
	}
}

decimal ReadDecimal(string message)
{
	while (true)
	{
		Console.Write(message);
		if (decimal.TryParse(Console.ReadLine(), out var value) && value >= 0)
		{
			return value;
		}
		Console.WriteLine("✗ Please enter a valid positive number.");
	}
}
