using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataStructures
{
	public static class Converters
	{
		public static char SPLIT_DELIMITER = '\t';
		public static char PAIR_DELIMITER = '|';

		public static int ToInt(string s)
		{
			int r = 0;
			Int32.TryParse(s, out r);
			return r;
		}
		public static bool ToBool(string s)
		{
			s = s.ToLower().Trim();
			return s == "true" || s == "1" || s == "yes";
		}
		public static string Join(string[] s)
		{
			string ret = "";

			for (int i = 0; i < s.Length; i++)
			{
				ret += s[i] + PAIR_DELIMITER;
			}
			if (s.Length > 0)
				ret = ret.Substring(0, ret.Length - PAIR_DELIMITER.ToString().Length);

			return ret;
		}
		public static string Join(string[] s, string inter)
		{
			string ret = "";

			for (int i = 0; i < s.Length; i++)
			{
				ret += s[i] + inter;
			}
			if (s.Length > 0)
				ret = ret.Substring(0, ret.Length - inter.Length);

			return ret;
		}
		public static string[] Split(string s)
		{
			return s.Split(SPLIT_DELIMITER);
		}
		public static string DictToString<T, V>(IEnumerable<KeyValuePair<T, V>> items)
		{
			StringBuilder itemString = new StringBuilder();
			foreach (var item in items)
				itemString.AppendFormat("{0}='{1}'" + SPLIT_DELIMITER, item.Key, item.Value);

			return itemString.ToString();
		}
		public static Dictionary<string,string> StringToDict(string input)
		{
			Dictionary<string, string> ret = new Dictionary<string, string>();

			MatchCollection c = Regex.Matches(input, "(.*?)='(.*?)'"+SPLIT_DELIMITER);

			foreach (Match m in c)
			{
				if (m.Groups.Count == 3)
					ret.Add(m.Groups[1].Value, m.Groups[2].Value);
			}

			return ret;
		}
		public static string ObjectsToString(params object[] a)
		{
			try
			{
				if (a == null) return "";

				string s = "";
				for (int i = 0; i < a.Length; i++)
				{
					if (a[i] != null)
						s += a[i].ToString() + PAIR_DELIMITER;
				}
				if (s.Length > 0)
					s = s.Substring(0, s.Length - PAIR_DELIMITER.ToString().Length); //In case PAIR_DELIMITER ever becomes a string

				return s;
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				return "";
			}
		}
		public static string StringToObjects(string s, int index)
		{
			s += PAIR_DELIMITER;

			MatchCollection c = Regex.Matches(s, "(.*?)"+Regex.Escape(PAIR_DELIMITER.ToString()));
			if (index < c.Count)
				return c[index].Groups[0].Value;
			else
				return null;
		}
		public static string SQLiteEscape(string s)
		{
			if (s == null) return "";
			return s.Replace("'", "''");
		}
	}
}
