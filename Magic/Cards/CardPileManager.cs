using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic
{
	public class CardPileManager
	{
		public static CardPileManager Instance;

		private List<CardPile> Piles;

		private Dictionary<int, int> RowCounts;

		public CardPileManager()
		{
			Piles = new List<CardPile>();
			RowCounts = new Dictionary<int, int>();

			CardPileManager.Instance = this;
		}

		public CardPile AddCardPile(int size, int row)
		{
			CardPile pile = new CardPile(size, row, GetRowCount(row));

			Piles.Add(pile);
			IncrementRowCount(row);

			return pile;
		}

		public void RemoveCardPile(CardPile pile)
		{
			DecrementRowCount(pile.RowIndex);
			Piles.Remove(pile);
		}

		public bool TryFindPile(Card card, out CardPile pile)
		{
			foreach (CardPile p in Piles)
			{
				if ((p.PrimaryCard == null || p.PrimaryCard.Name == card.Name) && !p.IsFull)
				{
					pile = p;
					return true;
				}
			}
			pile = null;
			return false;
		}

		public void ClearPiles()
		{
			Piles.Clear();
			RowCounts.Clear();
		}

		public int GetRowCount(int row)
		{
			if (RowCounts.ContainsKey(row))
				return RowCounts[row];
			else return 0;
		}

		public List<CardPile> GetPilesInRow(int row)
		{
			List<CardPile> o = new List<CardPile>();
			foreach (CardPile p in Piles)
			{
				if (p.RowIndex == row)
					o.Add(p);
			}
			return o;
		}

		public double GetHorizontalPosition(CardPile p)
		{
			List<CardPile> others = GetPilesInRow(p.RowIndex);

			double d = 0.0d;

			foreach (CardPile pile in others)
			{
				if (pile.ColumnIndex < p.ColumnIndex)
					d += pile.TotalWidth + ClientConstants.UIConstants.BattlefieldPileSpacing;
			}

			return d;
		}

		public void IncrementRowCount(int row)
		{
			if (!RowCounts.ContainsKey(row))
				RowCounts.Add(row, 0);
			RowCounts[row]++;
		}

		public void DecrementRowCount(int row)
		{
			if (!RowCounts.ContainsKey(row))
				return;
			RowCounts[row]--;
		}

		public Point GetCardPos(UICard c)
		{
			double X = c.Pile.ColumnIndex;
			double Y = c.Pile.RowIndex;

			return new Point(X, Y);
		}
	}
}
