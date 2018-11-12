using System;

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
		protected abstract ValidationState CompleteValidate(string[] words);
		public abstract void CallCommand();
		public abstract void PrintParameterInfo(int i);

		protected T CommandShape;

		public bool ValidateKeyword(string keyword) => keyword.Equals(KeyWord,StringComparison.OrdinalIgnoreCase) || keyword.Equals(GetType().Name,StringComparison.OrdinalIgnoreCase);


		public ValidationState Validate(string[] words) {
			Type[] requiredParams = typeof(T).GenericTypeArguments;
			if (requiredParams.Length == 0) { requiredParams = new Type[] { typeof(T) }; }

			if (words.Length - 1 < requiredParams.Length) { return ValidationState.Error; }

			for (int i = 0; i < requiredParams.Length && i < words.Length - 1; i++) {
				if (!TryValidate(words[i + 1],requiredParams[i])) {
					PrintParameterInfo(i);
					return ValidationState.Error;
				}
			}

			return CompleteValidate(words);
		}

		bool TryValidate(string x,Type t) {
			try {
				if (t == typeof(string)) { return true; }
				else if (t == typeof(int)) { int.Parse(x); }
				else if (t == typeof(float)) { float.Parse(x); }
				else if (t == typeof(bool)) { bool.Parse(x); }
			} catch { return false; }

			return true;
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

		protected static bool CheckIfStringsMatch(string a,string b) => string.Equals(a,b,StringComparison.OrdinalIgnoreCase);
	}
}
