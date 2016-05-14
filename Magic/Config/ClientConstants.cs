using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public static class ClientConstants
	{
		public static class UIConstants
		{
			public static readonly double CardsInHandPadding = 5.0d;
			public static readonly double BattlefieldRowSpacing = 10.0d;
			public static readonly double BattlefieldPileSpacing = 10.0d;
		}

		public static class GameConstants
		{
			//I'm setting these so that you can draw your entire Battle of Wits deck
			public static readonly int MaxPermanents = 100;
			public static readonly int MaxHandSize = 250;
		}
	}
}
