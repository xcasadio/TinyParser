// Licensed under the MIT license. See LICENSE file.

using System;
using System.Xml;
using System.IO;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class Calculator
	{
		#region Fields

	    private CalculatorToken _root;

	    #endregion

        #region Properties

		/// <summary>
		/// 
		/// </summary>
		public CalculatorToken Root
		{
			get { return _root; }
			set { _root = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public TinyParser.Parser Parser { get; }

	    #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parser"></param>
		public Calculator(Parser parser)
		{
			Parser = parser;
		}

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public float Evaluate()
		{
			return _root.Evaluate();
		}

		#region Save / Load

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public void Load(XmlNode el, SaveOption option)
		{
			_root = null;

			XmlNode root = el.SelectSingleNode("Root/Node");

			if (root != null)
			{
				_root = CreateCalculatorToken(this, root, option);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public void Save(XmlNode el, SaveOption option)
		{
			if (_root != null)
			{
				XmlNode node = el.OwnerDocument.CreateElement("Root");
				el.AppendChild(node);
				_root.Save(node, option);
			}
		}

        ///>
        public void Save(BinaryWriter bw, SaveOption option)
        {
            _root?.Save(bw, option);
        }

	    /// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="el"></param>
        /// <param name="option"></param>
        /// <returns></returns>
	    public static CalculatorToken CreateCalculatorToken(Calculator calculator, XmlNode el, SaveOption option)
		{
			CalculatorToken token;

			CalculatorTokenType type = (CalculatorTokenType)int.Parse(el.Attributes["type"].Value);

			switch (type)
			{
				case CalculatorTokenType.BinaryOperator:
					token = new CalculatorTokenBinaryOperator(calculator, el, option);
					break;

				case CalculatorTokenType.Keyword:
					token = new CalculatorTokenKeyword(calculator, el, option);
					break;

				/*case CalculatorTokenType.UnaryOperator:
					token = new CalculatorTokenUnaryOperator(el, option);
					break;*/

				case CalculatorTokenType.Value:
					token = new CalculatorTokenValue(calculator, el, option);
					break;

				case CalculatorTokenType.Function:
					token = new CalculatorTokenFunction(calculator, el, option);
					break;

				default:
					throw new InvalidOperationException("unknown CalculatorTokenType");
			}

			return token;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="br"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static CalculatorToken CreateCalculatorToken(Calculator calculator, BinaryReader br, SaveOption option)
        {
            CalculatorToken token;

            CalculatorTokenType type = (CalculatorTokenType)br.ReadInt32();

            switch (type)
            {
                case CalculatorTokenType.BinaryOperator:
                    token = new CalculatorTokenBinaryOperator(calculator, br, option);
                    break;

                case CalculatorTokenType.Keyword:
                    token = new CalculatorTokenKeyword(calculator, br, option);
                    break;

                /*case CalculatorTokenType.UnaryOperator:
                    token = new CalculatorTokenUnaryOperator(el, option);
                    break;*/

                case CalculatorTokenType.Value:
                    token = new CalculatorTokenValue(calculator, br, option);
                    break;

                case CalculatorTokenType.Function:
                    token = new CalculatorTokenFunction(calculator, br, option);
                    break;

                default:
                    throw new InvalidOperationException("unknown CalculatorTokenType");
            }

            return token;
        }

		#endregion

		#endregion
	}
}
