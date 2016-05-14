using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Battlefield
	{
		public Player Owner;
		public List<Card> Permanents { get; private set; }
		public int NumPermanents { get { return Permanents.Count; } }

		public Battlefield(Player owner)
		{
			Owner = owner;
			Permanents = new List<Card>();
		}

		public void AddCard(Card c)
		{
			c.CurrentZone = Zones.Battlefield;
			
			CardPile pile;
			if (CardPileManager.Instance.TryFindPile(c, out pile))
			{
				pile.AddCard(c.UI);
			}
			else
			{
				if (c.IsType(CardTypes.Land))
				{
					CardPile p = CardPileManager.Instance.AddCardPile(4, 1);
					p.AddCard(c.UI);
				}
				else
				{
					CardPile p = CardPileManager.Instance.AddCardPile(4, 0);
					p.AddCard(c.UI);
				}
				
			}

			UIMain.Instance.RegisterCard(c);

			Permanents.Add(c);
		}
	}
}
