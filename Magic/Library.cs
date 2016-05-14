using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Library
	{
		List<Card> Cards;
		public int CardCount { get { return Cards.Count; } }

		public Library(Deck deck)
		{
			Cards = new List<Card>(deck.DeckSize);
			foreach (Card c in deck.Cards)
			{
				Cards.Add(new Card(c.Name));
			}
		}

		public Card PopTopCard()
		{
			Card ret = Cards.ElementAt(0);
			Cards.RemoveAt(0);
			return ret;
		}

		public List<Card> PopTopCards(int count)
		{
			return new List<Card>();
		}

		public void Shuffle()
		{
			ShuffleFunc<Card>(Cards);
		}

		private static void ShuffleFunc<T>(IList<T> list)
		{
			Random rng = new Random();
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}
