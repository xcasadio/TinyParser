// Licensed under the MIT license. See LICENSE file.

using System;
using System.Xml;
using System.IO;

namespace TinyParser
{
	/// <summary>
	/// 
	/// </summary>
	class CalculatorTokenSequence
		: CalculatorToken
	{
		/// <summary>
		/// 
		/// </summary>
		public enum TokenSequence
		{
			Sequence,
			StartSequence,
			EndSequence
		}

		#region Fields

	    #endregion

        #region Properties

		/// <summary>
		/// 
		/// </summary>
		public TokenSequence Sequence { get; }

	    #endregion

        #region Constructors

		/// <summary>
        /// 
        /// </summary>
        /// <param name="calculator"></param>
        /// <param name="sequence"></param>
		public CalculatorTokenSequence(Calculator calculator, TokenSequence sequence)
			: base(calculator)
		{
			Sequence = sequence;
		}

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override float Evaluate()
		{
			throw new InvalidOperationException("Don't use to evaluate");
		}

		#region Save / Load

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Save(XmlNode el, SaveOption option)
		{
			throw new InvalidOperationException("Can't save this object. It is a temporary object");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		/// <param name="option"></param>
		public override void Load(XmlNode el, SaveOption option)
		{
            throw new InvalidOperationException("Can't save this object. It is a temporary objecte");
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="option"></param>
        public override void Save(BinaryWriter bw, SaveOption option)
        {
            throw new InvalidOperationException("Can't save this object. It is a temporary object");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="el_"></param>
        /// <param name="option"></param>
        public override void Load(BinaryReader br, SaveOption option)
        {
            throw new InvalidOperationException("Can't save this object. It is a temporary object");
        }

		#endregion

        #endregion
	}
}
