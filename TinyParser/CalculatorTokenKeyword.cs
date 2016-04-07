// Licensed under the MIT license. See LICENSE file.

using System.Xml;
using System.IO;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class CalculatorTokenKeyword
		: CalculatorToken
	{
		#region Fields

		string _keyword;

        #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyword"></param>
		public CalculatorTokenKeyword(Calculator calculator, string keyword)
			: base(calculator)
		{
			_keyword = keyword;
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="el"></param>
        /// <param name="option"></param>
		public CalculatorTokenKeyword(Calculator calculator, XmlNode el, SaveOption option)
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
        public CalculatorTokenKeyword(Calculator calculator, BinaryReader br, SaveOption option)
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
			return Calculator.Parser.EvaluateKeyword(_keyword);
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
			node.AddAttribute("type", ((int)CalculatorTokenType.Keyword).ToString());
			XmlNode valueNode = el.OwnerDocument.CreateElementWithText("Keyword", _keyword);
			node.AppendChild(valueNode);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Load(XmlNode el, SaveOption option)
		{
			_keyword = el.SelectSingleNode("Keyword").InnerText;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="option"></param>
        public override void Save(BinaryWriter bw, SaveOption option)
        {
            bw.Write((int)CalculatorTokenType.Keyword);
            bw.Write(_keyword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="option"></param>
        public override void Load(BinaryReader br, SaveOption option)
        {
            br.ReadInt32();
            _keyword = br.ReadString();
        }

		#endregion

        #endregion
	}
}
