using System;

namespace CCReader.Commands {

	[ListOrder(int.MinValue + 2)]
	class ClearCommand : BaseCommand {

		public override string KeyWord => "clear";
		public override string UsageString => "Type 'clear' to clear the console";

		public override ValidationState Validate(string[] words) {

			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Are you sure you want to clear the console? [y/n]");
			Console.ResetColor();

			while (true) {
				var answer = Console.ReadKey(true);
				if (answer.Key == ConsoleKey.Y) { return ValidationState.Success; }
				else if (answer.Key == ConsoleKey.N) { return ValidationState.Error; }
			}
		}

		public override void CallCommand() {
			Console.Clear();
			Console.WriteLine("Console successfully cleared");
		}

		public override void PrintErrorMessage() => Console.WriteLine("\b \b\b \b\b \b\b \b\b[N]");
	}
}

