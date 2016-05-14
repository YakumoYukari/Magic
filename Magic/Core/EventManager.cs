using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public static class EventManager
	{
		public delegate void EventHandler(object Sender, EventArgs e);

		public static Dictionary<Events, EventHandler> EventDict = new Dictionary<Events, EventHandler>();

		public static void RegisterEvent(Events e, EventHandler callback)
		{
			if (!EventDict.ContainsKey(e))
				EventDict.Add(e, callback);
			else
				EventDict[e] += callback;
		}

		public static void UnregisterEvent(Events e, EventHandler callback)
		{
			if (!EventDict.ContainsKey(e)) return;

			EventDict[e] -= callback;
		}

		public static void NotifyEvent(Events e, object sender, EventArgs args)
		{
			if (!EventDict.ContainsKey(e)) return;

			EventDict[e](sender, args);
		}
	}
}
