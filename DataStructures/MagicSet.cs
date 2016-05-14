using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
	public class MagicSet
	{
		[JsonProperty("name")]
		[DefaultValue("")]
		public string Name;

		[JsonProperty("code")]
		[DefaultValue("")]
		public string Code;

		[JsonProperty("gathererCode")]
		[DefaultValue("")]
		public string GathererCode;

		[JsonProperty("oldCode")]
		[DefaultValue("")]
		public string OldCode;

		[JsonProperty("releaseDate")]
		[DefaultValue("")]
		public string ReleaseDate;

		[JsonProperty("border")]
		[DefaultValue("")]
		public string Border;

		[JsonProperty("type")]
		[DefaultValue("")]
		public string Type;

		[JsonProperty("block")]
		[DefaultValue("")]
		public string Block;

		[JsonProperty("onlineOnly")]
		[DefaultValue("")]
		public bool OnlineOnly;

		[JsonProperty("booster")]
		public object[] Booster;

		public string BoosterString; //After processing the Object Array.

		[JsonProperty("cards")]
		public MagicCard[] Cards;

		public string[] CardNames; //After grabbing cards.

		public void ProcessData()
		{
			if (Booster != null)
				BoosterString = Converters.ObjectsToString(Booster);

			if (Cards != null)
			{
				CardNames = new string[Cards.Length];
				for (int i = 0; i < Cards.Length; i++)
				{
					CardNames[i] = Cards[i].Name;
				}
			}
		}
	}
}
