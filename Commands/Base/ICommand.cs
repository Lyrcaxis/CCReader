using System;
using System.Collections.Generic;
using System.Linq;

namespace CCReader.Commands {

	/// <summary>
	/// Interface for commands.
	/// Used in <see cref="CommandsHolder"/>.
	/// </summary>
	public interface ICommand {
		ValidationState Validate(string[] words);

		void CallCommand();
		void PrintErrorMessage();
		void PrintHelpMessage();
		bool ValidateKeyword(string keyword);

		string KeyWord { get; }
		string UsageString { get; }
	}
}
