using DataStructures;
using MagicDatabaseBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic
{
	public static class CodeWritingFunctions
	{
		public static void WriteSetLandCountDict()
		{
			Translator.Init();
			string t = "";
			foreach (string code in Translator.SetCodeTranslationTable.Keys)
			{
				t += "\n" + ("SetBasicLandTypeCounts.Add(\"" + code + "\", new Dictionary<BasicLandTypes, int>());");
				t += "\n" + ("SetBasicLandTypeCounts[\"" + code + "\"].Add(BasicLandTypes.Forest," + ImageData.GetLandTypeCount(BasicLandTypes.Forest, Translator.GetSetCode(code)) + ");");
				t += "\n" + ("SetBasicLandTypeCounts[\"" + code + "\"].Add(BasicLandTypes.Mountain," + ImageData.GetLandTypeCount(BasicLandTypes.Mountain, Translator.GetSetCode(code)) + ");");
				t += "\n" + ("SetBasicLandTypeCounts[\"" + code + "\"].Add(BasicLandTypes.Swamp," + ImageData.GetLandTypeCount(BasicLandTypes.Swamp, Translator.GetSetCode(code)) + ");");
				t += "\n" + ("SetBasicLandTypeCounts[\"" + code + "\"].Add(BasicLandTypes.Island," + ImageData.GetLandTypeCount(BasicLandTypes.Island, Translator.GetSetCode(code)) + ");");
				t += "\n" + ("SetBasicLandTypeCounts[\"" + code + "\"].Add(BasicLandTypes.Plains," + ImageData.GetLandTypeCount(BasicLandTypes.Plains, Translator.GetSetCode(code)) + ");");
			}
			Clipboard.SetText(t);
			Console.WriteLine();
		}
	}
}
