using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

/// <summary>
/// Repository interface for car management operations.
/// Implements the Repository Pattern, separating data access logic from domain logic.
/// </summary>
public interface ICarRepository
{
	/// <summary>
	/// Adds a new car to the repository.
	/// </summary>
	void Add(Car car);

	/// <summary>
	/// Retrieves all cars from the repository.
	/// </summary>
	List<Car> GetAll();

	/// <summary>
	/// Retrieves a car by its ID.
	/// </summary>
	Car? GetById(int id);

	/// <summary>
	/// Updates an existing car in the repository.
	/// </summary>
	bool Update(Car updatedCar);

	/// <summary>
	/// Deletes a car from the repository by its ID.
	/// </summary>
	bool Delete(int id);
}
