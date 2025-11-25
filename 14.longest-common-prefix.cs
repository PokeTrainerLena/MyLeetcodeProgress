// LeetCode 14 — Longest Common Prefix
// URL: https://leetcode.com/problems/longest-common-prefix/
// Difficulty: Easy
// Tags: String
// Solved: 2025-11-25
/*
 * @lc app=leetcode id=14 lang=csharp
 *
 * [14] Longest Common Prefix

Write a function to find the longest common prefix string amongst an array of strings.

If there is no common prefix, return an empty string "".

Constraints:
1 <= strs.length <= 200
0 <= strs[i].length <= 200
strs[i] consists of only lowercase English letters if it is non-empty.
 */

// @lc code=start
public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        string sReturn = "";
       for (int i=0; i < strs.Min(s => s.Length); i++)
        {
            if( strs.All(str => str[i] == strs[0][i]))
            {
                sReturn += strs[0][i];
            }
            else
            {
                break;
            }
        }
        return sReturn;

    }
}
// @lc code=end

