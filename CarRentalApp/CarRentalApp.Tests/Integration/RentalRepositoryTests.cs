using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Repositories;

namespace CarRentalApp.Tests.Integration;

public class RentalRepositoryTests
{
	private IRentalRepository _repository = null!;

	[SetUp]
	public void SetUp()
	{
		_repository = new RentalRepository();
		// Clean up any existing test data
		if (File.Exists("rentals.json"))
		{
			File.Delete("rentals.json");
		}
		_repository = new RentalRepository();
	}

	[TearDown]
	public void TearDown()
	{
		// Clean up test data after each test
		if (File.Exists("rentals.json"))
		{
			File.Delete("rentals.json");
		}
	}

	[Test]
	public void AddRental_RentalCanBeReadFromRepository()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		_repository.Add(rental);

		var savedRental = _repository.GetById(rental.Id);

		Assert.That(savedRental, Is.Not.Null);
		Assert.That(savedRental!.CustomerId, Is.EqualTo(1));
		Assert.That(savedRental.CarId, Is.EqualTo(1));
	}

	[Test]
	public void GetActiveRentals_ReturnsOnlyActiveRentals()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);

		var activeRental = new Rental(1, 1, startDate);
		_repository.Add(activeRental);

		var completedRental = new Rental(2, 2, startDate);
		_repository.Add(completedRental);
		completedRental.CompleteRental(startDate.AddDays(3));
		_repository.Update(completedRental);

		var activeRentals = _repository.GetActiveRentals();

		Assert.That(activeRentals.Count, Is.EqualTo(1));
		Assert.That(activeRentals[0].Status, Is.EqualTo(RentalStatus.Active));
	}

	[Test]
	public void GetByCustomerId_ReturnsRentalsForCustomer()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);

		var rental1 = new Rental(1, 1, startDate);
		var rental2 = new Rental(1, 2, startDate);
		var rental3 = new Rental(2, 3, startDate);

		_repository.Add(rental1);
		_repository.Add(rental2);
		_repository.Add(rental3);

		var customer1Rentals = _repository.GetByCustomerId(1);

		Assert.That(customer1Rentals.Count, Is.EqualTo(2));
		Assert.That(customer1Rentals.All(r => r.CustomerId == 1), Is.True);
	}

	[Test]
	public void GetByCarId_ReturnsRentalsForCar()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);

		var rental1 = new Rental(1, 1, startDate);
		var rental2 = new Rental(2, 1, startDate);
		var rental3 = new Rental(3, 2, startDate);

		_repository.Add(rental1);
		_repository.Add(rental2);
		_repository.Add(rental3);

		var car1Rentals = _repository.GetByCarId(1);

		Assert.That(car1Rentals.Count, Is.EqualTo(2));
		Assert.That(car1Rentals.All(r => r.CarId == 1), Is.True);
	}

	[Test]
	public void UpdateRental_UpdatesExistingRental()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(1, 1, startDate);

		_repository.Add(rental);

		var endDate = startDate.AddDays(3);
		rental.CompleteRental(endDate);

		var result = _repository.Update(rental);

		Assert.That(result, Is.True);
		var retrieved = _repository.GetById(rental.Id);
		Assert.That(retrieved!.Status, Is.EqualTo(RentalStatus.Completed));
		Assert.That(retrieved.RentalEndDate, Is.EqualTo(endDate));
	}

	[Test]
	public void DeleteRental_RemovesRentalFromRepository()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(1, 1, startDate);

		_repository.Add(rental);
		var deleted = _repository.Delete(rental.Id);

		Assert.That(deleted, Is.True);
		Assert.That(_repository.GetById(rental.Id), Is.Null);
	}

	[Test]
	public void PersistenceAcrossInstances_DataIsSavedToFile()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(1, 1, startDate);

		_repository.Add(rental);

		// Create new repository instance to simulate application restart
		var newRepository = new RentalRepository();
		var retrieved = newRepository.GetById(rental.Id);

		Assert.That(retrieved, Is.Not.Null);
		Assert.That(retrieved!.CustomerId, Is.EqualTo(1));
		Assert.That(retrieved.CarId, Is.EqualTo(1));
	}
}
