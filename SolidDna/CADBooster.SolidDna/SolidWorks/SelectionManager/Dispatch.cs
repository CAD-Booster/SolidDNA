using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Convert a list of solidworks objects into an array of dispatch wrappers.
    /// Pass the array as an argument for SolidWorks methods that take an array containing different types.
    /// </summary>
    public static class Dispatch
    {
        /// <summary>
        /// Convert a list of SolidWorks objects to a dispatch wrapper array.
        /// A dispatch wrapper is necessary when you want to combine different types in a list.
        /// Returns an array because that's what SolidWorks expects.
        /// </summary>
        /// <param name="solidWorksObjects"></param>
        /// <returns></returns>
        public static DispatchWrapper[] ConvertListToDispatchWrapperArray(List<object> solidWorksObjects) => solidWorksObjects.Select(x => new DispatchWrapper(x)).ToArray();
    }
}