using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DataStructures;
using System.IO;

namespace MagicDatabaseBuilder
{
	public static class ImageData
	{
		public static string IMAGE_FILEPATH = @"C:\Users\Lukez_000\Desktop\Folders\Magic Resources\Images";

		public static Bitmap GetCardImage(string name, string setcode)
		{
			setcode = setcode.ToUpper();
			MagicCard Card = CardData.GetCard(name);

			string ImageName = Card.ImageName;

			if (setcode == "CON") setcode = "_CON"; //Windows prohibits folders named "CON"
			ImageName = HandleLandCards(name, setcode, 1);

			string path = Path.Combine(IMAGE_FILEPATH, setcode.ToUpper(), ImageName + ".hq.jpg");

			return Bitmap.FromFile(path) as Bitmap;
		}
		public static string GetCardImagePath(string name, string setcode)
		{
			setcode = setcode.ToUpper();
			MagicCard Card = CardData.GetCard(name);

			string ImageName = Card.ImageName;
			
			if (setcode == "CON") setcode = "_CON"; //Windows prohibits folders named "CON"
			ImageName = HandleLandCards(name, setcode, 1);

			string path = Path.Combine(IMAGE_FILEPATH, setcode.ToUpper(), ImageName + ".hq.jpg");

			return path;
		}

		public static int GetLandTypeCount(BasicLandTypes type, string setcode)
		{
			setcode = setcode.ToUpper();
			if (setcode == "CON") setcode = "_CON";

			int count = 0;

			string path = Path.Combine(IMAGE_FILEPATH, setcode);

			string[] Filenames = Directory.GetFiles(path);
			foreach (string s in Filenames)
			{
				string t = Path.GetFileNameWithoutExtension(s);
				if (t.StartsWith(type.ToString().ToLower())) count++;
			}

			return count;
		}
		private static string HandleLandCards(string name, string setcode, int num)
		{
			name = name.ToLower();
			if (name != "forest" && name != "plains" && name != "swamp" && name != "island" && name != "mountain") return name;

			int count = 0;

			string path = Path.Combine(IMAGE_FILEPATH, setcode.ToUpper());
			string[] Filenames = Directory.GetFiles(path);
			foreach (string s in Filenames)
			{
				string t = Path.GetFileNameWithoutExtension(s);
				if (t.StartsWith(name)) count++;
			}

			if (num > count)
				num = count;

			return name + num;
		}
	}
}
