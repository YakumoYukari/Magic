using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class TheStack
	{
		public static TheStack Instance;

		private Stack<Spell> Spells;

		public TheStack()
		{
			Spells = new Stack<Spell>();
			TheStack.Instance = this;
		}
	}
}
