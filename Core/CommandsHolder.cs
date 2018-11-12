using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCReader.Commands {
	public static class CommandsHolder {

		public static List<ICommand> Commands;

		static CommandsHolder() {
			Commands = new List<ICommand>();
			Type[] types = Assembly.GetAssembly(typeof(ICommand)).GetTypes();
			IEnumerable<Type> myTypes = types.Where(myType => myType.IsClass && !myType.IsAbstract && typeof(ICommand).IsAssignableFrom(myType));

			foreach (Type type in myTypes) {
				ICommand obj = (ICommand)Activator.CreateInstance(type);
				if (obj == null) { Console.WriteLine("Error creating instance of " + type.Name); continue; }
				Commands.Add(obj);
			}

			Commands = Commands.OrderBy(x => x.GetType().GetCustomAttribute<ListOrderAttribute>(false)?.Order ?? 10).ToList();
		}

		public static void TryExecuteCommand(string[] words) {
			ICommand cmd = FindCommandWithKeyword(words[0]);

			if (cmd != null) {
				ValidationState result = cmd.Validate(words);
				if (result.Equals(ValidationState.Fail)) { }
				else if (result.Equals(ValidationState.Error)) { cmd.PrintErrorMessage(); }
				else if (result.Equals(ValidationState.Success)) { cmd.CallCommand(); }
			}
			else { PrintErrorMessage(); }
		}

		static void PrintErrorMessage() {
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("Invalid syntax. Type 'list' for a list of available commands.");
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static ICommand FindCommandWithKeyword(string keyword) {
			foreach (ICommand cmd in Commands) {
				if (cmd.ValidateKeyword(keyword)) { return cmd; }
			}
			return null;
		}
	}
}
