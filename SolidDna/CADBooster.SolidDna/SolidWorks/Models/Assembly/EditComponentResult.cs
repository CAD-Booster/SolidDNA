namespace CADBooster.SolidDna;

/// <summary>
/// The result of activating Edit Part/Assembly mode.
/// </summary>
public enum EditComponentResult
{
    /// <summary>
    /// The selected component must have write access.
    /// </summary>
    ComponentMustHaveWriteAccess = -5,

    /// <summary>
    /// The selected component must be resolved.
    /// </summary>
    ComponentMustBeResolved = -4,

    /// <summary>
    /// You must select a component before entering Edit Part/Assembly mode.
    /// </summary>
    ComponentMustBeSelected = -3,

    /// <summary>
    /// The assembly must be saved before entering Edit Part/Assembly mode.
    /// </summary>
    AssemblyMustBeSaved = -2,

    /// <summary>
    /// General failure.
    /// </summary>
    Failure = -1,

    /// <summary>
    /// Edit Part/Assembly mode activated successfully.
    /// </summary>
    Successful = 0,

    /// <summary>
    /// The selected component is not positioned yet.
    /// </summary>
    ComponentNotPositioned = 1
}
