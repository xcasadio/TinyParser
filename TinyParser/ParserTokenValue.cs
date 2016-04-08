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
            else if (float.TryParse(sentence, out _value))
            {
                Parser.AddCalculator(new CalculatorTokenValue(Parser.Calculator, _value));
            }
            else if (IsValidString(sentence))
            {
                Parser.AddCalculator(new CalculatorTokenValue(Parser.Calculator, sentence));
            }
            else
            {
                return false;
            }

		    Token = sentence;

			return true;
		}

	    private bool IsValidString(string sentence)
	    {
            if (string.IsNullOrEmpty(sentence)
                || sentence.Trim().Contains(" ")
                || char.IsLetter(sentence[0]) == false)
	        {
	            return false;
            }

	        return true;
	    }

	    #endregion
	}
}
