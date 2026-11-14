using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Tests.Domain;

public class CustomerTests
{
	[Test]
	public void NewCustomer_WithValidData_CreatesSuccessfully()
	{
		var customer = new Customer(
			"John Doe",
			"john@example.com",
			"+47 12345678",
			"DL123456");

		Assert.That(customer.Name, Is.EqualTo("John Doe"));
		Assert.That(customer.Email, Is.EqualTo("john@example.com"));
		Assert.That(customer.PhoneNumber, Is.EqualTo("+47 12345678"));
		Assert.That(customer.LicenseNumber, Is.EqualTo("DL123456"));
	}

	[Test]
	public void NewCustomer_WithNullName_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				null!,
				"john@example.com",
				"+47 12345678",
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithEmptyName_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"",
				"john@example.com",
				"+47 12345678",
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithWhitespaceName_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"   ",
				"john@example.com",
				"+47 12345678",
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithNullEmail_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"John Doe",
				null!,
				"+47 12345678",
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithEmptyEmail_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"John Doe",
				"",
				"+47 12345678",
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithInvalidEmail_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"John Doe",
				"invalid-email",
				"+47 12345678",
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithNullPhoneNumber_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"John Doe",
				"john@example.com",
				null!,
				"DL123456"));
	}

	[Test]
	public void NewCustomer_WithNullLicenseNumber_ThrowsException()
	{
		Assert.Throws<ArgumentException>(() =>
			new Customer(
				"John Doe",
				"john@example.com",
				"+47 12345678",
				null!));
	}
}
