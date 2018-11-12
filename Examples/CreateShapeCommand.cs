using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CCReader.Commands;

namespace CCReader.Examples {

	class CreateShape : XCommand<(string shape, float size, string color)> {

		public override string KeyWord => "create";
		public override string UsageString => "Usage: 'create [shape] [size] [color]'";

		Dictionary<string,Shape> ValidShapes = new Dictionary<string,Shape> {
			{"Circle", new Circle()},
			{"Square", new Square()},
			{"Triangle",new Triangle()},
		};

		float minSize = 0, maxSize = 5;

		Dictionary<string,ConsoleColor> ValidColors = new Dictionary<string,ConsoleColor>{
			{"Red", ConsoleColor.Red},
			{"Blue", ConsoleColor.Blue},
			{"Green",ConsoleColor.Green},
		};

		protected override ValidationState CompleteValidate(string[] words) {
			int step = 0;
			try {
				CommandShape.shape = ValidShapes.First(x => CheckIfStringsMatch(x.Key,words[1])).Key;
				step++;
				CommandShape.size = float.Parse(words[2],NumberStyles.Any,CultureInfo.InvariantCulture);
				if (CommandShape.size > maxSize) { throw new Exception("ASDFFFFFFFF"); }
				step++;
				CommandShape.color = ValidColors.First(x => CheckIfStringsMatch(x.Key,words[3])).Key;
			} catch {
				PrintParameterInfo(step);
				return ValidationState.Error;
			}

			return ValidationState.Success;
		}

		public override void CallCommand() {
			Console.WriteLine("Creating {0} with size {1} and color {2} (size not implemented)",CommandShape.shape,CommandShape.size,CommandShape.color);
			Console.ForegroundColor = ValidColors[CommandShape.color];
			Console.WriteLine(ValidShapes[CommandShape.shape].ShapeInAscii);
		}


		protected override string HelpMessage() {
			string x = string.Empty;
			x += "Valid Shapes: ";
			foreach (var item in ValidShapes) { x += $"[{item.Key}]"; }
			x += "\n";
			x += $"Valid Size [{minSize} to {maxSize}]";
			x += "\n";
			x += "Valid Colors: ";
			foreach (var item in ValidColors) { x += $"[{item.Key}]"; }
			x += "\n";
			return x;
		}

		public override void PrintParameterInfo(int i) {
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.Write("-- ");
			if (i == 0) { Console.Write("Wrong Shape"); }
			else if (i == 1) { Console.Write("Wrong Size"); }
			else if (i == 2) { Console.Write("Wrong Color"); }
			Console.WriteLine(" --");
			Console.ResetColor();
		}

	}
}

