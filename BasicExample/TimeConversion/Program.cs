namespace TimeConversion
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Time time = new();
			time.Hours = 1;

			Console.WriteLine(time.Seconds);
		}
	}
}
