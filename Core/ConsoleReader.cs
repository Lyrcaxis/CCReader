using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCReader.Commands;

namespace CCReader {
	public class ConsoleReader {

		public ConsoleReader() {
			Console.Title = "Console Command Reader";
			Console.WriteLine("Welcome to the Console Command Reader.\n");

			Task.Run(ConsoleChecking);
		}


		async Task ConsoleChecking() {

			CommandsHolder.TryExecuteCommand(new []{ "list" });
			Console.WriteLine();

			while (true) {
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				var line = Console.ReadLine();
				InsertSymbol(ref line);
				Console.ResetColor();

				var words = GetWordsFromString(ref line);
				if (words.Length > 0) {
					CommandsHolder.TryExecuteCommand(words);
					Console.WriteLine();
				}
				await Task.Delay(100);
			}
		}

		static void InsertSymbol(ref string s) {
			int linesOfInput = 1 + (s.Length / Console.BufferWidth);
			//Move cursor to just before the input just entered
			Console.CursorTop -= linesOfInput;
			Console.CursorLeft = 0;
			//blank out the content that was just entered
			Console.WriteLine(new string(' ',s.Length));
			//move the cursor to just before the input was just entered
			Console.CursorTop -= linesOfInput;
			Console.CursorLeft = 0;
			Console.WriteLine("> " + s); 
		}

		string[] GetWordsFromString(ref string str) => str.Split(new[] { ' ' },StringSplitOptions.RemoveEmptyEntries);

	}

}