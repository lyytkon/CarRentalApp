namespace FractionApp
{
	class Program
	{
		static void Main()
		{
			// Some primitive testing.
			// Test coverage will be MUCH BETTER once you have written some unit tests! ;-)

			Fraction empty = new();
			Fraction oneThird = new(1, 3);
			Fraction twoThird = new(2, 3);
			Fraction twoFourth = new(2, 4);

			Console.WriteLine($"Empty fraction is initialized to: {empty}\n");

			Console.WriteLine($"2/4 is automatically reduced to: {twoFourth}\n");

            Fraction addedTogether = oneThird.Add(twoFourth);
			// After operator overloading, we should be able to do:
            //Fraction addedTogether = oneThird + twoFourth;
            Console.WriteLine($"{oneThird} + {twoFourth} = {addedTogether}\n");

			Fraction multipliedTogether = twoThird.Multiply(twoFourth);
            // After operator overloading, we should be able to do:
            //Fraction multipliedTogether = twoThird * twoFourth;
            Console.WriteLine($"{twoThird} * {twoFourth} = {multipliedTogether}\n");
		}
	}
}
