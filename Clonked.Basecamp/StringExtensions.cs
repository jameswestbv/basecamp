using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp
{
    public static class StringExtensions
    {
        public static string FormatWith(this string s, object arg0)
        {
            return string.Format(s, arg0);
        }

        public static string FormatWith(this string s, object arg0, object arg1)
        {
            return string.Format(s, arg0, arg1);
        }

        public static string FormatWith(this string s, object arg0, object arg1, object arg2)
        {
            return string.Format(s, arg0, arg1, arg2);
        }

        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        //from http://www.aspcode.net/ProperCase-function-in-C.aspx
        public static string ToProperCase(this string s)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool fEmptyBefore = true;
            foreach (char ch in s)
            {
                char chThis = ch;
                if (Char.IsWhiteSpace(chThis))
                    fEmptyBefore = true;
                else
                {
                    if (Char.IsLetter(chThis) && fEmptyBefore)
                        chThis = Char.ToUpper(chThis);
                    else
                        chThis = Char.ToLower(chThis);
                    fEmptyBefore = false;
                }
                sb.Append(chThis);
            }
            return sb.ToString();  
        }
    }
}
