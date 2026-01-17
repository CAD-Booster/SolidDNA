using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class SketchSegmentIdTest
{
    [Test]
    public void Constructor_FromIntegers()
    {
        var sketchSegment = new Mock<SketchSegment>();
        sketchSegment.Setup(x => x.GetID()).Returns(new int[] { 10, 11 });
        sketchSegment.Setup(x => x.GetType()).Returns(1);
        var sketch = new Mock<Sketch>();
        sketch.As<Feature>().Setup(x => x.Name).Returns("TestSketch");
        sketchSegment.Setup(x => x.GetSketch()).Returns(sketch.Object);
        
        var sketchSegmentId = new SketchSegmentId(sketchSegment.Object);

        Assert.That(sketchSegmentId.Id0, Is.EqualTo(10));
        Assert.That(sketchSegmentId.Id1, Is.EqualTo(11));
        Assert.That(sketchSegmentId.SketchName, Is.EqualTo("TestSketch"));
        Assert.That(sketchSegmentId.Type, Is.EqualTo(SketchSegmentType.Arc));
        Assert.That(sketchSegmentId.ToString(), Is.EqualTo("Sketch segment ID TestSketch-Arc-10-11"));
    }

    [Test]
    public void Constructor_FromLongs()
    {
        var sketchSegment = new Mock<SketchSegment>();
        sketchSegment.Setup(x => x.GetID()).Returns(new long[] { 20, 21 });
        sketchSegment.Setup(x => x.GetType()).Returns(2);
        var sketch = new Mock<Sketch>();
        sketch.As<Feature>().Setup(x => x.Name).Returns("AnotherSketch");
        sketchSegment.Setup(x => x.GetSketch()).Returns(sketch.Object);
        var sketchSegmentId = new SketchSegmentId(sketchSegment.Object);

        Assert.That(sketchSegmentId.Id0, Is.EqualTo(20));
        Assert.That(sketchSegmentId.Id1, Is.EqualTo(21));
        Assert.That(sketchSegmentId.SketchName, Is.EqualTo("AnotherSketch"));
        Assert.That(sketchSegmentId.ToString(), Is.EqualTo("Sketch segment ID AnotherSketch-Ellipse-20-21"));
    }

    [Test]
    public void Equals_GetHashcode()
    {
        var sketchSegment = new Mock<SketchSegment>();
        sketchSegment.Setup(x => x.GetID()).Returns(new long[] { 20, 21 });
        var sketch = new Mock<Sketch>();
        sketch.As<Feature>().Setup(x => x.Name).Returns("AnotherSketch");
        sketchSegment.Setup(x => x.GetSketch()).Returns(sketch.Object);

        var sketchSegmentId0 = new SketchSegmentId(sketchSegment.Object);
        var sketchSegmentId1 = new SketchSegmentId(sketchSegment.Object);

        Assert.That(sketchSegmentId0.Equals(new object()), Is.False);
        Assert.That(sketchSegmentId0.Equals(sketchSegmentId1), Is.True);
        Assert.That(sketchSegmentId0.GetHashCode(), Is.EqualTo(sketchSegmentId1.GetHashCode()));
    }
}
