using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// Interface for a SolidWorks assembly document.
/// Enables mocking for unit testing code that consumes assembly documents.
/// </summary>
public interface IAssemblyDocument
{
    #region Properties

    /// <summary>
    /// The raw underlying COM object.
    /// WARNING: Use with caution. You must handle all disposal from this point on.
    /// </summary>
    AssemblyDoc UnsafeObject { get; }

    #endregion

    #region Component Methods

    /// <summary>
    /// Set the suppression state for a list of components in a certain configuration.
    /// You cannot set components as Lightweight with this method.
    /// </summary>
    /// <param name="components">The components to change</param>
    /// <param name="state">The suppression state to set</param>
    /// <param name="configurationOption">The configuration option</param>
    /// <param name="configurationName">If you select <see cref="ModelConfigurationOptions.SpecificConfiguration"/>, pass the configuration name here.</param>
    /// <returns>True if successful, false if it fails or if the list is empty</returns>
    bool SetComponentSuppressionState(List<Component> components, ComponentSuppressionStates state,
        ModelConfigurationOptions configurationOption = ModelConfigurationOptions.ThisConfiguration, string configurationName = null);

    /// <summary>
    /// Set the configuration for a file that was just dropped into the assembly.
    /// Use this method after receiving a <see cref="Model.FileDropped"/> event to set the configuration name of the dropped model.
    /// </summary>
    /// <param name="configurationName">The configuration name to set</param>
    /// <returns>True if successful</returns>
    bool SetConfigurationForDroppedFile(string configurationName);

    #endregion

    #region Feature Methods

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name.
    /// Only works for features in the root assembly, not in sub-assemblies.
    /// Returns the actual model feature or null when not found.
    /// </summary>
    /// <param name="featureName">Name of the feature</param>
    /// <returns>The <see cref="ModelFeature"/> for the named feature</returns>
    ModelFeature GetFeatureByName(string featureName);

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform a function on it.
    /// Only works for features in the root assembly, not in sub-assemblies.
    /// </summary>
    /// <typeparam name="T">The return type of the function</typeparam>
    /// <param name="featureName">Name of the feature</param>
    /// <param name="function">The function to perform on this feature</param>
    /// <returns>The result of the function</returns>
    T GetFeatureByName<T>(string featureName, Func<ModelFeature, T> function);

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform an action on it.
    /// Only works for features in the root assembly, not in sub-assemblies.
    /// </summary>
    /// <param name="featureName">Name of the feature</param>
    /// <param name="action">The action to perform on this feature</param>
    void GetFeatureByName(string featureName, Action<ModelFeature> action);

    #endregion
}