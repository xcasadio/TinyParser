// Licensed under the MIT license. See LICENSE file.

namespace TinyParser
{
	class ParserTokenVariable
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
		/// <param name="parser_"></param>
		public ParserTokenVariable(Parser parser_)
			: base(parser_, string.Empty)
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
			if (string.IsNullOrEmpty(sentence) == true)
			{
				return true;
			}

			int c = (int)sentence.ToCharArray(0, 1)[0];

			if (c >= (int)'0' || c <= (int)'9')
			{
				return false;
			}

			Token = sentence;

			return true;
		}

        #endregion
	}
}
