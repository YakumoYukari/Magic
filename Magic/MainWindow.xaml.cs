
#define DEBUG_MODE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MagicDatabaseBuilder;
using DataStructures;
using System.Windows;
using System.Windows.Media.Imaging;
using Artefact;
using Artefact.Animation;
using Artefact.Utilities;
using System.Windows.Input;

namespace Magic
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static MainWindow Instance;

		public GameManager Game;
		public bool Ready = false;

		private bool AutoScroll = true;

		public MainWindow()
		{
			InitializeComponent();
			Instance = this;
		}

		public void Init()
		{
			Game = new GameManager(this);
			Game.Test();
			Ready = true;
		}

		#region Events
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (Ready)
			{
				#if DEBUG_MODE
					UIMain.Instance.OnResize();
				#else
					try
					{
						UIMain.Instance.OnResize();
					}

					catch (Exception ex)
					{
						DebugLogger.Log(ex.ToString());
						throw ex;
					}
				#endif
			}
		}
		private void CardDisplayCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{

		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			#if DEBUG_MODE
				Init();
			#else
				try
				{
					Init();
				}
				catch (Exception ex)
				{
					DebugLogger.Log(ex.ToString());
					throw ex;
				}
			#endif

			//MagicCard Counterspell = CardData.GetCard("Counterspell");
			//TextBlock.Text = Counterspell.Text;

			//MagicSet Commander2014 = SetData.GetSetByName("Commander 2014");
			//if (Commander2014 != null)
			//	TextBlock.Text = Commander2014.ReleaseDate;

			//MAGIC CARD DIM: 480x680, RATIO = 12/17

			//CardDisplayImage.AlphaTo(.5, 3.0, AnimationTransitions.CubicEaseOut, 0.0); //GLORY BE IT FUCKING WORKS!
			//CardDisplayImage.XTo(50, 2.0, AnimationTransitions.QuadEaseOut, 0.0);

			//JSONReader.ProcessSetsFile("AllSetsArray-x.json");
			//JSONReader.ProcessCardsFile("AllCards-x.json");
		}
		private void CommandBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			try
			{
				EventManager.NotifyEvent(Events.UI_CHATBOX_ONKEY, CommandBox, new UIEventArgsChatBox() { Text = CommandBox.Text, Sender = PlayerManager.Instance.GetPlayer(Players.PlayerOne), LastKey = e.Key });
				if (e.Key == Key.Enter)
				{
					EventManager.NotifyEvent(Events.UI_CHATBOX_SUBMIT, CommandBox, new UIEventArgsChatBox() { Text = CommandBox.Text, Sender = PlayerManager.Instance.GetPlayer(Players.PlayerOne), LastKey = e.Key });
				}
			}

			catch (Exception ex)
			{
				DebugLogger.Log(ex.ToString());
				throw ex;
			}
		}
		private void LogBoxScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
		{
			if (e.ExtentHeightChange == 0)
			{
				if (LogBoxScrollViewer.VerticalOffset == LogBoxScrollViewer.ScrollableHeight)
				{
					AutoScroll = true;
				}
				else
				{
					AutoScroll = false;
				}
			}

			if (AutoScroll && e.ExtentHeightChange != 0)
			{
				LogBoxScrollViewer.ScrollToVerticalOffset(LogBoxScrollViewer.ExtentHeight);
			}
		}
		private void CommandBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (e.Changes.ElementAt(0).AddedLength > 0)
				CommandProcessor.Instance.OnTextChanged(CommandBox.Text);
		}
		private void ChatSplitter_SizeChanged(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
			if (Ready)
			{
				try
				{
					UIMain.Instance.OnResize();
				}

				catch (Exception ex)
				{
					DebugLogger.Log(ex.ToString());
					throw ex;
				}
			}
		}
		#endregion
	}
}
