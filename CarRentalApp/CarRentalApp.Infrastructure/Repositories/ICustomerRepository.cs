using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

/// <summary>
/// Repository interface for customer management operations.
/// Implements the Repository Pattern for Customer entities.
/// </summary>
public interface ICustomerRepository
{
	/// <summary>
	/// Adds a new customer to the repository.
	/// </summary>
	void Add(Customer customer);

	/// <summary>
	/// Retrieves all customers from the repository.
	/// </summary>
	List<Customer> GetAll();

	/// <summary>
	/// Retrieves a customer by its ID.
	/// </summary>
	Customer? GetById(int id);

	/// <summary>
	/// Updates an existing customer in the repository.
	/// </summary>
	bool Update(Customer updatedCustomer);

	/// <summary>
	/// Deletes a customer from the repository by its ID.
	/// </summary>
	bool Delete(int id);
}
