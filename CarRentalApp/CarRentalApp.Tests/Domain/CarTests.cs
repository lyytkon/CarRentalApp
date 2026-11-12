using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Tests.Domain;

public class CarTests
{
	[Test]
	public void NewCar_IsAvailableByDefault()
	{
		var car = new Car(
			"ABC-123",
			"Toyota",
			"Corolla",
			2022,
			45.00m);

		Assert.That(car.IsAvailable, Is.True);
	}
}