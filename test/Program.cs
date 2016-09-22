using CommandLineParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserTest {
	class Program {
		private static void Assert(bool condition){
			if (!condition) {
				throw new Exception("Assertion failed");
			}
		}

		static void Main(string[] args) {
			Parser parser = new Parser(new string[]{
				"normal1",
				"normal2",
				"-abc",
				"--long1",
				"value1_long1",
				"value2_long1",
				"--long2=value1_long2",
				"normal3",
				"-d",
				"-e",
				"normal4",
				"--flag"
			});

			Assert(parser.Exists(""));
			Assert(parser.Exists(null, 'a'));
			Assert(parser.Exists(null, 'b'));
			Assert(parser.Exists(null, 'c'));
			Assert(parser.Exists(null, 'd'));
			Assert(parser.Exists(null, 'e'));
			Assert(!parser.Exists(null, 'f'));
			Assert(parser.Exists("blah", 'a'));
			Assert(parser.Exists("long1", 'z'));
			Assert(parser.Exists("long1"));
			Assert(parser.Exists("long2"));
			Assert(!parser.Exists("long3"));
			Assert(!parser.Exists("long3", 'z'));
			Assert(parser.Exists(""));
			Assert(parser.SwitchExists('b'));
			Assert(!parser.SwitchExists('z'));
			Assert(parser.OptionExists(""));
			Assert(parser.OptionExists("long1"));
			Assert(!parser.OptionExists("long3"));
			Assert(parser.SwitchExistsOrValueIs('a', "long2", "value1_long1"));
			Assert(!parser.SwitchExistsOrValueIs('z', "long2", "value1_long1"));
			Assert(parser.SwitchExistsOrValueIs('z', "long2", "value1_long2"));
			Assert(!parser.SwitchExistsOrValueIs('z', "long2", "value1_long1"));
			Assert(parser.SwitchExistsOrValueIs('a', "long3", "value1_long1"));
			Assert(!parser.SwitchExistsOrValueIs('z', "long3", "value1_long1"));
			Assert(parser.Get("") == "normal1");
			Assert(parser.GetAll("")[0] == "normal1");
			Assert(parser.GetAll("")[1] == "normal2");
			Assert(parser.GetAll("")[2] == "normal3");
			Assert(parser.GetAll("")[3] == "normal4");
			Assert(parser.GetAll("").Length == 4);
			Assert(parser.GetAll("long1")[0] == "value1_long1");
			Assert(parser.GetAll("long1")[1] == "value2_long1");
			Assert(parser.GetAll("long1").Length == 2);
			Assert(parser.GetAll("long2")[0] == "value1_long2");
			Assert(parser.GetAll("long2").Length == 1);
			Assert(parser.OptionExists("flag"));
		}
	}
}
