using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DataStructures
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MagicCard
	{
		[JsonProperty("name")]
		[DefaultValue("")]
		public string Name { get; set; }

		[JsonProperty("names")]
		[DefaultValue("")]
		public string[] Names { get; set; }

		[JsonProperty("manaCost")]
		[DefaultValue("")]
		public string ManaCost { get; set; }

		[JsonProperty("cmc")]
		[DefaultValue(0)]
		public int CMC { get; set; }

		[JsonProperty("colors")]
		public string[] Colors { get; set; }

		[JsonProperty("type")]
		[DefaultValue("")]
		public string Type { get; set; }

		[JsonProperty("supertypes")]
		public string[] Supertypes { get; set; }

		[JsonProperty("types")]
		public string[] Types { get; set; }

		[JsonProperty("subtypes")]
		public string[] Subtypes { get; set; }

		[JsonProperty("rarity")]
		[DefaultValue("")]
		public string Rarity { get; set; }

		[JsonProperty("text")]
		[DefaultValue("")]
		public string Text { get; set; }

		[JsonProperty("flavor")]
		[DefaultValue("")]
		public string Flavor { get; set; }

		[JsonProperty("artist")]
		[DefaultValue("")]
		public string Artist { get; set; }

		[JsonProperty("number")]
		[DefaultValue("")]
		public string Number { get; set; }

		[JsonProperty("power")]
		[DefaultValue("")]
		public string Power { get; set; }

		[JsonProperty("toughness")]
		[DefaultValue("")]
		public string Toughness { get; set; }

		[JsonProperty("loyalty")]
		public int Loyalty { get; set; }

		[JsonProperty("layout")]
		[DefaultValue("")]
		public string Layout { get; set; }

		[JsonProperty("multiverseid")]
		public int MultiverseID { get; set; }

		[JsonProperty("variations")]
		public string[] Variations { get; set; }

		[JsonProperty("imageName")]
		[DefaultValue("")]
		public string ImageName { get; set; }

		[JsonProperty("watermark")]
		[DefaultValue("")]
		public string Watermark { get; set; }

		[JsonProperty("border")]
		[DefaultValue("")]
		public string Border { get; set; }

		[JsonProperty("timeshifted")]
		public bool Timeshifted { get; set; }

		[JsonProperty("hand")]
		public int HandSizeModifier { get; set; }

		[JsonProperty("life")]
		public int LifeModifier { get; set; }

		[JsonProperty("reserved")]
		public bool Reserved { get; set; }

		[JsonProperty("releasedate")]
		[DefaultValue("")]
		public string ReleaseDate { get; set; }

		[JsonProperty("rulings")]
		[DefaultValue("")]
		public Rulings[] Rulings { get; set; }

		public string RulingsString;

		[JsonProperty("foreignNames")]
		[DefaultValue("")]
		public ForeignNames[] ForeignNames { get; set; }

		public string ForeignNamesString;

		[JsonProperty("printings")]
		[DefaultValue("")]
		public string[] Printings { get; set; }

		[JsonProperty("originalText")]
		[DefaultValue("")]
		public string OriginalText { get; set; }

		[JsonProperty("originalType")]
		[DefaultValue("")]
		public string OriginalType { get; set; }

		[JsonProperty("legalities")]
		[DefaultValue("")]
		public Dictionary<string,string> Legalities { get; set; }

		public string LegalitiesString;

		[JsonProperty("source")]
		[DefaultValue("")]
		public string Source { get; set; }

		public void ProcessData()
		{
			if (Rulings != null)
				foreach (Rulings r in Rulings)
				{
					RulingsString = Converters.ObjectsToString(RulingsString, r.ToString());
				}

			if (ForeignNames != null)
				foreach (ForeignNames n in ForeignNames)
				{
					ForeignNamesString = Converters.ObjectsToString(ForeignNamesString, n.ToString());
				}

			if (Legalities != null)
				LegalitiesString = Converters.DictToString(Legalities);

		}

	}

	public class Rulings
	{
		[JsonProperty("date")]
		[DefaultValue("")]
		string Date;

		[JsonProperty("text")]
		[DefaultValue("")]
		string Ruling;

		public string ToString()
		{
			return Converters.ObjectsToString(Date, Ruling);
		}
	}

	public class ForeignNames
	{
		[JsonProperty("language")]
		[DefaultValue("")]
		string Language;

		[JsonProperty("name")]
		[DefaultValue("")]
		string Name;

		public string ToString()
		{
			return Converters.ObjectsToString(Language, Name);
		}
	}
}
