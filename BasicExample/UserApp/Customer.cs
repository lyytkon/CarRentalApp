using System;
using System.Collections.Generic;
using System.Text;

namespace UserApp
{
	public class Customer
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		private int _age;
		public int Age { 
			get {
				return _age;
			}
			set {
				if (value >= 0) {
					_age = value;
				}
			} 
		}

		private int Rating { get; set; }
	}
}
