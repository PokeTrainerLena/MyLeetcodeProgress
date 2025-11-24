using System.Linq;
using Xunit;

public class PrefixDivisibleBy5Tests
{
    [Fact]
    public void Example1()
    {
        var sol = new Solution();
        int[] nums = { 0, 1, 1 };
        var res = sol.PrefixesDivBy5(nums);
        Assert.Equal(new bool[] { true, false, false }, res.ToArray());
    }

    [Fact]
    public void LargeCase()
    {
        var sol = new Solution();
        int[] nums = { 1,0,0,1,0,1,0,0,1,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,1,0,0,0,0,1,1,0,1,0,0,0 };
        var res = sol.PrefixesDivBy5(nums).ToArray();
        // Compute expected values using modulo accumulation (independent of solution helper)
        var expected = new bool[nums.Length];
        int mod = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            mod = (mod * 2 + nums[i]) % 5;
            expected[i] = (mod == 0);
        }
        Assert.Equal(expected, res);
    }
}
