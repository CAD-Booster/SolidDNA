using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// The type of mate. Same values as <see cref="swMateType_e"/>.
/// </summary>
public enum MateType
{
    /// <summary>
    /// Coincident mate. A coincident mate between origins uses <see cref="CoincidentOrigin"/>.
    /// </summary>
    Coincident = 0,

    /// <summary>
    /// Concentric mate
    /// </summary>
    Concentric = 1,

    /// <summary>
    /// Perpendicular mate
    /// </summary>
    Perpendicular = 2,

    /// <summary>
    /// Parallel mate
    /// </summary>
    Parallel = 3,

    /// <summary>
    /// Tangent mate
    /// </summary>
    Tangent = 4,

    /// <summary>
    /// Distance mate. Can be fixed distance or min-max distance.
    /// </summary>
    Distance = 5,

    /// <summary>
    /// Angle mate. Can be fixed angle or min-max angle.
    /// </summary>
    Angle = 6,

    /// <summary>
    /// Unknown mate type.
    /// </summary>
    Unknown = 7,

    /// <summary>
    /// Symmetric mate
    /// </summary>
    Symmetric = 8,

    /// <summary>
    /// Cam follower mate
    /// </summary>
    Cam = 9,

    /// <summary>
    /// Gear mate
    /// </summary>
    Gear = 10,

    /// <summary>
    /// Width mate
    /// </summary>
    Width = 11,

    /// <summary>
    /// Lock to sketch mate. Unknown mate type.
    /// </summary>
    LockToSketch = 12,

    /// <summary>
    /// Rack and pinion mate
    /// </summary>
    RackPinion = 13,

    /// <summary>
    /// Max mates mate. Unknown mate type.
    /// </summary>
    MaxMates = 14,

    /// <summary>
    /// Path mate
    /// </summary>
    Path = 15,

    /// <summary>
    /// Lock mate
    /// </summary>
    Lock = 16,

    /// <summary>
    /// Screw mate
    /// </summary>
    Screw = 17,

    /// <summary>
    /// Linear coupler mate
    /// </summary>
    LinearCoupler = 18,

    /// <summary>
    /// Universal joint mate
    /// </summary>
    UniversalJoint = 19,

    /// <summary>
    /// Coincident mate between origins
    /// </summary>
    CoincidentOrigin = 20,

    /// <summary>
    /// Slot mate
    /// </summary>
    Slot = 21,

    /// <summary>
    /// Hinge mate
    /// </summary>
    Hinge = 22,

    /// <summary>
    /// Slider mate. Unknown mate type.
    /// </summary>
    Slider = 23,

    /// <summary>
    /// Profile center mate
    /// </summary>
    ProfileCenter = 24,

    /// <summary>
    /// Magnetic mate
    /// </summary>
    Magnetic = 25,
}
