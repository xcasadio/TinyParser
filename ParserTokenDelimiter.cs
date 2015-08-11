﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightParser
{
	class ParserTokenDelimiter
		: ParserToken
	{
		#region Fields

		string m_Close;

        #endregion

        #region Properties

        #endregion

        #region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parser_"></param>
		/// <param name="open_"></param>
		/// <param name="close_"></param>
		public ParserTokenDelimiter(AbstractParser parser_, string open_, string close_)
			: base(parser_, open_)
		{
			m_Close = close_;
			Parser.AddToken(close_);
		}

        #endregion

        #region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sentence_"></param>
		/// <returns></returns>
		public override bool Check(string sentence_)
		{
			string res;
			string outside;

			if (GetStringBetweenDelimiter(sentence_, m_Token, m_Close, out res, out outside) == true)
			{
				bool r = true;

				//attention inverse droite et gauche !!!!!!
				if (string.IsNullOrEmpty(outside) == false)
				{
					r = Parser.Check(outside);
				}

				Parser.AddCalculator(new CalculatorTokenSequence(Parser.Calculator, CalculatorTokenSequence.TokenSequence.StartSequence));
				r &= Parser.Check(res);
				Parser.AddCalculator(new CalculatorTokenSequence(Parser.Calculator, CalculatorTokenSequence.TokenSequence.EndSequence));

				return r;
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str_"></param>
		/// <param name="open_"></param>
		/// <param name="close_"></param>
		/// <param name="s1_">la sequence</param>
		/// <param name="s2_">la phrase avec la sequence remplacé par &lt;seq&gt;</param>
		/// <returns></returns>
		static public bool GetStringBetweenDelimiter(string str_, string open_, string close_, out string s1_, out string s2_)
		{
			s1_ = string.Empty;
			s2_ = string.Empty;

			int first = str_.IndexOf(open_);

			if (first != -1)
			{
				int p = 1; // one open found
				int index = -1;

				for (int i = first + open_.Length; i < str_.Length; i += open_.Length)
				{
					string tmp = str_.Substring(i, str_.Length - open_.Length - i + 1);

					if (tmp.StartsWith(open_) == true)
					{
						p++;
					}
					else if (tmp.StartsWith(close_) == true)
					{
						p--;
					}

					if (p == 0)
					{
						index = i;
						break;
					}
				}

				if (index == -1 || index == first + open_.Length)
				{
					return false;
				}

				s1_ = str_.Substring(first + open_.Length, index - open_.Length - first).Trim();
				s2_ = str_.Substring(open_.Length + index, str_.Length - index - 1).Trim();
				//on decoupe a gauche puis a droite
				s2_ = str_.Substring(0, first + open_.Length - 1).Trim();
				s2_ += ParserTokenSequence.sequence;
				s2_ += str_.Substring(open_.Length + index, str_.Length - index - 1).Trim();

				return true;
			}

			return false;
		}

        #endregion
	}
}