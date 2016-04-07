// Licensed under the MIT license. See LICENSE file.

using System;
using System.Xml;
using System.IO;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class CalculatorTokenBinaryOperator
		: CalculatorToken
	{
		/// <summary>
		/// 
		/// </summary>
		public enum BinaryOperator
		{
			Plus,
			Minus,
			Multiply,
			Divide,

			Equal,
			Different,
			Superior,
			Inferior,
			Or,
			And,
			SupEqual,
			InfEqual,
		}

		#region Fields

		BinaryOperator _operator;
		CalculatorToken _left;		
		CalculatorToken _right;

        #endregion

        #region Properties

		/// <summary>
		/// 
		/// </summary>
		public CalculatorToken Left
		{
			get { return _left; }
			set { _left = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public CalculatorToken Right
		{
			get { return _right; }
			set { _right = value; }
		}

        #endregion

        #region Constructors

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="operator_"></param>
		public CalculatorTokenBinaryOperator(Calculator calculator, BinaryOperator operator_)
			: base(calculator)
		{
			_operator = operator_;
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="el"></param>
        /// <param name="option"></param>
		public CalculatorTokenBinaryOperator(Calculator calculator, XmlNode el, SaveOption option)
			: base(calculator)
		{
			Load(el, option);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="br"></param>
        /// <param name="option"></param>
        public CalculatorTokenBinaryOperator(Calculator calculator, BinaryReader br, SaveOption option)
            : base(calculator)
        {
            Load(br, option);
        }

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override float Evaluate()
		{
			float res;			

			switch (_operator)
			{
				case BinaryOperator.Plus:
					res = _left.Evaluate() + _right.Evaluate();
					break;

				case BinaryOperator.Minus:
					res = _left.Evaluate() - _right.Evaluate();
					break;

				case BinaryOperator.Multiply:
					res = _left.Evaluate() * _right.Evaluate();
					break;

				case BinaryOperator.Divide:
					res = _left.Evaluate() / _right.Evaluate();
					break;

				case BinaryOperator.Equal:
					res = Convert.ToSingle(_left.Evaluate() == _right.Evaluate());
					break;

				case BinaryOperator.Different:
					res = Convert.ToSingle(_left.Evaluate() != _right.Evaluate());
					break;

				case BinaryOperator.Superior:
					res = Convert.ToSingle(_left.Evaluate() > _right.Evaluate());
					break;

				case BinaryOperator.Inferior:
					res = Convert.ToSingle(_left.Evaluate() < _right.Evaluate());
					break;

				case BinaryOperator.SupEqual:
					res = Convert.ToSingle(_left.Evaluate() >= _right.Evaluate());
					break;

				case BinaryOperator.InfEqual:
					res = Convert.ToSingle(_left.Evaluate() <= _right.Evaluate());
					break;

				case BinaryOperator.Or:
					res = Convert.ToSingle(Convert.ToBoolean(_left.Evaluate()) || Convert.ToBoolean(_right.Evaluate()));
					break;

				case BinaryOperator.And:
					res = Convert.ToSingle(Convert.ToBoolean(_left.Evaluate()) && Convert.ToBoolean(_right.Evaluate()));
					break;

				default:
					throw new InvalidOperationException("CalculatorTokenBinaryOperator.Evaluate() : BinaryOperator unknown");
			}

			return res;
		}

		#region Save / Load

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Save(XmlNode el, SaveOption option)
		{
			XmlNode node = el.OwnerDocument.CreateElement("Node");
			el.AppendChild(node);
            node.AddAttribute("type", ((int)CalculatorTokenType.BinaryOperator).ToString());
            node.AddAttribute("operator", ((int)_operator).ToString());

			XmlNode child = el.OwnerDocument.CreateElement("Left");
			node.AppendChild(child);
			_left.Save(child, option);

			child = el.OwnerDocument.CreateElement("Right");
			node.AppendChild(child);
			_right.Save(child, option);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Load(XmlNode el, SaveOption option)
		{
			_operator = (BinaryOperator) int.Parse(el.Attributes["operator"].Value);

			XmlNode child = (XmlNode)el.SelectSingleNode("Left/Node");
			_left = Calculator.CreateCalculatorToken(Calculator, child, option);

			child = (XmlNode)el.SelectSingleNode("Right/Node");
			_right = Calculator.CreateCalculatorToken(Calculator, child, option);

		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="option"></param>
        public override void Save(BinaryWriter bw, SaveOption option)
        {
            bw.Write((int)CalculatorTokenType.BinaryOperator);
            bw.Write((int)_operator);
            _left.Save(bw, option);
            _right.Save(bw, option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="option"></param>
        public override void Load(BinaryReader br, SaveOption option)
        {
            _operator = (BinaryOperator)br.ReadInt32();
            _left = Calculator.CreateCalculatorToken(Calculator, br, option);
            _right = Calculator.CreateCalculatorToken(Calculator, br, option);
        }

		#endregion

		#endregion
		
	}
}
