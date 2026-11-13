using System;

namespace CarRentalApp.Domain.Entities;

public class Rental
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public int CarId { get; set; }
	public DateTime RentalStartDate { get; set; }
	public DateTime? RentalEndDate { get; set; }
	public RentalStatus Status { get; set; }

	public Rental(
		int customerId,
		int carId,
		DateTime rentalStartDate)
	{
		if (customerId <= 0)
		{
			throw new ArgumentException(
				"Customer ID must be greater than zero.",
				nameof(customerId));
		}

		if (carId <= 0)
		{
			throw new ArgumentException(
				"Car ID must be greater than zero.",
				nameof(carId));
		}

		if (rentalStartDate < DateTime.UtcNow.Date)
		{
			throw new ArgumentException(
				"Rental start date cannot be in the past.",
				nameof(rentalStartDate));
		}

		CustomerId = customerId;
		CarId = carId;
		RentalStartDate = rentalStartDate;
		RentalEndDate = null;
		Status = RentalStatus.Active;
	}

	/// <summary>
	/// Calculates the total rental cost based on the daily rate and rental duration.
	/// </summary>
	public decimal CalculateTotalCost(decimal dailyRate)
	{
		if (RentalEndDate is null)
		{
			return 0; // Rental is still active
		}

		var rentalDays = (RentalEndDate.Value.Date - RentalStartDate.Date).Days;
		if (rentalDays < 1)
		{
			rentalDays = 1; // Minimum 1 day
		}

		return dailyRate * rentalDays;
	}

	/// <summary>
	/// Completes the rental by setting the end date and status.
	/// </summary>
	public void CompleteRental(DateTime endDate)
	{
		if (endDate < RentalStartDate)
		{
			throw new ArgumentException(
				"End date cannot be before start date.",
				nameof(endDate));
		}

		RentalEndDate = endDate;
		Status = RentalStatus.Completed;
	}

	/// <summary>
	/// Cancels the rental.
	/// </summary>
	public void CancelRental()
	{
		Status = RentalStatus.Cancelled;
	}
}
