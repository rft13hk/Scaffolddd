using System.Collections.Generic;
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
    }
}