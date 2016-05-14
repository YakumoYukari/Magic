using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public static class DebugLogger
	{
		public static string DEBUG_FILENAME = "Log.txt";

		public static void Log(string t)
		{
			t = "[" + System.DateTime.Now.ToLongTimeString() + "] " + t;
			File.AppendAllText(DEBUG_FILENAME, t);
		}

		public static void DeleteLogFile()
		{
			File.Delete(DEBUG_FILENAME);
		}
	}
}
