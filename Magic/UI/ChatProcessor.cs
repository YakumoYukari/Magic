using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
	public class ChatProcessor
	{
		public static ChatProcessor Instance;

		public ChatProcessor()
		{
			ChatProcessor.Instance = this;
		}

		//Enable emoticons, swear filtering, links, etc...
		public string ProcessText(string t)
		{
			return t;
		}
	}
}
