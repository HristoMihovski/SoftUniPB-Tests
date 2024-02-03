using NUnit.Framework;

namespace PBTests
{

    [TestFixture]
    public class ProcessingClassTest
    {
        private const string TestScenarioSeparator = "===";

        private static IEnumerable<TestParam> GenerateTestParams()
        {
            var paramsList = new List<TestParam>();
            // Hardcoded full path to the test files
            var inputFile = new StreamReader(@"C:\Users\hrist\Downloads\VR\PBTests\PBTests\testInputs.txt");
            var expectedOutputFile = new StreamReader(@"C:\Users\hrist\Downloads\VR\PBTests\PBTests\testExpectedResults.txt");

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
                        // Replace carriage return and newline characters with just newline characters
                        expectedOutput += outputLine.Replace("\r\n", "\n") + "\n";
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
                    // Replace carriage return and newline characters with just newline characters
                    expectedOutput += outputLine.Replace("\r\n", "\n") + "\n";
                }
                paramsList.Add(new TestParam { TestCaseNumber = testCaseNumber, Input = input, ExpectedOutput = expectedOutput });
            }
            return paramsList;
        }


        [TestCaseSource("GenerateTestParams")]
        public void Test(TestParam param)
        {
            // Ensure param.Input and param.ExpectedOutput are not null before using them
            if (param.Input != null && param.ExpectedOutput != null)
            {
                using (var input = new StringReader(param.Input))
                using (var output = new StringWriter())
                {
                    ProcessingClass.Process(input, output);

                    // Remove carriage return and newline characters for comparison
                    var expected = param.ExpectedOutput.Replace("\r", "").Replace("\n", "");
                    var actual = output.ToString().Replace("\r", "").Replace("\n", "");

                    Assert.AreEqual(expected, actual, "Test failed for TestCaseNumber: " + param.TestCaseNumber);
                }
            }
            else
            {
                Assert.Fail("Input or ExpectedOutput is null.");
            }
        }
    }

    }
