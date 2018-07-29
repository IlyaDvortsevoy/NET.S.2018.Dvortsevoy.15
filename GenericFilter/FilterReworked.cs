using System;
using System.Collections.Generic;

namespace GenericFilter
{
    /// <summary>
    /// Defines method to filter a collection in accordance with a certain criteria
    /// </summary>
    public static class FilterReworked
    {
        #region Public API
        /// <summary>
        /// Filters the collection using the specified condition
        /// </summary>
        /// <typeparam name="T"> Type of items in the collection </typeparam>
        /// <param name="collection"> The collection to filter </param>
        /// <param name="condition"> The specified condition for the filter </param>
        /// <returns> The array consisting of filtered items or null if there is no items </returns>
        /// <exception cref="T:System.ArgumentNullException"> Thrown when one of the input arguments is null </exception>
        public static T[] Filter<T>(this IEnumerable<T> collection, Predicate<T> condition)
        {
            ValidateParameters(collection, condition);

            var list = new List<T>();

            foreach (var item in collection)
            {
                if (condition.Invoke(item))
                {
                    list.Add(item);
                }
            }

            if (list.Count > 0)
            {
                return list.ToArray();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Private Helper Methods
        private static void ValidateParameters<T>(
            IEnumerable<T> collection,
            Predicate<T> condition)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(
                    "The parameter can't bu null",
                    nameof(collection));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(
                    "The parameter can't bu null",
                    nameof(condition));
            }
        }
        #endregion
    }
}