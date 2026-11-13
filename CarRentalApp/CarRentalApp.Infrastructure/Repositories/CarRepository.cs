using System.Text.Json;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

/// <summary>
/// Implementation of the ICarRepository interface.
/// Handles car data persistence using JSON file storage.
/// </summary>
public class CarRepository : ICarRepository
{
	private readonly string _filePath = "cars.json";
	private readonly List<Car> _cars;
	private int _nextId;

	public CarRepository()
	{
		if (File.Exists(_filePath))
		{
			var json = File.ReadAllText(_filePath);
			_cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
		}
		else
		{
			_cars = new List<Car>();
		}

		_nextId = _cars.Count == 0 ? 1 : _cars.Max(car => car.Id) + 1;
	}

	public void Add(Car car)
	{
		if (car == null)
		{
			throw new ArgumentNullException(nameof(car));
		}

		car.Id = _nextId++;
		_cars.Add(car);
		Save();
	}

	public List<Car> GetAll()
	{
		return _cars;
	}

	public Car? GetById(int id)
	{
		return _cars.FirstOrDefault(car => car.Id == id);
	}

	public bool Delete(int id)
	{
		var car = _cars.FirstOrDefault(car => car.Id == id);

		if (car is null)
		{
			return false;
		}

		_cars.Remove(car);
		Save();
		return true;
	}

	public bool Update(Car updatedCar)
	{
		if (updatedCar == null)
		{
			throw new ArgumentNullException(nameof(updatedCar));
		}

		var existingCar = _cars.FirstOrDefault(car => car.Id == updatedCar.Id);

		if (existingCar is null)
		{
			return false;
		}

		existingCar.RegistrationNumber = updatedCar.RegistrationNumber;
		existingCar.Brand = updatedCar.Brand;
		existingCar.Model = updatedCar.Model;
		existingCar.Year = updatedCar.Year;
		existingCar.DailyRate = updatedCar.DailyRate;
		existingCar.IsAvailable = updatedCar.IsAvailable;

		Save();
		return true;
	}

	private void Save()
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(_cars, options);
		File.WriteAllText(_filePath, json);
	}
}
