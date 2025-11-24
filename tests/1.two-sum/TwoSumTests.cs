using Xunit;

public class TwoSumTests
{
    [Fact]
    public void Example1()
    {
        var sol = new Solution();
        int[] nums = { 2, 7, 11, 15 };
        var res = sol.TwoSum(nums, 9);
        Assert.Equal(new int[] { 0, 1 }, res);
    }
}
