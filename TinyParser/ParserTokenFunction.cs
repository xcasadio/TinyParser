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
			if (sentence.StartsWith(Token) == true)
			{
				List<string> args = new List<string>();
				string str = sentence.Replace(Token + "=", "");

			    if (str.StartsWith("\"") == true)
				{
					str = str.Substring(1);

					int index = str.IndexOf("\"", StringComparison.Ordinal);

					if (index != -1)
					{
						string argument = str.Substring(0, index);
                        args.AddRange(argument.Split(','));
					}
					else
					{
						//error
						return false;
					}
				}

				Parser.AddCalculator(new CalculatorTokenFunction(Parser.Calculator, Token, args.ToArray()));

				return true;
			}

			return false;
		}

        #endregion
	}
}
