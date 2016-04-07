// Licensed under the MIT license. See LICENSE file.

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class ParserTokenKeyword
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
		public ParserTokenKeyword(Parser parser, string token)
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
			if (Token.Equals(sentence) == true)
			{
				Parser.AddCalculator(new CalculatorTokenKeyword(Parser.Calculator, Token));
				return true;
			}

			return false;
		}

        #endregion
	}
}
