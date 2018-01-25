using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace eCafe.Infrastructure.Extensions
{
    public static class Utils
    {
        /// <summary>
        /// Returns the number of steps required to transform the source string
        /// into the target string.
        /// </summary>
        public static int ComputeLevenshteinDistance(this string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 0 : target.Length;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 0 : source.Length;

            var sourceLength = source.Length;
            var targetLength = target.Length;

            var distance = new int[sourceLength + 1, targetLength + 1];

            // Step 1
            for (var i = 0; i <= sourceLength; distance[i, 0] = i++) ;
            for (var j = 0; j <= targetLength; distance[0, j] = j++) ;

            for (var i = 1; i <= sourceLength; i++)
            {
                for (var j = 1; j <= targetLength; j++)
                {
                    // Step 2
                    var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 3
                    distance[i, j] = Math.Min(
                                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceLength, targetLength];
        }

        /// <summary> 
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        public static double CalculateSimilarity(this string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 1 : 0;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 1 : 0;

            double stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - (stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        /// <summary>
        /// Returns similarity between two strings, which we will refer to as the source string (s1) and the target string (s2).
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static float Distance(string s1, string s2)
        {
            var maxOffset = 5;
            if (string.IsNullOrEmpty(s1))
                return
                string.IsNullOrEmpty(s2) ? 0 : s2.Length;
            if (string.IsNullOrEmpty(s2))
                return s1.Length;
            var c = 0;
            var offset1 = 0;
            var offset2 = 0;
            var dist = 0;
            while ((c + offset1 < s1.Length)
            && (c + offset2 < s2.Length))
            {
                if (s1[c + offset1] != s2[c + offset2])
                {
                    offset1 = 0;
                    offset2 = 0;
                    for (var i = 0; i < maxOffset; i++)
                    {
                        if ((c + i < s1.Length)
                        && (s1[c + i] == s2[c]))
                        {
                            if (i > 0)
                            {
                                dist++;
                                offset1 = i;
                            }
                            goto ender;
                        }
                        if ((c + i < s2.Length)
                        && (s1[c] == s2[c + i]))
                        {
                            if (i > 0)
                            {
                                dist++;
                                offset2 = i;
                            }
                            goto ender;
                        }
                    }
                    dist++;
                }
                ender:
                c++;
            }
            return dist + (s1.Length - offset1
            + s2.Length - offset2) / 2 - c;
        }

        /// <summary>
        /// Calculate percentage similarity of two strings
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static float Similarity(this string s1, string s2)
        {
            var dis = Distance(s1, s2);
            var maxLen = Math.Max(s1.Length, s2.Length);
            if (maxLen == 0) return 1;
            else
                return 1 - dis / maxLen;
        }      

        
    }
}
