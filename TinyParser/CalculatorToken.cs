// Licensed under the MIT license. See LICENSE file.

using System.IO;
using System.Xml;

namespace TinyParser
{
	enum CalculatorTokenType
	{
		BinaryOperator,
		UnaryOperator,
		Keyword,
		Value,
		Function
	}

	/// <summary>
	/// 
	/// </summary>
	abstract class CalculatorToken
		: ISaveLoad
	{
		#region Fields

	    readonly Calculator _calculator;

        #endregion

        #region Properties

		/// <summary>
		/// 
		/// </summary>
		protected TinyParser.Calculator Calculator => _calculator;

	    #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="calculator"></param>
		protected CalculatorToken(Calculator calculator)
		{
			_calculator = calculator;
		}

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public abstract float Evaluate();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		protected float EvaluateKeyword(string keyword)
		{
			return _calculator.Parser.EvaluateKeyword(keyword);
		}

		#region ISaveLoad Members

		public abstract void Save(XmlNode el, SaveOption option);
		public abstract void Load(XmlNode el, SaveOption option);

        public abstract void Save(BinaryWriter bw, SaveOption option);
        public abstract void Load(BinaryReader br, SaveOption option);

		#endregion

        #endregion		
	}
}
