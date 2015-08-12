using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyParser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ParserConcrete parser = new ParserConcrete();
            parser.AddFunctionToken("command");
            parser.AddKeywordToken("ctrl");

            Evaluate(parser, "8");
            Evaluate(parser, "1 + 1");
            Evaluate(parser, "8 * 6");
            Evaluate(parser, "7 - 3");
            Evaluate(parser, "10 / 5");
            Evaluate(parser, "1 == 1");
            Evaluate(parser, "1 != 1");
            Evaluate(parser, "1 != 2");
            Evaluate(parser, "0 || 0");
            Evaluate(parser, "1 || 0");
            Evaluate(parser, "1 || 1");
            Evaluate(parser, "0 && 0");
            Evaluate(parser, "1 && 0");
            Evaluate(parser, "1 && 1");
            Evaluate(parser, "1 < 0");
            Evaluate(parser, "1 < 1");
            Evaluate(parser, "1 < 2");
            Evaluate(parser, "1 <= 1");
            Evaluate(parser, "1 >= 1");

            Evaluate(parser, "command=\"PunchLight\"");
            Evaluate(parser, "ctrl");
            Evaluate(parser, "command=\"PunchLight\" && ctrl");

            Console.WriteLine("Press any keys to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parser_"></param>
        /// <param name="s_"></param>
        static private void Evaluate(AbstractParser parser_, string s_)
        {
            Console.WriteLine("{0} : {1}", s_, parser_.Evaluate(s_));
        }
    }
}
