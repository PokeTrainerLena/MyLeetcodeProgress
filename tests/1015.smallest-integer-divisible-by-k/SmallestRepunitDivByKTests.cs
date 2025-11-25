using Xunit;

public class SmallestRepunitDivByKTests
{
    [Fact]
    public void MultipleCases()
    {
        int[] ks = new[] { 1, 2, 3, 7, 13, 17, 19, 23, 37, 41 };

        var sol = new Solution();
        foreach (var k in ks)
        {
            var res = sol.SmallestRepunitDivByK(k);

            int expected;
            if (k % 2 == 0 || k % 5 == 0)
            {
                expected = -1;
            }
            else
            {
                int rem = 0;
                expected = -1;
                // By pigeonhole, the length (if exists) will be <= k
                for (int len = 1; len <= k; len++)
                {
                    rem = (rem * 10 + 1) % k;
                    if (rem == 0)
                    {
                        expected = len;
                        break;
                    }
                }
            }

            Assert.Equal(expected, res);
        }
    }
}
