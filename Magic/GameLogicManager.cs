using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class GameLogicManager
	{
		public static GameLogicManager Instance;

		public GameLogicManager()
		{
			RegisterEvents();
			GameLogicManager.Instance = this;
		}

		public void RegisterEvents()
		{
			EventManager.RegisterEvent(Events.UI_CARD_CLICKED, OnUICardClicked);
		}

		#region Events

		public void OnUICardClicked(object sender, EventArgs args)
		{
			UICard card = sender as UICard;

			if (card.TargetCard.CurrentZone == Zones.Hand)
			{
				PlayerManager.Instance.GetPlayer(card.TargetCard.CurrentPlayer).PlayCard(card.TargetCard);
			}
			else if (card.TargetCard.CurrentZone == Zones.Battlefield)
			{
				if (card.TargetCard.Tapped)
					card.TargetCard.Untap();
				else
					card.TargetCard.Tap();
			}
		}

		#endregion
	}
}
