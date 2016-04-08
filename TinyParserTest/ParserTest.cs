using NFluent;
using NUnit.Framework;
using TinyParser;

namespace TinyParserTest
{
    public class ParserTest
    {
        private readonly Parser _parser = new Parser();

        [Test]
        [TestCase(8.0f, "8")]
        [TestCase(2.0f, "1+1")]
        [TestCase(2.0f, " 1 + 1 ")]
        [TestCase(48.0f, "8 * 6")]
        [TestCase(4.0f, "7 - 3")]
        [TestCase(-3.0f, "1 - 4")]
        [TestCase(2.0f, "10 / 5")]
        [TestCase(1.0f, "1 == 1")]
        [TestCase(0.0f, "1 != 1")]
        [TestCase(1.0f, "1 != 2")]
        [TestCase(0.0f, "0 || 0")]
        [TestCase(1.0f, "1 || 0")]
        [TestCase(1.0f, "1 || 1")]
        [TestCase(0.0f, "0 && 0")]
        [TestCase(0.0f, "1 && 0")]
        [TestCase(1.0f, "1 && 1")]
        [TestCase(0.0f, "1 < 0")]
        [TestCase(0.0f, "1 < 1")]
        [TestCase(1.0f, "1 < 2")]
        [TestCase(1.0f, "1 <= 1")]
        [TestCase(1.0f, "1 >= 1")]
        public void Should_return_expected(float expected, string sentence)
        {
            Check.That(expected).IsEqualTo(_parser.Evaluate(sentence));
        }

        [Test]
        [TestCase(1.0f, "ctrl")]
        [TestCase(0.0f, "notAKeyword")]
        public void Should_return_expected_when_evaluate_keyword(float expected, string sentence)
        {
            _parser.KeywordDelegate = EvaluateKeyword;
            _parser.AddKeywordToken("ctrl");
            Check.That(expected).IsEqualTo(_parser.Evaluate(sentence));
        }

        [Test]
        [TestCase(1.0f, "command=\"PunchLight\"")]
        [TestCase(0.0f, "notAFunction=\"arg\"")]
        public void Should_return_expected_when_evaluate_function(float expected, string sentence)
        {
            _parser.FunctionDelegate = EvaluateFunction;
            _parser.AddFunctionToken("command");
            Check.That(expected).IsEqualTo(_parser.Evaluate(sentence));
        }

        [Test]
        [TestCase(0.0f, "command=\"PunchLigh")]
        public void Should_return_expected_when_evaluate_function_with_argument_wrong_formatted(float expected, string sentence)
        {
            _parser.FunctionDelegate = EvaluateFunction;
            _parser.AddFunctionToken("command");
            Check.That(expected).IsEqualTo(_parser.Evaluate(sentence));
        }

        [Test]
        [TestCase(0.0f, "command == ctrlPunchLight")]
        public void Should_return_expected_when_evaluate_function_and_keyword(float expected, string sentence)
        {
            _parser.KeywordDelegate = EvaluateKeyword;
            _parser.FunctionDelegate = EvaluateFunction;
            _parser.AddFunctionToken("command");
            _parser.AddKeywordToken("ctrlPunchLight");
            Check.That(expected).IsEqualTo(_parser.Evaluate(sentence));
        }

        static float EvaluateKeyword(string keyword)
        {
            switch (keyword)
            {
                case "ctrl":
                    return 1.0f;
                case "ctrlPunchLight":
                    return 64.0f;
                default:
                    return 0.0f;
            }
        }

        static float EvaluateFunction(string functionName, string[] args)
        {
            if ("command".Equals(functionName) == true)
            {
                if (args.Length > 0)
                {
                    return "PunchLight".Equals(args[0]) ? 1.0f : 0.0f;
                }
                else
                {
                    return 64.0f;
                }
            }

            return 0.0f;
        }

        [Test]
        public void Should_return_false_when_sentence_is_null_or_empty()
        {
            Check.That(false).IsEqualTo(_parser.Check(string.Empty));
            Check.That(false).IsEqualTo(_parser.Check(null));
        }

        [Test]
        public void Should_return_false_when_sentence_is_right_formatted()
        {
            Check.That(true).IsEqualTo(_parser.Check("1"));
        }

        [Test]
        public void Should_return_false_when_sentence_is_wrong_formatted()
        {
            Check.That(false).IsEqualTo(_parser.Check("1 &"));
        }
    }
}
