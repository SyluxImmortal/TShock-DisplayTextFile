using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaApi.Server;

namespace DisplayTextFile
{
	[ApiVersion(1, 16)]
	public class Main : TerrariaPlugin
	{
		#region Plugin Info

		public override Version Version
		{
			get { return new Version("0.0.0"); }
		}
		public override string Name
		{
			get { return "DisplayTextFile"; }
		}
		public override string Author
		{
			get { return "TheWall"; }
		}
		public override string Description
		{
			get { return "Displays text files like MOTD"; }
		}
		public Main(Terraria.Main game)
			: base(game)
		{
			Order = 998;
		}
		#endregion

		public override void Initialize()
		{
			Commands.DisplayTextFile.Init();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}
	}
}
