// Licensed under the MIT license. See LICENSE file.

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class ParserTokenUnaryOperator
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
		public ParserTokenUnaryOperator(Parser parser, string token)
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
			if (sentence.StartsWith(Token) == true)
			{
				Parser.Check(Token.Substring(1));
			}		

			return false;
		}

        #endregion
	}
}
