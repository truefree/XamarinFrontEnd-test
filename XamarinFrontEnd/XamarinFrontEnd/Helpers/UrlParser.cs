using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XamarinFrontEnd.Helpers
{
    public static class Parser
    {
        private static readonly Regex GuidRegEx =
            new Regex(
                @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}",
                RegexOptions.IgnoreCase);

        private static readonly Regex EmailRegEx =
            new Regex(
                @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:partner\.)?sk.com$", RegexOptions.IgnoreCase);

        public static Guid? ParseGuid(string url)
        {
            Guid guid;
            if (TryParseGUID(url, out guid))
            {
                return guid;
            }
            return null;
        }

        public static bool IsEmailString(string txt)
        {
            return EmailRegEx.IsMatch(txt);
        }

        public static string ParseGuidToString(string url)
        {
            Guid guid;
            if(TryParseGUID(url, out guid))
            {
                return guid.ToString();
            }
            return string.Empty;
        }

        public static bool TryParseGUID(string candidate, out Guid output)
        {
            bool isValid = false;
            output = Guid.Empty;
            if(candidate != null)
            {
                if(GuidRegEx.IsMatch(candidate))
                {
                    try
                    {
                        output = new Guid(GuidRegEx.Match(candidate).Value);
                        isValid = true;
                    }
                    catch(Exception)
                    {
                    }
                }
            }
            return isValid;
        }
    }
}
