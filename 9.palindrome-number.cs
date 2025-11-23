// LeetCode 9 — Palindrome Number
// URL: https://leetcode.com/problems/palindrome-number/
// Difficulty: Easy
// Tags: Math
// Solved: 2025-11-23
/*
 * @lc app=leetcode id=9 lang=csharp
 *
 * [9] Palindrome Number
 * */

// @lc code=start
public class Solution {
    /// <summary>
    /// Determines whether the integer <paramref name="x"/> reads the same backwards and forwards.
    /// </summary>
    /// <param name="x">Integer to test for palindrome property.</param>
    /// <returns>True if <paramref name="x"/> is a palindrome, otherwise false.</returns>
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

