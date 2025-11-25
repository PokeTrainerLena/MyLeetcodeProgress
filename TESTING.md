# Unified Testing Approach

Instead of creating individual test projects for each problem, add test cases to `test_cases.yml` and one line to the unified test. This approach uses **Roslyn** to dynamically compile solution files at runtime, allowing all solutions to coexist without naming conflicts.

## Adding a New Problem

1. **Add test cases** to `test_cases.yml`:

```yaml
- id: 14
  method_name: LongestCommonPrefix
  cases:
    - input: [["flower","flow","flight"]]
      expected: "fl"
    - input: [["dog","racecar","car"]]
      expected: ""
```

2. **Add one line** to `tests/UnifiedTests/UnifiedSolutionTests.cs`:

```csharp
[InlineData(14, "LongestCommonPrefix")]
```

3. **Run tests**:

```powershell
dotnet test tests/UnifiedTests
```

That's it! No need to create a whole test project structure.

## Supported Types

**Input/Output Types:**
- Integers: `42`, `-121`
- Booleans: `true`, `false`
- Strings: `"III"`, `"MCMXCIV"`
- Integer arrays: `[1, 2, 3]`
- String arrays: `["a", "b", "c"]`
- Nested arrays: `[[1, 2], [3, 4]]`

## Current Working Examples

### Problem 1: Two Sum
```yaml
- id: 1
  method_name: TwoSum
  cases:
    - input: [[2, 7, 11, 15], 9]
      expected: [0, 1]
```

### Problem 9: Palindrome Number
```yaml
- id: 9
  method_name: IsPalindrome
  cases:
    - input: [121]
      expected: true
    - input: [-121]
      expected: false
```

### Problem 13: Roman to Integer
```yaml
- id: 13
  method_name: RomanToInt
  cases:
    - input: ["III"]
      expected: 3
    - input: ["MCMXCIV"]
      expected: 1994
```

## Notes

- Test cases support basic types (int, string, bool) and arrays
- For complex types (like LinkedList), you'll still need individual test projects
- The unified test compiles each solution file on demand using Roslyn
- All three current tests (1, 9, 13) are passing ✅
- Solutions automatically get `using System.Linq;` added if needed
- for individual tests:  dotnet test tests\UnifiedTests --filter "DisplayName~14"