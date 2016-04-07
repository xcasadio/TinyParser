// Licensed under the MIT license. See LICENSE file.

using System;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class ParserTokenBinaryOperator
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
		public ParserTokenBinaryOperator(Parser parser, string token)
			: base(parser, token)
		{

		}

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sentence"></param>
		/// <returns></returns>
		public override bool Check(string sentence)
		{
			int index = sentence.IndexOf(Token, StringComparison.Ordinal);

			if (index != -1)
			{
				Parser.AddCalculator(Parser.GetCalculatorByBinaryOperator(Token));

			    var s1 = sentence.Substring(0, index);
				var s2 = sentence.Substring(index + Token.Length, sentence.Length - index - Token.Length);

				return Parser.Check(s1) && Parser.Check(s2);
			}

			return false;
		}

        #endregion
	}
}
