using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Deck
	{
		public List<Card> Cards;
		public int DeckSize { get { return Cards.Count; } }

		public Deck(int size)
		{
			Cards = new List<Card>(size);
		}

		public void AddCard(Card c)
		{
			Cards.Add(c);
		}

		public void AddCard(string name, int quantity = 1)
		{
			for (int i = 0; i < quantity; i++)
			{
				Card c = new Card(name);
				if (c.IsValid)
					Cards.Add(c);
			}
		}
	}
}
