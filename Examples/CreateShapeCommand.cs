using System;
using System.Collections.Generic;
using CCReader.Commands;

namespace CCReader.Examples {

	class CreateShape : XCommand<(string shape, string color, float size)> {
		public override string KeyWord => "create";
		public override string UsageString => "Usage: 'create [shape] [color] [size]'";

		Dictionary<string,Shape> ValidShapes = new Dictionary<string,Shape> {
			{"Circle", new Circle()},
			{"Square", new Square()},
			{"Triangle",new Triangle()},
		};
		Dictionary<string,ConsoleColor> ValidColors = new Dictionary<string,ConsoleColor>{
			{"Red", ConsoleColor.Red},
			{"Blue", ConsoleColor.Blue},
			{"Green",ConsoleColor.Green},
		};

		string shape;
		string color;
		float size, minSize = 0, maxSize = 5;

		protected override ValidationState CompleteValidation(List<object> args) {
			try {
				shape = FindKeyInDictionary(ValidShapes,(string)args[0]);
				color = FindKeyInDictionary(ValidColors,(string)args[1]);
				size = (float)args[2];
			} catch { return ValidationState.Error; }

			return ValidationState.Success;
		}

		public override void CallCommand() {
			Console.WriteLine("Creating {0} with size {1} and color {2} (size not implemented)",shape,size,color);
			Console.ForegroundColor = ValidColors[color];
			Console.WriteLine(ValidShapes[shape].ShapeInAscii);
		}

		protected override string HelpMessage() {
			string x = "Valid Shapes: ";
			foreach (var item in ValidShapes) { x += $"[{item.Key}]"; }
			x += "\n Valid Colors: ";
			foreach (var item in ValidColors) { x += $"[{item.Key}]"; }
			x += $"\n Valid Size [{minSize} to {maxSize}]";
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

