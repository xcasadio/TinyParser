// Licensed under the MIT license. See LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Save editor and game data
        /// </summary>
		Editor,

        /// <summary>
        /// Save only game data
        /// </summary>
		Game
	}

	/// <summary>
	/// Save load interface
	/// </summary>
	public interface ISaveLoad
	{
        void Save(BinaryWriter bw_, SaveOption option_);
		void Save(XmlNode el_, SaveOption option_);
        void Load(BinaryReader br_, SaveOption option_);
		void Load(XmlNode el_, SaveOption option_);
	}
}
