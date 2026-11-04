namespace FractionApp
{
	public class Fraction : IComparable<Fraction>
	{
		#region Properties

		public int Numerator { get; private set; }
		
		private int _denominator;
		public int Denominator 
		{ 
			get { 
				return _denominator; 
			} 
			private set {
				if (value != 0) {
					_denominator = value;
				} else {
					// Denominator should never be set to zero.
					throw new ArgumentOutOfRangeException(nameof(_denominator), value, $"The denominator can't be zero.");
				}
			} 
		}
		
		#endregion


        #region Constructors

		// By adding default values for both numerator and denominator, we also cover a parameterless constructor.
		public Fraction(int numerator = 0, int denominator = 1)
		{
            // Denominator should never stay negative (but numerator can do that).
            if (denominator < 0) {
                Numerator = -numerator;
                Denominator = -denominator;
            } else {
                Numerator = numerator;
                Denominator = denominator;
            }
            Reduce();
		}

		#endregion


		#region Instance Methods

		/// <summary>
		/// Reduces the fraction.
		/// </summary>
		/// <returns>Nothing (void).</returns>
		private void Reduce()
		{
            // Reduce algorithm using the MathUtil class.
            int greatestCommonDivisor = (int)MathUtil.GreatestCommonDivisor((uint)Math.Abs(Numerator), (uint)Math.Abs(Denominator));

            Numerator /= greatestCommonDivisor;
            Denominator /= greatestCommonDivisor;
		}


		public override string ToString()
		{
			return $"{Numerator}/{Denominator}";
		}


        public bool Equals(Fraction otherFraction)
		{
			return Numerator * otherFraction.Denominator == otherFraction.Numerator * Denominator;
		}


		/// <summary>
		/// Compares this otherFraction to another.
		/// </summary>
		/// <param name="obj">The object we compare with.</param>
		/// <returns>Returns 0 if equal, 1 if greater and -1 if lesser.</returns>
		public int CompareTo(Fraction? other)
		{
			// NOTE: This method is required if we choose to implement the IComparable
			// interface (letting us sort Fractions stored in collections and the like.)

			if (other is null || this.GreaterThan(other)) {
				return 1;
			} else if (this == other) {
				return 0;
			} else {
				return -1;
			}
		}


		public float ToFloat()
		{
			// Need to cast at least one of them, to get float as answer (int / int gives int as answer).
			return Numerator / (float)Denominator;
		}


		public double ToDouble()
		{
			return Numerator / (double)Denominator;
		}


		public Fraction Negate()
		{
			return new Fraction(-Numerator, Denominator);
		}


		// Reciprocal: a / b flipped into b / a
		public Fraction Reciprocal()
		{
			return new Fraction(Denominator, Numerator);
		}


		public Fraction Add(Fraction otherFraction)
		{
			int newNumeratorResult = (Numerator * otherFraction.Denominator) + (otherFraction.Numerator * Denominator);

			int newDenominatorResult = Denominator * otherFraction.Denominator;

			return new Fraction(newNumeratorResult, newDenominatorResult);
		}


		public Fraction Substract(Fraction otherFraction)
		{
			return Add(otherFraction.Multiply(-1));
		}


		public Fraction Multiply(Fraction otherFraction)
		{
			return new Fraction(Numerator * otherFraction.Numerator, Denominator * otherFraction.Denominator);
		}


		public Fraction Multiply(int number)
		{
			return new Fraction(Numerator * number, Denominator);
		}


		public Fraction Divide(Fraction otherFraction)
		{
			return Multiply(otherFraction.Reciprocal());
		}


		public Fraction Power(int power)
		{
			if (power == 1) {
				return new Fraction(Numerator, Denominator);
			}

			int numerator = (int)Math.Pow(Numerator, Math.Abs(power));
			int denominator = (int)Math.Pow(Denominator, Math.Abs(power));

			// (a/b)^-1 == 1/(a/b) == (b/a)
			// If powerRight less than zero return reciprocal.
			return power > 0 ? new Fraction(numerator, denominator) : new Fraction(denominator, numerator);
		}


		public bool GreaterThan(Fraction otherFraction)
		{
			return (Numerator * otherFraction.Denominator) > (otherFraction.Numerator * Denominator);
		}


		public bool LesserThan(Fraction otherFraction)
		{
			return !GreaterThanOrEqual(otherFraction);
		}


		public bool GreaterThanOrEqual(Fraction otherFraction)
		{
			return Equals(otherFraction) || GreaterThan(otherFraction);
		}


		public bool LesserThanOrEqual(Fraction otherFraction)
		{
			return !GreaterThan(otherFraction);
		}

		#endregion


		#region Operator Overloads

		// TODO: Add operator overloads here. :-)

        #endregion
    }
}
