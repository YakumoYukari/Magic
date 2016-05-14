using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data.SQLite;
using DataStructures;

namespace MagicDatabaseBuilder
{
    public static class JSONReader
    {
		static SQLiteConnection DatabaseConnection;
		static List<DataStructures.MagicCard> CardList;
		static List<DataStructures.MagicSet> SetList;

		public static void PrintSets()
		{
			ReadJSONSetsFile("AllSetsArray-x.json");

			Console.WriteLine("Dictionary<string,string> SetCodeTranslationTable = new Dictionary<string,string>();");
			foreach(MagicSet set in SetList)
			{
				Console.WriteLine("SetCodeTranslationTable.Add(\"" + set.Name + "\",\"" + set.Code + "\");");
			}
		}

		public static void ProcessSetsFile(string Filename)
		{
			InitDatabaseConnection();
			CreateSetDatabase();
			ReadJSONSetsFile(Filename);
			WriteMagicSets();
			CloseDatabaseConnection();
		}
		public static void ProcessCardsFile(string Filename)
		{
			InitDatabaseConnection();
			ReadJSONCardsFile(Filename);
			WriteMagicCards();
			CloseDatabaseConnection();
		}
		
		private static void CreateCardDatabase()
		{
			SQLiteCommand Command;
			try
			{
				SQLiteConnection.CreateFile("MagicCardDB.sqlite");
				Command = new SQLiteCommand("drop table Cards", DatabaseConnection);
				Command.ExecuteNonQuery();

				string sql = "create table Cards (Name varchar(1),Names varchar(1),ManaCost varchar(1),CMC int,Colors varchar(1),Type varchar(1),Supertypes varchar(1),Types varchar(1),Subtypes varchar(1),Rarity varchar(1),Text varchar(1),Flavor varchar(1),Artist varchar(1),Number varchar(1),Power varchar(1),Toughness varchar(1),Loyalty varchar(1),Layout varchar(1),MultiverseID int,Variations varchar(1),ImageName varchar(1),Watermark varchar(1),Border varchar(1),Timeshifted varchar(1),HandSizeModifier int,LifeModifier int,Reserved varchar(1),ReleaseDate varchar(1),Rulings varchar(1),ForeignNames varchar(1),Printings varchar(1),OriginalText varchar(1),OriginalType varchar(1),Legalities varchar(1),Source varchar(1))";
				Command = new SQLiteCommand(sql, DatabaseConnection);
				Command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		private static void CreateSetDatabase()
		{
			SQLiteCommand Command;
			try
			{
				//Command = new SQLiteCommand("drop table Sets", DatabaseConnection);
				//Command.ExecuteNonQuery();

				string sql = "create table Sets (Name varchar(1),Code varchar(1),GathererCode varchar(1),OldCode varchar(1),ReleaseDate varchar(1),Border varchar(1),Type varchar(1),Block varchar(1),OnlineOnly varchar(1),Booster varchar(1),Cards varchar(1))";
				Command = new SQLiteCommand(sql, DatabaseConnection);
				Command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		private static void ReadJSONCardsFile(string Filename)
		{
			string JSONString;
			string SerializedString;
			DataStructures.MagicCard Card;

			try
			{
				JSONString = File.ReadAllText(Filename);
				var results = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(JSONString, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
				
				CardList = new List<DataStructures.MagicCard>();
				foreach (var a in results.Values)
				{
					try
					{
						SerializedString = JsonConvert.SerializeObject(a);

						Card = JsonConvert.DeserializeObject<DataStructures.MagicCard>(SerializedString, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
						Card.ProcessData();
						CardList.Add(Card);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						continue; //Unhinged has fractional mana costs.
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			
		}
		private static void ReadJSONSetsFile(string Filename)
		{
			string JSONString = File.ReadAllText(Filename);
			string SerializedString;
			DataStructures.MagicSet Set;

			try
			{
				var results = JsonConvert.DeserializeObject<List<dynamic>>(JSONString, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });

				SetList = new List<DataStructures.MagicSet>();
				foreach (var a in results)
				{
					try
					{
						SerializedString = JsonConvert.SerializeObject(a);
						Set = JsonConvert.DeserializeObject<DataStructures.MagicSet>(SerializedString);
						Set.ProcessData();
						SetList.Add(Set);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						continue; //Again, we won't process Unhinged.
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			
		}

		private static void WriteMagicCards()
		{
			int count = 0;

			String CommandString;
			SQLiteCommand Command;

			foreach(DataStructures.MagicCard card in CardList)
			{
				try
				{
					CommandString = String.Format("INSERT INTO Cards (Name,Names,ManaCost,CMC,Colors,Type,Supertypes,Types,Subtypes,Rarity,Text,Flavor,Artist,Number,Power,Toughness,Loyalty,Layout,MultiverseID,Variations,ImageName,Watermark,Border,Timeshifted,HandSizeModifier,LifeModifier,Reserved,ReleaseDate,Rulings,ForeignNames,Printings,OriginalText,OriginalType,Legalities,Source)VALUES('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',{16},'{17}',{18},'{19}','{20}','{21}','{22}','{23}',{24},{25},'{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}')",
									 SQLiteEscape(card.Name), ArrayToString(card.Names), SQLiteEscape(card.ManaCost), card.CMC, ArrayToString(card.Colors), SQLiteEscape(card.Type), ArrayToString(card.Supertypes), ArrayToString(card.Types),
									 ArrayToString(card.Subtypes), SQLiteEscape(card.Rarity), SQLiteEscape(card.Text), SQLiteEscape(card.Flavor), SQLiteEscape(card.Artist), SQLiteEscape(card.Number), SQLiteEscape(card.Power), SQLiteEscape(card.Toughness), card.Loyalty, SQLiteEscape(card.Layout), card.MultiverseID,
									 ArrayToString(card.Variations), SQLiteEscape(card.ImageName), SQLiteEscape(card.Watermark), SQLiteEscape(card.Border), SQLiteEscape(card.Timeshifted.ToString().ToLower()), card.HandSizeModifier, card.LifeModifier, SQLiteEscape(card.Reserved.ToString().ToLower()), SQLiteEscape(card.ReleaseDate),
									 SQLiteEscape(card.RulingsString), SQLiteEscape(card.ForeignNamesString), ArrayToString(card.Printings), SQLiteEscape(card.OriginalText), SQLiteEscape(card.OriginalType), SQLiteEscape(card.LegalitiesString), SQLiteEscape(card.Source));

					Command = new SQLiteCommand(CommandString, DatabaseConnection);

					Command.ExecuteNonQuery();
					if (++count % 20 == 0)
					{
						Console.WriteLine("Progress {0}/{1}: {2}", count, CardList.Count, card.Name);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
		private static void WriteMagicSets()
		{
			int count = 0;

			String CommandString;
			SQLiteCommand Command;

			foreach (DataStructures.MagicSet set in SetList)
			{
				try
				{
					CommandString = String.Format("INSERT INTO Sets (Name,Code,GathererCode,OldCode,ReleaseDate,Border,Type,Block,OnlineOnly,Booster,Cards)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
									 SQLiteEscape(set.Name),SQLiteEscape(set.Code),SQLiteEscape(set.GathererCode),SQLiteEscape(set.OldCode),SQLiteEscape(set.ReleaseDate),SQLiteEscape(set.Border),SQLiteEscape(set.Type),
									 SQLiteEscape(set.Block),SQLiteEscape(set.OnlineOnly.ToString().ToLower()),SQLiteEscape(set.BoosterString),ArrayToString(set.CardNames));

					Command = new SQLiteCommand(CommandString, DatabaseConnection);

					Command.ExecuteNonQuery();
					if (++count % 20 == 0)
					{
						Console.WriteLine("Progress {0}/{1}: {2}", count, SetList.Count, set.Name);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private static void InitDatabaseConnection()
		{
			DatabaseConnection = new SQLiteConnection("Data Source=MagicCardDB.sqlite;Version=3");
			//SQLiteCommand Command;

			try
			{
				DatabaseConnection.Open();

				//Command = new SQLiteCommand("drop table Cards", DatabaseConnection);
				//Command.ExecuteNonQuery();

				//string sql = "create table Cards (Name varchar(1),Names varchar(1),ManaCost varchar(1),CMC int,Colors varchar(1),Type varchar(1),Supertypes varchar(1),Types varchar(1),Subtypes varchar(1),Rarity varchar(1),Text varchar(1),Flavor varchar(1),Artist varchar(1),Number varchar(1),Power varchar(1),Toughness varchar(1),Loyalty varchar(1),Layout varchar(1),MultiverseID int,Variations varchar(1),ImageName varchar(1),Watermark varchar(1),Border varchar(1),Timeshifted varchar(1),HandSizeModifier int,LifeModifier int,Reserved varchar(1),ReleaseDate varchar(1),Rulings varchar(1),ForeignNames varchar(1),Printings varchar(1),OriginalText varchar(1),OriginalType varchar(1),Legalities varchar(1),Source varchar(1))";
				//Command = new SQLiteCommand(sql, DatabaseConnection);
				//Command.ExecuteNonQuery();
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

		private static string SQLiteEscape(string s)
		{
			if (s == null) return "";
			return s.Replace("'", "''");
		}
		private static string ArrayToString(string[] arr)
		{
			if (arr == null || arr.Length == 0) return "";
			string s = "";

			foreach (string i in arr)
			{
				s += i + Converters.SPLIT_DELIMITER;
			}
			if (s.Length > 0)
				s = s.Substring(0, s.Length - 1);

			return SQLiteEscape(s);
		}
    }
}
