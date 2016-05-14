using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
	public static class Mathd
	{
		public static double Clamp(double a, double min, double max)
		{
			if (a < min) return min;
			if (a > max) return max;
			return a;
		}
	}
}
