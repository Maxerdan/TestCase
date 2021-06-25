using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Testcase
{
    static class Exceptions
    {
        public static string CheckForExceptions(string expression)
        {
            var exceptionsDictionary = new Dictionary<string, string>() {
                {@"[\.+\-*\/]{2,}", "OperationException! - Can't parse:\n"},
                {@"[^\d\.+\-*\/()]+", "ParseException! - Unknown symbols:\n"},
                {@"\.[+\-*\/()]", "ParseException! - Unfinished number:\n"},
                {@"\A[+*\/]{1}", "StartException! - Expression starts with the: "},
                {@"[\.+\-*\/]{1}\Z", "EndException! - Expression ends with the: "}
            };
            var exceptions = "";
            var listOfExceptions = "";

            //check for the right input
            foreach(var exc in exceptionsDictionary)
            {
                MatchCollection matches = new Regex(exc.Key).Matches(expression);
                if (matches.Count > 0)
                {
                    exceptions = "";
                    foreach (Match match in matches)
                        exceptions += match.Value + "\n";

                    listOfExceptions += exc.Value + exceptions;
                }
            }

            int leftBracketsCount = expression.Count(x => x == '(');
            int rightBracketsCount = expression.Count(x => x == ')');
            if (leftBracketsCount != rightBracketsCount)
                listOfExceptions += "BracketsException! - Wrong number of left and right brackets";

            if (listOfExceptions != "")
                return listOfExceptions;
            return "";
        }
    }
}
