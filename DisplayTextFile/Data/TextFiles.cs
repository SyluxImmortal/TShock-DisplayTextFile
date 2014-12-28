using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayTextFile.Data
{
	class TextFiles
	{
		public static string Get(string filename)
		{
			string fileContents = null;
			if (File.Exists("tshock\\DisplayTextFiles\\" + filename))
			{
				try
				{
					fileContents = File.ReadAllText("tshock\\DisplayTextFiles\\" + filename);
				}
				catch (Exception)
				{
				}
			}
			return fileContents;
		}

		public static List<string> List()
		{
			List<string> files = new List<string>();
			try
			{
				foreach (var file in Directory.GetFiles("tshock\\DisplayTextFiles"))
				{
					files.Add(file.Remove(0,file.LastIndexOf('\\') + 1));
				}
			}
			catch (Exception)
			{
			}
			return files;
		}
	}
}
