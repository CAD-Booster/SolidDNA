using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class FeatureMateTest
{
    [Test]
    public void Constructor()
    {
        var mate = new Mock<IMate2>();
        mate.Setup(m => m.Alignment).Returns((int) MateAlignment.Aligned);
        mate.Setup(m => m.CanBeFlipped).Returns(true);
        mate.SetupProperty(m => m.Flipped, false);
        mate.Setup(m => m.Type).Returns((int) MateType.Coincident);
        mate.Setup(m => m.GetMateEntityCount()).Returns(2);
        var mateEntity0 = new Mock<MateEntity2>();
        var mateEntity1 = new Mock<MateEntity2>();
        mate.Setup(m => m.MateEntity(0)).Returns(mateEntity0.Object);
        mate.Setup(m => m.MateEntity(1)).Returns(mateEntity1.Object);
        mate.Setup(m => m.MateEntity(2)).Returns((MateEntity2) null);
        mate.Setup(m => m.MateEntity(3)).Returns((MateEntity2) null);

        var featureMate = new FeatureMate(mate.Object);

        Assert.That(featureMate.Alignment, Is.EqualTo(MateAlignment.Aligned));
        Assert.That(featureMate.CanBeFlipped, Is.True);
        Assert.That(featureMate.IsFlipped, Is.False);
        Assert.That(featureMate.Type, Is.EqualTo(MateType.Coincident));
        Assert.That(featureMate.GetEntityCount(), Is.EqualTo(2));
        Assert.That(featureMate.GetEntity0(), Is.Not.Null);
        Assert.That(featureMate.GetEntity1(), Is.Not.Null);
        Assert.That(featureMate.GetEntity2(), Is.Null);
        Assert.That(featureMate.GetEntity3(), Is.Null);
        Assert.That(featureMate.ToString(), Is.EqualTo("Mate feature, type Coincident, alignment Aligned, flipped False, entity count 2"));
    }

    [Test]
    public void Equality()
    {
        var mate = new Mock<IMate2>();
        mate.Setup(m => m.Alignment).Returns((int) MateAlignment.Aligned);
        mate.Setup(m => m.CanBeFlipped).Returns(true);
        mate.SetupProperty(m => m.Flipped, false);
        mate.Setup(m => m.Type).Returns((int) MateType.Coincident);
        mate.Setup(m => m.GetMateEntityCount()).Returns(1);
        mate.As<Feature>();
        var mateEntity = new Mock<MateEntity2>();
        mate.Setup(m => m.MateEntity(0)).Returns(mateEntity.Object);
        mate.Setup(m => m.MateEntity(1)).Returns((MateEntity2) null);

        var mate1 = new FeatureMate(mate.Object);
        var mate2 = new FeatureMate(mate.Object);

        Assert.That(mate1, Is.EqualTo(mate2));
        Assert.That(mate1.Equals((object) mate2), Is.True);
        Assert.That(mate1.Equals(new object()), Is.False);
        Assert.That(mate1.Equals(mate2), Is.True);
        Assert.That(mate1.GetHashCode(), Is.EqualTo(mate2.GetHashCode()));
    }
}
