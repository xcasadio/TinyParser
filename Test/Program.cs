// Licensed under the MIT license. See LICENSE file.

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
            Parser parser = new Parser(EvaluateKeyword, EvaluateFunction);
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
        static private void Evaluate(Parser parser_, string s_)
        {
            Console.WriteLine("{0} : {1}", s_, parser_.Evaluate(s_));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword_"></param>
        /// <returns></returns>
        static float EvaluateKeyword(string keyword_)
        {
            return keyword_.Equals("command") == true ? 1.0f : 0.0f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName_"></param>
        /// <param name="args_"></param>
        /// <returns></returns>
        static float EvaluateFunction(string functionName_, string[] args_)
        {
            if (functionName_.Equals("command"))
            {
                if (args_[0].Equals("PunchLight"))
                {
                    return 1;
                }
            }

            return 0.0f;
        }
    }
}
