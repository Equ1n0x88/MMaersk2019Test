using System;
using System.Collections.Generic;

namespace SortingApi.SortingLogic
{
    /// <summary>
    /// Interface for the sorting algorithm.
    /// Normally, we could just have it sort integers, but it can easily be used to sort any comparable item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISortAlgorithm<T> where T: IComparable<T>
    {
        List<T> Sort(List<T> input);
    }
}
