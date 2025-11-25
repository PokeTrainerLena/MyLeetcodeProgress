# MyLeetcodeProgress

This repository contains a collection of LeetCode problems solved in C#. Each file is a standalone solution for a single LeetCode problem and includes XML documentation comments for the primary method.

**Repository contents**
- `1.two-sum.cs` — Two Sum
- `2.add-two-numbers.cs` — Add Two Numbers
- `9.palindrome-number.cs` — Palindrome Number
- `1262.greatest-sum-divisible-by-three.cs` — Greatest Sum Divisible by Three

**How to run / experiment locally**

The solutions are class definitions (no `Main` method). To run a solution quickly, create a small console project, copy the desired solution file(s) into it and call the solution from `Program.cs`.

Short examples (PowerShell):

Create a temporary runner project and run one solution:

```powershell
dotnet new console -o runner
Copy-Item -Path ..\1.two-sum.cs -Destination runner -Force
# edit runner\Program.cs to call TwoSum.Solution or similar
dotnet run --project runner
```

Run a single test project (recommended):

```powershell
dotnet test tests\1015.smallest-integer-divisible-by-k
```

Run all tests (requires the solution file):

```powershell
dotnet test
```

Create a repository solution and add all test projects:

```powershell
dotnet new sln -n MyLeetcodeProgress
Get-ChildItem -Path .\tests -Recurse -Filter *.csproj | ForEach-Object { dotnet sln add $_.FullName }
dotnet test
```

Validator (metadata)
- Use the included validator to ensure per-file headers and `problems.yml` stay in sync. Run the wrapper from PowerShell to avoid execution policy changes:

```powershell
.\scripts\validate_metadata.cmd
```

Adding problems
- Add the solution file named `NNNN.title.cs` with header metadata lines (see examples in repo).
- Add a matching entry to `problems.yml`.
- Add an xUnit test project under `tests/` named exactly like the solution file and link the `.cs` file in the test project.
- Run the validator.

Support & notes
- Tests target `net6.0` and use xUnit; adjust test csproj targets if your environment differs.
- For numeric problems: the iterative-remainder approach is often preferred for correctness and performance; BigInteger is available for large-number requirements and is used where requested.

---

Generated/updated on 2025-11-25


