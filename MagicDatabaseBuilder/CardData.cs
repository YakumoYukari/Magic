using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using System.Data.SQLite;

namespace MagicDatabaseBuilder
{
    public static class CardData
    {
		static SQLiteConnection DatabaseConnection;

		private static void InitDatabaseConnection()
		{

			DatabaseConnection = new SQLiteConnection("Data Source=MagicCardDB.sqlite;Version=3");
			try
			{
				DatabaseConnection.Open();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error in InitDatabaseConnection!");
				Console.WriteLine(e.ToString());
			}
		}
		private static void CloseDatabaseConnection()
		{
			try
			{
				DatabaseConnection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error in CloseDatabaseConnection!");
				Console.WriteLine(e.ToString());
			}
		}

		public static DataStructures.MagicCard GetCard(string name)
		{
			name = Converters.SQLiteEscape(name);
			return LoadCardFromDictionary(GetCardDBRow(name));
		}

		private static Dictionary<string, string> GetCardDBRow(string name)
		{
			Dictionary<string, string> ResultDict = new Dictionary<string, string>();
			try
			{
				InitDatabaseConnection();

				SQLiteCommand c = new SQLiteCommand("SELECT * FROM Cards WHERE Name='" + name + "'", DatabaseConnection);
				SQLiteDataReader reader = c.ExecuteReader();


				reader.Read();

				for (int i = 0; i < reader.FieldCount; i++)
				{
					ResultDict.Add(reader.GetName(i), reader.GetValue(i).ToString());
				}

				c.Dispose();
				reader.Dispose();

				CloseDatabaseConnection();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return ResultDict;
		}
		private static DataStructures.MagicCard LoadCardFromDictionary(Dictionary<string, string> dict)
		{
			if (dict.Count == 0) return null;

			DataStructures.MagicCard c = new DataStructures.MagicCard();

			c.Name = dict["Name"];
			c.Names = Converters.Split(dict["Names"]);
			c.ManaCost = dict["ManaCost"];
			c.CMC = Converters.ToInt(dict["CMC"]);
			c.Colors = Converters.Split(dict["Colors"]);
			c.Type = dict["Type"];
			c.Supertypes = Converters.Split(dict["Supertypes"]);
			c.Types = Converters.Split(dict["Types"]);
			c.Subtypes = Converters.Split(dict["Subtypes"]);
			c.Rarity = dict["Rarity"];
			c.Text = dict["Text"];
			c.Flavor = dict["Flavor"];
			c.Artist = dict["Artist"];
			c.Number = dict["Number"];
			c.Power = dict["Power"];
			c.Toughness = dict["Toughness"];
			c.Loyalty = Converters.ToInt(dict["Loyalty"]);
			c.Layout = dict["Layout"];
			c.MultiverseID = Converters.ToInt(dict["MultiverseID"]);
			c.Variations = Converters.Split(dict["Variations"]);
			c.ImageName = dict["ImageName"];
			c.Watermark = dict["Watermark"];
			c.Border = dict["Border"];
			c.Timeshifted = Converters.ToBool(dict["Timeshifted"]);
			c.HandSizeModifier = Converters.ToInt(dict["HandSizeModifier"]);
			c.LifeModifier = Converters.ToInt(dict["LifeModifier"]);
			c.Reserved = Converters.ToBool(dict["Reserved"]);
			c.ReleaseDate = dict["ReleaseDate"];
			c.RulingsString = dict["Rulings"];
			c.ForeignNamesString = dict["ForeignNames"];
			c.Printings = Converters.Split(dict["Printings"]);
			c.OriginalText = dict["OriginalText"];
			c.OriginalType = dict["OriginalType"];
			c.LegalitiesString = dict["Legalities"];
			c.Source = dict["Source"];

			return c;
		}
    }
}
