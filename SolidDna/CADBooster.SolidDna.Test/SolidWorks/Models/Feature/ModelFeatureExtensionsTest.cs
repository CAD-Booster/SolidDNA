using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class ModelFeatureExtensionsTest
{
    [Test]
    public void AsConfiguration()
    {
        var configuration = new Mock<Configuration>();
        configuration.Setup(x => x.Name).Returns("Config1");
        var feature = new Mock<Feature>();
        feature.Setup(f => f.GetSpecificFeature2()).Returns(configuration.Object);
        var modelFeature = new ModelFeature(feature.Object);

        var modelConfiguration = modelFeature.AsConfiguration();

        Assert.That(modelConfiguration.UnsafeObject, Is.Not.Null);
        Assert.That(modelConfiguration, Is.TypeOf<ModelConfiguration>());
        //Assert.That(modelConfiguration.UnsafeObject, Is.TypeOf<Configuration>()); // doesn't work because of mocking
        Assert.That(modelConfiguration.Name, Is.EqualTo("Config1"));
    }

    [Test]
    public void AsSketch()
    {
        var sketch = new Mock<Sketch>();
        var feature = new Mock<Feature>();
        feature.Setup(f => f.GetSpecificFeature2()).Returns(sketch.Object);
        var modelFeature = new ModelFeature(feature.Object);

        var featureSketch = modelFeature.AsSketch();
        
        Assert.That(featureSketch.UnsafeObject, Is.Not.Null);
        Assert.That(featureSketch, Is.TypeOf<FeatureSketch>());
        //Assert.That(featureSketch.UnsafeObject, Is.TypeOf<Sketch>()); // doesn't work because of mocking
    }
}
