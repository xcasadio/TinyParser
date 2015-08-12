[TinyParser](https://github.com/xcasadio/TinyParser) -A tiny parser developed in C#
===================================================================================

Features:
 * Mathematics operators : +, -, *, /, ==, !=, <, >, <=, >=, &&, ||
 * Function function_name=arg1,arg2,...
 * Keywords

The functions and the key words are evaluated by a delegate that you pass to the Parser.

Examples:
 * "1 + 1" returns 2
 * "10 / 5" returns 2
 * "1 != 2" returns 1
 * "1 < 2" returns 1
 * "some_function="true"" returns 1
 * "keyword_equals_to_0" returns 0
