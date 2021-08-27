using System.Collections.Generic;

namespace Scaffolddd.Core.Helpers
{
    public static class StringUtils
    {
        public static string Replace(string text, Dictionary<string, string> lstSwap)
        {
            var newtext = text;

            

            foreach (var item in lstSwap)
            {
                int p=0;

                while (p>=0) 
                {
                    p=newtext.IndexOf(item.Key,p);    

                    if (p>=0 && ( !char.IsLetterOrDigit(newtext[p-1]) && !char.IsLetterOrDigit(newtext[ p + item.Key.Length ])))
                    {
                        newtext = newtext.Replace(item.Key , item.Value); 
                    }

                    if(p>=0) p++;
                }

            }

            return newtext;
        }
    }
}