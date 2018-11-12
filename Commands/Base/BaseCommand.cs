using System;
using System.Collections.Generic;
using System.Linq;

namespace CCReader.Commands {	

	/// <summary>
	/// Base Parametereless command.
	/// Commands with parameters should inherit from <see cref="BaseCommand"/>.
	/// </summary>
	abstract class BaseCommand : ICommand {
		public abstract string KeyWord { get; }
		public abstract string UsageString { get; }

		public bool ValidateKeyword(string keyword) => keyword.Equals(KeyWord,StringComparison.OrdinalIgnoreCase) || keyword.Equals(this.GetType().Name,StringComparison.OrdinalIgnoreCase);

		public virtual void PrintErrorMessage() {
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine($"Invalid syntax for {this.GetType().Name}");
			Console.WriteLine(UsageString);
			Console.WriteLine($"Type 'help {KeyWord}' for more info.");

			Console.ResetColor();
		}

		public virtual void PrintHelpMessage() {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(UsageString);

			Console.ResetColor();
		}


		public abstract void CallCommand();
		public abstract ValidationState Validate(string[] words);
	}


}
