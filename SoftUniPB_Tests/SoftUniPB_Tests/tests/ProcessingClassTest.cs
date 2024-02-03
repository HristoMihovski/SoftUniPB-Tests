using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class ProcessingClassTest
{
    private const string TestScenarioSeparator = "===";

    private static IEnumerable<TestParam> GenerateTestParams()
    {
        var paramsList = new List<TestParam>();
        var inputFile = new StreamReader("testInputs.txt");
        var expectedOutputFile = new StreamReader("testExpectedResults.txt");
        string line;
        int testCaseNumber = 0;
        string input = "";
        string expectedOutput = "";
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
                input = "";
                expectedOutput = "";
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
        return paramsList;
    }

    [TestCaseSource("GenerateTestParams")]
    public void Test(TestParam param)
    {
        using (var input = new StringReader(param.Input))
        using (var output = new StringWriter())
        {
            ProcessingClass.Process(input, output);
            Assert.AreEqual(param.ExpectedOutput, output.ToString());
        }
    }
}
