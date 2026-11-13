using System.Text.Json;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Infrastructure.Repositories;

/// <summary>
/// Implementation of the ICustomerRepository interface.
/// Handles customer data persistence using JSON file storage.
/// </summary>
public class CustomerRepository : ICustomerRepository
{
	private readonly string _filePath = "customers.json";
	private readonly List<Customer> _customers;
	private int _nextId;

	public CustomerRepository()
	{
		if (File.Exists(_filePath))
		{
			var json = File.ReadAllText(_filePath);
			_customers = JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
		}
		else
		{
			_customers = new List<Customer>();
		}

		_nextId = _customers.Count == 0 ? 1 : _customers.Max(c => c.Id) + 1;
	}

	public void Add(Customer customer)
	{
		if (customer == null)
		{
			throw new ArgumentNullException(nameof(customer));
		}

		customer.Id = _nextId++;
		_customers.Add(customer);
		Save();
	}

	public List<Customer> GetAll()
	{
		return _customers;
	}

	public Customer? GetById(int id)
	{
		return _customers.FirstOrDefault(c => c.Id == id);
	}

	public bool Update(Customer updatedCustomer)
	{
		if (updatedCustomer == null)
		{
			throw new ArgumentNullException(nameof(updatedCustomer));
		}

		var existingCustomer = _customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);

		if (existingCustomer is null)
		{
			return false;
		}

		existingCustomer.Name = updatedCustomer.Name;
		existingCustomer.Email = updatedCustomer.Email;
		existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
		existingCustomer.LicenseNumber = updatedCustomer.LicenseNumber;

		Save();
		return true;
	}

	public bool Delete(int id)
	{
		var customer = _customers.FirstOrDefault(c => c.Id == id);

		if (customer is null)
		{
			return false;
		}

		_customers.Remove(customer);
		Save();
		return true;
	}

	private void Save()
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(_customers, options);
		File.WriteAllText(_filePath, json);
	}
}
