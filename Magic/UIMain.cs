using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using DataStructures;
using System.IO;
using System.Net.Cache;

namespace Magic
{
	public class UIMain
	{
		public static UIMain Instance;

		public MainWindow Main;

		List<UICard> UICards;
		string CurrentInspectorImage;

		public UIMain()
		{
			Initialize();
		}

		public UIMain(MainWindow hWnd)
		{
			Main = hWnd;
			Initialize();
		}

		public void Initialize()
		{
			UICards = new List<UICard>();
			UIMain.Instance = this;
		}

		public void Update()
		{
			OnResize();
		}

		public void OnResize()
		{
			RenderCards();
			ResizeCardInspector();
		}

		public void SetCardInspector(Card c)
		{
			SetCardInspectorImage(c.ImagePath);
			ResizeCardInspector();
		}
		public void SetCardInspectorImage(string src)
		{
			BitmapImage _image = new BitmapImage();
			_image.BeginInit();
			_image.CacheOption = BitmapCacheOption.None;
			_image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
			_image.CacheOption = BitmapCacheOption.OnLoad;
			_image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
			_image.UriSource = new Uri(src);
			_image.EndInit();
			Main.CardDisplayImage.Source = _image;

			CurrentInspectorImage = src;
		}
		public void ResizeCardInspector()
		{
			Thickness m = Main.CardDisplayImage.Margin;
			m.Bottom = Main.MainGrid.ActualHeight - Main.MainGrid.ColumnDefinitions[0].ActualWidth * 17 / 12;
			Main.CardDisplayImage.Margin = m;
		}

		public void RegisterCard(Card card)
		{
			if (card == null || !card.IsValid) return;

			UICard c = card.UI;

			if (UICards.Contains(c)) UnregisterCard(c.TargetCard);

			switch (card.CurrentZone)
			{
				case Zones.Battlefield:
					//Sort into groups and front/second row
					if (card.CurrentPlayer == Players.PlayerOne)
						Main.PlayerBattlefield.Children.Add(c);
					if (card.CurrentPlayer == Players.PlayerTwo)
						Main.OpponentBattlefield.Children.Add(c);
					break;
				case Zones.Command:
					//Only one card, draw if Command zone is drawn
					break;
				case Zones.Exile:
					//Pile area, draw if zone is drawn
					break;
				case Zones.Graveyard:
					//Pile area, draw if zone is drawn
					break;
				case Zones.Hand:
					//Always drawn, separate as much as possible
					Main.Hand.Children.Add(c);
					break;
				case Zones.Library:
					//Don't draw
					break;
				case Zones.Stack:
					//Draw in stack view
					break;
				default:
					break;
			}

			UICards.Add(c);
			RenderCards();
		}

		public void UnregisterCard(Card card)
		{
			UICard target = card.UI;
			if (target == null) return;
			
			switch (card.CurrentZone)
			{
				case Zones.Battlefield:
					//Sort into groups and front/second row
					if (card.CurrentPlayer == Players.PlayerOne)
						Main.PlayerBattlefield.Children.Remove(target);
					if (card.CurrentPlayer == Players.PlayerTwo)
						Main.OpponentBattlefield.Children.Remove(target);
					break;
				case Zones.Command:
					//Only one card, draw if Command zone is drawn
					break;
				case Zones.Exile:
					//Pile area, draw if zone is drawn
					break;
				case Zones.Graveyard:
					//Pile area, draw if zone is drawn
					break;
				case Zones.Hand:
					//Always drawn, separate as much as possible
					Main.Hand.Children.Remove(target);
					break;
				case Zones.Library:
					//Don't draw
					break;
				case Zones.Stack:
					//Draw in stack view
					break;
				default:
					break;
			}
			UICards.Remove(target);
			RenderCards();
		}

		public UICard FetchUICard(Card card)
		{
			foreach (UICard ui in UICards)
			{
				if (ui.TargetCard == card)
					return ui;
			}
			return null;
		}

		public void RenderCards()
		{
			//Resize the hand field properly if we don't have any cards
			if (Main.Hand.ActualWidth < Main.HandBorder.ActualWidth)
				Main.Hand.Width = Main.HandBorder.ActualWidth;

			foreach (UICard card in UICards)
			{
				switch (card.TargetCard.CurrentZone)
				{
					case Zones.Battlefield:
						//Sort into groups and front/second row
						RenderCardsOnBattlefield(card);
						break;
					case Zones.Command:
						//Only one card, draw if Command zone is drawn
						break;
					case Zones.Exile:
						//Pile area, draw if zone is drawn
						break;
					case Zones.Graveyard:
						//Pile area, draw if zone is drawn
						break;
					case Zones.Hand:
						//Always drawn, separate as much as possible
						RenderCardsInHand(card);
						break;
					case Zones.Library:
						//Don't draw
						break;
					case Zones.Stack:
						//Draw in stack view
						break;
					default:
						break;
				}
			}
		}

		public void RenderCardsOnBattlefield(UICard card)
		{
			if (card.Pile == null) return;

			Card data = card.TargetCard;

			if (data.CurrentPlayer == Players.PlayerOne)
			{
				card.Scale(Main.PlayerBattlefieldGrid.ActualHeight / 2 * 5.0/8.0);

				Point pos = CardPileManager.Instance.GetCardPos(card);

				card.Pile.SetSizes(card.Width, card.Height);

				double X = CardPileManager.Instance.GetHorizontalPosition(card.Pile) + card.Pile.CardOffsetPos(card).X + 20.0d;
				double Y = card.Pile.RowIndex * Main.PlayerBattlefield.ActualHeight / 2 + pos.Y + card.Pile.CardOffsetPos(card).Y + 20.0d;

				card.SetPos(X, Y);
			}
			else if (data.CurrentPlayer == Players.PlayerTwo)
			{

			}
		}

		public void RenderCardsInHand(UICard card)
		{
			//This is only drawn for player one
			if (card.TargetCard.CurrentPlayer == Players.PlayerOne)
			{
				double buf = ClientConstants.UIConstants.CardsInHandPadding;

				card.Scale(Main.HandBorder.ActualHeight);

				int CardsInHand = GameManager.Instance.PlayerMgr[card.TargetCard.CurrentPlayer].HandSize;
				double Spacing = Mathd.Clamp((Main.HandBorder.ActualWidth - (card.Width)) / (CardsInHand - 1), card.Width / 2, card.Width + buf);

				Main.Hand.Width = Mathd.Clamp(Spacing * (CardsInHand-1) + card.Width, Main.HandBorder.ActualWidth, double.MaxValue);


				card.SetPos((GameManager.Instance.PlayerMgr[card.TargetCard.CurrentPlayer].Hand.GetPositionInHand(card.TargetCard) - 1) * Spacing, 0);
			}
		}

	}
}
