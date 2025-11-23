/*
 * @lc app=leetcode id=1262 lang=csharp
 *
 * [1262] Greatest Sum Divisible by Three
 */

// @lc code=start
public class Solution
{
    public int MaxSumDivThree(int[] nums)
    {
        int iSum = nums.Sum();
        if (iSum % 3 == 0) return iSum;
        else if (iSum % 3 == 1)
        {
            var iMinMod1List = nums.Where(n => n % 3 == 1);
            var iMinMod1 = iMinMod1List != null && iMinMod1List.Count() >= 1 ? iMinMod1List.Min() : int.MaxValue;
            var iMinMod2List = nums.Where(n => n % 3 == 2);
            var iMinMod2 = iMinMod2List != null && iMinMod2List.Count() >= 2 ? iMinMod2List.OrderBy(n => n).Take(2).Sum() : int.MaxValue;
            iSum -= Math.Min(iMinMod1, iMinMod2);
        }
        else if (iSum % 3 == 2)
        {
            var iMinMod2List = nums.Where(n => n % 3 == 2);
            var iMinMod2 = iMinMod2List != null && iMinMod2List.Count() >= 1 ? iMinMod2List.Min() : int.MaxValue;
            var iMinMod1List = nums.Where(n => n % 3 == 1);
            var iMinMod1 = iMinMod1List != null && iMinMod1List.Count() >= 2 ? iMinMod1List.OrderBy(n => n).Take(2).Sum() : int.MaxValue;
            iSum -= Math.Min(iMinMod1, iMinMod2);
        }
        return iSum > 0 ? iSum : 0;
    }
}
// @lc code=end

