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
		public abstract void CallCommand();
		public abstract ValidationState Validate(string[] words);

		public bool ValidateKeyword(string word) => EqualStrings(word,KeyWord) || EqualStrings(word,KeyWord);

		public virtual void PrintErrorMessage() {
			Console.ForegroundColor = ConsoleColor.Red;

			Console.WriteLine($"Invalid syntax for {GetType().Name}");
			Console.WriteLine(UsageString);
			Console.WriteLine($"Type 'help {KeyWord}' for more info.");

			Console.ResetColor();
		}

		public virtual void PrintHelpMessage() {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(UsageString);
			Console.ResetColor();
		}

		protected static bool EqualStrings(string a,string b) => string.Equals(a,b,StringComparison.OrdinalIgnoreCase);
		protected static string FindKeyInDictionary<U>(Dictionary<string,U> dict,string caseInsensitiveString) => dict.First(x => EqualStrings(x.Key,caseInsensitiveString)).Key;
	}

}
