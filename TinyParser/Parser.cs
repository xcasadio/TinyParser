// Licensed under the MIT license. See LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;

namespace TinyParser
{
    /// <summary>
    /// Recognized operators : 
    /// +, -, /, *, ==, !=, >, &lt;, >=, &lt;=, ||, &&
    /// 
    /// Can support functions, format : function_name="arg1, arg2, ..."
    /// Can support keyword
    /// </summary>
    public class Parser
    {
        #region Delegate

        public delegate float EvaluateKeywordDelegate(string keyword);
        public delegate float EvaluateFunctionDelegate(string function, string[] args);

        #endregion // Delegate

        #region Fields

        public EvaluateKeywordDelegate KeywordDelegate { private get; set; }
        public EvaluateFunctionDelegate FunctionDelegate { private get; set; }

        readonly Dictionary<string, CalculatorTokenBinaryOperator.BinaryOperator> _mapBinaryOperator = new Dictionary<string, CalculatorTokenBinaryOperator.BinaryOperator>();
        private Dictionary<int, List<ParserToken>> _tokens = new Dictionary<int, List<ParserToken>>();

        readonly List<CalculatorToken> _calculatorTokens = new List<CalculatorToken>();
        readonly Calculator _calculator;

        readonly List<string> _tokensValue = new List<string>();

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        internal TinyParser.Calculator Calculator => _calculator;

        /// <summary>
        /// Gets
        /// </summary>
        internal string[] TokensValue => _tokensValue.ToArray();

        #endregion

        #region Constructors

        public Parser()
        {
            _mapBinaryOperator.Add("+", CalculatorTokenBinaryOperator.BinaryOperator.Plus);
            _mapBinaryOperator.Add("-", CalculatorTokenBinaryOperator.BinaryOperator.Minus);
            _mapBinaryOperator.Add("/", CalculatorTokenBinaryOperator.BinaryOperator.Divide);
            _mapBinaryOperator.Add("*", CalculatorTokenBinaryOperator.BinaryOperator.Multiply);
            _mapBinaryOperator.Add("==", CalculatorTokenBinaryOperator.BinaryOperator.Equal);
            _mapBinaryOperator.Add("!=", CalculatorTokenBinaryOperator.BinaryOperator.Different);
            _mapBinaryOperator.Add(">=", CalculatorTokenBinaryOperator.BinaryOperator.SupEqual);
            _mapBinaryOperator.Add("<=", CalculatorTokenBinaryOperator.BinaryOperator.InfEqual);
            _mapBinaryOperator.Add(">", CalculatorTokenBinaryOperator.BinaryOperator.Superior);
            _mapBinaryOperator.Add("<", CalculatorTokenBinaryOperator.BinaryOperator.Inferior);
            _mapBinaryOperator.Add("||", CalculatorTokenBinaryOperator.BinaryOperator.Or);
            _mapBinaryOperator.Add("&&", CalculatorTokenBinaryOperator.BinaryOperator.And);

            AddParserToken(new ParserTokenSequence(this), 1);
            AddParserToken(new ParserTokenDelimiter(this, "(", ")"), 2);
            AddParserToken(new ParserTokenBinaryOperator(this, "*"), 4);
            AddParserToken(new ParserTokenBinaryOperator(this, "/"), 4);
            AddParserToken(new ParserTokenBinaryOperator(this, "+"), 5);
            AddParserToken(new ParserTokenBinaryOperator(this, "-"), 5);
            AddParserToken(new ParserTokenBinaryOperator(this, "<="), 6);
            AddParserToken(new ParserTokenBinaryOperator(this, ">="), 6);
            AddParserToken(new ParserTokenBinaryOperator(this, "<"), 6);
            AddParserToken(new ParserTokenBinaryOperator(this, ">"), 6);
            AddParserToken(new ParserTokenBinaryOperator(this, "=="), 7);
            AddParserToken(new ParserTokenBinaryOperator(this, "!="), 7);
            //AddToken(new ParserTokenOperator(this, "^"), 8);
            AddParserToken(new ParserTokenBinaryOperator(this, "&&"), 9);
            AddParserToken(new ParserTokenBinaryOperator(this, "||"), 9);
            //AddToken(new ParserTokenKeyword(this, ""), 10);
            AddParserToken(new ParserTokenValue(this), 12);

            _calculator = new Calculator(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="priority">Plus la valeur est petite plus la priorite est grande</param>
        private void AddParserToken(ParserToken token, int priority)
        {
            if (_tokens.ContainsKey(priority) == true)
            {
                _tokens[priority].Add(token);
            }
            else
            {
                List<ParserToken> list = new List<ParserToken> { token };
                _tokens.Add(priority, list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        public void AddKeywordToken(string keyword)
        {
            AddParserToken(new ParserTokenKeyword(this, keyword), 11);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName"></param>
        public void AddFunctionToken(string functionName)
        {
            AddParserToken(new ParserTokenFunction(this, functionName), 10);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public bool Check(string sentence)
        {
            sentence = sentence.Trim();

            if (string.IsNullOrEmpty(sentence) == true)
            {
                return false;
            }

            //sort the dictionary by priority
            _tokens = _tokens.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return _tokens.Any(pair => pair.Value.Any(token => token.Check(sentence) == true));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private bool Compile(string sentence)
        {
            _calculatorTokens.Clear();

            if (Check(sentence) == true)
            {
                CalculatorToken root;
                CompileCalculatorToken(out root, 0);

                _calculator.Root = root;
                return true;
            }

            _calculatorTokens.Clear();
            _calculator.Root = null;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public float Evaluate(string sentence)
        {
            if (Compile(sentence) == true)
            {
                return _calculator.Evaluate();
            }

            throw new InvalidOperationException("Can't Compile!; Please check your sentence");
        }

        /// <summary>
        /// Used only a sentence is already compiled
        /// </summary>
        /// <returns></returns>
        public float Evaluate()
        {
            if (_calculator.Root == null)
            {
                throw new InvalidOperationException("Compile before use Evaluate()");
            }

            return _calculator.Evaluate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public float EvaluateKeyword(string keyword)
        {
            return KeywordDelegate(keyword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public float EvaluateFunction(string functionName, string[] args)
        {
            return FunctionDelegate(functionName, args);
        }

        #region Compile

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        internal void AddToken(string token)
        {
            _tokensValue.Add(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int CompileCalculatorToken(out CalculatorToken token, int index)
        {
            token = _calculatorTokens[index];

            if (token is CalculatorTokenBinaryOperator)
            {
                index = CompileBinaryOperator(token as CalculatorTokenBinaryOperator, index + 1);
            }
            else if (token is CalculatorTokenSequence)
            {
                CalculatorToken res;
                index = CompileSequence(token as CalculatorTokenSequence, out res, index + 1);
                token = res;
            }

            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int CompileBinaryOperator(CalculatorTokenBinaryOperator parent, int index)
        {
            CalculatorToken left, right;
            CompileCalculatorToken(out left, index);
            index = CompileCalculatorToken(out right, index + 1);

            parent.Left = left;
            parent.Right = right;

            return index + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="res"></param>
        /// <param name="index"></param>
        /// <returns>the index of the end of the sequence</returns>
        private int CompileSequence(CalculatorToken parent, out CalculatorToken res, int index)
        {
            int end = -1;
            res = null;

            for (int i = index; i < _calculatorTokens.Count; i++)
            {
                var token = _calculatorTokens[i] as CalculatorTokenSequence;
                var seq = token;

                if (seq?.Sequence != CalculatorTokenSequence.TokenSequence.StartSequence) continue;

                end = CompileCalculatorToken(out res, i + 1) + 1;
                _calculatorTokens.RemoveRange(i, end - i);

                return i;
            }

            return end;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        internal void AddCalculator(CalculatorToken token)
        {
            _calculatorTokens.Add(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operator_"></param>
        /// <returns></returns>
        internal CalculatorToken GetCalculatorByBinaryOperator(string operator_)
        {
            if (operator_ == null) throw new ArgumentNullException(nameof(operator_));

            return new CalculatorTokenBinaryOperator(_calculator, _mapBinaryOperator[operator_]);
        }

        #endregion

        #region Save/Load

        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        /// <param name="option"></param>
        public void Save(XmlNode el, SaveOption option)
        {
            _calculator.Save(el, option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="el"></param>
        /// <param name="option"></param>
        public void Load(XmlNode el, SaveOption option)
        {
            _calculator.Load(el, option);
        }

        /// <summary>
        /// s
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="option"></param>
        public void Save(BinaryWriter bw, SaveOption option)
        {
            _calculator.Save(bw, option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="el_"></param>
        /// <param name="option"></param>
        public void Load(BinaryReader br, SaveOption option)
        {
            //_calculator.Load(br_, option_);
        }

        #endregion

        #endregion
    }
}
