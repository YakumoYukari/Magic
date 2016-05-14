using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class AbilityManager
	{
		public static AbilityManager Instance;

		public AbilityManager()
		{
			AbilityManager.Instance = this;
		}
	}
}
