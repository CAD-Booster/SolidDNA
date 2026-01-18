using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// The type of mate alignment. Same values as <see cref="swMateAlign_e"/>, but without the obsolete, duplicate values.
/// </summary>
public enum MateAlignment
{
    /// <summary>
    /// Aligned
    /// </summary>
    Aligned = 0,

    /// <summary>
    /// Anti-Aligned
    /// </summary>
    AntiAligned = 1,

    /// <summary>
    /// Closest
    /// </summary>
    Closest = 2,
}