using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalApp.Domain.Entities;

public class Car
{
	public int Id { get; set; }

	public string RegistrationNumber { get; private set; }

	public string Brand { get; private set; }

	public string Model { get; private set; }

	public int Year { get; private set; }

	public decimal DailyRate { get; private set; }

	public bool IsAvailable { get; private set; }

	public Car(
		string registrationNumber,
		string brand,
		string model,
		int year,
		decimal dailyRate)
	{
		if (string.IsNullOrWhiteSpace(registrationNumber))
		{
			throw new ArgumentException(
				"Registration number cannot be empty.",
				nameof(registrationNumber));
		}

		if (string.IsNullOrWhiteSpace(brand))
		{
			throw new ArgumentException(
				"Brand cannot be empty.",
				nameof(brand));
		}

		if (string.IsNullOrWhiteSpace(model))
		{
			throw new ArgumentException(
				"Model cannot be empty.",
				nameof(model));
		}

		if (year < 1886 || year > DateTime.UtcNow.Year + 1)
		{
			throw new ArgumentOutOfRangeException(
				nameof(year),
				"Year must be between 1886 and next year.");
		}

		if (dailyRate < 0)
		{
			throw new ArgumentOutOfRangeException(
				nameof(dailyRate),
				"Daily rate cannot be negative.");
		}
		
		RegistrationNumber = registrationNumber;
		Brand = brand;
		Model = model;
		Year = year;
		DailyRate = dailyRate;
		IsAvailable = true;
	}
}