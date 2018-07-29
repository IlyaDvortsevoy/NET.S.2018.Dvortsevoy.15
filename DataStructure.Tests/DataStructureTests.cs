using System.Collections.Generic;
using NUnit.Framework;

namespace DataStructure.Tests
{
    [TestFixture]
    public class DataStructureTests
    {
        #region Helper Properties
        public static IEnumerable<TestCaseData> TestDataEnqueue
        {
            get
            {
                yield return new TestCaseData(
                    new int[] { 1, 1, 45, 88, -4 }).Returns(
                    new int[] { 1, 1, 45, 88, -4 });
            }
        }

        public static IEnumerable<TestCaseData> TestDataDequeue
        {
            get
            {
                yield return new TestCaseData(
                    new int[] { 1, 1, 45, 88, -4 }).Returns(1);
            }
        }

        public static IEnumerable<TestCaseData> TestDataInt
        {
            get
            {
                yield return new TestCaseData(
                    new int[] { 12, 5, 0, -9, 44, 120, 8 }).Returns(
                    new int[] { 12, 5, 0, -9, 44, 120, 8 });
            }
        }

        public static IEnumerable<TestCaseData> TestDataDouble
        {
            get
            {
                yield return new TestCaseData(
                    new double[] { 12.6, -3.7, 123.34, -1D }).Returns(
                    new double[] { 12.6, -3.7, 123.34, -1D });
            }
        }
        #endregion

        [Test, TestCaseSource(nameof(TestDataInt))]
        public int[] Queue_Class_Parameterized_Ctor_Works_Properly_With_Int32(
            int[] array)
        {
            var queue = new Queue<int>(array);
            var list = new List<int>();

            foreach (int item in queue)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        [Test, TestCaseSource(nameof(TestDataDouble))]
        public double[] Queue_Class_Parameterized_Ctor_Works_Properly_With_Double(
            double[] array)
        {
            var queue = new Queue<double>(array);
            var list = new List<double>();

            foreach (double item in queue)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        [Test, TestCaseSource(nameof(TestDataEnqueue))]
        public int[] Enqueue_Method_Works_Properly_With_Int32(
            int[] array)
        {
            var queue = new Queue<int>();
            var list = new List<int>();

            foreach (int item in array)
            {
                queue.Enqueue(item);
            }

            foreach (int item in queue)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        [Test, TestCaseSource(nameof(TestDataDequeue))]
        public int Dequeue_Method_Works_Properly_With_Int32(
            int[] array)
            => new Queue<int>(array).Dequeue();
    }
}