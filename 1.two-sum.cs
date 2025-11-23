/*
 * @lc app=leetcode id=1 lang=csharp
 *
 * [1] Two Sum
 */

// @lc code=start
public class Solution {
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

