// LeetCode 3 — Longest Substring Without Repeating Characters
// URL: https://leetcode.com/problems/longest-substring-without-repeating-characters/
// Difficulty: Medium
// Tags: Hash Table, Two Pointers, String, Sliding Window
// Solved: 2025-11-24
/*
 * @lc app=leetcode id=3 lang=csharp
 *
 * [3] Longest Substring Without Repeating Characters
 */

// @lc code=start
public class Solution {
    public int LengthOfLongestSubstring(string s) {
        string currentSubstring = "";
        int iLongestSubstringLength = 0;
        foreach ( char c in s)
        {
            int existingCharIndex = currentSubstring.IndexOf(c);
            if ( existingCharIndex >= 0)
            {
                currentSubstring = currentSubstring.Substring(existingCharIndex + 1);
            }
            currentSubstring += c;
            iLongestSubstringLength = Math.Max(iLongestSubstringLength, currentSubstring.Length);   
        }
        return iLongestSubstringLength;
    }
}
// @lc code=end

