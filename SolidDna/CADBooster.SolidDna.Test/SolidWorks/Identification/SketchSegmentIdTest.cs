using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;
#pragma warning disable CS0618 // Type or member is obsolete

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class SketchSegmentIdTest
{
    [SetUp]
    public void SetUpModelAndApplication()
    {
        var mockExtension = new Mock<ModelDocExtension>();
        mockExtension.Setup(x => x.GetPersistReference3(It.IsAny<object>())).Returns(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
        var mockModel = new Mock<ModelDoc2>();
        mockModel.Setup(x => x.Extension).Returns(mockExtension.Object);
        var model = new Model(mockModel.Object);
        var application = new Mock<ISolidWorksApplication>();
        application.Setup(x => x.ActiveModel).Returns(model);
        SolidWorksEnvironment.SetApplicationForTesting(application.Object);
    }

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
        Assert.That(sketchSegmentId.ToString(), Is.EqualTo("Sketch segment ID Arc-10-11"));
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
        Assert.That(sketchSegmentId.ToString(), Is.EqualTo("Sketch segment ID Ellipse-20-21"));
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
