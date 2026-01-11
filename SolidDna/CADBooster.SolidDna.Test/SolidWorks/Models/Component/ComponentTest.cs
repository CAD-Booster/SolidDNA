using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

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
}