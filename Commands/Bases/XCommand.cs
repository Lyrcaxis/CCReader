using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CCReader.Commands {

	/// <summary>
	/// Base command with parameters.
	/// <para>Parametereless commands should inherit from <see cref="BaseCommand"/></para>
	/// </summary>
	/// <typeparam name="T">Type of the required parameter</typeparam>
	abstract class XCommand<T> : ICommand {
		public abstract string KeyWord { get; }
		public abstract string UsageString { get; }
		protected abstract string HelpMessage();
		public abstract void CallCommand();
		public abstract void PrintParameterInfo(int i);

		public bool ValidateKeyword(string word) => EqualStrings(word,KeyWord) || EqualStrings(word,KeyWord);

		public ValidationState Validate(string[] words) {
			Type[] requiredParams = typeof(T).GenericTypeArguments;
			if (requiredParams.Length == 0) { requiredParams = new Type[] { typeof(T) }; }
			if (words.Length - 1 < requiredParams.Length) { return ValidationState.Error; }

			//Perform a first-hand validation to check if the types match.
			List<object> args = new List<object>();
			for (int i = 0; i < requiredParams.Length; i++) {
				object obj = TryParse(words[i + 1],requiredParams[i]);
				if (obj == null) { return ValidationState.Error; }
				args.Add(obj);
			}

			return CompleteValidation(args);
		}

		//This will handle extended validation from within the command.
		protected abstract ValidationState CompleteValidation(List<object> args);

		object TryParse(string x,Type t) {
			object obj;
			try {
				if (t == typeof(string)) { obj = x; }
				else if (t == typeof(int)) { obj = int.Parse(x,NumberStyles.Any,CultureInfo.InvariantCulture); }
				else if (t == typeof(float)) { obj = float.Parse(x,NumberStyles.Any,CultureInfo.InvariantCulture); }
				else if (t == typeof(bool)) { obj = bool.Parse(x); }
				//Other types cannot be parsed
				else { return null; }
			} catch { return null; }
			return obj;
		}

		public virtual void PrintErrorMessage() {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Invalid syntax for {GetType().Name}");
			Console.WriteLine(UsageString);
			Console.WriteLine($"Type 'help {KeyWord}' for more info.");
			Console.ResetColor();
		}

		public virtual void PrintHelpMessage() {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(HelpMessage());
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(UsageString);
			Console.ResetColor();
		}

		protected static bool EqualStrings(string a,string b) => string.Equals(a,b,StringComparison.OrdinalIgnoreCase);
		protected static string FindKeyInDictionary<U>(Dictionary<string,U> dict,string caseInsensitiveString) => dict.First(x => EqualStrings(x.Key,caseInsensitiveString)).Key;
	}
}
