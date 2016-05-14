using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class PlayerManager
	{
		public static PlayerManager Instance;

		public int NumPlayers { get { return (PlayerOne != null ? 1 : 0) + (PlayerTwo != null ? 1 : 0) + (PlayerThree != null ? 1 : 0) + (PlayerFour != null ? 1 : 0); } }

		public Player PlayerOne;
		public Player PlayerTwo;
		public Player PlayerThree;
		public Player PlayerFour;

		public PlayerManager()
		{
			PlayerManager.Instance = this;
		}

		public void AddPlayer(Players position, Player player)
		{
			switch (position)
			{
				case Players.PlayerOne:
					PlayerOne = player;
					player.PlayerID = 1;
					break;
				case Players.PlayerTwo:
					PlayerTwo = player;
					player.PlayerID = 2;
					break;
				case Players.PlayerThree:
					PlayerThree = player;
					player.PlayerID = 3;
					break;
				case Players.PlayerFour:
					PlayerFour = player;
					player.PlayerID = 4;
					break;
				default:
					break;
			}
		}

		public Player GetPlayer(Players player)
		{
			switch (player)
			{
				case Players.PlayerOne:
					return PlayerOne;
				case Players.PlayerTwo:
					return PlayerTwo;
				case Players.PlayerThree:
					return PlayerThree;
				case Players.PlayerFour:
					return PlayerFour;
				default:
					return null;
			}
		}
		public Players GetPlayer(Player player)
		{
			if (PlayerOne == player)
				return Players.PlayerOne;
			if (PlayerTwo == player)
				return Players.PlayerTwo;
			if (PlayerThree == player)
				return Players.PlayerThree;
			if (PlayerFour == player)
				return Players.PlayerFour;
			return Players.PlayerOne;
		}

		public Player GetNextPlayer(Players player)
		{
			switch (player)
			{
				case Players.PlayerOne:
					if (PlayerTwo != null) return PlayerTwo;
					else return PlayerOne;
				case Players.PlayerTwo:
					if (PlayerThree != null) return PlayerThree;
					else return PlayerOne;
				case Players.PlayerThree:
					if (PlayerFour != null) return PlayerFour;
					else return PlayerOne;
				case Players.PlayerFour:
					return PlayerOne;
				default:
					return null;
			}
		}
		public Player GetNextPlayer(Player player)
		{
			return GetNextPlayer(GetPlayer(player));
		}

		public int GetHandSize(Players player)
		{
			Player p = GetPlayer(player);
			if (p == null) return 0;

			return p.HandSize;
		}

		public Player this[Players player]
		{
			get
			{
				return GetPlayer(player);
			}
		}
	}
}
