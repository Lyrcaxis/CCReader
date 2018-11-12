using System.Threading;

namespace CCReader {
	class Program {
		static void Main(string[] args) {
			new ConsoleReader();
			while (true) { Thread.Sleep(1000); }
		}
	}
}
