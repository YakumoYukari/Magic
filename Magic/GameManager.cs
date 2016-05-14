using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class GameManager
	{
		public static GameManager Instance;

		public GameLogicManager GameLogicMgr;
		public CardPileManager CardPileMgr;
		public CommandProcessor CommandMgr;
		public PlayerManager PlayerMgr;
		public TurnManager TurnMgr;
		public LogBoxManager LogBoxMgr;
		public ChatProcessor ChatProcessor;
		public UIMain UI;
		public MainWindow Main;

		public GameManager(MainWindow window)
		{
			Main = window;

			GameLogicMgr = new GameLogicManager();
			CardPileMgr = new CardPileManager();
			CommandMgr = new CommandProcessor();
			PlayerMgr = new PlayerManager();
			TurnMgr = new TurnManager();
			UI = new UIMain(window);
			LogBoxMgr = new LogBoxManager(window.LogBox, 500);
			ChatProcessor = new ChatProcessor();

			LogBoxMgr.InputBox = Main.CommandBox;

			GameManager.Instance = this;
		}

		public void Test()
		{
			PlayerMgr.AddPlayer(Players.PlayerOne, new Player("Hot Soup"));
			/*
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Mana Leak"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Counterspell"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Mana Drain"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Mass Polymorph"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Phyrexian Rebirth"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Eight-and-a-Half-Tails"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Reflecting Pool"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("City of Brass"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Bringer of the Black Dawn"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Umezawa's Jitte"));
			PlayerMgr.PlayerOne.Hand.AddCard(new Card("Forbidden Orchard"));
			
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Forest"));
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Forest"));
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Forest"));
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Forest"));
			
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Plains"));
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Plains"));

			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Sensei's Divining Top"));
			PlayerMgr.PlayerOne.Battlefield.AddCard(new Card("Goblin Guide"));
			*/
			#region Decks

			Deck Slivers = new Deck(60);
			Slivers.AddCard("Cavern of Souls", 4);
			Slivers.AddCard("Forest", 2);
			Slivers.AddCard("Gavony Township", 1);
			Slivers.AddCard("Mountain", 4);
			Slivers.AddCard("Plains", 2);
			Slivers.AddCard("Sacred Foundry", 3);
			Slivers.AddCard("Stomping Ground", 2);
			Slivers.AddCard("Temple Garden", 2);

			Slivers.AddCard("Battle Sliver", 3);
			Slivers.AddCard("Bonescythe Sliver", 1);
			Slivers.AddCard("Frontline Medic", 1);
			Slivers.AddCard("Manaweft Sliver", 3);
			Slivers.AddCard("Megantic Sliver", 2);
			Slivers.AddCard("Predatory Sliver", 4);
			Slivers.AddCard("Steelform Sliver", 4);
			Slivers.AddCard("Thorncaster Sliver", 4);

			Slivers.AddCard("Boros Charm", 4);
			Slivers.AddCard("Creeping Renaissance", 2);
			Slivers.AddCard("Door of Destinies", 3);
			Slivers.AddCard("Hive Stirrings", 4);
			Slivers.AddCard("Mizzium Mortars", 4);

			Slivers.AddCard("Garruk, Caller of Beasts", 1);

			#endregion

			PlayerMgr.PlayerOne.Deck = Slivers;
			PlayerMgr.PlayerOne.Library = new Library(Slivers);
			PlayerMgr.PlayerOne.ShuffleLibrary();
		}
	}
}
