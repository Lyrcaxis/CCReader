namespace CCReader.Commands {

	[ListOrder(int.MinValue)]
	class HelpCommand : BaseCommand {

		public override string KeyWord => "help";
		public override string UsageString => "Usage: 'help [Command]'";

		ICommand tarCommand;

		public override void CallCommand() => tarCommand.PrintHelpMessage();


		public override ValidationState Validate(string[] words) {
			if (words.Length > 1) { tarCommand = CommandsHolder.FindCommandWithKeyword(words[1]); }
			else { tarCommand = this; }

			if (tarCommand == null) { return ValidationState.Error; }
			else { return ValidationState.Success; }
		}
	}
}
