// Licensed under the MIT license. See LICENSE file.

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class ParserTokenSequence
		: ParserToken
	{
		#region Fields

	    public const string Sequence = "`";

	    #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parser"></param>
		public ParserTokenSequence(Parser parser)
			: base(parser, Sequence)
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
			if (Token.Equals(sentence) == true)
			{
				Parser.AddCalculator(new CalculatorTokenSequence(Parser.Calculator, CalculatorTokenSequence.TokenSequence.Sequence));
				return true;
			}

			return false;
		}

        #endregion
	}
}
