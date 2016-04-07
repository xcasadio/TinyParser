// Licensed under the MIT license. See LICENSE file.

namespace TinyParser
{
    /// <summary>
    /// Mother for all token than the Parser can identify
    /// </summary>
	abstract class ParserToken
	{
		#region Fields

		protected string Token;

        #endregion

        #region Properties

		/// <summary>
		/// Gets
		/// </summary>
		protected Parser Parser { get; }

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parser"></param>
		/// <param name="token"></param>
		protected ParserToken(Parser parser, string token)
		{
			Token = token;
			Parser = parser;

            parser.AddToken(token);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public abstract bool Check(string sentence);

        #endregion
	}
}
