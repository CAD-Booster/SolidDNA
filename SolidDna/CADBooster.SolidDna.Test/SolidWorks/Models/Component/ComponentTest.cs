using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;
using System.Linq;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class ComponentTest
{
    [Test]
    public void Constructor_Null_DoesNotThrow()
    {
        var component = new Component(null);

        Assert.That(component, Is.Not.Null);
        Assert.That(component.UnsafeObject, Is.Null);
    }

    [Test]
    public void Constructor_WithComponent_Sets()
    {
        var component2 = new Mock<Component2>();
        component2.Setup(c => c.Name2).Returns("Test/part-1@Assembly1.sldasm");

        var component = new Component(component2.Object);

        Assert.That(component.UnsafeObject, Is.Not.Null);
        Assert.That(component.Name, Is.EqualTo("Test/part-1@Assembly1.sldasm"));
        Assert.That(component.CleanName, Is.EqualTo("part"));
    }

    [TestCase("Part1-1")] // top-level component
    [TestCase("SubAssemblyName-1/Part1-1")] // part in sub-assembly
    [TestCase("HighLevelAssembly-33/LowLevelAssemblyName-123/Part1-1337")] // part in sub-assembly
    [TestCase("Part1^MainAssembly-1")] // virtual top-level component
    [TestCase("HighLevelAssembly-33/Part1^LowLevelAssembly-1")] // virtual top-level component
    public void CleanName(string fullName)
    {
        var component2 = new Mock<Component2>();
        component2.Setup(c => c.Name2).Returns(fullName);

        var component = new Component(component2.Object);

        Assert.That(component.UnsafeObject, Is.Not.Null);
        Assert.That(component.Name, Is.EqualTo(fullName));
        Assert.That(component.CleanName, Is.EqualTo("Part1"));
    }

    [Test]
    public void GetMates()
    {
        var mate0 = new Mock<IMate2>();
        var mate1 = new Mock<IMateInPlace>();
        var component2 = new Mock<Component2>();
        component2.Setup(x => x.GetMates()).Returns(new object[] { mate0.Object, mate1.Object });
        var component = new Component(component2.Object);

        var mates = component.GetMates();

        Assert.That(mates.Count(), Is.EqualTo(1));
        Assert.That(mates, Has.Exactly(1).InstanceOf<FeatureMate>());
        Assert.That(mates, Has.Exactly(0).InstanceOf<FeatureInPlaceMate>());
        Assert.That(mates.First().UnsafeObject, Is.InstanceOf<IMate2>());
    }

    [Test]
    public void GetInPlaceMates()
    {
        var mate0 = new Mock<IMate2>();
        var mate1 = new Mock<IMateInPlace>();
        var component2 = new Mock<Component2>();
        component2.Setup(x => x.GetMates()).Returns(new object[] { mate0.Object, mate1.Object });
        var component = new Component(component2.Object);

        var mates = component.GetInPlaceMates();
        
        Assert.That(mates.Count(), Is.EqualTo(1));
        Assert.That(mates, Has.Exactly(0).InstanceOf<FeatureMate>());
        Assert.That(mates, Has.Exactly(1).InstanceOf<FeatureInPlaceMate>());
        Assert.That(mates.First().UnsafeObject, Is.InstanceOf<IMateInPlace>());
    }
}