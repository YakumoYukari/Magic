using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
	public class Pair<T,U>
	{
		public T First;
		public U Second;

		public Pair(T one, U two)
		{
			First = one;
			Second = two;
		}
	}
}
