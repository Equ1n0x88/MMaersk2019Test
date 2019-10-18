using System.Collections.Generic;
using SortingApi.SortingLogic;
using Xunit;

namespace SortingApi.Test
{
    public class MergeSortAlgorithmTests
    {
        [Fact]
        public void TestSortAlgorithmForAnArray()
        {
            List<int> unsortedInput = new List<int> { -32, 1, -555, 0, 1, -29304, -5, 55, 55, 678, 23, -567, 10000, 950, -1, 10000, 23000};

            MergeSortAlgorithm testAlgorithm = new MergeSortAlgorithm();

            // Sort with merge sort
            var sortedInput = testAlgorithm.Sort(unsortedInput);

            // Sort with the default algorithm
            unsortedInput.Sort();
            Assert.Equal(unsortedInput, sortedInput);
        }
    }
}
