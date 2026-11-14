using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Repositories;

namespace CarRentalApp.Tests.Integration;

public class CustomerRepositoryTests
{
	private ICustomerRepository _repository = null!;

	[SetUp]
	public void SetUp()
	{
		_repository = new CustomerRepository();
		// Clean up any existing test data
		if (File.Exists("customers.json"))
		{
			File.Delete("customers.json");
		}
		_repository = new CustomerRepository();
	}

	[TearDown]
	public void TearDown()
	{
		// Clean up test data after each test
		if (File.Exists("customers.json"))
		{
			File.Delete("customers.json");
		}
	}

	[Test]
	public void AddCustomer_CustomerCanBeReadFromRepository()
	{
		var customer = new Customer(
			"Jane Smith",
			"jane@example.com",
			"+47 87654321",
			"DL654321");

		_repository.Add(customer);

		var savedCustomer = _repository.GetById(customer.Id);

		Assert.That(savedCustomer, Is.Not.Null);
		Assert.That(savedCustomer!.Name, Is.EqualTo("Jane Smith"));
		Assert.That(savedCustomer.Email, Is.EqualTo("jane@example.com"));
	}

	[Test]
	public void GetAll_ReturnsAllCustomers()
	{
		var customer1 = new Customer(
			"John Doe",
			"john@example.com",
			"+47 12345678",
			"DL123456");
		var customer2 = new Customer(
			"Jane Smith",
			"jane@example.com",
			"+47 87654321",
			"DL654321");

		_repository.Add(customer1);
		_repository.Add(customer2);

		var allCustomers = _repository.GetAll();

		Assert.That(allCustomers.Count, Is.EqualTo(2));
	}

	[Test]
	public void UpdateCustomer_UpdatesExistingCustomer()
	{
		var customer = new Customer(
			"John Doe",
			"john@example.com",
			"+47 12345678",
			"DL123456");

		_repository.Add(customer);

		var updatedCustomer = new Customer(
			"John Updated",
			"john.updated@example.com",
			"+47 99999999",
			"DL999999")
		{
			Id = customer.Id
		};

		var result = _repository.Update(updatedCustomer);

		Assert.That(result, Is.True);
		var retrieved = _repository.GetById(customer.Id);
		Assert.That(retrieved!.Name, Is.EqualTo("John Updated"));
	}

	[Test]
	public void DeleteCustomer_RemovesCustomerFromRepository()
	{
		var customer = new Customer(
			"John Doe",
			"john@example.com",
			"+47 12345678",
			"DL123456");

		_repository.Add(customer);
		var deleted = _repository.Delete(customer.Id);

		Assert.That(deleted, Is.True);
		Assert.That(_repository.GetById(customer.Id), Is.Null);
	}

	[Test]
	public void DeleteNonExistentCustomer_ReturnsFalse()
	{
		var result = _repository.Delete(999);

		Assert.That(result, Is.False);
	}

	[Test]
	public void PersistenceAcrossInstances_DataIsSavedToFile()
	{
		var customer = new Customer(
			"John Doe",
			"john@example.com",
			"+47 12345678",
			"DL123456");

		_repository.Add(customer);

		// Create new repository instance to simulate application restart
		var newRepository = new CustomerRepository();
		var retrieved = newRepository.GetById(customer.Id);

		Assert.That(retrieved, Is.Not.Null);
		Assert.That(retrieved!.Name, Is.EqualTo("John Doe"));
	}
}
