// LeetCode 1015 — Smallest Integer Divisible by K
// URL: https://leetcode.com/problems/smallest-integer-divisible-by-k/
// Difficulty: Medium
// Tags: Math, Number Theory
// Solved: 2025-11-25
/*
 * @lc app=leetcode id=1015 lang=csharp
 *
 * [1015] Smallest Integer Divisible by K

 Given a positive integer k, you need to find the length of the smallest positive integer n such that n is divisible by k, and n only contains the digit 1.

Return the length of n. If there is no such n, return -1.
Note: n may not fit in a 64-bit signed integer.
 */

using System.Numerics;

// @lc code=start
public class Solution {
    public int SmallestRepunitDivByK(int k) {
        BigInteger n = 0;
        if( k % 2 == 0 || k % 5 == 0)
        {
            return -1;
        }  
        for (int iReturnLength = 1; iReturnLength <= k;iReturnLength++)
        {
            n = n*10 + 1;
            if ((n % k) == 0)
            {
                return iReturnLength;
            }
            
        }
        return -1;
    }
}
// @lc code=end

