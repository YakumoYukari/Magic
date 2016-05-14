using DataStructures;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDatabaseBuilder
{
	public static class SetData
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

		public static DataStructures.MagicSet GetSetByName(string name)
		{
			return LoadCardFromDictionary(GetSetDBRowByName(name));
		}
		public static DataStructures.MagicSet GetSetByCode(string code)
		{
			return LoadCardFromDictionary(GetSetDBRowByCode(code));
		}

		private static Dictionary<string, string> GetSetDBRowByName(string name)
		{
			Dictionary<string, string> ResultDict = new Dictionary<string, string>();
			try
			{
				InitDatabaseConnection();

				SQLiteCommand c = new SQLiteCommand("SELECT * FROM Sets WHERE Name='" + name + "'", DatabaseConnection);
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
		private static Dictionary<string, string> GetSetDBRowByCode(string code)
		{
			Dictionary<string, string> ResultDict = new Dictionary<string, string>();
			try
			{
				InitDatabaseConnection();

				SQLiteCommand c = new SQLiteCommand("SELECT * FROM Sets WHERE Code='" + code + "'", DatabaseConnection);
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
		private static DataStructures.MagicSet LoadCardFromDictionary(Dictionary<string, string> dict)
		{
			if (dict == null || dict.Count == 0) return null;

			DataStructures.MagicSet s = new DataStructures.MagicSet();

			s.Name = dict["Name"];
			s.Code = dict["Code"];
			s.GathererCode = dict["GathererCode"];
			s.OldCode = dict["OldCode"];
			s.ReleaseDate = dict["ReleaseDate"];
			s.Border = dict["Border"];
			s.Type = dict["Type"];
			s.Block = dict["Block"];
			s.OnlineOnly = Converters.ToBool(dict["OnlineOnly"]);
			s.BoosterString = dict["Booster"];
			s.CardNames = Converters.Split(dict["Cards"]);

			return s;
		}
	}
}
