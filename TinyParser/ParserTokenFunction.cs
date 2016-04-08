// Licensed under the MIT license. See LICENSE file.

using System;
using System.Collections.Generic;

namespace TinyParser
{
	/// <summary>
    /// format : function_name="arg1, arg2, ..."
	/// </summary>
	class ParserTokenFunction
		: ParserToken
	{
		#region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parser"></param>
		/// <param name="token"></param>
		public ParserTokenFunction(Parser parser, string token)
			: base(parser, token)
		{}

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		public override bool Check(string sentence)
		{
			if (sentence.StartsWith(Token))
			{
			    List<string> args;
                if (GetArguments(sentence.Replace(Token + "=", ""), out args) == true)
			    {
                    Parser.AddCalculator(new CalculatorTokenFunction(Parser.Calculator, Token, args.ToArray()));
                    return true;
			    }
			}

			return false;
		}

	    private bool GetArguments(string sentence, out List<string> args)
	    {
            args = new List<string>();

            if (sentence.StartsWith("\""))
            {
                sentence = sentence.Substring(1);
                int index = sentence.IndexOf("\"", StringComparison.Ordinal);

                if (index != -1)
                {
                    string argument = sentence.Substring(0, index);
                    args.AddRange(argument.Split(','));
                }
                else
                {
                    return false;
                }

                return true;
            }

	        return false;
	    }

	    #endregion
	}
}
