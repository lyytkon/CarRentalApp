using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Repositories;

namespace CarRentalApp.Tests.Integration;

public class CarRepositoryTests
{
	[Test]
	public void AddCar_CarCanBeReadFromRepository()
	{
		var repository = new CarRepository();

		var car = new Car(
			"ADD-TEST-123",
			"Toyota",
			"Corolla",
			2022,
			45.00m);

		repository.Add(car);

		var savedCar = repository.GetAll()
			.Single(c => c.Id == car.Id);

		Assert.That(savedCar.RegistrationNumber, Is.EqualTo("ADD-TEST-123"));
		Assert.That(savedCar.Brand, Is.EqualTo("Toyota"));
		Assert.That(savedCar.Model, Is.EqualTo("Corolla"));
	}

	[Test]
	public void DeleteCar_RemovesSelectedCar()
	{
		var repository = new CarRepository();

		var car = new Car(
			"DELETE-123",
			"Toyota",
			"Corolla",
			2022,
			45.00m);

		repository.Add(car);

		var deleted = repository.Delete(car.Id);

		Assert.That(deleted, Is.True);
		Assert.That(
			repository.GetAll().Any(c => c.Id == car.Id),
			Is.False);
	}
	[Test]
	public void UpdateCar_ChangesCarInformation()
	{
		var repository = new CarRepository();

		var car = new Car(
			"UPDATE-123",
			"Toyota",
			"Corolla",
			2022,
			45.00m);

		repository.Add(car);

		car.Brand = "Volvo";
		car.Model = "V60";
		car.DailyRate = 60.00m;

		var updated = repository.Update(car);

		var savedCar = repository.GetAll()
			.Single(c => c.Id == car.Id);

		Assert.That(updated, Is.True);
		Assert.That(savedCar.Brand, Is.EqualTo("Volvo"));
		Assert.That(savedCar.Model, Is.EqualTo("V60"));
		Assert.That(savedCar.DailyRate, Is.EqualTo(60.00m));
	}
}