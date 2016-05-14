using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Magic
{
	public class LogBoxManager
	{
		public static LogBoxManager Instance;

		public TextBox InputBox;
		public TextBlock LogBox;

		private List<string> MyChatHistory;
		private int ChatHistoryIndex;
		public int MaxChatHistory;

		public int LineCount;
		public int MaxLineCount;

		public bool TimestampLogs = true;
		public bool TimestampChats = true;

		public LogBoxManager(TextBlock box, int MaxLines)
		{
			LogBox = box;
			MaxLineCount = MaxLines;

			MyChatHistory = new List<string>();
			ChatHistoryIndex = -1;
			MaxChatHistory = 5;

			RegisterEvents();
			Clear();

			LogBoxManager.Instance = this;
		}

		public void RegisterEvents()
		{
			EventManager.RegisterEvent(Events.UI_CHATBOX_ONKEY, OnChatBoxKey);
			EventManager.RegisterEvent(Events.UI_CHATBOX_SUBMIT, OnChatBoxSubmit);
		}

		public void AddLine(string t, Brush color = null)
		{
			if (color != null)
			{
				LogBox.Inlines.Add(new Run(t + "\r\n") { Foreground = color });
			}
			else
				LogBox.Inlines.Add(new Run(t + "\r\n") { Foreground = Brushes.Black });

			LineCount++;
			if (LineCount > MaxLineCount)
			{
				LogBox.Inlines.Remove(LogBox.Inlines.FirstInline);
				//LogBox.Text = LogBox.Text.Substring(LogBox.Text.IndexOf("\r\n") + 2, LogBox.Text.Length - (LogBox.Text.IndexOf("\r\n") + 2));
			}
		}

		public void Log(string log, Brush color = null)
		{
			if (TimestampLogs)
				AttachTimestamp(ref log);

			AddLine(log, color);
		}

		public void Chat(Players player, string text)
		{
			text = ChatProcessor.Instance.ProcessText(text);

			text = PlayerManager.Instance.GetPlayer(player).PlayerName + ": " + text;

			if (TimestampChats)
				AttachTimestamp(ref text);

			AddLine(text);
		}

		public void Clear()
		{
			LineCount = 0;
			LogBox.Text = "";
		}

		public void ClearInputBox()
		{
			InputBox.Text = "";
		}

		public void AttachTimestamp(ref string s)
		{
			s = "[" + System.DateTime.Now.ToLongTimeString().Replace(" AM", "").Replace(" PM", "") + "] " + s;
		}

		public bool IsCommandInput(string s)
		{
			return s.StartsWith("/");
		}

		public void AddChatHistory(string s)
		{
			if (MyChatHistory.Contains(s))
				MyChatHistory.Remove(s);

			if (MyChatHistory.Count >= MaxChatHistory)
				MyChatHistory.RemoveAt(0);

			MyChatHistory.Add(s);
		}

		#region Events

		public void OnChatBoxKey(object sender, EventArgs args)
		{
			UIEventArgsChatBox e = args as UIEventArgsChatBox;
			TextBox box = sender as TextBox;

			if (e.LastKey == Key.Up)
			{
				if (ChatHistoryIndex == -1)
				{
					ChatHistoryIndex = MyChatHistory.Count;
				}
				
				if (ChatHistoryIndex > 0)
				{
					box.Text = MyChatHistory.ElementAt(--ChatHistoryIndex);
					box.CaretIndex = box.Text.Length;
				}
			}
			if (e.LastKey == Key.Down)
			{
				if (ChatHistoryIndex > -1)
				{
					if (ChatHistoryIndex < MyChatHistory.Count - 1)
					{
						box.Text = MyChatHistory.ElementAt(++ChatHistoryIndex);
						box.CaretIndex = box.Text.Length;
					}
					else if (ChatHistoryIndex == MyChatHistory.Count - 1)
					{
						box.Text = "";
						ChatHistoryIndex = -1;
					}
				}
			}
			if (e.LastKey != Key.Up && e.LastKey != Key.Down)
			{
				ChatHistoryIndex = -1;
			}

		}

		public void OnChatBoxSubmit(object sender, EventArgs args)
		{
			UIEventArgsChatBox e = args as UIEventArgsChatBox;

			e.Text = e.Text.Trim();

			if (String.IsNullOrEmpty(e.Text)) return;

			if (IsCommandInput(e.Text))
			{
				AddChatHistory(e.Text);
				e.Text = e.Text.Substring(1, e.Text.Length - 1);
				CommandProcessor.Instance.RunCommand(e.Text);
			}
			else
			{
				LogBoxManager.Instance.Chat(PlayerManager.Instance.GetPlayer(e.Sender), e.Text);
				AddChatHistory(e.Text);
			}

			ClearInputBox();
			ChatHistoryIndex = -1;
		}

		#endregion
	}
}
