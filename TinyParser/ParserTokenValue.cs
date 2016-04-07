// Licensed under the MIT license. See LICENSE file.

namespace TinyParser
{
	class ParserTokenValue
		: ParserToken
	{
		#region Fields

		float _value;

        #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parser"></param>
		public ParserTokenValue(Parser parser)
			: base(parser, string.Empty)
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
			if (string.IsNullOrEmpty(sentence))
			{
				return false;
			}

		    Parser.AddCalculator(float.TryParse(sentence, out _value) == true
		        ? new CalculatorTokenValue(Parser.Calculator, _value)
		        : new CalculatorTokenValue(Parser.Calculator, sentence));

		    Token = sentence;

			return true;
		}

        #endregion
	}
}
