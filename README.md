# MyLeetcodeProgress

This repository contains a collection of LeetCode problems solved in C#. Each file is a standalone solution for a single LeetCode problem and includes XML documentation comments for the primary method.

**Repository contents**
- `1.two-sum.cs` — Two Sum
- `2.add-two-numbers.cs` — Add Two Numbers
- `9.palindrome-number.cs` — Palindrome Number
- `1262.greatest-sum-divisible-by-three.cs` — Greatest Sum Divisible by Three

**What I added**
- XML documentation comments (`/// <summary>`, `<param>`, `<returns>`) for the public methods in each solution so IDEs (Visual Studio / VS Code) can show method summaries and signatures.

**How to run / experiment locally**

The solutions are class definitions (no `Main` method). To run them quickly, create a small console project that calls the solutions with sample inputs.

Example (PowerShell):

```powershell
# 1) Create a console project
dotnet new console -o runner

# 2) Copy solution files into the project folder
Copy-Item -Path ..\*.cs -Destination runner -Force

# 3) Replace the generated Program.cs with a small runner that invokes a solution.
#    (Edit runner\Program.cs and add code to create inputs and print results.)

# 4) Run the project
dotnet run --project runner
```

If you prefer compiling single files with the C# compiler (MSBuild/Csc) you can create a `Program.cs` with a `Main` method and compile with `dotnet` or Visual Studio.


