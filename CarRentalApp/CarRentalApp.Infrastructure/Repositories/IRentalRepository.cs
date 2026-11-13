using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

/// <summary>
/// Repository interface for rental management operations.
/// Implements the Repository Pattern for Rental entities.
/// </summary>
public interface IRentalRepository
{
	/// <summary>
	/// Creates a new rental record.
	/// </summary>
	void Add(Rental rental);

	/// <summary>
	/// Retrieves all rentals from the repository.
	/// </summary>
	List<Rental> GetAll();

	/// <summary>
	/// Retrieves a rental by its ID.
	/// </summary>
	Rental? GetById(int id);

	/// <summary>
	/// Retrieves all active rentals.
	/// </summary>
	List<Rental> GetActiveRentals();

	/// <summary>
	/// Retrieves all rentals for a specific customer.
	/// </summary>
	List<Rental> GetByCustomerId(int customerId);

	/// <summary>
	/// Retrieves all rentals for a specific car.
	/// </summary>
	List<Rental> GetByCarId(int carId);

	/// <summary>
	/// Updates an existing rental in the repository.
	/// </summary>
	bool Update(Rental updatedRental);

	/// <summary>
	/// Deletes a rental from the repository by its ID.
	/// </summary>
	bool Delete(int id);
}
