using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic
{
	public class CardPile
	{
		public Pair<double, double> OffsetAmounts; //Fraction of card width/height to shift so all cards aren't entirely covering each other

		public int MaxPileSize { get; private set; }
		public int PileSize { get { return Cards.Count; } }
		public int RowIndex { get; private set; }
		public int ColumnIndex { get; private set; }
		public bool IsFull { get { return PileSize >= MaxPileSize; } }

		//Sizes given to cards in the pile

		public double TotalWidth;
		public double TotalHeight;

		public List<UICard> Cards;
		public Card PrimaryCard; //Most piles are for duplcates. Occasionally we need equipment/enchantments.

		public CardPile(int size, int row, int column)
		{
			Cards = new List<UICard>();

			OffsetAmounts = new Pair<double, double>(0.1, 0.1);
			RowIndex = row;
			ColumnIndex = column;
			MaxPileSize = size;
		}

		public int AddCard(UICard card)
		{
			if (PileSize >= MaxPileSize)
			{
				Console.WriteLine("Cannot add card to pile: pile is full with capacity: {0}/{1}", PileSize, MaxPileSize);
				return -1;
			}

			if (Cards.Count == 0)
				PrimaryCard = card.TargetCard;

			card.Pile = this;
			card.PileIndex = Cards.Count;

			Cards.Add(card);

			return Cards.Count - 1;
		}

		//For ease of computation we assume all the cards in the pile are the same size
		//Right now I don't plan that they ever will be different sizes on the battlefield
		public Point CardOffsetPos(UICard card)
		{
			if (card.Pile == null) return new Point(0, 0);
			if (card.Pile != this) return card.Pile.CardOffsetPos(card);

			double X = card.PileIndex * card.Width * OffsetAmounts.First;
			double Y = card.PileIndex * card.Height * OffsetAmounts.Second;

			return new Point(X, Y);
		}

		public void RemoveCard(UICard card)
		{
			for (int i = card.PileIndex; i < Cards.Count; i++)
			{
				Cards[i].PileIndex--;
			}

			card.Pile = null;
			card.PileIndex = -1;

			Cards.Remove(card);
		}

		public List<Card> EjectCards()
		{
			List<Card> ret = new List<Card>();
			foreach (UICard card in Cards)
			{
				ret.Add(card.TargetCard);
			}
			Cards.Clear();
			PrimaryCard = null;
			return ret;
		}

		public Point SetSizes(double width, double height)
		{
			TotalWidth = width;
			TotalHeight = height;
			for (int i = 0; i < Cards.Count; i++)
			{
				TotalWidth += Cards.ElementAt(i).TargetCard.Tapped ? OffsetAmounts.Second * height : OffsetAmounts.First * width;
				TotalHeight += Cards.ElementAt(i).TargetCard.Tapped ? OffsetAmounts.First * width : OffsetAmounts.Second * height;
			}
				
			return new Point(TotalWidth, TotalHeight);
		}

		public void ClearPile()
		{
			Cards.Clear();
		}

	}
}
