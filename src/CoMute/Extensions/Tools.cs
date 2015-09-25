using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Tools
{
    public static string FormalFormat(this string inString)
    {
        string outString = string.Empty;
        string _ErrorMessage = string.Empty;
        try
        {
            // Formal Format is made for names and addresses to assure 
            // proper formatting and capitalization
            if (string.IsNullOrEmpty(inString))
            {
                return string.Empty;
            }
            inString = inString.Trim();
            if (string.IsNullOrEmpty(inString))
            {
                return string.Empty;
            }
            // see if this is a word or a series of words
            //if(inString.IndexOf(" ") > 0)
            //{
            // Break out each word in the string. 
            char[] charSep = { ' ' };
            string[] aWords = inString.Split(charSep);
            int i = 0;
            int CapAfterHyphen = 0;
            for (i = 0; i < aWords.Length; i++)
            {

                string Word = aWords[i].Trim();
                CapAfterHyphen = Word.IndexOf("-");
                char[] chars = Word.ToCharArray();
                if (chars.Length > 3)
                {
                    if (Char.IsLower(chars[1]) && Char.IsUpper(chars[2]))
                    {
                        Word = Word.Substring(0, 1).ToUpper() + Word.Substring(1, 1).ToLower() + Word.Substring(2, 1).ToUpper() + Word.Substring(3).ToLower();
                    }
                    else
                    {
                        Word = Word.Substring(0, 1).ToUpper() + Word.Substring(1).ToLower();
                    }
                }
                if (CapAfterHyphen > 0)
                {
                    Word = Word.Substring(0, CapAfterHyphen + 1) + Word.Substring(CapAfterHyphen + 1, 1).ToUpper() + Word.Substring(CapAfterHyphen + 2);
                }
                if (i > 0)
                {
                    outString += " " + Word;
                }
                else
                {
                    outString = Word;
                }
            }
        }
        catch (Exception e)
        {
            outString = inString;
            _ErrorMessage = e.Message;
        }
        return outString;
    }
}
