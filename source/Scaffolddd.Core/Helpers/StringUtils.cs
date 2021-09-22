using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Scaffolddd.Core.Helpers
{
    public static class StringUtils
    {

        
        public static string ReplaceWholeWords(string input, string oldtext, string replace, RegexOptions options )
        {
            string pattern = @"\b"+oldtext+@"\b";
            string result = Regex.Replace(input, pattern, replace, options);
            return result;
        }


        public static string Replace(string text, Dictionary<string, string> lstSwap)
        {
            var textStart = text;

            foreach (var item in lstSwap)
            {
                textStart = ReplaceWholeWords(textStart,item.Key, item.Value, RegexOptions.IgnoreCase);
            }

            return textStart;
        }

        public static string RemoveWhitespace(this string input)
        {
           return new string(input.ToCharArray()
               .Where(c => !Char.IsWhiteSpace(c))
               .ToArray());
        }

        public static string RemoveBreakLine(this string input)
        {
            return input.Replace(System.Environment.NewLine, input); //add a line terminating ;
        }

        public static bool CompareString(string text1, string text2)
        {
            return (text1.RemoveBreakLine().RemoveWhitespace().ToUpper() == text2.RemoveBreakLine().RemoveWhitespace().ToUpper());
        }
    }
}