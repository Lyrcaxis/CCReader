using System;

namespace CCReader.Commands {

	[ListOrder(int.MinValue+1)]
	class ListCommand : BaseCommand {

		public override string KeyWord => "list";
		public override string UsageString => "Type 'list' for a list of available commands.";

		public override ValidationState Validate(string[] words) => ValidationState.Success;

		public override void CallCommand() {
			Console.WriteLine("List of available commands:");
			foreach (ICommand item in CommandsHolder.Commands) {
				Console.ForegroundColor = ConsoleColor.DarkMagenta;
				Console.Write($"{item.GetType().Name.Replace("Command","")}: ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{item.UsageString}");
			}
			Console.ResetColor();
		}
	}
}

