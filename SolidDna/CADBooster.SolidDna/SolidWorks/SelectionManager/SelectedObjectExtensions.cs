using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Provides extension methods for <see cref="SelectedObject"/> to enable fluent-style casting
    /// </summary>
    public static class SelectedObjectExtensions
    {
        /// <summary>
        /// Wraps a <see cref="SolidDnaObject"/> as a <see cref="SelectedObject"/> for use in selection operations
        /// </summary>
        /// <param name="dnaObject">The SolidDna-wrapped SOLIDWORKS object</param>
        /// <returns>A new SelectedObject instance</returns>
        public static SelectedObject AsSelectedObject(this SolidDnaObject dnaObject)
            => new SelectedObject(dnaObject.UnsafeObject);

        /// <summary>
        /// Converts a enumerable of <see cref="SolidDnaObject"/> to <see cref="SelectedObject"/> instances
        /// </summary>
        /// <param name="dnaObjects">The collection of SolidDna-wrapped objects</param>
        /// <returns>An enumerable of <see cref="SelectedObject"/> instances</returns>
        public static IEnumerable<SelectedObject> AsSelectedObjects(this IEnumerable<SolidDnaObject> dnaObjects)
            => dnaObjects.Select(x => x.AsSelectedObject());

        /// <summary>
        /// Casts the selected object to a <see cref="ModelFeature"/>
        /// </summary>
        /// <param name="selectedObject">The selected object to cast</param>
        /// <returns>A new ModelFeature instance</returns>
        public static ModelFeature AsFeature(this SelectedObject selectedObject)
            => selectedObject.AsSpecificObject((Feature x) => new ModelFeature(x));

        /// <summary>
        /// Casts the selected object to a <see cref="ModelDisplayDimension"/>
        /// </summary>
        /// <param name="selectedObject">The selected object to cast</param>
        /// <returns>A new ModelDisplayDimension instance</returns>
        public static ModelDisplayDimension AsDimension(this SelectedObject selectedObject)
            => selectedObject.AsSpecificObject((IDisplayDimension x) => new ModelDisplayDimension(x));

        /// <summary>
        /// Internal method to handle generic type casting of selected objects
        /// </summary>
        /// <typeparam name="TWrapper">The wrapper type to return</typeparam>
        /// <typeparam name="TBase">The base SolidWorks interface type</typeparam>
        /// <param name="selectedObject">The selected object to cast</param>
        /// <param name="factory">Factory method to create the wrapper</param>
        /// <returns>The wrapped SolidWorks object</returns>
        internal static TWrapper AsSpecificObject<TWrapper, TBase>(this SelectedObject selectedObject, Func<TBase, TWrapper> factory)
            => SolidDnaErrors.Wrap(() =>
                factory.Invoke((TBase)selectedObject.UnsafeObject),
                SolidDnaErrorTypeCode.SolidWorksModel,
                SolidDnaErrorCode.SolidWorksModelSelectedObjectCastError
            );
    }
}
