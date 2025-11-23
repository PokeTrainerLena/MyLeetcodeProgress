// LeetCode 1 — Two Sum
// URL: https://leetcode.com/problems/two-sum/
// Difficulty: Easy
// Tags: Array, Hash Table
// Solved: 2025-11-23
/*
 * @lc app=leetcode id=1 lang=csharp
 *
 * [1] Two Sum
 * */

// @lc code=start
public class Solution {
    /// <summary>
    /// Finds two indices in <paramref name="nums"/> whose values add up to <paramref name="target"/>.
    /// This implementation uses a brute-force search over pairs.
    /// </summary>
    /// <param name="nums">Array of integers to search.</param>
    /// <param name="target">Target sum to find.</param>
    /// <returns>An array with two indices [i, j] where nums[i] + nums[j] == target, or an empty array if none found.</returns>
    public int[] TwoSum(int[] nums, int target) {
        for ( int i=0; i <nums.Count()-1;i++)
        {
            for ( int j=i+1; j <nums.Count();j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    return new int[] { i,j };
                }
            }
        }
        return new int[] { };
    }
}
// @lc code=end

