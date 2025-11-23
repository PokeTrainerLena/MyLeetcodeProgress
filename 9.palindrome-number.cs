/*
 * @lc app=leetcode id=9 lang=csharp
 *
 * [9] Palindrome Number
 */

// @lc code=start
public class Solution {
    public bool IsPalindrome(int x) {
        string sInput = x.ToString();
        for ( int i=0; i < sInput.Length / 2; i++)
        {
            if (sInput[i] != sInput[sInput.Length -1 -i])
            {
                return false;
            }
        }
        return true;
    }
}
// @lc code=end

