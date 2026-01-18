using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna.Test.Assembly;

[TestFixture]
internal class MateEntityTest
{
    [Test]
    public void Constructor()
    {
        var mockComponent = new Mock<Component2>();
        mockComponent.Setup(c => c.Name2).Returns("Parent-2/child-1");
        var mockMateEntity = new Mock<IMateEntity2>();
        mockMateEntity.Setup(x => x.ReferenceComponent).Returns(mockComponent.Object);
        var face = new Mock<Face2>();
        face.As<Entity>();
        mockMateEntity.Setup(x => x.Reference).Returns(face.Object);
        mockMateEntity.Setup(x => x.ReferenceType2).Returns((int) swSelectType_e.swSelFACES);

        var mateEntity = new MateEntity(mockMateEntity.Object);

        Assert.That(mateEntity.Component.Name, Is.EqualTo("Parent-2/child-1"));
        Assert.That(mateEntity.EntityType, Is.EqualTo(swSelectType_e.swSelFACES));
        Assert.That(mateEntity.Entity, Is.InstanceOf<Face2>());
        Assert.That(mateEntity.ToString(), Is.EqualTo("Mate entity of type swSelFACES from component Parent-2/child-1"));
    }

    [Test]
    public void Equality()
    {
        var mockComponent = new Mock<Component2>();
        mockComponent.Setup(c => c.Name2).Returns("Parent-2/child-1");
        mockComponent.Setup(x => x.ReferencedConfiguration).Returns("Default");
        mockComponent.Setup(x => x.GetPathName()).Returns("C:\\test.sldprt");
        var face = new Mock<Face2>();
        face.As<Entity>();
        var mockMateEntity1 = new Mock<IMateEntity2>();
        mockMateEntity1.Setup(x => x.ReferenceComponent).Returns(mockComponent.Object);
        mockMateEntity1.Setup(x => x.Reference).Returns(face.Object);
        mockMateEntity1.Setup(x => x.ReferenceType2).Returns((int) swSelectType_e.swSelFACES);

        var mockMateEntity2 = new Mock<IMateEntity2>();
        mockMateEntity2.Setup(x => x.ReferenceComponent).Returns(mockComponent.Object);
        mockMateEntity2.Setup(x => x.Reference).Returns(face.Object);
        mockMateEntity2.Setup(x => x.ReferenceType2).Returns((int) swSelectType_e.swSelFACES);

        var mateEntity1 = new MateEntity(mockMateEntity1.Object);
        var mateEntity2 = new MateEntity(mockMateEntity2.Object);

        Assert.That(mateEntity1.Equals(mateEntity2), Is.True);
        Assert.That(mateEntity1.Equals((object) mateEntity2), Is.True);
        Assert.That(mateEntity1.Equals(new object()), Is.False);
        Assert.That(mateEntity1.GetHashCode(), Is.EqualTo(mateEntity2.GetHashCode()));
    }
}
