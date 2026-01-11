using SolidWorks.Interop.swconst;
using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks selected object.
    /// Enables mocking for unit testing code that consumes selected objects.
    /// </summary>
    public interface ISelectedObject : IDisposable
    {
        #region Properties

        /// <summary>
        /// The type of the selected object.
        /// </summary>
        swSelectType_e ObjectType { get; }

        /// <summary>
        /// True if this object is a feature.
        /// From the feature you can check the specific feature type and get the specific feature from that.
        /// </summary>
        bool IsFeature { get; }

        /// <summary>
        /// True if this object is a dimension.
        /// </summary>
        bool IsDimension { get; }

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        object UnsafeObject { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Cast the object to a <see cref="ModelFeature"/>.
        /// Check with <see cref="IsFeature"/> first to assure that it is this type.
        /// </summary>
        /// <param name="action">The feature is passed into this action to be used within it</param>
        void AsFeature(Action<ModelFeature> action);

        /// <summary>
        /// Cast the object to a <see cref="ModelDisplayDimension"/>.
        /// Check with <see cref="IsDimension"/> first to assure that it is this type.
        /// </summary>
        /// <param name="action">The Dimension is passed into this action to be used within it</param>
        void AsDimension(Action<ModelDisplayDimension> action);

        #endregion
    }
}
