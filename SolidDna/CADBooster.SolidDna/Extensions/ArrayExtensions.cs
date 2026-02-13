using System.Linq;

namespace CADBooster.SolidDna;

/// <summary>
/// Extension methods for arrays
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Append the given objects to the original source array
    /// </summary>
    /// <typeparam name="T">The type of array</typeparam>
    /// <param name="source">The original array of values</param>
    /// <param name="toAdd">The values to append to the source</param>
    /// <returns></returns>
    public static T[] Append<T>(this T[] source, params T[] toAdd)
    {
        // If the original array is null, only return the items to add
        if (source == null)
            return toAdd;

        // If there is nothing to add, return the source
        if (toAdd == null)
            return source;

        // Append and return the new items
        return source.Concat(toAdd).ToArray();
    }

    /// <summary>
    /// Prepend the given objects to the original source array
    /// </summary>
    /// <typeparam name="T">The type of array</typeparam>
    /// <param name="source">The original array of values</param>
    /// <param name="toAdd">The values to prepend to the source</param>
    /// <returns></returns>
    public static T[] Prepend<T>(this T[] source, params T[] toAdd) => toAdd.Append(source); // Prepend and return the new items
}