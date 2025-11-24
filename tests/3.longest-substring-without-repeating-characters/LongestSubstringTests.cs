using Xunit;

public class LongestSubstringTests
{
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("", 0)]
    public void Examples(string s, int expected)
    {
        var sol = new Solution();
        var result = sol.LengthOfLongestSubstring(s);
        Assert.Equal(expected, result);
    }
}
