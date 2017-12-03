using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTestApp;
using NUnit.Framework;
using System.Threading;
using System.IO;

namespace CalculatorTest
{    
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator testObj = null;

        [SetUp]
        public void InitialSetup()
        {
            Console.WriteLine($"Setup method for {TestContext.CurrentContext.Test.MethodName}");
            testObj = new Calculator();
        }

        [Test]
        [Parallelizable]
        [Category("1")]
        [Category("3")]
        [Sequential]//Makes set of tests based on matching index values of each parameter
        public void AddTest(
            [Values(1, 2, 3)] int num1,
            [Values(4, 5, 6)] int num2,
            [Values(5, 7, 9)] int expectedResult
            )
        {
            Assert.AreEqual(expectedResult, testObj.Add(num1, num2), "Verified add functionality");
        }

        [Test]
        [Parallelizable]
        [Category("2")]
        [TestCase(2, 1, 1)]
        [TestCase(4, 2, 2)]
        [TestCase(5, 2, 2)]
        public void DiffTest(int num1, int num2, int expectedDiff)
        {
            Assert.AreEqual(expectedDiff, testObj.Diff(num1, num2), "Verified difference functionality");
        }


        [Test]
        [TestCaseSource("TestCases")]
        public double DivTest(double num1, double num2)
        {
            return testObj.Div(8, 4);
        }

        /// <summary>
        /// Data source method for DivTest
        /// </summary>
        /// <returns></returns>
        public static List<TestCaseData> TestCases()
        {
            List<TestCaseData> inputList = new List<TestCaseData>();
            //inputList.Add(new TestCaseData(8, 4).Returns(2));
            //inputList.Add(new TestCaseData(10, 3).Returns(3));
            //inputList.Add(new TestCaseData(8, 2).Returns(10));
            using (var fs = File.OpenRead(@"D:\test.csv"))
            {
                using (var sr = new StreamReader(fs))
                {
                    string line = string.Empty;
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            string[] split = line.Split(new char[] { ',' },
                                StringSplitOptions.None);

                            double num1 = Convert.ToDouble(split[0]);
                            double num2 = Convert.ToDouble(split[1]);
                            decimal expectedResult = Convert.ToDecimal(split[2]);

                            var testCase = new TestCaseData(num1, num2).Returns(expectedResult);
                            inputList.Add(testCase);
                        }
                    }
                }
                return inputList;
            }
        }
    }
}
