using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artefact;
using Artefact.Animation;
using Artefact.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace Magic
{
	public partial class CommandProcessor
	{

		public void RegisterChatCommands()
		{
			RegisterCommand("help", HelpCommand, "Args({Search}) - Lists help info for found commands");
			RegisterCommand("fetchvalue", FetchValue, "Gets whatever value programmed into this function");
			RegisterCommand("autocomplete", ToggleAutocomplete, "Toggles command processor autocomplete");
			RegisterCommand("addcardhand", AddCardToHand, "Args(Card Name) - Adds the named card to hand");
			RegisterCommand("addcardbattlefield", AddCardToBattlefield, "Args(Card Name) - Adds the named card to the player's battlefield");
			RegisterCommand("setrightcol", SetRightColumnSize, "Args(Width) - Sets the width of the righthand column");
			RegisterCommand("drawcards", DrawCards, "Args(number) - Draws a number of cards from the player's deck");
			RegisterCommand("shuffle", ShuffleLibrary, "Shuffles the player's library");
		}

		CommandResult HelpCommand(string[] args)
		{
			string val = "";
			int found = 0;

			if (args.Length == 1)
				foreach (string key in CommandDict.Keys)
				{
					val += key + ": " + CommandDict[key].Second + "\r\n";
					found++;
				}	
			else
				foreach (string key in CommandDict.Keys)
					if (key.Contains(args[1]))
					{
						val += key + ": " + CommandDict[key].Second + "\r\n";
						found++;
					}

			if (val.Length > 0)
				val = val.Substring(0, val.Length - 2); //Cut off the last \r\n

			val = "Found " + found + " results:\r\n" + val;

			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = val };
		}

		CommandResult FetchValue(string[] args)
		{
			string s = "";
			foreach (UIElement e in MainWindow.Instance.PlayerBattlefieldGrid.Children)
			{
				Image a = (Image)e;
				s += String.Format("({0},{1}), ", a.Margin.Left, a.Margin.Top);
			}
			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Value: " + s };
		}

		CommandResult ToggleAutocomplete(string[] args)
		{
			Autocomplete = !Autocomplete;
			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Autocomplete set to: " + Autocomplete };
		}

		CommandResult AddCardToHand(string[] args)
		{
			if (args.Length == 1)
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = "Card name required!" };

			string CardName = args[1];

			if (args.Length > 2)
			{
				CardName = "";
				for (int i = 1; i < args.Length; i++ )
				{
					CardName += args[i] + " ";
				}
				CardName = CardName.Trim();
			}

			Card c = new Card(CardName);

			if (!c.IsValid)
			{
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = CardName + " is not a valid card!" };
			}

			PlayerManager.Instance.PlayerOne.Hand.AddCard(c);

			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Added card " + CardName + " to hand." };
		}

		CommandResult AddCardToBattlefield(string[] args)
		{
			if (args.Length == 1)
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = "Card name required!" };

			string CardName = args[1];

			if (args.Length > 2)
			{
				CardName = "";
				for (int i = 1; i < args.Length; i++)
				{
					CardName += args[i] + " ";
				}
				CardName = CardName.Trim();
			}

			Card c = new Card(CardName);

			if (!c.IsValid)
			{
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = CardName + " is not a valid card!" };
			}

			PlayerManager.Instance.PlayerOne.Battlefield.AddCard(c);

			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Added card " + CardName + " to battlefield." };
		}

		CommandResult SetRightColumnSize(string[] args)
		{
			if (args.Length < 2)
			{
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = "Invalid arg count." };
			}

			double val = 0.0d;
		
			if (!double.TryParse(args[1], out val))
			{
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = "Cannot parse " + args[1] + " as double." };
			}

			double LeftColSize = MainWindow.Instance.MainGrid.ColumnDefinitions[0].ActualWidth;

			MainWindow.Instance.MainGrid.ColumnDefinitions[2].Width = new System.Windows.GridLength(val);

			MainWindow.Instance.MainGrid.ColumnDefinitions[0].Width = new System.Windows.GridLength(LeftColSize);

			UIMain.Instance.Update();

			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Set right margin size to: " + val };
		}

		CommandResult DrawCards(string[] args)
		{
			if (args.Length < 2)
			{
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = "Invalid arg count." };
			}

			int val = 0;

			if (!int.TryParse(args[1], out val))
			{
				return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Failure, Value = "Cannot parse " + args[1] + " as int." };
			}

			PlayerManager.Instance.PlayerOne.DrawCard(val);

			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Set right margin size to: " + val };
		}

		CommandResult ShuffleLibrary(string[] args)
		{
			PlayerManager.Instance.PlayerOne.ShuffleLibrary();

			return new CommandResult() { ReturnType = CommandResult.ReturnTypes.Success, Value = "Player one's library shuffled." };
		}

	}
}
