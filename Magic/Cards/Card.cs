using MagicDatabaseBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Magic
{
	public class Card
	{
		public static Dictionary<string, Pair<MagicCard, string>> LoadedCards = new Dictionary<string, Pair<MagicCard, string>>();
		public static Dictionary<string, BitmapImage> LoadedImages = new Dictionary<string, BitmapImage>();

		public bool IsValid { get; private set; }
		private MagicCard Data;
		public string ImagePath { get; private set; }
		public UICard UI;

		#region Members

		//Data members

		public string Name { get; private set; }
		public string Set { get; private set; }
		public string SetCode { get; private set; }
		public string Text { get; private set; }
		public int ConvertedCost { get; private set; }
		public string ManaCost { get; private set; }
		public string Type { get; private set; }
		public string[] Types { get; private set; }

		public string[] Printings { get; private set; }

		//Game members

		public Zones CurrentZone;
		public Players CurrentPlayer;

		public bool IsToken;
		public bool Tapped { get; private set; }

		#endregion

		public Card(string name)
		{
			SetCode = GetSetCodes(name)[0];

			LoadData(name, SetCode);

			if (Data != null && !String.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
			{
				SetFields(Data);
				IsValid = true;
			}
			CreateUI();
		}
		public Card(string name, string setcode)
		{
			string code = Translator.GetSetCode(setcode);
			if (!String.IsNullOrEmpty(code)) setcode = code;

			SetCode = setcode;

			LoadData(name, SetCode);

			if (Data != null && !String.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
			{
				SetCode = setcode;
				SetFields(Data);
				IsValid = true;
			}
			CreateUI();
		}
		public void LoadData(string name, string setcode)
		{
			if (!LoadedCards.ContainsKey(name))
			{
				Data = CardData.GetCard(name);
				ImagePath = ImageData.GetCardImagePath(name, SetCode);
				LoadedCards.Add(name, new Pair<MagicCard, string>(Data, ImagePath));
			}
			else
			{
				Pair<MagicCard, string> pair = LoadedCards[name];
				Data = pair.First;
				ImagePath = pair.Second;
			}
		}
		public UICard CreateUI()
		{
			if (String.IsNullOrEmpty(ImagePath))
			{
				throw new Exception("The ImagePath for this card is invalid: " + ImagePath);
			}

			UICard c = new UICard(this);
			if (!LoadedImages.ContainsKey(ImagePath))
			{
				BitmapImage card = new BitmapImage(new Uri(ImagePath));
				c.Source = card;
				LoadedImages.Add(ImagePath, card);
			}
			else
			{
				c.Source = LoadedImages[ImagePath];
			}
			c.TargetCard = this;
			UI = c;
			return c;
		}

		private void SetFields(MagicCard card)
		{
			Name = card.Name;
			Text = card.Text;
			ConvertedCost = card.CMC;
			ManaCost = card.ManaCost;
			Type = card.Type;
			Types = card.Types;

			Printings = card.Printings;
		}

		public static string[] GetSetCodes(string cardname)
		{
			MagicCard c = CardData.GetCard(cardname);
			return Translator.ToCodeArray(c.Printings);
		}

		#region Modifier Methods

		public void Tap()
		{
			Tapped = true;
			UITweens.TapCard(this);
			UIMain.Instance.Update();
		}

		public void Untap()
		{
			Tapped = false;
			UITweens.UntapCard(this);
			UIMain.Instance.Update();
		}

		#endregion

		#region Accessor Methods

		public bool IsType(CardTypes type)
		{
			string cmp = type.ToString();

			if (Types.Contains(cmp))
				return true;

			return false;
		}

		#endregion
	}
}
