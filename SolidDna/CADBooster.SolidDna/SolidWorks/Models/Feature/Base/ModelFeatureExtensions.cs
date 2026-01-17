using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

public static class ModelFeatureExtensions
{
    /// <summary>
    /// Get the underlying configuration from a feature.
    /// </summary>
    /// <param name="modelFeature"></param>
    /// <returns></returns>
    public static ModelConfiguration AsConfiguration(this ModelFeature modelFeature)
    {
        // Cast the specific feature
        var configuration = (Configuration) modelFeature.SpecificFeature;

        // Wrap it
        return new ModelConfiguration(configuration);
    }

    /// <summary>
    /// Get the underlying sketch from a feature.
    /// </summary>
    /// <param name="modelFeature"></param>
    /// <returns></returns>
    public static FeatureSketch AsSketch(this ModelFeature modelFeature)
    {
        // Cast the specific feature
        var sketch = (Sketch) modelFeature.SpecificFeature;

        // Wrap it
        return new FeatureSketch(sketch);
    }
}
