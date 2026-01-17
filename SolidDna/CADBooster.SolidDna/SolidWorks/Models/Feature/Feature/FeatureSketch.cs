using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents a SolidWorks Sketch feature
/// </summary>
public class FeatureSketch : SolidDnaObject<ISketch>
{
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public FeatureSketch(ISketch model) : base(model)
    {
    }

    #endregion

    #region Features

    /// <summary>
    /// Cast a sketch to a feature.
    /// </summary>
    /// <returns></returns>
    public ModelFeature AsFeature()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        return new ModelFeature((Feature) UnsafeObject);
    }

    #endregion
}