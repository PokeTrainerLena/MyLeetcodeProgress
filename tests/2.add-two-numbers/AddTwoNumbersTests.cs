using Xunit;

public class AddTwoNumbersTests
{
    private ListNode Build(int[] vals)
    {
        ListNode head = null, cur = null;
        foreach (var v in vals)
        {
            var n = new ListNode(v);
            if (head == null) { head = n; cur = n; }
            else { cur.next = n; cur = n; }
        }
        return head;
    }

    private int[] ToArray(ListNode head)
    {
        var list = new System.Collections.Generic.List<int>();
        while (head != null) { list.Add(head.val); head = head.next; }
        return list.ToArray();
    }

    [Fact]
    public void Example1()
    {
        var sol = new Solution();
        var a = Build(new int[] { 2, 4, 3 });
        var b = Build(new int[] { 5, 6, 4 });
        var sum = sol.AddTwoNumbers(a, b);
        Assert.Equal(new int[] { 7, 0, 8 }, ToArray(sum));
    }
}
