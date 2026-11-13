using System;

namespace CarRentalApp.Domain.Entities;

public class Customer
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string PhoneNumber { get; set; }
	public string LicenseNumber { get; set; }

	public Customer(
		string name,
		string email,
		string phoneNumber,
		string licenseNumber)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException(
				"Name cannot be empty.",
				nameof(name));
		}

		if (string.IsNullOrWhiteSpace(email))
		{
			throw new ArgumentException(
				"Email cannot be empty.",
				nameof(email));
		}

		if (!email.Contains("@"))
		{
			throw new ArgumentException(
				"Email must be a valid email address.",
				nameof(email));
		}

		if (string.IsNullOrWhiteSpace(phoneNumber))
		{
			throw new ArgumentException(
				"Phone number cannot be empty.",
				nameof(phoneNumber));
		}

		if (string.IsNullOrWhiteSpace(licenseNumber))
		{
			throw new ArgumentException(
				"License number cannot be empty.",
				nameof(licenseNumber));
		}

		Name = name;
		Email = email;
		PhoneNumber = phoneNumber;
		LicenseNumber = licenseNumber;
	}
}
