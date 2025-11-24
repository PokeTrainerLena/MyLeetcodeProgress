using Xunit;

public class GreatestSumDivisibleByThreeTests
{
    [Fact]
    public void Example1()
    {
        var sol = new Solution();
        int[] nums = { 3, 6, 5, 1, 8 };
        Assert.Equal(18, sol.MaxSumDivThree(nums));
    }
}
