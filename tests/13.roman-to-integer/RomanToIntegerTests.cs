using Xunit;

public class RomanToIntegerTests
{
    [Theory]
    [InlineData("III", 3)]
    [InlineData("IV", 4)]
    [InlineData("IX", 9)]
    [InlineData("LVIII", 58)]
    [InlineData("MCMXCIV", 1994)]
    public void Examples(string s, int expected)
    {
        var sol = new Solution();
        var result = sol.RomanToInt(s);
        Assert.Equal(expected, result);
    }
}
