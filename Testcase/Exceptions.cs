﻿using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace Testcase
{
    static class Exceptions
    {
        public static void CheckForExceptions(string expression)
        {
            var exceptionsDictionary = new Dictionary<string, string>() {
                {@"[\.+\-*\/,]{2,}", "OperationException! - Can't parse: "},
                {@"[^\d\.+\-*\/(),]+", "ParseException! - Unknown symbols: "},
                {@"\.[+\-*\/()]", "ParseException! - Unfinished number: "},
                {@",[+\-*\/()]", "ParseException! - Unfinished number: "},
                {@"\A[+*\/\.,]{1}", "StartException! - Expression starts with the: "},
                {@"[\.+\-*\/,]{1}\Z", "EndException! - Expression ends with the: "},
                {@"[\d]\(", "ParseException! - Can't parse: "},
                {@"\)[\d]", "ParseException! - Can't parse: "},
                {@"\(\)", "ParseException! - Can't parse: "}
            };

            foreach (var exc in exceptionsDictionary)
            {
                MatchCollection matches = new Regex(exc.Key).Matches(expression);
                if (matches.Count > 0)
                {
                    throw new Exception(exc.Value + matches[0].Value);
                }
            }

            CheckBrackets(expression);
        }

        private static void CheckBrackets(string expression)
        {
            int leftBracketsCount = expression.Count(x => x == '(');
            int rightBracketsCount = expression.Count(x => x == ')');
            if (leftBracketsCount != rightBracketsCount)
                throw new Exception("BracketsException! - Wrong number of left and right brackets");
        }
    }
}
