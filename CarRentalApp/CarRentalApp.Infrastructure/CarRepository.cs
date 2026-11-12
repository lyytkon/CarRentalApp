using System.Text.Json;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

public class CarRepository
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
		car.Id = _nextId++;
		_cars.Add(car);
		Save();
	}

	public List<Car> GetAll()
	{
		return _cars;
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