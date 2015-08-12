using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyParser;

namespace Test
{
    class ParserConcrete
        : AbstractParser
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword_"></param>
        /// <returns></returns>
        public override float EvaluateKeyword(string keyword_)
        {
            return keyword_.Equals("command") == true ? 1.0f : 0.0f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName_"></param>
        /// <param name="args_"></param>
        /// <returns></returns>
        public override float EvaluateFunction(string functionName_, string[] args_)
        {
            if (functionName_.Equals("command"))
            {
                if (args_[0].Equals("PunchLight"))
                {
                    return 1;
                }
            }

            return 0;
        }

        #endregion
    }
}
