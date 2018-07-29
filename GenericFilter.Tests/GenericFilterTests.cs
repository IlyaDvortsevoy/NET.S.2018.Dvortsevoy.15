using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace GenericFilter.Tests
{
    [TestFixture]
    public class GenericFilterTests
    {
        public static IEnumerable<TestCaseData> DataForInt
        {
            get
            {
                yield return new TestCaseData(
                    new int[] { -33, 14, 503, 200, 13 },
                    3).Returns(new int[] { -33, 503, 13 });

                yield return new TestCaseData(
                    new int[] { 1, 34, 455, 788, 84, 723, -45, 40, 0, 13, 432 },
                    5).Returns(new int[] { 455, -45 });
            }
        }

        public static IEnumerable<TestCaseData> DataForString
        {
            get
            {
                yield return new TestCaseData(
                    new string[] { "sdf", "adszf", "bbbb", "Trgdf" },
                    "a").Returns(new string[] { "adszf" });

                yield return new TestCaseData(
                    new string[] { "Hello", "World", "Back", "Ignore" },
                    "o").Returns(new string[] { "Hello", "World", "Ignore" });
            }
        }

        public static bool FilterIntByDivision(int digit, int target)
        {
            int temp = digit;

            if (temp < 0)
            {
                temp *= -1;
            }

            while (temp > 0)
            {
                if (temp % 10 == target)
                {
                    return true;
                }

                temp /= 10;
            }

            return false;
        }

        [Test, TestCaseSource(nameof(DataForInt))]
        public int[] Filter_Method_Works_Properly_With_Int32(
            IEnumerable<int> collection,
            int target)
        {
            return collection.Filter(x => FilterIntByDivision(x, target));
        }

        [Test, TestCaseSource(nameof(DataForString))]
        public string[] Filter_Method_Works_Properly_With_String(
            IEnumerable<string> collection,
            string target)
        {
            return collection.Filter(x => x.Contains(target));
        }
    }
}