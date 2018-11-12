using System;

namespace CCReader.Commands {

	[ListOrder(int.MinValue + 3)]
	class ExitCommand : BaseCommand {

		public override string KeyWord => "exit";
		public override string UsageString => "Type 'exit' to close the program.";

		public override ValidationState Validate(string[] words) {

			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Are you sure you want to exit? [y/n]");
			Console.ResetColor();
			Console.Beep();

			while (true) {
				ConsoleKeyInfo answer = Console.ReadKey(true);
				if (answer.Key == ConsoleKey.Y) { return ValidationState.Success; }
				else if (answer.Key == ConsoleKey.N) { return ValidationState.Error; }
			}
		}

		public override async void CallCommand() {
			Console.WriteLine();
			Console.WriteLine("See you!");
			await System.Threading.Tasks.Task.Delay(1000);
			Environment.Exit(0);
		}

		public override void PrintErrorMessage() => Console.WriteLine("\b \b\b \b\b \b\b \b\b[N]");

	}
}

