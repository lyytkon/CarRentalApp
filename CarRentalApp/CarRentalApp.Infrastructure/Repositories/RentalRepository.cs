using System.Text.Json;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

/// <summary>
/// Implementation of the IRentalRepository interface.
/// Handles rental data persistence using JSON file storage.
/// </summary>
public class RentalRepository : IRentalRepository
{
	private readonly string _filePath = "rentals.json";
	private readonly List<Rental> _rentals;
	private int _nextId;

	public RentalRepository()
	{
		if (File.Exists(_filePath))
		{
			var json = File.ReadAllText(_filePath);
			_rentals = JsonSerializer.Deserialize<List<Rental>>(json) ?? new List<Rental>();
		}
		else
		{
			_rentals = new List<Rental>();
		}

		_nextId = _rentals.Count == 0 ? 1 : _rentals.Max(r => r.Id) + 1;
	}

	public void Add(Rental rental)
	{
		if (rental == null)
		{
			throw new ArgumentNullException(nameof(rental));
		}

		rental.Id = _nextId++;
		_rentals.Add(rental);
		Save();
	}

	public List<Rental> GetAll()
	{
		return _rentals;
	}

	public Rental? GetById(int id)
	{
		return _rentals.FirstOrDefault(r => r.Id == id);
	}

	public List<Rental> GetActiveRentals()
	{
		return _rentals.Where(r => r.Status == RentalStatus.Active).ToList();
	}

	public List<Rental> GetByCustomerId(int customerId)
	{
		return _rentals.Where(r => r.CustomerId == customerId).ToList();
	}

	public List<Rental> GetByCarId(int carId)
	{
		return _rentals.Where(r => r.CarId == carId).ToList();
	}

	public bool Update(Rental updatedRental)
	{
		if (updatedRental == null)
		{
			throw new ArgumentNullException(nameof(updatedRental));
		}

		var existingRental = _rentals.FirstOrDefault(r => r.Id == updatedRental.Id);

		if (existingRental is null)
		{
			return false;
		}

		existingRental.CustomerId = updatedRental.CustomerId;
		existingRental.CarId = updatedRental.CarId;
		existingRental.RentalStartDate = updatedRental.RentalStartDate;
		existingRental.RentalEndDate = updatedRental.RentalEndDate;
		existingRental.Status = updatedRental.Status;

		Save();
		return true;
	}

	public bool Delete(int id)
	{
		var rental = _rentals.FirstOrDefault(r => r.Id == id);

		if (rental is null)
		{
			return false;
		}

		_rentals.Remove(rental);
		Save();
		return true;
	}

	private void Save()
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(_rentals, options);
		File.WriteAllText(_filePath, json);
	}
}
