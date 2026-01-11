using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class SolidDnaObjectExtensionsTest
{
    [Test]
    public void CreateOrNull_NullInput_ReturnsNull()
    {
        var solidDnaObject = new SolidDnaObject<Feature>(null);
        Assert.That(solidDnaObject, Is.Not.Null);
        Assert.That(solidDnaObject.UnsafeObject, Is.Null);

        var createOrNull = solidDnaObject.CreateOrNull();

        Assert.That(createOrNull, Is.Null);
    }

    [Test]
    public void CreateOrNull_NonNullInput_ReturnsObject()
    {
        var mockFeature = new Mock<Feature>();
        var solidDnaObject = new SolidDnaObject<Feature>(mockFeature.Object);
        Assert.That(solidDnaObject, Is.Not.Null);
        Assert.That(solidDnaObject.UnsafeObject, Is.Not.Null);

        var createOrNull = solidDnaObject.CreateOrNull();

        Assert.That(createOrNull, Is.Not.Null);
        Assert.That(createOrNull, Is.EqualTo(solidDnaObject));
        Assert.That(createOrNull.UnsafeObject, Is.EqualTo(mockFeature.Object));
    }
}