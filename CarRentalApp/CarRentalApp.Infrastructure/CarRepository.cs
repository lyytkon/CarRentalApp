using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

public class CarRepository
{
	private readonly List<Car> _cars = new();
	private int _nextId = 1;

	public void Add(Car car)
	{
		car.Id = _nextId;
		_nextId++;
		_cars.Add(car);
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
		return true;
	}
}