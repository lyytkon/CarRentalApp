using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalApp.Domain.Entities;

public class Car
{
	public int Id { get; private set; }

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
		// Lisää tähän omat tarkistuksesi:
		// - tekstit eivät saa olla tyhjiä
		// - year on järkevä vuosiluku
		// - dailyRate ei saa olla negatiivinen

		RegistrationNumber = registrationNumber;
		Brand = brand;
		Model = model;
		Year = year;
		DailyRate = dailyRate;
		IsAvailable = true;
	}
}