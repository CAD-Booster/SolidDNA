using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a component (Part or Assembly) in a SolidWorks assembly.
/// An assembly has a root component, which is the assembly itself.
/// Each component can have child components. The structure is the same as you see in the feature tree.
/// Enables mocking for unit testing code that consumes components.
/// </summary>
public interface IComponent : IDisposable
{
    #region Properties

    /// <summary>
    /// Get the Model from the component.
    /// Warning: this can be null if the component is suppressed or lightweight.
    /// </summary>
    Model AsModel { get; }

    /// <summary>
    /// Get children from this Component
    /// </summary>
    List<Component> Children { get; }

    /// <summary>
    /// Get the real name of the component, without the sub-assembly name and without instance numbers.
    /// </summary>
    string CleanName { get; }

    /// <summary>
    /// The name of the configuration for this component.
    /// </summary>
    string ConfigurationName { get; set; }

    /// <summary>
    /// The name of the display state for this component.
    /// </summary>
    string DisplayStateName { get; set; }

    /// <summary>
    /// The complete path to the component.
    /// </summary>
    string FilePath { get; }

    /// <summary>
    /// True if this component is an assembly
    /// </summary>
    bool IsAssembly { get; }

    /// <summary>
    /// Check if this sub-assembly is flexible.
    /// </summary>
    bool IsFlexible { get; }

    /// <summary>
    /// True if this component is a part
    /// </summary>
    bool IsPart { get; }

    /// <summary>
    /// Check if the Component is the root component.
    /// In an assembly, this is the assembly itself. In a part, this is the part itself.
    /// </summary>
    bool IsRoot { get; }

    /// <summary>
    /// Check if the component is suppressed in the current assembly configuration.
    /// </summary>
    bool IsSuppressed { get; }

    /// <summary>
    /// Check if the component is a virtual component.
    /// Virtual components are saved within the assembly, not to a separate file.
    /// </summary>
    bool IsVirtual { get; }

    /// <summary>
    /// Check if this component is visible. Returns false when the visibility is hidden or unknown.
    /// </summary>
    bool IsVisible { get; set; }

    /// <summary>
    /// Get the type of component, either a part or an assembly.
    /// </summary>
    ComponentTypes ModelType { get; }

    /// <summary>
    /// Get the name of the component including the instance number.
    /// If this component is in a sub-assembly, the name starts with the name of the sub-assembly component.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Get the parent component for this component. Is null for the root component.
    /// </summary>
    Component Parent { get; }

    /// <summary>
    /// Get the unique 32-character alphanumeric identifier for this component.
    /// </summary>
    string PlmId { get; }

    /// <summary>
    /// The raw underlying COM object.
    /// WARNING: Use with caution. You must handle all disposal from this point on.
    /// </summary>
    Component2 UnsafeObject { get; }

    #endregion

    #region Methods

    /// <summary>
    /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name.
    /// Returns a model feature or null when not found.
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
    /// Gets the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform an action on it.
    /// </summary>
    /// <param name="featureName">Name of the feature</param>
    /// <param name="action">The action to perform on this feature</param>
    void GetFeatureByName(string featureName, Action<ModelFeature> action);

    /// <summary>
    /// Select the sub-assembly component and mark it as flexible.
    /// </summary>
    /// <returns>True if successful</returns>
    bool SetFlexible();

    /// <summary>
    /// Select the sub-assembly component and mark it as rigid.
    /// </summary>
    /// <returns>True if successful</returns>
    bool SetRigid();

    /// <summary>
    /// Suppress this component in the current assembly configuration.
    /// </summary>
    /// <returns>Result enum</returns>
    ComponentSuppressionResults Suppress();

    /// <summary>
    /// Unsuppress this component in the current assembly configuration.
    /// </summary>
    /// <returns>Result enum</returns>
    ComponentSuppressionResults Unsuppress();

    /// <summary>
    /// Get the assembly that owns this component.
    /// </summary>
    /// <returns>The parent assembly model</returns>
    Model GetParentAssembly();

    #endregion
}