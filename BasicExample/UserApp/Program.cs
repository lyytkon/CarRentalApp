namespace UserApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Customer customer = new ();
			customer.FirstName = "Tony";
			customer.LastName = "Stark";
			customer.Age = 8;

			Console.WriteLine($"{customer.FirstName} {customer.LastName} was able to built this in a fucking cave in {customer.Age} months.");

		}
	}
}
