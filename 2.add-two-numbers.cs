// LeetCode 2 — Add Two Numbers
// URL: https://leetcode.com/problems/add-two-numbers/
// Difficulty: Medium
// Tags: Linked List, Math
// Solved: 2025-11-23
/*
 * @lc app=leetcode id=2 lang=csharp
 *
 * [2] Add Two Numbers
 * */

// @lc code=start
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    /// <summary>
    /// Adds two numbers represented by linked lists <paramref name="l1"/> and <paramref name="l2"/>.
    /// Each node contains a single digit and the digits are stored in reverse order.
    /// The method returns a new list representing the sum in the same reversed digit format.
    /// </summary>
    /// <param name="l1">Head of the first number's linked list (least significant digit first).</param>
    /// <param name="l2">Head of the second number's linked list (least significant digit first).</param>
    /// <returns>Head of a linked list representing the sum of the two numbers.</returns>
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        int result = l1.val + l2.val;
        int carry = result / 10;    
        if(l1.next == null && l2.next == null && carry == 0)
        {
            return new ListNode(result);
        }
        else
        {
            if(carry > 0)
            {
                if(l1.next == null)
                {
                    l1.next = new ListNode(carry);
                }
                else
                {
                    l1.next.val += carry;
                }
            }
            ListNode nextSum = AddTwoNumbers(l1.next??new ListNode(0), l2.next??new ListNode(0));
            
            return new ListNode(result % 10, nextSum);
        }
    }
}
// @lc code=end

