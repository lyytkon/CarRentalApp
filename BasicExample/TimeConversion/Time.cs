using System;
using System.Collections.Generic;
using System.Text;

namespace TimeConversion
{
	public class Time
	{
		public int Seconds { get; set; }

		public int Hours
		{
			get {
				return Seconds / 3600;
			}
			set	{
				Seconds = value * 3600;
			}
		}
	}
}