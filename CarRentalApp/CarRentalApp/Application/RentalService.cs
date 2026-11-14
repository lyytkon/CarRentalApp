using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Repositories;

namespace CarRentalApp.Application;

/// <summary>
/// Service class for car rental operations.
/// Orchestrates interactions between repositories and presents a cohesive API to the presentation layer.
/// Demonstrates Facade Pattern and Single Responsibility Principle.
/// </summary>
public class RentalService
{
	private readonly ICarRepository _carRepository;
	private readonly ICustomerRepository _customerRepository;
	private readonly IRentalRepository _rentalRepository;

	public RentalService(
		ICarRepository carRepository,
		ICustomerRepository customerRepository,
		IRentalRepository rentalRepository)
	{
		_carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
		_customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
		_rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
	}

	// ========== CAR OPERATIONS ==========

	public void AddCar(Car car)
	{
		_carRepository.Add(car);
	}

	public List<Car> GetAllCars()
	{
		return _carRepository.GetAll();
	}

	public Car? GetCarById(int id)
	{
		return _carRepository.GetById(id);
	}

	public bool UpdateCar(Car car)
	{
		return _carRepository.Update(car);
	}

	public bool DeleteCar(int id)
	{
		return _carRepository.Delete(id);
	}

	// ========== CUSTOMER OPERATIONS ==========

	public void AddCustomer(Customer customer)
	{
		_customerRepository.Add(customer);
	}

	public List<Customer> GetAllCustomers()
	{
		return _customerRepository.GetAll();
	}

	public Customer? GetCustomerById(int id)
	{
		return _customerRepository.GetById(id);
	}

	public bool UpdateCustomer(Customer customer)
	{
		return _customerRepository.Update(customer);
	}

	public bool DeleteCustomer(int id)
	{
		return _customerRepository.Delete(id);
	}

	// ========== RENTAL OPERATIONS ==========

	public void CreateRental(Rental rental)
	{
		var car = _carRepository.GetById(rental.CarId);
		if (car == null)
		{
			throw new InvalidOperationException("Car not found.");
		}

		if (!car.IsAvailable)
		{
			throw new InvalidOperationException("Car is not available for rental.");
		}

		var customer = _customerRepository.GetById(rental.CustomerId);
		if (customer == null)
		{
			throw new InvalidOperationException("Customer not found.");
		}

		_rentalRepository.Add(rental);
		car.IsAvailable = false;
		_carRepository.Update(car);
	}

	public List<Rental> GetAllRentals()
	{
		return _rentalRepository.GetAll();
	}

	public Rental? GetRentalById(int id)
	{
		return _rentalRepository.GetById(id);
	}

	public List<Rental> GetActiveRentals()
	{
		return _rentalRepository.GetActiveRentals();
	}

	public List<Rental> GetRentalsByCustomer(int customerId)
	{
		return _rentalRepository.GetByCustomerId(customerId);
	}

	public List<Rental> GetRentalsByCar(int carId)
	{
		return _rentalRepository.GetByCarId(carId);
	}

	public bool CompleteRental(int rentalId, DateTime endDate)
	{
		var rental = _rentalRepository.GetById(rentalId);
		if (rental == null || rental.Status != RentalStatus.Active)
		{
			return false;
		}

		rental.CompleteRental(endDate);
		_rentalRepository.Update(rental);

		var car = _carRepository.GetById(rental.CarId);
		if (car != null)
		{
			car.IsAvailable = true;
			_carRepository.Update(car);
		}

		return true;
	}

	public bool CancelRental(int rentalId)
	{
		var rental = _rentalRepository.GetById(rentalId);
		if (rental == null || rental.Status != RentalStatus.Active)
		{
			return false;
		}

		rental.CancelRental();
		_rentalRepository.Update(rental);

		var car = _carRepository.GetById(rental.CarId);
		if (car != null)
		{
			car.IsAvailable = true;
			_carRepository.Update(car);
		}

		return true;
	}
}
