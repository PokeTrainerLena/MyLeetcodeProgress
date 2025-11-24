using Xunit;

public class PalindromeTests
{
    [Theory]
    [InlineData(121, true)]
    [InlineData(-121, false)]
    [InlineData(10, false)]
    [InlineData(0, true)]
    public void TestIsPalindrome(int input, bool expected)
    {
        var s = new Solution();
        var result = s.IsPalindrome(input);
        Assert.Equal(expected, result);
    }
}
