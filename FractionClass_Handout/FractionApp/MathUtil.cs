namespace FractionApp
{
	public class MathUtil
	{
		/// <summary>
		/// Returns the greatest common divisor of two numbers.
		/// </summary>
		/// <param name="u"></param>
		/// <param name="v"></param>
		/// <returns>Greatest common divisor of u and v.</returns>
		public static uint GreatestCommonDivisor(uint u, uint v)
		{
			// Recursive algorithm adapted from the C code algoritm found here:
			// https://en.wikipedia.org/wiki/Binary_GCD_algorithm#Recursive_version_in_C

			// Simple cases (termination).
			if (u == v) {
				return u;
			}
			if (u == 0) {
				return v;
			}
			if (v == 0) {
				return u;
			}

			// Look for factors of 2.
			// u is even.
			if (u % 2 == 0) {
				// v is odd.
				if (v % 2 != 0) {
					return GreatestCommonDivisor(u / 2, v);
				}
				// Both u and v are even.
				return GreatestCommonDivisor(u / 2, v / 2) * 2;
			}

			// u is odd, v is even.
			if (v % 2 == 0) {
				return GreatestCommonDivisor(u, v / 2);
			}

			// Reduce larger argument.
			if (u > v) {
				return GreatestCommonDivisor((u - v) / 2, v);
			}
			return GreatestCommonDivisor((v - u) / 2, u);
		}


		/// <summary>
		/// Returns the least common multiple between two numbers.
		///  https://en.wikipedia.org/wiki/Least_common_multiple#Reduction_by_the_greatest_common_divisor
		/// </summary>
		/// <param name="u"></param>
		/// <param name="v"></param>
		/// <returns>Least common multiple of u and v.</returns>
		public static uint LeastCommonMultiple(uint u, uint v)
		{
			if (u == 0 && v == 0) {
				return 0;
			}
			return (uint)Math.Abs(u * v) / GreatestCommonDivisor(u, v);
		}
	}
}
