using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingApi.SortingLogic
{
    /// <summary>
    /// Standard implementation of merge sort
    /// </summary>
    public class MergeSortAlgorithm : ISortAlgorithm<int>
    {
        public List<int> Sort(List<int> input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null");
            }

            if (input.Count <= 1)
                return input;

            var left = new List<int>();
            var right = new List<int>();

            int middle = input.Count / 2;
            for (int i = 0; i < middle; i++) // Dividing the unsorted list
            {
                left.Add(input[i]);
            }
            for (int i = middle; i < input.Count; i++)
            {
                right.Add(input[i]);
            }

            left = Sort(left);
            right = Sort(right);
            return Merge(left, right);
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First()) // Comparing First two elements to see which is smaller
                    {
                        result.Add(left.First());
                        left.Remove(left.First()); // Rest of the list minus the first element
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());

                    right.Remove(right.First());
                }
            }
            return result;
        }
    }
}

