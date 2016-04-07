// Licensed under the MIT license. See LICENSE file.

using System.Linq;
using System.Xml;
using System.IO;

namespace TinyParser
{
    /// <summary>
    /// 
    /// </summary>
    class CalculatorTokenFunction
		: CalculatorToken
	{
		#region Fields

		string _functionName;
		string[] _args;

        #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="calculator"></param>
		/// <param name="functionName"></param>
		/// <param name="args"></param>
		public CalculatorTokenFunction(Calculator calculator, string functionName, string[] args)
			: base(calculator)
		{
			_functionName = functionName;
			_args = args;
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="el"></param>
        /// <param name="option"></param>
		public CalculatorTokenFunction(Calculator calculator, XmlNode el, SaveOption option)
			: base(calculator)
		{
			Load(el, option);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="option"></param>
        public CalculatorTokenFunction(Calculator calculator, BinaryReader br, SaveOption option)
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
			return Calculator.Parser.EvaluateFunction(_functionName, _args);
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
            node.AddAttribute("type", ((int)CalculatorTokenType.Function).ToString());
			XmlNode valueNode = el.OwnerDocument.CreateElementWithText("FunctionName", _functionName);
			node.AppendChild(valueNode);

			XmlNode argNode = el.OwnerDocument.CreateElement("ArgumentList");
			node.AppendChild(argNode);

			foreach (string a in _args)
			{
				valueNode = el.OwnerDocument.CreateElementWithText("Argument", a);
				argNode.AppendChild(valueNode);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Load(XmlNode el, SaveOption option)
		{
			_functionName = el.SelectSingleNode("FunctionName").InnerText;
		    _args = (from XmlNode n in el.SelectNodes("ArgumentList/Argument") select n.InnerText).ToArray();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="option"></param>
        public override void Save(BinaryWriter bw, SaveOption option)
        {
            bw.Write((int)CalculatorTokenType.Function);
            bw.Write(_functionName);
            bw.Write(_args.Length);

            foreach (string a in _args)
            {
                bw.Write(a);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="option"></param>
        public override void Load(BinaryReader br, SaveOption option)
        {
            br.ReadInt32();
            _functionName = br.ReadString();
            int count = br.ReadInt32();
            _args = new string[count];

            for (int i=0; i<count; i++)
            {
                _args[i] = br.ReadString();
            }
        }

		#endregion

        #endregion
	}
}
