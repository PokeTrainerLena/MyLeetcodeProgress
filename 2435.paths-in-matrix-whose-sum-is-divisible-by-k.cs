/*
 * @lc app=leetcode id=2435 lang=csharp
 *
 * [2435] Paths in Matrix Whose Sum Is Divisible by K
 */

// @lc code=start

/*
Notes: 
Given a m x n matrix of integers grid and an integer k, return the number of paths from the top-left to the bottom-right where the values of that path are divisible by k.
a 3x3 matrix has 6 paths from top-left to bottom-right.
a 2x1 matrix has 1 path from top-left to bottom-right.
a 3x4 matrix has 10 paths from top-left to bottom-right.
a 1x4 matrix has 1 path from top-left to bottom-right.
a 4x4 matrix has 20 paths from top-left to bottom-right.


example:
[[ 5,2,4],
 [3,0,5],
 [0,7,2]]

 Paths: 
 Step 1: [(5)]
 Step 2: [(5,2),(5,3)]
 Step 3: [(5,2,4),(5,2,0),(5,3,0),(5,3,0)]
*/
public class Solution {
    private const int MOD = 1000000007;
    
    public int NumberOfPaths(int[][] grid, int k) {
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            return 0;
        if (m == 1 && n == 1)
            return (startSumMod == 0) ? 1 : 0;  
        int m = grid.Length;
        int n = grid[0].Length;
        
        // Use dictionary to deduplicate paths with same state (position + sum mod k)
        Dictionary<(int x, int y, int sumMod), long> currentPaths = new Dictionary<(int, int, int), long>();
        int startSumMod = ((grid[0][0] % k) + k) % k;  // Normalize to [0, k-1]
        currentPaths[(0, 0, startSumMod)] = 1;
        
        // Process each step
        int totalSteps = m + n - 2;
        for (int step = 0; step < totalSteps; step++)
        {
            Dictionary<(int x, int y, int sumMod), long> nextPaths = new Dictionary<(int, int, int), long>();
            
            foreach (var kvp in currentPaths)
            {
                Path path = new Path(kvp.Key.x, kvp.Key.y, kvp.Key.sumMod, kvp.Value);
                var newPaths = path.AddStep(grid, k);
                
                // Merge paths into next step
                foreach (var newPath in newPaths)
                {
                    var key = (newPath.X, newPath.Y, newPath.SumMod);
                    if (!nextPaths.ContainsKey(key))
                        nextPaths[key] = 0;
                    nextPaths[key] = (nextPaths[key] + newPath.Count) % MOD;
                }
            }
            
            currentPaths = nextPaths;
        }
        
        // Sum all paths that end at bottom-right with sum divisible by k
        long result = 0;
        
        // Debug: if no paths after loop, something went wrong
        if (currentPaths.Count == 0)
            return 0;
            
        foreach (var kvp in currentPaths)
        {
            // Check if at destination with correct sum mod
            if (kvp.Key.x == m - 1 && kvp.Key.y == n - 1 && kvp.Key.sumMod == 0)
                result = (result + kvp.Value) % MOD;
        }
        return (int)result;
    }
    
    private class Path
    {
        public int X;
        public int Y;
        public int SumMod;  // Sum modulo k (only need remainder)
        public long Count;   // Number of paths with this state (use long to prevent overflow)
        
        public Path(int x, int y, int sumMod, long count = 1)
        {
            X = x;
            Y = y;
            SumMod = sumMod;
            Count = count;
        }
        
        public List<Path> AddStep(int[][] grid, int k)
        {
            List<Path> newPaths = new List<Path>();
            int m = grid.Length;
            int n = grid[0].Length;
            
            // Try moving down
            if (X + 1 < m)
            {
                int newSumMod = (SumMod + grid[X + 1][Y]) % k;
                // Handle negative modulo
                if (newSumMod < 0) newSumMod += k;
                newPaths.Add(new Path(X + 1, Y, newSumMod, Count));
            }
            
            // Try moving right
            if (Y + 1 < n)
            {
                int newSumMod = (SumMod + grid[X][Y + 1]) % k;
            
                newPaths.Add(new Path(X, Y + 1, newSumMod, Count));
            }
            
            return newPaths;
        }
    }
}
// @lc code=end

