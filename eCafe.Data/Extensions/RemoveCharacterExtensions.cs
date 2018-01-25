using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace eCafe.Infrastructure.Extensions
{
    public static class RemoveCharacterExtensions
    {
        public static string RemoveLastCharacter(this String instr)
        {
            return instr.Substring(0, instr.Length - 1);
        }
        public static string RemoveLast(this String instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }
        public static string RemoveFirstCharacter(this String instr)
        {
            return instr.Substring(1);
        }
        public static string RemoveFirst(this String instr, int number)
        {
            return instr.Substring(number);
        }

        /// <summary>
        /// Trim the string and reduce the whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAndReduce(this string str)
        {
            return ConvertWhitespacesToSingleSpaces(str).Trim();
        }

        /// <summary>
        /// reduce the whitespace to single white space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", string.Empty);
        }

        /// <summary>
        /// remove all white space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveAllWhiteSpace(this string value)
        {
            return Regex.Replace(value, @"\s+", "");
        }

        /// <summary>
        /// remove all special charters from string except numbers and alphabets
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharcters(this string text)
        {
            return Regex.Replace(text, "[^0-9A-Za-z ,]", string.Empty);
        }

        /// <summary>
        /// replace repetitive characters into single characters
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveRepeativeCharctersIfGreaterThen2(this string text)
        {
            var r = new Regex("(.)(?<=\\1\\1\\1)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(text, string.Empty);
        }

        /// <summary>
        /// Remove white space and also remove repetitive characters
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public static string CleanData(this string SearchText)
        {
            //White Space & Special Characters have been removed
            var arr = SearchText.Where(c => (char.IsLetterOrDigit(c) &&
                                                !char.IsWhiteSpace(c) &&
                                                !char.IsSymbol(c))).ToArray();

            var res = new string(arr);
            //Removing Repetitive Characters
            res.Distinct()
            .Select(c => c.ToString())
            .ToList()
            .ForEach(c =>
            {
                while (res.Contains(c + c))
                    res = res.Replace(c + c, c);
            });

            return res;
        }

        /// <summary>
        /// list of matching words
        /// </summary>
        /// <returns></returns>
        public static List<string> FilteredWords()
        {
            var list = new List<string> { "the", "as", "if" };
            return list;
        }

        /// <summary>
        ///  remove matching word from string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveMatchedWord(this string text)
        {
            var result = text
                        .Split(' ')
                        .Where(item => !FilteredWords().Contains(item.ToLower()))
                        .Select(x => x)
                        .ToList();
            return string.Join(" ", result.ToArray()); ;
        }

        /// <summary>
        /// Removes duplicate chars using string concats
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string MakeDistinct(this string key)
        {
            // Store encountered letters in this string.
            var table = "";

            // Store the result in this string.
            var result = "";

            // Loop over each character.
            foreach (var value in key)
            {
                // See if character is in the table.
                if (table.IndexOf(value) == -1)
                {
                    // Append to the table and the result.
                    table += value;
                    result += value;
                }
            }
            return result;
        }

        /// <summary>
        /// remove duplicate characters
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RemoveDuplicateChars(this string key)
        {

            var res = key.Split(' ').Select(x => x.MakeDistinct()).ToList();
            return string.Join(" ", res);
        }

        /// <summary>
        /// remove all match word, duplicate characters and special characters
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToPlanText(this string text)
        {
            return text.RemoveMatchedWord()
                   .RemoveDuplicateChars()
                   .RemoveSpecialCharcters();
        }
    }

}
