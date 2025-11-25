// LeetCode 13 — Roman to Integer
// URL: https://leetcode.com/problems/roman-to-integer/
// Difficulty: Easy
// Tags: String, Math
// Solved: 2025-11-25
/*
 * @lc app=leetcode id=13 lang=csharp
 *
 * [13] Roman to Integer


 Constraints:

1 <= s.length <= 15
s contains only the characters ('I', 'V', 'X', 'L', 'C', 'D', 'M').
It is guaranteed that s is a valid roman numeral in the range [1, 3999].
 */

// @lc code=start
public class Solution {
    public int RomanToInt(string s) {
        int iReturnValue = 0;
       RomanNumeral[] romanNumerals = new RomanNumeral[]
       {
           new RomanNumeral('I', 1),
           new RomanNumeral('V', 5),
           new RomanNumeral('X', 10),
           new RomanNumeral('L', 50),
           new RomanNumeral('C', 100),
           new RomanNumeral('D', 500),
           new RomanNumeral('M', 1000)
       };
         for ( int i=0; i < s.Length; i++)
         {
              RomanNumeral currentNumeral = romanNumerals.First(rn => rn.cSymbol == s[i]);
              if ( i < s.Length -1)
              {
                RomanNumeral nextNumeral = romanNumerals.First(rn => rn.cSymbol == s[i+1]);
                if ( nextNumeral.iValue > currentNumeral.iValue)
                {
                     iReturnValue += (nextNumeral.iValue - currentNumeral.iValue);
                     i++;
                     continue;
                }
              }
              iReturnValue += currentNumeral.iValue;
         }
            return iReturnValue;
    }
    public class RomanNumeral
    {
        public char cSymbol { get; set; }
        public int iValue { get; set; }
        public RomanNumeral(char symbol, int value)
        {
            cSymbol = symbol;
            iValue = value;
        }
    }
}
// @lc code=end

