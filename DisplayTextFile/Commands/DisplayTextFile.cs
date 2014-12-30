using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace DisplayTextFile.Commands
{
	class DisplayTextFile
	{
		public static void Init()
		{
			TShockAPI.Commands.ChatCommands.Add(new Command(new List<string>() { "displaytextfile.view", "displaytextfile.broadcast" }, Main, "displaytext"));
			if (!System.IO.Directory.Exists("tshock\\DisplayTextFiles"))
			{
				System.IO.Directory.CreateDirectory("tshock\\DisplayTextFiles");
			}
		}

		public static void Main(CommandArgs args)
		{

			string subCommand;
			if (args.Parameters.Count > 0)
			{
				subCommand = args.Parameters[0].ToLower();
				args.Parameters.RemoveAt(0);
			}
			else
			{
				subCommand = "";
			}

			var group = args.Player.Group;

			switch (subCommand)
			{
				case "list":
				case "view":
					if (!group.HasPermission("displaytextfile.view")) { subCommand = ""; }
					break;
				case "broadcast":
					if (!group.HasPermission("displaytextfile.broadcast")) { subCommand = ""; }
					break;
				default:
					break;
			}

			switch (subCommand)
			{
				case "view":
					ViewCommand(args);
					break;
				case "broadcast":
					BroadcastCommand(args);
					break;
                case "list":
					ListCommand(args);
                    break;
				default:
                    args.Player.SendErrorMessage("Invalid subcommand! Valid subcommands:");
                    args.Player.SendInfoMessage("/displaytext view <text file name> - Sends the contents of <filename> to the requesting player");
                    args.Player.SendInfoMessage("/displaytext broadcast <text file name> - Sends the contents of <filename> to every player");
                    args.Player.SendInfoMessage("/dispalytext list [page] - Lists the files in the tshock/DisplayTextFiles folder");
					break;
			}

		}

		private static void ViewCommand(CommandArgs args)
		{
			TSPlayer player = args.Player;
			if (Utils.Commands.SetGameCommand(args, false, 1, "/displaytext view <filename>")) { return; }

			string filename = args.Parameters[0];
			string message = Data.TextFiles.Get(filename);
			if (message != null)
			{
				args.Player.SendSuccessMessage("displaying contents of: {0}", filename);
				foreach (var line in message.Split('\n'))
				{
					args.Player.SendInfoMessage(line);					
				}
			}
			else
			{
				args.Player.SendSuccessMessage("file ({0}) is null", filename);
			}
		}

		private static void BroadcastCommand(CommandArgs args)
		{
			TSPlayer player = args.Player;
			if (Utils.Commands.SetGameCommand(args, false, 1, "/displaytext broadcast <filename>")) { return; }

			string filename = args.Parameters[0];
			string message = Data.TextFiles.Get(filename);
			if (message != null)
			{
				args.Player.SendSuccessMessage("broadcasting contents of: {0}", filename);
				foreach (var line in message.Split('\n'))
				{
					TSPlayer.All.SendInfoMessage(line);
				}
			}
			else
			{
				args.Player.SendSuccessMessage("file ({0}) is null", filename);
			}
		}

		private static void ListCommand(CommandArgs args)
		{
			TSPlayer player = args.Player;
			if (Utils.Commands.SetGameCommand(args, false, 0, "/displaytext list [page]")) { return; }

			List<string> list = Data.TextFiles.List();
			int page = 0;

			if (list.Count > 0)
			{
				page = 1;
				if (!PaginationTools.TryParsePageNumber(args.Parameters, 0, args.Player, out page))
					page = 1;
			}

			if (page > 0)
			{
				PaginationTools.SendPage(args.Player, page, list,
					new PaginationTools.Settings
					{
						HeaderFormat = "File list ({0}/{1}):",
						FooterFormat = "Type /displaytext list {0} for more."
					});
			}
			else
			{
				player.SendInfoMessage("Zero files found");
			}
		}
	}
}
