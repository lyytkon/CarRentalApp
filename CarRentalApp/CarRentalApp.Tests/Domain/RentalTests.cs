using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Tests.Domain;

public class RentalTests
{
	[Test]
	public void NewRental_WithValidData_CreatesSuccessfully()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		Assert.That(rental.CustomerId, Is.EqualTo(1));
		Assert.That(rental.CarId, Is.EqualTo(1));
		Assert.That(rental.RentalStartDate, Is.EqualTo(startDate));
		Assert.That(rental.RentalEndDate, Is.Null);
		Assert.That(rental.Status, Is.EqualTo(RentalStatus.Active));
	}

	[Test]
	public void NewRental_WithInvalidCustomerId_ThrowsException()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);

		Assert.Throws<ArgumentException>(() =>
			new Rental(
				customerId: 0,
				carId: 1,
				rentalStartDate: startDate));
	}

	[Test]
	public void NewRental_WithNegativeCustomerId_ThrowsException()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);

		Assert.Throws<ArgumentException>(() =>
			new Rental(
				customerId: -1,
				carId: 1,
				rentalStartDate: startDate));
	}

	[Test]
	public void NewRental_WithInvalidCarId_ThrowsException()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);

		Assert.Throws<ArgumentException>(() =>
			new Rental(
				customerId: 1,
				carId: 0,
				rentalStartDate: startDate));
	}

	[Test]
	public void NewRental_WithPastStartDate_ThrowsException()
	{
		var pastDate = DateTime.UtcNow.Date.AddDays(-1);

		Assert.Throws<ArgumentException>(() =>
			new Rental(
				customerId: 1,
				carId: 1,
				rentalStartDate: pastDate));
	}

	[Test]
	public void CalculateTotalCost_WithActiveRental_ReturnsZero()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		var cost = rental.CalculateTotalCost(dailyRate: 50.00m);

		Assert.That(cost, Is.EqualTo(0));
	}

	[Test]
	public void CalculateTotalCost_WithCompletedRental_ReturnsTotalCost()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var endDate = startDate.AddDays(5);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		rental.CompleteRental(endDate);
		var cost = rental.CalculateTotalCost(dailyRate: 50.00m);

		// 5 days * 50 = 250
		Assert.That(cost, Is.EqualTo(250.00m));
	}

	[Test]
	public void CalculateTotalCost_WithOneDayRental_ReturnsOneDay()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var endDate = startDate; // Same day
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		rental.CompleteRental(endDate);
		var cost = rental.CalculateTotalCost(dailyRate: 50.00m);

		// Minimum 1 day
		Assert.That(cost, Is.EqualTo(50.00m));
	}

	[Test]
	public void CompleteRental_WithValidEndDate_CompletesSuccessfully()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var endDate = startDate.AddDays(3);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		rental.CompleteRental(endDate);

		Assert.That(rental.RentalEndDate, Is.EqualTo(endDate));
		Assert.That(rental.Status, Is.EqualTo(RentalStatus.Completed));
	}

	[Test]
	public void CompleteRental_WithEndDateBeforeStart_ThrowsException()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var endDate = startDate.AddDays(-1);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		Assert.Throws<ArgumentException>(() =>
			rental.CompleteRental(endDate));
	}

	[Test]
	public void CancelRental_ChangesStatusToCancelled()
	{
		var startDate = DateTime.UtcNow.Date.AddDays(1);
		var rental = new Rental(
			customerId: 1,
			carId: 1,
			rentalStartDate: startDate);

		rental.CancelRental();

		Assert.That(rental.Status, Is.EqualTo(RentalStatus.Cancelled));
	}
}
