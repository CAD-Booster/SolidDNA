using SolidWorks.Interop.sldworks;
using System;

namespace CADBooster.SolidDna;

/// <summary>
/// Interface for a SolidWorks part document.
/// Enables mocking for unit testing code that consumes part documents.
/// </summary>
public interface IPartDocument
{
    #region Properties

    /// <summary>
    /// The raw underlying COM object.
    /// WARNING: Use with caution. You must handle all disposal from this point on.
    /// </summary>
    PartDoc UnsafeObject { get; }

    #endregion

    #region Feature Methods

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name.
    /// Returns the actual model feature or null when not found.
    /// </summary>
    /// <param name="featureName">Name of the feature</param>
    /// <returns>The <see cref="ModelFeature"/> for the named feature</returns>
    ModelFeature GetFeatureByName(string featureName);

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform a function on it.
    /// </summary>
    /// <typeparam name="T">The return type of the function</typeparam>
    /// <param name="featureName">Name of the feature</param>
    /// <param name="function">The function to perform on this feature</param>
    /// <returns>The result of the function</returns>
    T GetFeatureByName<T>(string featureName, Func<ModelFeature, T> function);

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform an action on it.
    /// </summary>
    /// <param name="featureName">Name of the feature</param>
    /// <param name="action">The action to perform on this feature</param>
    void GetFeatureByName(string featureName, Action<ModelFeature> action);

    #endregion
}