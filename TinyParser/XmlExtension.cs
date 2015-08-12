using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	static public class XMLExtension
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlDoc_"></param>
		/// <param name="nodeName_"></param>
		static public XmlNode AddRootNode(this XmlDocument xmlDoc_, string nodeName_)
		{
			//let's add the XML declaration section
			XmlNode xmlnode = xmlDoc_.CreateNode(XmlNodeType.XmlDeclaration, "", "");
			xmlDoc_.AppendChild(xmlnode);

			//let's add the root element
			XmlNode xmlElem = xmlDoc_.CreateElement("", nodeName_, "");
			xmlDoc_.AppendChild(xmlElem);

			return xmlElem;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlDoc_"></param>
		/// <param name="xmlElement_"></param>
		/// <param name="attributeName_"></param>
		/// <param name="value_"></param>
		static public void AddAttribute(this XmlDocument xmlDoc_, XmlNode xmlElement_, string attributeName_, string value_)
		{
			XmlAttribute att = xmlDoc_.CreateAttribute(attributeName_);
			att.Value = value_;
			xmlElement_.Attributes.Append(att);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlDoc_"></param>
		/// <param name="nodeName_"></param>
		/// <param name="val_"></param>
		/// <returns></returns>
		static public XmlNode CreateNodeWithText(this XmlDocument xmlDoc_, string nodeName_, string val_)
		{
			XmlNode el = xmlDoc_.CreateElement(nodeName_);
			XmlText txtXML = xmlDoc_.CreateTextNode(val_);
			el.AppendChild(txtXML);

			return el;
		}
	}
}
