﻿// Licensed under the MIT license. See LICENSE file.

using System.Globalization;
using System.Xml;
using System.IO;

namespace TinyParser
{
    /// <summary>
    /// 
    /// </summary>
    class CalculatorTokenValue
		: CalculatorToken
	{
		#region Fields

		int _type;
		float _value;
		string _string;

        #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="value"></param>
		public CalculatorTokenValue(Calculator calculator, float value)
			: base(calculator)
		{
			_value = value;
			_type = 0;
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="value"></param>
		public CalculatorTokenValue(Calculator calculator, string value)
			: base(calculator)
		{
			_string = value;
			_type = 1;
		}

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="el"></param>
        /// <param name="option"></param>
		public CalculatorTokenValue(Calculator calculator, XmlNode el, SaveOption option)
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
        public CalculatorTokenValue(Calculator calculator, BinaryReader br, SaveOption option)
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
			return _value;
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
            node.AddAttribute("type", ((int)CalculatorTokenType.Value).ToString());

            string value = _type == 0 ? _value.ToString(CultureInfo.InvariantCulture) : _string;

            XmlNode valueNode = el.OwnerDocument.CreateElementWithText("Value", value);
            valueNode.AddAttribute("type", _type.ToString());
			node.AppendChild(valueNode);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Load(XmlNode el, SaveOption option)
		{
			_type = int.Parse(el.SelectSingleNode("Value").Attributes["type"].Value);

			if (_type == 0)
			{
				_value = float.Parse(el.SelectSingleNode("Value").InnerText);
			}
			else
			{
				_string = el.SelectSingleNode("Value").InnerText;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="option"></param>
        public override void Save(BinaryWriter bw, SaveOption option)
        {
            bw.Write((int)CalculatorTokenType.Value);
            string value = _type == 0 ? _value.ToString(CultureInfo.InvariantCulture) : _string;
            bw.Write(value);
            bw.Write(_type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="br"></param>
        /// <param name="option"></param>
        public override void Load(BinaryReader br, SaveOption option)
        {
            _type = br.ReadInt32();

            if (_type == 0)
            {
                _value = float.Parse(br.ReadString());
            }
            else
            {
                _string = br.ReadString();
            }
        }

		#endregion

        #endregion
	}
}
