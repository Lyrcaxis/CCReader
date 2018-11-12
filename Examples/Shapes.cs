namespace CCReader.Examples {
	abstract class Shape {
		public abstract string ShapeInAscii { get; }
	}

	class Square : Shape {
		public override string ShapeInAscii {
			get {
				string f = string.Empty;
				f += " _______ \n";
				f += "|       |\n";
				f += "|       |\n";
				f += "|_______|";
				return f;
			}
		}
	}

	class Circle : Shape {
		public override string ShapeInAscii {
			get {
				string f = string.Empty;
				f += "     *  *    \n";
				f += "  *        * \n";
				f += " *          *\n";
				f += " *          *\n";
				f += "  *        * \n";
				f += "     *  *    ";
				return f;
			}
		}
	}


	class Triangle : Shape {
		public override string ShapeInAscii {
			get {
				string f = string.Empty;
				f += @"    /\    "+"\n";
				f += @"   /  \	 "+"\n";
				f += @"  /    \  "+"\n";
				f += @" /      \ "+"\n";
				f += @"/________\"+"";
				return f;
			}
		}
	}

}
