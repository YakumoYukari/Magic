using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Ability
	{
		public Card Source;
		public AbilityTypes Type;

		public Ability(Card source, AbilityTypes type)
		{
			Source = source;
			Type = type;
		}
	}
}
