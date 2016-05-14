using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
	public static class Random
	{
		static System.Random r = new System.Random();

		public static int Range(int min, int max)
		{
			return r.Next(min, max);
		}

	}
}
