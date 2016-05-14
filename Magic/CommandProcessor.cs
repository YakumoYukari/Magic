using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Magic
{
	public partial class CommandProcessor
	{
		public static CommandProcessor Instance;
		
		public delegate CommandResult Command(string[] args);
		public Dictionary<string, Pair<Command, string>> CommandDict;
		public List<string> SortedCommandList;

		public bool Autocomplete = true;

		public CommandProcessor()
		{
			CommandDict = new Dictionary<string, Pair<Command, string>>();
			SortedCommandList = new List<string>();

			RegisterChatCommands();

			CommandProcessor.Instance = this;
		}

		public bool HasCommand(string command)
		{
			return (CommandDict.ContainsKey(command));
		}

		public void RunCommand(string command)
		{
			if (String.IsNullOrEmpty(command)) 
			{
				LogBoxManager.Instance.Log("Null command!");
				return;
			}

			string[] bits = SplitWithQuotes(command);
			if (bits.Length == 0) return;

			if (HasCommand(bits[0]))
			{
				CommandResult result = CommandDict[bits[0]].First(bits);

				if (result.ReturnType == CommandResult.ReturnTypes.Success)
				{
					LogBoxManager.Instance.Log(result.Value, Brushes.Green);
				}
				else if (result.ReturnType == CommandResult.ReturnTypes.Failure)
				{
					LogBoxManager.Instance.Log(result.Value, Brushes.Red);
				}
				else
				{
					LogBoxManager.Instance.Log(result.Value);
				}
			}
			else
			{
				LogBoxManager.Instance.Log("No such command: " + bits[0]);
			}
		}

		public void RegisterCommand(string name, Command command, string help)
		{
			if (HasCommand(name)) return;

			CommandDict.Add(name, new Pair<Command, string>(command, help));
		}

		public void UnregisterCommand(string name)
		{
			if (!HasCommand(name)) return;
			CommandDict.Remove(name);
		}

		private string[] SplitWithQuotes(string s)
		{
			return s.Split('"').Select((element, index) => index % 2 == 0 
										   ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
										   : new string[] { element }).SelectMany(element => element).ToArray<string>();
		}

		public void OnTextChanged(string text)
		{
			if (SortedCommandList.Count == 0)
				SortedCommandList.AddRange(CommandDict.Keys.OrderBy(s => s).ToArray());

			if (Autocomplete && text.Length > 1 && text.StartsWith("/"))
			{
				string t = text.Substring(1);
				int CaretPos = LogBoxManager.Instance.InputBox.CaretIndex;
				foreach(string s in SortedCommandList)
				{
					if (s.StartsWith(t))
					{
						LogBoxManager.Instance.InputBox.Text = "/" + t + s.Substring(t.Length);
						LogBoxManager.Instance.InputBox.CaretIndex = CaretPos;
						LogBoxManager.Instance.InputBox.Select(t.Length + 1, s.Length - t.Length + 1);
					}
						
				}
			}
		}

		public class CommandResult
		{
			public enum ReturnTypes
			{
				Success,
				Failure
			}

			public ReturnTypes ReturnType;
			public string Value;
		}
	}
}
