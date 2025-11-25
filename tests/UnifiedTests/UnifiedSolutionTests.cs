using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

public class UnifiedSolutionTests
{
    private readonly Dictionary<int, ProblemTestCases> _testCases;
    private readonly string _repoRoot;

    public UnifiedSolutionTests()
    {
        _repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
        var testCasesPath = Path.Combine(AppContext.BaseDirectory, "test_cases.yml");
        
        if (File.Exists(testCasesPath))
        {
            var yaml = File.ReadAllText(testCasesPath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            _testCases = deserializer.Deserialize<List<ProblemTestCases>>(yaml)
                .ToDictionary(p => p.Id);
        }
        else
        {
            _testCases = new Dictionary<int, ProblemTestCases>();
        }
    }

    private Assembly CompileSolution(int problemId)
    {
        var solutionFile = Directory.GetFiles(_repoRoot, $"{problemId}.*.cs").FirstOrDefault();
        if (solutionFile == null)
            throw new FileNotFoundException($"Solution file for problem {problemId} not found");

        var code = File.ReadAllText(solutionFile);
        
        // Add necessary using directives if not present
        if (!code.Contains("using System;"))
        {
            code = "using System;\n" + code;
        }
        if (!code.Contains("using System.Linq"))
        {
            code = "using System.Linq;\n" + code;
        }
        if (!code.Contains("using System.Collections.Generic"))
        {
            code = "using System.Collections.Generic;\n" + code;
        }
        
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        
        var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Math).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Collections").Location),
        };
        
        // Add System.Numerics reference if the code uses BigInteger
        if (code.Contains("BigInteger"))
        {
            references.Add(MetadataReference.CreateFromFile(Assembly.Load("System.Runtime.Numerics").Location));
        }

        var compilation = CSharpCompilation.Create(
            $"Problem{problemId}",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);
        
        if (!result.Success)
        {
            var errors = string.Join("\n", result.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => d.ToString()));
            throw new InvalidOperationException($"Compilation failed:\n{errors}");
        }

        ms.Seek(0, SeekOrigin.Begin);
        return Assembly.Load(ms.ToArray());
    }

    [Theory]
    [InlineData(1, "TwoSum")]
    [InlineData(9, "IsPalindrome")]
    [InlineData(13, "RomanToInt")]
    [InlineData(1015, "SmallestRepunitDivByK")]
    [InlineData(1018, "PrefixesDivBy5")]
    [InlineData(1262, "MaxSumDivThree")]
    public void TestSolution(int problemId, string methodName)
    {
        if (!_testCases.ContainsKey(problemId))
        {
            // Skip if no test cases defined
            return;
        }

        var testCase = _testCases[problemId];
        var assembly = CompileSolution(problemId);
        var solutionType = assembly.GetType("Solution");
        Assert.NotNull(solutionType);
        
        var instance = Activator.CreateInstance(solutionType);
        var method = solutionType.GetMethod(methodName);
        Assert.NotNull(method);
        
        // Run test cases
        foreach (var tc in testCase.Cases)
        {
            var parameters = method.GetParameters();
            var args = new object[parameters.Length];
            
            // Convert input objects to match parameter types
            for (int i = 0; i < parameters.Length; i++)
            {
                var paramType = parameters[i].ParameterType;
                var inputValue = tc.Input[i];
                
                args[i] = ConvertToParameterType(inputValue, paramType);
            }
            
            var result = method.Invoke(instance, args);
            
            // Compare results with proper type handling
            CompareResults(tc.Expected, result);
        }
    }

    private void CompareResults(object expected, object? actual)
    {
        if (actual is System.Collections.IList actualList && expected is List<object> expectedObjList)
        {
            // Handle IList<bool> and other IList types (check before Array to catch bool[])
            Assert.Equal(expectedObjList.Count, actualList.Count);
            for (int i = 0; i < expectedObjList.Count; i++)
            {
                var actualItem = actualList[i];
                var expectedItem = expectedObjList[i];
                
                // Determine if we're dealing with booleans
                if (actualItem is bool || expectedItem is bool || 
                    (expectedItem is string strItem && (strItem == "true" || strItem == "false")))
                {
                    Assert.Equal(Convert.ToBoolean(expectedItem), Convert.ToBoolean(actualItem));
                }
                else
                {
                    Assert.Equal(Convert.ToInt64(expectedItem), Convert.ToInt64(actualItem));
                }
            }
        }
        else if (actual is Array resultArray && expected is List<object> expectedList)
        {
            Assert.Equal(expectedList.Count, resultArray.Length);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.Equal(Convert.ToInt32(expectedList[i]), Convert.ToInt32(resultArray.GetValue(i)));
            }
        }
        else if (actual is bool || expected is bool)
        {
            Assert.Equal(Convert.ToBoolean(expected), Convert.ToBoolean(actual));
        }
        else if (actual is int || actual is long || expected is int || expected is long)
        {
            Assert.Equal(Convert.ToInt64(expected), Convert.ToInt64(actual));
        }
        else if (expected is string expectedString)
        {
            Assert.Equal(expectedString, actual?.ToString());
        }
        else
        {
            Assert.Equal(expected, actual);
        }
    }

    private object ConvertToParameterType(object value, Type targetType)
    {
        if (targetType == typeof(int))
        {
            return Convert.ToInt32(value);
        }
        else if (targetType == typeof(long))
        {
            return Convert.ToInt64(value);
        }
        else if (targetType == typeof(string))
        {
            return value.ToString()!;
        }
        else if (targetType == typeof(int[]))
        {
            if (value is List<object> list)
            {
                return list.Select(v => Convert.ToInt32(v)).ToArray();
            }
        }
        else if (targetType == typeof(long[]))
        {
            if (value is List<object> list)
            {
                return list.Select(v => Convert.ToInt64(v)).ToArray();
            }
        }
        else if (targetType == typeof(string[]))
        {
            if (value is List<object> list)
            {
                return list.Select(v => v.ToString()!).ToArray();
            }
        }
        
        return value;
    }
}

public class ProblemTestCases
{
    public int Id { get; set; }
    public string MethodName { get; set; } = "";
    public List<TestCase> Cases { get; set; } = new();
}

public class TestCase
{
    public List<object> Input { get; set; } = new();
    public object Expected { get; set; } = null!;
}
