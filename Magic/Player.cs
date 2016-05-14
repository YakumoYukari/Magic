using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class Player
	{
		//Player data
		public string PlayerName;
		public int PlayerID;

		//Game data
		public Deck Deck;
		public Hand Hand;
		public Library Library;
		public Battlefield Battlefield;

		//Attributes
		public int DeckSize { get { return Deck.DeckSize; } }
		public int MaxHandSize = 7;
		public int HandSize {get {return Hand.HandSize;}}
		public int NumPermanents { get { return Battlefield.NumPermanents; } }
		public bool Lost;

		public Player(string name)
		{
			PlayerName = name;
			Hand = new Hand(this);
			Deck = new Deck(60);
			Library = new Library(Deck);
			Battlefield = new Battlefield(this);
		}

		public void PlayCard(Card card)
		{
			if (Hand.GetCards().Contains(card))
				Hand.RemoveCard(card);

			Battlefield.AddCard(card);
		}

		public void DrawCard(int number = 1)
		{
			for (int i = 0; i < number; i++)
			{
				if (Library.CardCount > 0)
					Hand.AddCard(Library.PopTopCard());
				else
				{
					Lost = true;
					break;
				}
			}
		}

		public void ShuffleLibrary()
		{
			Library.Shuffle();
		}
	}
}
