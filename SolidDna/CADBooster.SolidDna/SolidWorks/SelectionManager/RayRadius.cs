namespace CADBooster.SolidDna;

/// <summary>
/// Used by the <see cref="SelectionManager"/> for selecting an object by casting a ray from a point in a direction.
/// We select the first object (of a given type) that intersects with the ray within the specified radius.
/// </summary>
public enum RayRadius
{
    /// <summary>
    /// 0.0001 or 0.1mm
    /// </summary>
    ExtraExtraSmall,

    /// <summary>
    /// 0.0002 or 0.2mm
    /// </summary>
    ExtraSmall,

    /// <summary>
    /// 0.0004 or 0.4mm
    /// </summary>
    Small,

    /// <summary>
    /// 0.0008 or 0.8mm
    /// </summary>
    Standard,

    /// <summary>
    /// 0.0016 or 1.6mm
    /// </summary>
    Large,

    /// <summary>
    /// 0.0032 or 3.2mm
    /// </summary>
    ExtraLarge,
}