// Licensed under the MIT license. See LICENSE file.

using System.IO;
using System.Xml;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	public enum SaveOption
	{
        /// <summary>
        /// Save sentence and token tree
        /// </summary>
		Complete,

        /// <summary>
        /// Save only the calculator tree
        /// </summary>
		Compact
	}

	/// <summary>
	/// Save load interface
	/// </summary>
	public interface ISaveLoad
	{
        void Save(BinaryWriter bw, SaveOption option);
		void Save(XmlNode el, SaveOption option);
        void Load(BinaryReader br, SaveOption option);
		void Load(XmlNode el, SaveOption option);
	}
}
