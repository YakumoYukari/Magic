using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Spell
	{
		public string Name;
		public Card Source;
		public UICard Display;

		public Spell(string name, Card source)
		{
			Name = name;
			Source = source;
		}
	}
}
