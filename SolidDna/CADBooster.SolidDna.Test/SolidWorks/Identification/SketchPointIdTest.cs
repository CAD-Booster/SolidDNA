using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class SketchPointIdTest
{
    [Test]
    public void Constructor_FromIntegers()
    {
        var sketchPoint = new Mock<SketchPoint>();
        sketchPoint.Setup(x => x.GetID()).Returns(new int[] { 10, 11 });
        var sketch = new Mock<Sketch>();
        sketch.As<Feature>().Setup(x => x.Name).Returns("TestSketch");
        sketchPoint.Setup(x => x.GetSketch()).Returns(sketch.Object);

        var sketchPointId = new SketchPointId(sketchPoint.Object);

        Assert.That(sketchPointId.Id0, Is.EqualTo(10));
        Assert.That(sketchPointId.Id1, Is.EqualTo(11));
        Assert.That(sketchPointId.SketchName, Is.EqualTo("TestSketch"));
        Assert.That(sketchPointId.ToString(), Is.EqualTo("Sketch point ID TestSketch-10-11"));
    }

    [Test]
    public void Constructor_FromLongs()
    {
        var sketchPoint = new Mock<SketchPoint>();
        sketchPoint.Setup(x => x.GetID()).Returns(new long[] { 20, 21 });
        var sketch = new Mock<Sketch>();
        sketch.As<Feature>().Setup(x => x.Name).Returns("AnotherSketch");
        sketchPoint.Setup(x => x.GetSketch()).Returns(sketch.Object);

        var sketchPointId = new SketchPointId(sketchPoint.Object);
        
        Assert.That(sketchPointId.Id0, Is.EqualTo(20));
        Assert.That(sketchPointId.Id1, Is.EqualTo(21));
        Assert.That(sketchPointId.SketchName, Is.EqualTo("AnotherSketch"));
        Assert.That(sketchPointId.ToString(), Is.EqualTo("Sketch point ID AnotherSketch-20-21"));
    }

    [Test]
    public void Equals_GetHashcode()
    {
        var sketchPoint = new Mock<SketchPoint>();
        sketchPoint.Setup(x => x.GetID()).Returns(new long[] { 20, 21 });
        var sketch = new Mock<Sketch>();
        sketch.As<Feature>().Setup(x => x.Name).Returns("AnotherSketch");
        sketchPoint.Setup(x => x.GetSketch()).Returns(sketch.Object);

        var sketchPointId0 = new SketchPointId(sketchPoint.Object);
        var sketchPointId1 = new SketchPointId(sketchPoint.Object);

        Assert.That(sketchPointId0.Equals(new object()), Is.False);
        Assert.That(sketchPointId0.Equals(sketchPointId1), Is.True);
        Assert.That(sketchPointId0.GetHashCode(), Is.EqualTo(sketchPointId1.GetHashCode()));
    }
}
