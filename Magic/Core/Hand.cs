using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Hand
	{
		Player Owner;
		List<Card> CardsInHand;
		public int HandSize { get { return CardsInHand.Count; } }

		public Hand(Player owner)
		{
			Owner = owner;
			CardsInHand = new List<Card>();
		}

		public Hand(Player owner, List<Card> cards)
		{
			Owner = owner;
			Card[] cArray = new Card[cards.Count];
			cards.CopyTo(cArray, 0);
			CardsInHand = cArray.ToList();

			AddCard(CardsInHand);
		}

		public void AddCard(Card card)
		{
			if (card == null) return;

			card.CurrentPlayer = PlayerManager.Instance.GetPlayer(Owner);
			card.CurrentZone = Zones.Hand;
			CardsInHand.Add(card);
			UIMain.Instance.RegisterCard(card);
		}

		public void AddCard(List<Card> cards)
		{
			foreach (Card c in cards)
			{
				AddCard(c);
			}
		}

		public void RemoveCard(Card card)
		{
			if (!CardsInHand.Contains(card)) return;

			CardsInHand.Remove(card);
			UIMain.Instance.UnregisterCard(card);
		}

		public List<Card> GetCards()
		{
			return CardsInHand;
		}

		public int GetPositionInHand(Card card)
		{
			int count = 0;

			foreach(Card c in CardsInHand)
			{
				count++;
				if (c == card)
					return count;
			}

			return 0;
		}

	}
}
