using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class TurnManager
	{
		public static TurnManager Instance;

		Player CurrentPlayer;
		Stack<Player> ExtraTurnStack;

		public TurnManager()
		{
			ExtraTurnStack = new Stack<Player>();
			TurnManager.Instance = this;
		}

		public void FirstTurn()
		{

		}

		public void AddExtraTurn(Player p, int turns = 1)
		{
			for (int i = 0; i < turns; i++)
			{
				ExtraTurnStack.Push(p);
			}
		}

		public Player NextTurn()
		{
			if (ExtraTurnStack.Count > 0)
			{
				Player Next = ExtraTurnStack.Pop();
				CurrentPlayer = Next;
				return Next;
			}
			else
			{
				Player Next = PlayerManager.Instance.GetNextPlayer(CurrentPlayer);
				CurrentPlayer = Next;
				return Next;
			}
		}
	}
}
