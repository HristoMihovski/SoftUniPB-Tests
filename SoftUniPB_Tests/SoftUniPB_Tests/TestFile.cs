using System;
using System.IO;
using NUnit.Framework;

public struct TestParam
{
    public int TestCaseNumber;
    public string Input;
    public string ExpectedOutput;
}

public class PbTest
{
    private const string TestScenarioSeparator = "===";

    public static IEnumerable<TestParam> GenerateTestParams()
    {
        var paramsList = new List<TestParam>();
        using (var inputFile = new StreamReader("../tests/testInputs.txt"))
        using (var expectedOutputFile = new StreamReader("../tests/testExpectedResults.txt"))
        {
            int testCaseNumber = 0;
            string line;
            string input = string.Empty;
            string expectedOutput = string.Empty;

            while ((line = inputFile.ReadLine()) != null)
            {
                if (line == TestScenarioSeparator)
                {
                    string outputLine;
                    while ((outputLine = expectedOutputFile.ReadLine()) != null && outputLine != TestScenarioSeparator)
                    {
                        expectedOutput += outputLine + "\n";
                    }
                    paramsList.Add(new TestParam { TestCaseNumber = testCaseNumber++, Input = input, ExpectedOutput = expectedOutput });
                    input = string.Empty;
                    expectedOutput = string.Empty;
                }
                else
                {
                    input += line + "\n";
                }
            }

            if (!string.IsNullOrEmpty(input))
            {
                string outputLine;
                while ((outputLine = expectedOutputFile.ReadLine()) != null && outputLine != TestScenarioSeparator)
                {
                    expectedOutput += outputLine + "\n";
                }
                paramsList.Add(new TestParam { TestCaseNumber = testCaseNumber, Input = input, ExpectedOutput = expectedOutput });
            }
        }

        return paramsList;
    }

    [Test]
    [TestCaseSource(nameof(GenerateTestParams))]
    public void Test(TestParam param)
    {
        using (var iss = new StringReader(param.Input))
        using (var ossOutput = new StringWriter())
        {
            ProcessingClass.Process();
            Assert.Equals(param.ExpectedOutput, ossOutput.ToString());
        }
    }
}
